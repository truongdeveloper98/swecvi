using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IUserService
    {
        Task<PagedResponseDto<UserInformationDto>> GetUsers(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch);
        Task<UserInformationDto> GetUserById(int id);
        Task CreateUser(UserInformationDto model);
        Task UpdateUser(int id, UserInformationDto model);
        Task ActiveUser(int id);
        Task InactiveUser(int id);
    }
}
