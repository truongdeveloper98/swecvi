namespace SWECVI.ApplicationCore.ViewModels
{
    public class ExportStudyParameterViewModel
    {

        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string Sex { get; set; }
        public DateTime DOB { get; set; }
        public string StudyId { get; set; }
        public string StudyDescription { get; set; }
        public string StudyInstanceUID { get; set; }
        public string InstitutionName { get; set; }
        public string SOPClassUID { get; set; }
        public string SOPInstanceUID { get; set; }
        public string AccessionNumber { get; set; }
        public float ResultValue { get; set; }
        public string ValueUnit { get; set; }
        public string ParameterNameLogic { get; set; }
    }
}
