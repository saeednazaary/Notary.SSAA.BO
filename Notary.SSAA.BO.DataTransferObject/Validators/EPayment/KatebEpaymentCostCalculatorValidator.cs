using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;

namespace Notary.SSAA.BO.DataTransferObject.Validators.EpaymentCostCalculator
{
    public class KatebEpaymentCostCalculatorValidator : AbstractValidator<KatebEpaymentCostCalculatorQuery>
    {
        public KatebEpaymentCostCalculatorValidator()
        {
            RuleFor(v => v.EpaymentUseCaseId).Cascade(CascadeMode.Stop).NotNull().WithMessage("خدمت اجباری می‌باشد");
            RuleFor(v => v.EpaymentUseCaseId).Must(v => v != null && v.Any()).WithMessage("خدمت اجباری می‌باشد");
            RuleFor(v => v.EpaymentUseCaseId.First()).Matches(@"^\d+$").WithMessage("مقدار ورودی خدمت نامعتبر می‌باشد");
            RuleFor(v => v.DocumentTypeId).NotNull().When(x => x.EpaymentUseCaseId.First() == "07").WithMessage("نوع سند اجباری می‌باشد");
            RuleFor(v => v.DocumentTypeId).Must(v => v != null && v.Any()).When(x => x.EpaymentUseCaseId.First() == "07").WithMessage("نوع سند اجباری می‌باشد");
            RuleFor(v => v.DocumentTypeId.First()).Matches(@"^\d+$").WithMessage("مقدار ورودی نوع سند نامعتبر می‌باشد")
           .When(v => v.DocumentTypeId != null && v.DocumentTypeId.Any());
            RuleFor(v => v.Elzam).Must(v => v == false)
           .When(v => v.Eghale == true || v.FinancialDocument == true || v.Cadastre == true)
           .WithMessage("در صورتی که سند دارای تبصره 2 ماده 1 قانون الزام باشد ، نمی‌تواند سایر شرایط سند را دارا باشد");
            RuleFor(v => v.Eghale).Must(v => v == false)
           .When(v => v.Elzam == true || v.FinancialDocument == true || v.Cadastre == true)
           .WithMessage("در صورتی که سند دارای اقاله بعد از 6 ماه باشد ، نمی‌تواند سایر شرایط سند را دارا باشد");
            RuleFor(v => v.FinancialDocument).Must(v => v == false)
           .When(v => v.Elzam == true || v.Eghale == true)
           .WithMessage("در صورتی که سند مالی باشد ، نمی‌تواند سایر شرایط سند(بجز کاداستر) را دارا باشد");
            RuleFor(v => v.Cadastre).Must(v => v == false)
           .When(v => v.Elzam == true || v.Eghale == true)
           .WithMessage("در صورتی که سند مالی باشد ، نمی‌تواند سایر شرایط سند(بجز مالی) را دارا باشد");
        }
    }
}
