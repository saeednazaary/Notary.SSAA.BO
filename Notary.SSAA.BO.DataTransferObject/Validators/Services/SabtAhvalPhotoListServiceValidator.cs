using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services
{
    public class SabtAhvalPhotoListServiceValidator : AbstractValidator<SabtAhvalPhotoListServiceQuery>
    {
        public SabtAhvalPhotoListServiceValidator()
        {
            RuleForEach(x => x.NationalNos).ChildRules(order =>
                {
                    order.RuleFor(x => x).Must(ValidatorHelper.BeValidPersianNationalNumber).WithMessage("شماره ملی وارد شده معتبر نیست");
                });

        }
    }
}
