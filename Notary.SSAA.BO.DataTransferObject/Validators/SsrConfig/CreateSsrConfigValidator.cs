using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrConfig;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SsrConfig
{

    public class CreateSsrConfigValidator : AbstractValidator<CreateSsrConfigCommand>
    {
        public CreateSsrConfigValidator()
        {
            // Main Subject Id list
            RuleFor(x => x.SsrConfigMainSubjectId)
                .NotNull().WithMessage("شناسه‌های موضوع اصلی نباید خالی باشد")
                .Must(list => list.Any()).WithMessage("شناسه‌های موضوع اصلی باید حداقل یک مورد داشته باشد");

            // Subject Id list
            RuleFor(x => x.SsrConfigSubjectId)
                .NotNull().WithMessage("شناسه‌های موضوع نباید خالی باشد")
                .Must(list => list.Any()).WithMessage("شناسه‌های موضوع باید حداقل یک مورد داشته باشد");

            //// StartDate
            RuleFor(x => x.ConfigStartDate)
                .NotEmpty().WithMessage("تاریخ شروع نباید خالی باشد")
                .Must(ValidatorHelper.BeValidPersianDate).WithMessage("فرمت تاریخ شروع معتبر نیست");

            //// EndDate
            RuleFor(x => x.ConfigEndDate)
                .NotEmpty().WithMessage("تاریخ پایان نباید خالی باشد")
                .Must(ValidatorHelper.BeValidPersianDate).WithMessage("فرمت تاریخ پایان معتبر نیست");

            //// Starttime
            RuleFor(x => x.ConfigStartTime)
                .NotEmpty().WithMessage("زمان شروع نباید خالی باشد");

            //// EndTime
            RuleFor(x => x.ConfigEndTime)
                .NotEmpty().WithMessage("تاریخ پایان نباید خالی باشد");

            // ConfigValue


            // ConditionValue
            RuleFor(x => x.ConditionValue)
                .MaximumLength(200).WithMessage("مقدار شرط نباید بیش از 200 کاراکتر باشد");
            
            // ConditionType
            RuleFor(x => x.ConditionType)
                .NotEmpty().WithMessage("نوع شرط نباید خالی باشد")
                .WithMessage("نوع شرط باید فقط شامل حروف و اعداد باشد");

            //// ActionType
            //RuleFor(x => x.ActionType)
            //    .NotEmpty().WithMessage("نوع اقدام نباید خالی باشد")
            //    .WithMessage("نوع اقدام باید فقط شامل حروف و اعداد باشد");

            // Optional boolean flags logic
            RuleFor(x => x.IsValid)
                .Equal(true).WithMessage("دستور باید معتبر باشد").When(x => x.IsNew);

        }


    }
}
