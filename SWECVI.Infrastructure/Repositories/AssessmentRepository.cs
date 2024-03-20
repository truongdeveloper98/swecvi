using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class AssessmentRepository : RepositoryBase<AssessmentTextReference>, IAssessmentRepository
    {
        public AssessmentRepository(ManagerHospitalDbContext context) : base(context)
        {
        }
    }
}
