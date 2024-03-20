using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.Infrastructure.Data;

namespace SWECVI.Infrastructure.Repositories
{
    public class DicomTagRepository : RepositoryBase<DicomTags>, IDicomTagRepository
    {
        public DicomTagRepository(ManagerHospitalDbContext context) : base(context)
        {
        }
    }
}
