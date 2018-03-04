using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories
{
    public interface IQueryRepository<T> where T : IEntity
    {
        // T Get(long id);
        // IQueryable<T> GetAll();
        // IQueryable<T> GetAll(Expression<Func<T, bool>> where);
        // IList<T> GetByHql(string hqlCommand);
        // IList<T> GetBySql(string sqlCommand);

        //IQueryable<T> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class;

        T Get(long id);        
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] include);
        IQueryable<T> GetAll();
        ODataResult<T> GetOData(List<KeyValuePair<string, string>> queryStringParts);
    }
}