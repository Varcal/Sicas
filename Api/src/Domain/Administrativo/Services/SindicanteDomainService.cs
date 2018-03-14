using System.Collections.Generic;
using System.Runtime.InteropServices;
using Domain.Account.Contracts.Repositories;
using Domain.Account.Entities;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Contracts.Services;
using Domain.Administrativo.Entities;
using SharedKernel.Enums;

namespace Domain.Administrativo.Services
{
    public class SindicanteDomainService : ISindicanteDomainService
    {
        private readonly IPerfilRepository _perfilRepository;
        private readonly ISindicanteRepository _sindicanteRepository;

        public SindicanteDomainService(IPerfilRepository perfilRepository, ISindicanteRepository sindicanteRepository)
        {
            _perfilRepository = perfilRepository;
            _sindicanteRepository = sindicanteRepository;
        }

        public Sindicante Registrar(int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular, string email, DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> enderecos, ICollection<Honorario> honorarios, [Optional] decimal valorPorKm)
        {

            Sindicante sindicante = null;

            switch ((SindicanteTipoEnum)sindicanteTipoId)
            {
                case SindicanteTipoEnum.Interno:

                    var usuario = CriarUsuario(nome, email);

                    sindicante = new SindicanteInterno(usuario, sindicanteTipoId, nome, cpf, rg, telefone, celular, email, dadosBancarios, enderecos, honorarios);
                    break;
                case SindicanteTipoEnum.Externo:
                   sindicante = new Sindicante(sindicanteTipoId, nome, cpf, rg, telefone, celular, email, dadosBancarios, enderecos, honorarios, valorPorKm);
                    break;
            }


            return sindicante;
        }

        public Sindicante Alterar(int id, int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular,string email, DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> enderecos, ICollection<Honorario> honorarios, [Optional] decimal valorPorKm)
        {

            Sindicante sindicante = null;
            
            switch (sindicanteTipoId)
            {
                case (int) SindicanteTipoEnum.Interno:
                    var interno = _sindicanteRepository.ObterInternoPorId(id);

                    if (interno.Usuario == null)
                    {
                        var usuario = CriarUsuario(nome, email);
                        interno.Alterar(usuario, sindicanteTipoId, nome, cpf, rg, telefone, celular, email, dadosBancarios,enderecos, honorarios);
                    }
                    else
                    {
                        interno.Alterar(sindicanteTipoId, nome, cpf, rg, telefone, celular, email, dadosBancarios, enderecos, honorarios, valorPorKm);
                    }
                                       
                    return interno;

                case (int) SindicanteTipoEnum.Externo:
                    sindicante = _sindicanteRepository.ObterPorId(id);
                    sindicante.Alterar(sindicanteTipoId, nome, cpf, rg, telefone, celular, email, dadosBancarios, enderecos, honorarios, valorPorKm);

                    break;

                default:
                    ///TODO Adicionar Domain Notification
                    break;
            }

            return sindicante;
        }

        public Sindicante Excluir(int id, int sindicanteTipo)
        {
            

            switch ((SindicanteTipoEnum)sindicanteTipo)
            {
                case SindicanteTipoEnum.Interno:
                    var sindicanteInterno = _sindicanteRepository.ObterInternoPorId(id);
                    sindicanteInterno.Excluir();
                    return sindicanteInterno;
                case SindicanteTipoEnum.Externo:
                    var sindicante = _sindicanteRepository.ObterPorId(id);
                    sindicante.Excluir();
                    return sindicante;
                default:
                    return null;
            }
        }


        private Usuario CriarUsuario(string nome, string email)
        {
            var perfil = _perfilRepository.ObterPerfil((int) PerfilEnum.Analista);
            return new Usuario(nome, email, perfil);
        }
    }
}
