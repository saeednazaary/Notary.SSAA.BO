using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Notary.SSAA.BO.Infrastructure.Services.Implementations.Security
{
    public class CryptoService
    {
        private byte[] _key;
        private byte[] _iv;
        private bool _isInitialized = false;

        /// <summary>
        /// Generate exactly the same key and IV as Angular CryptoJS.PBKDF2
        /// </summary>
        public void SetKeyFromPassword ( string password, string salt )
        {
            try
            {

                using var deriveBytesKey = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 1000);
                _key = deriveBytesKey.GetBytes ( 32 );

                using var deriveBytesIV = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt + "IV"), 1000);
                _iv = deriveBytesIV.GetBytes ( 16 );

                _isInitialized = true;

                Console.WriteLine ( "C# - Key generated successfully" );
                Console.WriteLine ( "C# - Key (Hex): " + BitConverter.ToString ( _key ).Replace ( "-", "" ).ToLower () );
                Console.WriteLine ( "C# - IV (Hex): " + BitConverter.ToString ( _iv ).Replace ( "-", "" ).ToLower () );
            }
            catch ( Exception ex )
            {
                throw new Exception ( $"Failed to initialize crypto service: {ex.Message}", ex );
            }
        }

        /// <summary>
        /// Alternative method with custom iterations
        /// </summary>

        public bool IsReady ( ) => _isInitialized && _key != null && _iv != null;


        public T? Decrypt<T> ( string encryptedData, IEnumerable<System.Security.Claims.Claim> cl )
        {
            try
            {
                var auth_time = cl
                ?.FirstOrDefault(x => x.Type.Equals("auth_time", StringComparison.OrdinalIgnoreCase))?.Value;
                var subject = cl
                ?.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", StringComparison.OrdinalIgnoreCase))?.Value;

                if ( auth_time != null && subject != null )
                {
                    string password = auth_time.Replace("1","3");
                    string salt = subject.Replace("A","J");
                    SetKeyFromPassword ( salt, password );

                }
                else
                {
                    return default ( T? );
                }

                if ( !IsReady () )
                    throw new InvalidOperationException ( "Crypto service not initialized. Call SetKeyFromPassword() first." );

                try
                {
                    var encryptedBytes = Convert.FromBase64String(encryptedData);

                    using var aes = Aes.Create();
                    aes.Key = _key;
                    aes.IV = _iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    var json = Encoding.UTF8.GetString(decryptedBytes);

                    if ( string.IsNullOrWhiteSpace ( json ) )
                        throw new Exception ( "Decrypted data is empty" );

                    return JsonConvert.DeserializeObject<T> ( json );
                }
                catch ( FormatException ex )
                {
                    throw new Exception ( $"Invalid Base64 string: {ex.Message}", ex );
                }
                catch ( CryptographicException ex )
                {
                    throw new Exception ( $"Decryption failed - possibly wrong password or corrupted data: {ex.Message}", ex );
                }
                catch ( JsonException ex )
                {
                    throw new Exception ( $"Invalid JSON after decryption: {ex.Message}", ex );
                }
                catch ( Exception ex )
                {
                    throw new Exception ( $"Decryption failed: {ex.Message}", ex );
                }
            }
            catch ( Exception e )
            {
                return default ( T );
            }
            return default ( T );

        }


    }
}
