using System.ComponentModel.DataAnnotations.Schema;

namespace SWECVI.ApplicationCore.Entities
{
    public class ValveData: BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        
        [NotMapped]
        public List<string> SelectList { get; set; }
    }
}
