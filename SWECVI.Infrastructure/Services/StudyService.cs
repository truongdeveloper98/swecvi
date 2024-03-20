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

namespace SWECVI.Infrastructure.Services
{
    public class StudyService : IStudyService
    {
        private readonly IParameterService _parameterService;
        private readonly IStudyRepository _studyRepository;
        private readonly IStudyFindingRepository _studyFindingRepository;
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
            studyVM.InstitutionName = study.InstitutionName;
            studyVM.StudyDateTime = study.StudyDateTime;
            studyVM.PatientViewModel = new PatientViewModel
            {
                PatientId = patient.PatientId,
                DOB = patient.DOB.ToString(),
                PatientName = patient.FirstName + " " + patient.LastName,
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

            List<string> parametersWithIndex = new List<string>() 
            {
                "LAESV",
                "LAAs",
                "LADs",
                "AOStjunct",
                "AOsinus",
                "AOascendens",
                "AVD",
                "LVEDV",
                "LVMass",
                "LVIDd",
                "LVIDs",
                "LVESV",
                "RAESV",
                "RVAd",
                "RVAs"
            };

            var parameterWithIndexs = new List<ExamsDto.Measurement>();

            foreach (var measurement in measurements)
            {
                if (parametersWithIndex.Contains(measurement.Name))
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
                                    paramTextTemp += param.Name + ": " + param.Value + "* " + param.Unit + " (" + param.Reference + ");";
                                }
                                else
                                {
                                    paramTextTemp += param.Name + ": " + param.Value + " " + param.Unit + " (" + param.Reference + ");";
                                }
                            }
                            else
                            {
                                paramTextTemp += param.Name + ": " + param.Value + " " + param.Unit + " (" + param.Reference + ");";
                            }
                        }
                        else
                        {
                            paramTextTemp += param.Name + ": " + param.Value + " " + param.Unit + ";";
                        }

                    }
                    parameterText += paramTextTemp.Substring(0, paramTextTemp.Length - 1);
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
                StressText = ""
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
    }
}
