using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;

namespace SharedKernel.Domain.Validation
{
    public class UserValidator : Validator<User>
    {
        public UserService Service { get; set; }
    }
}