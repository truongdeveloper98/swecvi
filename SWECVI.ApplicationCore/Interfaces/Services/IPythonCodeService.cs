using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IPythonCodeService
    {
        Task<List<PythonCodeVersionDto.CodeVersion>> GetPythonCodes();
        Task CreatePythonCode(int id, string script);
        Task DeletePythonCode(int id);
        Task SetCurrentVersion(int id);
        Task ResetDefault(int id, bool force);
    }
}