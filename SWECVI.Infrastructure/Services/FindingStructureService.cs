using Microsoft.Extensions.Logging;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;

namespace SWECVI.Infrastructure.Services
{
    public class FindingStructureService : IFindingStructureService
    {
        private readonly IFindingStructureRepository _findingStructureRepository;
        private readonly ILogger<FindingStructureService> _logger;


        public FindingStructureService(
                IFindingStructureRepository findingStructureRepository,
                ILogger<FindingStructureService> logger
            )
        {
            _logger = logger;
            _findingStructureRepository = findingStructureRepository;
        }

        public async Task<List<FindingStructure>> GetAll()
        {
            return (List<FindingStructure>)await _findingStructureRepository.QueryAsync();
        }
    }
}
