using System.Collections.Generic;
using SharedKernel.DependencyInjector;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;

namespace SharedKernel.Test.Utils
{
    public static class FactoryTools
    {
        public static T Save<T>(this T entidade) where T : EntityBase, IAggregateRoot
        {
            var servico = Kernel.Get<CrudService<T>>();
            servico.Insert(entidade, "test");
            return entidade;
        }

        public static IList<T> Save<T>(this List<T> entidades) where T : EntityBase, IAggregateRoot
        {
            var servico = Kernel.Get<CrudService<T>>();
            foreach(var entidade in entidades)
                servico.Insert(entidade, "test");
            return entidades;
        }
    }
}