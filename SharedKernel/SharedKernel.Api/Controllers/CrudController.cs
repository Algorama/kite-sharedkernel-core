using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.Validation;
using SharedKernel.Api.Security;
using SharedKernel.Api.Filters;

namespace SharedKernel.Api.Controllers
{
    [UserAuthorization]
    public class CrudController<T> : QueryController<T> where T : EntityBase, IAggregateRoot
    {
        protected new CrudService<T> Service { get; set; }

        public CrudController(CrudService<T> service) : base(service)
        {
            Service = service;
        }

        /// <summary>
        /// Incluí uma nova entidade (INSERT)
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <returns>Entidade inclusa</returns>
        [HttpPost]
        public virtual IActionResult Post([FromBody]T entity)
        {
            try
            {               
                var token = HttpContext.RecuperarToken();

                Service.Insert(entity, token?.Login ?? "");

                return CreatedAtAction(
                    "GetById",
                    new { id = entity.Id },
                    entity);
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Atualiza dados da entidade (UPDATE)
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="id"></param>
        /// <returns>Entidade atualizada</returns>
        [HttpPut("{id}")]
        public virtual IActionResult Put([FromBody]T entity, long id)
        {
            try
            {
                var fromDb = Service.Get(id);
                if (fromDb == null)
                    return NotFound(id);

                entity.Id = id;

                var token = HttpContext.RecuperarToken();
                Service.Update(entity, token?.Login);

                return Ok(entity);
            }
            catch (ValidatorException ex)
            {
                if (ex.Errors.FirstOrDefault()?.ErrorMessage == "Entity can not be null!")
                    return NotFound(entity.Id);

                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Excluí a entidade (DELETE)
        /// </summary>
        /// <param name="id">ID da Entidade</param>
        /// <returns>OK</returns>
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(long id)
        {
            try
            {
                Service.Delete(id);

                return Ok();
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}