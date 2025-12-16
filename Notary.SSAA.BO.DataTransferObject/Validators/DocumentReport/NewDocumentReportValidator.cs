using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentReport;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentReport
{
    public class NewDocumentReportValidator : AbstractValidator<NewDocumentReportQuery>
    {
        public NewDocumentReportValidator()
        {
            RuleFor(v => v.Id)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage("شناسه معتبر نیست");
        }
    }
}
