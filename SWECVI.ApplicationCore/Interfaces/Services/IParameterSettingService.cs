using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IParameterSettingService
    {
        Task<ParameterSettingViewModel> GetById(int id);
        Task<PagedResponseDto<ParameterSettingViewModel>> Gets(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch);
        Task<bool> Create(ParameterSettingViewModel model);
        Task<bool> Update(int id, ParameterSettingViewModel model);
    }
}
