using System.Text.Json;

namespace SWECVI.ApplicationCore.CustomExceptions
{
    public class CustomValidationException : Exception
    {
        public const int StatusCode = 400;

        public static string ModifyMessage(object result)
        {
            return JsonSerializer.Serialize(result);
        }

        public CustomValidationException(string message) : base(message)
        {

        }

        public CustomValidationException(Exception ex) : base(ex.Message)
        {

        }
    }
}