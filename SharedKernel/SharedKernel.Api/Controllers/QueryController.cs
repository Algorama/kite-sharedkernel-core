using System;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;
using SharedKernel.Api.Filters;
using SharedKernel.DependencyInjector;

namespace SharedKernel.Api.Controllers
{
    [UserAuthorization]
    public class QueryController<T> : ControllerBase where T : EntityBase
    {
        protected QueryService<T> Service { get; set; }

        public QueryController()
        {
            Service = Kernel.Get<QueryService<T>>();
        }

        /// <summary>
        /// Retorna uma entidade dado o seu ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade</returns>
        [HttpGet("{id}")]
        public virtual IActionResult GetById(long id)
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
                return StatusCode(500, ex);
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
                return StatusCode(500, ex);
            }
        }
    }
}