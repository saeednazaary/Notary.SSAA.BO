using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate
{
    public class EstateTaxCityLookupValidator : AbstractValidator<EstateTaxCityLookupQuery>
    {
        public EstateTaxCityLookupValidator()
        {
            RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(x => x.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(x => x.ExtraParams)
            .NotNull().WithMessage("ابتدا  شهرستان را انتخاب کنید");
            RuleFor(x => x.ExtraParams.CountyId)
            .NotEmpty().WithMessage("ابتدا شهرستان را انتخاب کنید");
        }
    }
}
