using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.ViewModels.MirthConnect;
public class StudyFindingViewModel
{
    public int? Id {get;set;}
    public int StudyId { get; set; }
    public int HospitalId { get; set; }
    public List<FingdingStudyItem> FingdingStudyItems { get; set; }
}

public class FingdingStudyItem
{
    public int Id { get; set; }
    public string Value { get; set; } = default!;
    public FindingInput? InputType { get; set; }
    public string? InputLabel { get; set; } = default!;
    public string? TabName { get; set; } = default!;
    public int? OrderInReport { get; set; }
}