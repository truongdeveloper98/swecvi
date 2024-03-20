using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class ReferenceRepository : RepositoryBase<ParameterReference>, IReferenceRepository
    {
        public ReferenceRepository(ManagerHospitalDbContext context) : base(context)
        {

        }
    }
}
