using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; 
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework
{
    public class QueryRepository<T> : IQueryRepository<T> where T : EntityBase
    {
        protected DatabaseContext Context { get; set; }
        protected DbSet<T> Entities {get; set; }

        public QueryRepository(DatabaseContext context)
        {
            Context  = context;
            Entities = Context.Set<T>(); 
        }

        public T Get(long id)
        {            
            return Entities.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return Entities.AsNoTracking();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> @where)
        {
            return Entities.AsNoTracking().Where(@where);
        }

        public IList<T> GetByHql(string comandoHql)
        {
            throw new NotImplementedException();
            //return Session.CreateQuery(comandoHql).List<T>().ToList();
        }

        public IList<T> GetBySql(string sql)
        {
            throw new NotImplementedException();
            // var result = Session.CreateSQLQuery(sql).AddEntity(typeof(T));
            // var res = result.List<T>().ToList();
            // return res;
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