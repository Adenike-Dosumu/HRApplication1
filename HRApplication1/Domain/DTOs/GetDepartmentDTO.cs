using HRApplication1.Auth;

namespace HRApplication1.Domain.DTOs
{
    public class GetDepartmentDTO:AuditEntity
    {
        public string DepartmentName { get; set; }
    }
}
