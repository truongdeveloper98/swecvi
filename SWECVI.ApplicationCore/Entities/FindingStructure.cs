using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.Entities
{
    public class FindingStructure : BaseEntity
    {
        public int RowOrder { get; set; }
        public string TabName { get; set; } = default!;
        public string BoxHeader { get; set; } = default!;
        public string InputLabel { get; set; } = default!;
        public FindingInput InputType { get; set; } = default!;
        public string InputOptions { get; set; } = default!;
        public int? OrderInReport { get; set; } = default!;
        public ICollection<StudyFinding> StudyFindings { get; set; } = default!;
    }
}
