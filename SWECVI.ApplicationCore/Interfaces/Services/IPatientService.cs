using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.ViewModels.MirthConnect;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IPatientService
    {
        Task<bool> GetById(string id);
        Task<bool> Delete(int id);
        Task<bool> Create(DicomResultMirthViewModel model, int hospitalId, List<ManufacturerDicomParameters> manufacturerDicomParameters, List<DicomTags> dicomTags);
        Task<List<PatientStudyViewModel>> GetExamsByPatientId(int patientId, string? period, int hospitalId);
        Task<MemoryStream> ExportData(DateTime? startDate = null, DateTime? endDate = null, int? partientId = null, int? studyId = null);
        Task<PagedResponseDto<PatientViewModel>> GetPatients(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch, string? dicomPatientId, string? period, int hospitalId, string role);
    }
}
