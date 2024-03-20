using FellowOakDicom;
using ClosedXML.Excel;
using ExtractDicomConsole;
using ExtractDicomConsole.ViewModels;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Events;
using Newtonsoft.Json;

try
{
    Logging.Configure();


    string test = "20180122";

    string year = test.Substring(6, 2);

    string path = Directory.GetCurrentDirectory();

    ConsoleAppUtilities.ClearData();

    var resultFiles = ConsoleAppUtilities.DirSearch(path);

    Log.Information($"File DCM : {JsonConvert.SerializeObject(resultFiles)}");

    string fileName = string.Empty;

    List<DicomSRMeaViewModel> dicomMeaSRs = new List<DicomSRMeaViewModel>();
    List<DicomSRViewModel> dicomSRs = new List<DicomSRViewModel>();

    List<DicomSRViewModel> dicomSRReuslts = new List<DicomSRViewModel>();
    List<DicomSRMeaViewModel> dicomSRMeaReuslts = new List<DicomSRMeaViewModel>();
    List<PartientViewModel> partientViewModels = new List<PartientViewModel>();

    foreach (var resultFile in resultFiles)
    {
        try
        {
            var file = await DicomFile.OpenAsync(resultFile);

            fileName = Path.GetFileName(resultFile);

            var items = file.Dataset.EnumerateGroup((ushort)24581); // 6005
            var measurementXMLExtracted = "";
            foreach (var item in items)
            {
                if (item.Tag.ToString().Contains("6005,1030") || item.Tag.ToString().Contains("6005,1010"))
                {
                    measurementXMLExtracted = file.Dataset.GetString(item.Tag); // get xml with tag 6005-1030
                }
            }

            var inputXmlFormat = new List<string>() { measurementXMLExtracted };


            foreach (var xml in inputXmlFormat)
            {
                if (xml.Contains("MeasurementExport"))
                {
                    var model = xml.FromXml<DicomSRViewModel>();
                    if (model != null)
                    {
                        dicomSRs.Add(model);
                    }
                }
                else
                {
                    var model = xml.FromXml<DicomSRMeaViewModel>();
                    if (model != null)
                    {
                        dicomMeaSRs.Add(model);
                    }
                }
            }

            Log.Information("\n");

            Log.Information("\n");

            Log.Information($"Data when parse data from xml to object : {JsonConvert.SerializeObject(dicomSRs)}");
        }
        catch (Exception ex)
        {
            Log.Error($"Error when convert file dcm to object {ex.Message}");
        }
    }

    List<PatientViewModel> dataToExport = new List<PatientViewModel>();


    if (dicomSRs.Count > 0)
    {
        foreach (var model in dicomSRs)
        {
            partientViewModels.Add(new PartientViewModel()
            {
                DicomSRViewModel = model,
                PartientId = model.Patient.PatientId,
                StudyTime = model.Patient.Study.StudyDateTime
            });
        }

        partientViewModels = partientViewModels.OrderBy(x => x.PartientId)
                                            .ThenByDescending(x => x.StudyTime)
                                            .ToList();

        var resultModel = partientViewModels.GroupBy(x => x.PartientId)
                                            .Select(x => x.Key)
                                            .ToList();

        foreach (var model in resultModel)
        {
            var findData = partientViewModels.Where(x => x.PartientId == model).FirstOrDefault();

            dicomSRReuslts.Add(findData.DicomSRViewModel);
        }

        Log.Information($"Data new for each partient : {JsonConvert.SerializeObject(dicomSRReuslts)}");

        foreach (var item in dicomSRReuslts)
        {
            foreach (var item2 in item.Patient.Study.Series.Parameter)
            {
                dataToExport.Add(new PatientViewModel()
                {
                    PatientId = item.Patient.PatientId,
                    Number_Of_Parameter = item.Patient.Study.Series.Parameter.Count() == null ? 0 : item.Patient.Study.Series.Parameter.Count(),
                    SeriesInstanceUID = item.Patient.Study.Series.SeriesInstanceUID == null ? string.Empty : item.Patient.Study.Series.SeriesInstanceUID,
                    SeriesDateTime = item.Patient.Study.Series.SeriesDateTime,
                    StudyDateTime = item.Patient.Study.StudyDateTime,
                    StudyInstanceUID = item.Patient.Study.StudyInstanceUID == null ? string.Empty : item.Patient.Study.StudyInstanceUID,
                    Weight = item.Patient.Study.Weight == null ? 0 : item.Patient.Study.Weight,
                    Height = item.Patient.Study.Height == null ? 0 : item.Patient.Study.Height,
                    BSA = item.Patient.Study.BSA == null ? 0 : item.Patient.Study.BSA.Value,
                    InstitutionName = item.Patient.Study.Series.InstitutionName == null ? string.Empty : item.Patient.Study.Series.InstitutionName,
                    MeasureId = item2.MeasureId == null ? string.Empty : item2.MeasureId,
                    ParameterId = item2.ParameterId == null ? string.Empty : item2.ParameterId,
                    ParameterName = item2.ParameterName == null ? string.Empty : item2.ParameterName,
                    Measurement = ConsoleAppUtilities.GetMeasurementType(Convert.ToInt32(item2.ResultNo)),
                    AverageType = item2.AverageType == null ? string.Empty : item2.AverageType,
                    ResultValue = item2.ResultValue == null ? 0 : item2.ResultValue.Value,
                    DisplayUnit = item2.DisplayUnit == null ? string.Empty : item2.DisplayUnit,
                    ScanMode = item2.ScanMode == null ? string.Empty : item2.ScanMode,
                    StudyId = item2.StudyId == null ? string.Empty : item2.StudyId,
                    ParameterType = item2.ParameterType == null ? string.Empty : item2.ParameterType,
                    DisplayValue = item2.DisplayValue == null ? string.Empty : item2.DisplayValue,
                    FirstName = item.Patient.FirstName == null ? string.Empty : item.Patient.FirstName,
                    LastName = item.Patient.LastName == null ? string.Empty : item.Patient.LastName,
                    OtherPatientId = item.Patient.OtherPatientId.PatientId == null ? string.Empty : item.Patient.OtherPatientId.PatientId,
                });
            }
        }
    }

    if (dicomMeaSRs.Count > 0)
    {
        foreach (var model in dicomMeaSRs)
        {
            partientViewModels.Add(new PartientViewModel()
            {
                DicomSRMeaViewModel = model,
                PartientId = model.Patient.patientId,
                StudyTimeMea = model.Patient.Exam.StudyTime
            });
        }

        partientViewModels = partientViewModels.OrderBy(x => x.PartientId)
                                            .ThenByDescending(x => x.StudyTimeMea)
                                            .ToList();

        var resultModel = partientViewModels.GroupBy(x => x.PartientId)
                                            .Select(x => x.Key)
                                            .ToList();

        foreach (var model in resultModel)
        {
            var findData = partientViewModels.Where(x => x.PartientId == model).FirstOrDefault();

            if (findData.DicomSRMeaViewModel != null)
            {
                dicomSRMeaReuslts.Add(findData.DicomSRMeaViewModel);
            }
        }

        Log.Information($"Data new for each partient : {JsonConvert.SerializeObject(dicomSRMeaReuslts)}");

        foreach (var item in dicomSRMeaReuslts)
        {
            foreach (var item2 in item.Patient.Exam.MEASUREMENT)
            {
                dataToExport.Add(new PatientViewModel()
                {
                    PatientId = item.Patient.patientId,
                    Number_Of_Parameter = item.Patient.Exam.MEASUREMENT.Count() == null ? 0 : item.Patient.Exam.MEASUREMENT.Count(),
                    SeriesInstanceUID = item.Patient.Exam.SeriesInstanceUID == null ? string.Empty : item.Patient.Exam.SeriesInstanceUID,
                    //SeriesDateTime = item.Patient.Exam.SeriesDate,
                    //StudyDateTime = item.Patient.Study.StudyDateTime,
                    StudyInstanceUID = item.Patient.Exam.StudyInstanceUID == null ? string.Empty : item.Patient.Exam.StudyInstanceUID,
                    Weight = item.Patient.Exam.weight == null ? 0 : item.Patient.Exam.weight,
                    Height = item.Patient.Exam.height == null ? 0 : item.Patient.Exam.height,
                    BSA = 0,
                    //InstitutionName = item.Patient.Study.Series.InstitutionName == null ? string.Empty : item.Patient.Study.Series.InstitutionName,
                    //MeasureId = item2.m == null ? string.Empty : item2.MeasureId,
                    ParameterId = item2.parameterId == null ? string.Empty : item2.parameterId,
                    ParameterName = item2.parameterName == null ? string.Empty : item2.parameterName,
                    Measurement = ConsoleAppUtilities.GetMeasurementType(Convert.ToInt32(item2.resultNo)),
                    AverageType = item2.avgType == null ? string.Empty : item2.avgType,
                    ResultValue = item2.valueDouble == null ? 0 : item2.valueDouble,
                    DisplayUnit = item2.displayUnit == null ? string.Empty : item2.displayUnit,
                    ScanMode = item2.mode == null ? string.Empty : item2.mode,
                    StudyId = item2.study == null ? string.Empty : item2.study,
                    ParameterType = item2.parameterType == null ? string.Empty : item2.parameterType,
                    DisplayValue = item2.valueString == null ? string.Empty : item2.valueString,
                    FirstName = item.Patient.firstName == null ? string.Empty : item.Patient.firstName,
                    LastName = item.Patient.lastName == null ? string.Empty : item.Patient.lastName,
                    OtherPatientId = item.Patient.OtherId == null ? string.Empty : item.Patient.OtherId,
                });
            }
        }
    }



    StringExtension.ExportCsv(dataToExport, "dcmexport");

    Log.Information("\n");

    Log.Information("Export to file csv is successed!!!");


}
catch (Exception ex)
{
    Log.Error($"Error when write result in to file {ex.Message.ToString()}");
}