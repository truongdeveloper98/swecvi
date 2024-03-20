namespace SWECVI.ApplicationCore.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int SendingUnit { get; set; }
        public string Modality { get; set; } = default!;
        public string Location { get; set; } = default!;
        public int IndexHospital { get; set; }
        public Hospital Hospital { get; set; }
    }
}
