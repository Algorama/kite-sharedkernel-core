using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Domain.Services
{
    public class QueryService<T> : IQueryService<T> where T : EntityBase
    {
        public IHelperRepository Helper { get; }

        public QueryService(IHelperRepository helper)
        {
            Helper = helper;
        }

        public virtual T Get(long id)
        {
            using (var session = Helper.OpenSession())
            {
                var repo = session.QueryRepository<T>();
                try
                {
                    session.StartTransaction();
                    var entity = repo.Get(id);
                    session.CommitTransaction();
                    return entity;
                }
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }

        public virtual IList<T> GetAll()
        {
            using (var session = Helper.OpenSession())
            {
                var repo = session.QueryRepository<T>();
                try
                {
                    session.StartTransaction();
                    var entities = repo.GetAll().ToList();
                    session.CommitTransaction();
                    return entities;
                }
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> @where)
        {
            using (var session = Helper.OpenSession())
            {
                var repo = session.QueryRepository<T>();
                try
                {
                    session.StartTransaction();
                    var entities = repo.Get(@where).ToList();
                    session.CommitTransaction();
                    return entities;
                }
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }

        public virtual PageResult<T> GetPaged(int page, PageSize size)
        {
            if (page < 1)
                throw new ValidationException("N�mero da P�gina come�a em 1");

            using (var session = Helper.OpenSession())
            {
                var repo = session.QueryRepository<T>();
                try
                {
                    session.StartTransaction();

                    var result = new PageResult<T> { Page = page };
                    var query = repo.GetAll();

                    result.TotalItems = query.Count();
                    result.PageSize = (int)size;
                    result.TotalPages =
                        (int)Math.Ceiling((double)result.TotalItems / (int)size);

                    result.Data = query
                        .Skip((int)size * --page)
                        .Take((int)size)
                        .ToList();

                    session.CommitTransaction();
                    return result;
                }
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }

        public virtual ODataResult<T> GetOData(List<KeyValuePair<string, string>> queryStringParts)
        {
            using (var session = Helper.OpenSession())
            {
                var repo = session.QueryRepository<T>();
                try
                {
                    session.StartTransaction();
                    var result = repo.GetOData(queryStringParts);
                    session.CommitTransaction();
                    return result;
                }
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }
    }
}