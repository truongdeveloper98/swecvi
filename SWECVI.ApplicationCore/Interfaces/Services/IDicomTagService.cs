namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IDicomTagService
    {
        Task<bool> TagExistsAsync(string CM, string CV);
    }
}
