namespace SWECVI.ApplicationCore.Entities
{
    public class Study : BaseEntity
    {
        public float Weight { get; set; }
        public float Height { get; set; }
        public int Age { get; set; }
        public string StudyDescription { get; set; }
        public DateTime StudyDateTime { get; set; }
        public string StudyInstanceUID { get; set; }
        public string StudyID { get; set; }
        public string InstitutionName { get; set; }
        public int PatientId { get; set; }
        public string SOPClassUID { get; set; }
        public string SOPInstanceUID { get; set; }
        public string AccessionNumber { get; set; }
        public string InstitutionalDepartmentName { get; set; }
        public string ModalitiesInStudy { get; set; }
        public bool QualityControlSubject { get; set; }
        public float BodySurfaceArea { get; set; }
        public int IndexDepartment { get; set; }
        public string SystolicBloodPressure { get; set; }
        public string DiastoilccBloodPressure { get; set; }
        public string SoftwareVersion { get; set; }
        public string Manufacture { get; set; }
        public string ManufactureModel { get; set; }
        public DateTime ContentDateTime { get; set; }
        public Patient Patient { get; set; }
        public ICollection<StudyFinding> StudyFindings { get; set; }
        public ICollection<StudyParameter> Parameters { get; set; }
    }
}
