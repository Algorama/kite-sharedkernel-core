using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
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
            return Entities.Find(id);
            //return Entities.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate).AsQueryable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] include)
        {
            IQueryable<T> result = Entities.Where(predicate);
            
            foreach (string item in include)
                result = result.Include(item).AsQueryable();
            
            return result.AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
            //return Entities.AsNoTracking();
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
    }
}