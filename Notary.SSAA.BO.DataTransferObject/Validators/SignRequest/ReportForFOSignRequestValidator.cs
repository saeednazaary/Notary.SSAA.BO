using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public class ReportForFOSignRequestValidator : AbstractValidator<ReportForFOSignRequestQuery>
    {
        public ReportForFOSignRequestValidator()
        {
            RuleFor(v => v.SignRequestId)
                .NotEmpty().WithMessage("شناسه اجباری است").When(x => string.IsNullOrWhiteSpace(x.SignRequestNo)); ;
        }
    }
}
