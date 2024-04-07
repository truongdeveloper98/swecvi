using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.ViewModels
{
    public class FindingStructureViewModel
    {
        public int Id { get; set; }
        public int RowOrder { get; set; }
        public string TabName { get; set; } = default!;
        public string BoxHeader { get; set; } = default!;
        public string InputLabel { get; set; } = default!;
        public FindingInput InputType { get; set; } = default!;
        public string InputOptions { get; set; } = default!;
        public int? OrderInReport { get; set; } = default!;
    }
}
