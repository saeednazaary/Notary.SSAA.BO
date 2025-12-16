using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups
{
    public sealed class DocumentTypeSubjectLookupValidator : AbstractValidator<DocumentTypeSubjectLookupQuery>
    {
        public DocumentTypeSubjectLookupValidator()
        {
            RuleFor(v => v.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
                .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleFor(v => v.PageSize)
                 .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
                 .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
        }

    }
}
