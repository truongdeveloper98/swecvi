using System.ComponentModel.DataAnnotations.Schema;
using SWECVI.ApplicationCore.Common;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Entities
{
    public class Exam : BaseEntity
    {
        public Exam()
        {
            Parameters = new Dictionary<string, ParameterViewModel>();
        }

        public string DicomStudyId { get; set; }

        public string? Date { get; set; }
        public DateTime? FormattedDate { get; set; }

        public string? Time { get; set; }

        public TimeSpan? FormattedTime { get; set; }

        public int? Age { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }

        public string? BloodPressure { get; set; }

        public ICollection<string>? DicomSRXmls { get; set; }

        public ICollection<string>? Types { get; set; }

        public double? BSA { get; set; }

        public OldPatient Patient { get; set; }

        public bool IsParentExam { get; set; }

        public string? AccessNumber { get; set; }

        [NotMapped]
        public Dictionary<string, ParameterViewModel> Parameters { get; set; }

        public ICollection<HL7Measurement> HL7Measurements { get; set; }

        [NotMapped]
        public string FormattedBSA
        {
            get
            {
                if (this.BSA != null)
                {
                    return Math.Round(this.BSA.Value, 2).ToString();
                }

                return string.Empty;
            }
        }

        [NotMapped]
        public int? HeightInCm
        {
            get
            {
                if (this.Height != null)
                {
                    return (int)(this.Height * 100);
                }

                return null;
            }
        }

        [NotMapped]
        public List<DicomSRDto>? FormattedDicomSRXmls
        {
            get
            {
                List<DicomSRDto> dicomSRs = new List<DicomSRDto>();
                if (this.DicomSRXmls == null || this.DicomSRXmls.Count == 0)
                {
                    return null;

                }
                foreach (var xml in this.DicomSRXmls)
                {
                    dicomSRs.Add(xml.FromXml<DicomSRDto>());
                }
                return dicomSRs;
            }
        }
    }
}