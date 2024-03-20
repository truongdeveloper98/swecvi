using System.ComponentModel.DataAnnotations.Schema;
using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.Entities
{
    public class ParameterReference : BaseEntity
    {
        public string? ParameterId { get; set; }
        public string? ParameterNameLogic { get; set; }
        public string? DisplayUnit { get; set; }
        public double? DepaermentId { get; set; }
        public double? AgeFrom { get; set; }
        public double? AgeTo { get; set; }
        public double? NormalRangeLower { get; set; }
        public double? NormalRangeUpper { get; set; }
        public double? MildlyAbnormalRangeLower { get; set; }
        public double? MildlyAbnormalRangeUpper { get; set; }
        public double? ModeratelyAbnormalRangeLower { get; set; }
        public double? ModeratelyAbnormalRangeUpper { get; set; }
        public double? SeverelyAbnormalRangeMoreThan { get; set; }
        public double? SeverelyAbnormalRangeLessThan { get; set; }
        public virtual Gender? Gender { get; set; }

        [NotMapped]
        public string? ReferenceRange { get; set; }

        [NotMapped]
        public virtual double MinAge
        {
            get
            {
                if (AgeFrom == null)
                {
                    return double.MinValue;
                }

                return AgeFrom.Value;
            }
        }

        [NotMapped]
        public virtual double MaxAge
        {
            get
            {
                if (AgeTo == null)
                {
                    return double.MaxValue;
                }

                return AgeTo.Value;
            }
        }
    }
}
