namespace SWECVI.ApplicationCore.Entities
{
    public class StudyFinding : BaseEntity
    {
        public int StudyId { get; set; }
        public int FindingStructureId { get; set; }
        public string SelectOptions { get; set; } = default!;
        public Study Study { get; set; }
        public FindingStructure FindingStructure { get; set; }
    }

}
