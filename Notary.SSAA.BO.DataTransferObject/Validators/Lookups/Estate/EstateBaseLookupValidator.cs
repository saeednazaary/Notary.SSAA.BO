using FluentValidation;
using Notary.SSAA.BO.SharedKernel.Constants;
using System.Linq.Expressions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate
{
    public class EstateBaseLookupValidator<T> : AbstractValidator<T>
    {
        public EstateBaseLookupValidator()
        {
            var param = Expression.Parameter(typeof(T), "x");
            var pageIndexMember = Expression.Property(param, "PageIndex");
            var pageIndexlambda = Expression.Lambda<Func<T, int>>(pageIndexMember, param);

            var pageSizeMember = Expression.Property(param, "PageSize");
            var pageSizelambda = Expression.Lambda<Func<T, int>>(pageSizeMember, param);

            RuleFor(pageIndexlambda)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
            RuleFor(pageSizelambda)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);
        }
    }
}
