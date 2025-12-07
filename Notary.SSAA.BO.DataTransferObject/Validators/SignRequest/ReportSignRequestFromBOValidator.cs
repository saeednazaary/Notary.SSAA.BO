using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class ReportSignRequestFromBOValidator : AbstractValidator<ReportSignRequestFromBOQuery>
    {
        public ReportSignRequestFromBOValidator()
        {
            RuleFor(v => v.SignRequestId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه معتبر نیست").When(x => string.IsNullOrWhiteSpace(x.SignRequestNo)); ;
        }
    }
}
