using System.Linq;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories.Mock
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
            Data.Add(entity);
        }
        
        public void Delete(T entity)
        {
            var entityToDelete = Data.FirstOrDefault(x => x.Id == entity.Id);
            Data.Remove(entityToDelete);
        }

        private static long GenerateId()
        {
            return Data.Count == 0 ? 1 : Data.Max(x => x.Id) + 1;
        }
    }
}