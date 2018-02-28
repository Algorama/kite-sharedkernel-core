using System.Linq;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Mock
{
    public class MockRepository<T> : MockQueryRepository<T>, IRepository<T> where T : EntityBase, IAggregateRoot
    {
        public void Insert(T entity)
        {
            entity.Id = GenerateId();
            Data.Add(entity);
        }

        public void Update(T entity)
        {
            Delete(entity);
            Insert(entity);
        }

        public void Save(T entity)
        {
            Delete(entity);
            Insert(entity);
        }

        public void Delete(T entity)
        {
            Data.Remove(entity);
        }

        public void RunCommand(string hqlCommand)
        {
            
        }

        private static long GenerateId()
        {
            return Data.Count == 0 ? 1 : Data.Max(x => x.Id) + 1;
        }
    }
}