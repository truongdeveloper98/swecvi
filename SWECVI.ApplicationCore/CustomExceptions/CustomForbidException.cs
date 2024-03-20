namespace SWECVI.ApplicationCore.CustomExceptions
{
    public class CustomForbidException : Exception
    {
        public const int StatusCode = 403;
        private const string DefaultMessage = "You don't have permission to access this resource";

        public CustomForbidException() : base(DefaultMessage)
        {

        }

        public CustomForbidException(string message) : base(message)
        {

        }

        public CustomForbidException(Exception ex) : base(ex.Message)
        {

        }
    }
}