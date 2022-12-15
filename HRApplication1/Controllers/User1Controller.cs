using AutoMapper;
using HRApplication1.Auth;
using HRApplication1.Domain.DTOs;
using HRApplication1.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Web;

namespace HRApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User1Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<User1Controller> _logger;

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public User1Controller(ApplicationDbContext context,
            IMapper mapper, ILogger<User1Controller> logger, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;

        }
        // GET: api/Users
        [HttpGet("getallusers")]
        public async Task<ActionResult> GetUsers()
        {
            try

            {
                _logger.LogInformation("Getting All users...");
                var findUsers = await _context.Users.ToListAsync();
                if (_context.Users == null)
                {
                    return NoContent();
                }

                return Ok(findUsers);

            }
            catch (Exception ex)
            {
                return BadRequest();

            }
        }
        //GET: api/User/5
        [HttpGet("getuserbyid/{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            try
            {
                var findUser = await _userManager.Users.FirstOrDefaultAsync(q => q.Id == id);
                if (findUser == null)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(findUser);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();


            }
        }
        //POST: api/User
        [HttpPost("createuser")]
        public async Task<ActionResult> CreateUser(CreateUserDTO createUser)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FirstName = createUser.FirstName,
                    LastName = createUser.LastName,
                    DOB = createUser.DOB,
                    DepartmentId = createUser.DepartmentId,
                    Email = createUser.EmailAddress,
                    UserName = createUser.EmailAddress,
                    PasswordHash = createUser.Password,
                    EmailConfirmed=true,

                    AuditEntity = new AuditEntity
                    {
                        Status = Enum.Status.Active,
                        CreatedBy = "NIKE"
                    }
                };

                var doesUserExist = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == createUser.EmailAddress.Trim());


                if (doesUserExist == null) //user does not exist in our db
                {

                    var result = await _userManager.CreateAsync(user, createUser.Password);
                    await _userManager.AddToRoleAsync(user, createUser.Role);


                    if (result.Succeeded) return Ok("User has been created");
                    else return BadRequest(result.Errors.Select(x=> x.Description));
                }

                return Ok("User already exist");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something happened");
                return BadRequest();
            }
        }
        [HttpPut("updatepassword{email}/{token}/{newPassword}")]
        public async Task<IActionResult> UpdateUser(string email, string token, string newPassword)
        {
            try
            {
                string newToken = HttpUtility.UrlDecode(token);
               var findUser= await _userManager.FindByEmailAsync(email);
                if (findUser == null)
                    return NotFound(); 
                else

                {
                
                    IdentityResult? res = await _userManager.ResetPasswordAsync(findUser, newToken, newPassword);
                    if (res.Succeeded)
                        return Ok("Password updated");
                    else
                    {
                        return BadRequest(res.Errors.Select(x=> x.Description));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("generatepasswordreset/{email}")]
        public async Task<ActionResult> GeneratePasswordReset(string email)
        {
            try
            {
                var res = await _userManager.FindByEmailAsync(email);
                if (res == null)
                    return NotFound();
                else
                {
                    var token=  await _userManager.GeneratePasswordResetTokenAsync(res);
                    return Ok(token);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        //api/UpdateEmail/Password
        [HttpPut("updateuser/{emailAddress}/{currentPassword}/{newPassword}")]
        public async Task<ActionResult> UpdateUserPassword(string emailAddress, string currentPassword, string newPassword)
        {
            try
            {
                ApplicationUser? findUser = await _userManager.FindByEmailAsync(emailAddress.Trim());
                if (findUser == null)
                    return NotFound("user is not found");
                else
                {
                    var result = await _userManager.ChangePasswordAsync(findUser, currentPassword, newPassword);
                    if (result.Succeeded)
                        return Ok("Password updated succesfully");
                    return BadRequest();
                    //return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something happened");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("deactivateuserstatus/{userName}")]
        public async Task<ActionResult> DeactivateUser(string userName)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);

                if (userName == null)
                {
                    return NotFound();
                }

                user.AuditEntity.Status = Enum.Status.Deactivated;
                user.AuditEntity.DateModified = DateTime.Now;
                user.AuditEntity.ModifiedBy = "Admin";

                var res = await _userManager.UpdateAsync(user);

                return Ok(); ;

            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("reactivateuserstatus/{userName}")]
        public async Task<ActionResult> ReactivateUser(string userName)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);

                if (userName == null)
                {
                    return NotFound();
                }

                user.AuditEntity.Status = Enum.Status.Active;
                user.AuditEntity.DateModified = DateTime.Now;
                user.AuditEntity.ModifiedBy = "Admin";

                var res = await _userManager.UpdateAsync(user);

                return Ok(); ;

            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
