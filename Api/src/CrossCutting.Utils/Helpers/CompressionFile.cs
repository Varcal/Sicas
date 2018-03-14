using System.Collections.Generic;
using System.IO;
using CrossCutting.Utils.Dtos;
using Ionic.Zip;

namespace CrossCutting.Utils.Helpers
{
    public class CompressionFile
    {
        public static FileDto Create(IList<FileDto> arquivos)
        {
            byte[] bytes;

            using (var zip = new ZipFile())
            {
                MemoryStream msZip = new MemoryStream();

                foreach (var arquivo in arquivos)
                {
                    var ms = new MemoryStream(arquivo.Bytes);
                    ms.Seek(0, SeekOrigin.Begin);
                    zip.AddEntry(arquivo.Nome + arquivo.Extensao, ms);                    
                }

                
                zip.Save(msZip);
                msZip.Seek(0, SeekOrigin.Begin);
                bytes = msZip.ToArray();
                
                
            }

            return new FileDto("recibos", bytes, ".zip");
        }

        
    }
}
