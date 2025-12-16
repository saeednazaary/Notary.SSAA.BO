using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups
{
    public class ExecutiveSupportPersonAddressChangeLookupValidator : AbstractValidator<ExecutiveSupportPersonAddressChangeLookupQuery>
    {
        public ExecutiveSupportPersonAddressChangeLookupValidator()
        {
            RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(e => e.ExtraParams.ExtraParam1)
            .NotEmpty().WithMessage("شناسه درخواست تبعی را انتخاب کنید");
            RuleFor(e => e.ExtraParams.ExtraParam2)
           .NotEmpty().WithMessage("نوع درخواست تبعی را انتخاب کنید");
        }
    }
}
