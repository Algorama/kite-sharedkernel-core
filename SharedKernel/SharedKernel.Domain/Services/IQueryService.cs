using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Domain.Services
{
    public interface IQueryService<T> where T : IEntity
    {
        T Get(long id);
        IList<T> GetAll();
        IList<T> GetAll(Expression<Func<T, bool>> where);
        PageResult<T> GetPaged(int page, PageSize size);
    }
}