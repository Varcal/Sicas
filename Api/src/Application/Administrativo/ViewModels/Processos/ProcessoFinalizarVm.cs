using System;

namespace Application.Administrativo.ViewModels.Processos
{
    public class ProcessoFinalizarVm
    {
        public int Id { get; set; }
        public int NumeroNF { get; set; }
        public DateTime DataEmissaoNF { get; set; }
        public byte[] Arquivo { get; set; }

        public ProcessoFinalizarVm()
        {
            
        }

        public ProcessoFinalizarVm(int id, int numeroNf, DateTime dataEmissaoNf, byte[] arquivo)
        {
            Id = id;
            NumeroNF = numeroNf;
            DataEmissaoNF = dataEmissaoNf;
            Arquivo = arquivo;
        }
    }
}
