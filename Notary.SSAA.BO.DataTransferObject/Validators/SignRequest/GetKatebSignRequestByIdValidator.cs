using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class GetKatebSignRequestByIdValidator : AbstractValidator<GetKatebSignRequestByIdQuery>
    {
        public GetKatebSignRequestByIdValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("شناسه اجباری است");
        }
    }
}
