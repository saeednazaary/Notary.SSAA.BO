using System.Security.Cryptography;
using System.Text;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.Utilities.Security;


public static class FastAesGcm
{
    private const int NonceSize = 12;
    private const int TagSize = 16; // AES-GCM standard tag length

    public static string Encrypt(string plaintext, byte[] key)
    {
        // Pre-checks: must match AES-256 key length (32 bytes)
        if (plaintext == null || key == null || key.Length != 32)
            return null;

        try
        {
            byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);

            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] ciphertext = new byte[plaintextBytes.Length];
            byte[] tag = new byte[TagSize];

            using (var aes = new AesGcm(key))
                aes.Encrypt(nonce, plaintextBytes, ciphertext, tag);

            // Combine [nonce + tag + cipher]
            byte[] combined = new byte[NonceSize + TagSize + ciphertext.Length];
            Buffer.BlockCopy(nonce, 0, combined, 0, NonceSize);
            Buffer.BlockCopy(tag, 0, combined, NonceSize, TagSize);
            Buffer.BlockCopy(ciphertext, 0, combined, NonceSize + TagSize, ciphertext.Length);

            return combined.EncodeBase64Url();
        }
        catch { return null; }
    }

    public static string Decrypt(string base64Payload, byte[] key)
    {
        // Pre-checks
        if (string.IsNullOrEmpty(base64Payload) || key == null || key.Length != 32)
            return null;

        try
        {
            byte[] combined = base64Payload.DecodeBase64Url();

            // Must at least contain nonce+tag
            if (combined.Length < NonceSize + TagSize)
                return null;

            byte[] nonce = new byte[NonceSize];
            byte[] tag = new byte[TagSize];
            byte[] ciphertext = new byte[combined.Length - NonceSize - TagSize];

            Buffer.BlockCopy(combined, 0, nonce, 0, NonceSize);
            Buffer.BlockCopy(combined, NonceSize, tag, 0, TagSize);
            Buffer.BlockCopy(combined, NonceSize + TagSize, ciphertext, 0, ciphertext.Length);

            byte[] plaintextBytes = new byte[ciphertext.Length];
            using (var aes = new AesGcm(key))
                aes.Decrypt(nonce, ciphertext, tag, plaintextBytes);

            return Encoding.UTF8.GetString(plaintextBytes);
        }
        catch { return null; }
    }
}

