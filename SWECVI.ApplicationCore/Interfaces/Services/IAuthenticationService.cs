using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<LoginDto.LoginResult> Login(LoginDto.Login model);
    }
}
