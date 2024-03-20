using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class StudyFindingRepository : RepositoryBase<StudyFinding>, IStudyFindingRepository
    {
        public StudyFindingRepository(ManagerHospitalDbContext context) : base(context)
        {
        }
    }
}
