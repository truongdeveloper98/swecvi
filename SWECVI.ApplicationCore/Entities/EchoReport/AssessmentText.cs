namespace SWECVI.ApplicationCore.Entities
{
    public class AssessmentText: BaseEntity
    {
        public int CallFunction { get; set; }

        public int EchoReportAssessmentTextId { get; set; }

        public int Level { get; set; }

        public string? Text { get; set; }
    }
}
