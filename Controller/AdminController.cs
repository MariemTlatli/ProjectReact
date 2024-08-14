using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UsersApi.Dtos.Admin;

namespace api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin", Policy = "AdminPolicy")]
    // [Authorize(Roles = "Admin" )]  

    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                return Ok(roles);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeRoleDto changeRoleDto)
        {
            try
            {
                // Rechercher l'utilisateur par email
                var user = await _userManager.FindByEmailAsync(changeRoleDto.Email);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Vérifier si le rôle spécifié existe
                var roleExists = await _roleManager.RoleExistsAsync(changeRoleDto.NewRole);
                if (!roleExists)
                {
                    return BadRequest("Role does not exist");
                }

                // Récupérer les rôles actuels de l'utilisateur
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Retirer l'utilisateur de ses rôles actuels
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return StatusCode(500, removeResult.Errors);
                }

                // Ajouter l'utilisateur au nouveau rôle
                var addRoleResult = await _userManager.AddToRoleAsync(user, changeRoleDto.NewRole);
                if (!addRoleResult.Succeeded)
                {
                    return StatusCode(500, addRoleResult.Errors);
                }

                return Ok("User role updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("admin-change-password")]
        public async Task<IActionResult> AdminChangeUserPassword([FromBody] AdminChangePasswordDto adminChangePasswordDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(adminChangePasswordDto.Email);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, adminChangePasswordDto.NewPassword);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok("Password changed successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var user = new User
                {
                    UserName = createUserDto.UserName,
                    Email = createUserDto.Email,
                    PhoneNumber = createUserDto.PhoneNumber // Add this line
                };

                var result = await _userManager.CreateAsync(user, createUserDto.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                // Optionally assign a role to the user
                if (!string.IsNullOrEmpty(createUserDto.Role))
                {
                    var roleExists = await _roleManager.RoleExistsAsync(createUserDto.Role);
                    if (roleExists)
                    {
                        await _userManager.AddToRoleAsync(user, createUserDto.Role);
                    }
                    else
                    {
                        return BadRequest("Role does not exist");
                    }
                }

                return Ok("User created successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }


        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(updateUserDto.Id);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                user.Email = updateUserDto.Email ?? user.Email;
                user.UserName = updateUserDto.UserName ?? user.UserName;
                user.PhoneNumber = updateUserDto.PhoneNumber ?? user.PhoneNumber; // Add this line

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok("User updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok("User deleted successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userDtos = new List<UserDto>();

                foreach (var user in users)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    userDtos.Add(new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        Role = role.FirstOrDefault(), // Assuming each user has only one role
                        PhoneNumber = user.PhoneNumber // Add this line
                    });
                }

                return Ok(userDtos);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

    }
}
