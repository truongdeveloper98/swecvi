namespace SWECVI.ApplicationCore.Constants
{
    public class CacheKeys
    {
        private static readonly string _prefix = "SWECVI.";

        public static string Parameters
        {
            get
            {
                return _prefix + "Parameters";
            }
        }

        public static string References
        {
            get
            {
                return _prefix + "References";
            }
        }

        public static string AssessmentText
        {
            get
            {
                return _prefix + "AssessmentText";
            }
        }

        public static string AssessmentTextReference
        {
            get
            {
                return _prefix + "AssessmentTextReference";
            }
        }

        public static string SuperAdminDicomTag
        {
            get
            {
                return _prefix + "SuperAdminDicomTag";
            }
        } 
        
        public static string ManufacturerDicomParameter
        {
            get
            {
                return _prefix + "ManufacturerDicomParameter";
            }
        }

        public static string Valves
        {
            get
            {
                return _prefix + "Valves";
            }
        }
    }
}
