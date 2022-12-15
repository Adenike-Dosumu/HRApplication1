using System.ComponentModel.DataAnnotations;

namespace HRApplication1.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "EmailAddress is required")]
        [EmailAddress(ErrorMessage= "What you typed is not an EmailAddress")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
