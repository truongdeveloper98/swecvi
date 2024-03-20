using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWECVI.ApplicationCore.ViewModels.MirthConnect
{
    public class PatientStudyViewModel
    {
            public int Id { get; set; }
            public string DicomStudyId { get; set; }
            public string? Date { get; set; }
            public string? Time { get; set; }
            public double? Height { get; set; }
            public double? Weight { get; set; }
            public int PatientId { get; set; }
    }

    public class StudyReport
    {
        public IEnumerable<HospitalParameterVM> Measurements { get; set; }
        public string? AssessmentText { get; set; }
        public string? StressText { get; set; }
    }

    public class HospitalParameterVM
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? FriendlyName { get; set; }
        public string? Unit { get; set; }
        public float? Value { get; set; }
        public string? Reference { get; set; }
        public bool? Is4D { get; set; }
        public string POH { get; set; }
        public bool? IsOutsideReferenceRange { get; set; }
        public int? DisplayDecimal { get; set; }
        public int? SelectedFunction { set; get; }

    }

}
