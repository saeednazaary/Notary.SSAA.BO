using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DigitalSign
{
    public class ValidateCertificateValidator : AbstractValidator<ValidateCertificateQuery>
    {
        public ValidateCertificateValidator()
        {
            RuleFor(x => x.Certificate).NotEmpty().WithMessage("گواهی امضای دیجیتال خالی می باشد");            
        }
    }
    
}
