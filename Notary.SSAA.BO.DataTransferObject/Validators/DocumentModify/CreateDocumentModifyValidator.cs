using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentModify;
using Notary.SSAA.BO.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentModify
{
    public class CreateDocumentModifyValidator : AbstractValidator<CreateDocumentModifyCommand>
    {
        public CreateDocumentModifyValidator()
        {
            // قوانین منطقی درخواست
            RuleFor(x => x.IsNew)
                .Equal(true).WithMessage("نوع درخواست باید جدید باشد");

            RuleFor(x => x.IsValid)
                .Equal(true).WithMessage("درخواست معتبر نمی‌باشد");

            RuleFor(x => x.IsDelete)
                .Equal(false).WithMessage("درخواست حذف مجاز نیست");


            // DocumentId
            RuleFor(x => x.DocumentId)
                  .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه سند معتبر نیست")
                   .NotEmpty().WithMessage("شناسه سند اجباری است");

            // ClassifyNoOld → NOT NULL
            RuleFor(x => x.ClassifyNoOld)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("شماره ترتیب قبلی باید عددی باشد")
                .Must(v => ValidatorHelper.ValidateRangeValue(v, 1, 999999)).WithMessage("شماره ترتیب قبلی باید بین 1 تا 999999 باشد")
                .NotEmpty().WithMessage("شماره ترتیب قبلی اجباری است");

            // ClassifyNoNew → nullable
            RuleFor(x => x.ClassifyNoNew)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("شماره ترتیب جدید باید عددی باشد")
                .Must(v => ValidatorHelper.ValidateRangeValue(v, 1, 999999)).WithMessage("شماره ترتیب جدید باید بین 1 تا 999999 باشد")
                .NotEmpty().WithMessage("شماره ترتیب جدید اجباری است");

            // WriteInBookDateOld
            RuleFor(x => x.WriteInBookDateOld)
                .Must(ValidatorHelper.BeValidPersianDate).WithMessage("تاریخ قبلی ثبت در دفتر نامعتبر است")
                .NotEmpty().WithMessage("تاریخ قبلی ثبت در دفتر اجباری است");

            // WriteInBookDateNew
            RuleFor(x => x.WriteInBookDateNew)
                .Must(ValidatorHelper.BeValidPersianDate).WithMessage("تاریخ جدید ثبت در دفتر نامعتبر است")
                .When(v => !string.IsNullOrWhiteSpace(v.WriteInBookDateNew));

            // RegisterVolumeNoOld
            RuleFor(x => x.RegisterVolumeNoOld)
                .MaximumLength(20).WithMessage("طول شماره جلد قبلی بیشتر از حد مجاز است")
                .NotEmpty().WithMessage("شماره جلد قبلی دفتر اجباری است");

            // RegisterVolumeNoNew
            RuleFor(x => x.RegisterVolumeNoNew)
                .MaximumLength(20).WithMessage("طول شماره جلد جدید بیشتر از حد مجاز است")
                .NotEmpty().WithMessage("طول شماره جلد جدید اجباری است");

            // RegisterPagesNoOld
            RuleFor(x => x.RegisterPagesNoOld)
                .MaximumLength(20).WithMessage("طول شماره صفحات قبلی بیشتر از حد مجاز است")
                .NotEmpty().WithMessage("شماره صفحات قبلی دفتر اجباری است");

            // RegisterPagesNoNew
            RuleFor(x => x.RegisterPagesNoNew)
                .MaximumLength(20).WithMessage("طول شماره صفحات جدید بیشتر از حد مجاز است")
                .NotEmpty().WithMessage("طول شماره صفحات جدید اجباری است");

        }
    }
}
