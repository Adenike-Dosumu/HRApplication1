using HRApplication1.Enum;
using Microsoft.EntityFrameworkCore;

namespace HRApplication1.Auth
{

    [Owned]
    public class AuditEntity
    {

        //public string Name { get; set; }
        public DateTime DateCreated { get; set; }= DateTime.Now;
        public string CreatedBy { get; set; } = "NIKE";
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }// = "";
        public Status Status { get; set; }// = Status.Active;
        //public string StatusDescription { get { return Status.ToString(); } }

    }
}
