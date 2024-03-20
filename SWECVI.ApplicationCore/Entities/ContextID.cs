namespace SWECVI.ApplicationCore.Entities
{
    public class ContextID : BaseEntity
    {
        public string CV { get; set; } = default!;
        public string CSD { get; set; } = default!;
        public string CM { get; set; } = default!;
        public ICollection<DicomTags> ParameterCodes { get; set; } = default!;
    }
}
