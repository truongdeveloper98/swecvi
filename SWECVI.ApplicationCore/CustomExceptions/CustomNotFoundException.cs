namespace SWECVI.ApplicationCore.CustomExceptions
{
    public class CustomNotFoundException : Exception
    {
        public const int StatusCode = 404;
        private const string DefaultMessage = "Resource not found";

        public CustomNotFoundException() : base(DefaultMessage)
        {

        }

        public CustomNotFoundException(string message) : base(message)
        {

        }

        public CustomNotFoundException(Exception ex) : base(ex.Message)
        {

        }
    }
}