using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class ParameterSettingRepository : RepositoryBase<ParameterSetting>, IParameterSettingRepository
    {
        public ParameterSettingRepository(ManagerHospitalDbContext context) : base(context)
        {

        }
    }
}
