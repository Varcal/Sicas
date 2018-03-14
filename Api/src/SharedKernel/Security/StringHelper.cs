using System;
using System.Security.Cryptography;
using System.Text;

namespace SharedKernel.Security
{
    public class StringHelper
    {
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            value += "|54be1d80-b6d0-45c0-b8d7-13b3c798729f";

            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(value));
            var sbString = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));

            return sbString.ToString();
        }

        public static byte[] StringToBase64Array(string text)
        {
            var  ret = Encoding.Unicode.GetBytes(text);
            var s = Convert.ToBase64String(ret);
            ret = Encoding.Unicode.GetBytes(s);
            return ret;
        }
    }
}