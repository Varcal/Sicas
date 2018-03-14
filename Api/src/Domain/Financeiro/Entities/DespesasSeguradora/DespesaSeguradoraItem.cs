using System;
using System.Linq;
using System.Text;
using Domain.Core.Entities;
using SharedKernel.Enums;

namespace Domain.Financeiro.Entities.DespesasSeguradora
{
    public class DespesaSeguradoraItem: EntityId
    {
        public int DespesaSeguradoraId { get; set; }
        public DateTime Data { get; private set; }
        public string Descricao { get; private set; }
        public string Endereco { get; private set; }
        public decimal KmPercorrido { get; private set; }
        public decimal ValorKmPercorrido { get; private set; }
        public decimal TransportePedagio { get; private set; }
        public decimal HospedagemRefeicao { get; private set; }
        public decimal Total { get; private set; }

        protected DespesaSeguradoraItem()
        {
            
        }

        public DespesaSeguradoraItem(Despesa despesa)
        {
            Data = despesa.Data;
            Descricao = despesa.Descricao;
            Endereco = RetornaEndereco(despesa.Destino);
            KmPercorrido = despesa.Kms;
            ValorKmPercorrido = despesa.ValorKm;
            TransportePedagio = despesa.DespesasAdicionais.Where(x => x.DespesaTipoId == (int)DespesaTipoEnum.Transporte || x.DespesaTipoId == (int)DespesaTipoEnum.Pedagio).Select(d => d.Valor).Sum();
            HospedagemRefeicao = despesa.DespesasAdicionais.Where(x => x.DespesaTipoId == (int)DespesaTipoEnum.Refeicao || x.DespesaTipoId == (int)DespesaTipoEnum.Hospedagem).Select(d => d.Valor).Sum(); ;
            Total = TransportePedagio + HospedagemRefeicao + ValorKmPercorrido;
        }

        private static string RetornaEndereco(EnderecoDespesa destino)
        {
            var sb = new StringBuilder();
            sb.Append(destino.Endereco.Logradouro.Trim());
            sb.Append(", ");
            sb.Append(destino.Numero.Trim());
            sb.Append("-");
            sb.Append(destino.Endereco.Bairro.Trim());
            sb.Append(", ");
            sb.Append(destino.Endereco.Cidade.Nome.Trim());
            sb.Append("-");
            sb.Append(destino.Endereco.Cidade.Estado.Uf.Trim());
            sb.Append(", ");
            sb.Append(destino.Endereco.Cep.Trim());
            return sb.ToString();
        }
    }
}
