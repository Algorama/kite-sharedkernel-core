using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SharedKernel.Test.Moks;

namespace SharedKernel.Test.Entities
{
    [TestClass]
    public class TestEntity
    {
        [TestMethod]
        public void Test_Persisted_Entities_Equals()
        {
            var foo1 = new Foo { Id = 1 };
            var foo2 = new Foo { Id = 1 };

            foo1.Should().Be(foo2, because: "they must have the same Id");
        }

        [TestMethod]
        public void Test_Persisted_Entities_Not_Equals()
        {
            var foo1 = new Foo { Id = 1 };
            var foo2 = new Foo { Id = 2 };

            foo1.Should().NotBe(foo2, because: "they doesn't must have the same Id");
        }

        [TestMethod]
        public void Test_Not_Persisted_Entities_Equals()
        {
            var foo1 = new Foo();
            var foo2 = foo1;

            foo1.Should().Be(foo2, because: "they must have the same reference");
        }

        [TestMethod]
        public void Test_Not_Persisted_Entities_Not_Equals()
        {
            var foo1 = new Foo();
            var foo2 = new Foo();

            foo1.Should().NotBe(foo2, because: "they doesn't must have the same reference");
        }

        [TestMethod]
        public void Test_Can_Clone_Entity()
        {
            var foo1 = new Foo{ Bar = 123 };
            var foo2 = (Foo)foo1.Clone();
            
            foo2.Bar.Should().Be(foo1.Bar, because: "they were cloned");
        }        
    }
}