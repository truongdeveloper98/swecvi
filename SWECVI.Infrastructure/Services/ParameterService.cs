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
using SWECVI.Infrastructure.Repositories;
using SWECVI.ApplicationCore.Utilities;

namespace SWECVI.Infrastructure.Services
{
    public class ParameterService : IParameterService
    {

        private readonly ICacheService _cacheService;
        private readonly ILogger<ParameterService> _logger;
        private readonly IParameterReferenceRepository _parameterReferenceRepository;
        private readonly IParameterSettingRepository _parameterSettingRepository;
        private readonly IParameterRepository _studyRepository;
        private readonly IParameterRepository _parameterRepository;
        private readonly IStudyRepository _studyExamRepository;

        public ParameterService(ILogger<ParameterService> logger,
                                IParameterRepository studyRepository,
                                ICacheService cacheService, 
                                IParameterSettingRepository parameterSettingRepository,
                                IParameterRepository parameterRepository,
                                IStudyRepository studyExamRepository,
                                IParameterReferenceRepository parameterReferenceRepository)
        {
            _cacheService = cacheService;
            _parameterReferenceRepository = parameterReferenceRepository;
            _logger = logger;
            _parameterSettingRepository = parameterSettingRepository;
            _studyRepository = studyRepository;
            _parameterRepository = parameterRepository;
            _studyExamRepository = studyExamRepository;
        }

        public async Task<List<ParameterDto.ParameterValuesChartByPatientId>> GetValuesByParametersForPatient(int[] ids, int patientId)
        {

            var patientIdByStudy = (_studyExamRepository.FirstOrDefault(x => x.Id == patientId))?.PatientId;

            List<ParameterDto.ParameterValuesChartByPatientId> parameterValuesCharts = new List<ParameterValuesChartByPatientId>();


            if (patientIdByStudy == null)
            {
                return parameterValuesCharts;
            }

            if (ids.Length == 0)
            {
                return parameterValuesCharts;
            }
            var parameters = await _parameterSettingRepository.QueryAsync(i => ids.Contains(i.Id) && !String.IsNullOrEmpty(i.TableFriendlyName));

            Expression<Func<StudyParameter, bool>> filter = i => i.HospitalStudy.Patient.Id == patientIdByStudy;

            var examPatients = await _parameterRepository.QueryAsync(filter: filter, includeProperties: "HospitalStudy");

            foreach (var parameter in parameters)
            {
                var parameterValuesChart = new ParameterDto.ParameterValuesChartByPatientId
                {
                    ParameterName = parameter.TableFriendlyName,
                    ValueByTimes = examPatients.Where(m => m.ParameterId == parameter.ParameterId && m.HospitalStudy.StudyDateTime != null)
                                    .Select(m => new ValueByTime
                                    {
                                        Value = (decimal)Math.Round(m.ResultValue,0, MidpointRounding.AwayFromZero),
                                        Time = (m.ResultValue != null) ? m.HospitalStudy.StudyDateTime : null
                                    }).ToList()
                };
                parameterValuesCharts.Add(parameterValuesChart);
            }
            return parameterValuesCharts;
        }


