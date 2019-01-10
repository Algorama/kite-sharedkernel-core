using System;
using System.Linq;
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Helpers;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Validation;
using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Domain.Services
{
    public class UserService : CrudService<User>
    {
        public UserService(IHelperRepository helperRepository, UserValidator validator) : base(helperRepository, validator)
        {
            validator.Service = this;
        }

        public override User Get(long id)
        {
            var user = base.Get(id);
            user.Password = null;
            return user;
        }

        public override PageResult<User> GetPaged(int page, PageSize size)
        {
            var users = base.GetPaged(page, size);
            foreach (var user in users.Data)
                user.Password = null;

            return users;
        }

        public override void Insert(User entity, string user = "system")
        {
            entity.Password = CryptoTools.ComputeHashMd5(entity.Password);
            base.Insert(entity, user);
        }

        public override void Update(User entity, string user = "system")
        {
            var oldUser = base.Get(entity.Id);
            entity.Password = oldUser.Password;
            base.Update(entity, user);
        }

        public Token Login(LoginRequest loginRequest)
        {            
            var user = GetAll(x => x.Login.ToUpper() == loginRequest.Login.ToUpper()).FirstOrDefault();
            if (user == null) return null;

            var password = CryptoTools.ComputeHashMd5(loginRequest.Password);
            if (user.Password != password) return null;

            var token = new Token
            {
                UserId = user.Id,
                UserName = user.Name,
                Login = user.Login,
                ExpirateAt = DateTime.Now.AddHours(12)
            };

            return token;
        }

        public void ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var oldPassword = CryptoTools.ComputeHashMd5(changePasswordRequest.OldPassword);
            var user = GetAll(x =>
                x.Login.ToUpper() == changePasswordRequest.Login.ToUpper() &&
                x.Password == oldPassword).FirstOrDefault();

            if (user == null)
                throw new ValidatorException("Old Password is invalid!");

            user.Password = CryptoTools.ComputeHashMd5(changePasswordRequest.NewPassword);

            base.Update(user);
        }
    }
}