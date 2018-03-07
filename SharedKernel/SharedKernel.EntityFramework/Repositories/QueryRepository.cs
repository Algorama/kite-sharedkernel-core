using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore; 
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : EntityBase
    {
        protected DbContext Context { get; set; }
        protected DbSet<T> Entities {get; set; }

        public QueryRepository(DbContext context)
        {
            Context  = context;
            Entities = Context.Set<T>(); 
        }

        public T Get(long id)
        {
            var result = Entities.Where(x => x.Id == id).AsQueryable();

            // Incluindo automaticamente o primeiro nível de associações e coleções
            var include = GetInclude(typeof(T));
            foreach (var item in include)
                result = result.Include(item).AsQueryable();

            return result.FirstOrDefault();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var result = Entities.Where(predicate).AsQueryable();

            // Incluindo automaticamente o primeiro nível de associações e coleções
            var include = GetInclude(typeof(T));
            foreach (var item in include)
                result = result.Include(item).AsQueryable();

            return result;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] include)
        {
            IQueryable<T> result = Entities.Where(predicate);
            
            foreach (var item in include)
                result = result.Include(item).AsQueryable();
            
            return result.AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            var result = Entities.AsQueryable();

            // Incluindo automaticamente o primeiro nível de associações e coleções
            var include = GetInclude(typeof(T));
            foreach (var item in include)
                result = result.Include(item).AsQueryable();

            return result;
        }

        public ODataResult<T> GetOData(List<KeyValuePair<string, string>> queryStringParts)
        {
            throw new NotImplementedException();

            // //Separo e removo o $inlinecount pq o ODataQuery não implementa 
            // var inlinecount = queryStringParts.FirstOrDefault(x => x.Key == "$inlinecount");
            // queryStringParts.Remove(inlinecount);

            // //Realiza a consulta
            // var dados = Session.ODataQuery<T>(queryStringParts, new ODataParserConfiguration { CaseSensitiveLike = false }).List<T>();

            // //Verifica se vai ter Count
            // var count = inlinecount.Value == "allpages";

            // if (!count) return new ODataResult<T>(dados);

            // //Adiciona o clausula $count e executa a query
            // queryStringParts.Add(new KeyValuePair<string, string>("$count", "true"));

            // //remove a clausula orderby para realizar o count
            // var orderby = queryStringParts.FirstOrDefault(x => x.Key == "$orderby");
            // queryStringParts.Remove(orderby);
            // var result = Session.ODataQuery<T>(queryStringParts, new ODataParserConfiguration { CaseSensitiveLike = false }).List();

            // return new ODataResult<T>(result.Count > 0 ? (int)result[0] : 0, dados);
        }

        private static IEnumerable<string> GetInclude(Type type)
        {
            var lista = new List<string>();

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                // Associações
                if (typeof(EntityBase).IsAssignableFrom(prop.PropertyType))
                    lista.Add(prop.Name);

                // Coleções
                if(IsCollection(prop))
                    lista.Add(prop.Name);
            }

            return lista.ToArray();
        }

        private static bool IsCollection(PropertyInfo prop)
        {
            var propType = prop.PropertyType;
            if (!propType.IsGenericType)
                return false;

            var genericType = propType.GetGenericTypeDefinition();
            return genericType == typeof(IList<>);
        }
    }
}