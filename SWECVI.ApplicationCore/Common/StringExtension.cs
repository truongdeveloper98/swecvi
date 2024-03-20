using System.Globalization;
using System.Xml.Serialization;

namespace SWECVI.ApplicationCore.Common
{
    public static class StringExtension
    {
        public static DateTime ConvertStringDateToDate(this string? stringDate)
        {
            try
            {
                if (!string.IsNullOrEmpty(stringDate) && stringDate.Length == 8)
                {
                    return DateTime.ParseExact(stringDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                return DateTime.MinValue;
            }
            catch (System.Exception)
            {
                return DateTime.MinValue;
            }

        }
        public static string FormatBloodPressure(this string? str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.Split('/').First();
            }
            return "";
        }

        public static TimeSpan? ConvertStringTimeToTime(this string stringTime)
        {
            try
            {
                if (!string.IsNullOrEmpty(stringTime) && (stringTime.Length == 6 || stringTime.Length == 5))
                {
                    return TimeSpan.ParseExact(stringTime, "hmmss", CultureInfo.InvariantCulture);
                }
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static bool IsIPV4(this string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            string[] parts = ipAddress.Split('.');
            if (parts.Length != 4)
            {
                return false;
            }

            byte b;

            return parts.All(r => byte.TryParse(r, out b));
        }

        public static T FromXml<T>(this string xml)
        {
            T returnedXmlClass = default(T);
            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        returnedXmlClass = (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            return returnedXmlClass;
        }
    }
}
