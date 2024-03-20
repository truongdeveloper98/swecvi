using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class ManufacturerDicomParametersRepository : RepositoryBase<ManufacturerDicomParameters>, IManufacturerDicomParametersRepository
    {
        public ManufacturerDicomParametersRepository(ManagerHospitalDbContext context) : base(context)
        {
        }
    }
}
