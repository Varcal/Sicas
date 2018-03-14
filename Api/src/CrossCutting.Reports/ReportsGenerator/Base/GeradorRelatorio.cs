using Microsoft.Reporting.WebForms;

namespace CrossCutting.Reports.ReportsGenerator.Base
{
    public abstract class GeradorRelatorio
    {
        protected string reportType = "PDF";
        protected string mimeType;
        protected string deviceInfo;
        protected string encoding;
        protected string fileNameExtension;
        protected Warning[] warnings;
        protected string[] streams;
        protected byte[] bytes;

        public string RetornarDeviceInfo()
        {
            var deviceInfo = "";

            return deviceInfo;
        }
    }
}
