using HRApplication1.Auth;
using HRApplication1.Enum;
using Microsoft.AspNetCore.Identity;

namespace HRApplication1.Entity
{
    public class ApplicationRole:IdentityRole
    {
        public AuditEntity AuditEntity { get; set; }


    }
}
