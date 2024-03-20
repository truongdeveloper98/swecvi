namespace SWECVI.ApplicationCore.Entities
{
    public class PythonCode : BaseEntity
    {
        public string? Script { get; set; }
        public bool IsCurrentVersion { get; set; } = false;
        public bool IsDefault { get; set; } = false;
        public int Version { get; set; } = 1;
        public string? Path { set; get; }
        public int PythonDefaultId { get; set; }
        public PythonDefault PythonDefault { get; set; }
    }
}