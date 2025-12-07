using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentTemplate
{
    public sealed class UpdateDocumentTemplateValidator : AbstractValidator<UpdateDocumentTemplateCommand>
    {
        public UpdateDocumentTemplateValidator()
        {
            RuleFor(v => v.IsNew)
                .Must(x => x == false).WithMessage("نوع درخواست جدید میباشد");

            RuleFor(v => v.IsDelete)
                .Must(x => x == false).WithMessage("نوع درخواست حذف میباشد");

            RuleFor(v => v.IsDirty)
                .Must(x => x == true).WithMessage("نوع درخواست ویرایش نمیباشد");

            RuleFor(v => v.DocumentTemplateStateId).Must(value => ValidatorHelper.ValidateRangeValue(value, 1, 2))
                .WithMessage("مقدار فیلد شناسه غیر مجاز است");

            RuleFor(v => v.DocumentTemplateId).Must(ValidatorHelper.BeValidGuid)
                .WithMessage("مقدار فیلد وضعیت غیر مجاز است").When(x => x.ScriptoriumId != null);

            RuleFor(v => v.DocumentTemplateCode).Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد کد غیر مجاز است")
                .MaximumLength(5).WithMessage("طول مقدار فیلد کد بیش از حد مجاز است ").When(x => x.ScriptoriumId != null);

            RuleFor(x => x.DocumentTypeId).Must(x => x != null && x.Count == 1).WithMessage("تعداد نوع سند غیر مجاز است  ").When(x => x.ScriptoriumId != null)
                .DependentRules(() =>
                {
                    RuleForEach(v => v.DocumentTypeId).Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد شناسه نوع سند غیر مجاز است")
                    .MaximumLength(4).WithMessage("طول مقدار فیلد شناسه نوع سند بیش از حد مجاز است ");
                });

            RuleFor(v => v.DocumentTemplateTitle).NotEmpty().WithMessage("فیلد عنوان اجباری است")
                .MaximumLength(100).WithMessage("طول مقدار فیلد عنوان بیش از حد مجاز است").When(x => x.ScriptoriumId != null);


        }
    }
}
