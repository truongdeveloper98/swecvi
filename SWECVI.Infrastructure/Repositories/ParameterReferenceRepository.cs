using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class ParameterReferenceRepository : RepositoryBase<ParameterReference>, IParameterReferenceRepository
    {
        public ParameterReferenceRepository(ManagerHospitalDbContext context) : base(context)
        {
        }
    }
}
