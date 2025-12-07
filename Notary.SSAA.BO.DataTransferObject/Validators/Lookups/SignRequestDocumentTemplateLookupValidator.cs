using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups
{
    public sealed class SignRequestDocumentTemplateLookupValidator : AbstractValidator<SignRequestDocumentTemplateLookupQuery>
    {
        public SignRequestDocumentTemplateLookupValidator()
        {
            RuleFor(v => v.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
                .NotNull().WithMessage("فیلد شماره صفحه اجباری است");

            RuleFor(v => v.PageSize)
                 .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
                 .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
        }

    }
}
