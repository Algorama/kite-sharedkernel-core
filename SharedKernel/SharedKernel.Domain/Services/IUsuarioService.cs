using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Services
{
    public interface IUsuarioService : ICrudService<Usuario>
    {
        Token Login(LoginRequest loginRequest);
        void TrocaSenha(ChangePasswordRequest changePasswordRequest);
        string GetTema(long usuarioId);
        void ChangeTema(long usuarioId, string newTema);
        void UpdatePerfil(Usuario entity, string user);
    }
}