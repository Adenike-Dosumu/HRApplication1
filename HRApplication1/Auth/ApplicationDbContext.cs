using HRApplication1.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HRApplication1.Auth
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
   // public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationUser,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //public DbSet<User> Users { get; set; } 
        //public DbSet<Role> Roles { get; set; } 
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

        internal Task GetTypeDepartmentAsync(string departmentName)
        {
            throw new NotImplementedException();
        }

        internal Task SaveChangesAsync(string departmentName)
        {
            throw new NotImplementedException();
        }
    }
}
