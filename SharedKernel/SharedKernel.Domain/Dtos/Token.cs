using System;

namespace SharedKernel.Domain.Dtos
{
    public class Token
    {
        public long     UsuarioId       { get; set; }
        public string   UsuarioNome     { get; set; }
        public string   Login           { get; set; }
        public DateTime DataExpiracao   { get; set; }
        public bool     DeveTrocarSenha { get; set; }
    }
}