using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Helpers;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Validation;
using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Domain.Services
{
    public class UsuarioService : CrudService<Usuario>, IUsuarioService
    {
        public UsuarioService(IRepository<Usuario> repository, UsuarioValidator validator) : base(repository, validator)
        {
            validator.Service = this;
        }

        public override Usuario Get(long id)
        {
            var usuario = base.Get(id);
            usuario.Senha = null;
            usuario.Foto = null;
            return usuario;
        }

        public override PageResult<Usuario> GetPaged(int page, PageSize size)
        {
            var usuarios = base.GetPaged(page, size);
            foreach (var usuario in usuarios.Data)
            {
                usuario.Senha = null;
                usuario.Foto = null;
            }
            return usuarios;
        }

        public override void Insert(Usuario entidade, string user = "sistema")
        {
            entidade.Senha = CryptoTools.ComputeHashMd5(entidade.Senha);
            DefineRegrasParaTrocaSenha(entidade);
            base.Insert(entidade, user);
        }

        public override void Update(Usuario entidade, string user = "sistema")
        {
            var usuarioOld = base.Get(entidade.Id);
            entidade.Senha = usuarioOld.Senha;
            entidade.Foto = usuarioOld.Foto;
            DefineRegrasParaTrocaSenha(entidade);
            base.Update(entidade, user);
        }

        public void UpdatePerfil(Usuario entidade, string user = "sistema")
        {
            var usuarioOld = base.Get(entidade.Id);
            entidade.Senha = usuarioOld.Senha;
            base.Update(entidade, user);
        }

        private static void DefineRegrasParaTrocaSenha(Usuario entidade)
        {
            if (entidade.ForcarTrocaDeSenha)
            {
                if (entidade.DataDaUltimaTrocaDeSenha == null)
                    entidade.DataDaUltimaTrocaDeSenha = DateTime.Today;

                entidade.DataDaProximaTrocaDeSenha = entidade.IntervaloDiasParaTrocaDeSenha > 0
                    ? entidade.DataDaUltimaTrocaDeSenha.Value.AddDays(entidade.IntervaloDiasParaTrocaDeSenha)
                    : DateTime.Today;
            }
            else
            {
                entidade.DataDaProximaTrocaDeSenha = null;
            }
        }

        public Token Login(LoginRequest loginRequest)
        {
            var senha = CryptoTools.ComputeHashMd5(loginRequest.Password);
            var usuario = GetAll(x => 
                x.Login.ToUpper() == loginRequest.Login.ToUpper() && x.Bloqueado == false).FirstOrDefault();

            if (usuario == null) return null;

            if (usuario.Senha == senha)
            {
                usuario.QtdeLoginsErrados = 0;
                Update(usuario);

                var token = new Token
                {
                    UsuarioId = usuario.Id,
                    UsuarioNome = usuario.Nome,
                    Login = usuario.Login,
                    DataExpiracao = DateTime.Now.AddHours(12)
                };
                return token;
            }

            usuario.QtdeLoginsErrados++;

            if (usuario.QtdeLoginsErrados >= usuario.QtdeLoginsErradosParaBloquear)
                usuario.Bloqueado = true;

            Update(usuario);

            return null;
        }

        public void TrocaSenha(ChangePasswordRequest changePasswordRequest)
        {
            var senhaAntiga = CryptoTools.ComputeHashMd5(changePasswordRequest.OldPassword);
            var usuario = GetAll(x =>
                x.Login.ToUpper() == changePasswordRequest.Login.ToUpper() &&
                x.Senha == senhaAntiga).FirstOrDefault();

            if (usuario == null)
                throw new ValidationException("Senha antiga não confere");

            usuario.Senha = CryptoTools.ComputeHashMd5(changePasswordRequest.NewPassword);
            usuario.DataDaUltimaTrocaDeSenha = DateTime.Today;
            
            if (usuario.ForcarTrocaDeSenha)
            {
                if (usuario.IntervaloDiasParaTrocaDeSenha > 0)
                    usuario.DataDaProximaTrocaDeSenha = usuario.DataDaUltimaTrocaDeSenha.Value.AddDays(usuario.IntervaloDiasParaTrocaDeSenha);
                else
                {
                    usuario.ForcarTrocaDeSenha = false;
                    usuario.DataDaProximaTrocaDeSenha = null;
                }
            }

            base.Update(usuario);
        }

        public string GetTema(long usuarioId)
        {
            var usuario = Get(usuarioId);
            if(usuario == null)
                throw new ValidationException("Usuário Inválido!");

            return usuario.Tema;
        }

        public void ChangeTema(long usuarioId, string newTema)
        {
            if(string.IsNullOrEmpty(newTema))
                throw new ValidationException("Tema Inválido!");

            var usuario = Get(usuarioId);
            if (usuario == null)
                throw new ValidationException("Usuário Inválido!");

            usuario.Tema = newTema;

            Update(usuario, "ChangeTema");
        }
    }
}