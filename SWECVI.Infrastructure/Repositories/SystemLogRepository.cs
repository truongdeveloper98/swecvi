using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.Infrastructure.Data;


namespace SWECVI.Infrastructure.Repositories
{
    public class SystemLogRepository : RepositoryBase<SystemLog>, ISystemLogRepository
    {
        public SystemLogRepository(ManagerHospitalDbContext context) : base(context)
        {

        }
    }
}
