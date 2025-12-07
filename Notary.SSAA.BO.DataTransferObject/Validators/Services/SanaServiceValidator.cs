
using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services
{
    public class SanaServiceValidator : AbstractValidator<SanaServiceQuery>
    {
        public SanaServiceValidator()
        {
            RuleFor(v => v.NationalNo)
            .Length(10).WithMessage("مقدار کد ملی مجاز نیست")
            .NotNull().WithMessage("کد ملی اجباری است");
        }
    }
}
