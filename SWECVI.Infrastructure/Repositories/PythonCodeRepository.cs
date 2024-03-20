using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.Infrastructure.Data;


namespace SWECVI.Infrastructure.Repositories
{
    public class PythonCodeRepository : RepositoryBase<PythonCode>, IPythonCodeRepository
    {
        public PythonCodeRepository(ManagerHospitalDbContext context) : base(context)
        {

        }
    }
}
