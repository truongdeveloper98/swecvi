using SWECVI.ApplicationCore.Entities;

namespace SWECVI.ApplicationCore.Interfaces
{
    public interface ICacheService
    {
        IEnumerable<ParameterSetting> GetParameterSettings();
        IEnumerable<DicomTags> GetDicomTags();
        IEnumerable<AssessmentTextReference> GetAssessmentText();
        IEnumerable<ManufacturerDicomParameters> GetManufacturerDicomParameters();
        IEnumerable<ParameterReference> GetReferences();
        IEnumerable<ValveData> GetValves();
        void RemoveKey(string key);
    }
}