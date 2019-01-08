using System.Collections.Generic;
using Bogus;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Extensions;
using SharedKernel.Test.Utils;

namespace SharedKernel.Test.Factories
{
    public class UserFactory
    {
        private static Faker<User> Faker()
        {
            return new Faker<User>().CustomInstantiator(f => new User())
                    .RuleFor(x => x.Name, y => y.Name.FullName().Truncate(100))
                    .RuleFor(x => x.Login, y => y.Name.LastName().ToLower().Truncate(100))
                    .RuleFor(x => x.Password, y => y.Random.String(6,10));
        }

        public static IList<User> Get(int qtde, bool save = false)
        {
            var entities = new List<User>();
            for(var i=0; i<qtde; i++)
                entities.Add(Get(save));

            return entities;
        }

        public static User Get(bool save = false)
        {
            var entity = Faker().Generate();
            
            if(save) 
                entity.Save();

            return entity;
        }
    }
}
