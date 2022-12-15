using HRApplication1.Auth;
using HRApplication1.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRApplication1.Entity
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
       // [Index("Ix_DepartmentName",  IsUnique = true)]
        public string DepartmentName { get; set; }// = "Sales";
        //public DepartmentType DepartmentType { get; set; } //= DepartmentType.Sales;
        //public string DepartmentTypeDesc { get; set; } //= "1"; 


        public AuditEntity AuditEntity { get; set; }
        //public string DepartmentId { get; internal set; }

        //void GetNothing()
        //{
        //    Id.
        //}
    }
    //public class BaseEntity
    //{
    //    public int Id { get; set; }
    //}
}
