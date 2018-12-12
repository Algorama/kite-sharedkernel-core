using System;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.Validation;
using SharedKernel.Api.Security;
using SharedKernel.DependencyInjector;

namespace SharedKernel.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Recurso para Autenticar Usuários da Aplicação
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        /// <summary>
        /// Recurso para Autenticar Usuários da Aplicação
        /// </summary>
        public TokenController()
	    {
	        _usuarioService = Kernel.Get<UsuarioService>();
        }

        /// <summary>
        /// Efetua a Autenticação do Usuário
        /// </summary>
        /// <param name="login">Login e Senha do Usuário para a Autenticação</param>
        /// <returns>Token de Autenticação à ser utilizado nas requisições privadas</returns>
        [HttpPost]        
        public IActionResult Post([FromBody]LoginRequest login)
        {
            try
            {
                var usuario = _usuarioService.Login(login);
                if (usuario == null) return Unauthorized();
                if (usuario.DeveTrocarSenha) return BadRequest("Troca de Senha Obrigatória!");

                var token = usuario.GerarTokenString();
                return Ok(new { Token = token });
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
        /// Troca de Senha Obrigatória de Usuário com senha expirada
        /// </summary>
        /// <param name="changePasswordRequest">Informações para a troca de senha obrigatória</param>
        /// <returns>Ok</returns>
        [HttpPost]
        [Route("TrocaSenha")]
        public ActionResult PostTrocaSenha([FromBody]ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                _usuarioService.TrocaSenha(changePasswordRequest);

                var login = new LoginRequest
                {
                    Login = changePasswordRequest.Login,
                    Password = changePasswordRequest.NewPassword
                };

                var usuario = _usuarioService.Login(login);
                if (usuario == null) return Unauthorized();

                var token = usuario.GerarTokenString();
                return Ok(new { Token = token });
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