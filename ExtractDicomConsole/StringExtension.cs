using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace ExtractDicomConsole
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

        public static void ExportCsv<T>(List<T> genericList, string fileName)
        {
            var sb = new StringBuilder();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var finalPath = Path.Combine(basePath, fileName + ".csv");
            var header = "";
            var info = typeof(T).GetProperties();
            if (!File.Exists(finalPath))
            {
                var file = File.Create(finalPath);
                file.Close();
                foreach (var prop in typeof(T).GetProperties())
                {
                    header += prop.Name + "\t";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
            foreach (var obj in genericList)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += prop.GetValue(obj, null) + "\t";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
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
                    catch (InvalidOperationException e)
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
