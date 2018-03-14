namespace CrossCutting.Reports.Base
{
    public class ReportDto
    {
        public string Nome { get; set; }
        public byte[] Bytes { get; private set; }
        public string MimeType { get; private set; }

       
        public ReportDto(string nome, byte[] bytes, string mimeType)
        {
            Nome = nome.Replace(" ","_").ToUpper();
            Bytes = bytes;
            MimeType = mimeType;
        }
    }
}
