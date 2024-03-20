namespace SWECVI.ApplicationCore.ViewModels;

public class SessionDto
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? English { get; set; }
        public string? Swedish { get; set; }
        public ICollection<SessionField>? Fields { get; set; }

    }

    public class SessionField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? English { get; set; }
        public string? Swedish { get; set; }
        public int? SessionId { get; set; }
    }
}