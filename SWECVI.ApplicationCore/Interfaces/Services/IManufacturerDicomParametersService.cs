namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IManufacturerDicomParametersService
    {
        Task<int> TagExistsAsync(string CM, string CV);
    }
}
