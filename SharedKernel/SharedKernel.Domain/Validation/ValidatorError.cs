using System;

namespace SharedKernel.Domain.Validation
{
    public class ValidatorError
    {
        public string ErrorMessage { get; set; }

        public ValidatorError()
        {
        }

        public ValidatorError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ValidatorError(Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}