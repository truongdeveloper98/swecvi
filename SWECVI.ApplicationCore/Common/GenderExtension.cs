using System.Text.RegularExpressions;
using static SWECVI.ApplicationCore.Enum;

namespace SWECVI.ApplicationCore.Common
{
    public class GenderExtension
    {
        public static Gender GetGenderFromSSN(string ssn)
        {
            const int ssnLength = 12; // Numbers of digits in SSN string

            if (!string.IsNullOrEmpty(ssn))
            {
                ssn = Regex.Replace(ssn, @"[^0-9]", ""); // Allow only digits 0-9 in SSN

                if (ssn.Length == ssnLength)
                {
                    if ((int.Parse(ssn.Substring(10, 1))) % 2 == 0) // mod
                    {
                        return Gender.Female;  // even number = FEMALE
                    }
                    else
                    {
                        return Gender.Male;    // uneven number = MALE
                    }
                }
                else
                {
                    return Gender.Unknown;
                }
            }
            else
            {
                // User dialog to set correct gender?
                return Gender.Unknown;
            }
        }

        public static bool? ConvertToBoolean(Gender gender)
        {
            if (gender == Gender.Unknown)
            {
                return true;            // Default is Male
            }

            return gender == Gender.Male;
        }

        public static Gender ConvertFromBoolean(bool? value)
        {
            if (value == null)
            {
                return Gender.Unknown;
            }

            return value.Value ? Gender.Male : Gender.Female;
        }

        public static Gender ConvertFromString(string value)
        {
            if (value == "M")
            {
                return Gender.Male;
            }

            if (value == "F")
            {
                return Gender.Female;
            }
            
            return Gender.Unknown;
        }
    }
}
