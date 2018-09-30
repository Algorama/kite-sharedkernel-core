using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedKernel.Domain.Repositories;
using SharedKernel.NHibernate.Repositories;
using SharedKernel.Test.Moks;
using System;
using System.Linq;

namespace SharedKernel.Test.Repositories
{
    [TestClass]
    public class TestNHibernateRepository
    {
        private readonly IRepository<Foo> _repository;

        public TestNHibernateRepository()
        {
            HelperRepository.CreateSchema();

            _repository = new Repository<Foo>(HelperRepository.OpenSession());

            var foo1 = new Foo { Bar = 10, DataInclusao = DateTime.Now, UsuarioInclusao = "test" };
            var foo2 = new Foo { Bar = 20, DataInclusao = DateTime.Now, UsuarioInclusao = "test" };
            var foo3 = new Foo { Bar = 30, DataInclusao = DateTime.Now, UsuarioInclusao = "test" };

            _repository.Insert(foo1);
            _repository.Insert(foo2);
            _repository.Insert(foo3);
        }

        [TestMethod]
        public void Test_Get()
        {
            var foo = _repository.Get(1);
            foo.Bar.Should().Be(10);

            Console.WriteLine(foo);
        }

        [TestMethod]
        public void Test_GetAll()
        {
            var foos = _repository.Query.ToList();
            foos.Any().Should().Be(true);

            foreach (var foo in foos)
                Console.WriteLine(foo);
        }

        [TestMethod]
        public void Test_Get_Filtered()
        {
            var foos = _repository.Query.Where(x => x.Bar > 15);
            foos.Any().Should().Be(true);

            foreach (var foo in foos)
                Console.WriteLine(foo);
        }

        [TestMethod]
        public void Test_Get_Linq()
        {
            var foos = from x in _repository.Query
                       where x.Bar < 30
                       orderby x.Bar descending
                       select x;

            foos.Any().Should().Be(true);

            foreach (var foo in foos)
                Console.WriteLine(foo);
        }

        [TestMethod]
        public void Test_Insert()
        {
            var foo = new Foo { Bar = 40, DataInclusao = DateTime.Now, UsuarioInclusao = "test" };
            _repository.Insert(foo);
            foo.Id.Should().BeGreaterThan(0);

            Console.WriteLine(foo);
        }

        [TestMethod]
        public void Test_Update()
        {
            var fooToUpdate = _repository.Get(2);
            fooToUpdate.Bar = 25;
            fooToUpdate.DataAlteracao = DateTime.Now;
            fooToUpdate.UsuarioAlteracao = "test";
            
            _repository.Update(fooToUpdate);

            var fooFromRepo = _repository.Get(2);
            fooFromRepo.Bar.Should().Be(25);

            Console.WriteLine(fooFromRepo);
        }

        [TestMethod]
        public void Test_Delete()
        {
            var fooToDelete = _repository.Get(3);

            _repository.Delete(fooToDelete);

            var fooFromRepo = _repository.Get(3);
            fooFromRepo.Should().BeNull();
        }
    }
}
