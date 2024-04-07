namespace SWECVI.ApplicationCore.ViewModels
{
    public class ParameterSettingViewModel
    {
        public int Id { get; set; }
        public string ParameterId { get; set; } = default!;
        public bool ShowInChart { get; set; }
        public bool? ShowInParameterTable { get; set; }
        public virtual bool? ShowInAssessmentText { get; set; }
        public string? TableFriendlyName { get; set; }
        public string? TextFriendlyName { get; set; }
        public string? ParameterHeader { get; set; }
        public string? ParameterSubHeader { get; set; }
        public int? DisplayDecimal { get; set; }
        public int? ParameterOrder { get; set; }
        public int? ParameterHeaderOrder { get; set; }
        public virtual string? POH { get; set; }
        public string? Description { get; set; }
        public Enum.FunctionSelector? FunctionSelector { get; set; }
        public string FunctionSelectorName { get; set; }
        public string? DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
    }
}
