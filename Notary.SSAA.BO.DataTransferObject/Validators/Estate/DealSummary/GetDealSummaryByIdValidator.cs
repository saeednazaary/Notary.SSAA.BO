using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.DealSummary
{
    public class GetDealSummaryByIdValidator:AbstractValidator<GetDealSummaryByIdQuery>
    {
        public GetDealSummaryByIdValidator()
        {
            RuleFor(x => x.DealSummaryId)
            .NotNull().When(x => string.IsNullOrWhiteSpace(x.LegacyId)).WithMessage("یکی از موارد شناسه استعلام یا شناسه استعلام در سامانه قدیمی باید مقدار داشته باشد");

            RuleFor(x => x.LegacyId)
            .NotNull().When(x => string.IsNullOrWhiteSpace(x.DealSummaryId)).WithMessage("یکی از موارد شناسه استعلام یا شناسه استعلام در سامانه قدیمی باید مقدار داشته باشد");
        }
    }
}
