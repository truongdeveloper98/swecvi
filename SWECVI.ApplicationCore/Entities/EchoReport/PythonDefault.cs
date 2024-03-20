namespace SWECVI.ApplicationCore.Entities
{
    public class PythonDefault : BaseEntity
    {
        public string? FileName { set; get; }
        public string? Script { get; set; }
        public string? Path { get; set; }
        public ICollection<PythonCode> PythonCodes { get; set; } = new List<PythonCode>();
    }
}