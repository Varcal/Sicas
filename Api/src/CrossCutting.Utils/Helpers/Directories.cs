using System.IO;
using System.Text;
using SharedKernel.DomainEvents;
using SharedKernel.DomainEvents.Events.DomainNotifications;
using System.Web;
using CrossCutting.Utils.Enums;
using Domain.Administrativo.ValueObjects;

namespace CrossCutting.Utils.Helpers
{
    public class Directories
    {
        public static Diretorio CreateUploadDirectories(string name)
        {
            //var directoryName = CreateDirectoryName(name);


            var directory = new Diretorio(@"\" + name + @"\Administrativo\", @"\" + name + @"\Financeiro\");

            try
            {
                Directory.CreateDirectory(GetPathUploads(directory.Financeiro));
                Directory.CreateDirectory(GetPathUploads(directory.Administrativo));
                return directory;
            }
            catch (IOException ex)
            {
                DomainEvent.Raise(new DomainNotification("Directories", "Erro ao tentar criar diretórios"));
                throw ex;
            }
        }

        public static void DeleteDirectories(Diretorio diretorio)
        {
            try
            {
                Directory.Delete(diretorio.Administrativo);
                Directory.Delete(diretorio.Financeiro);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public static string GetPathUploads(string path)
        {
            return HttpRuntime.AppDomainAppPath + @"Uploads\" + path;
        }
   
        public static void SaveToFolder(string path, string nome, ArquivoTipo arquivoTipo, byte[] arquivo)
        {
            var sb = new StringBuilder();
            sb.Append(HttpRuntime.AppDomainAppPath);
            sb.Append(@"Uploads\");
            sb.Append(path);
            sb.Append(nome);
            sb.Append(".");
            sb.Append(arquivoTipo);
            
            var bw = new BinaryWriter(File.Open(sb.ToString(), FileMode.OpenOrCreate));
            bw.Write(arquivo);
        }
    }
}
