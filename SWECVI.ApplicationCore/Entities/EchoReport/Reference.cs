//using System.ComponentModel.DataAnnotations.Schema;
//using static SWECVI.ApplicationCore.Enum;

//namespace SWECVI.ApplicationCore.Entities
//{
//  public class Reference: BaseEntity
//  {
//    public virtual int? AgeFrom { get; set; }

//    public virtual int? AgeTo { get; set; }

//    public virtual decimal? Min { get; set; }

//    public virtual decimal? Max { get; set; }

//    public virtual decimal? Low { get; set; }

//    public virtual decimal? NormalLower { get; set; }

//    public virtual decimal? NormalUpper { get; set; }

//    public virtual decimal? MildlyLower { get; set; }

//    public virtual decimal? MildlyUpper { get; set; }

//    public virtual decimal? ModeratelyLower { get; set; }

//    public virtual decimal? ModeratelyUpper { get; set; }

//    public virtual decimal? SeverelyLimit { get; set; }

//    public virtual int ParameterId { get; set; }

//    public string? ParameterName { get; set; }
//    public bool? IsCalculated { get; set; }
//    public bool? UsedInCode { get; set; }
//    public string? ScanMode { get; set; }

//    public string? View { get; set; }

//    public int? RaceKey { get; set; }

//    public string? Unit { get; set; }

//    public string? ToolTip { get; set; }

//    public string? PrimaryReference { get; set; }

//    public string? SecondaryReference { get; set; }

//    public string? Commentary { get; set; }

//    public virtual Gender? Gender { get; set; }

//    [NotMapped]
//    public virtual int MinAge
//    {
//      get
//      {
//        if (this.AgeFrom == null)
//        {
//          return int.MinValue;
//        }

//        return this.AgeFrom.Value;
//      }
//    }

//    [NotMapped]
//    public virtual int MaxAge
//    {
//      get
//      {
//        if (this.AgeTo == null)
//        {
//          return int.MaxValue;
//        }

//        return this.AgeTo.Value;
//      }
//    }
//    [NotMapped]
//    public string ReferenceRange { get; set; }
//  }
//}
