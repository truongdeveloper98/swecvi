using System.Text.Json;

namespace SWECVI.ApplicationCore.CustomExceptions
{
    public class CustomException : Exception
    {
        public const int StatusCode = 420;

        public static string ModifyMessage(object result)
        {
            return JsonSerializer.Serialize(result);
        }

        public CustomException(string message) : base(ModifyMessage(message))
        {

        }

        public CustomException(Exception ex) : base(ModifyMessage(ex.Message))
        {

        }
    }
}