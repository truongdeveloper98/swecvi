namespace SWECVI.ApplicationCore.Entities
{
    public class SystemLog : BaseEntity
    {
        public string Message { get; set; } = default;
        public string Status { get; set; } = default;
    }
}
