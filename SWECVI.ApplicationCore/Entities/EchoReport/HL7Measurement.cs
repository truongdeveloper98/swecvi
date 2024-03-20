namespace SWECVI.ApplicationCore.Entities
{
    public class HL7Measurement: BaseEntity
    {
        public virtual string? TextValue { get; set; }

        public virtual string? CodeMeaning { get; set; }

        public virtual string? CodingSchemeDesignator { get; set; }

        public virtual string? CodeValue { get; set; }

        public Exam Exam { get; set; }
    }
}
