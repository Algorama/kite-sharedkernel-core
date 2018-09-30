using NHibernate;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.NHibernate.Repositories
{
    public class Repository<T> : QueryRepository<T>, IRepository<T> where T : EntityBase, IAggregateRoot
    {
        public Repository(ISession session) : base(session)
        {
        }

        public void Insert(T entity)
        {
            Session.Save(entity);
        }
        
        public void Update(T entity)
        {
            Session.Merge(entity);
        }

        public void Save(T entity)
        {
            Session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }
    }
}