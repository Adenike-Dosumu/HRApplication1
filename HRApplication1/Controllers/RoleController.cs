using AutoMapper;
using HRApplication1.Auth;
using HRApplication1.Domain.DTOs;
using HRApplication1.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HRApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleController> _logger;


        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManger;

        public RoleController(ApplicationDbContext context,
            IMapper mapper, ILogger<RoleController> logger, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _userManger = userManager;

        } // GET: api/Roles
        [HttpGet("getallroles")]
        public async Task<ActionResult> GetRoles()
        {
            try

            {
                _logger.LogInformation("Getting All roles...");
                var findRoles = await _context.Roles.ToListAsync();
                if (_context.Roles == null)
                {
                    return NoContent();
                }

                return Ok(findRoles);

            }
            catch (Exception ex)
            {
                return BadRequest();

            }
        }
        //GET: api/Role/5
        [HttpGet("getrolebyid/{id}")]
        public async Task<Result> GetRole(string id)
        {
            try
            {
                var findRole = await _context.Roles.Where(q => q.Id == id).FirstOrDefaultAsync();
                if (findRole == null)
                {
                    return Result.Failure($"Role with Id {id} not found");
                }
                else
                {
                    return Result.Success("suceeded", findRole);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);

            }
        }
        //POST: api/Role
        [HttpPost("createrole/{name}")]
        public async Task<ActionResult> CreateRole(string name)
        {
            try
            {
                var role = new ApplicationRole
                {
                    Name = name,
                    AuditEntity = new AuditEntity
                    {
                        Status = Enum.Status.Active,
                        CreatedBy
                    = "NIKE"
                    }
                };

                var doesRoleExist = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == name.Trim());



                if (doesRoleExist == null) //role doest not exist in our db
                {

                    IdentityResult result = await _roleManager.CreateAsync(role);


                    if (result.Succeeded) return Ok("role has been created");
                    else return BadRequest("");
                }


                return Ok("Role already exist");


                // return Result.Success("Role Created Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something happened");
                return BadRequest();
            }
        }

        [Authorize(Roles  ="Admin")]
        [HttpPut("deactivaterolestatus/{roleName}")]
        public async Task<ActionResult> DeactivateRole(string roleName)
        {
            try
            {
                var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == roleName);

                if (role == null)
                {
                    return NotFound();
                }

                var users = await _userManger.GetUsersInRoleAsync(roleName);

                if (users.Count == 0)
                {

                    role.AuditEntity.Status = Enum.Status.Deactivated;
                    role.AuditEntity.DateModified = DateTime.Now;
                    role.AuditEntity.ModifiedBy = "NIke";
                }
                else
                {
                    return BadRequest("You cannot deactivate this role because there are users currently assigned to it");
                }

                var res = await _roleManager.UpdateAsync(role);

                return Ok(); ;


            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut("activaterolestatus/{roleName}")]
        public async Task<ActionResult> ActivateRole(string roleName)
        {
            try
            {
                var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == roleName);

                if (role == null)
                {
                    return NotFound();
                }


                    role.AuditEntity.Status = Enum.Status.Active;
                    role.AuditEntity.DateModified = DateTime.Now;
                    role.AuditEntity.ModifiedBy = "NIke";
               
                var res = await _roleManager.UpdateAsync(role);

                return Ok("Role is Active");

            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updaterole/{roleName}/{newRoleName}")]
        public async Task<ActionResult> UpdateRole(string roleName, string newRoleName)
        {
            try
            {
                var findRole = await _roleManager.Roles.FirstOrDefaultAsync(s => s.Name == roleName.Trim());
                if (findRole == null)
                {
                    return NotFound("Role Not Found");
                }

                else
                {

                    findRole.Name = newRoleName.Trim();
                    findRole.AuditEntity.DateModified = DateTime.Now;
                   
                    var result = await _roleManager.UpdateAsync(findRole);
                    if (result.Succeeded)
                    {
                        return Ok("Role has been updated");
                    }
                    else
                    {
                        return BadRequest();
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something happened");

                return BadRequest();
            }
        }
        [HttpDelete("deleterole/{role}")]
        public async Task<ActionResult> DeleteRole(string role)
        {
            try
            {
                var users = await _userManger.GetUsersInRoleAsync(role);
                
                if (users.Count > 0)
                {  
                    return BadRequest( $"You cannot delete this role {role} because there are users currently assigned to it");

                }
                var findRole = await _roleManager.Roles.FirstOrDefaultAsync(s => s.Name == role);
                if (findRole == null)
                    return NotFound();
                await _roleManager.DeleteAsync(findRole);
               

                    return Ok("Role deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
