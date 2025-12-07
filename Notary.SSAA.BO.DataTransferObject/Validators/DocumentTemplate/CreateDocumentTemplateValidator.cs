using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentTemplate
{
    public sealed class CreateDocumentTemplateValidator : AbstractValidator<CreateDocumentTemplateCommand>
    {
        public CreateDocumentTemplateValidator()
        {
            RuleFor(v => v.IsNew)
                .Must(x => x == true).WithMessage("نوع درخواست جدید نمیباشد");

            RuleFor(v => v.IsDelete)
                .Must(x => x == false).WithMessage("نوع درخواست حذف میباشد");

            RuleFor(v => v.DocumentTemplateStateId).Must(value => ValidatorHelper.ValidateRangeValue(value, 1, 2))
                .WithMessage("مقدار فیلد وضعیت غیر مجاز است");

            RuleFor(v => v.DocumentTemplateCode).Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد کد غیر مجاز است")
                .MaximumLength(5).WithMessage("طول مقدار فیلد کد بیش از حد مجاز است ");

            RuleFor(x => x.DocumentTypeId).Must(x => x != null && x.Count == 1).WithMessage("تعداد نوع سند غیر مجاز است ")
                .DependentRules(() =>
                {
                    RuleForEach(v => v.DocumentTypeId).Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد شناسه نوع سند غیر مجاز است")
                    .MaximumLength(4).WithMessage("طول مقدار فیلد شناسه نوع سند بیش از حد مجاز است ");
                });

            RuleFor(v => v.DocumentTemplateTitle).NotEmpty().WithMessage("فیلد عنوان اجباری است")
                .MaximumLength(100).WithMessage("طول مقدار فیلد عنوان بیش از حد مجاز است");
        }
    }
}
