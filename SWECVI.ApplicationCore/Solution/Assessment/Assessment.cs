//using SWECVI.ApplicationCore.Business;
//using SWECVI.ApplicationCore.Entities;
//using SWECVI.ApplicationCore.Solution;
//using Microsoft.Extensions.Logging;
//using SWECVI.ApplicationCore.ViewModels.MirthConnect;
//using SWECVI.ApplicationCore.ViewModels;

//namespace SWECVI.ApplicationCore
//{
//    public partial class Assessment
//    {
//        private ILogger _logger;
//        public Assessment(ILogger logger)
//        {
//            _logger = logger;
//        }

//        public string CreateAssessment(List<ExamsDto.Measurement> parameters, StudyViewModel study, IList<ValveData> valveDataList, IEnumerable<AssessmentTextReference> assessmentTexts)
//        {

//            PythonScript pythonScript = new PythonScript();


//            return pythonScript.RunGenerateAssessmentPythonScript(parameters, study, valveDataList, assessmentTexts);
//        }

//        /// <summary>
//        /// Calculation parameter index
//        /// </summary>
//        public void ParameterIndexCalculator(ref ParameterDictionary PAR, Exam Exam)
//        {
//            var BSA = Helpers.GetBSA(PAR);
//            if (BSA == null || BSA == 0)
//                return;

//            List<string> keys = new List<string>(PAR.value.Keys);
//            foreach (var key in keys)
//            {
//                var parameter = PAR.value[key];
//                if (parameter != null && parameter != 0)
//                {
//                    var indexValue = parameter / BSA.Value;
//                    var parameterNameIndex = string.Format("{0}Index", key);
//                    if (Exam.Parameters.ContainsKey(parameterNameIndex))
//                    {
//                        var indexParameter = Exam.Parameters[parameterNameIndex];
//                        if (indexParameter != null)
//                        {
//                            indexParameter.ResultValue = (float?)ParameterHelper.Round((decimal?)indexValue, indexParameter.DisplayDecimal);
//                            PAR.AddOrUpdateParameter(indexParameter);
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
