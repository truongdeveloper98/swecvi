using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.ViewModels
{
    public class ParameterReferenceViewModel
    {
        public double Id { get; set; }
        public string? ParameterId { get; set; }
        public string? ParameterNameLogic { get; set; }
        public string? DisplayUnit { get; set; }
        public double? DepaermentId { get; set; }
        public double? DepartmentName { get; set; }
        public double? AgeFrom { get; set; }
        public double? AgeTo { get; set; }
        public double? ReferenceLow { get; set; }
        public double? ReferenceMin { get; set; }
        public double? ReferenceMax { get; set; }
        public double? NormalRangeLower { get; set; }
        public double? NormalRangeUpper { get; set; }
        public double? MildlyAbnormalRangeLower { get; set; }
        public double? MildlyAbnormalRangeUpper { get; set; }
        public double? ModeratelyAbnormalRangeLower { get; set; }
        public double? ModeratelyAbnormalRangeUpper { get; set; }
        public double? SeverelyAbnormalRangeMoreThan { get; set; }
        public double? SeverelyAbnormalRangeLessThan { get; set; }
        public string GenderName { get; set; }
        public Gender? Gender { get; set; }
    }
}
