using SWECVI.ApplicationCore.Entities;
using System.Text;
using static SWECVI.ApplicationCore.Enum;
using SWECVI.ApplicationCore.Solution;
using Microsoft.Extensions.Logging;
using SWECVI.ApplicationCore.Infrastructure;
using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.Business;

namespace SWECVI.ApplicationCore
{
    public class EchoReportGenerator
    {
        private ILogger _logger;
        public EchoReportGenerator(ILogger logger)
        {
            _logger = logger;
        }

        public string GetAssessmentText(List<ExamsDto.Measurement> parameters, StudyViewModel study, IList<ValveData> valveDataList, IEnumerable<AssessmentTextReference> assessmentTexts,List<ParameterReference> parameterReferences, string parameterResults, string findingText)
        {
            if (parameters == null || parameters.Count == 0)
            {
                _logger.LogWarning("PAR or its value is NULL or empty");
            }


            return CreateAssessment(parameters, study, valveDataList, assessmentTexts, parameterReferences, parameterResults, findingText);
        }

        public string CreateAssessment(List<ExamsDto.Measurement> parameters, StudyViewModel study, IList<ValveData> valveDataList, IEnumerable<AssessmentTextReference> assessmentTexts, List<ParameterReference> parameterReferences, string parameterResults, string findingText)
        {

            PythonScript pythonScript = new PythonScript();


            return pythonScript.RunGenerateAssessmentPythonScript(parameters, study, valveDataList, assessmentTexts, parameterReferences, parameterResults, findingText);
        }

        /// <summary>
        /// Get Assessment stress
        /// </summary>
        /// <returns></returns>
        public string GetAssessmentStress()
        {
            StringBuilder assessmentBuilder = new StringBuilder();
            AssessmentText? assessmentText;
            ApplicationState.Assessments.TryGetValue((int)Header.MainStresstest, out assessmentText);   // Get Header

            AssessmentText? assessmentContent;
            if (ApplicationState.Assessments.TryGetValue((int)Header.TextStressReply, out assessmentContent))
                assessmentBuilder.AppendLine();
            assessmentBuilder.AppendLine(Helpers.GetHeader(Header.DividerDash));           // --------------------------------
            assessmentBuilder.AppendLine(assessmentText?.Text ?? "");                     // STRESS TEST
            assessmentBuilder.AppendLine(Helpers.GetHeader(Header.DividerDash));           // --------------------------------

            // Add Detail
            assessmentBuilder.AppendLine(assessmentContent?.Text ?? "");

            return assessmentBuilder.ToString();
        }

        /// <summary>
        /// Calculation parameter index
        /// </summary>
        public void ParameterIndexCalculator(ref ParameterDictionary PAR, Exam Exam)
        {
            var BSA = Helpers.GetBSA(PAR);
            if (BSA == null || BSA == 0)
                return;

            List<string> keys = new List<string>(PAR.value.Keys);
            foreach (var key in keys)
            {
                var parameter = PAR.value[key];
                if (parameter != null && parameter != 0)
                {
                    var indexValue = parameter / BSA.Value;
                    var parameterNameIndex = string.Format("{0}Index", key);
                    if (Exam.Parameters.ContainsKey(parameterNameIndex))
                    {
                        var indexParameter = Exam.Parameters[parameterNameIndex];
                        if (indexParameter != null)
                        {
                            indexParameter.ResultValue = (float?)ParameterHelper.Round((decimal?)indexValue, indexParameter.DisplayDecimal);
                            PAR.AddOrUpdateParameter(indexParameter);
                        }
                    }
                }
            }
        }
    }
}
