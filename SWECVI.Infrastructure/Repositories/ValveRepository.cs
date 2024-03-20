using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.Infrastructure.Data;


namespace SWECVI.Infrastructure.Repositories
{
    public class ValveRepository : RepositoryBase<ValveData>, IValveRepository
    {
        public ValveRepository(ManagerHospitalDbContext context) : base(context)
        {

        }
    }
}
