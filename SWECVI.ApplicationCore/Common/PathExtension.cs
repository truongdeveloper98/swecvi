namespace SWECVI.ApplicationCore.Common
{
    public class PathExtension
    {
        public static string DefaultRoot()
        {
            var root = Path.Join(Directory.GetCurrentDirectory(), "PythonScript".AsSpan());
            var path = Path.Join(root, "generate_assessment.py");
            if (File.Exists(path))
            {
                return root;
            }
            return Path.Join(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, "SWECVI.ApplicationCore".AsSpan(), "PythonScript".AsSpan());
        }
    }
}
