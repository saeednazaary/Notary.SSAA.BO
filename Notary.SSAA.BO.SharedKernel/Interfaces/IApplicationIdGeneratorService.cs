
namespace Notary.SSAA.BO.SharedKernel.Interfaces
{
    public interface IApplicationIdGeneratorService
    {
        public Guid ProvideNewGuid();
        public string ProvideEncryptedGuid();
        public string EncryptGuid(Guid Id);

        public Guid DecryptGuid(string encryptedGUID);
    }
}
