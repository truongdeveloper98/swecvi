using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.ViewModels.Hospital;
using SWECVI.ApplicationCore.ViewModels.MirthConnect;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IHospitalService
    {
        Task<HopsitalViewModel> GetById(int id);
        Task<PagedResponseDto<HopsitalViewModel>> GetHospitals(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch);
        Task<bool> CreateHospital(HopsitalViewModel model);
        Task<bool> UpdateHospital(int id,HopsitalViewModel model);
        Task<SuperAdminHospitalViewModel> GetHospitalById(int id);
        Task<List<SuperAdminHospitalViewModel>> GetHospitals();
    }
}
