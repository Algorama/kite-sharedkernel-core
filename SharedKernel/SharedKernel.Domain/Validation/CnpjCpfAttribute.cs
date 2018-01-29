using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharedKernel.Domain.Extensions;

namespace SharedKernel.Domain.Validation
{
    public sealed class CnpjCpfAttribute : ValidationAttribute
    {
        public CnpjCpfAttribute()
        {
            ErrorMessage = "CPF/CNPJ Inválido";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (((string)value).ValidarCnpjCpf())
                    return ValidationResult.Success;

                return new ValidationResult(ErrorMessage, new List<string>
                {
                    validationContext.MemberName
                });
            }
            catch (Exception)
            {
                return new ValidationResult(ErrorMessage, new List<string>
                {
                    validationContext.MemberName
                });
            }
        }
    }
}