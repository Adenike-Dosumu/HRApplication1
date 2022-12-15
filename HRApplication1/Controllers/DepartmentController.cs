using AutoMapper;
using HRApplication1.Auth;
using HRApplication1.Domain.DTOs;
using HRApplication1.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HRApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DepartmentController(ApplicationDbContext context, IMapper mapper, ILogger<DepartmentController> logger, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        //POST: api/Department
        [HttpPost("createdepartment")]
        public async Task<ActionResult> CreateDepartment(string DepartmentName)
        {
            try
            {
               var res= _context.Departments.Where(d=> d.DepartmentName.ToLower().Trim()==DepartmentName.ToLower().Trim()).ToList();
                if (res.Count > 0)
                    return BadRequest("Department already exist");
                
                var newDept = new Department
                {
                    DepartmentName = DepartmentName,
                    AuditEntity = new AuditEntity
                    {
                        Status = Enum.Status.Active,
                        CreatedBy
                    = "NIKE"
                    }
                  
                };
                await _context.Departments.AddAsync(newDept);
                var nikeString = JsonSerializer.Serialize(newDept);
                string nike = nikeString;
                await _context.SaveChangesAsync();
                return Ok("New Department Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getUsersByDepartment/{id}")]
        public async Task<ActionResult> GetUsersByDepartment(int id)
        {
            try
            {
                var res = await _context.Departments.FindAsync(id);
                if (res == null)
                    return NotFound("Department not found");
                else
                {
                    var findUsers = await _userManager.Users.Where(s => s.DepartmentId == id).ToListAsync();
                    var UserDTOList= new List<CreateUserDTO>();
                    foreach (var user in findUsers)
                    {
                        UserDTOList.Add(new CreateUserDTO
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            DOB = user.DOB
                        });
                    }
                    return Ok(UserDTOList);
                }
                   
                    
            }
            catch (Exception ex)
            {

                return StatusCode(500,"An error occurred");
            }
        }
        
        //GET: api/Department
        [HttpGet("getalldepartments")]
        public async Task<Result> GetDepartment()
        {
            try
            {
                _logger.LogInformation("Getting All departments...");
                var findDepartments = await _context.Departments.ToListAsync();
                if (_context.Departments.Count() <= 0)
                {
                    return Result.Failure("Departments not found");
                }
                return Result.Success(findDepartments);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
        [HttpPut("updatedepartment/{Id}/{newDepartName}")]

        public async Task<ActionResult> UpdateDepartment(int Id, string newDepartName)
        {
            try
            {

               var department = await _context.Departments.FindAsync(Id);
                if (department != null)
                {
                    department.DepartmentName = newDepartName;
                    department.AuditEntity.DateModified = DateTime.Now;
                      await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest("Department does not exist");
               
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpPut("deactivatedepartmentstatus/{departmentName}")]
        public async Task<ActionResult> DeactivateDepartment(string departmentName)
        {
            try
            {
                var res = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentName == departmentName);

                if (departmentName == null)
                {
                    return NotFound();
                }
                

                    res.AuditEntity.Status = Enum.Status.Deactivated;
                    res.AuditEntity.DateModified = DateTime.Now;
                    res.AuditEntity.ModifiedBy = "NIke";

                var result = _context.SaveChangesAsync(departmentName);

                return Ok("Department has been deactivate"); 

            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut("reactivatedepartmentstatus/{departmentName}")]
        public async Task<ActionResult> ReactivateDepartment(string departmentName)
        {
            try
            {
                var res = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentName == departmentName);

                if (departmentName == null)
                {
                    return NotFound();
                }
               
                res.AuditEntity.Status = Enum.Status.Active;
                res.AuditEntity.DateModified = DateTime.Now;
                res.AuditEntity.ModifiedBy = "NIke";
               
                var result = _context.SaveChangesAsync(departmentName);

                return Ok("department has been reactivate");
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
        

