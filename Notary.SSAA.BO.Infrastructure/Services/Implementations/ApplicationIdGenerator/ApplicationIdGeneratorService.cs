using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Security;

namespace Notary.SSAA.BO.Infrastructure.Services.Implementations.ApplicationIdGenerator
{
    public class ApplicationIdGeneratorService(IConfiguration _configuration) : IApplicationIdGeneratorService
    {
        private readonly byte[] aesKey = Convert.FromBase64String(_configuration.GetValue<string>("aesEncryptionKey"));
        public string ProvideEncryptedGuid()
        {
            return Guid.NewGuid().ToString();
            var rawGuid = Guid.NewGuid().ToString();
            string encryptedGuid = FastAesGcm.Encrypt(rawGuid, aesKey);
            return encryptedGuid;
        }

        public Guid ProvideNewGuid()
        {
            return Guid.NewGuid();
        }
        public string EncryptGuid(Guid decryptedGUID)
        {
            return decryptedGUID.ToString();
            string unencryptedGuid = FastAesGcm.Encrypt(decryptedGUID.ToString(), aesKey);
            return unencryptedGuid;
        }
        public Guid DecryptGuid(string encryptedGUID)
        {
            return Guid.Parse(encryptedGUID);
            string unencryptedGuid = FastAesGcm.Decrypt(encryptedGUID, aesKey);
            if (Guid.TryParse(unencryptedGuid, out _))
            {
                return unencryptedGuid.ToGuid();

            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}
