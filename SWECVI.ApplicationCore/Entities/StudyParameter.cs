namespace SWECVI.ApplicationCore.Entities
{
    public class StudyParameter : BaseEntity
    {

        public string ParameterId { get; set; }
        public float ResultValue { get; set; }
        public int FindingSite { get; set; }
        public int ImageView { get; set; }
        public int ImageMode { get; set; }
        public int CardiacCyclePoint { get; set; }
        public int RespiratoryCyclePoint { get; set; }
        public int MeasurementMethod { get; set; }
        public int IndexMeasurementUnit { get; set; }
        public int Derivation { get; set; }
        public int SelectionStatus { get; set; }
        public int DirectionOfFlow { get; set; }
        public int StudyIndex { get; set; }
        public ICollection<ParameterReference> ParameterReferences { get; set; }
        public Study HospitalStudy { get; set; }
        public string ValueUnit { get; set; }
    }
}
