using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class GetPersonFingerprintImageValidator : AbstractValidator<GetPersonFingerprintImageQuery>
    {
        public GetPersonFingerprintImageValidator()
        {

            RuleFor(v => v.MainObjectId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه مدرک مربوطه اجباری است .");

            RuleFor(v => v.ValidateAllPeople)
                .NotEmpty().WithMessage("آیا همه افراد استعلام شود ؟ اجباری است .");

            RuleFor(x => x.PersonObjectIds).Must(x => x != null && x.Count == 1).WithMessage("تعدادافراد برای استعلام غیر مجاز است ")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.PersonObjectIds)
                    .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه فرد الزامی است ");
                }).
                When(x => !x.ValidateAllPeople);

        }
    }
}
