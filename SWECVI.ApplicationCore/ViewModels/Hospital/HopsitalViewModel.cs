namespace SWECVI.ApplicationCore.ViewModels.Hospital
{
    public class HopsitalViewModel
    {
        public int Id { get; set; }
        public string HospitalName { get; set; } = default!;
        public int IndexRegion { get; set; }
        public string? AdminEmail { get; set; }
        public string? AdminPassword { get; set; }
        public string ConnectionString { get; set; } = default!;
        public int? IndexDepartment { set; get; }

    }
}
