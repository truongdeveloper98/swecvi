namespace SWECVI.ApplicationCore.ViewModels.ElasticSearch
{
    public class PatientElasticSearchModel
    {
        public string Id { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string? Sex { get; set; }
        public DateTime? DOB { get; set; }
        public string HospitalId { get; set; }
    }
}
