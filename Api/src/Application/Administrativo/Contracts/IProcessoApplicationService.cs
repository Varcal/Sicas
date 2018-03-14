using System;
using System.Collections.Generic;
using Application.Administrativo.ViewModels.Processos;
using Application.Administrativo.ViewModels.Sindicantes;

namespace Application.Administrativo.Contracts
{
    public interface IProcessoApplicationService
    {
        void Registrar(ProcessoCreateVm model, string usuarioLogado);
        IEnumerable<ProcessoListVm> SelecionarPorSeguradora(int seguradoraId, int statusId, int alerta, int usuarioId);
        ProcessoEditVm ObterPorId(int id);
        ProcessoParecerVm ObterParaParecer(int id);
        void Editar(ProcessoEditVm processo, string name);
        IEnumerable<ProcessoHistoricoVm> SelecionarHistoricos(int seguradoraId, string numeroSinistro, int usuarioId);
        void FinalizarParecer(ProcessoParecerVm model, string usuarioLogado);
        void FinalizarAnalise(int id, string usuarioLogado);
        void FinalizarDespesas(int id, string usuarioLogado);     
        ArquivoVm BaixarAquivo(int id);
        void CancelarProcesso(int id, string usuarioLogado);
        void EnviarSindicanteExterno(int id, int sindicanteExternoId, string usuarioLogado);
        IEnumerable<ProcessoListVm> SelecionarPorSeguradoraParaDespesas(int? seguradoraId);
        IEnumerable<ProcessoListVm> SelecionarPorSeguradoraData(int seguradoraId, DateTime dataInicio, DateTime dataFim);
        void Finalizar(ProcessoFinalizarVm processoFinalizar, string usuarioLogado);
        IEnumerable<ProcessoListVm> SelecionarPorSeguradorasAtivas(int seguradoraId);
        AcionamentoVm BaixarAcionamento(int id);
        AlertaVm ObterAlerta(int usuarioId);
        IEnumerable<SindicateHomeVm> SindicantesProcesosMes();
    }
}
