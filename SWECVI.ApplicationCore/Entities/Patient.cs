namespace SWECVI.ApplicationCore.Entities
{
    public class Patient : BaseEntity
    {
        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime DOB { get; set; }
        public ICollection<Study> Studies { get; set; } = default!;
    }
}
