using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;


namespace SSAA.Notary.DataTransferObject.Validators.Lookups.Estate
{
    public class EstateOwnershipTypeLookupValidator : AbstractValidator<EstateOwnershipTypeLookupQuery>
    {
        public EstateOwnershipTypeLookupValidator()
        {
            RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
            .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
            RuleFor(x => x.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
        }
    }
}
