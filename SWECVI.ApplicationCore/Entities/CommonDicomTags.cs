namespace SWECVI.ApplicationCore.Entities
{
    public class CommonDicomTags : BaseEntity
    {
        public int Element { get; set; }
        public double Tag { get; set; }
        public string CV { get; set; } = default!;
        public string CSD { get; set; } = default!;
        public string CM { get; set; } = default!;
    }
}
