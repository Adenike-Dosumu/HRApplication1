using AutoMapper;
using HRApplication1.Auth;
using HRApplication1.Domain.DTOs;
using HRApplication1.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


//namespace HRApplication1.Controllers : ControllerBase
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//}
//{
//    [Route("api/[controller]")]
//[ApiController]
//public class UsersController : ControllerBase

//        private readonly ApplicationDbContext _context;
//        private readonly IMapper _mapper;
//        private readonly ILogger<UsersController> _logger;
//        private object createUser;

//        public UsersController(ApplicationDbContext context, IMapper mapper, ILogger<UsersController> logger)
//        {
//            _context = context;
//            _mapper = mapper;
//            _logger = logger;
//        }

//        public object createUsers { get; private set; }

//        // GET: api/Users
//        [HttpGet("getallusers")]
//        public async Task<Result> GetUsers()
//        {
//            try
//            {
//                _logger.LogInformation("Getting All users...");
//                var findUsers = await _context.Users.ToListAsync();
//                if (findUsers == null)
//                {
//                    return Result.Failure("Users Not Found");
//                }

//                return Result.Success("Suceeded, findUsers");

//            }
//            catch (Exception ex)
//            {
//                return Result.Failure(ex.Message);

//            }
//        }
//        // GET: api/Users/5
//        [HttpGet("getuserbyid/{id}")]
//        public async Task<Result> GetUser(int id)
//        {
//            try
//            {
//                var findUser = await _context.Users.Where(q => q.Id == id).FirstOrDefaultAsync();
//                if (_context.Departments.Count() <= 0)
//                    return Result.Failure($"User with Id {id} not found");
//                else
//                    return Result.Success("suceeded", findUser);
//            }
//            catch (Exception ex)
//            {
//                return Result.Failure(ex.Message);
//            }

//        }
//        [HttpPost("createuser")]
//        public async Task<Result> CreateUser(CreateUserDTO createuser)
//        {
//            try
//            {
//                var newUser = new User
//                {
//                    Email = createUser.Email,
//                    FirstName = createuser.FirstName,
//                    LastName =createuser.LastName,
//                    Role=createuser.Role,
//                    DOB=createuser.DOB,
//                    Status = Enum.Status.Active,
//                    DateCreated = DateTime.Now,
//                    CreatedBy = "",
//                    ModifiedBy = "",
//                    DateModified = DateTime.Now
//                };
//                _context.Users.AddAsync(newUser);
//                var nikeString = JsonSerializer.Serialize(newUser);
//                string nike = nikeString;
//                await _context.SaveChangesAsync();
//                return Result.Success("New User Created", newUser);
//                var mappedUser = _mapper.Map<User>(createuser);
//                _context.Users.Add(mappedUser);
//                await _context.SaveChangesAsync();

//                return Result.Success("User Created Successfully");
//            }
//            catch (Exception ex)
//            {
//                return Result.Failure(ex.Message);
//            }
//        }

//        // DELETE: api/Users/5
//        [HttpDelete("deleteuser/{id}")]
//        public async Task<Result> DeleteUser(int id, User user)
//        {
//            try
//            {
//                var findUser = _context.Users.Where(p => p.Id == id).FirstOrDefault();
//                if (findUser == null)
//                {
//                    return Result.Failure("User not found");
//                }
//                _context.Users.Remove(user);
//                await _context.SaveChangesAsync();

//                return Result.Success("User deleted Successfully");
//            }
//            catch (Exception ex)
//            {
//                return Result.Failure(ex.Message);
//            }
//        }
//        [HttpPut("updateuser/{id}")]
//        public async Task<Result> UpdateUser(int id, User user)
//        {
//            try
//            {
//                var findUser = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();
//                if (findUser == null)
//                    return Result.Failure("User Not Found");
//                else
//                {
//                    findUser.DOB = user.DOB;
//                    findUser.Status = user.Status;
//                    findUser.FirstName = user.FirstName;
//                    findUser.LastName = user.LastName;
//                    findUser.Email = user.Email;
//                    //findUser.UserRoles=user.UserRoles;
//                    findUser.Department = user.Department;

//                    _context.Update(findUser);
//                    await _context.SaveChangesAsync();
//                    return Result.Success("User updated successfully", findUser);
//                }

//            }
//            catch (Exception ex)
//            {
//                return Result.Failure(ex.Message);
//            }
//        }
//    }
//}
