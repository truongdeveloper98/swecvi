namespace SWECVI.ApplicationCore.Entities
{
    public class Region : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ICollection<Hospital> Hospitals { get; set; } = default!;
    }
}
