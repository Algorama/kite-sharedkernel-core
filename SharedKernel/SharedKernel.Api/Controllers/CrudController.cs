using System;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.ValueObjects;
using SharedKernel.Domain.Validation;
using SharedKernel.Api.Security;
using SharedKernel.Api.Filters;

namespace SharedKernel.Api.Controllers
{
    [UserAuthorization]
    public class CrudController<T> : QueryController<T> where T : EntityBase, IAggregateRoot
    {
        protected new ICrudService<T> Service { get; set; }

        public CrudController(ICrudService<T> service) : base(service)
        {
            Service = service;
        }

        /// <summary>
        /// Incluí uma nova entidade (INSERT)
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <returns>Entidade inclusa</returns>
        public virtual IActionResult Post([FromBody]T entity)
        {
            try
            {               
                var token = HttpContext.RecuperarToken();

                Service.Insert(entity, token?.Login ?? "");
                
                // TODO: Recuperar UrlHelper
                // var helper = new UrlHelper(Request);
                // var location = helper.Link("DefaultApi", new { id = entity.Id });

                // return Created(location, entity);

                return Ok(entity);
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Atualiza dados da entidade (UPDATE)
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <returns>Entidade atualizada</returns>
        public virtual IActionResult Put([FromBody]T entity)
        {
            try
            {
                var existe = Service.Get(entity.Id) != null;
                if (existe == false)
                    return NotFound();

                var token = HttpContext.RecuperarToken();
                Service.Update(entity, token?.Login);

                entity = Service.Get(entity.Id);

                return Ok(entity);
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Excluí a entidade (DELETE)
        /// </summary>
        /// <param name="id">ID da Entidade</param>
        /// <returns>OK</returns>
        public virtual IActionResult Delete(long id)
        {
            try
            {
                var entity = Service.Get(id);
                if (entity == null)
                    return NotFound();

                Service.Delete(entity);

                return Ok();
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}