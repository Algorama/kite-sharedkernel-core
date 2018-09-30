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
        public IQueryRepository<T> Repository { get; }

        public QueryService(IQueryRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual T Get(long id)
        {
            try
            {
                var entity = Repository.Get(id);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<T> GetAll()
        {
            try
            {
                var entities = Repository.Query.ToList();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> @where)
        {
            try
            {
                var entities = Repository.Query.Where(@where).ToList();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual PageResult<T> GetPaged(int page, PageSize size)
        {
            if (page < 1)
                throw new ValidationException("Número da Página começa em 1");

            try
            {
                var result = new PageResult<T> { Page = page };
                var query = Repository.Query;

                result.TotalItems = query.Count();
                result.PageSize = (int)size;
                result.TotalPages =
                    (int)Math.Ceiling((double)result.TotalItems / (int)size);

                result.Data = query
                    .Skip((int)size * --page)
                    .Take((int)size)
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}