using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class SignRequestElectronicValidator : AbstractValidator<SignRequestElectronicBookPageQuery>
    {
        public SignRequestElectronicValidator()
        {
            RuleFor(x => x.PageNumber)
            .NotEmpty().WithMessage("شماره صفحه اجباری است ");
            RuleFor(x => x.SignRequestNationalNo).MaximumLength(18).WithMessage("طول شناسه یکتا گواهی امضا مجاز نمیباشد ");
            RuleFor(x => x.PersonSignClassifyNo).Must(ValidatorHelper.BeValidNumber)
                .When(x=>!string.IsNullOrEmpty( x.PersonSignClassifyNo) ).WithMessage("طول  شماره ترتیب گواهی امضا مجاز نمیباشد ")
                .MaximumLength(5).WithMessage("طول  شماره ترتیب گواهی امضا مجاز نمیباشد ");
        }
    }
}
