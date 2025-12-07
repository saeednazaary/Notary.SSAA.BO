using System.Security.Cryptography;
using System.Text;

namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class SecurityHelper
    {
        public static string GetSha256Hash(this string input)
        {
            byte[] byteValue = Encoding.UTF8.GetBytes(input);
            byte[] byteHash = SHA256.HashData(byteValue);
            return Convert.ToBase64String(byteHash);
        }
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
