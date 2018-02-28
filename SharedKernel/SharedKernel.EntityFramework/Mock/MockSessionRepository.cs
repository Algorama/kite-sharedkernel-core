using System;
using System.Collections.Generic;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Mock
{
    public class MockSessionRepository : ISessionRepository
    {
        private static Dictionary<string, object> _repositorios;

        public IQueryRepository<T> QueryRepository<T>() where T : EntityBase
        {
            if (_repositorios == null)
                _repositorios = new Dictionary<string, object>();

            var entidadeNome = typeof(T).Name;

            if (!_repositorios.ContainsKey(entidadeNome))
                _repositorios.Add(entidadeNome, new MockQueryRepository<T>());

            return _repositorios[entidadeNome] as MockQueryRepository<T>;
        }

        public IRepository<T> Repository<T>() where T : EntityBase, IAggregateRoot
        {
            if (_repositorios == null)
                _repositorios = new Dictionary<string, object>();

            var entidadeNome = typeof(T).Name;

            if (!_repositorios.ContainsKey(entidadeNome))
                _repositorios.Add(entidadeNome, new MockRepository<T>());

            return _repositorios[entidadeNome] as MockRepository<T>;
        }

        public void StartTransaction()
        {
            Console.WriteLine("Mock: Transação Iniciada");
        }

        public void CommitTransaction()
        {
            Console.WriteLine("Mock: Transação Commitada");
        }

        public void RollBackTransaction()
        {
            Console.WriteLine("Mock: RollBack da Transação");
        }

        public void Dispose()
        {
            Console.WriteLine("Mock: Dispose da Sessão");
        }
    }
}