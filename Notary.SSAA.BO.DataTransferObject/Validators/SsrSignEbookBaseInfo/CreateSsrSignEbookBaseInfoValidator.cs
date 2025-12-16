using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SsrSignEbookBaseInfo
{
    public class CreateSsrSignEbookBaseInfoValidator : AbstractValidator<CreateSsrSignEbookBaseInfoCommand>
    {
        public CreateSsrSignEbookBaseInfoValidator()
        {
            RuleFor(x => x.NumberOfBooks)
            .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار تعداد کل دفاتر کاغذی نامعتبر است ")
            .MaximumLength(6).WithMessage("مقدار تعداد کل دفاتر کاغذی بیش از حد مجاز است ")
            .NotEmpty().WithMessage("مقدار تعداد کل دفاتر کاغذی اجباری است");
            RuleFor(x => x.LastRegisterVolumeNo)
            .MaximumLength(20).WithMessage("مقدار شماره جلد دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی بیش از حد مجاز است ")
            .NotEmpty().WithMessage("مقدار شماره جلد دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی اجباری است");
            RuleFor(x => x.LastRegisterPaperNo)
              .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار شماره صفحه دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی نامعتبر است ")
              .MaximumLength(3).WithMessage("مقدار شماره صفحه دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی بیش از حد مجاز است ")
              .NotEmpty().WithMessage("مقدار شماره صفحه دفتر آخرین گواهی امضاء ثبت شده در دفاتر كاغذی اجباری است");
            RuleFor(x => x.LastClassifyNo)
             .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدارشماره ترتیب آخرین گواهی امضاء ثبت شده در دفاتر كاغذی نامعتبر است ")
            .MaximumLength(6).WithMessage("مقدار شماره ترتیب آخرین گواهی امضاء ثبت شده در دفاتر كاغذی بیش از حد مجاز است ")
             .NotEmpty().WithMessage("مقدار شماره ترتیب آخرین گواهی امضاء ثبت شده در دفاتر كاغذی اجباری است");
        }
    }
}
