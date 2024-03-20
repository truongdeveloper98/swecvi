using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.Entities
{
    public class OldPatient: BaseEntity
    {
        public string? Name { get; set; }

        /// <summary>
        /// Social Security Number
        /// </summary>
        public string? SSN { get; set; }

        public string DicomPatientId { get; set; }

        public DateTime? LastExaminationDate { get; set; } = default;

        public string? Sex { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
