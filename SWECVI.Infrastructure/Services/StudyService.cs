using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.ViewModels.MirthConnect;
using SWECVI.ApplicationCore.CustomExceptions;
using SWECVI.ApplicationCore.Common;
using SWECVI.ApplicationCore.Business;
using Microsoft.Extensions.Logging;
using SWECVI.ApplicationCore;
using SWECVI.ApplicationCore.ViewModels.ElasticSearch;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Constants;
using SWECVI.ApplicationCore.Utilities;
using SWECVI.ApplicationCore.ViewModels.Hospital;
using System.Linq.Expressions;
using static SWECVI.ApplicationCore.Enum;
using NPOI.SS.Formula.Functions;

namespace SWECVI.Infrastructure.Services
{
    public class StudyService : IStudyService
    {
        private readonly IParameterService _parameterService;
        private readonly IStudyRepository _studyRepository;
        private readonly IStudyFindingRepository _studyFindingRepository;
        private readonly IFindingStructureRepository _findingStructureRepository;
        private readonly ILogger<StudyService> _logger;
        private readonly ICacheService _cacheService;
        private readonly IParameterReferenceRepository _parameterReferenceRepository;
        private readonly IAssessmentRepository _assessmentRepository;

        public StudyService(IParameterService parameterService,
                IStudyRepository studyRepository,
                ILogger<StudyService> logger,
                IStudyFindingRepository studyFindingRepository,
                IParameterReferenceRepository parameterReferenceRepository,
                ICacheService cacheService,
                IFindingStructureRepository findingStructureRepository,
                IAssessmentRepository assessmentRepository
            )
        {
            _parameterReferenceRepository = parameterReferenceRepository;
            _parameterService = parameterService;
            _logger = logger;
            _studyRepository = studyRepository;
            _cacheService = cacheService;
            _studyFindingRepository = studyFindingRepository;
            _assessmentRepository = assessmentRepository;
            _findingStructureRepository = findingStructureRepository;
        }

