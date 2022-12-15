using DocuSign.eSign.Model;     
using HRApplication1.Auth;
using HRApplication1.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApplication1.Entity
{
    public class ApplicationUser : IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }

        public DateTime DOB { get; set; }
        
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        //public string Department { get; set; }=Enum.DepartmentType

        public AuditEntity AuditEntity { get; set; }

    }

    
}
