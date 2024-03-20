using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.Interfaces;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using SWECVI.ApplicationCore.Constants;
using static SWECVI.ApplicationCore.ViewModels.ParameterDto;
using Enum = SWECVI.ApplicationCore.Enum;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Interfaces.Services;

namespace SWECVI.Infrastructure.Services
{
    public class ParameterService : IParameterService
    {

        private readonly ICacheService _cacheService;
        private readonly ILogger<ParameterService> _logger;
        private readonly IParameterReferenceRepository _parameterReferenceRepository;
        private readonly IParameterSettingRepository _parameterSettingRepository;

        public ParameterService(ILogger<ParameterService> logger, ICacheService cacheService, IParameterSettingRepository parameterSettingRepository, IParameterReferenceRepository parameterReferenceRepository)
        {
            _cacheService = cacheService;
            _parameterReferenceRepository = parameterReferenceRepository;
            _logger = logger;
            _parameterSettingRepository = parameterSettingRepository;
        }

        public async Task<List<ParameterDto.ParameterValuesChartByPatientId>> GetValuesByParametersForPatient(int[] ids, int patientId)
        {
            //List<ParameterDto.ParameterValuesChartByPatientId> parameterValuesCharts = new List<ParameterValuesChartByPatientId>();
            //if (ids.Length == 0)
            //{
            //    return parameterValuesCharts;
            //}
            //var parameters = await _parameterRepository.QueryAsync(i => ids.Contains(i.Id) && !String.IsNullOrEmpty(i.TableFriendlyName));

            //Expression<Func<ExamParameterOld, bool>> filter = i => i.Exam.Patient.Id == patientId;

            //var examPatients = await _examPatientRepository.QueryAsync(filter: filter, includeProperties: "Exam");

            //foreach (var parameter in parameters)
            //{
            //    var parameterValuesChart = new ParameterDto.ParameterValuesChartByPatientId
            //    {
            //        ParameterName = parameter.TableFriendlyName,
            //        ValueByTimes = examPatients.Where(m => m.TableFriendlyName == parameter.TableFriendlyName && m.Exam.FormattedDate != null)
            //                        .Select(m => new ValueByTime
            //                        {
            //                            Value = m.Value,
            //                            Time = (m.Value != null) ? m.Exam.FormattedDate : null
            //                        }).ToList()
            //    };
            //    parameterValuesCharts.Add(parameterValuesChart);
            //}
            return new List<ParameterValuesChartByPatientId>();
        }