        public Task<bool> Create(HospitalStudyViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ExamsDto.ExamReport> GetParametersByExam(int Id, int examId)
        {
            var study = await _studyRepository.Get(examId, "Patient,Parameters");

            var bsa = ExamHelper.CalculateBSA(study.Height, study.Height);

            if (study == null)
            {
                throw new CustomNotFoundException("Exam not found");
            }
            if (study.Patient == null)
            {
                throw new CustomNotFoundException("Patient not found");
            }
            var patient = study.Patient;
            var parametersWithReference = await _parameterService.GetParametersWithReferences();

            var studyVM = new StudyViewModel();
            studyVM.Age = ExamHelper.Age(patient.DOB, patient.PatientId.ToString(), study.StudyDateTime);
            studyVM.BodySurfaceArea = study.BodySurfaceArea;
            studyVM.AccessionNumber = study.AccessionNumber;
            studyVM.Height = study.Height;
            studyVM.Weight = study.Weight;
            studyVM.StudyDescription = study.StudyDescription;
            studyVM.StudyDateTime = study.StudyDateTime;
            studyVM.PatientViewModel = new PatientViewModel
            {
                PatientId = patient.PatientId,
                DOB = patient.DOB.ToString(),
                PatientName = patient.FirstName,
                Sex = patient.Sex
            };
            var gender = GenderExtension.ConvertFromString(studyVM.PatientViewModel.Sex == "O" ? "M" : studyVM.PatientViewModel.Sex);

            foreach (var parameter in parametersWithReference)
            {
                parameter.Reference = parameter.MatchReference(studyVM.Age, gender);
            }

            var parameters = _parameterService.GetExamParameters(study, parametersWithReference);

            foreach (var p in parameters)
            {
                ParameterHelper.RoundResultValue(p.Value);
            }

            string parameterText = string.Empty;

            string divider_dash = "-------------------------------------";

            var assessmentTexts = await _assessmentRepository.Get();


            string findingText = divider_dash + "\n";
            findingText += assessmentTexts.FirstOrDefault(x=>x.ACode == -4 && x.DCode == -4)?.DescriptionReportText + "\n";
            findingText += divider_dash + "\n";

            Expression<Func<StudyFinding, FingdingStudyItem>> selectorExpression = m => new FingdingStudyItem
            {
                Id = m.FindingStructureId,
                Value = m.SelectOptions,
                InputLabel = m.FindingStructure.InputLabel,
                InputType = m.FindingStructure.InputType,
                TabName = m.FindingStructure.TabName,
                OrderInReport = m.FindingStructure.OrderInReport,
            };


            var studyFindings = await _studyFindingRepository.QueryAndSelectAsync(selectorExpression,x => x.StudyId == study.Id,
                null, "FindingStructure");

            if(studyFindings != null && studyFindings.Count > 0)
            {
                studyFindings = studyFindings.OrderBy(x => x.OrderInReport).ToList();

                foreach(var finding in studyFindings)
                {
                    findingText += finding.InputLabel + ": " + finding.Value + "\n";
                }
            }
            else
            {
                findingText += assessmentTexts.FirstOrDefault(x => x.ACode == -6 && x.DCode == -6)?.DescriptionReportText + "\n";
            }


            var measurements = parameters.Where(x => x.Value.ResultValue > 0 && !string.IsNullOrEmpty(x.Value.ParameterName))
                                         .Select(x => new ExamsDto.Measurement()
                                         {
                                             DisplayDecimal = x.Value.DisplayDecimal,
                                             Value = (decimal?)x.Value.ResultValue,
                                             FriendlyName = x.Value.TextFriendlyName,
                                             Id = x.Value.Id,
                                             Is4D = x.Value.Is4D,
                                             Name = x.Value.ParameterName,
                                             POH = x.Value.PartOfHeart,
                                             Reference = x.Value.Reference?.ReferenceRange,
                                             Unit = x.Value.UnitName,
                                             IsOutsideReferenceRange = x.Value.IsOutsideReferenceRange,
                                             ParameterHeader = x.Value.ParameterHeader,
                                             ParameterSubHeader = x.Value.ParameterSubHeader,
                                             ParameterOrder = x.Value.ParameterOrder,
                                         })
                                          .ToList();

           

            var parameterWithIndexs = new List<ExamsDto.Measurement>();

            foreach (var measurement in measurements)
            {
                if (ParameterIndexName.parametersWithIndex.Contains(measurement.Name))
                {
                    parameterWithIndexs.Add(new ExamsDto.Measurement()
                    {
                        DisplayDecimal = measurement.DisplayDecimal,
                        Value = study.BodySurfaceArea != null && study.BodySurfaceArea > 0 ?  ParameterHelper.Round((decimal?)(Convert.ToDouble(measurement.Value.Value) / study.BodySurfaceArea),2) : null,
                        Is4D = measurement.Is4D,
                        Name = measurement.Name + "Index",
                        POH = measurement.POH,
                        Reference = measurement.Reference,
                        Unit = measurement.Unit,
                        IsOutsideReferenceRange = measurement.IsOutsideReferenceRange,
                        ParameterHeader = measurement.ParameterHeader,
                        ParameterSubHeader = measurement.ParameterSubHeader,
                        ParameterOrder = measurement.ParameterOrder,
                    });
                }
            }

            foreach(var index in parameterWithIndexs)
            {
                measurements.Add(index);
            }

            var parameterResults = measurements.GroupBy(x => x.ParameterHeader)
                                               .Select(x => new ParameterAssessmentViewModel()
                                               {
                                                   ParameterHeader = x.Key,
                                                   Measurements = x.GroupBy(i=>i.ParameterSubHeader)
                                                                   .Select(i=> new ParameterSubAssessmentViewModel()
                                                                   {
                                                                       ParameterSubHeader = i.Key,
                                                                       Measurements = i.ToList().OrderBy(y=>y.ParameterOrder).ToList(),
                                                                   })
                                                                   .OrderBy(x=>x.ParameterSubHeader)
                                                                   .ToList()
                                               })
                                               .ToList()
                                               .OrderBy(x=>x.ParameterHeader);

           

            parameterText += divider_dash + "\n";
            parameterText += assessmentTexts.FirstOrDefault(x => x.ACode == -5 && x.DCode == -5)?.DescriptionReportText + "\n";
            parameterText += divider_dash + "\n";

            foreach (var measurement in parameterResults)
            {
                if(!string.IsNullOrEmpty(measurement.ParameterHeader))
                {
                     parameterText += measurement.ParameterHeader + "\n";
                }

                foreach(var sub in measurement.Measurements)
                {
                    if (!string.IsNullOrEmpty(sub.ParameterSubHeader))
                    {
                        parameterText += "-" + sub.ParameterSubHeader + ": " + "\n";
                    }
                    string paramTextTemp = string.Empty;

                    foreach (var param in sub.Measurements)
                    {
                        if (!string.IsNullOrEmpty(param.Reference))
                        {

                            if (param.IsOutsideReferenceRange.HasValue)
                            {
                                if ((bool)param.IsOutsideReferenceRange)
                                {
                                    paramTextTemp += param.Name + ": " + param.Value + "* " + param.Unit + " (" + param.Reference + ")";
                                }
                                else
                                {
                                    paramTextTemp += param.Name + ": " + param.Value + " " + param.Unit + " (" + param.Reference + ")";
                                }
                            }
                            else
                            {
                                paramTextTemp += param.Name + ": " + param.Value + " " + param.Unit + " (" + param.Reference + ")";
                            }
                        }
                        else
                        {
                            paramTextTemp += param.Name + ": " + param.Value + " " + param.Unit;
                        }

                    }
                    parameterText += paramTextTemp;
                    parameterText += "\n";
                }
            }

            // buid report text
            var reportGenerator = new EchoReportGenerator(_logger);

            var valDataList = LoadValveData();

            var parameterReferences = await _parameterReferenceRepository.Get();

            var assessmentText = reportGenerator.GetAssessmentText(measurements, studyVM, valDataList, assessmentTexts, parameterReferences, parameterText, findingText);

            return new ExamsDto.ExamReport()
            {
                AssessmentText = assessmentText,
                Measurements = measurements,
                StressText = string.Empty,
                SSN = study.AccessionNumber,
                ExamDate = study.StudyDateTime.ToString("dd-MM-yyyy"),
                PatientName = study.Patient.FirstName,
                StudyType = study.StudyDescription
            };
        }

        private List<ValveData> LoadValveData()
        {
            //var valveData = _cacheService.GetValves();
            var valveDataList = new List<ValveData>();
            //foreach (var item in valveData.GroupBy(x => x.Name))
            //{
            //    List<string> listValveValue = item.Select(x => x.Value).ToList();
            //    listValveValue.Insert(0, Resources.SelectGrade);

            //    valveDataList.Add(
            //        new ValveData
            //        {
            //            Name = item.Key,
            //            Value = Resources.SelectGrade,
            //            SelectList = listValveValue
            //        });
            //}
            return valveDataList;
        }

        public async Task<List<string>> GetCodeMeanings()
        {
            var codeMeanings = (await _findingStructureRepository.QueryAsync())
                               .Select(i => i.TabName).Distinct().ToList();
            return codeMeanings;
        }

        public async Task<List<ExamsDto.CodeMeaningChart>> GetTextValueByCodeMeaningCharts(string[] codeMeaningSelect, string? period)
        {

            List<ExamsDto.CodeMeaningChart> codeMeaningCharts = new List<ExamsDto.CodeMeaningChart>();
            DateTime today = DateTime.UtcNow.Date;

            Expression<Func<StudyFinding, bool>> filterStudy = i => codeMeaningSelect.Contains(i.FindingStructure.TabName);

            if (!string.IsNullOrEmpty(period))
            {
                if (period.Equals("today"))
                {
                    Expression<Func<StudyFinding, bool>> searchFilter = i => i.Study.StudyDateTime.Year == today.Year && i.Study.StudyDateTime.Month == today.Month && i.Study.StudyDateTime.Day == today.Day;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-day-ago"))
                {
                    today = today.AddDays(-1);

                    Expression<Func<StudyFinding, bool>> searchFilter = i => i.Study.StudyDateTime.Year == today.Year && i.Study.StudyDateTime.Month == today.Month && i.Study.StudyDateTime.Day == today.Day;

                    filterStudy = filterStudy.AndAlso(searchFilter);

                }
                else if (period.Equals("2-days-ago"))
                {
                    var dayAgo = today.AddDays(-2);

                    Expression<Func<StudyFinding, bool>> searchFilter = m => m.Study.StudyDateTime < today && m.Study.StudyDateTime >= dayAgo;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-week-ago"))
                {
                    DateTime lastWeekSunday = today.AddDays(-(int)today.DayOfWeek);
                    lastWeekSunday = lastWeekSunday.AddDays(1).AddTicks(-1);
                    DateTime lastWeekMonday = lastWeekSunday.AddDays(-6);
                    lastWeekMonday = lastWeekMonday.AddTicks(1);

                    Expression<Func<StudyFinding, bool>> searchFilter = e => e.Study.StudyDateTime >= lastWeekMonday && e.Study.StudyDateTime <= lastWeekSunday;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-month-ago"))
                {
                    today = today.AddMonths(-1);

                    Expression<Func<StudyFinding, bool>> searchFilter = e => e.Study.StudyDateTime.Month == today.Month && e.Study.StudyDateTime.Year == today.Year;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("this-year"))
                {
                    Expression<Func<StudyFinding, bool>> searchFilter = e => e.Study.StudyDateTime.Year == today.Year;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
            }

            var sutdyFindings = await _studyFindingRepository.QueryAsync(filterStudy, null, "Study,FindingStructure");



            var studyFindingValues = sutdyFindings.Select(m => new
            {
                CodeMeaning = m.FindingStructure.TabName,
                TextValue = m.FindingStructure.BoxHeader,
                ValueString = m.SelectOptions
            }).ToList();

            var groupByCodeMeaning = studyFindingValues.GroupBy(m => m.CodeMeaning);
            foreach (var it in groupByCodeMeaning)
            {
                ExamsDto.CodeMeaningChart codeMeaningChart = new ExamsDto.CodeMeaningChart();
                codeMeaningChart.CodeMeaning = it.Key;
                codeMeaningChart.TextValueByCodeMeanings = it.GroupBy(m => m.TextValue).Select(m => new ExamsDto.TextValueByCodeMeaning
                {
                    TextValue = m.Key,
                    Count = m.Count()
                }).ToList();
                codeMeaningCharts.Add(codeMeaningChart);
            }

            return codeMeaningCharts;
        }

        public async Task<StudyParameterElasticsearchModel> GetStudy(int id)
        {
            var exam = await _studyRepository.Get(id, includeProperties: "Patient");
            if (exam == null)
            {
                throw new Exception("Study not found");
            }

            StudyParameterElasticsearchModel examDto = new StudyParameterElasticsearchModel
            {
                Id = exam.Id.ToString(),
                Date = exam.StudyDateTime.Date.ToString(),
                Time = exam.StudyDateTime.TimeOfDay.ToString(),
                DicomPatientId = exam.Patient.PatientId.ToString(),
                PatientId = exam.Patient.Id.ToString(),
                PatientName = exam.Patient.LastName,
            };
            return examDto;
        }

        public async Task<List<ExamsDto.ExamsCount>> GetExamsByPeriod(string? period)
        {
            DateTime today = DateTime.UtcNow.Date;
            List<ExamsDto.ExamsCount> examsCounts = new List<ExamsDto.ExamsCount>();

            Expression<Func<Study, bool>> filterStudy = m => m.StudyDateTime != DateTime.MinValue;

            if (string.IsNullOrEmpty(period))
            {
                var result = await _studyRepository.QueryAsync(filterStudy);

                examsCounts = result.GroupBy(x=>x.StudyDateTime.Year)
                                    .Select(x => new ExamsDto.ExamsCount()
                                    {
                                        Time = x.Key,
                                        Count = x.Count()
                                    })
                                    .ToList();
            }
            else if (period.Equals("this-year"))
            {
                Expression<Func<Study, bool>> searchFilter = i => i.StudyDateTime.Year == DateTime.Now.Year;
                filterStudy = filterStudy.AndAlso(searchFilter);

                var result = await _studyRepository.QueryAsync(filterStudy);

                for (int i = 1; i <= 12; i++)
                {
                    ExamsDto.ExamsCount examsCount = new ExamsDto.ExamsCount
                    {
                        Time = i,
                        Count = result.Where(exam => exam.StudyDateTime.Month == i).Count()
                    };
                    examsCounts.Add(examsCount);
                }
            }
            //else if (period.Equals("1-month-ago"))
            //{
            //    today = today.AddMonths(-1);
            //    int lastDayOfMonth = DateTime.DaysInMonth(today.Year, today.Month);
            //    exams = exams.Where(i => i.StudyDateTime.Value.Year == today.Year && i.StudyDateTime.Value.Month == today.Month);
            //    for (int i = 1; i <= lastDayOfMonth; i++)
            //    {
            //        ExamsDto.ExamsCount examsCount = new ExamsDto.ExamsCount
            //        {
            //            Time = i,
            //            Count = exams.Where(exam => exai.StudyDateTime.Day == i).Count()
            //        };
            //        examsCounts.Add(examsCount);
            //    }
            //}
            //else if (period.Equals("1-week-ago"))
            //{
            //    DateTime lastWeekSunday = today.AddDays(-(int)today.DayOfWeek);
            //    DateTime lastWeekMonday = lastWeekSunday.AddDays(-6);

            //    exams = exams.Where(i => i.StudyDateTime.Value.Year == today.Year && i.StudyDateTime.Value.Month == today.Month);
            //    for (int i = 0; i < 7; i++)
            //    {
            //        ExamsDto.ExamsCount examsCount = new ExamsDto.ExamsCount
            //        {
            //            Time = lastWeekMonday.Day + i,
            //            Count = exams.Where(exam => exai.StudyDateTime.Day == (lastWeekMonday.Day + i)).Count()
            //        };
            //        examsCounts.Add(examsCount);
            //    }
            //}
            //else if (period.Equals("2-days-ago"))
            //{
            //    today = today.AddDays(-2);
            //    exams = exams.Where(i => i.StudyDateTime.Value.Year == today.Year && i.StudyDateTime.Value.Month == today.Month);

            //    var tempQuery = exams.Where(i => i.StudyDateTime.Value.Year == today.Year && i.StudyDateTime.Value.Month == today.Month && i.StudyDateTime.Value.Day == today.Day);
            //    for (int i = 0; i < 24; i += 1)
            //    {
            //        ExamsDto.ExamsCount examsCount = new ExamsDto.ExamsCount
            //        {
            //            Time = i,
            //            Count = tempQuery.Where(exam => exam.FormattedTime.Value.Hours == i).Count()
            //        };
            //        examsCounts.Add(examsCount);
            //    }
            //    today = today.AddDays(1);
            //    tempQuery = exams.Where(i => i.StudyDateTime.Value.Year == today.Year && i.StudyDateTime.Value.Month == today.Month && i.StudyDateTime.Value.Day == today.Day);

            //    for (int i = 0; i < 24; i += 1)
            //    {
            //        ExamsDto.ExamsCount examsCount = new ExamsDto.ExamsCount
            //        {
            //            Time = i,
            //            Count = tempQuery.Where(exam => exam.FormattedTime.Value.Hours == i).Count()
            //        };
            //        examsCounts.Add(examsCount);
            //    }
            //}
            //else if (period.Equals("1-day-ago"))
            //{
            //    today = today.AddDays(-1);
            //    exams = exams.Where(i => i.StudyDateTime.Value.Year == today.Year && i.StudyDateTime.Value.Month == today.Month && i.StudyDateTime.Value.Day == today.Day);
            //    for (int i = 0; i < 24; i++)
            //    {
            //        ExamsDto.ExamsCount examsCount = new ExamsDto.ExamsCount
            //        {
            //            Time = i,
            //            Count = exams.Where(exam => exai.StudyDateTime.Hour == i).Count()
            //        };
            //        examsCounts.Add(examsCount);
            //    }
            //}
            //else if (period.Equals("today"))
            //{
            //    exams = exams.Where(i => i.StudyDateTime.Value.Year == today.Year && i.StudyDateTime.Value.Month == today.Month && i.StudyDateTime.Value.Day == today.Day);
            //    for (int i = 0; i < 24; i++)
            //    {
            //        ExamsDto.ExamsCount examsCount = new ExamsDto.ExamsCount
            //        {
            //            Time = i,
            //            Count = exams.Where(exam => exam.FormattedTime.Value.Hours == i).Count()
            //        };
            //        examsCounts.Add(examsCount);
            //    }
            //}
            return examsCounts;
        }

        public async Task<List<ExamsDto.ExamType>> GetExamTypesByPeriod(string? period)
        {
            DateTime today = DateTime.UtcNow.Date;

            Expression<Func<Study, bool>> filterStudy = i => !string.IsNullOrEmpty(i.StudyDescription);
            if (!string.IsNullOrEmpty(period))
            {
                if (period.Equals("today"))
                {
                    Expression<Func<Study, bool>> searchFilter = i => i.StudyDateTime.Year == today.Year && i.StudyDateTime.Month == today.Month && i.StudyDateTime.Day == today.Day;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-day-ago"))
                {
                    today = today.AddDays(-1);

                    Expression<Func<Study, bool>> searchFilter = i => i.StudyDateTime.Year == today.Year && i.StudyDateTime.Month == today.Month && i.StudyDateTime.Day == today.Day;

                    filterStudy = filterStudy.AndAlso(searchFilter);

                }
                else if (period.Equals("2-days-ago"))
                {
                    var dayAgo = today.AddDays(-2);

                    Expression<Func<Study, bool>> searchFilter = m => m.StudyDateTime < today && m.StudyDateTime >= dayAgo;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-week-ago"))
                {
                    DateTime lastWeekSunday = today.AddDays(-(int)today.DayOfWeek);
                    lastWeekSunday = lastWeekSunday.AddDays(1).AddTicks(-1);
                    DateTime lastWeekMonday = lastWeekSunday.AddDays(-6);
                    lastWeekMonday = lastWeekMonday.AddTicks(1);

                    Expression<Func<Study, bool>> searchFilter = e => e.StudyDateTime >= lastWeekMonday && e.StudyDateTime <= lastWeekSunday;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("1-month-ago"))
                {
                    today = today.AddMonths(-1);

                    Expression<Func<Study, bool>> searchFilter = e => e.StudyDateTime.Month == today.Month && e.StudyDateTime.Year == today.Year;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
                else if (period.Equals("this-year"))
                {
                    Expression<Func<Study, bool>> searchFilter = e => e.StudyDateTime.Year == today.Year;

                    filterStudy = filterStudy.AndAlso(searchFilter);
                }
            }


            var examTypes = await _studyRepository.QueryAsync(filterStudy);

            var types = examTypes.AsEnumerable().Select(x => x.StudyDescription);

            var result = types.GroupBy(x=>x).Select(x=> new ExamsDto.ExamType()
            {
                Count = x.Count(),
                Type = x.Key
            }).ToList();

         
            return result;
        }
    }
}
