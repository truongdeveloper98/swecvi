using SWECVI.ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace SWECVI.ApplicationCore.ViewModels
{
    public class UserInformationDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = default!;
        [Required]
        public string LastName { get; set; } = default!;
        public string PhoneNumber { set; get; } = default!;
        [Required]
        public string[] Roles { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Password { set; get; }
        public string Role { set; get; }
        public string Department { set; get; }
        public bool IsActive { set; get; } = true;
        public int? IndexDepartment { set; get; }
        public AppUser AppUser { get; set; }
    }
}
