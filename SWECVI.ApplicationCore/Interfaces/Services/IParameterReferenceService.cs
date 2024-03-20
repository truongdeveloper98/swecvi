using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IParameterReferenceService
    {
        Task<ParameterReferenceViewModel> GetById(int id);
        Task<PagedResponseDto<ParameterReferenceViewModel>> Gets(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch);
        Task<bool> Create(ParameterReferenceViewModel model);
        Task<bool> Update(int id, ParameterReferenceViewModel model);
    }
}
