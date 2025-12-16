using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.DealSummary
{
    public class UnRestrictDealSummaryValidator : AbstractValidator<UnRestrictDealSummaryCommand>
    {
        public UnRestrictDealSummaryValidator()
        {
            RuleFor(x => x.RemoveRestrictionNo).NotEmpty().WithMessage("شماره رفع محدودیت خالی می باشد");
            RuleFor(x => x.RemoveRestrictionDate).NotEmpty().WithMessage("تاریخ رفع محدودیت خالی می باشد");
            RuleFor(x => x.RemoveRestrictionTypeId).NotEmpty().WithMessage("شناسه نوع رفع محدودیت خالی می باشد");
        }
    }
    
}
