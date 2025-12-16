using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.DealSummary
{
    public class DealSummaryPrintValidator : AbstractValidator<DealSummaryPrintQuery>
    {
        public DealSummaryPrintValidator()
        {
            RuleFor(x => x.DealSummaryId).NotEmpty().WithMessage("شناسه خلاصه معامله خالی می باشد");
        }
    }
    
}
