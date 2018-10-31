using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedKernel.Domain.Repositories;
using SharedKernel.NHibernate.Repositories;
using SharedKernel.Test.Moks;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace SharedKernel.Test.Repositories
{
    [TestClass]
    public class TestRepositoryNHibernate
    {
        private static IHelperRepository _helperRepository;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            NHibernateHelper.CreateSchema();

            DependencyInjector.Kernel.StartNHibernate();
            _helperRepository = DependencyInjector.Kernel.Get<IHelperRepository>();

            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();
                try
                {
                    session.StartTransaction();

                    var foo1 = new Foo { Bar = 10, DataInclusao = DateTime.Now, UsuarioInclusao = "test" };
                    var foo2 = new Foo { Bar = 20, DataInclusao = DateTime.Now, UsuarioInclusao = "test" };
                    var foo3 = new Foo { Bar = 30, DataInclusao = DateTime.Now, UsuarioInclusao = "test" };

                    repo.Insert(foo1);
                    repo.Insert(foo2);
                    repo.Insert(foo3);

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }
        }

        [TestMethod]
        public void Test_Get()
        {
            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();

                var foo = repo.Get(1);

                foo.Bar.Should().Be(10);
                Console.WriteLine(foo);
            }
        }

        [TestMethod]
        public void Test_GetAll()
        {
            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();

                var foos = repo.Query.ToList();
                foos.Any().Should().Be(true);

                foreach (var foo in foos)
                    Console.WriteLine(foo);
            }
        }

        [TestMethod]
        public void Test_Get_Filtered()
        {
            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();

                var foos = repo.Query.Where(x => x.Bar > 15);
                foos.Any().Should().Be(true);

                foreach (var foo in foos)
                    Console.WriteLine(foo);
            }
        }

        [TestMethod]
        public void Test_Get_Linq()
        {
            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();

                var foos = from x in repo.Query
                           where x.Bar < 30
                           orderby x.Bar descending
                           select x;

                foreach (var foo in foos)
                    Console.WriteLine(foo);
            }
        }

        [TestMethod]
        public void Test_Insert()
        {
            var foo = new Foo { Bar = 40, DataInclusao = DateTime.Now, UsuarioInclusao = "test" };

            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();
                try
                {
                    session.StartTransaction();
                    
                    repo.Insert(foo);

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }

            foo.Id.Should().BeGreaterThan(0);
            Console.WriteLine(foo);
        }

        [TestMethod]
        public void Test_Update()
        {
            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();
                try
                {
                    session.StartTransaction();

                    var fooToUpdate = repo.Get(2);
                    fooToUpdate.Bar = 25;
                    fooToUpdate.DataAlteracao = DateTime.Now;
                    fooToUpdate.UsuarioAlteracao = "test";
                    repo.Update(fooToUpdate);

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }

            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();

                var fooFromRepo = repo.Get(2);
                fooFromRepo.Bar.Should().Be(25);
                Console.WriteLine(fooFromRepo);
            }
        }

        [TestMethod]
        public void Test_Delete()
        {
            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();
                try
                {
                    session.StartTransaction();

                    var fooToDelete = repo.Get(3);
                    repo.Delete(fooToDelete);

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }

            using (var session = _helperRepository.OpenSession())
            {
                var repo = session.GetRepository<Foo>();

                var fooFromRepo = repo.Get(3);
                fooFromRepo.Should().BeNull();
            }
        }
    }
}