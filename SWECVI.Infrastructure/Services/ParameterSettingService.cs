﻿using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Utilities;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.Infrastructure.Repositories;
using System.Linq.Expressions;
using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.Infrastructure.Services
{
    public class ParameterSettingService : IParameterSettingService
    {
        private readonly IParameterSettingRepository _parameterSettingRepository;
        private readonly IDepartmentRepository _departmentRepository;


        public ParameterSettingService(IParameterSettingRepository parameterSettingRepository,
            IDepartmentRepository departmentRepository)
        {
            _parameterSettingRepository = parameterSettingRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<ParameterSettingViewModel> GetById(int id)
        {
            var setting = await _parameterSettingRepository.Get(id);

            if (setting is null)
            {
                throw new Exception($"Setting not found with Id : {id}");
            }

            var result = new ParameterSettingViewModel()
            {
                ParameterId = setting.ParameterId,
                ShowInChart = setting.ShowInChart,
                ShowInParameterTable = setting.ShowInParameterTable,
                ShowInAssessmentText = setting.ShowInAssessmentText,
                TableFriendlyName = setting.TableFriendlyName,
                TextFriendlyName = setting.TextFriendlyName,
                ParameterHeader = setting.ParameterHeader,
                ParameterSubHeader = setting.ParameterSubHeader,
                DisplayDecimal = setting.DisplayDecimal,
                ParameterOrder = setting.ParameterOrder,
                ParameterHeaderOrder = setting.ParameterHeaderOrder,
                POH = setting.POH,
                Description = setting.Description,
                FunctionSelector = setting.FunctionSelector,
                DepartmentName = setting.DepartmentId != null ? (_departmentRepository.FirstOrDefault(x => x.Id == setting.DepartmentId.Value, null, "")).Name : string.Empty,
            };

            return result;
        }

        public async Task<PagedResponseDto<ParameterSettingViewModel>> Gets(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch)
        {
            Expression<Func<ParameterSetting, bool>> filter = i => !i.IsDeleted;

            if (!string.IsNullOrEmpty(textSearch))
            {
                Expression<Func<ParameterSetting, bool>> searchFilter = i => i.ParameterId.Contains(textSearch)
                                         || i.TableFriendlyName.Contains(textSearch)
                                         || i.ParameterHeader.Contains(textSearch);

                filter = PredicateBuilder.AndAlso(filter, searchFilter);
            }

            Expression<Func<ParameterSetting, ParameterSettingViewModel>> selectorExpression = i => new ParameterSettingViewModel
            {
                Id = i.Id,
                ParameterId = i.ParameterId,
                ShowInChart = i.ShowInChart,
                ShowInParameterTable = i.ShowInParameterTable,
                ShowInAssessmentText = i.ShowInAssessmentText,
                TableFriendlyName = i.TableFriendlyName,
                TextFriendlyName = i.TextFriendlyName,
                ParameterHeader = i.ParameterHeader,
                ParameterSubHeader = i.ParameterSubHeader,
                DisplayDecimal = i.DisplayDecimal,
                ParameterOrder = i.ParameterOrder,
                ParameterHeaderOrder = i.ParameterHeaderOrder,
                POH = i.POH,
                Description = i.Description,
                FunctionSelectorName = i.FunctionSelector == FunctionSelector.Min ? "Min" : i.FunctionSelector == FunctionSelector.Max ? "Max" : i.FunctionSelector == FunctionSelector.Avg ? "Avg" : i.FunctionSelector == FunctionSelector.Latest ? "Lastest" : string.Empty,
                DepartmentId = i.DepartmentId,
            };


            var totalItems = await _parameterSettingRepository.Count(filter);

            var items = await _parameterSettingRepository
                .QueryAndSelectAsync(
                    selector: selectorExpression,
                    filter,
                    orderBy: m => PredicateBuilder.ApplyOrder(m, sortColumnName, sortColumnDirection),
                    "",
                    pageSize,
            page: currentPage
            );

            items.ToList().ForEach(i => i.DepartmentName = i.DepartmentId != null ? (_departmentRepository.FirstOrDefault(x => x.Id == i.DepartmentId.Value, null, "") == null ? string.Empty : _departmentRepository.FirstOrDefault(x => x.Id == i.DepartmentId.Value, null, "").Name) : string.Empty);

            return new PagedResponseDto<ParameterSettingViewModel>()
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = (List<ParameterSettingViewModel>)items
            };
        }

        public async Task<bool> Create(ParameterSettingViewModel i)
        {
            var department = new ParameterSetting()
            {
                ParameterId = i.ParameterId,
                ShowInChart = i.ShowInChart,
                ShowInParameterTable = i.ShowInParameterTable,
                ShowInAssessmentText = i.ShowInAssessmentText,
                TableFriendlyName = i.TableFriendlyName,
                TextFriendlyName = i.TextFriendlyName,
                ParameterHeader = i.ParameterHeader,
                ParameterSubHeader = i.ParameterSubHeader,
                DisplayDecimal = i.DisplayDecimal,
                ParameterOrder = i.ParameterOrder,
                ParameterHeaderOrder = i.ParameterHeaderOrder,
                POH = i.POH,
                Description = i.Description,
                FunctionSelector = i.FunctionSelector,
                DepartmentId = i.DepartmentId,
                IsDeleted = false,
            };

            await _parameterSettingRepository.Add(department);

            return true;
        }

        public async Task<bool> Update(int id, ParameterSettingViewModel model)
        {
            var setting = await _parameterSettingRepository.Get(id);

            if (setting is null)
            {
                throw new Exception($"Setting not found with Id : {id}");
            }

            setting.ParameterId = model.ParameterId;
            setting.ShowInChart = model.ShowInChart;
            setting.ShowInParameterTable = model.ShowInParameterTable;
            setting.ShowInAssessmentText = model.ShowInAssessmentText;
            setting.TableFriendlyName = model.TableFriendlyName;
            setting.TextFriendlyName = model.TextFriendlyName;
            setting.ParameterHeader = model.ParameterHeader;
            setting.ParameterSubHeader = model.ParameterSubHeader;
            setting.DisplayDecimal = model.DisplayDecimal;
            setting.ParameterOrder = model.ParameterOrder;
            setting.ParameterHeaderOrder = model.ParameterHeaderOrder;
            setting.POH = model.POH;
            setting.Description = model.Description;
            setting.FunctionSelector = model.FunctionSelector;
            setting.DepartmentId = model.DepartmentId;
            setting.UpdatedAt = DateTime.Now;

            await _parameterSettingRepository.Update(setting);

            return true;
        }
    }
}
