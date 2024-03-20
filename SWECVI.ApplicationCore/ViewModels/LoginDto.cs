using System.ComponentModel.DataAnnotations;
namespace SWECVI.ApplicationCore.ViewModels;

public class LoginDto
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int HospitalId { get; set; } = 2;

        public bool RememberMe { get; set; } = false;
    }
    
    public class LoginResult 
    {
        public string Token { get; set; }
    }
}

