using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services
{
    public class PostServiceValidator : AbstractValidator<PostServiceQuery>
    {
        public PostServiceValidator()
        {
            RuleFor(v => v.PostalCode)
            .Length(10).WithMessage("مقدار کد پستی مجاز نیست")
            .NotNull().WithMessage("مقدار کد پستی اجباری است");
        }
    }
}
