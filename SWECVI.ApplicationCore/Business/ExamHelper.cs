using System.Globalization;

namespace SWECVI.ApplicationCore.Business
{
    public class ExamHelper
    {
        public static double? CalculateBSA(double? weight, double? height)
        {
            if (weight == null || height == null)
            {
                return null;
            }

            if (weight == 0 || height == 0)
            {
                return null;
            }

            height = height.Value * 100; // m => cm

            return (Math.Pow(weight.Value, 0.425) * Math.Pow(height.Value, 0.725) * 0.007184);
        }

        public static int? GetAgeFromSSN(string ssn, DateTime dateTime)
        {
            if (!string.IsNullOrEmpty(ssn) && ssn.Length >= 4)
            {
                int yearOfBirth;
                if (int.TryParse(ssn.Substring(0, 4), out yearOfBirth))
                {
                    var age = dateTime.Year - yearOfBirth;
                    if (age >= 0 && age <= 110)
                    {
                        return age;
                    }
                }
            }

            return null;
        }

        public static int? Age(DateTime birthday, string ssn, DateTime examinationDate)
        {
            if (birthday == null || birthday.Year < 1900)
            {
                return GetAgeFromSSN(ssn, examinationDate);
            }
            int age = examinationDate.Year - birthday.Year;
            if (examinationDate < birthday.AddYears(age)) age--;

            return age;
        }
    }
}