        public async Task<List<ParameterDto.ParameterStaticChart>> GetValuesByParameters(int ySelectorId, string xValueSelector, string? period)
        {

            //Expression<Func<ParameterViewModel, bool>> filter = i => i.Id == ySelectorId && !String.IsNullOrEmpty(i.TableFriendlyName);

            //var parameter = _parameterRepository.FirstOrDefault(filter : filter);

            //if (parameter == null)
            //    throw new Exception("Can not found parameter");

            //Expression<Func<ExamPatient, bool>> filterExams = m => m.TableFriendlyName == parameter.TableFriendlyName;

            //DateTime today = DateTime.UtcNow.Date;
            //Dictionary<string, int> countByValueSelected = new Dictionary<string, int>();
            //if (!string.IsNullOrEmpty(period))
            //{
            //    if (period.Equals("today"))
            //    {
            //        Expression<Func<ExamPatient, bool>> searchFilter = i => i.Exam.FormattedDate == today;

            //        filterExams = PredicateBuilder.AndAlso(filterExams, searchFilter);

            //    }
            //    else if (period.Equals("1-day-ago"))
            //    {
            //        today = today.AddDays(-1);

            //        Expression<Func<ExamPatient, bool>> searchFilter = i => i.Exam.FormattedDate == today;

            //        filterExams = PredicateBuilder.AndAlso(filterExams, searchFilter);
            //    }
            //    else if (period.Equals("2-days-ago"))
            //    {
            //        var dayAgo = today.AddDays(-2);

            //        Expression<Func<ExamPatient, bool>> searchFilter = i => i.Exam.FormattedDate >= dayAgo;

            //        filterExams = PredicateBuilder.AndAlso(filterExams, searchFilter);
            //    }
            //    else if (period.Equals("1-week-ago"))
            //    {
            //        DateTime lastWeekSunday = today.AddDays(-(int)today.DayOfWeek);

            //        lastWeekSunday = lastWeekSunday.AddDays(1).AddTicks(-1);

            //        DateTime lastWeekMonday = lastWeekSunday.AddDays(-6);

            //        lastWeekMonday = lastWeekMonday.AddTicks(1);

            //        Expression<Func<ExamPatient, bool>> searchFilter = i => i.Exam.FormattedDate >= lastWeekMonday && i.Exam.FormattedDate <= lastWeekSunday;

            //        filterExams = PredicateBuilder.AndAlso(filterExams, searchFilter);
            //    }
            //    else if (period.Equals("1-month-ago"))
            //    {
            //        Expression<Func<ExamPatient, bool>> searchFilter = i => i.Exam.FormattedDate!.Value.Month == today.Month - 1 && i.Exam.FormattedDate.Value.Year == today.Year;

            //        filterExams = PredicateBuilder.AndAlso(filterExams, searchFilter);
            //    }
            //    else if (period.Equals("this-year"))
            //    {
            //        Expression<Func<ExamPatient, bool>> searchFilter = i => i.Exam.FormattedDate!.Value.Year == today.Year;

            //        filterExams = PredicateBuilder.AndAlso(filterExams, searchFilter);
            //    }
            //}


            //Expression<Func<ExamPatient, ExamPatient>> selectorExpression = examPartient => new ExamPatient
            //{
            //    ParameterName = examPartient.ParameterName,
            //    TableFriendlyName = examPartient.TableFriendlyName,
            //    Unit  = examPartient.Unit,
            //    Value = examPartient.Value,
            //    Reference = examPartient.Reference,
            //    IsOutsideReferenceRange = examPartient.IsOutsideReferenceRange,
            //    DisplayDecimal = examPartient.DisplayDecimal,
            //    SelectedFunction = examPartient.SelectedFunction,
            //    Exam = examPartient.Exam,
            //    Id = examPartient.Id,
            //};

            //var exams = await _examPatientRepository.QueryAndSelectAsync(selector: selectorExpression,
            //        filter : filterExams,
            //        includeProperties: "Exam");


            //List<ParameterDto.ParameterStaticChart> parameterStaticCharts = new List<ParameterStaticChart>();
            //if (xValueSelector == "Age")
            //{
            //    var existedAge = exams.Where(m => m.Exam.Age != null);
            //    if (existedAge != null)
            //    {
            //        parameterStaticCharts = exams.Select(m => new ParameterStaticChart
            //        {
            //            XValue = m.Exam.Age.ToString(),
            //            YValue = m.Value
            //        }).ToList();
            //    }
            //}
            //else if (xValueSelector == "Height")
            //{
            //    var existedHeight = exams.Where(m => m.Exam.Height != null);
            //    if (existedHeight != null)
            //    {
            //        parameterStaticCharts = exams.Select(m => new ParameterStaticChart
            //        {
            //            XValue = m.Exam.Height.ToString(),
            //            YValue = m.Value
            //        }).ToList();
            //    }
            //}
            //else if (xValueSelector == "Weight")
            //{
            //    var existedWeight = exams.Where(m => m.Exam.Weight != null);
            //    if (existedWeight != null)
            //    {
            //        parameterStaticCharts = exams.Select(m => new ParameterStaticChart
            //        {
            //            XValue = m.Exam.Weight.ToString(),
            //            YValue = m.Value
            //        }).ToList();
            //    }
            //}
            //else if (xValueSelector == "BloodPressure")
            //{
            //    var existedBloodPressure = exams.Where(m => m.Exam.BloodPressure != null);
            //    if (existedBloodPressure != null)
            //    {
            //        parameterStaticCharts = exams.Where(m => m.Exam.BloodPressure != null).Select(m => new ParameterStaticChart
            //        {
            //            XValue = m.Exam.BloodPressure.FormatBloodPressure(),
            //            YValue = m.Value
            //        }).ToList();
            //    }
            //}
            //else if (xValueSelector == "BSA")
            //{
            //    var existedBSA = exams.Where(m => m.Exam.BSA != null);
            //    if (existedBSA != null)
            //    {
            //        parameterStaticCharts = exams.Select(m => new ParameterStaticChart
            //        {
            //            XValue = m.Exam.BSA.ToString(),
            //            YValue = m.Value
            //        }).ToList();
            //    }
            //}
            //return parameterStaticCharts;

            return null;
        }
        public async Task<List<ParameterDto.ParameterSelector>> GetParameterNames()
        {
            //var cachedParameters = _cacheService.GetParameterSettings();
            //var groupTableFriendlyName = cachedParameters.Where(i => !String.IsNullOrEmpty(i.TableFriendlyName))
            //.GroupBy(m => m.TableFriendlyName);
            //List<ParameterDto.ParameterSelector> parameters = groupTableFriendlyName.Select(i => new ParameterDto.ParameterSelector()
            //{
            //    Id = i.Last().Id,
            //    ParameterName = i.Last().TableFriendlyName!
            //}).OrderBy(i => i.ParameterName).ToList();
            return new List<ParameterDto.ParameterSelector>();
        }

