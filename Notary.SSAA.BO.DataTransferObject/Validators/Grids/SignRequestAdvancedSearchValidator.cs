using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids;

public sealed class SignRequestAdvancedSearchValidator
    : AbstractValidator<SignRequestAdvancedSearchQuery>
{
    public SignRequestAdvancedSearchValidator()
    {
        RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(1)
                .WithMessage(SystemMessagesConstant.Grid_PageIndex_Invalid)
            .NotNull()
                .WithMessage(SystemMessagesConstant.Grid_PageIndex_Required);

        RuleFor(v => v.PageSize)
            .ExclusiveBetween(0, 11)
                .WithMessage(SystemMessagesConstant.Grid_PageSize_Invalid)
            .NotNull()
                .WithMessage(SystemMessagesConstant.Grid_PageSize_Required);

        RuleFor(v => v.ExtraParams.SignRequestReqNo)
            .MaximumLength(18)
                .WithMessage(SystemMessagesConstant.SignRequest_RequestNo_MaxLength)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.SignRequestReqNo.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.SignRequestNationalNo)
            .MaximumLength(18)
                .WithMessage(SystemMessagesConstant.SignRequest_NationalNo_MaxLength)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.SignRequestNationalNo.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.SignRequestStateId)
            .Must(x => int.TryParse(x, out var v) && v is >= 1 and <= 5)
                .WithMessage(SystemMessagesConstant.SignRequest_State_Invalid)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.SignRequestStateId.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.SignRequestReqDateFrom)
            .Must(ValidatorHelper.BeValidPersianDate)
                .WithMessage(SystemMessagesConstant.SignRequest_RequestDateFrom_Invalid)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.SignRequestReqDateFrom.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.SignRequestReqDateTo)
            .Must(ValidatorHelper.BeValidPersianDate)
                .WithMessage(SystemMessagesConstant.SignRequest_RequestDateTo_Invalid)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.SignRequestReqDateTo.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.SignRequestSignDateFrom)
            .Must(ValidatorHelper.BeValidPersianDate)
                .WithMessage(SystemMessagesConstant.SignRequest_SignDateFrom_Invalid)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.SignRequestSignDateFrom.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.SignRequestSignDateTo)
            .Must(ValidatorHelper.BeValidPersianDate)
                .WithMessage(SystemMessagesConstant.SignRequest_SignDateTo_Invalid)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.SignRequestSignDateTo.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.PersonNationalNo)
            .MaximumLength(10)
                .WithMessage(SystemMessagesConstant.Person_NationalNo_MaxLength)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.PersonNationalNo.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.PersonName)
            .MaximumLength(150)
                .WithMessage(SystemMessagesConstant.Person_Name_MaxLength)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.PersonName.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.PersonFamily)
            .MaximumLength(50)
                .WithMessage(SystemMessagesConstant.Person_Family_MaxLength)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.PersonFamily.IsNullOrWhiteSpace());

        RuleFor(v => v.ExtraParams.PersonFatherName)
            .MaximumLength(50)
                .WithMessage(SystemMessagesConstant.Person_FatherName_MaxLength)
            .When(x => x.ExtraParams != null &&
                       !x.ExtraParams.PersonFatherName.IsNullOrWhiteSpace());
    }
}
