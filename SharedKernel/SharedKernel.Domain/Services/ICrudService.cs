using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Services
{
    public interface ICrudService<T> : IQueryService<T> where T : EntityBase, IAggregateRoot
    {
        void Delete(long id);
        void Insert(string user, params T[] entities);
        void Insert(T entity, string user);
        void Update(string user, params T[] entities);
        void Update(T entity, string user);
        void RunCommand(string hqlCommand);
    }
}