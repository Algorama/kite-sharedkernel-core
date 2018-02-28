using System;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.Validation;
using SharedKernel.Api.Security;
using SharedKernel.Api.Filters;
using SharedKernel.DependencyInjector;

namespace SharedKernel.Api.Controllers
{
    [UserAuthorization]
    public class CrudController<T> : QueryController<T> where T : EntityBase, IAggregateRoot
    {
        protected new ICrudService<T> Service { get; set; }

        public CrudController()
        {
            Service = Kernel.Get<ICrudService<T>>();
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
        [HttpPut]
        public virtual IActionResult Put([FromBody]T entity)
        {
            try
            {
                var token = HttpContext.RecuperarToken();
                Service.Update(entity, token?.Login);

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
        [HttpDelete]
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