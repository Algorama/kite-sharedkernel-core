using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories
{
    public interface IRepository<T> : IQueryRepository<T> where T : EntityBase, IAggregateRoot
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}