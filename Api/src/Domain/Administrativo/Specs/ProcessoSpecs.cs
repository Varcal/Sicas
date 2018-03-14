using System;
using System.Linq.Expressions;
using Domain.Administrativo.Entities;
using SharedKernel.Enums;

namespace Domain.Administrativo.Specs
{
    public static class ProcessoSpecs
    {
        public static Expression<Func<Processo, bool>> SelecionarTodosParaEmitirNf()
        {
            return x => (x.StatusId == (int)StatusEnum.AguardandoEmissaoNf || x.StatusId == (int)StatusEnum.EnviadoSeguradora || x.StatusId == (int)StatusEnum.Finalizado || x.StatusId == (int)StatusEnum.EnviadoFinanceiro);
        }

        public static Expression<Func<Processo, bool>> SelecionarPorId(int id)
        {
            return x => x.Id == id;
        }

        public static Expression<Func<Processo, bool>> SelecionarPorSeguradoraIdNumeroSinistro(int seguradoraId, string numeroSinistro)
        {
            return x => x.SeguradoraId == seguradoraId && x.NumeroSinistro == numeroSinistro;
        }

        public static Expression<Func<Processo, bool>> SelecionarPorIdSinistroUsuario(int seguradoraId, string numeroSinistro, int usuarioId)
        {
            return x => x.SeguradoraId == seguradoraId && x.NumeroSinistro == numeroSinistro && x.UsuarioResponsavelId == usuarioId;
        }

        public static Expression<Func<Processo, bool>> SelecionarPorSeguradoraData(int seguradoraId, DateTime datainicio, DateTime dataFim)
        {
            return
                x =>
                    x.SeguradoraId == seguradoraId &&
                    (x.EmitidoParecerEm.Value >= datainicio && x.EmitidoParecerEm.Value <= dataFim) &&
                    (x.StatusId == (int)StatusEnum.AguardandoEmissaoNf || x.StatusId == (int)StatusEnum.EnviadoSeguradora || x.StatusId == (int)StatusEnum.Finalizado);
        }


        public static Expression<Func<Processo, bool>> AbertoMais7Dias(int? usuarioId)
        {
            var data = DateTime.Now.AddDays(-7);

            if (!usuarioId.HasValue)
            {
                return x => x.DataCadastro < data && x.FinalizadaAnaliseEm == null && x.StatusId != (int)StatusEnum.Cancelado;
            }

            return x => x.UsuarioResponsavelId == usuarioId && x.DataCadastro < data && x.FinalizadaAnaliseEm == null && x.StatusId != (int)StatusEnum.Cancelado;
        }

        public static Expression<Func<Processo, bool>> AbertoDe5A7Dias(int? usuarioId)
        {
            var dataInicio = DateTime.Now.Date.AddDays(-4);
            var dataFim = DateTime.Now.Date.AddDays(-8).AddSeconds(-1);

            if (!usuarioId.HasValue)
            {
                return x => (x.DataCadastro <= dataInicio && x.DataCadastro >= dataFim) && x.FinalizadaAnaliseEm == null;
            }

            return x => x.UsuarioResponsavelId == usuarioId && ((x.DataCadastro <= dataInicio && x.DataCadastro >= dataFim) && x.FinalizadaAnaliseEm == null);
        }

        public static Expression<Func<Processo, bool>> AguardandoParecer()
        {
            return x => x.StatusId == (int)StatusEnum.FinalizadaAnalise && x.EmitidoParecerEm == null;
        }

        public static Expression<Func<Processo, bool>> Finalizados(int? usuarioId)
        {
            var dataInicio = new DateTime(DateTime.Today.Year, DateTime.Now.Month, 1);
            var dataFim = new DateTime(DateTime.Today.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1);

            if (usuarioId.HasValue)
            {
                return x => x.UsuarioResponsavelId == usuarioId && (x.FinalizadaAnaliseEm >= dataInicio && x.FinalizadaAnaliseEm <= dataFim);
            }

            return x => x.FinalizadaAnaliseEm >= dataInicio && x.FinalizadaAnaliseEm <= dataFim;
        }

        public static Expression<Func<Processo, bool>> Finalizados()
        {
            var dataInicio = new DateTime(DateTime.Today.Year, DateTime.Now.Month, 1);
            var dataFim = new DateTime(DateTime.Today.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1);

            return x => x.FinalizadoEm.Value >= dataInicio && x.FinalizadoEm.Value <= dataFim;
        }

        public static Expression<Func<Processo, bool>> AguardandoEmitirNf()
        {
            return x => x.StatusId == (int)StatusEnum.AguardandoEmissaoNf;
        }

        public static Expression<Func<Processo, bool>> EnviadoParaFinanceiro()
        {
            return x => x.StatusId == (int)StatusEnum.EnviadoFinanceiro;
        }

        public static Expression<Func<Processo, bool>> SelecionarTodos()
        {
            return x => x.StatusId != (int)StatusEnum.Finalizado && x.StatusId != (int)StatusEnum.Cancelado;
        }

    }
}
