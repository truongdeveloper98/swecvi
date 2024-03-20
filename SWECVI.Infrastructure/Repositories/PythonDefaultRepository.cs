using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.Infrastructure.Data;


namespace SWECVI.Infrastructure.Repositories
{
    public class PythonDefaultRepository : RepositoryBase<PythonDefault>, IPythonDefaultRepository
    {
        public PythonDefaultRepository(ManagerHospitalDbContext context) : base(context)
        {

        }
    }
}
