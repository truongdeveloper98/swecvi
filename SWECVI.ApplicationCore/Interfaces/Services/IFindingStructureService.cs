using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IFindingStructureService
    {
        Task<List<FindingStructure>> GetAll();
        Task<FindingStructureViewModel> GetById(int id);
        Task<PagedResponseDto<FindingStructureViewModel>> Gets(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch);
        Task<bool> Create(FindingStructureViewModel model);
        Task<bool> Update(int id, FindingStructureViewModel model);
    }
}
