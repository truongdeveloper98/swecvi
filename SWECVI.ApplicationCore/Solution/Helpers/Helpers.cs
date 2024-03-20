using SWECVI.ApplicationCore.Business;
using SWECVI.ApplicationCore.Common;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Infrastructure;
using System.Data;
using static SWECVI.ApplicationCore.Enum;


namespace SWECVI.ApplicationCore.Solution
{
    public static class Helpers
    {
        public static double? GetBSA(ParameterDictionary PAR)
        {
            return ExamHelper.CalculateBSA(PAR?.value.GetValue(ParameterNames.PatientWeight), PAR?.value.GetValue(ParameterNames.PatientHeight));
        }

        public static DateTime FormatStringToDate(string date, string time = "")
        {
            if(date.Length != 8)
            {
                return DateTime.Now;
            }

            if(!string.IsNullOrEmpty(time) && time.Length != 6)
            {
                return DateTime.Now;
            }

            if(!string.IsNullOrEmpty(time))
            {
               DateTime dateFormat = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4, 2)), Convert.ToInt32(date.Substring(6, 2)),
                    Convert.ToInt32(time.Substring(0, 2)), Convert.ToInt32(date.Substring(2, 2)), Convert.ToInt32(date.Substring(4, 2)));

                return dateFormat;
            }

            DateTime dateFormatWithoutTime = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4, 2)), Convert.ToInt32(date.Substring(6, 2)));

            return dateFormatWithoutTime;
        }

        public static void LVEEDoppler(ParameterDictionary PAR, ref double? LVEprimeavg, ref double? LVEEPrime)
        {
            double? EPrimeLatDoppler = PAR?.value.GetValue(ParameterNames.LVEprimelat);
            double? EPrimeSeptDoppler = PAR?.value.GetValue(ParameterNames.LVEprimesept);
            double? MVEVelocity = PAR?.value.GetValue(ParameterNames.MVEVelocity);

            if ((EPrimeLatDoppler == null) && (EPrimeSeptDoppler != null))
            {
                LVEprimeavg = EPrimeSeptDoppler;
            }
            else if ((EPrimeLatDoppler != null) && (EPrimeSeptDoppler == null))
            {
                LVEprimeavg = EPrimeLatDoppler;
            }
            else if ((EPrimeLatDoppler != null) && (EPrimeSeptDoppler != null))
            {
                LVEprimeavg = (EPrimeLatDoppler + EPrimeSeptDoppler) / 2;
            }
            else
            {
                LVEprimeavg = null;
            }

            if (MVEVelocity != null && LVEprimeavg != null)
            {
                LVEEPrime = Math.Abs(MVEVelocity.Value / LVEprimeavg.Value);
            }
            else
            {
                LVEEPrime = null;
            }

        }

        #region EETDI
        public static void LVEETDI(ParameterDictionary PAR, ref double? LVEmavg, ref double? LVEEm)
        {
            // Compensate for lower velocities when measured with color TDI
            // http://www.ncbi.nlm.nih.gov/pmc/articles/PMC2898098/
            const double slope = 1.17;
            const double intersection = 1.25;

            double? EPrimeLatTDI = PAR?.value.GetValue(ParameterNames.LVEmlat);
            double? EPrimeSeptTDI = PAR?.value.GetValue(ParameterNames.LVEmsept);
            double? MVEVelocity = PAR?.value.GetValue(ParameterNames.MVEVelocity);

            if ((EPrimeLatTDI == null) && (EPrimeSeptTDI != null))
            {
                LVEmavg = EPrimeSeptTDI.Value;
                LVEmavg = slope * LVEmavg + intersection;
            }
            else if ((EPrimeLatTDI != null) && (EPrimeSeptTDI == null))
            {
                LVEmavg = EPrimeLatTDI.Value;
                LVEmavg = slope * LVEmavg + intersection;
            }
            else if ((EPrimeLatTDI != null) && (EPrimeSeptTDI != null))
            {
                LVEmavg = Math.Abs((EPrimeLatTDI.Value + EPrimeSeptTDI.Value) / 2);
                LVEmavg = (slope * LVEmavg + intersection) * -1;

            }
            else
            {
                LVEmavg = null;
            }

            if (MVEVelocity != null && LVEmavg != null)
            {
                LVEEm = Math.Abs(MVEVelocity.Value / LVEmavg.Value);
            }
            else
            {
                LVEEm = null;
            }
        }
        #endregion EETDI

        public static string GetHeader(Header header)
        {
            AssessmentText assessment;
            if (ApplicationState.Assessments.TryGetValue((int)header, out assessment))
            {
                return assessment.Text;
            }

            return string.Empty;
        }

    }
}
