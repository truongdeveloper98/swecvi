namespace SWECVI.ApplicationCore.Entities
{
    public class AssessmentReportReference : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int ReportID { get; set; }
    }
}
