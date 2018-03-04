using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories
{
    public interface IRepository<T> : IQueryRepository<T> where T : IAggregateRoot
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        int RunCommand(string command, params object[] poParams);

        // void Add<T>(T obj) where T : class;
        // void Delete<T>(Func<T, bool> predicate) where T : class;
        // void Edit<T>(T obj) where T : class;
        // int ExecuteSql(string psSql, params object[] poParams);
        // void Save();   
    }
}