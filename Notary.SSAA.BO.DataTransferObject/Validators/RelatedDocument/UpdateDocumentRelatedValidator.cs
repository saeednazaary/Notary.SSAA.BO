using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.RelatedDocument;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.RelatedDocument
{
    public class UpdateDocumentRelatedValidator : AbstractValidator<UpdateRelatedDocumentCommand>
    {
        public UpdateDocumentRelatedValidator()
        {
            RuleFor(v => v.RequestId)
               .Must(ValidatorHelper.BeValidGuid)
               .WithMessage("شناسه معتبر نیست");

            RuleFor(v => v.IsNew)
               .Must(x => x == false).WithMessage("نوع درخواست جدید نمیباشد");

            RuleFor(v => v.IsDelete)
                .Must(x => x == false).WithMessage("نوع درخواست حذف میباشد");

            RuleFor(v => v.IsRequestInSsar)
                .NotEmpty().WithMessage("مقدار آیا سند در سیستم ثبت الکترونیک اسناد ثبت شده است؟ اجباری میباشد");

            RuleFor(v => v.DocumentTypeId)
                .NotEmpty().WithMessage("مقدار نوع سند اجباری میباشد");

            //RuleFor(v => v.DocumentSecretCode)
            //    .Must(ValidatorHelper.BeValidNumber).WithMessage("فرمت رمز تصدیق اشتباه است")
            //    .MaximumLength(6).WithMessage("مقدار رمز تصدیق غیرمجاز میباشد")
            //    .NotEmpty().WithMessage("مقدار رمز تصدیق اجباری میباشد")
            //    .When(v => v.IsRequestInSsar == true);

            RuleFor(v => v.NationalNo)
                .MaximumLength(18).WithMessage("مقدار شناسه یکتا غیرمجاز میباشد")
                .NotEmpty().WithMessage("مقدار شناسه یکتا اجباری میباشد")
                .When(v => v.IsRequestInSsar == true);

            RuleFor(v => v.RequestNo)
                .NotEmpty().WithMessage("مقدار شماره سند اجباری میباشد")
                .When(v => v.IsRequestInSsar == false);

            RuleFor(v => v.DocumentDate)
                .NotEmpty().WithMessage("مقدار تاریخ سند اجباری میباشد");
        }
    }
}
