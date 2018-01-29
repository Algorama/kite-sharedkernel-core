using System;
using System.Collections.Generic;

namespace SharedKernel.Domain.Validation
{
    public class ValidatorException : Exception
    {
        public List<ValidatorError> Errors { get; }

        public ValidatorException(string message) : base(message)
        {
            Errors = new List<ValidatorError> { new ValidatorError(message) };
        }

        public ValidatorException(Exception ex) : base(ParseErros(ex))
        {
            Errors = new List<ValidatorError> { new ValidatorError(ParseErros(ex)) };
        }

        public ValidatorException(List<ValidatorError> errors) : base(ParseErros(errors))
        {
            Errors = errors;
        }

        private static string ParseErros(IEnumerable<ValidatorError> errors)
        {
            var mensagem = "Erros encontrados :" + Environment.NewLine;
            foreach (var erro in errors)
                mensagem += $"  - {erro.ErrorMessage}{Environment.NewLine}";

            return mensagem;
        }

        private static string ParseErros(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
    }
}