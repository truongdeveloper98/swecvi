namespace SWECVI.ApplicationCore.ViewModels.ElasticSearch
{
    public class StudyElasticsearchModel
    {
        public string Id { get; set; }
        public string StudyId { get; set; }
        public string DicomPatientId { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string PatientStudyId { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public DateTime StudyDateTime { get; set; }
        public string HospitalId { get; set; }
    }
}
