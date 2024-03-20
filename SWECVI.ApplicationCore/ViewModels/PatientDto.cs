using SWECVI.ApplicationCore.Entities;

namespace SWECVI.ApplicationCore.ViewModels;

public class PatientDto
{
    public int Id { get; set; }
    
    public string? Name { get; set; }

    public string? SSN { get; set; }

    public string DicomPatientId { get; set; }

    public string? Sex { get; set; }

    public DateTime? LastExaminationDate { get; set; } = default;

    public int? NoOfExam { get; set; }

    public List<Exam>? Exams { get; set; }

    public ICollection<DateTime>? ExamDates { get; set;}
}

