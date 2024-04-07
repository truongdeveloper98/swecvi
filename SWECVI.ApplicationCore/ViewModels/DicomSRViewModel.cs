using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SWECVI.ApplicationCore.ViewModels
{
    [XmlRoot("MeasurementExport")]
    [XmlType("MeasurementExport")]
    public class DicomSRViewModel
    {
        public PatientModel Patient { get; set; }

        public class PatientModel
        {
            public DateTime Birthdate { get; set; }
            public string FirstName { get; set; }
            public object IssuerOfPatientId { get; set; }
            public string LastName { get; set; }
            public string PatientId { get; set; }
            public long PatientIdValidationStatus { get; set; }
            public string Sex { get; set; }
            public OtherPatientId OtherPatientId { get; set; }
            public Study Study { get; set; }
        }
        public class OtherPatientId
        {
            public bool IsPrimary { get; set; }
            public object IssuerOfPatientId { get; set; }
            public string PatientId { get; set; }
            public long PatientIdValidationStatus { get; set; }
            public string TypeOfPatientId { get; set; }
        }
        public class Study
        {
            public double Height { get; set; }
            public string PregnancyOrigin { get; set; }
            public DateTime StudyDateTime { get; set; }
            public string StudyDescription { get; set; }
            public string StudyInstanceUID { get; set; }
            public double Weight { get; set; }
            public Series Series { get; set; }
            public double? BSA { get; set; }
        }

        public class Series
        {
            public string BloodPressure { get; set; }
            public string Category { get; set; }
            public string InstitutionName { get; set; }
            public bool IsStressExam { get; set; }
            public string LastModifiedByAdsVersion { get; set; }
            public string LastModifiedByVersion { get; set; }
            public string Modality { get; set; }
            public string PpsDescription { get; set; }
            public DateTimeOffset SeriesDateTime { get; set; }
            public string SeriesInstanceUID { get; set; }
            public bool SignedOff { get; set; }
            [XmlElement("Parameter")]
            public List<Parameter> Parameter { get; set; }
        }
        public class Parameter
        {
            public string StudyId { get; set; }
            public string ScanMode { get; set; }
            public double? ResultValue { get; set; }
            public long ResultNo { get; set; }
            public object Qualifier { get; set; }
            public string ParameterName { get; set; }
            public string ParameterType { get; set; }
            public string ParameterId { get; set; }
            public bool ExcludedFromCalc { get; set; }
            public bool ExcludedFromAvg { get; set; }
            public bool Edited { get; set; }
            public string DisplayUnit { get; set; }
            public string Category { get; set; }
            public string AverageType { get; set; }
            public string MeasureId { get; set; }
            public string DisplayValue { get; set; }
        }
    }

    public class PartientViewModel
    {
        public DicomSRViewModel DicomSRViewModel { get; set; } = default!;
        public DicomSRMeaViewModel DicomSRMeaViewModel { get; set; } = default!;
        public string PartientId { get; set; }
        public DateTime StudyTime { get; set; }
        public string StudyTimeMea { get; set; }
    }
}
