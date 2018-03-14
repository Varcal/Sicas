using System.Collections.Generic;
using System.Linq;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.Processos;
using Application.Administrativo.ViewModels.Sindicantes;
using Application.Core.Services;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Contracts.Services;
using Domain.Administrativo.Entities;
using Infra.Data.Core.UnitOfWork;

namespace Application.Administrativo.Services
{
    public class SindicanteApplicationService:ApplicationService, ISindicanteApplicationService
    {
        private readonly ISindicanteRepository _sindicanteRepository;
        private readonly IEnderecoDomainService _enderecoDomainService;
        private readonly ISindicanteDomainService _sindicanteDomainService;
            

        public SindicanteApplicationService(IUnitOfWork uow, ISindicanteRepository sindicanteRepository, IEnderecoDomainService enderecoDomainService, ISindicanteDomainService sindicanteDomainService) 
            :base(uow)
        {
            _sindicanteRepository = sindicanteRepository;
            _enderecoDomainService = enderecoDomainService;
            _sindicanteDomainService = sindicanteDomainService;
        }

        public void Registrar(SindicanteVm sindicanteVm, string user)
        {

            var endereco = _enderecoDomainService.RetornaEnderecoSindicate(sindicanteVm.Endereco.Logradouro, sindicanteVm.Endereco.Numero, sindicanteVm.Endereco.Cep,sindicanteVm.Endereco.Bairro, sindicanteVm.Endereco.CidadeId, sindicanteVm.Endereco.Complemento);
            var dadosBancarios = new DadosBancarios(sindicanteVm.BancoId, sindicanteVm.ContaTipo, sindicanteVm.Agencia, sindicanteVm.Conta, sindicanteVm.Digito);
            var honorarios = sindicanteVm.RetornHonorarios(sindicanteVm);

            var sindicante = _sindicanteDomainService.Registrar(sindicanteVm.SindicanteTipoId, sindicanteVm.Nome,
                sindicanteVm.Cpf, sindicanteVm.Rg, sindicanteVm.Telefone, sindicanteVm.Celular, sindicanteVm.Email, dadosBancarios,
                endereco, honorarios, sindicanteVm.ValorPorKm);

            _sindicanteRepository.Add(sindicante);

            Save(user);
        }

        public IEnumerable<SindicanteVm> SelecionarTodosAtivos()
        {
            var sindicantes = _sindicanteRepository.GetAllActive();

            return sindicantes.Select(sindicante => new SindicanteVm(sindicante));
        }

        public void Excluir(SindicanteDeleteVm model, string user)
        {
            
            var sindicante = _sindicanteDomainService.Excluir(model.Id, model.SindicanteTipoId);
            _sindicanteRepository.Delete(sindicante);
            Save(user);
        }

        public SindicanteEditVm ObterPorId(int id)
        {
            var sindicante = _sindicanteRepository.ObterPorId(id);

            return new SindicanteEditVm(sindicante);
        }

        public void Editar(SindicanteVm sindicanteVm, string user)
        {
            var endereco = _enderecoDomainService.RetornaEnderecoSindicate(sindicanteVm.Endereco.Logradouro, sindicanteVm.Endereco.Numero, sindicanteVm.Endereco.Cep, sindicanteVm.Endereco.Bairro, sindicanteVm.Endereco.CidadeId, sindicanteVm.Endereco.Complemento);
            var dadosBancarios = new DadosBancarios(sindicanteVm.BancoId, sindicanteVm.ContaTipo, sindicanteVm.Agencia, sindicanteVm.Conta, sindicanteVm.Digito);
            var honorarios = sindicanteVm.RetornHonorarios(sindicanteVm);
                    
            var sindicante = _sindicanteDomainService.Alterar(sindicanteVm.Id, sindicanteVm.SindicanteTipoId, sindicanteVm.Nome, sindicanteVm.Cpf, sindicanteVm.Rg, sindicanteVm.Telefone, sindicanteVm.Celular, sindicanteVm.Email, dadosBancarios, endereco, honorarios, sindicanteVm.ValorPorKm);

            _sindicanteRepository.Update(sindicante);

            Save(user);
        }

        public IEnumerable<SindicanteVm> SelecionarTodosExternosAtivos()
        {
            var sindicantesExternos =  _sindicanteRepository.SelecionarTodosExternosAtivos();

            return sindicantesExternos.Select(sindicante => new SindicanteVm(sindicante));
        }

        public IEnumerable<ProcessoListVm> ObterProcessos(int sindicanteId)
        {
            var processos = _sindicanteRepository.ObterProcessos(sindicanteId);

            return processos.Select(x => new ProcessoListVm(x)).ToList();
        }

        public IEnumerable<SindicanteVm> SelecionarPorProcesso(int processoId)
        {
            var sindicantes = _sindicanteRepository.SelecionarPorProcesso(processoId);

            return sindicantes.Select(x => new SindicanteVm(x)).ToList();
        }
    }
}
