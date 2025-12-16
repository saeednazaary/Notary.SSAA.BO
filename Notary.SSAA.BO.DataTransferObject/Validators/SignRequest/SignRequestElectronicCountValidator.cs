using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class SignRequestElectronicCountValidator : AbstractValidator<SignRequestElectronicBookPageCountQuery>
    {
        public SignRequestElectronicCountValidator()
        {

        }
    }
}
