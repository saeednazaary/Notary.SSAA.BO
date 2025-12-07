using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;


namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DigitalSignatureConfigurationRepository:Repository<DigitalSignatureConfiguration>,IDigitalSignatureConfigurationRepository
    {
        public DigitalSignatureConfigurationRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
        public async Task<DigitalSignatureConfiguration> GetSignRequestElectronicBookConfiguration(CancellationToken cancellationToken)
        {
            return await TableNoTracking
                   .Where(x => x.ConfigName == "SIGN_REQUEST_ELECTRONIC_BOOK_DIGITAL_SIGNATURE_CONFIG" && x.FormName == "SIGN_REQUEST_FORM")
                   .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
