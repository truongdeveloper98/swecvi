namespace SWECVI.ApplicationCore.Entities
{
    public class DicomTags : BaseEntity
    {
        public string CV { get; set; } = default!;
        public string CSD { get; set; } = default!;
        public string CM { get; set; } = default!;
        public string SNOMED { get; set; } = default!;
        public int IndexContextID { get; set; } = default!;
        public ContextID ContextID { get; set; } = default!;
        public virtual ICollection<ManufacturerDicomParameters> DicomParameters1 { get; set; } = default!;
        public virtual ICollection<ManufacturerDicomParameters> DicomParameters2 { get; set; } = default!;
        public virtual ICollection<ManufacturerDicomParameters> DicomParameters3 { get; set; } = default!;
        public virtual ICollection<ManufacturerDicomParameters> DicomParameters4 { get; set; } = default!;
        public virtual ICollection<ManufacturerDicomParameters> DicomParameters5 { get; set; } = default!;
        public virtual ICollection<ManufacturerDicomParameters> DicomParameters6 { get; set; } = default!;
    }
}
