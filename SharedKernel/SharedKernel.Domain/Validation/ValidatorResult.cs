using System.Collections.Generic;
using System.Linq;

namespace SharedKernel.Domain.Validation
{
    public class ValidatorResult
    {
        public List<ValidatorError> Errors { get; set; }

        public virtual bool IsValid => !Errors.Any();

        public ValidatorResult()
        {
            Errors = new List<ValidatorError>();
        }

        public void AddError(string errorMessage)
        {
            if (Errors == null)
                Errors = new List<ValidatorError>();

            Errors.Add(new ValidatorError
            {
                ErrorMessage = errorMessage
            });
        }
    }
}