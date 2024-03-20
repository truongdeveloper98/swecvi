namespace SWECVI.ApplicationCore.ViewModels;

public class PythonCodeVersionDto
{
    public class CodeVersion
    {
        public string? FileName { set; get; }
        public List<PythonCode> Versions { set; get; }
    }
    public class PythonCode
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string? Script { get; set; }
        public bool IsCurrentVersion { get; set; }
    }

    public class CreatePythonCode
    {
        public string? Script { get; set; }
    }
}

