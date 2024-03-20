

using System.ComponentModel.DataAnnotations;

namespace SWECVI.ApplicationCore.Entities
{
    public class SessionField : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string? English { get; set; }

        public string? Swedish { get; set; }
        public Session Session { get; set; }

    }
}
