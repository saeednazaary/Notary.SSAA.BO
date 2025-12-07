using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDigitalSignatureConfigurationRepository:IRepository<DigitalSignatureConfiguration>
    {
        Task<DigitalSignatureConfiguration> GetSignRequestElectronicBookConfiguration(CancellationToken cancellationToken);
    }
}
