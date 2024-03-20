namespace SWECVI.ApplicationCore.Entities
{
    public class Finding : BaseEntity
    {
        public int IndexExam { get; set; }
        public int TricuspidValveFinding { get; set; }
        public int MitralValveFinding { get; set; }
        public int AorticValveFinding { get; set; }
        public int PulmonicValveFinding { get; set; }
        public int ECGFinding { get; set; }
        public int IndexDiagnose { get; set; }
        public int PatientStatus { get; set; }
    }
}
