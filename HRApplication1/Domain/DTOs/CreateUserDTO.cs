namespace HRApplication1.Domain.DTOs
{
    public class CreateUserDTO
    {
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Department { get; set; }
        public int DepartmentId { get; set; }

        public DateTime DOB { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get;  set; }
        public string Role { get;  set; }

    }
}
