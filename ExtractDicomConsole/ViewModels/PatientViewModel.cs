using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractDicomConsole.ViewModels
{
    public class PatientViewModel
    {
            public string PatientId {get;set;}
            public int Number_Of_Parameter {get;set;}
            public string SeriesInstanceUID {get;set;}
            public DateTimeOffset SeriesDateTime {get;set;}
            public DateTimeOffset StudyDateTime {get;set;}
            public string StudyInstanceUID {get;set;}
            public double Weight {get;set;}
            public double Height {get;set;}
            public double BSA {get;set;}
            public string InstitutionName {get;set;}
            public string MeasureId {get;set;}
            public string ParameterId {get;set;}
            public string ParameterName {get;set;}
            public string Measurement {get;set;}
            public string AverageType {get;set;}
            public double ResultValue {get;set;}
            public string DisplayUnit {get;set;}
            public string ScanMode {get;set;}
            public string StudyId {get;set;}
            public string ParameterType {get;set;}
            public string DisplayValue {get;set;}
            public string FirstName {get;set;}
            public string LastName {get;set;}
            public string OtherPatientId {get;set;}
    }
}
