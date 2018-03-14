using CrossCutting.Utils.Dtos;

namespace Application.Financeiro.ViewModels
{
    public class ReciboReportVm
    {
        public string Nome { get; set; }
        public byte[] Bytes { get; set; }


        public ReciboReportVm(string nome, byte[] bytes)
        {
            Nome = nome;
            Bytes = bytes;
        }

        public ReciboReportVm(FileDto fileDto)
        {
            Nome = fileDto.Nome + fileDto.Extensao;
            Bytes = fileDto.Bytes;
        }
    }
}
