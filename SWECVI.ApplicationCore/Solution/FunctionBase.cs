using SWECVI.ApplicationCore.Business;
using SWECVI.ApplicationCore.Infrastructure;
using SWECVI.ApplicationCore.Entities;
using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.Solution
{
    public static class FunctionBase
    {
        public static DCode DataMissingOrNormal()
        {
            return DCode.Normal;
        }
    }
}
