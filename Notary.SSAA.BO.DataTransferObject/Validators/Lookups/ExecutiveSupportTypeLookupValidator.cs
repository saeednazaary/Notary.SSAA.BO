using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups
{
    public class ExecutiveSupportTypeLookupValidator : AbstractValidator<ExecutiveSupportTypeLookupQuery>
    {
        public ExecutiveSupportTypeLookupValidator()
        {
            RuleFor(v => v.PageIndex)
             .GreaterThanOrEqualTo(1).WithMessage("مقدار شماره صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage("مقدار اندازه صقحه غیر مجاز است")
             .NotNull().WithMessage("فیلد شماره صفحه اجباری است");
        }
    }
}
