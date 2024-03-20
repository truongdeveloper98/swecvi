namespace SWECVI.ApplicationCore.Business
{
    public class ValidationResult
    {
        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }

        public object Data { get; set; }

        public ValidationResult()
        {
            this.Succeeded = true;
        }

        public ValidationResult(string errorMessage)
        {
            this.Succeeded = false;
            this.ErrorMessage = errorMessage;
        }

        public ValidationResult(string errorMessage, object data)
        {
            this.Succeeded = false;
            this.ErrorMessage = errorMessage;
            this.Data = data;
        }
    }
}
