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

        public DepartmentController(ApplicationDbContext context, IMapper mapper, ILogger<DepartmentController> logger, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
        }
        //POST: api/Department
        [HttpPost("createdepartment")]
        public async Task<ActionResult> CreateDepartment(string  DepartmentName)
        {
            try
            {
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
        //GET: api/Staff/Department
        //[HttpGet("getstaffbydepartment/{id}")]
       // await _context.Users.Where(x => x.DepartmentId == id).TolistAsycn()
        //public async Task<Result> GetStaffByDepartment(int id)
        //{
        //    try
        //    {
        //        var findStaff = await _context.s.Include(x => x.Department).Where(q => q.Department.Id == id).FirstOrDefaultAsync();
        //        if (findStaff == null)
        //        {
        //            return Result.Failure($"Staff with Id {id} not found");
        //        }
        //        else
        //        {
        //            return Result.Success("suceeded", findStaff);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Failure(ex.Message);
        //    }
        //}
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
        

