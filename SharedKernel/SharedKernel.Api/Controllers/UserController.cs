using Microsoft.AspNetCore.Mvc;
using SharedKernel.DependencyInjector;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Services;

namespace SharedKernel.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Recurso para Autenticar Usuários da Aplicação
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CrudController<User>
    {
        public new UserService Service { get; set; }

        public UserController()
        {
            Service = Kernel.Get<UserService>();
        }
    }
}