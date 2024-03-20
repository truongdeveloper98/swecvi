using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Infrastructure
{
    public class ApplicationState
    {
        public static List<ParameterViewModel> ParameterList { get; set; }

        // Id, AssessmentText
        public static Dictionary<int, AssessmentText> Assessments { get; set; }

        public static int StandardReplyEndIndex { get; set; }

        public static bool ValveNormal { get; set; }

        public static int CountNo4DParameters { get; set; }

        public static Dictionary<string, string> WallSegmentDictionary { get; set; }

    }
}
