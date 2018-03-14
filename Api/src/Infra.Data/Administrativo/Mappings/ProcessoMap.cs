using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class ProcessoMap: BaseMap<Processo>
    {
        public ProcessoMap()
        {
            ToTable("Processo");

            HasRequired(p => p.Seguradora)
                .WithMany()
                .HasForeignKey(fk => fk.SeguradoraId);

            HasRequired(p => p.Cidade)
                .WithMany()
                .HasForeignKey(fk => fk.CidadeId);

            HasRequired(p => p.Segurado)
                .WithMany()
                .HasForeignKey(fk => fk.SeguradoId);

            //HasOptional(p => p.LocalFatos)
            //    .WithMany()
            //    .HasForeignKey(fk => fk.LocalFatosId);

            HasRequired(p => p.ServicoSeguradora)
                .WithMany()
                .HasForeignKey(fk => fk.ServicoSeguradoraId);

            HasRequired(p => p.EventoTipo)
               .WithMany()
               .HasForeignKey(fk => fk.EventoTipoId);

            HasRequired(p => p.ServicoSindicante)
               .WithMany()
               .HasForeignKey(fk => fk.ServicoSindicanteId);

            HasRequired(p => p.Status)
               .WithMany()
               .HasForeignKey(fk => fk.StatusId);

            HasOptional(p => p.UsuarioResponsavel)
                .WithMany()
                .HasForeignKey(fk => fk.UsuarioResponsavelId);

            HasOptional(p => p.Acionamento)
                .WithMany()
                .HasForeignKey(fk => fk.AcionamentoId);

            HasMany(p => p.Sindicantes)
                .WithMany(p=>p.Processos)
                .Map(m =>
                {
                    m.ToTable("ProcessoSindicante");
                    m.MapLeftKey("ProcessoId");
                    m.MapRightKey("SindicanteId");
                });

            HasMany(p => p.Historicos)
                .WithRequired()
                .HasForeignKey(fk => fk.ProcessoId);

            HasMany(p => p.Despesas)
                .WithRequired()
                .HasForeignKey(fk => fk.ProcessoId);


            Property(p => p.EventoTipoId)
                .HasColumnName("EventoTipoId")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.NumeroReferencia)
                .HasColumnName("NumeroReferencia")
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsOptional();
            Property(p => p.NumeroSinistro)
                .HasColumnName("NumeroSinistro")
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();
            Property(p => p.Analista)
                .HasColumnName("Analista")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
            Property(p => p.Placa)
                .HasColumnName("Placa")
                .HasColumnType("char")
                .HasMaxLength(8)
                .IsOptional();
            Property(p => p.SeguradoraId)
                .HasColumnName("SeguradoraId")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.ServicoSeguradoraId)
                .HasColumnName("SeguradoraServicoId")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.UsuarioResponsavelId)
                .HasColumnName("UsuarioResponsavelId")
                .HasColumnType("int")
                .IsOptional();
            Property(p => p.ServicoSindicanteId)
                .HasColumnName("SindicanteServicoId")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.Situacao)
                .HasColumnName("Situacao")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.StatusId)
                .HasColumnName("Status")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.FinalizadaAnaliseEm)
                .HasColumnName("FinalizadaAnaliseEm")
                .HasColumnType("datetime2")
                .HasPrecision(3)
                .IsOptional();
            Property(p => p.EmitidoParecerEm)
                .HasColumnName("EmitidoParecerEm")
                .HasColumnType("datetime2")
                .HasPrecision(3)
                .IsOptional();
            Property(p => p.FinalizadoEm)
                .HasColumnName("FinalizadoEm")
                .HasColumnType("datetime2")
                .HasPrecision(3)
                .IsOptional();
            Property(p => p.Comentario)
                .HasMaxLength(500)
                .IsOptional();
            Property(p => p.NumeroNF)
                .HasColumnName("NumeroNF")
                .HasColumnType("int")
                .IsOptional();
            Property(p => p.DataEmissaoNF)
                .HasColumnName("DataEmissaoNF")
                .HasColumnType("datetime2")
                .HasPrecision(3)
                .IsOptional();         
        }
    }
}
