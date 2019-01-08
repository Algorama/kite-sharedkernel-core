using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedKernel.DependencyInjector;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;
using SharedKernel.Test.Factories;

namespace SharedKernel.Test.Services
{
    [TestClass]
    public class TestUserService
    {
        private static UserService _service;
        private static IList<User> _entities;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Use esse Bind para rodar os testes com Repositorios Mock
            Kernel.StartMockRepository();

            // Use esse Bind para rodar os testes no SQL Server
            //Kernel.StartNHibernate();
            //NHibernate.Repositories.NHibernateHelper.CreateSchema();

            _service = Kernel.Get<UserService>();
            _entities = UserFactory.Get(4, true);
        }

        [TestMethod]
        public void Test_Get()
        {
            var entity = _service.Get(_entities[0].Id);
            entity.Should().NotBeNull();
            Console.WriteLine(entity);
        }

        [TestMethod]
        public void Test_GetAll()
        {
            var entities = _service.GetAll();
            entities.Count.Should().BeGreaterOrEqualTo(1);
            foreach (var entity in entities)
                Console.WriteLine(entity);
        }

        [TestMethod]
        public void Test_Insert()
        {
            var entity = UserFactory.Get();
            _service.Insert(entity, "test");
            entity.Id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void Test_Update()
        {
            var entity = _entities[1];
            _service.Update(entity, "test");
            entity.DataAlteracao.Should().HaveValue();
        }

        [TestMethod]
        public void Test_Delete()
        {
            _service.Delete(_entities[3].Id);
            var entity = _service.Get(_entities[3].Id);
            entity.Should().BeNull();
        }
    }
}
