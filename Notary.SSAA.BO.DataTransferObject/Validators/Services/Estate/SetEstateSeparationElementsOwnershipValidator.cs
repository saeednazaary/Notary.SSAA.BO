using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services.Estate
{
    public class SetEstateSeparationElementsOwnershipValidator : AbstractValidator<SetEstateSeparationElementsOwnershipQuery>
    {
        public SetEstateSeparationElementsOwnershipValidator()
        {
            RuleFor(x => x.TheElementOwnershipInfoList).NotEmpty().WithMessage("اطلاعات مالکیت قطعات حاصل از تفکیک اجباری می باشد");
        }
    }
}
