namespace SWECVI.ApplicationCore.ViewModels;

public class ParameterDto
{
    public class ParameterSelector
    {
        public int Id { get; set; }
        public string? ParameterName { get; set; }
    }
    public class ParameterStaticChart
    {
        public string? XValue { set; get; }
        public decimal? YValue { get; set; }
    }

    public class ParameterValuesChartByPatientId
    {
        public string? ParameterName { set; get; }
        public List<ParameterDto.ValueByTime>? ValueByTimes { get; set; }
    }

    public class ParameterValue
    {
        public int Id { get; set; }
        public string? UnitName { get; set; }
        public string? DatabaseName { get; set; }
        public bool ShowInChart { get; set; }
        public bool? ShowInParameterTable { get; set; }
        public virtual bool? ShowInAssessmentText { get; set; }
        public virtual string? ParameterName { get; set; }
        public string? TableFriendlyName { get; set; }
        public string? TextFriendlyName { get; set; }
        public int? DisplayDecimal { get; set; }
        public virtual bool? Is4D { get; set; }
        public int? OrderInAssessment { get; set; }
        public string? SourceUrl { get; set; }
        public bool? SuppressReference { get; set; }
        public string? POH { get; set; }
        public virtual int? Priority { get; set; }
        public string? SRT { get; set; }
        public string? Description { get; set; }
        public int? FunctionSelector { set; get; }
    }
    public class ValueByTime
    {
        public DateTime? Time { set; get; }
        public decimal? Value { get; set; }
    }

    public class ValueByHL7Measurement
    {
        public string? CodeMeaning { set; get; }
        public string? TextValue { get; set; }
    }

    public class EnumFunctionSelectorModel
    {
        public string? FunctionName { get; set; }
        public int? Value { get; set; }
    }
}