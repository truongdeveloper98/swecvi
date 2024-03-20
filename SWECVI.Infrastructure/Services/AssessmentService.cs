using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Utilities;
using SWECVI.ApplicationCore.ViewModels;
using System.Linq.Expressions;

namespace SWECVI.Infrastructure.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _assessmentRepository;


        public AssessmentService(IAssessmentRepository assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }

        public async Task<AssessmentViewModel> GetById(int id)
        {
            var assessment = await _assessmentRepository.Get(id);

            if (assessment is null)
            {
                throw new Exception($"Assessment not found with Id : {id}");
            }

            var result = new AssessmentViewModel()
            {
                DescriptionReportText = assessment.DescriptionReportText,
                ACode = assessment.ACode,
                DCode = assessment.DCode,
                CallFunction = assessment.CallFunction,
                ReportTextSE = assessment.ReportTextSE
            };

            return result;
        }

        public async Task<PagedResponseDto<AssessmentViewModel>> Gets(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch)
        {
            Expression<Func<AssessmentTextReference, bool>> filter = i => !i.IsDeleted;

            if (!string.IsNullOrEmpty(textSearch))
            {
                Expression<Func<AssessmentTextReference, bool>> searchFilter = i => i.DescriptionReportText.Contains(textSearch);

                filter = PredicateBuilder.AndAlso(filter, searchFilter);
            }

            Expression<Func<AssessmentTextReference, AssessmentViewModel>> selectorExpression = i => new AssessmentViewModel
            {
                Id = i.Id,
                DescriptionReportText = i.DescriptionReportText,
                ACode = i.ACode,
                DCode = i.DCode,
                CallFunction = i.CallFunction,
                ReportTextSE = i.ReportTextSE
            };


            var totalItems = await _assessmentRepository.Count(filter);

            var items = await _assessmentRepository
                .QueryAndSelectAsync(
                    selector: selectorExpression,
                    filter,
                    orderBy: m => PredicateBuilder.ApplyOrder(m, sortColumnName, sortColumnDirection),
                    "",
                    pageSize,
                    page: currentPage
                );

            return new PagedResponseDto<AssessmentViewModel>()
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = (List<AssessmentViewModel>)items
            };
        }

        public async Task<bool> Create(AssessmentViewModel model)
        {
            var department = new AssessmentTextReference()
            {
                DescriptionReportText = model.DescriptionReportText,
                ACode = model.ACode,
                DCode = model.DCode,
                CallFunction = model.CallFunction,
                ReportTextSE = model.ReportTextSE,
                IsDeleted = false,
            };

            await _assessmentRepository.Add(department);

            return true;
        }

        public async Task<bool> Update(int id, AssessmentViewModel model)
        {
            var assessment = await _assessmentRepository.Get(id);

            if (assessment is null)
            {
                throw new Exception($"Assessment not found with Id : {id}");
            }

            assessment.DescriptionReportText = model.DescriptionReportText;
            assessment.ACode = model.ACode;
            assessment.DCode = model.DCode;
            assessment.CallFunction = model.CallFunction;
            assessment.ReportTextSE = model.ReportTextSE;
            assessment.UpdatedAt = DateTime.Now;

            await _assessmentRepository.Update(assessment);

            return true;
        }
    }
}
