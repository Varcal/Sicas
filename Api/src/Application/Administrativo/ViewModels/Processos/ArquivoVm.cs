using System.IO;

namespace Application.Administrativo.ViewModels.Processos
{
    public class ArquivoVm
    {
        public string Nome { get; }
        public FileStream Bytes { get; }
        public long Length { get; }


        public ArquivoVm(string nome, FileStream bytes, long length)
        {
            Nome = nome;
            Bytes = bytes;
            Length = length;
        }        
    }
}
