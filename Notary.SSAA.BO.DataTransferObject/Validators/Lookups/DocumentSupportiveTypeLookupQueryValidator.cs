using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.SharedKernel.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups
{
    public class DocumentSupportiveTypeLookupQueryValidator : AbstractValidator<DocumentSupportiveTypeLookupQuery>
    {
        public DocumentSupportiveTypeLookupQueryValidator()
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
