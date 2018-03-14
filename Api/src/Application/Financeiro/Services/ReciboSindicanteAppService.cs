using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Services;
using Application.Financeiro.Contracts;
using Application.Financeiro.ViewModels;
using Application.Financeiro.ViewModels.Lancamentos;
using CrossCutting.Reports.ReportsGenerator.Interfaces;
using CrossCutting.Utils.Dtos;
using CrossCutting.Utils.Helpers;
using Domain.Financeiro.Contracts.Repositories;
using Infra.Data.Core.UnitOfWork;

namespace Application.Financeiro.Services
{
    public class ReciboSindicanteAppService: ApplicationService, IReciboSindicanteAppService
    {
        private readonly IReciboSindicanteRepository _reciboSindicanteRepository;
        private readonly IReciboSindicanteGeradorRelatorio _reciboSindicanteGeradorRelatorio;

        public ReciboSindicanteAppService(IUnitOfWork uow, IReciboSindicanteRepository reciboSindicanteRepository, IReciboSindicanteGeradorRelatorio reciboSindicanteGeradorRelatorio) 
            : base(uow)
        {
            _reciboSindicanteRepository = reciboSindicanteRepository;
            _reciboSindicanteGeradorRelatorio = reciboSindicanteGeradorRelatorio;
        }

        public IEnumerable<ReciboSindicanteRelatorioVm> SelecionarPorSindicanteData(DateTime dataInicio, DateTime dataFim)
        {
            var recibos = _reciboSindicanteRepository.SelecionarPorSindicanteData(dataInicio, dataFim);

            return recibos.Select(x => new ReciboSindicanteRelatorioVm(x)).ToList();
        }

        public ReciboReportVm EmitirRecibo(int id, string usuarioLogado)
        {
            var reciboSindicante = _reciboSindicanteRepository.ObterPorId(id);
            reciboSindicante.Emitir();
            _reciboSindicanteRepository.Update(reciboSindicante);
            
            var report = _reciboSindicanteGeradorRelatorio.GerarRelatorio(reciboSindicante);

            Save(usuarioLogado);

            return new ReciboReportVm(reciboSindicante.Sindicante, report.Bytes);
        }

        public ReciboReportVm EmitirTodos(DateTime dataInicio, DateTime dataFim, string usuarioLogado)
        {
            var recibosSindicantes = _reciboSindicanteRepository.SelecionarTodosNaoEmitidosPorData(dataInicio, dataFim).ToList();

            foreach (var reciboSindicante in recibosSindicantes)
            {
                reciboSindicante.Emitir();
                _reciboSindicanteRepository.Update(reciboSindicante);
            }


            var reportDtoList = _reciboSindicanteGeradorRelatorio.GerarRelatorioEmLote(recibosSindicantes);

            var arquivos = reportDtoList.Select(x => new FileDto(x.Nome, x.Bytes, ".pdf")).ToList();

            var zip = CompressionFile.Create(arquivos);


            Save(usuarioLogado);

            return new ReciboReportVm(zip);
        }
    }
}
