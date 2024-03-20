namespace SWECVI.ApplicationCore.Entities
{
    public class AssessmentTextReference : BaseEntity
    {
        public string DescriptionReportText { get; set; } = default!;
        public string CallFunction { get; set; } = default!;
        public string? ReportTextSE { get; set; }
        public int DCode { get; set; }
        public int ACode { get; set; }
    }
}
