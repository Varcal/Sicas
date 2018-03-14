using Application.Account.Contracts;
using Application.Account.Services;
using Application.Administrativo.Contracts;
using Application.Administrativo.Services;
using Application.Core.Contracts;
using Application.Core.Services;
using Application.Financeiro.Contracts;
using Application.Financeiro.Services;
using Application.Handlers;
using CrossCutting.Reports.ReportsGenerator;
using CrossCutting.Reports.ReportsGenerator.Interfaces;
using Domain.Account.Contracts.Repositories;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Contracts.Services;
using Domain.Administrativo.Services;
using Domain.Core.Contracts.Repositories;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Contracts.Services;
using Domain.Financeiro.Services;
using Infra.Data.Account.Repositories;
using Infra.Data.Administrativo.Repositories;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories;
using Infra.Data.Core.UnitOfWork;
using Infra.Data.Financeiro.Repositories;
using SharedKernel.DomainEvents.Events.DomainNotifications;
using SharedKernel.DomainEvents.Events.Emails;
using SharedKernel.DomainEvents.Handlers.Base;
using SharedKernel.DomainEvents.Handlers.DomainNotifications;
using SimpleInjector;
using CidadeApplicationService = Application.Administrativo.Services.CidadeApplicationService;
using EstadoApplicationService = Application.Administrativo.Services.EstadoApplicationService;


namespace CrossCutting.Ioc.Configurations
{
    internal class Bootstraper
    {
        internal static void RegisterServices(Container container)
        {

            #region Application Services
            
            RegistrarServicosAplicacaoAccount(container);
            RegistrarServicosAplicacaoCore(container);
            RegistrarServicosAplicacaoAdministrativo(container);
            RegistrarServicosAplicacaoFinanceiro(container);

            #endregion

            #region Domain Services

            RegistrarServicosAdministrativo(container);
            RegistrarServicosFinanceiro(container);

            #endregion

            #region Domain Events
            container.Register<Handler<DomainNotification>, DomainNotificationHandler>(Lifestyle.Scoped);
            container.Register<Handler<Email>,EmailHandler>(Lifestyle.Scoped);
            #endregion

            #region Repositories
    
            RegistrarRepositoriosAccount(container);
            RegistrarRepositoriosAdministrativo(container);           
            RegistrarRepositoriosFinanceiro(container);
            RegistrarRepositoriosCore(container);

            #endregion

            #region Reports

            RegistrarReports(container);
            #endregion

            #region Contexts
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<EfContext>(Lifestyle.Scoped);
            container.Register<AdoContext>(Lifestyle.Scoped);
            #endregion
        }

       

        #region Metodos Auxiliares

        #region Aplicacao
        private static void RegistrarServicosAplicacaoAdministrativo(Container container)
        {
            container.Register<ISeguradoraApplicationService, SeguradoraApplicationService>(Lifestyle.Scoped);
            container.Register<IEventoTipoApplicationService, EventoTipoApplicationService>(Lifestyle.Scoped);
            container.Register<IServicoSeguradoraApplicationService, ServicoSeguradoraApplicationService>(Lifestyle.Scoped);
            container.Register<ISindicanteApplicationService, SindicanteApplicationService>(Lifestyle.Scoped);
            container.Register<IBancoApplicantionService, BancoApplicationService>(Lifestyle.Scoped);
            container.Register<IServicoSindicanteApplicationService, ServicoSindicanteApplicationService>(Lifestyle.Scoped);
            container.Register<IProcessoApplicationService, ProcessoApplicationService>(Lifestyle.Scoped);
        }

        private static void RegistrarServicosAplicacaoCore(Container container)
        {
            container.Register<IEnderecoApplicationService, EnderecoApplicationService>(Lifestyle.Scoped);
            container.Register<ICidadeApplicationService, CidadeApplicationService>(Lifestyle.Scoped);
            container.Register<IEstadoApplicationService, EstadoApplicationService>(Lifestyle.Scoped);
            container.Register<IStatusApplicationService, StatusApplicationService>(Lifestyle.Scoped);
        }

