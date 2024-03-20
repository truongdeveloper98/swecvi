using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class FindingStructureRepository : RepositoryBase<FindingStructure>, IFindingStructureRepository
    {
        public FindingStructureRepository(ManagerHospitalDbContext context) : base(context)
        {
        }
    }
}
