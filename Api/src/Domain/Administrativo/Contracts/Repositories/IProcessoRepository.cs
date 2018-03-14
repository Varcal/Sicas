using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Domain.Account.Entities;
using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories.Base;
using Domain.Dtos;

namespace Domain.Administrativo.Contracts.Repositories
{
    public interface IProcessoRepository: IRepositoryBase<Processo>
    {
        IEnumerable<Processo> SelecionarTodos();
        IEnumerable<Processo> SelecionarTodos(Perfil perfil, [Optional] int? usuarioId);
        IEnumerable<Processo> SelecionarProcessos(int seguradoraId, int statusId, Perfil perfil, [Optional]int? usuarioResponsaselId);
        IEnumerable<Processo> SelecionarPorSeguradoraCombo(int seguradoraId);     
        Processo ObterPorId(int id);
        Processo ObterParaParecer(int id);
        Processo SelecionarComHistorico(int seguradoraId, string numeroSinistro);
        Processo SelecionarComHistorico(int seguradoraId, string numeroSinistro, int usuarioId);
        IEnumerable<Processo> SelecionarPorSeguradoraParaDespesas(int? seguradoraId);
        Processo ObterParaDespesa(int id);
        IEnumerable<Processo> SelecionarPorSeguradoraData(int seguradoraId, DateTime dataInicio, DateTime dataFim);
        Processo SelecionarProcessoParaRecibo(int processoId);
        IEnumerable<Processo> SelecionarPorSeguradoraFinanceiro(int seguradoraId);
        Processo SelecionarComHistoricoFinanceiro(int seguradoraId, string numeroSinistro);
        Acionamento ObterAcionamento(int id);
        Processo ObterPorIdComAcionamento(int id);
        AlertaDto ObterAlerta(int? usuarioId);
        AlertaFinanceiroDto ObterAlertaFinanceiro();
        IEnumerable<SindicanteHomeDto> SelecionarSindicantesStatusMensal();
        IEnumerable<Processo> SelecionarPorAlerta(int alerta, Perfil perfil, [Optional]int? usuarioId);
    }
}
