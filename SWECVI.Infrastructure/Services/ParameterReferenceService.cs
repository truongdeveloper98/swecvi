using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Utilities;
using SWECVI.ApplicationCore.ViewModels;
using System.Linq.Expressions;

namespace SWECVI.Infrastructure.Services
{
    public class ParameterReferenceService : IParameterReferenceService
    {
        private readonly IParameterReferenceRepository _parameterReferenceRepository;


        public ParameterReferenceService(IParameterReferenceRepository parameterReferenceRepository)
        {
            _parameterReferenceRepository = parameterReferenceRepository;
        }

        public async Task<ParameterReferenceViewModel> GetById(int id)
        {
            var reference = await _parameterReferenceRepository.Get(id);

            if (reference is null)
            {
                throw new Exception($"Reference not found with Id : {id}");
            }

            var result = new ParameterReferenceViewModel()
            {
                ParameterId = reference.ParameterId,
                ParameterNameLogic = reference.ParameterNameLogic,
                AgeFrom = reference?.AgeFrom,
                AgeTo = reference?.AgeTo,
                Gender = reference?.Gender,
                DisplayUnit = reference?.DisplayUnit,
                DepaermentId = reference?.DepaermentId,
                MildlyAbnormalRangeLower = reference?.MildlyAbnormalRangeLower,
                MildlyAbnormalRangeUpper = reference?.MildlyAbnormalRangeUpper,
                NormalRangeLower = reference?.NormalRangeLower,
                NormalRangeUpper = reference?.NormalRangeUpper,
                ModeratelyAbnormalRangeLower = reference?.ModeratelyAbnormalRangeLower,
                ModeratelyAbnormalRangeUpper = reference?.ModeratelyAbnormalRangeUpper,
                SeverelyAbnormalRangeLessThan = reference?.SeverelyAbnormalRangeLessThan,
                SeverelyAbnormalRangeMoreThan = reference?.SeverelyAbnormalRangeMoreThan
            };

            return result;
        }

        public async Task<PagedResponseDto<ParameterReferenceViewModel>> Gets(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch)
        {
            Expression<Func<ParameterReference, bool>> filter = i => !i.IsDeleted;

            if (!string.IsNullOrEmpty(textSearch))
            {
                Expression<Func<ParameterReference, bool>> searchFilter = i => i.ParameterNameLogic.Contains(textSearch) || i.ParameterId.Contains(textSearch);

                filter = PredicateBuilder.AndAlso(filter, searchFilter);
            }

            Expression<Func<ParameterReference, ParameterReferenceViewModel>> selectorExpression = i => new ParameterReferenceViewModel
            {
                Id = i.Id,
                ParameterId = i.ParameterId,
                ParameterNameLogic = i.ParameterNameLogic,
                AgeFrom = i.AgeFrom,
                AgeTo = i.AgeTo,
                GenderName = i.Gender == ApplicationCore.Enum.Gender.Male ? "Male" : i.Gender == ApplicationCore.Enum.Gender.Female ? "Female" : "Unknown",
                DisplayUnit = i.DisplayUnit,
                DepaermentId = i.DepaermentId,
                MildlyAbnormalRangeLower = i.MildlyAbnormalRangeLower,
                MildlyAbnormalRangeUpper = i.MildlyAbnormalRangeUpper,
                NormalRangeLower = i.NormalRangeLower,
                NormalRangeUpper = i.NormalRangeUpper,
                ModeratelyAbnormalRangeLower = i.ModeratelyAbnormalRangeLower,
                ModeratelyAbnormalRangeUpper = i.ModeratelyAbnormalRangeUpper,
                SeverelyAbnormalRangeLessThan = i.SeverelyAbnormalRangeLessThan,
                SeverelyAbnormalRangeMoreThan = i.SeverelyAbnormalRangeMoreThan
            };


            var totalItems = await _parameterReferenceRepository.Count(filter);

            var items = await _parameterReferenceRepository
                .QueryAndSelectAsync(
                    selector: selectorExpression,
                    filter,
                    orderBy: m => PredicateBuilder.ApplyOrder(m, sortColumnName, sortColumnDirection),
                    "",
                    pageSize,
                    page: currentPage
                );

            return new PagedResponseDto<ParameterReferenceViewModel>()
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = (List<ParameterReferenceViewModel>)items
            };
        }

        public async Task<bool> Create(ParameterReferenceViewModel model)
        {
            var reference = new ParameterReference()
            {
                ParameterId = model.ParameterId,
                ParameterNameLogic = model.ParameterNameLogic,
                AgeFrom = model.AgeFrom,
                AgeTo = model.AgeTo,
                Gender = model.Gender,
                DisplayUnit = model.DisplayUnit,
                DepaermentId = model.DepaermentId,
                MildlyAbnormalRangeLower = model.MildlyAbnormalRangeLower,
                MildlyAbnormalRangeUpper = model.MildlyAbnormalRangeUpper,
                NormalRangeLower = model.NormalRangeLower,
                NormalRangeUpper = model.NormalRangeUpper,
                ModeratelyAbnormalRangeLower = model.ModeratelyAbnormalRangeLower,
                ModeratelyAbnormalRangeUpper = model.ModeratelyAbnormalRangeUpper,
                SeverelyAbnormalRangeLessThan = model.SeverelyAbnormalRangeLessThan,
                SeverelyAbnormalRangeMoreThan = model.SeverelyAbnormalRangeMoreThan,
                IsDeleted = false,
            };

            await _parameterReferenceRepository.Add(reference);

            return true;
        }

        public async Task<bool> Update(int id, ParameterReferenceViewModel model)
        {
            var reference = await _parameterReferenceRepository.Get(id);

            if (reference is null)
            {
                throw new Exception($"Reference not found with Id : {id}");
            }

            reference.ParameterId = model.ParameterId;
            reference.ParameterNameLogic = model.ParameterNameLogic;
            reference.AgeFrom = model.AgeFrom;
            reference.AgeTo = model.AgeTo;
            reference.Gender = model.Gender;
            reference.DisplayUnit = model.DisplayUnit;
            reference.DepaermentId = model.DepaermentId;
            reference.MildlyAbnormalRangeLower = model.MildlyAbnormalRangeLower;
            reference.MildlyAbnormalRangeUpper = model.MildlyAbnormalRangeUpper;
            reference.NormalRangeLower = model.NormalRangeLower;
            reference.NormalRangeUpper = model.NormalRangeUpper;
            reference.ModeratelyAbnormalRangeLower = model.ModeratelyAbnormalRangeLower;
            reference.ModeratelyAbnormalRangeUpper = model.ModeratelyAbnormalRangeUpper;
            reference.SeverelyAbnormalRangeLessThan = model.SeverelyAbnormalRangeLessThan;
            reference.SeverelyAbnormalRangeMoreThan = model.SeverelyAbnormalRangeMoreThan;
            reference.UpdatedAt = DateTime.Now;

            await _parameterReferenceRepository.Update(reference);

            return true;
        }
    }
}
