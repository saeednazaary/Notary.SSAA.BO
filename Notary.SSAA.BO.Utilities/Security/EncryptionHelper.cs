using System.Security.Cryptography;
using System.Text;

namespace Notary.SSAA.BO.Utilities.Security
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string clearText, string password)
        {
            byte[] clearBytes =
              Encoding.UTF8.GetBytes(clearText);
            PasswordDeriveBytes pdb = new(password,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);

        }

        private static byte[] Encrypt(byte[] clearData, byte[] key, byte[] iv)
        {
            using MemoryStream ms = new();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            using (CryptoStream cs = new(ms, alg.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearData, 0, clearData.Length);
                cs.Close();
            }

            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }
    }
}
