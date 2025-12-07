using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services.Estate
{
    public class GetEstateSeparationElementsValidator : AbstractValidator<GetEstateSeparationElementsQuery>
    {
        public GetEstateSeparationElementsValidator()
        {
            RuleFor(x => x.EstateInquiryId).NotEmpty().WithMessage("شناسه استعلام اجباری می باشد");

        }
    }
}