        public async Task<List<ParameterViewModel>> GetParametersWithReferences()
        {
            var cachedParameterSettings = await _parameterSettingRepository.Get();
            //var parameterList = cachedParameters.OrderBy(i => i.ParameterName).ThenBy(i => i.Priority).ToList();
            var parameterList = cachedParameterSettings.ToList().Select(setting => new ParameterViewModel()
            {
                Description = setting.Description,
                FunctionSelector = setting.FunctionSelector,
                DisplayDecimal = setting.DisplayDecimal,
                POH = setting.POH,
                ShowInAssessmentText = setting.ShowInAssessmentText,
                ShowInChart = setting.ShowInChart,
                TableFriendlyName = setting.TableFriendlyName,
                ShowInParameterTable = setting.ShowInParameterTable,
                ParameterId = setting.ParameterId,
                ParameterSubHeader = setting.ParameterSubHeader,
                ParameterHeader = setting.ParameterHeader,
                ParameterOrder = setting.ParameterOrder,
            }).ToList();
            var parametersToCheckUnique = new Dictionary<string, ParameterViewModel>();
            foreach (ParameterViewModel parameter in parameterList)
            {
                //parameter.EPParameterName = parameter.DatabaseName ?? "-";
                if (!parametersToCheckUnique.ContainsKey(parameter.ParameterId.ToString()))
                {
                    parametersToCheckUnique.Add(parameter.ParameterId.ToString(), parameter);
                }
            }
            var references = _cacheService.GetReferences();
            foreach (var reference in references)
            {
                try
                {
                    if (reference != null && reference.ParameterId != null)
                    {
                        ParameterViewModel parameter;
                        if (parametersToCheckUnique.TryGetValue(reference.ParameterId.ToString(), out parameter))
                        {
                            if (parameter != null && reference.NormalRangeLower.HasValue && reference.NormalRangeUpper.HasValue)
                            {
                                // Build reference range
                                var normalLower = Math.Round((double)reference.NormalRangeLower, !parameter.DisplayDecimal.HasValue ? 2 : parameter.DisplayDecimal.Value, MidpointRounding.AwayFromZero);
                                var normalUpper = Math.Round((double)reference.NormalRangeUpper, !parameter.DisplayDecimal.HasValue ? 2 : parameter.DisplayDecimal.Value, MidpointRounding.AwayFromZero);

                                reference.ReferenceRange = string.Format("{0} - {1}", normalLower, normalUpper);


                                if (parameter.AvailableReferences == null)
                                {
                                    parameter.AvailableReferences = new List<ParameterReference>();
                                }

                                parameter.AvailableReferences.Add(reference);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message.ToString());
                }
            }

            return parameterList;
        }

        private ParameterViewModel CloneParameter(ParameterViewModel parameter, Exam exam, Dictionary<string, DicomSRDto.Parameter> epParameters)
        {
            //ParameterViewModel clonedParam = parameter.Clone();
            //clonedParam.Exam = exam;

            //DicomSRDto.Parameter epParameter = null;
            ////if (parameter.EPParameterName != null && epParameters.TryGetValue(parameter.EPParameterName.Trim(), out epParameter))
            //if (parameter.ParameterId != null && epParameters.TryGetValue(parameter.ParameterId.Trim(), out epParameter))
            //{
            //    if (epParameter != null)
            //    {
            //        if (epParameter.ResultValue != null && parameter.DisplayDecimal >= 0)
            //        {
            //            clonedParam.ResultValue = (float)Math.Round(UnitExtension.ConvertFromSI((decimal)epParameter.ResultValue.Value, clonedParam.UnitName), parameter.DisplayDecimal.Value, MidpointRounding.AwayFromZero);
            //        }
            //        else if (epParameter.ResultValue.HasValue)
            //        {
            //            clonedParam.ResultValue = UnitExtension.ConvertFromSI((decimal)epParameter.ResultValue.Value, clonedParam.UnitName);
            //        }
            //    }
            //}
            //clonedParam.Reference = clonedParam.MatchReference(exam.Age, exam.Patient.Gender);
            //return clonedParam;
            return new ParameterViewModel();
        }

        public async Task<PagedResponseDto<ParameterDto.ParameterValue>> GetParameterValues(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch)
        {

            //Expression<Func<ParameterViewModel, bool>> filter = i => !i.IsDeleted;

            //if (!string.IsNullOrEmpty(textSearch))
            //{
            //    Expression<Func<ParameterViewModel, bool>> searchFilter = m =>
            //        m.ParameterName.Contains(textSearch) ||
            //        m.TableFriendlyName.Contains(textSearch) ||
            //        m.UnitName.Contains(textSearch) ||
            //        m.SRT.Contains(textSearch) ||
            //        m.POH.Contains(textSearch) ||
            //        m.Description.Contains(textSearch);

            //    filter = PredicateBuilder.AndAlso(filter, searchFilter);
            //}

            //Expression<Func<ParameterViewModel, ParameterDto.ParameterValue>> selectorExpression = m => new ParameterDto.ParameterValue
            //{
            //    Id = m.Id,
            //    UnitName = m.UnitName,
            //    DatabaseName = m.DatabaseName,
            //    ShowInChart = m.ShowInChart,
            //    ShowInParameterTable = m.ShowInParameterTable,
            //    ShowInAssessmentText = m.ShowInAssessmentText,
            //    ParameterName = m.ParameterName,
            //    TableFriendlyName = m.TableFriendlyName,
            //    TextFriendlyName = m.TextFriendlyName,
            //    DisplayDecimal = m.DisplayDecimal,
            //    Is4D = m.Is4D,
            //    OrderInAssessment = m.OrderInAssessment,
            //    SourceUrl = m.SourceUrl,
            //    SuppressReference = m.SuppressReference,
            //    Priority = m.Priority,
            //    SRT = m.SRT,
            //    POH = m.POH,
            //    Description = m.Description,
            //    FunctionSelector = (int?)m.FunctionSelector
            //};

            //var totalItems = await _parameterRepository.Count(filter);

            //var items = await _parameterRepository
            //     .QueryAndSelectAsync(
            //         selector: selectorExpression,
            //         filter,
            //         orderBy: m => PredicateBuilder.ApplyOrder(m, sortColumnName, "ASC"),
            //         "",
            //         pageSize,
            //         page: currentPage
            //     );

            //return new PagedResponseDto<ParameterDto.ParameterValue>()
            //{
            //    Page = currentPage,
            //    Limit = pageSize,
            //    TotalItems = totalItems,
            //    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            //    Items = (IList<ParameterValue>)items
            //};
            return new PagedResponseDto<ParameterDto.ParameterValue>();
        }

        public async Task UpdateParameterValue(int id, ParameterDto.ParameterValue model)
        {
            //var parameter = await _parameterRepository.Get(id);
            //if (parameter == null)
            //{
            //    throw new Exception("Can not found Parameter");
            //}
            ////parameter.UnitName = model.UnitName;
            ////parameter.DatabaseName = model.DatabaseName;
            //parameter.ShowInChart = model.ShowInChart;
            //parameter.ShowInParameterTable = model.ShowInParameterTable;
            //parameter.ShowInAssessmentText = model.ShowInAssessmentText;
            ////parameter.ParameterName = model.ParameterName;
            //parameter.TableFriendlyName = model.TableFriendlyName;
            //parameter.TextFriendlyName = model.TextFriendlyName;
            //parameter.DisplayDecimal = model.DisplayDecimal;
            ////parameter.Is4D = model.Is4D;
            ////parameter.OrderInAssessment = model.OrderInAssessment;
            ////parameter.SourceUrl = model.SourceUrl;
            ////parameter.SuppressReference = model.SuppressReference;
            //parameter.Description = model.Description;
            //parameter.POH = model.POH;
            ////parameter.Priority = model.Priority;
            ////parameter.SRT = model.SRT;
            //if (parameter.FunctionSelector != (Enum.FunctionSelector?)model.FunctionSelector && model.FunctionSelector > 0 && model.FunctionSelector < 5)
            //{
            //    parameter.FunctionSelector = (Enum.FunctionSelector?)model.FunctionSelector;
            //    await _examPatientRepository.TruncateTable("ExamPatients");
            //}
            //await _parameterRepository.Update(parameter);
            //_cacheService.RemoveKey(CacheKeys.Parameters);
            //_cacheService.RemoveKey(CacheKeys.References);
            //_cacheService.RemoveKey(CacheKeys.AssessmentText);
        }

        public async Task<List<string>> GetXValueSelector()
        {
            List<string> xValueSelector = new List<string> { "Age", "Height", "Weight", "BloodPressure", "BSA" };
            return xValueSelector;
        }
        public async Task<List<EnumFunctionSelectorModel>> GetFunctionNameSelector()
        {
            List<EnumFunctionSelectorModel> functionSelectorModels = ((ApplicationCore.Enum.FunctionSelector[])System.Enum.GetValues(typeof(ApplicationCore.Enum.FunctionSelector)))
            .Select(c => new EnumFunctionSelectorModel() { Value = (int)c, FunctionName = c.ToString() }).ToList();
            return functionSelectorModels;
        }

        public Dictionary<string, ParameterViewModel> GetExamParameters(Study study, List<ParameterViewModel> parametersWithReference)
        {
            var parametersNoDerivedValueGroupedById = new Dictionary<string, List<StudyParameter>>();
            var parametersWithDerivedValue = new Dictionary<string, StudyParameter>();

            var valueByGroupedById = new List<StudyParameter>();

            foreach (var rawParameter in study.Parameters)
            {
                if (rawParameter.Derivation > 0)
                {
                    if (!parametersWithDerivedValue.ContainsKey(rawParameter.ParameterId.ToString()))
                    {
                        parametersWithDerivedValue.Add(rawParameter.ParameterId.ToString(), rawParameter);
                    }

                }
                else
                {
                    parametersNoDerivedValueGroupedById.TryGetValue(rawParameter.ParameterId.ToString(), out valueByGroupedById);
                    if (valueByGroupedById == null)
                        parametersNoDerivedValueGroupedById[rawParameter.ParameterId.ToString()] = new List<StudyParameter>();
                    parametersNoDerivedValueGroupedById[rawParameter.ParameterId.ToString()].Add(rawParameter);
                }
            }

            var finalparameterVMList = new Dictionary<string, ParameterViewModel>();

            var studyParam = new StudyParameter();

            foreach (var parameterVM in parametersWithReference)
            {
                parametersWithDerivedValue.TryGetValue(parameterVM.ParameterId.ToString(), out studyParam);
                var useValueFromExam = false;
                if (true /*custom function selector has same value with code meaning from studyParam.Derivation */
                    /*example: parameterVM.FunctionSelector is Enum.FunctionSelector.Avg == code meaning from studyParam.Derivation is 'Mean' */

                    && parametersWithDerivedValue.ContainsKey(parameterVM.ParameterId.ToString()))
                {
                    parameterVM.ParameterId = studyParam.ParameterId;
                    parameterVM.ResultValue = studyParam.ResultValue;
                    parameterVM.UnitName = studyParam.ValueUnit;//get code meaning from measurement unit key  studyParam.IndexMeasurementUnit;
                    parameterVM.ParameterName = GetParameterNameByParameterId(parameterVM.ParameterId.ToString()); //get globalshortname from ManufacturerDicomParameters table using TemParameterId.ToString()

                    if (!finalparameterVMList.ContainsKey(parameterVM.ParameterId.ToString()))
                    {
                        finalparameterVMList.Add(parameterVM.ParameterId.ToString(), parameterVM);
                    }

                    continue;
                }

                if (!parametersNoDerivedValueGroupedById.ContainsKey(parameterVM.ParameterId.ToString()))
                {
                    continue;
                }

                var agregatedValue = 0f;
                studyParam = parametersNoDerivedValueGroupedById[parameterVM.ParameterId.ToString()].Last();
                parameterVM.ParameterId = studyParam.ParameterId;
                parameterVM.UnitName = studyParam.ValueUnit;//get code meaning from measurement unit key  studyParam.IndexMeasurementUnit;
                parameterVM.ParameterName = GetParameterNameByParameterId(parameterVM.ParameterId.ToString()); ; //get globalshortname from ManufacturerDicomParameters table using TemParameterId.ToString()
                if (parameterVM.FunctionSelector == Enum.FunctionSelector.Avg)
                {
                    agregatedValue = parametersNoDerivedValueGroupedById[parameterVM.ParameterId.ToString()].Average(m => m.ResultValue);
                }
                else if (parameterVM.FunctionSelector == Enum.FunctionSelector.Min)
                {
                    agregatedValue = parametersNoDerivedValueGroupedById[parameterVM.ParameterId.ToString()].Min(m => m.ResultValue);
                }
                else if (parameterVM.FunctionSelector == Enum.FunctionSelector.Max)
                {
                    agregatedValue = parametersNoDerivedValueGroupedById[parameterVM.ParameterId.ToString()].Max(m => m.ResultValue);
                }
                else  //parameterVM.FunctionSelector == Enum.FunctionSelector.Latest or parameterVM.FunctionSelector is null
                {
                    agregatedValue = studyParam.ResultValue;
                }
                parameterVM.ResultValue = agregatedValue;
                if (!finalparameterVMList.ContainsKey(parameterVM.ParameterId.ToString()))
                {
                    finalparameterVMList.Add(parameterVM.ParameterId.ToString(), parameterVM);
                }


                //priority of parameter selection ?

                //index parameters ?

                //wallsegment scoring ?

            }

            var parameterByOrders = new Dictionary<string, ParameterViewModel>();

            if (finalparameterVMList != null && finalparameterVMList.Count() > 0)
            {
                var parameterViewModelInOrder = new ParameterViewModel();
                foreach (var parameterVM in finalparameterVMList)
                {

                    if (parameterByOrders == null || parameterByOrders.Count == 0)
                    {
                        if (!parameterByOrders.ContainsKey(parameterVM.Value.ParameterName))
                        {
                            parameterByOrders.Add(parameterVM.Value.ParameterName, parameterVM.Value);
                        }
                    }
                    else
                    {
                        parameterByOrders.TryGetValue(parameterVM.Value.ParameterName.ToString(), out parameterViewModelInOrder);

                        if (parameterViewModelInOrder == null)
                        {
                            if (!parameterByOrders.ContainsKey(parameterVM.Value.ParameterName))
                            {
                                parameterByOrders.Add(parameterVM.Value.ParameterName, parameterVM.Value);
                            }
                        }
                        else
                        {
                            if (parameterVM.Value.ParameterOrder > parameterViewModelInOrder.ParameterOrder && parameterVM.Value.ResultValue > 0)
                            {
                                parameterByOrders[parameterVM.Value.ParameterName.ToString()] = parameterVM.Value;
                            }
                        }
                    }
                }
            }

            var parameterResults = new Dictionary<string, ParameterViewModel>();

            foreach (KeyValuePair<string, ParameterViewModel> paramOrder in parameterByOrders)
            {
                if (!parameterResults.ContainsKey(paramOrder.Value.ParameterId)) 
                {
                    parameterResults.Add(paramOrder.Value.ParameterId, paramOrder.Value);
                }
            }

            

            return parameterResults;
        }

        private string GetParameterNameByParameterId(string parameterId)
        {
            string parameterName = string.Empty;
            if (!string.IsNullOrEmpty(parameterId))
            {
                var manufacturerDicomParameter = _parameterReferenceRepository.FirstOrDefault(x => x.ParameterId == parameterId);

                if (manufacturerDicomParameter != null)
                {
                    parameterName = string.IsNullOrEmpty(manufacturerDicomParameter.ParameterNameLogic) ? string.Empty : manufacturerDicomParameter.ParameterNameLogic;
                }

            }

            return parameterName;
        }
    }
}
