using System;

namespace SharedKernel.Domain.Entities
{
    public class Usuario : EntityBase, IAggregateRoot
    {               
        public string       Nome                                  { get; set; }
        public string       Login                                 { get; set; }
        public string       Senha                                 { get; set; }
        public string       Tema                                  { get; set; }
        public string       Email                                 { get; set; }
        public byte[]       Foto                                  { get; set; }
        public bool         Bloqueado                             { get; set; }
        public int          QtdeLoginsErradosParaBloquear         { get; set; }
        public int          QtdeLoginsErrados                     { get; set; }
        public int          QtdeConexoesSimultaneasPermitidas     { get; set; }
        public bool         ForcarTrocaDeSenha                    { get; set; }
        public int          IntervaloDiasParaTrocaDeSenha         { get; set; }
        public DateTime?    DataDaUltimaTrocaDeSenha              { get; set; }
        public DateTime?    DataDaProximaTrocaDeSenha             { get; set; }

        public Usuario()
        {
            Bloqueado = false;
            QtdeLoginsErradosParaBloquear = 3;
            QtdeLoginsErrados = 0;
            QtdeConexoesSimultaneasPermitidas = 1;
            ForcarTrocaDeSenha = false;
            IntervaloDiasParaTrocaDeSenha = 0;
        }
    }
}