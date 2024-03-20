using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;
using SWECVI.ApplicationCore.Common;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Infrastructure;
using SWECVI.ApplicationCore.ViewModels;
using System.Text;

namespace SWECVI.ApplicationCore
{
    public class PythonScript
    {
        private ScriptEngine _engine;

        public PythonScript()
        {
            var engineOptions = new Dictionary<string, object> { ["LightweightScopes"] = true };
            _engine = Python.CreateEngine(engineOptions);
        }

        public string RunGenerateAssessmentPythonScript(List<ExamsDto.Measurement> parameters, StudyViewModel study, IList<ValveData> valveDataList, IEnumerable<AssessmentTextReference> assessmentTexts, List<ParameterReference> parameterReferences, string parameterResults, string findingText)
        {
            try
            {
                var root = PathExtension.DefaultRoot();
                var executeScriptsFolder = Path.Join(root, "ExecuteScrips".AsSpan());
                var currentScript = Path.Join(executeScriptsFolder, "generate_assessment.py");

                var path = "";
                if (File.Exists(currentScript))
                {
                    // current version progress
                    path = currentScript;
                }
                else
                {
                    executeScriptsFolder = root;
                    currentScript = Path.Join(executeScriptsFolder, "generate_assessment.py");
                    path = currentScript;
                }

                var libs = Path.Join(root, "Libs".AsSpan());

                 var logics = Path.Join(executeScriptsFolder, "Logic".AsSpan());

                //var newLogics = Path.Join(executeScriptsFolder, "Logic".AsSpan());

                var constants = Path.Join(executeScriptsFolder, "Constants".AsSpan());
                var dtos = Path.Join(executeScriptsFolder, "DTOs".AsSpan());
                var helpers = Path.Join(executeScriptsFolder, "Helpers".AsSpan());

                ScriptSource source = _engine.CreateScriptSourceFromFile(path);
                CompiledCode compiledCode = source.Compile();
                ScriptScope scope = _engine.CreateScope();

                var searchPath = new[] {
                    libs,
                    constants,
                    dtos,
                    helpers,
                    logics
                };

                _engine.SetSearchPaths(searchPath);

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string serializedParams = JsonConvert.SerializeObject(parameters, Formatting.Indented, jsonSerializerSettings);

                Dictionary<string, string> strings = new Dictionary<string, string>();

                strings.Add("serializedParameters", serializedParams);
                IList<PatientData> patientDataList = new List<PatientData>();

                StringBuilder generalData = new StringBuilder();
                generalData.Append($"Patient: {study.PatientViewModel.PatientName} {study.PatientViewModel.PatientId} \n");
                generalData.Append($"Exam date: {study.StudyDateTime.ToString("yyyy-MM-dd hh:mm")} \n");
                generalData.Append($"Exam type: {study.InstitutionName} \n");
                generalData.Append($"Department: Heart Clinic \n");
                generalData.Append($"Signed by: Doctor1  2023-01-01 13:54\n \n");

                string patientData = generalData.ToString();

                var serializedStudy = JsonConvert.SerializeObject(patientData, Formatting.Indented, jsonSerializerSettings);
                string _ValveDataList = JsonConvert.SerializeObject(valveDataList, Formatting.Indented, jsonSerializerSettings);
                string _assessmentTexts = JsonConvert.SerializeObject(assessmentTexts, Formatting.Indented, jsonSerializerSettings);
                string _PatientDataList = JsonConvert.SerializeObject(patientDataList, Formatting.Indented, jsonSerializerSettings);
                string _Assessments = JsonConvert.SerializeObject(ApplicationState.Assessments, Formatting.Indented, jsonSerializerSettings);
                string parameterList = JsonConvert.SerializeObject(parameterReferences, Formatting.Indented, jsonSerializerSettings);
                string parameterText = JsonConvert.SerializeObject(parameterResults, Formatting.Indented, jsonSerializerSettings);
                string finding = JsonConvert.SerializeObject(findingText, Formatting.Indented, jsonSerializerSettings);
                string _WallSegmentDictionary = JsonConvert.SerializeObject(ApplicationState.WallSegmentDictionary, Formatting.Indented, jsonSerializerSettings);
                strings.Add("serializedStudy", serializedStudy);
                strings.Add("ValveDataList", _ValveDataList);
                strings.Add("PatientDataList", _PatientDataList);
                strings.Add("Assessments", _Assessments);
                strings.Add("serializedAssessmentTexts", _assessmentTexts);
                strings.Add("findingText", finding);
                strings.Add("parameterList", parameterList);
                strings.Add("parameterResults", parameterText);
                strings.Add("WallSegmentDictionary", _WallSegmentDictionary);

                _engine.GetSysModule().SetVariable("strings", strings);

                compiledCode.Execute(scope);

                string result = scope.GetVariable("AssessmentText");
                return result;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }
    }
}