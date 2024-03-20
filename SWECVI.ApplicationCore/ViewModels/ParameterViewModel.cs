using System.ComponentModel.DataAnnotations.Schema;
using SWECVI.ApplicationCore.Entities;

namespace SWECVI.ApplicationCore.ViewModels
{

    public class ParameterViewModel : BaseEntity
    {
        public ParameterViewModel()
        {
            AvailableReferences = new List<ParameterReference>();
        }

        public string ParameterId { get; set; }

        public string? UnitName { get; set; }

        //public string? DatabaseName { get; set; }

        public ParameterReference Reference { get; set; }

        public bool ShowInChart { get; set; }

        public bool? ShowInParameterTable { get; set; }

        public virtual bool? ShowInAssessmentText { get; set; }

        public virtual string? ParameterName { get; set; }

        public string? TableFriendlyName { get; set; }

        public string? TextFriendlyName { get; set; }

        public int? DisplayDecimal { get; set; }
        public string? ParameterHeader { get; set; }
        public string? ParameterSubHeader { get; set; }
        public int? ParameterOrder { get; set; }

        public virtual bool? Is4D { get; set; }

        public int? OrderInAssessment { get; set; }

        public virtual string? SourceUrl { get; set; }

        public virtual bool? SuppressReference { get; set; }

        /// <summary>
        /// Part of heart
        /// </summary>
        public virtual string? POH { get; set; }

        public virtual int? Priority { get; set; }

        public string? SRT { get; set; }

        public int TemParameterId { get; set; }
        public string? Description { get; set; }

        public Enum.FunctionSelector? FunctionSelector { get; set; }

        public virtual float? ResultValue { get; set; }

        /// <summary>
        /// Parameter name from EchoPAC
        /// </summary>
    
        ///public string EPParameterName { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual IList<ParameterReference> AvailableReferences { get; set; }

        public virtual string[] PartOfHeart
        {
            get
            {
                if (string.IsNullOrEmpty(POH))
                    return new string[] { };

                return POH.Split(',');
            }
        }

        public bool IsOutsideReferenceRange
        {
            get
            {
                var reference = Reference;
                var result = ResultValue;

                if (result == null || reference == null)
                {
                    return false;
                }

                var lower = reference.NormalRangeLower;
                var upper = reference.NormalRangeUpper;

                if (result.Value >= lower && result.Value <= upper)
                {
                    return false;
                }
                return true;
            }
        }
        public ParameterViewModel Clone()
        {
            return new ParameterViewModel
            {
                Id = Id,
                //EPParameterName = EPParameterName,
                ShowInChart = ShowInChart,
                ShowInParameterTable = ShowInParameterTable,
                ShowInAssessmentText = ShowInAssessmentText,
                ParameterName = ParameterName,
                TableFriendlyName = TableFriendlyName,
                TextFriendlyName = TextFriendlyName,
                DisplayDecimal = DisplayDecimal,
                Is4D = Is4D,
                Priority = Priority,
                SourceUrl = SourceUrl,
                POH = POH,
                UnitName = UnitName,
                Exam = Exam,
                AvailableReferences = AvailableReferences,
                SuppressReference = SuppressReference,
                FunctionSelector = FunctionSelector
            };
        }
        public void AssignFrom(ParameterViewModel parameter)
        {
            if (parameter != null)
            {
                Id = parameter.Id;
                //EPParameterName = parameter.EPParameterName;
                ShowInChart = parameter.ShowInChart;
                ShowInParameterTable = parameter.ShowInParameterTable;
                ShowInAssessmentText = parameter.ShowInAssessmentText;
                ParameterName = parameter.ParameterName;
                TableFriendlyName = parameter.TableFriendlyName;
                TextFriendlyName = parameter.TextFriendlyName;
                DisplayDecimal = parameter.DisplayDecimal;
                Is4D = parameter.Is4D;
                Priority = parameter.Priority;
                SourceUrl = parameter.SourceUrl;
                POH = parameter.POH;
                UnitName = parameter.UnitName;
                Exam = parameter.Exam;
                AvailableReferences = parameter.AvailableReferences;
                SuppressReference = parameter.SuppressReference;
            }
        }
    }

    public class ParameterAssessmentViewModel
    {
        public string? ParameterHeader { get; set; }
        public List<ParameterSubAssessmentViewModel> Measurements { get; set; }
    }

    public class ParameterSubAssessmentViewModel
    {
        public string? ParameterSubHeader { get; set; }
        public List<ExamsDto.Measurement> Measurements { get; set; }
    }
}