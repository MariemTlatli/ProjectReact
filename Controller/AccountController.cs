using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApi.Dtos.Account;
using UsersApi.Service;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, TokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await RegisterUser(registerDto, "Admin");
            return response;
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await RegisterUser(registerDto, "User");
            return response;
        }

        private async Task<IActionResult> RegisterUser(RegisterDto registerDto, string role)
        {
            try
            {
                var userExists = await _userManager.Users.AnyAsync(x => x.Email.ToLower() == registerDto.Email.ToLower());
                if (userExists)
                    return BadRequest("Email is already in use");

                var phoneExists = await _userManager.Users.AnyAsync(x => x.PhoneNumber == registerDto.PhoneNumber);
                if (phoneExists)
                    return BadRequest("Phone number is already in use");

                var appUser = new User
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber // Utilize the existing PhoneNumber property
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (!createdUser.Succeeded)
                    return StatusCode(500, createdUser.Errors);

                var roleResult = await _userManager.AddToRoleAsync(appUser, role);
                if (!roleResult.Succeeded)
                    return StatusCode(500, roleResult.Errors);

                var roles = await _userManager.GetRolesAsync(appUser);
                return Ok(new NewUserDto
                {
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber, // Include the phone number in the response if needed
                    Token = _tokenService.CreateToken(appUser, roles)
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == loginDto.Email.ToLower());
            if (user == null)
                return Unauthorized("Invalid Email!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Email not found and/or password incorrect");

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new NewLoginUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user, roles)
            });
        }
    }
}
