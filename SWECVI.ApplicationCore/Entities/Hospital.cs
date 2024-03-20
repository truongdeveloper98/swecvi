namespace SWECVI.ApplicationCore.Entities
{
    public class Hospital : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int IndexRegion { get; set; }
        public int? IndexDepartment { get; set; }
        public string ConnectionString { get; set; } = default;
        public Region Region { get; set; } = default!;
        public ICollection<Department> Departments { get; set; }
    }
}
