using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;
using SharedKernel.Api.Filters;
using SharedKernel.DependencyInjector;

namespace SharedKernel.Api.Controllers
{
    [UserAuthorization]
    public class QueryController<T> : Controller where T : EntityBase
    {
        protected IQueryService<T> Service { get; set; }

        public QueryController()
        {
            Service = Kernel.Get<IQueryService<T>>();
        }

        /// <summary>
        /// Retorna uma entidade dado o seu ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade</returns>
        [HttpGet]
        [Route("{id}")]
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
        [HttpGet]
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

        [HttpGet]
        [Route("odata")]
        [EnableQuery]
        public IActionResult GetOdata()
        {
            return Ok(Service.GetAll());
        }
    }
}