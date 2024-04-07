using Microsoft.Extensions.Logging;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.Infrastructure.Repositories;
using System.Reflection.PortableExecutable;
using SWECVI.ApplicationCore.Utilities;
using System.Linq.Expressions;

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

        public async Task<bool> Create(FindingStructureViewModel model)
        {
            var findingStructure = new FindingStructure()
            {
                BoxHeader = model.BoxHeader,
                TabName = model.TabName,
                InputLabel = model.InputLabel,
                InputOptions = model.InputOptions,
                InputType = model.InputType,
                OrderInReport = model.OrderInReport,
                RowOrder = model.RowOrder,
                IsDeleted = false,
            };

            await _findingStructureRepository.Add(findingStructure);

            return true;
        }

        public async Task<List<FindingStructure>> GetAll()
        {
            return (List<FindingStructure>)await _findingStructureRepository.QueryAsync();
        }

        public async Task<FindingStructureViewModel> GetById(int id)
        {
            var findingStructure = await _findingStructureRepository.Get(id);

            if (findingStructure is null)
            {
                throw new Exception($"Finding Structure not found with Id : {id}");
            }

            var result = new FindingStructureViewModel()
            {
                BoxHeader = findingStructure.BoxHeader,
                TabName = findingStructure.TabName,
                InputLabel = findingStructure.InputLabel,
                InputOptions = findingStructure.InputOptions,
                InputType = findingStructure.InputType,
                OrderInReport = findingStructure.OrderInReport,
                RowOrder = findingStructure.RowOrder,
            };

            return result;
        }

        public async Task<PagedResponseDto<FindingStructureViewModel>> Gets(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch)
        {
            Expression<Func<FindingStructure, bool>> filter = i => !i.IsDeleted;

            if (!string.IsNullOrEmpty(textSearch))
            {
                Expression<Func<FindingStructure, bool>> searchFilter = i => i.TabName.Contains(textSearch) || i.InputLabel.Contains(textSearch) || i.BoxHeader.Contains(textSearch);

                filter = PredicateBuilder.AndAlso(filter, searchFilter);
            }

            Expression<Func<FindingStructure, FindingStructureViewModel>> selectorExpression = i => new FindingStructureViewModel
            {
                Id = i.Id,
                BoxHeader = i.BoxHeader,
                TabName = i.TabName,
                InputLabel = i.InputLabel,
                InputOptions = i.InputOptions,
                InputType = i.InputType,
                OrderInReport = i.OrderInReport,
                RowOrder = i.RowOrder,
            };


            var totalItems = await _findingStructureRepository.Count(filter);

            var items = await _findingStructureRepository
                .QueryAndSelectAsync(
                    selector: selectorExpression,
                    filter,
                    orderBy: m => PredicateBuilder.ApplyOrder(m, sortColumnName, sortColumnDirection),
                    "",
                    pageSize,
                    page: currentPage
                );

            return new PagedResponseDto<FindingStructureViewModel>()
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = (List<FindingStructureViewModel>)items
            };
        }

        public async Task<bool> Update(int id, FindingStructureViewModel model)
        {
            var findingStructure = await _findingStructureRepository.Get(id);

            if (findingStructure is null)
            {
                throw new Exception($"Finding Structure not found with Id : {id}");
            }

            findingStructure.BoxHeader = model.BoxHeader;
            findingStructure.TabName = model.TabName;
            findingStructure.InputLabel = model.InputLabel;
            findingStructure.InputOptions = model.InputOptions;
            findingStructure.InputType = model.InputType;
            findingStructure.OrderInReport = model.OrderInReport;
            findingStructure.RowOrder = model.RowOrder;
            findingStructure.IsDeleted = false;
            findingStructure.UpdatedAt = DateTime.Now;

            await _findingStructureRepository.Update(findingStructure);

            return true;
        }
    }
}
