using SWECVI.ApplicationCore.Entities;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IFindingStructureService
    {
        Task<List<FindingStructure>> GetAll();
    }
}
