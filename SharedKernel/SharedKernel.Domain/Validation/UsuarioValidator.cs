using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;

namespace SharedKernel.Domain.Validation
{
    public class UsuarioValidator : Validator<Usuario>
    {
        public UsuarioService Service { get; set; }
    }
}