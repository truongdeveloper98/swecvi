namespace SWECVI.ApplicationCore.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public int IdentityId { get; set; }
        public int? IndexDepartment { get; set; }
        public bool IsActive { get; set; } = true;
        public AppUser Identity { get; set; } = default!;
    }
}