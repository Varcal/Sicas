using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain.Account.Entities;
using Domain.Administrativo.Entities;
using Domain.Core.Entities.DadosCadastros;
using Domain.Core.Entities.Enderecos;
using Domain.Financeiro.Entities.DespesasSeguradora;
using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Account.Mappings;
using Infra.Data.Administrativo.Mappings;
using Infra.Data.Core.Mappings.DadosCadastros;
using Infra.Data.Core.Mappings.Enderecos;
using Infra.Data.Core.Mappings.Logs;
using Infra.Data.Financeiro.Mappings;
using TrackerEnabledDbContext;
using TrackerEnabledDbContext.Common.Configuration;

namespace Infra.Data.Contexts
{
    public class EfContext: TrackerContext
    {
        public EfContext(): base("Name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<EfContext>(null);
        }


        #region DBSets

        #region Account
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        #endregion

        #region Administrativo
        public DbSet<EnderecoSindicante> EnderecosSindicantes { get; set; }
        public DbSet<Seguradora> Seguradoras { get; set; }
        public DbSet<ServicoSeguradora> ServicosSeguradoras { get; set; }
        public DbSet<EventoTipo> EventosTipos { get; set; }
        public DbSet<Sindicante> Sindicantes { get; set; }
        public DbSet<SindicanteInterno> SindicantesInternos { get; set; }
        public DbSet<Honorario> Honorarios { get; set; }
        public DbSet<ServicoSindicante> ServicosSindicantes { get; set; }
        public DbSet<SindicanteTipo> SindicantesTipos { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<DadosBancarios> DadosBancarios { get; set; }
        public DbSet<Processo> Processos { get; set; }
        public DbSet<ProcessoHistorico> ProcessosHistoricos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        #endregion

        #region Financeiro

        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<DespesaAdicional> DespesasAdicionais { get; set; }
        public DbSet<EnderecoDespesa> EnderecosDespesas { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<Recibo> Recibos { get; set; }
        public DbSet<DespesaSeguradora> DespesasSeguradoras { get; set; }
        public DbSet<ReciboSindicante> RecibosSindicantes { get; set; }

        #endregion

        #region Base

        #region Enderecos
        public DbSet<EnderecoTipo> EnderecosTipos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        #endregion

        #region DadosCadastros
        public DbSet<DadosCadastrais> DadosCadastros { get; set; }
        public DbSet<DadosPessoaFisica> PessoaFisicas { get; set; }
        public DbSet<DadosPessoaJuridica> PessoaJuridicas { get; set; }
        #endregion

        #endregion

        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(x=>x.HasColumnType("varchar").HasMaxLength(50));

            AddMaps(modelBuilder);
            LogsConfig();
        }


        #region Metodos Auxiliares
        private static void AddMaps(DbModelBuilder modelBuilder)
        {

            #region Core

            #region Enderecos
            modelBuilder.Configurations.Add(new EnderecoTipoMap());
            modelBuilder.Configurations.Add(new EnderecoMap());
            modelBuilder.Configurations.Add(new EnderecoSindicanteMap());
            modelBuilder.Configurations.Add(new PaisMap());
            modelBuilder.Configurations.Add(new EstadoMap());
            modelBuilder.Configurations.Add(new CidadeMap());
            #endregion

            #region DadosCadastros

            modelBuilder.Configurations.Add(new DadosCadastraisMap());
            modelBuilder.Configurations.Add(new DadosPessoaFisicaMap());
            modelBuilder.Configurations.Add(new DadosPessoaJuridicaMap());

            #endregion]

            modelBuilder.Configurations.Add(new AuditLogMap());
            modelBuilder.Configurations.Add(new AuditLogDetailMap());
            modelBuilder.Configurations.Add(new LogMetaDataMap());
            #endregion

            #region Administrativo
            modelBuilder.Configurations.Add(new SeguradoraMap());
            modelBuilder.Configurations.Add(new ServicoSeguradoraMap());
            modelBuilder.Configurations.Add(new EventoTipoMap());
            modelBuilder.Configurations.Add(new EventoMap());
            modelBuilder.Configurations.Add(new SindicanteMap());
            modelBuilder.Configurations.Add(new HonorarioMap());
            modelBuilder.Configurations.Add(new ServicoSindicanteMap());
            modelBuilder.Configurations.Add(new SindicanteTipoMap());
            modelBuilder.Configurations.Add(new SindicanteInternoMap());
            modelBuilder.Configurations.Add(new ProcessoMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new BancoMap());
            modelBuilder.Configurations.Add(new DadosBancariosMap());
            modelBuilder.Configurations.Add(new ProcessoHistoricoMap());
            modelBuilder.Configurations.Add(new EnderecoSeguradoMap());
            modelBuilder.Configurations.Add(new SeguradoMap());
            modelBuilder.Configurations.Add(new LocalFatosMap());
            modelBuilder.Configurations.Add(new AcionamentoMap());
            #endregion

            #region Account
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            #endregion

            #region Financeiro

            modelBuilder.Configurations.Add(new DespesaMap());
            modelBuilder.Configurations.Add(new DespesaAdicionalMap());
            modelBuilder.Configurations.Add(new EnderecoDespesaMap());
            modelBuilder.Configurations.Add(new LancamentoMap());
            modelBuilder.Configurations.Add(new LancamentoDespesaMap());
            modelBuilder.Configurations.Add(new ReciboMap());
            modelBuilder.Configurations.Add(new DespesaSeguradoraMap());
            modelBuilder.Configurations.Add(new DespesaSeguradoraItemMap());
            modelBuilder.Configurations.Add(new ReciboSindicanteItemMap());
            modelBuilder.Configurations.Add(new ReciboSindicanteMap());

            #endregion

        }

        private static void LogsConfig()
        {
            #region Administrativo

            EntityTracker.TrackAllProperties<Banco>();
            EntityTracker.TrackAllProperties<DadosBancarios>();
            EntityTracker.TrackAllProperties<Honorario>();
            EntityTracker.TrackAllProperties<Processo>();
            EntityTracker.TrackAllProperties<ProcessoHistorico>();
            EntityTracker.TrackAllProperties<Segurado>();
            EntityTracker.TrackAllProperties<Seguradora>();
            EntityTracker.TrackAllProperties<ServicoSeguradora>();
            EntityTracker.TrackAllProperties<ServicoSindicante>();
            EntityTracker.TrackAllProperties<Sindicante>();
            EntityTracker.TrackAllProperties<SindicanteInterno>();

            #endregion

            #region Financeiro

            EntityTracker.TrackAllProperties<Despesa>();
            EntityTracker.TrackAllProperties<DespesaAdicional>();
            EntityTracker.TrackAllProperties<DespesaSeguradora>();
            EntityTracker.TrackAllProperties<DespesaSeguradoraItem>();
            EntityTracker.TrackAllProperties<EnderecoDespesa>();

            EntityTracker.TrackAllProperties<Lancamento>();
            EntityTracker.TrackAllProperties<LancamentoDespesa>();
            EntityTracker.TrackAllProperties<Recibo>();
            EntityTracker.TrackAllProperties<ReciboSindicante>();
            EntityTracker.TrackAllProperties<ReciboSindicanteItem>();

            #endregion

            #region Account
            EntityTracker.TrackAllProperties<Usuario>();
            EntityTracker.TrackAllProperties<Perfil>();
            #endregion

            #region Core

            EntityTracker.TrackAllProperties<DadosCadastrais>();
            EntityTracker.TrackAllProperties<DadosPessoaFisica>();
            EntityTracker.TrackAllProperties<DadosPessoaJuridica>();


            #region Enderecos
            EntityTracker.TrackAllProperties<Endereco>();
            EntityTracker.TrackAllProperties<EnderecoTipo>();
            EntityTracker.TrackAllProperties<EnderecoSindicante>(); 
            #endregion

            #endregion
        } 
        #endregion
    }
}
