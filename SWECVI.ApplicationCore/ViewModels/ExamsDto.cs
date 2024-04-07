using SWECVI.ApplicationCore.Entities;

namespace SWECVI.ApplicationCore.ViewModels
{
    public class ExamsDto
    {
        public class Exam
        {
            public int Id { get; set; }
            public string DicomStudyId { get; set; }
            public string? Date { get; set; }
            public string? Time { get; set; }
            public int? Age { get; set; }
            public double? Height { get; set; }
            public double? Weight { get; set; }
            public string? BloodPressure { get; set; }
            public double? BSA { get; set; }
            public string? AccessionNumber { get; set; }
        }

        public class ExamDetail
        {
            public int Id { get; set; }
            public string DicomPatientId { get; set; }
            public string? Date { get; set; }
            public string? Time { get; set; }
            public int PatientId { get; set; }
            public string PatientName { get; set; }
        }

        public class ExamSelector
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class ExamIdSelector
        {
            public int Id { get; set; }
        }

        public class ExamsByYear
        {
            public int ExamsCount { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        }

        public class ExamReport
        {
            public IEnumerable<ExamsDto.Measurement> Measurements { get; set; }
            public string? AssessmentText { get; set; }
            public string? StressText { get; set; }
            public string SSN { get; set; }
            public string ExamDate { get; set; }
            public string PatientName { get; set; }
            public string? StudyType { get; set; }
        }

        public class Measurement
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public string? FriendlyName { get; set; }
            public string? Unit { get; set; }
            public decimal? Value { get; set; }
            public string? Reference { get; set; }
            public string? ParameterHeader { get; set; }
            public string? ParameterSubHeader { get; set; }
            public int? ParameterOrder { get; set; }
            public bool? Is4D { get; set; }
            public string[] POH { get; set; }
            public bool? IsOutsideReferenceRange { get; set; }
            public int? DisplayDecimal { get; set; }
            public int? SelectedFunction { set; get; }

        }

        public class HL7Measurement
        {
            // session name
            public string? N { get; set; }
            public string? EL { get; set; }
            public string? SL { get; set; }
            // session fields
            public ICollection<ExamsDto.SessionField>? Fs { get; set; }

        }

        public class SessionField
        {
            public string? N { get; set; }

            // session field labels
            public string? EL { get; set; }

            public string? SL { get; set; }

            // session field value
            public string? V { get; set; }
        }

        public class ExamType
        {
            public int Count { get; set; }
            public string Type { get; set; }
        }
        public class ExamsCount
        {
            // hours / date / month / year
            public int Time { get; set; }
            public int Count { get; set; }
        };

        public class CodeMeaningChart
        {
            public string CodeMeaning { get; set; }
            public List<TextValueByCodeMeaning> TextValueByCodeMeanings { get; set; } = new List<TextValueByCodeMeaning>();
        };
        public class TextValueByCodeMeaning
        {
            public string TextValue { get; set; }
            public int Count { get; set; }
        };
    }
}