using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.Entities
{
    public class ExamParameterOld : BaseEntity
    {
        public string? ParameterName { get; set; }
        public string? TableFriendlyName { get; set; }
        public string? Unit { get; set; }
        public decimal? Value { get; set; }
        public string? Reference { get; set; }
        public bool? IsOutsideReferenceRange { get; set; }
        public int? DisplayDecimal { get; set; }
        public Enum.FunctionSelector? SelectedFunction { get; set; }
        public Exam Exam { get; set; }
    }
}
