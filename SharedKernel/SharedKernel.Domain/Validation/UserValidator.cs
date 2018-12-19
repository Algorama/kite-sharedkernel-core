using System.Linq;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;

namespace SharedKernel.Domain.Validation
{
    public class UserValidator : Validator<User>
    {
        public UserService Service { get; set; }

        protected override void DefaultValidations(ValidatorResult result, User entity)
        {
            base.DefaultValidations(result, entity);

            if(Service.GetAll(x => x.Login.ToLower() == entity.Login.ToLower() && x.Id != entity.Id).Any())
                result.AddError($"There is already a user with this Login: {entity.Login}");
        }
    }
}