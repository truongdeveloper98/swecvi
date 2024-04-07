using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Utilities;
using SWECVI.ApplicationCore.ViewModels;
using System.Linq.Expressions;
using SWECVI.ApplicationCore.ViewModels.MirthConnect;
using SWECVI.ApplicationCore.Business;
using SWECVI.ApplicationCore.Solution;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Nest;

namespace SWECVI.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IParameterRepository _parameterRepository;
        private readonly IStudyRepository _studyRepository;
        private readonly IManufacturerDicomParametersRepository _manufacturerDicomParametersRepository;
        private readonly IDicomTagRepository _dicomTagRepository;

        public PatientService(IPatientRepository patientRepository,
                IParameterRepository parameterRepository,
                IStudyRepository studyRepository,
                IDicomTagRepository dicomTagRepository,
                IManufacturerDicomParametersRepository manufacturerDicomParametersRepository
            )
        {
            _patientRepository = patientRepository;
            _parameterRepository = parameterRepository;
            _studyRepository = studyRepository;
            _manufacturerDicomParametersRepository = manufacturerDicomParametersRepository;
            _dicomTagRepository = dicomTagRepository;
        }

        public async Task<bool> Create(DicomResultMirthViewModel model, int hospitalId, List<ManufacturerDicomParameters> manufacturerDicomParameters, List<DicomTags> dicomTags, List<ParameterVaueViewModel> Parameters)
        {
            var existsPatient = _patientRepository.FirstOrDefault(x => x.PatientId == model.PatientId);

            if (existsPatient is null)
            {

                var partient = new Patient()
                {
                    FirstName = model.PatientName,
                    PatientId = model.PatientId,
                    Sex = model.PatientSex,
                    DOB = Helpers.FormatStringToDate(model.PatientDate),
                    LastName = model.PatientName
                };

                await _patientRepository.Add(partient);

                existsPatient = partient;
            }


            var existsStudy = _studyRepository.FirstOrDefault(x => x.StudyID == model.StudyId && x.StudyInstanceUID == model.StudyInstanceUID && x.PatientId == existsPatient.Id);

            if (existsStudy is null)
            {
                string bsa = string.IsNullOrEmpty(model.BSA) ? "0" : model.BSA;
                var study = new Study()
                {
                    AccessionNumber = model.AccessionNumber,
                    BodySurfaceArea = Convert.ToSingle(bsa),
                    ContentDateTime = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    DiastoilccBloodPressure = string.Empty,
                    Height = Convert.ToSingle(string.IsNullOrEmpty(model.StudyHeight) ? "0" : model.StudyHeight),
                    Weight = Convert.ToSingle(string.IsNullOrEmpty(model.StudyWeight) ? "0" : model.StudyWeight),
                    InstitutionName = model.InstitutionName,
                    StudyDescription = string.IsNullOrEmpty(model.StudyDescription) ? "TTE" : model.StudyDescription,
                    StudyInstanceUID = model.StudyInstanceUID,
                    StudyID = model.StudyId,
                    StudyDateTime = Helpers.FormatStringToDate(model.StudyDate, model.StudyTime),
                    SOPClassUID = model.SOPClassUID,
                    SOPInstanceUID = model.SOPInstanceUID,
                    InstitutionalDepartmentName = model.InstitutionalDepartmentName,
                    PatientId = existsPatient.Id,
                    ModalitiesInStudy = string.Empty,
                    SystolicBloodPressure = string.Empty,
                    SoftwareVersion = "1.0",
                    Manufacture = model.Manufacture,
                    ManufactureModel = model.ManufactureName,
                    Age = ExamHelper.Age(existsPatient.DOB, string.Empty, Helpers.FormatStringToDate(model.StudyDate, model.StudyTime)) ?? 0
                };

                await _studyRepository.Add(study);

                existsStudy = study;
            }

            foreach (var item in model.Parameters)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {

                    var parameter = new StudyParameter()
                    {
                        ResultValue = Convert.ToSingle(item.Value),
                        StudyIndex = existsStudy.Id,
                        ValueUnit = item.ValueUnitCode,
                        ParameterId = item.NameCode
                    };

                    if (item.NameCode.Split("|")[1].Trim() == "18026-5")
                    {

                    }
                    var cv = item.NameCode.Split("|")[1].Trim();
                    var csd = item.NameCode.Split("|")[0].Trim();

                    var findings = manufacturerDicomParameters.Where(x => x.MeasurementCV == cv && x.MeasurementCSD == csd);

                    if (findings != null)
                    {
                        //parameter.ParameterId = finding.ParameterId;
                        var imageId = 0;
                        var findingSiteId = 0;
                        var imageViewId = 0;
                        var cardiacCycleId = 0;
                        var respiratoryCycleId = 0;
                        var measurementMethodId = 0;
                        var derivationId = 0;
                        var selectionStatusId = 0;
                        var directionOfFlowId = 0;

                        if (item.ParameterDetails.Count > 0)
                        {
                            foreach (var itemDetail in item.ParameterDetails)
                            {
                                if (itemDetail.Name.Contains("Image Mode"))
                                {
                                    var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
                                    var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
                                    imageId = dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? 0 : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

                                }
                                else if (itemDetail.Name.Contains("Finding Site"))
                                {
                                    var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
                                    var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
                                    findingSiteId = dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? 0 : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

                                }
                                else if (itemDetail.Name.Contains("Image View"))
                                {
                                    var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
                                    var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
                                    imageViewId = dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? 0 : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

                                }
                                else if (itemDetail.Name.Contains("Cardiac Cycle Point"))
                                {
                                    var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
                                    var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
                                    cardiacCycleId = dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? 0 : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

                                }
                                else if (itemDetail.Name.Contains("Respiratory Cycle Point"))
                                {
                                    var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
                                    var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
                                    respiratoryCycleId = dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? 0 : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;
                                }
                                else if (itemDetail.Name.Contains("Measurement Method"))
                                {
                                    var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
                                    var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
                                    measurementMethodId = dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? 0 : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

                                }
                                else if (itemDetail.Name.Contains("Derivation"))
                                {
                                    var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
                                    var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
                                    derivationId = dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? 0 : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

                                }
                                else if (itemDetail.Name.Contains("Direction of Flow"))
                                {
                                    var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
                                    var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
                                    directionOfFlowId = dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? 0 : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;
                                }
                            }

                            if (imageId > 0)
                            {
                                findings = findings.Where(x => x.ImageMode == imageId);
                            }

                            if (imageViewId > 0)
                            {
                                findings = findings.Where(x => x.ImageView == imageViewId);
                            }

                            if (findingSiteId > 0)
                            {
                                findings = findings.Where(x => x.FindingSite == findingSiteId);
                            }

                            if (cardiacCycleId > 0)
                            {
                                findings = findings.Where(x => x.CardiacPhase == cardiacCycleId);
                            }

                            if (measurementMethodId > 0)
                            {
                                findings = findings.Where(x => x.MeasurementMethod == measurementMethodId);
                            }

                            if (directionOfFlowId > 0)
                            {
                                findings = findings.Where(x => x.FlowDirection == directionOfFlowId);
                            }
                        }

                        if (findings.Count() > 0)
                        {
                            var finding = findings.FirstOrDefault();
                            parameter.ParameterId = finding.ParameterId;
                            parameter.ImageMode = finding.ImageMode == null ? 0 : finding.ImageMode.Value;
                            parameter.ImageView = finding.ImageView == null ? 0 : finding.ImageView.Value;
                            parameter.FindingSite = finding.FindingSite == null ? 0 : finding.FindingSite.Value;
                            parameter.MeasurementMethod = finding.MeasurementMethod == null ? 0 : finding.MeasurementMethod.Value;
                            parameter.DirectionOfFlow = finding.FlowDirection == null ? 0 : finding.FlowDirection.Value;
                            parameter.Derivation = derivationId;
                            parameter.CardiacCyclePoint = finding.CardiacPhase == null ? 0 : finding.CardiacPhase.Value;

                            await _parameterRepository.Add(parameter);
                        }
                    }
                }
            }

            return true;
        }

        //public async Task<bool> Create(DicomResultMirthViewModel model, int hospitalId, List<ManufacturerDicomParameters> manufacturerDicomParameters, List<DicomTags> dicomTags, List<ParameterVaueViewModel> parameterResultViewModels)
        //{
        //    var existsPatient = _patientRepository.FirstOrDefault(x => x.PatientId == model.PatientId);

        //    if (existsPatient is null)
        //    {

        //        var partient = new Patient()
        //        {
        //            FirstName = model.PatientName,
        //            PatientId = model.PatientId,
        //            Sex = model.PatientSex,
        //            DOB = Helpers.FormatStringToDate(model.PatientDate),
        //            LastName = model.PatientName
        //        };

        //        await _patientRepository.Add(partient);

        //        existsPatient = partient;
        //    }


        //    var existsStudy = _studyRepository.FirstOrDefault(x => x.StudyID == model.StudyId && x.StudyInstanceUID == model.StudyInstanceUID && x.PatientId == existsPatient.Id);

        //    if (existsStudy is null)
        //    {
        //        string bsa = string.IsNullOrEmpty(model.BSA) ? "0" : model.BSA;
        //        var study = new Study()
        //        {
        //            AccessionNumber = model.AccessionNumber,
        //            BodySurfaceArea = Convert.ToSingle(bsa),
        //            ContentDateTime = DateTime.Now,
        //            CreatedAt = DateTime.Now,
        //            DiastoilccBloodPressure = string.Empty,
        //            Height = Convert.ToSingle(string.IsNullOrEmpty(model.StudyHeight) ? "0" : model.StudyHeight),
        //            Weight = Convert.ToSingle(string.IsNullOrEmpty(model.StudyWeight) ? "0" : model.StudyWeight),
        //            InstitutionName = model.InstitutionName,
        //            StudyDescription = model.StudyDescription,
        //            StudyInstanceUID = model.StudyInstanceUID,
        //            StudyID = model.StudyId,
        //            StudyDateTime = Helpers.FormatStringToDate(model.StudyDate, model.StudyTime),
        //            SOPClassUID = model.SOPClassUID,
        //            SOPInstanceUID = model.SOPInstanceUID,
        //            InstitutionalDepartmentName = model.InstitutionalDepartmentName,
        //            PatientId = existsPatient.Id,
        //            ModalitiesInStudy = string.Empty,
        //            SystolicBloodPressure = string.Empty,
        //            SoftwareVersion = "1.0",
        //            Manufacture = model.Manufacture,
        //            ManufactureModel = model.ManufactureName,
        //            Age = ExamHelper.Age(existsPatient.DOB, string.Empty, Helpers.FormatStringToDate(model.StudyDate, model.StudyTime)) ?? 0
        //        };

        //        await _studyRepository.Add(study);

        //        existsStudy = study;
        //    }

        //    string parameterId = string.Empty;

        //    foreach (var item in model.Parameters)
        //    {
        //        if (!string.IsNullOrEmpty(item.Name))
        //        {
        //            var valueInMatch = item.Value.Split('.');

        //            var valueFromDicom = valueInMatch[0] + valueInMatch[1].Substring(0, 2);

        //            var itemFind = new ParameterVaueViewModel();

        //            foreach (var par in parameterResultViewModels)
        //            {
        //                if (!string.IsNullOrEmpty(par.ResultValue))
        //                {
        //                    var valueInMatchTag = par.ResultValue.Split('.');

        //                    var valueFromDicomTag = valueInMatchTag[0] + valueInMatchTag[1].Substring(0, 2);


        //                    if (valueFromDicomTag == valueFromDicom)
        //                    {
        //                        itemFind = par;
        //                        break;
        //                    }
        //                }

        //            }

        //            if (itemFind != null)
        //            {
        //                if (!string.IsNullOrEmpty(itemFind.ParameterId))
        //                {
        //                    parameterId = itemFind.ParameterId;


        //                    var parameter = new StudyParameter()
        //                    {
        //                        ResultValue = Convert.ToSingle(item.Value),
        //                        StudyIndex = existsStudy.Id,
        //                        ValueUnit = item.ValueUnitCode,
        //                        ParameterId = parameterId,
        //                    };

        //                    var cv = item.NameCode.Split("|")[1].Trim();
        //                    var csd = item.NameCode.Split("|")[0].Trim();

        //                    var manufacturerDicomParameter = await _manufacturerDicomParametersRepository.Get(x => x.ParameterId == parameterId);


        //                    int? imageId = null;
        //                    int? findingSiteId = null;
        //                    int? imageViewId = null;
        //                    int? cardiacCycleId = null;
        //                    int? respiratoryCycleId = null;
        //                    int? measurementMethodId = null;
        //                    int? derivationId = null;
        //                    int? selectionStatusId = null;
        //                    int? directionOfFlowId = null;

        //                    if (item.ParameterDetails.Count > 0)
        //                    {
        //                        foreach (var itemDetail in item.ParameterDetails)
        //                        {
        //                            if (itemDetail.Name.Contains("Image Mode"))
        //                            {
        //                                var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
        //                                var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
        //                                imageId = _dicomTagRepository.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? null : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

        //                            }
        //                            else if (itemDetail.Name.Contains("Finding Site"))
        //                            {
        //                                var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
        //                                var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
        //                                findingSiteId = _dicomTagRepository.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? null : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

        //                            }
        //                            else if (itemDetail.Name.Contains("Image View"))
        //                            {
        //                                var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
        //                                var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
        //                                imageViewId = _dicomTagRepository.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? null : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

        //                            }
        //                            else if (itemDetail.Name.Contains("Cardiac Cycle Point"))
        //                            {
        //                                var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
        //                                var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
        //                                cardiacCycleId = _dicomTagRepository.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? null : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

        //                            }
        //                            else if (itemDetail.Name.Contains("Respiratory Cycle Point"))
        //                            {
        //                                var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
        //                                var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
        //                                respiratoryCycleId = _dicomTagRepository.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? null : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;
        //                            }
        //                            else if (itemDetail.Name.Contains("Measurement Method"))
        //                            {
        //                                var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
        //                                var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
        //                                measurementMethodId = _dicomTagRepository.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? null : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

        //                            }
        //                            else if (itemDetail.Name.Contains("Derivation"))
        //                            {
        //                                var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
        //                                var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
        //                                derivationId = _dicomTagRepository.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? null : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;

        //                            }
        //                            else if (itemDetail.Name.Contains("Direction of Flow"))
        //                            {
        //                                var cvDetail = itemDetail.ValueCode.Split("|")[1].Trim();
        //                                var csdDetail = itemDetail.ValueCode.Split("|")[0].Trim();
        //                                directionOfFlowId = _dicomTagRepository.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail) == null ? null : dicomTags.FirstOrDefault(x => x.CV == cvDetail && x.CSD == csdDetail).Id;
        //                            }
        //                        }

        //                        if (manufacturerDicomParameter != null)
        //                        {
        //                            manufacturerDicomParameter.ImageMode = imageId;
        //                            manufacturerDicomParameter.ImageView = imageViewId;
        //                            manufacturerDicomParameter.FindingSite = findingSiteId;
        //                            manufacturerDicomParameter.MeasurementMethod = measurementMethodId;
        //                            manufacturerDicomParameter.FlowDirection = directionOfFlowId;
        //                            manufacturerDicomParameter.CardiacPhase = cardiacCycleId;
        //                            manufacturerDicomParameter.UpdatedAt = DateTime.Now;

        //                            await _manufacturerDicomParametersRepository.Update(manufacturerDicomParameter);
        //                        }
        //                        else
        //                        {
        //                            var manufacturerDicom = new ManufacturerDicomParameters()
        //                            {
        //                                ProviderId = "GE",
        //                                ProviderParameterId = "GE" + parameterId,
        //                                ParameterId = parameterId,
        //                                ProviderParameterShortName = itemFind.ParameterName,
        //                                ParameterNameLogic = itemFind.ParameterName,
        //                                MeasurementCSD = csd,
        //                                MeasurementCV = cv,
        //                                MeasurementCM = item.Name,
        //                                FindingSite = findingSiteId,
        //                                ImageMode = imageId,
        //                                ImageView = imageViewId,
        //                                CardiacPhase = cardiacCycleId,
        //                                MeasurementMethod = measurementMethodId,
        //                                CreatedAt = DateTime.Now
        //                            };

        //                            await _manufacturerDicomParametersRepository.Add(manufacturerDicom);
        //                        }

        //                        //parameter.ImageMode = (int)imageId;
        //                        //parameter.ImageView = (int)imageViewId;
        //                        //parameter.FindingSite = (int)findingSiteId;
        //                        //parameter.MeasurementMethod = (int)measurementMethodId;
        //                        //parameter.DirectionOfFlow = (int)directionOfFlowId;
        //                        //parameter.Derivation = (int)derivationId;
        //                        //parameter.CardiacCyclePoint = (int)cardiacCycleId;

        //                        //await _parameterRepository.Add(parameter);
        //                    }
        //                }
        //                else
        //                {

        //                }
        //            }


        //        }
        //    }

        //    return true;
        //}

        public async Task<bool> Delete(int id)
        {
            var patient = await _patientRepository.Get(x => x.Id == id);

            if(patient is null)
            {
                throw new Exception("Patent not found");
            }

            await _patientRepository.Delete(patient);

            return true;
        }

        public async Task<MemoryStream> ExportData(DateTime? startDate, DateTime? endDate, int? partientId, int? studyId)
        {
            Expression<Func<StudyParameter, bool>> filterStudy = i => !i.IsDeleted;

            if (startDate.HasValue)
            {
                Expression<Func<StudyParameter, bool>> searchFilter = i => i.HospitalStudy.StudyDateTime >=  startDate;

                filterStudy = filterStudy.AndAlso(searchFilter);
            }

            if (endDate.HasValue)
            {
                Expression<Func<StudyParameter, bool>> searchFilter = i => i.HospitalStudy.StudyDateTime <= endDate;

                filterStudy = filterStudy.AndAlso(searchFilter);
            }

            if (partientId.HasValue)
            {
                Expression<Func<StudyParameter, bool>> searchFilter = i => i.HospitalStudy.Patient.Id == partientId.Value;

                filterStudy = filterStudy.AndAlso(searchFilter);
            }

            if (studyId.HasValue)
            {
                Expression<Func<StudyParameter, bool>> searchFilter = i => i.HospitalStudy.Id == studyId.Value;

                filterStudy = filterStudy.AndAlso(searchFilter);
            }

            Expression<Func<StudyParameter, ExportStudyParameterViewModel>> resultData = i => new ExportStudyParameterViewModel
            {
                PatientId = i.HospitalStudy.Patient.PatientId,
                PatientName = i.HospitalStudy.Patient.FirstName,
                DOB = i.HospitalStudy.Patient.DOB,
                Sex = i .HospitalStudy.Patient.Sex,
                AccessionNumber = i.HospitalStudy.AccessionNumber,
                InstitutionName = i.HospitalStudy.InstitutionName,
                SOPClassUID = i.HospitalStudy.SOPClassUID,
                SOPInstanceUID = i.HospitalStudy.SOPInstanceUID,
                StudyDescription = i.HospitalStudy.StudyDescription,
                ParameterNameLogic = i.ParameterId,
                ResultValue = i.ResultValue,
                ValueUnit = i.ValueUnit
                
            };

            var studyByDateTime = await _parameterRepository.QueryAndSelectAsync(resultData, filterStudy, null, "HospitalStudy");

            string customExcelSavingPath = Directory.GetCurrentDirectory() + "\\" + "ExportData.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Parameters");

                int rowIndex = 1;
                int stt = 1;
                //export data từng project
                foreach (var result in studyByDateTime)
                {
                    sheet.Cells[rowIndex, 1].Value = stt;
                    sheet.Cells[rowIndex, 2].Value = result.PatientId;
                    sheet.Cells[rowIndex, 3].Value = result.PatientName;
                    sheet.Cells[rowIndex, 4].Value = result.DOB;
                    sheet.Cells[rowIndex, 5].Value = result.Sex;
                    sheet.Cells[rowIndex, 6].Value = result.StudyId;
                    sheet.Cells[rowIndex, 7].Value = result.AccessionNumber;
                    sheet.Cells[rowIndex, 8].Value = result.SOPClassUID;
                    sheet.Cells[rowIndex, 9].Value = result.SOPInstanceUID;
                    sheet.Cells[rowIndex, 10].Value = result.InstitutionName;
                    sheet.Cells[rowIndex, 11].Value = result.StudyDescription;
                    sheet.Cells[rowIndex, 12].Value = result.ParameterNameLogic;
                    sheet.Cells[rowIndex, 13].Value = result.ResultValue;
                    sheet.Cells[rowIndex, 14].Value = result.ValueUnit;
                    rowIndex++;
                    
                }
                
                package.Save();

                
            }

            stream.Position = 0;

            return stream;
        }

        public Task<bool> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PatientStudyViewModel>> GetExamsByPatientId(int patientId, string? period, int hospitalId)
        {
            Expression<Func<Study, bool>> filter = i => i.PatientId == patientId;

            if (!string.IsNullOrEmpty(period))
            {
                DateTime today = DateTime.UtcNow.Date;

                Expression<Func<Study, bool>> searchFilter = i => i.StudyDateTime != DateTime.MinValue;

                filter = filter.AndAlso(searchFilter);

                if (period.Equals("today"))
                {
                    Expression<Func<Study, bool>> searchFilterToday = i => i.StudyDateTime == today;

                    filter = filter.AndAlso(searchFilterToday);
                }
                else if (period.Equals("1-day-ago"))
                {
                    today = today.AddDays(-1);

                    Expression<Func<Study, bool>> searchFilterToday = i => i.StudyDateTime == today;

                    filter = filter.AndAlso(searchFilterToday);
                }
                else if (period.Equals("2-days-ago"))
                {
                    var startDay = today.AddDays(-2);

                    Expression<Func<Study, bool>> searchFilterToday = i => i.StudyDateTime == today;

                    filter = filter.AndAlso(searchFilterToday);
                }
                else if (period.Equals("1-week-ago"))
                {
                    DateTime lastWeekSunday = today.AddDays(-(int)today.DayOfWeek);
                    lastWeekSunday = lastWeekSunday.AddDays(1).AddTicks(-1);
                    DateTime lastWeekMonday = lastWeekSunday.AddDays(-6);
                    lastWeekMonday = lastWeekMonday.AddTicks(1);

                    Expression<Func<Study, bool>> searchFilterToday = e => e.StudyDateTime >= lastWeekMonday && e.StudyDateTime <= lastWeekSunday;

                    filter = filter.AndAlso(searchFilterToday);

                }
                else if (period.Equals("1-month-ago"))
                {
                    var month = today.Month - 1;

                    Expression<Func<Study, bool>> searchFilterToday = e => e.StudyDateTime.Month == month;

                    filter = filter.AndAlso(searchFilterToday);
                }
                else if (period.Equals("this-year"))
                {
                    Expression<Func<Study, bool>> searchFilterToday = e => e.StudyDateTime.Year == today.Year;

                    filter = filter.AndAlso(searchFilterToday);
                }
            }


            Expression<Func<Study, PatientStudyViewModel>> selectorExpression = i => new PatientStudyViewModel
            {
                Id = i.Id,
                Date = i.StudyDateTime.Date.ToString(),
                Time = i.StudyDateTime.TimeOfDay.ToString(),
                Height = i.Height,
                Weight = i.Weight,
                StudyType = i.StudyDescription,
                AccessionNumber = i.AccessionNumber
            };

            var exams = await _studyRepository.QueryAndSelectAsync(selector: selectorExpression, filter: filter, null, "Patient");


            return (List<PatientStudyViewModel>)exams;
        }

        public async Task<PagedResponseDto<PatientViewModel>> GetPatients(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch, string? dicomPatientId, string? period, int hospitalId, string role)
        {
            Expression<Func<Patient, bool>> filter = i => !i.IsDeleted;

            Expression<Func<Patient, PatientViewModel>> selectorExpression = i => new PatientViewModel
            {
                PatientId = i.Id.ToString(),
                PatientName = i.LastName,
                Sex = i.Sex,
                DOB = i.DOB.ToString(),
                NoOfExam = i.Studies.Count()
            };

            if (string.IsNullOrEmpty(period))
            {
                if (!string.IsNullOrEmpty(dicomPatientId))
                {
                    Expression<Func<Patient, bool>> searchFilter = m => m.PatientId == dicomPatientId;

                    filter = filter.AndAlso(searchFilter);
                }
                if (!string.IsNullOrEmpty(textSearch))
                {
                    Expression<Func<Patient, bool>> searchFilter = m =>
                        m.LastName.Contains(textSearch) ||
                        m.Sex.Contains(textSearch);

                    filter = filter.AndAlso(searchFilter);
                }
            }

            var totalItems = await _patientRepository.Count(filter);

            var items = await _patientRepository.QueryAndSelectAsync(selector: selectorExpression,
                filter: filter,
                orderBy: m => PredicateBuilder.ApplyOrder(m, sortColumnName, "ASC"),
                "", pageSize, currentPage
                );

            return new PagedResponseDto<PatientViewModel>()
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = (List<PatientViewModel>)items
            };
        }


        //DateTime today = DateTime.UtcNow.Date;


        //Expression<Func<Exam, bool>> filterExam = i => i.FormattedDate != null && i.FormattedDate != DateTime.MinValue;

        //if (period.Equals("today"))
        //{
        //    Expression<Func<Exam, bool>> searchFilterToday = i => i.FormattedDate == today;

        //    filterExam = PredicateBuilder.AndAlso(filterExam, searchFilterToday);
        //}
        //else if (period.Equals("1-day-ago"))
        //{
        //    today = today.AddDays(-1);

        //    Expression<Func<Exam, bool>> searchFilterToday = i => i.FormattedDate == today;

        //    filterExam = PredicateBuilder.AndAlso(filterExam, searchFilterToday);
        //}
        //else if (period.Equals("2-days-ago"))
        //{
        //    var startDay = today.AddDays(-2);

        //    Expression<Func<Exam, bool>> searchFilterToday = i => i.FormattedDate == today;

        //    filterExam = PredicateBuilder.AndAlso(filterExam, searchFilterToday);
        //}
        //else if (period.Equals("1-week-ago"))
        //{
        //    DateTime lastWeekSunday = today.AddDays(-(int)today.DayOfWeek);
        //    lastWeekSunday = lastWeekSunday.AddDays(1).AddTicks(-1);
        //    DateTime lastWeekMonday = lastWeekSunday.AddDays(-6);
        //    lastWeekMonday = lastWeekMonday.AddTicks(1);

        //    Expression<Func<Exam, bool>> searchFilterToday = e => e.FormattedDate >= lastWeekMonday && e.FormattedDate <= lastWeekSunday;

        //    filterExam = PredicateBuilder.AndAlso(filterExam, searchFilterToday);

        //}
        //else if (period.Equals("1-month-ago"))
        //{
        //    var month = today.Month - 1;

        //    Expression<Func<Exam, bool>> searchFilterToday = e => e.FormattedDate.Value.Month == month;

        //    filterExam = PredicateBuilder.AndAlso(filterExam, searchFilterToday);
        //}
        //else if (period.Equals("this-year"))
        //{
        //    Expression<Func<Exam, bool>> searchFilterToday = e => e.FormattedDate.Value.Year == today.Year;

        //    filterExam = PredicateBuilder.AndAlso(filterExam, searchFilterToday);
        //}

        //var _patients = await _patientRepository.QueryAndSelectAsync(selector: selectorExpression, filter: filter, null, "",pageSize,currentPage);
        //var _totalItems = _patients.Count();
        //var _items = _patients.Skip(currentPage * pageSize).Take(pageSize).ToList();
        //return new PagedResponseDto<PatientDto>()
        //{
        //    Page = currentPage,
        //    Limit = pageSize,
        //    TotalItems = _totalItems,
        //    TotalPages = (int)Math.Ceiling(_totalItems / (double)pageSize),
        //    Items = _items
        //};
    }
}
