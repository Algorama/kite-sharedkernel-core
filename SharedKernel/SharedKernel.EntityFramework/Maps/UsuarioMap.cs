using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Entities;

namespace SharedKernel.EntityFramework.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> map)
        {
            map.HasKey(x => x.Id)
                .HasName("pk_usuario");
            map.Property(x => x.Id)
                .UseSqlServerIdentityColumn();

            map.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            map.Property(x => x.Login).HasMaxLength(100).IsRequired();
            map.HasIndex(x => x.Login).IsUnique();
            map.Property(x => x.Senha).HasMaxLength(32).IsRequired();
            map.Property(x => x.Tema).HasMaxLength(50);
            map.Property(x => x.Email).HasMaxLength(256);
            map.Property(x => x.Foto);
            map.Property(x => x.Bloqueado);
            map.Property(x => x.QtdeLoginsErradosParaBloquear);
            map.Property(x => x.QtdeLoginsErrados);
            map.Property(x => x.QtdeConexoesSimultaneasPermitidas);
            map.Property(x => x.ForcarTrocaDeSenha);
            map.Property(x => x.IntervaloDiasParaTrocaDeSenha);
            map.Property(x => x.DataDaUltimaTrocaDeSenha);
            map.Property(x => x.DataDaProximaTrocaDeSenha);
        }
    }
}