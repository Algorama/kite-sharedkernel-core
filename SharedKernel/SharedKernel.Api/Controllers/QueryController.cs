using System;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Api.Controllers
{
    public class QueryController<T> : Controller where T : EntityBase
    {
        protected IQueryService<T> Service { get; set; }

        public QueryController(IQueryService<T> service)
        {
            Service = service;
        }

        /// <summary>
        /// Retorna uma entidade dado o seu ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade</returns>
        public virtual IActionResult Get(long id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Informe o ID");

                var result = Service.Get(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Retorna todas as entidades
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public virtual IActionResult Get()
        {
            try
            {
                var result = Service.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }        

        // /// <summary>
        // /// Retorna uma lista paginada de entidades
        // /// </summary>
        // /// <param name="page">Número da Página Solicitada</param>
        // /// <param name="size">Tamanho da Página</param>
        // /// <returns>Lista Paginada de Entidades</returns>
        // public virtual IActionResult GetPage(int page, int size)
        // {
        //     try
        //     {
        //         if (!Enum.IsDefined(typeof(PageSize), size))
        //             throw new Exception("Tamanho de Página Inválido");

        //         var result = Service.GetPaged(page, (PageSize)size);

        //         if (result == null) return NotFound();

        //         var helper = new UrlHelper(Request);

        //         result.PreviousPage = page > 1 
        //             ? helper.Link("DefaultApi", new { page = page - 1, size }) 
        //             : string.Empty;

        //         result.NextPage = page < result.TotalPages 
        //             ? helper.Link("DefaultApi", new { page = page + 1, size }) 
        //             : string.Empty;

        //         return Ok(result);
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         throw;
        //     }
        // }

        // /// <summary>
        // /// Retorna uma Lista ODATA de Entidades
        // /// </summary>
        // /// <param name="queryOptions">Parametros do ODATA</param>
        // /// <returns>Lista ODATA de Entidades</returns>
        // public virtual IHttpActionResult GetOData(ODataQueryOptions<T> queryOptions)
        // {
        //     try
        //     {
        //         var queryStringParts = ODataParse.RetonaQueryStringParts(queryOptions);
        //         var result = Service.GetOData(queryStringParts);
        //         return Ok(result);
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         throw;
        //     }
        // }
    }
}