        private static void RegistrarServicosAplicacaoAccount(Container container)
        {
            container.Register<IUsuarioApplicationService, UsuarioApplicationService>(Lifestyle.Scoped);
            container.Register<IPerfilApplicationService, PerfilApplicationService>(Lifestyle.Scoped);
        }

        private static void RegistrarServicosAplicacaoFinanceiro(Container container)
        {
            container.Register<IDespesaAppService, DespesaAppService>(Lifestyle.Scoped);
            container.Register<ILancamentoAppService, LancamentoAppService>(Lifestyle.Scoped);
            container.Register<IReciboSindicanteAppService, ReciboSindicanteAppService>(Lifestyle.Scoped);
        }
        #endregion

        #region Dominio
        private static void RegistrarServicosAdministrativo(Container container)
        {
            container.Register<IEnderecoDomainService, EnderecoDomainService>(Lifestyle.Scoped);
            container.Register<ISindicanteDomainService, SindicanteDomainService>(Lifestyle.Scoped);
            
            container.Register<IProcessoHistoricoDomainService, ProcessoHistoricoDomainService>(Lifestyle.Scoped);
        }

        private static void RegistrarServicosFinanceiro(Container container)
        {
            container.Register<IEnderecoDespesaDomainService, EnderecoDespesaDomainService>(Lifestyle.Scoped);
            container.Register<ILancamentoDomainService, LancamentoDomainService>(Lifestyle.Scoped);
        }
        
        #endregion

        #region Repositorio
        private static void RegistrarRepositoriosAccount(Container container)
        {
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
            container.Register<IPerfilRepository, PerfilRepository>(Lifestyle.Scoped);
        }

        private static void RegistrarRepositoriosFinanceiro(Container container)
        {
            container.Register<IDespesaRepository, DespesaRepository>(Lifestyle.Scoped);
            container.Register<ILancamentoRepository, LancamentoRepository>(Lifestyle.Scoped);
            container.Register<IReciboRepository, ReciboRepository>(Lifestyle.Scoped);
            container.Register<IDespesaSeguradoraRepository, DespesaSeguradoraRepository>(Lifestyle.Scoped);
            container.Register<IReciboSindicanteRepository, ReciboSindicanteRepository>(Lifestyle.Scoped);
        }

        private static void RegistrarRepositoriosAdministrativo(Container container)
        {
            container.Register<ISeguradoraRepository, SeguradoraRepository>(Lifestyle.Scoped);
            container.Register<IEventoTipoRepository, EventoTipoRepository>(Lifestyle.Scoped);
            container.Register<IServicoSeguradoraRepository, ServicoSeguradoraRepository>(Lifestyle.Scoped);
            container.Register<ISindicanteRepository, SindicanteRepository>(Lifestyle.Scoped);
            container.Register<IBancoRepository, BancoRepository>(Lifestyle.Scoped);
            container.Register<IServicoSindicanteRepository, ServicoSindicanteRepository>(Lifestyle.Scoped);
            container.Register<IProcessoRepository, ProcessoRepository>(Lifestyle.Scoped);
            container.Register<IProcessoHstoricoRepository, ProcessoHistoricoRepository>(Lifestyle.Scoped);
            container.Register<IEventoRepository, EventoRepository>(Lifestyle.Scoped);

        }

        private static void RegistrarRepositoriosCore(Container container)
        {
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
            container.Register<ICidadeRepository, CidadeRepository>(Lifestyle.Scoped);
            container.Register<IEstadoRepository, EstadoRepository>(Lifestyle.Scoped);
        }
        #endregion

        #region Reports

        private static void RegistrarReports(Container container)
        {
            container.Register<IDespesaSeguradoraGeradorRelatorio, DespesaSeguradoraGeradorRelatorio>(Lifestyle.Scoped);
            container.Register<IReciboSindicanteGeradorRelatorio, ReciboSindicanteGeradorRelatorio>(Lifestyle.Scoped);
        }
        #endregion

        #endregion
    }
}
