using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace SharedKernel.NHibernate.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : EntityBase
    {
        public ISession Session { get; set; }

        public QueryRepository(ISession session)
        {
            Session = session;
        }

        public T Get(long id)
        {
            return Session.Get<T>(id);
        }

        public async Task<T> GetAsync(long id)
        {
            return await Session.GetAsync<T>(id);
        }

        public IQueryable<T> Query => Session.Query<T>();

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> @where)
        {
            return await Session.Query<T>().Where(@where).ToListAsync();
        }
    }
}