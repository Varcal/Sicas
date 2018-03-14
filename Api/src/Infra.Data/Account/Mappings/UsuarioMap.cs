using Domain.Account.Entities;
using Infra.Data.Core.Extensions;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Account.Mappings
{
    public class UsuarioMap : BaseMap<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");

            HasMany(p => p.Perfis)
                .WithMany(p => p.Usuarios)
                .Map(m=>
                {
                    m.ToTable("UsuarioPerfil");
                    m.MapLeftKey("UsuarioId");
                    m.MapRightKey("PerfilId");
                });

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsRequired();
            Property(p => p.Login)
                .HasColumnName("Login")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .HasUniqueIndexAnnotation("IX_Login_Unique")
                .IsRequired();
            Property(p => p.Senha)
                .HasColumnName("Senha")
                .HasColumnType("varchar")
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
