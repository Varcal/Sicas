using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Domain.Account.Entities;
using Domain.Core.Entities;
using Domain.Core.Entities.Enderecos;
using Domain.Financeiro.Entities.DespesasSeguradora;
using SharedKernel.DomainEvents;
using SharedKernel.DomainEvents.Events.DomainNotifications;
using SharedKernel.Enums;

namespace Domain.Administrativo.Entities
{
    public class Processo: EntityId
    {
        public int SeguradoraId { get; private set; }
        public int ServicoSeguradoraId { get; private set; }
        public int EventoTipoId { get; private set; }
        public int? UsuarioResponsavelId { get; private set; }
        public int ServicoSindicanteId { get; private set; }
        public string NumeroSinistro { get; private set; }
        public string NumeroReferencia { get; private set; }
        public string Analista { get; private set; }
        public string Placa { get; private set; }
        public int CidadeId { get; private set; }
        //public int? LocalFatosId { get; private set; }
        public int SeguradoId { get; private set; }
        public SituacaoEnum Situacao { get; private set; }
        public int StatusId { get; private set; }
        public DateTime? FinalizadaAnaliseEm { get; private set; }
        public DateTime? EmitidoParecerEm { get; private set; }
        public DateTime? FinalizadoEm { get; private set; }
        public string Comentario { get; private set; }
        public int? AcionamentoId { get; private set; }
        public int? NumeroNF { get; private set; }
        public DateTime? DataEmissaoNF { get; set; }

        public Status Status { get; private set; }
        public Seguradora Seguradora { get; private set; }
        public ICollection<Sindicante> Sindicantes { get; private set; }
        public Usuario UsuarioResponsavel { get; private set; }
        public ServicoSeguradora ServicoSeguradora { get; private set; }
        public EventoTipo EventoTipo { get; private set; }
        public ServicoSindicante ServicoSindicante { get; private set; }
        public ICollection<ProcessoHistorico> Historicos { get; private set; }
        public ICollection<Despesa> Despesas { get; private set; }
        //public LocalFatos LocalFatos { get; private set; }
        public Segurado Segurado { get; private set; }
        public Cidade Cidade { get; private set; }
        public Acionamento Acionamento { get; private set; }


        protected Processo()
        {
            Historicos = new List<ProcessoHistorico>();
            Sindicantes = new List<Sindicante>();
            Despesas = new List<Despesa>();
        }

        public Processo(DateTime dataCadastro, int seguradoraId, int servicoSeguradoraId, Sindicante sindicante, int eventoTipoId, Usuario usuarioResponsavel, 
            int servicoSindicanteId, string numeroSinistro, string analista, string placa, int cidadeId, Segurado segurado,[Optional]string numeroReferencia)
            :this()
        {
            DataCadastro = dataCadastro < DateTime.Now ? dataCadastro.Date : DateTime.Now;
            SeguradoraId = seguradoraId;
            ServicoSeguradoraId = servicoSeguradoraId;
            EventoTipoId = eventoTipoId;
            UsuarioResponsavel = usuarioResponsavel;           
            ServicoSindicanteId = servicoSindicanteId;
            NumeroSinistro = numeroSinistro;
            NumeroReferencia = numeroReferencia != null ? numeroReferencia : null;
            Analista = analista;
            Placa = placa;
            CidadeId = cidadeId;
            Segurado = segurado;
            //LocalFatos = localFatos;
            Situacao = SituacaoEnum.EmAnalise;
            StatusId = (int)StatusEnum.EmAnalise;
            AdicionarSindicante(sindicante);
            AdicionarHistorico();      
        }

        
        public void Alterar(int seguradoraId, int servicoSeguradoraId, int eventoTipoId,  Sindicante sindicante, int servicoSindicanteId, string numeroSinistro, string numeroReferencia, string analista, string placa, int cidadeId, Segurado segurado, 
            //LocalFatos localFatos, 
            byte[] arquivo)
        {
            SeguradoraId = seguradoraId;
            ServicoSeguradoraId = servicoSeguradoraId;
            EventoTipoId = eventoTipoId;
            ServicoSindicanteId = servicoSindicanteId;
            NumeroSinistro = numeroSinistro;
            NumeroReferencia = numeroReferencia;
            Analista = analista;
            UsuarioResponsavelId = sindicante.SindicanteTipoId == (int) SindicanteTipoEnum.Interno ? (int?) ((SindicanteInterno) sindicante).UsuarioId : null;
            Placa = placa;
            CidadeId = cidadeId;
;           Segurado.Alterar(segurado);
            //LocalFatos.Alterar(localFatos.Endereco, localFatos.EnderecoTipoId, localFatos.Numero, localFatos.Complemento);
            AlterarSindicante(sindicante);

            if (arquivo != null)
            {
                if(Acionamento == null)
                    Acionamento = new Acionamento(NumeroSinistro, arquivo);
                else
                    Acionamento.Alterar(arquivo);   
            }
        }

        public void AdicionarHistorico()
        {
            var processoHistorico = new ProcessoHistorico(this);
            Historicos.Add(processoHistorico);
        }

        public void AdicionarHistorico(bool finalizado)
        {
            var processoHistorico = new ProcessoHistorico(this);
            processoHistorico.FinalizarHistorico();
            Historicos.Add(processoHistorico);
        }

        public void AdicionarSindicante(Sindicante sindicante)
        {
            Sindicantes.Add(sindicante);
        }

        public void AlterarSindicante(Sindicante sindicante)
        {
            if (Sindicantes.Count > 1)
            {
                DomainEvent.Raise(new DomainNotification("ErrorAlter","Processo já movimentado para outro sindicante"));
                return;
            }

            Sindicantes.Clear();
            Sindicantes.Add(sindicante);
        }

        public void FinalizarParecer(int situacaoId, string comentario)
        {
            Situacao = (SituacaoEnum)situacaoId;
            StatusId = (int)StatusEnum.EnviadoFinanceiro;
            Comentario = comentario;
            EmitidoParecerEm = DateTime.Now;
            AdicionarHistorico();
        }

        public void FinalizarDespesas()
        {
            StatusId = (int)StatusEnum.AguardandoEmissaoNf;
            AdicionarHistorico();
        }

        public void FinalizarAnalise()
        {
            StatusId = (int)StatusEnum.FinalizadaAnalise;
            FinalizadaAnaliseEm = DateTime.Now;
            AdicionarHistorico();
        }

        public void EmitirNf()
        {
            StatusId = (int)StatusEnum.EnviadoSeguradora;
            AdicionarHistorico();
        }

        public void Cancelar()
        {
            FinalizadoEm = DateTime.Now;
            StatusId = (int)StatusEnum.Cancelado;   
            AdicionarHistorico();       
        }

        public void FinalizarProcesso(int numeroNf, DateTime dataEmissaoNf)
        {
            NumeroNF = numeroNf;
            DataEmissaoNF = dataEmissaoNf;
            FinalizadoEm = DateTime.Now;
            StatusId = (int)StatusEnum.Finalizado;
            AdicionarHistorico(true);
        }

        public void EnviarSindicanteExterno(Sindicante sindicante)
        {
            Sindicantes.Add(sindicante);
            StatusId = (int) StatusEnum.EnviadoSindicanteExterno;
            AdicionarHistorico();
        }

        public void AdicionarDespesas(IList<Despesa> despesas)
        {
            foreach (var despesa in despesas.Where(despesa => despesa.Id == 0))
            {
                Despesas.Add(despesa);
            }
        }

        public void IncluirAcionamento(Acionamento acionamento)
        {
            Acionamento = acionamento;
        }
    }
}
