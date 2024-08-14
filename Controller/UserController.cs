using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApi.Dtos;
using UsersApi.Dtos.Admin;
using UsersApi.Mapper;

namespace api.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserController(ApplicationDbContext applicationDBContext, UserManager<User> userManager)
        {
            _context = applicationDBContext;
            _userManager = userManager;
        }



        /*       // GET: api/users
               [HttpGet]
               public async Task<IActionResult> GetAllUsers()
               {
                   try
                   {
                       var users = await _context.Users
                                                 .Include(u => u.Role)
                                                 .Select(u => u.ToUsersPageDtoFromUserModel())
                                                 .ToListAsync();

                       if (users == null || !users.Any())
                       {
                           return NotFound("No users found");
                       }

                       return Ok(users);
                   }
                   catch (Exception ex)
                   {
                       // Log the exception (ex)
                       return StatusCode(500, "Internal server error");
                   }
               }

               // GET: api/users/{id}
               [HttpGet("{id}")]
               public async Task<IActionResult> GetUserById(string id)
               {
                   try
                   {
                       var user = await _context.Users
                                                .Include(u => u.Role)
                                                .Where(u => u.Id == id)
                                                .Select(u => u.ToUsersPageDtoFromUserModel())
                                                .FirstOrDefaultAsync();

                       if (user == null)
                       {
                           return NotFound("User not found");
                       }

                       return Ok(user);
                   }
                   catch (Exception ex)
                   {
                       // Log the exception (ex)
                       return StatusCode(500, "Internal server error");
                   }
               }

               // GET: api/users
               [HttpGet("withRolesandPermissions")]
               public async Task<IActionResult> GetAllUsersRole()
               {
                   try
                   {
                       var users = await _context.Users
                                                 .Include(u => u.Role)
                                                 .Select(u => u.ToUsersPostDtoFromUserModel())
                                                 .ToListAsync();

                       if (users == null || !users.Any())
                       {
                           return NotFound("No users found");
                       }

                       return Ok(users);
                   }
                   catch (Exception ex)
                   {
                       // Log the exception (ex)
                       return StatusCode(500, "Internal server error");
                   }
               }

              [HttpPost]
       public async Task<IActionResult> CreateUser([FromBody] UsersPostDto usersPostDto)
       {
           try
           {
               if (!ModelState.IsValid)
               {
                   return BadRequest(ModelState);
               }

               // Rechercher le rôle par RoleId
               var role = await _context.Roles
                                        .Include(r => r.Permissions) // Inclure les permissions du rôle
                                        .FirstOrDefaultAsync(r => r.Id == usersPostDto.RoleId);

               if (role == null)
               {
                   return NotFound("Role not found");
               }

               // Création de l'objet User
               var user = new User
               {
                   Id = usersPostDto.Id,
                   Name = usersPostDto.Name,
                   Email = usersPostDto.Email,
                   Password = usersPostDto.Password,
                   PhoneNumber = usersPostDto.PhoneNumber,
                   Role = role
               };

               _context.Users.Add(user);

               try
               {
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateException dbEx)
               {
                   // Capture de l'exception interne pour plus de détails
                   var innerException = dbEx.InnerException?.Message ?? dbEx.Message;
                   return StatusCode(500, $"Internal server error during save: {innerException}");
               }

               // Transformation en DTO pour la réponse
               var userDto = user.ToUsersPageDtoFromUserModel();
               return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDto);
           }
           catch (Exception ex)
           {
               // Log l'exception générale
               return StatusCode(500, $"Internal server error: {ex.Message}");
           }
       }
       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UsersPostDto usersPostDto)
       {
           try
           {
               if (!ModelState.IsValid)
               {
                   return BadRequest(ModelState);
               }

               // Rechercher l'utilisateur par ID
               var user = await _context.Users
                                        .Include(u => u.Role) // Inclure le rôle pour la mise à jour
                                        .FirstOrDefaultAsync(u => u.Id == id);

               if (user == null)
               {
                   return NotFound("User not found");
               }

               // Rechercher le rôle par RoleId
               var role = await _context.Roles
                                        .Include(r => r.Permissions) // Inclure les permissions du rôle
                                        .FirstOrDefaultAsync(r => r.Id == usersPostDto.RoleId);

               if (role == null)
               {
                   return NotFound("Role not found");
               }

               // Mise à jour des informations de l'utilisateur
               user.Name = usersPostDto.Name;
               user.Email = usersPostDto.Email;
               user.Password = usersPostDto.Password; // Assurez-vous de gérer les mots de passe de manière sécurisée
               user.PhoneNumber = usersPostDto.PhoneNumber;
               user.Role = role;

               _context.Users.Update(user);

               try
               {
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateException dbEx)
               {
                   // Capture de l'exception interne pour plus de détails
                   var innerException = dbEx.InnerException?.Message ?? dbEx.Message;
                   return StatusCode(500, $"Internal server error during save: {innerException}");
               }

               // Transformation en DTO pour la réponse
               var userDto = user.ToUsersPageDtoFromUserModel();
               return Ok(userDto);
           }
           catch (Exception ex)
           {
               // Log l'exception générale
               return StatusCode(500, $"Internal server error: {ex.Message}");
           }
       }

       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteUser([FromRoute] string id)
       {
           try
           {
               // Rechercher l'utilisateur par ID
               var user = await _context.Users
                                        .Include(u => u.Role) // Inclure le rôle si nécessaire pour la suppression
                                        .FirstOrDefaultAsync(u => u.Id == id);

               if (user == null)
               {
                   return NotFound("User not found");
               }

               _context.Users.Remove(user);

               try
               {
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateException dbEx)
               {
                   // Capture de l'exception interne pour plus de détails
                   var innerException = dbEx.InnerException?.Message ?? dbEx.Message;
                   return StatusCode(500, $"Internal server error during delete: {innerException}");
               }

               return NoContent(); // Indique que la suppression a réussi sans contenu à retourner
           }
           catch (Exception ex)
           {
               // Log l'exception générale
               return StatusCode(500, $"Internal server error: {ex.Message}");
           }
       }

       */
    }
}
