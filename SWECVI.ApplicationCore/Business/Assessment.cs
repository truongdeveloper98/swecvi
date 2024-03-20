//using SWECVI.ApplicationCore.Entities;
//using System.Text;
//using static SWECVI.ApplicationCore.Enum;
//using SWECVI.ApplicationCore.Solution;
//using Microsoft.Extensions.Logging;
//using SWECVI.ApplicationCore.Infrastructure;
//using SWECVI.ApplicationCore.ViewModels.MirthConnect;
//using SWECVI.ApplicationCore.ViewModels;

//namespace SWECVI.ApplicationCore
//{
//    public partial class Assessment
//    {
//        public string GetAssessmentText(List<ExamsDto.Measurement> parameters, StudyViewModel study, IList<ValveData> valveDataList, IEnumerable<AssessmentTextReference> assessmentTexts)
//        {
//            if (parameters == null || parameters.Count == 0)
//            {
//                _logger.LogWarning("PAR or its value is NULL or empty");
//            }


//            return CreateAssessment(parameters, study, valveDataList, assessmentTexts);
//        }

//        /// <summary>
//        /// Get Assessment stress
//        /// </summary>
//        /// <returns></returns>
//        public string GetAssessmentStress()
//        {
//            StringBuilder assessmentBuilder = new StringBuilder();
//            AssessmentText? assessmentText;
//            ApplicationState.Assessments.TryGetValue((int)Header.MainStresstest, out assessmentText);   // Get Header

//            AssessmentText? assessmentContent;
//            if (ApplicationState.Assessments.TryGetValue((int)Header.TextStressReply, out assessmentContent))
//                assessmentBuilder.AppendLine();
//            assessmentBuilder.AppendLine(Helpers.GetHeader(Header.DividerDash));           // --------------------------------
//            assessmentBuilder.AppendLine(assessmentText?.Text ?? "");                     // STRESS TEST
//            assessmentBuilder.AppendLine(Helpers.GetHeader(Header.DividerDash));           // --------------------------------

//            // Add Detail
//            assessmentBuilder.AppendLine(assessmentContent?.Text ?? "");

//            return assessmentBuilder.ToString();
//        }
//    }
//}
