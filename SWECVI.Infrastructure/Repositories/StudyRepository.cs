using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class StudyRepository : RepositoryBase<Study>, IStudyRepository
    {
        public StudyRepository(ManagerHospitalDbContext context) : base(context)
        {
        }
    }
}
