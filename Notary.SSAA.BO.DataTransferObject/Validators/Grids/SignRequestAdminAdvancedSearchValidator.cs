using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids
{
    public class SignRequestAdminAdvancedSearchValidator : AbstractValidator<SignRequestAdminAdvancedSearchQuery>
    {
        public SignRequestAdminAdvancedSearchValidator()
        {
            RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            RuleFor(v => v.PageSize)
             .ExclusiveBetween(0, 11).WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
             .NotNull().WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

            //RuleFor(v => v.ExtraParams.SignRequestReqNo).NotEmpty().WithMessage("لطفا شماره درخواست را همراه با کد دفترخانه پر کنید.")
            //    .When(x => x.ExtraParams is not null && x.ExtraParams.SignRequestReqNo.IsNullOrWhiteSpace() && !x.ExtraParams.SignRequestScriptoriumCode.IsNullOrWhiteSpace());

            //RuleFor(v => v.ExtraParams.SignRequestScriptoriumCode).NotEmpty().WithMessage("لطفا کد دفترخانه را همراه شماره درخواست پر کنید.")
            //    .When(x => x.ExtraParams is not null && x.ExtraParams.SignRequestScriptoriumCode.IsNullOrWhiteSpace() && !x.ExtraParams.SignRequestReqNo.IsNullOrWhiteSpace());

            //RuleFor(v => v.ExtraParams.SignRequestReqNo).MaximumLength(18).WithMessage("طول شماره درخواست مجاز نمیباشد")
            //    .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestReqNo.IsNullOrWhiteSpace());

            //RuleFor(v => v.ExtraParams.SignRequestNationalNo).MaximumLength(18).WithMessage("طول شناسه یکتا گواهی امضا مجاز نمیباشد ")
            //    .When(x => x.ExtraParams is not null && !x.ExtraParams.SignRequestNationalNo.IsNullOrWhiteSpace());
        }
    }
}
