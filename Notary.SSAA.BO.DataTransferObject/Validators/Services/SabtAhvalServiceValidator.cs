using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services
{
    public class SabtAhvalServiceValidator : AbstractValidator<SabtAhvalServiceQuery>
    {
        public SabtAhvalServiceValidator()
        {
            RuleFor(v => v.NationalNo)
            .Length(10).WithMessage("مقدار کد ملی مجاز نیست")
            .NotNull().WithMessage("کد ملی اجباری است");

            RuleFor(v => v.BirthDate)
            .Length(10).WithMessage("مقدار تاریخ تولد مجاز نیست")
            .NotNull().WithMessage("مقدار تاریخ تولد اجباری است");

        }
    }
}
