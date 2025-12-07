using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public sealed class UpdateSignRequestValidator : AbstractValidator<UpdateSignRequestCommand>
    {
        public UpdateSignRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ConfigureRequestRules();
            ConfigureSubjectRules();
            ConfigurePersonsRules();
            ConfigureRelatedPersonsRules();

        }

        //──────────────────────────────
        // Base Request Rules
        //──────────────────────────────
        private void ConfigureRequestRules()
        {
            RuleFor(x => x.IsNew)
                .Equal(false).WithMessage("درخواست جدید معتبر نیست.");

            RuleFor(x => x.IsDelete)
                .Equal(false).WithMessage("درخواست حذف معتبر نیست.");

            RuleFor(x => x.SignRequestId)
                .NotEmpty().WithMessage("شناسه گواهی امضا اجباری است.")
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه گواهی امضا غیر مجاز است.");
        }

        //──────────────────────────────
        // Subject Rules
        //──────────────────────────────
        private void ConfigureSubjectRules()
        {
            RuleFor(x => x.SignRequestSubjectId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("موضوع گواهی امضا الزامی است.")
                .Must(list => list.Count == 1)
                    .WithMessage("تعداد موضوعات گواهی امضا باید دقیقاً یک مورد باشد.")
                .ForEach(rule =>
                    rule.NotEmpty().WithMessage("شناسه موضوع گواهی امضا اجباری است.")
                );
        }

        //──────────────────────────────
        // Persons Rules
        //──────────────────────────────
        private void ConfigurePersonsRules()
        {
            RuleFor(x => x.SignRequestPersons)
                .NotNull().WithMessage("لیست اشخاص گواهی امضا غیر مجاز است.")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.SignRequestPersons).ChildRules(person =>
                    {
                        person.RuleFor(p => p.SignRequestId)
                            .NotEmpty().WithMessage("شناسه گواهی امضا اجباری است.")
                            .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه گواهی امضا غیر مجاز است.");

                        // Delete mode
                        person.When(x => x.IsDelete, () =>
                        {
                            person.RuleFor(x => x.IsNew)
                                .Equal(false).WithMessage("حالت حذف برای شخص نامعتبر است.");

                            person.RuleFor(x => x.PersonId)
                                .NotEmpty().WithMessage("شناسه شخص اجباری است.")
                                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه شخص غیر مجاز است.");
                        });

                        // New or Dirty
                        person.When(x => !x.IsDelete && (x.IsNew || x.IsDirty), () =>
                        {
                            person.RuleFor(x => x.PersonName)
                                .NotEmpty().WithMessage("نام اجباری است.")
                                .MaximumLength(150).WithMessage("طول نام بیش از حد مجاز است.");

                            person.RuleFor(x => x.PersonFamily)
                                .NotEmpty().WithMessage("نام خانوادگی اجباری است.")
                                .MaximumLength(100).WithMessage("طول نام خانوادگی بیش از حد مجاز است.");

                            person.RuleFor(x => x.PersonFatherName)
                                .NotEmpty().WithMessage("نام پدر اجباری است.")
                                .MaximumLength(100).WithMessage("طول نام پدر بیش از حد مجاز است.");

                            person.RuleFor(x => x.PersonSexType)
                                .NotEmpty().WithMessage("جنسیت اجباری است.")
                                .Must(v => ValidatorHelper.ValidateRangeValue(v, 1, 2))
                                    .WithMessage("مقدار جنسیت غیر مجاز است.")
                                .Must(ValidatorHelper.BeValidNumber)
                                    .WithMessage("مقدار جنسیت غیر مجاز است.");

                            // Iranian persons
                            person.When(p => p.IsPersonIranian, () =>
                            {
                                person.RuleFor(x => x.PersonNationalNo)
                                    .NotEmpty().WithMessage("شماره ملی اجباری است.")
                                    .Length(10).WithMessage("طول شماره ملی باید ۱۰ رقم باشد.");

                                person.RuleFor(x => x.PersonBirthDate)
                                    .NotEmpty().WithMessage("تاریخ تولد اجباری است.")
                                    .Must(ValidatorHelper.BeValidPersianDate)
                                    .WithMessage("فرمت تاریخ تولد اشتباه است.");

                                person.RuleFor(x => x.PersonIdentityNo)
                                    .NotEmpty().WithMessage("شماره شناسنامه اجباری است.")
                                    .MaximumLength(10).WithMessage("طول شماره شناسنامه بیش از حد مجاز است.")
                                    .Must(ValidatorHelper.BeValidNumber).WithMessage("فرمت شماره شناسنامه اشتباه است.");

                                person.RuleFor(x => x.PersonIdentityIssueLocation)
                                    .NotEmpty().WithMessage("محل صدور شناسنامه اجباری است.")
                                    .MaximumLength(60).WithMessage("طول محل صدور شناسنامه بیش از حد مجاز است.");
                            });

                            // Mobile
                            person.RuleFor(x => x.PersonMobileNo)
                                .NotEmpty().WithMessage("شماره موبایل اجباری است.")
                                .Must(ValidatorHelper.BeValidMobileNumber).WithMessage("فرمت شماره موبایل اشتباه است.");

                            // Tel optional
                            person.When(x => !string.IsNullOrWhiteSpace(x.PersonTel), () =>
                            {
                                person.RuleFor(x => x.PersonTel)
                                    .Must(ValidatorHelper.BeValidNumber).WithMessage("فرمت تلفن ثابت اشتباه است.")
                                    .MaximumLength(15).WithMessage("طول تلفن بیش از حد مجاز است.");
                            });

                            // Email optional
                            person.When(x => !string.IsNullOrWhiteSpace(x.PersonEmail), () =>
                            {
                                person.RuleFor(x => x.PersonEmail)
                                    .EmailAddress().WithMessage("فرمت ایمیل اشتباه است.")
                                    .MaximumLength(150).WithMessage("طول ایمیل بیش از حد مجاز است.");
                            });

                            person.RuleFor(x => x.PersonAddress)
                                .Cascade(CascadeMode.Stop)
                                .NotEmpty().WithMessage("آدرس اجباری است.")
                                .MinimumLength(1).WithMessage("طول آدرس بسیار کوتاه است.")
                                .MaximumLength(4000).WithMessage("طول آدرس بیش از حد مجاز است (حداکثر ۴۰۰۰ کاراکتر).");

                            // Postal Code
                            person.RuleFor(x => x.PersonPostalCode)
                                .NotEmpty().WithMessage("کد پستی اجباری است.")
                                .Length(10).WithMessage("طول کد پستی باید ۱۰ رقم باشد.")
                                .Must(code => code.All(char.IsDigit)).WithMessage("کد پستی باید فقط شامل اعداد باشد.");

                            // Description optional
                            person.When(x => !string.IsNullOrWhiteSpace(x.PersonDescription), () =>
                            {
                                person.RuleFor(x => x.PersonDescription)
                                    .MaximumLength(2000).WithMessage("طول توضیحات بیش از حد مجاز است.");
                            });

                            // Non-Iranian
                            person.When(x => !x.IsPersonIranian, () =>
                            {
                                person.RuleFor(x => x.PersonNationalityId)
                                    .NotNull().WithMessage("ملیت اجباری است.")
                                    .Must(l => l.Count == 1).WithMessage("تعداد ملیت غیر مجاز است.")
                                    .ForEach(rule =>
                                    {
                                        rule.NotEmpty().WithMessage("شناسه ملیت اجباری است.")
                                            .Must(ValidatorHelper.BeValidNumber).WithMessage("شناسه ملیت غیر مجاز است.");
                                    });
                            });
                        });
                    });
                });
        }

        //──────────────────────────────
        // Related Persons Rules
        //──────────────────────────────
        private void ConfigureRelatedPersonsRules()
        {
            RuleFor(x => x.SignRequestRelatedPersons)
                .Must(x => x != null)
                .WithMessage("مقدار اشخاص وابسته گواهی امضا غیر مجاز است ")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.SignRequestRelatedPersons)
                        .Cascade(CascadeMode.Stop)
                        .ChildRules(order =>
                        {
                            // --- SignRequestId ---
                            order.RuleFor(v => v.SignRequestId)
                                .NotEmpty().WithMessage("شناسه گواهی امضا اجباری است.")
                                .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه گواهی امضا اجباری است ");

                            // --- If new ---
                            order.When(x => x.IsNew, () =>
                            {
                                order.RuleFor(v => v.IsDelete)
                                    .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                            });

                            // --- If dirty but not new ---
                            order.When(x => x.IsDirty && x.IsNew == false, () =>
                            {
                                order.RuleFor(v => v.RelatedPersonId)
                                    .NotEmpty().WithMessage("شناسه شخص وابسته اجباری است.")
                                    .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه شخص اجباری است ");
                            });

                            // --- If deleting ---
                            order.When(x => x.IsDelete, () =>
                            {
                                order.RuleFor(v => v.IsNew && v.IsDirty)
                                    .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                            });

                            // --- Active (not deleted) and IsDirty or IsNew ---
                            order.When(x => x.IsDelete == false && (x.IsDirty || x.IsNew), () =>
                            {
                                // --- MainPersonId ---
                                order.RuleFor(x => x.MainPersonId)
                                    .Must(x => x != null && x.Count == 1)
                                    .WithMessage("تعداد اشخصاص اصلی شخص غیر مجاز میباشد ")
                                    .ChildRules(inner =>
                                    {
                                        inner.RuleForEach(x => x)
                                            .NotEmpty().WithMessage("شناسه شخص اصلی اجباری است")
                                            .Must(ValidatorHelper.BeValidGuid)
                                            .WithMessage("مقدار شناسه شخص اصلی غیر مجاز است . ");
                                    });

                                // --- RelatedAgentPersonId ---
                                order.RuleFor(x => x.RelatedAgentPersonId)
                                    .Must(x => x != null && x.Count == 1)
                                    .WithMessage("تعداد اشخاص نماینده غیر مجاز میباشد .")
                                    .ChildRules(inner =>
                                    {
                                        inner.RuleForEach(x => x)
                                            .NotEmpty().WithMessage("شناسه شخص نماینده اجباری است . ")
                                            .Must(ValidatorHelper.BeValidGuid)
                                            .WithMessage("مقدار شناسه شخص نماینده غیر مجاز است . ");
                                    });

                                // --- RelatedAgentTypeId ---
                                order.RuleFor(x => x.RelatedAgentTypeId)
                                    .Must(x => x != null && x.Count == 1)
                                    .WithMessage("تعداد انواع وابستگی غیر مجاز میباشد .")
                                    .ChildRules(inner =>
                                    {
                                        inner.RuleForEach(x => x)
                                            .NotEmpty().WithMessage("شناسه نوع وابستگی اجباری است . ");
                                    });

                                // --- Reason ---
                                order.RuleFor(x => x.RelatedReliablePersonReasonId)
                                    .NotNull().WithMessage("فیلد دلیل نیاز به معتمد غیر مجاز است . ");
                            });
                        })
                        .When(x => x.SignRequestRelatedPersons != null && x.SignRequestRelatedPersons.Count > 0);
                });
        }
    }
}
