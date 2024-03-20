using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.ViewModels.Hospital;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<DepartmentViewModel> GetById(int id);
        Task<PagedResponseDto<DepartmentViewModel>> GetDepartments(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch);
        Task<bool> CreateDepartment(DepartmentViewModel model);
        Task<bool> UpdateDepartment(int id, DepartmentViewModel model);
    }
}
