using System;

namespace CrossCutting.Utils.Dtos
{
    public class FileDto
    {
        public string Nome { get; set; }
        public byte[] Bytes { get; set; }
        public string Extensao { get; set; }

        public FileDto(string nome, byte[] bytes, string extensao)
        {
            Nome = nome + DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2,'0') +"_"+ Guid.NewGuid();
            Bytes = bytes;
            Extensao = extensao;
        }
    }
}
