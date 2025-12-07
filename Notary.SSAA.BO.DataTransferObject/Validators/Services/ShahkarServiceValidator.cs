using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services
{
    public class ShahkarServiceValidator : AbstractValidator<ShahkarServiceQuery>
    {
        public ShahkarServiceValidator()
        {
            RuleFor(v => v.MobileNumber)
            .Length(11).WithMessage("مقدار شماره موبایل مجاز نیست");

            RuleFor(v => v.NationalNo)
            .Length(10).WithMessage("مقدار شماره ملی مجاز نیست");
        }
    }
}
