
namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class ByteArrayExtensions
    {

        public static byte[] DecodeBase64Url(this string base64Url)
        {
            string padded = base64Url
                .Replace('-', '+')
                .Replace('_', '/');
            // restore padding
            switch (padded.Length % 4)
            {
                case 2: padded += "=="; break;
                case 3: padded += "="; break;
            }

            return Convert.FromBase64String(padded);
        }
        public static string toString ( this byte [ ] byteArray )
        {
            if ( byteArray is null )
                return null;
            else
            {
                return System.Text.Encoding.UTF8.GetString ( byteArray );
            }
        }
    }
}