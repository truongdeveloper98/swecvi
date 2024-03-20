using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class ParameterRepository : RepositoryBase<StudyParameter>, IParameterRepository
    {
        public ParameterRepository(ManagerHospitalDbContext context) : base(context)
        {
        }
    }
}
