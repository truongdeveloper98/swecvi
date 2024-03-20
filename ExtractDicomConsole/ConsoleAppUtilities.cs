using System.Linq;

namespace ExtractDicomConsole
{
    public class ConsoleAppUtilities
    {
        private static List<string> files;

        public static void ClearData()
        {
            if(files != null) files.Clear();
        }
        public static List<string> DirSearch(string sDir)
        {
           
            try
            {
                if (files is null)
                {
                    files = new List<string>();
                }

                if (files.Count() == 0)
                {
                    files = new List<string>();
                }

                foreach (string f in Directory.GetFiles(sDir))
                {
                    string file = string.Empty;
                    if (!Path.HasExtension(file))
                    {
                        file = Path.ChangeExtension(f, ".dcm");
                    }
                    string fileExt = System.IO.Path.GetExtension(file);
                    if (fileExt.Contains(".dcm") && !files.Any(x=>x == file))
                    files.Add(file);
                }

                if (Directory.GetDirectories(sDir).Count() == 0)
                    return files;

                var subFolders = Directory.GetDirectories(sDir);

                foreach (var d in subFolders)
                {
                    DirSearch(d);
                }

            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

            return files;
        }

        public static string GetMeasurementType(int resultNo)
        {
            string result = string.Empty;
            switch (resultNo)
            {
                case -1:
                    result = "R";
                    break;
                case 0:
                    result = "M1";
                    break;
                case 1:
                    result = "M2";
                    break;
                case 2:
                    result = "M3";
                    break;
                case 3:
                    result = "M4";
                    break;
                case 4:
                    result = "M5";
                    break;
                default:
                    result = "Error measurement undefined";
                    break;
            }
            return result;
        }
    }
}
