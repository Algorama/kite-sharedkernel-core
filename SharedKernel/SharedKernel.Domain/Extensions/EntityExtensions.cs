using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Extensions
{
    public static class EntityExtensions
    {
        /// <summary>
        ///     Método de extensão usado para vincular registros de uma bag ao seu registro pai,
        ///     realizando reflection no type para obter o tipo generico da propriedade,
        ///     que se for lista, altera os objetos dinamicamente, referenciando o objeto pai.
        /// </summary>
        /// <param name="source">Uma EntityBase</param>
        /// <param name="usuario">Login do Usuário</param>
        /// <returns>A própria Entity</returns>
        public static EntityBase AddInverseReferences(this EntityBase source, string usuario)
        {
            SetaPropriedadesAgregacaoRaiz(source, usuario);
            return source;
        }

        // O unico possivel loop infinito nessa operacao é quando uma entity tem uma lista de entity 
        // e esse lista de entity (cada entity) tem uma lista de entity da entity anterior. :D -- o que é um erro de modelagem!!
        private static void SetaPropriedadesAgregacaoRaiz(EntityBase entity, string usuario)
        {
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var resultado = prop.IsCollection();
                if (resultado == false)
                    continue;

                var collection = (IList)entity.GetType().GetProperty(prop.Name)?.GetValue(entity);
                if (collection == null)
                    continue;

                foreach (var item in collection)
                {
                    var type = item.GetType();
                    var sourceType = entity.GetType().Name;

                    var associacaoInversa = type.GetProperties().FirstOrDefault(x => x.PropertyType.Name.ToLower() == sourceType.ToLower());
                    associacaoInversa?.SetValue(item, entity);

                    // Definindo DataExpiracao e Usuário de Inclusão das Entidades Filhas, somente quando o ID é 0
                    var propId = type.GetProperties().FirstOrDefault(x => x.Name == "Id");
                    if (propId == null || (long)propId.GetValue(item) == 0)
                    {
                        var propDataInclusao = type.GetProperties().FirstOrDefault(x => x.Name == "DataInclusao");
                        propDataInclusao?.SetValue(item, DateTime.Now);

                        var propUsuarioInclusao = type.GetProperties().FirstOrDefault(x => x.Name == "UsuarioInclusao");
                        propUsuarioInclusao?.SetValue(item, usuario);
                    }

                    SetaPropriedadesAgregacaoRaiz((EntityBase)item, usuario);
                }
            }
        }

        private static bool IsCollection(this PropertyInfo prop)
        {
            var propType = prop.PropertyType;
            if (!propType.IsGenericType)
                return false;

            var genericType = propType.GetGenericTypeDefinition();
            return genericType == typeof(IList<>);
        }
    }
}
