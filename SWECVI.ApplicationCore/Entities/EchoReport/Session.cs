

using System.ComponentModel.DataAnnotations;

namespace SWECVI.ApplicationCore.Entities
{
    public class Session : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string? English { get; set; }

        public string? Swedish { get; set; }

        public ICollection<SessionField>? SessionFields { get; set; }
    }
}
