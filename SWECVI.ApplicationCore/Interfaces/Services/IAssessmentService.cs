using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IAssessmentService
    {
        Task<AssessmentViewModel> GetById(int id);
        Task<PagedResponseDto<AssessmentViewModel>> Gets(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch);
        Task<bool> Create(AssessmentViewModel model);
        Task<bool> Update(int id, AssessmentViewModel model);
    }
}
