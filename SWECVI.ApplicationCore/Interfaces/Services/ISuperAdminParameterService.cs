using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface ISuperAdminParameterService
    {
        Task InsertDataToDB(List<DicomtagParameterViewModel> models);
    }
}
