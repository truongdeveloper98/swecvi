using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SWECVI.ApplicationCore.ViewModels
{
    [XmlRoot("ROOT")]
    [XmlType("ROOT")]
    public class DicomSRMeaViewModel
    {
        public PatientMeaModel Patient { get; set; }
    }

    public class PatientMeaModel
    {
        public string patientId { get; set; }
        public string firstName { get; set; }
        public object IssuerOfPatientId { get; set; }
        public string lastName { get; set; }
        public DateTime BirthTime { get; set; }
        public string OtherId { get; set; }
        public string Comments { get; set; }
        public string sex { get; set; }
        public Exam Exam { get; set; }
    }
    public class Exam
    {
        public double height { get; set; }
        public string Category { get; set; }
        public string StudyDate { get; set; }
        public string StudyTime { get; set; }
        public string SeriesDate { get; set; }
        public string SeriesTime { get; set; }
        public DateTime StudyDateTime { get; set; }
        public DateTimeOffset SeriesDateTime { get; set; }

        public string SRSeriesInstanceUID { get; set; }
        public string SeriesInstanceUID { get; set; }
        public string StudyInstanceUID { get; set; }
        public double weight { get; set; }
        [XmlElement("MEASUREMENT")]
        public List<MEASUREMENT> MEASUREMENT { get; set; }
    }

    public class MEASUREMENT
    {
        public string parameterId { get; set; }
        public int resultNo { get; set; }
        public double excludeAvg { get; set; }
        public double excludeCalc { get; set; }
        public double valueDouble { get; set; }
        public string displayUnit { get; set; }
        public string displayMode { get; set; }
        public string parameterName { get; set; }
        public string parameterType { get; set; }
        public string valueString { get; set; }
        public string mode { get; set; }
        public string study { get; set; }
        public string measurement { get; set; }
        public string avgType { get; set; }
        public string maCategory { get; set; }
        public int deleted { get; set; }
    }
}