        public async Task<List<ParameterDto.ParameterStaticChart>> GetValuesByParameters(int ySelectorId, string xValueSelector, string? period)
        {
            var parameter = (await _parameterSettingRepository.QueryAsync(i => i.Id == ySelectorId && !String.IsNullOrEmpty(i.TableFriendlyName))).FirstOrDefault();

            if (parameter == null)
                throw new Exception("Can not found parameter");

            DateTime today = DateTime.UtcNow.Date;
            Dictionary<string, int> countByValueSelected = new Dictionary<string, int>();

            Expression<Func<StudyParameter, bool>> filterStudy = i => !string.IsNullOrEmpty(i.HospitalStudy.StudyDescription) && i.ParameterId == parameter.ParameterId;

            if (!string.IsNullOrEmpty(period))
            {
                if (period.Equals("today"))
                {
                    Expression<Func<StudyParameter, bool>> searchFilter = i => i.HospitalStudy.StudyDateTime.Year == today.Year && i.HospitalStudy.StudyDateTime.Month == today.Month && i.HospitalStudy.StudyDateTime.Day == today.Day;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-day-ago"))
                {
                    today = today.AddDays(-1);

                    Expression<Func<StudyParameter, bool>> searchFilter = i => i.HospitalStudy.StudyDateTime.Year == today.Year && i.HospitalStudy.StudyDateTime.Month == today.Month && i.HospitalStudy.StudyDateTime.Day == today.Day;

                    filterStudy = filterStudy.AndAlso(searchFilter);

                }
                else if (period.Equals("2-days-ago"))
                {
                    var dayAgo = today.AddDays(-2);

                    Expression<Func<StudyParameter, bool>> searchFilter = m => m.HospitalStudy.StudyDateTime < today && m.HospitalStudy.StudyDateTime >= dayAgo;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-week-ago"))
                {
                    DateTime lastWeekSunday = today.AddDays(-(int)today.DayOfWeek);
                    lastWeekSunday = lastWeekSunday.AddDays(1).AddTicks(-1);
                    DateTime lastWeekMonday = lastWeekSunday.AddDays(-6);
                    lastWeekMonday = lastWeekMonday.AddTicks(1);

                    Expression<Func<StudyParameter, bool>> searchFilter = e => e.HospitalStudy.StudyDateTime >= lastWeekMonday && e.HospitalStudy.StudyDateTime <= lastWeekSunday;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-month-ago"))
                {
                    today = today.AddMonths(-1);

                    Expression<Func<StudyParameter, bool>> searchFilter = e => e.HospitalStudy.StudyDateTime.Month == today.Month && e.HospitalStudy.StudyDateTime.Year == today.Year;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("this-year"))
                {
                    Expression<Func<StudyParameter, bool>> searchFilter = e => e.HospitalStudy.StudyDateTime.Year == today.Year;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
            }

            var examTypes = await _studyRepository.QueryAsync(filterStudy, null, "HospitalStudy");


            List<ParameterDto.ParameterStaticChart> parameterStaticCharts = new List<ParameterStaticChart>();
            if (xValueSelector == "Age")
            {
                var existedAge = examTypes.Where(m => m.HospitalStudy.Age != null);
                if (existedAge != null)
                {
                    parameterStaticCharts = examTypes.Select(m => new ParameterStaticChart
                    {
                        XValue = m.HospitalStudy.Age.ToString(),
                        YValue = (decimal?)m.ResultValue
                    }).ToList();
                }
            }
            else if (xValueSelector == "Height")
            {
                var existedHeight = examTypes.Where(m => m.HospitalStudy.Height != null);
                if (existedHeight != null)
                {
                    parameterStaticCharts = examTypes.Select(m => new ParameterStaticChart
                    {
                        XValue = m.HospitalStudy.Height.ToString(),
                        YValue = (decimal?)m.ResultValue
                    }).ToList();
                }
            }
            else if (xValueSelector == "Weight")
            {
                var existedWeight = examTypes.Where(m => m.HospitalStudy.Weight != null);
                if (existedWeight != null)
                {
                    parameterStaticCharts = examTypes.Select(m => new ParameterStaticChart
                    {
                        XValue = m.HospitalStudy.Weight.ToString(),
                        YValue = (decimal?)m.ResultValue
                    }).ToList();
                }
            }
            else if (xValueSelector == "BloodPressure")
            {
                var existedBloodPressure = examTypes.Where(m => m.HospitalStudy.DiastoilccBloodPressure != null);
                if (existedBloodPressure != null)
                {
                    parameterStaticCharts = examTypes.Where(m => m.HospitalStudy.DiastoilccBloodPressure != null).Select(m => new ParameterStaticChart
                    {
                        XValue = m.HospitalStudy.DiastoilccBloodPressure.ToString(),
                        YValue = (decimal?)m.ResultValue
                    }).ToList();
                }
            }
            else if (xValueSelector == "BSA")
            {
                var existedBSA = examTypes.Where(m => m.HospitalStudy.BodySurfaceArea != null);
                if (existedBSA != null)
                {
                    parameterStaticCharts = examTypes.Select(m => new ParameterStaticChart
                    {
                        XValue = m.HospitalStudy.BodySurfaceArea.ToString(),
                        YValue = (decimal?)m.ResultValue
                    }).ToList();
                }
            }
            return parameterStaticCharts;
        }
        public async Task<List<ParameterDto.ParameterSelector>> GetParameterNames()
        {
            var cachedParameters = _cacheService.GetParameterSettings();
            var groupTableFriendlyName = cachedParameters.Where(i => !String.IsNullOrEmpty(i.TableFriendlyName))
            .GroupBy(m => m.TableFriendlyName);
            List<ParameterDto.ParameterSelector> parameters = groupTableFriendlyName.Select(i => new ParameterDto.ParameterSelector()
            {
                Id = i.Last().Id,
                ParameterName = i.Last().TableFriendlyName!
            }).OrderBy(i => i.ParameterName).ToList();
            return parameters;
        }

        public async Task<List<ParameterViewModel>> GetParametersWithReferences()
        {
            var cachedParameterSettings = await _parameterSettingRepository.Get();


            var parameterList = new List<ParameterViewModel>();

            var manufacturerDicomParameters = _cacheService.GetManufacturerDicomParameters();

            foreach(var manufacturerDicom in manufacturerDicomParameters)
            {
                var param = new ParameterViewModel()
                {
                    ParameterId = manufacturerDicom.ParameterId
                };

                var setting = cachedParameterSettings.Where(x=>x.ParameterId == manufacturerDicom.ParameterId).FirstOrDefault();

                if(setting != null)
                {
                    param.Description = setting.Description;
                    param.FunctionSelector = setting.FunctionSelector;
                    param.DisplayDecimal = setting.DisplayDecimal;
                    param.POH = setting.POH;
                    param.ShowInAssessmentText = setting.ShowInAssessmentText;
                    param.ShowInChart = setting.ShowInChart;
                    param.TableFriendlyName = setting.TableFriendlyName;
                    param.ShowInParameterTable = setting.ShowInParameterTable;
                    param.ParameterSubHeader = setting.ParameterSubHeader;
                    param.ParameterHeader = setting.ParameterHeader;
                    param.ParameterOrder = setting.ParameterOrder;
                }

                parameterList.Add(param);
            }

            var parametersToCheckUnique = new Dictionary<string, ParameterViewModel>();
            foreach (ParameterViewModel parameter in parameterList)
            {
                //parameter.EPParameterName = parameter.DatabaseName ?? "-";
                if (!parametersToCheckUnique.ContainsKey(parameter.ParameterId.ToString()))
                {
                    parametersToCheckUnique.Add(parameter.ParameterId.ToString(), parameter);
                }
            }

            var references = await _parameterReferenceRepository.Get();
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
            var manufacturerDicomParameters = _cacheService.GetManufacturerDicomParameters();
            string parameterName = string.Empty;
            if (!string.IsNullOrEmpty(parameterId))
            {
                var manufacturerDicomParameter = manufacturerDicomParameters.FirstOrDefault(x => x.ParameterId == parameterId);

                if (manufacturerDicomParameter != null)
                {
                    parameterName = string.IsNullOrEmpty(manufacturerDicomParameter.ProviderParameterShortName) ? string.Empty : manufacturerDicomParameter.ProviderParameterShortName;
                }

            }

            return parameterName;
        }
    }
}
