using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentEbookBaseInfo;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentEbookBaseInfo
{
    public class CreateDocumentEbookBaseInfoValidator : AbstractValidator<CreateDocumentEbookBaseInfoCommand>
    {
        public CreateDocumentEbookBaseInfoValidator()
        {
            RuleFor(x => x.LastClassifyNo)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار شماره ترتیب آخرین سند  ثبت شده در دفاتر كاغذی نامعتبر است")
                .MaximumLength(6).WithMessage("مقدار شماره ترتیب آخرین سند ثبت شده در دفاتر كاغذی بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار شماره ترتیب آخرین گواهی سند در دفاتر كاغذی اجباری است");

            RuleFor(x => x.LastRegisterVolumeNo)
                .MaximumLength(20).WithMessage("مقدار شماره جلد آخرین سند ثبت شده بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار شماره جلد آخرین سند ثبت شده اجباری است");

            RuleFor(x => x.LastRegisterPaperNo)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار شماره صفحه دفتر آخرین سند ثبت شده نامعتبر است")
                .MaximumLength(3).WithMessage("مقدار شماره صفحه دفتر آخرین سند ثبت شده بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار شماره صفحه دفتر آخرین سند ثبت شده اجباری است");

            RuleFor(x => x.NumberOfBooksJari)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار تعداد جلد دفتر جاری نامعتبر است")
                .MaximumLength(6).WithMessage("مقدار تعداد جلد دفتر جاری بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار تعداد جلد دفتر جاری اجباری است");

            RuleFor(x => x.NumberOfBooksVehicle)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار تعداد جلد دفتر اتومبیل نامعتبر است")
                .MaximumLength(6).WithMessage("مقدار تعداد جلد دفتر اتومبیل بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار تعداد جلد دفتر اتومبیل اجباری است");

            RuleFor(x => x.NumberOfBooksRahni)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار تعداد جلد دفتر رهنی نامعتبر است")
                .MaximumLength(6).WithMessage("مقدار تعداد جلد دفتر رهنی بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار تعداد جلد دفتر رهنی اجباری است");

            RuleFor(x => x.NumberOfBooksOghaf)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار تعداد جلد دفتر اوقاف نامعتبر است")
                .MaximumLength(6).WithMessage("مقدار تعداد جلد دفتر اوقاف بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار تعداد جلد دفتر اوقاف اجباری است");

            RuleFor(x => x.NumberOfBooksArzi)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار تعداد جلد دفتر اصلاحات ارضی نامعتبر است")
                .MaximumLength(6).WithMessage("مقدار تعداد جلد دفتر اصلاحات ارضی بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار تعداد جلد دفتر اصلاحات ارضی اجباری است");

            RuleFor(x => x.NumberOfBooksAgent)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار تعداد جلد دفتر نماینده نامعتبر است")
                .MaximumLength(6).WithMessage("مقدار تعداد جلد دفتر نماینده بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار تعداد جلد دفتر نماینده اجباری است");

            RuleFor(x => x.NumberOfBooksOthers)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار تعداد جلد سایر دفاتر حاوی سند نامعتبر است")
                .MaximumLength(6).WithMessage("مقدار تعداد جلد سایر دفاتر حاوی سند بیش از حد مجاز است")
                .NotEmpty().WithMessage("مقدار تعداد جلد سایر دفاتر حاوی سند اجباری است");
            RuleFor(x => x.TotalBooks)
               .Must((model, total) =>
               {
                   string[] numberFields = {
            model.NumberOfBooksJari,
            model.NumberOfBooksVehicle,
            model.NumberOfBooksRahni,
            model.NumberOfBooksOghaf,
            model.NumberOfBooksArzi,
            model.NumberOfBooksAgent,
            model.NumberOfBooksOthers
                   };

                   if (numberFields.Any(f => !ValidatorHelper.BeValidNumber(f)))
                       return false;

                   int ParseInt(string s) => int.TryParse(s, out int n) ? n : 0;

                   int sum = numberFields.Sum(f => ParseInt(f));
                   int totalValue = ParseInt(total);

                   return totalValue == sum;
               })
               .WithMessage("مقدار تعداد کل دفاتر دستنویس باید برابر با جمع تعداد دفاتر دیگر باشد و همه فیلدها باید عدد باشند");

        }
    }
}
