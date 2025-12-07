using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public sealed class CreateSignRequestValidator : AbstractValidator<CreateSignRequestCommand>
    {
        public CreateSignRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            ConfigureBaseRules();
            ConfigureSubjectRules();
            ConfigurePersonsRules();
            ConfigureRelatedPersonsRules();

        }

        //──────────────────────────────
        // Base Request Rules
        //──────────────────────────────
        private void ConfigureBaseRules()
        {
            RuleFor(v => v.IsNew)
                .Equal(true).WithMessage("نوع درخواست جدید نمی‌باشد.");

            RuleFor(v => v.IsDelete)
                .Equal(false).WithMessage("درخواست حذف مجاز نمی‌باشد.");

            RuleFor(v => v.SignRequestId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه گواهی امضا معتبر نمیباشد.");
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
                .ForEach(r =>
                    r.NotEmpty().WithMessage("شناسه موضوع گواهی امضا اجباری است."));
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
                        person.RuleFor(v => v.IsNew)
                            .Equal(true).WithMessage("نوع درخواست شخص جدید نمی‌باشد.");

                        person.RuleFor(v => v.IsDelete)
                            .Equal(false).WithMessage("درخواست حذف برای شخص نامعتبر است.");

                        // ── Base person info ──
                        person.RuleFor(x => x.PersonName)
                            .NotEmpty().WithMessage("فیلد نام اجباری است.")
                            .MaximumLength(150).WithMessage("طول نام بیش از حد مجاز است.");

                        person.RuleFor(x => x.PersonFamily)
                            .NotEmpty().WithMessage("فیلد نام خانوادگی اجباری است.")
                            .MaximumLength(100).WithMessage("طول نام خانوادگی بیش از حد مجاز است.");

                        person.RuleFor(x => x.PersonFatherName)
                            .NotEmpty().WithMessage("فیلد نام پدر اجباری است.")
                            .MaximumLength(100).WithMessage("طول نام پدر بیش از حد مجاز است.");

                        // ── Sex Type ──
                        person.RuleFor(x => x.PersonSexType)
                            .Cascade(CascadeMode.Stop)
                            .NotEmpty().WithMessage("جنسیت اجباری است.")
                            .Must(v => ValidatorHelper.ValidateRangeValue(v, 1, 2)).WithMessage("مقدار جنسیت غیر مجاز است.")
                            .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار جنسیت غیر مجاز است.");

                        // ── Iranian persons validations ──
                        person.When(x => x.IsPersonIranian, () =>
                        {
                            person.RuleFor(x => x.PersonNationalNo)
                                .NotEmpty().WithMessage("شماره ملی اجباری است.")
                                .Length(10).WithMessage("شماره ملی باید ۱۰ رقم باشد.");

                            person.RuleFor(x => x.PersonBirthDate)
                                .NotEmpty().WithMessage("تاریخ تولد اجباری است.")
                                .Must(ValidatorHelper.BeValidPersianDate)
                                .WithMessage("فرمت تاریخ تولد صحیح نیست.");

                            person.RuleFor(x => x.PersonIdentityNo)
                                .NotEmpty().WithMessage("شماره شناسنامه اجباری است.")
                                .MaximumLength(10).WithMessage("طول شماره شناسنامه بیش از حد مجاز است.")
                                .Must(ValidatorHelper.BeValidNumber)
                                .WithMessage("فرمت شماره شناسنامه اشتباه است.");

                            person.RuleFor(x => x.PersonIdentityIssueLocation)
                                .NotEmpty().WithMessage("محل صدور شناسنامه اجباری است.")
                                .MaximumLength(60).WithMessage("طول محل صدور شناسنامه بیش از حد مجاز است.");
                        });

                        // ── Non-Iranian persons ──
                        person.When(x => !x.IsPersonIranian, () =>
                        {
                            person.RuleFor(x => x.PersonNationalityId)
                                .NotNull().WithMessage("ملیت اجباری است.")
                                .Must(l => l.Count == 1)
                                    .WithMessage("تعداد ملیت غیر مجاز است.")
                                .ForEach(r =>
                                    r.NotEmpty().WithMessage("شناسه ملیت اجباری است.")
                                     .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار شناسه ملیت غیر مجاز است."));
                        });

                        // ── Contact info ──
                        person.RuleFor(x => x.PersonMobileNo)
                            .Cascade(CascadeMode.Stop)
                            .NotEmpty().WithMessage("شماره موبایل اجباری است.")
                            .Must(ValidatorHelper.BeValidMobileNumber).WithMessage("فرمت شماره موبایل اشتباه است.");

                        person.When(x => x.PersonTel.HasValue(), () =>
                        {
                            person.RuleFor(x => x.PersonTel)
                                .Must(ValidatorHelper.BeValidNumber)
                                .WithMessage("فرمت شماره تلفن اشتباه است.")
                                .MaximumLength(15).WithMessage("طول شماره تلفن بیش از حد مجاز است.");
                        });

                        person.When(x => x.PersonEmail.HasValue(), () =>
                        {
                            person.RuleFor(x => x.PersonEmail)
                                .EmailAddress().WithMessage("فرمت ایمیل اشتباه است.")
                                .MaximumLength(150).WithMessage("طول ایمیل بیش از حد مجاز است.");
                        });

                        // ── Address (4000 chars) ──
                        person.RuleFor(x => x.PersonAddress)
                            .Cascade(CascadeMode.Stop)
                            .NotEmpty().WithMessage("آدرس اجباری است.")
                            .MinimumLength(1).WithMessage("طول آدرس بسیار کوتاه است.")
                            .MaximumLength(4000).WithMessage("طول آدرس بیش از حد مجاز است (حداکثر ۴۰۰۰ کاراکتر).");

                        // ── Postal Code ──
                        person.RuleFor(x => x.PersonPostalCode)
                            .Cascade(CascadeMode.Stop)
                            .NotEmpty().WithMessage("کد پستی اجباری است.")
                            .Length(10).WithMessage("کد پستی باید ۱۰ رقم باشد.")
                            .Must(ValidatorHelper.BeValidNumber)
                            .WithMessage("کد پستی باید فقط شامل عدد باشد.");

                        // ── Description optional ──
                        person.When(x => x.PersonDescription.HasValue(), () =>
                        {
                            person.RuleFor(x => x.PersonDescription)
                                .MaximumLength(2000).WithMessage("مقدار توضیحات بیش از حد مجاز است.");
                        });
                    })
                    .When(x => x.SignRequestPersons?.Count > 0);
                });
        }

        //──────────────────────────────
        // Related Persons Rules
        //──────────────────────────────
        private void ConfigureRelatedPersonsRules()
        {
            RuleFor(x => x.SignRequestRelatedPersons)
                .NotNull().WithMessage("لیست اشخاص وابسته گواهی امضا غیر مجاز است.")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.SignRequestRelatedPersons).ChildRules(rp =>
                    {
                        rp.RuleFor(v => v.SignRequestId)
                            .NotEmpty().WithMessage("شناسه گواهی امضا اجباری است.")
                            .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه گواهی امضا غیر مجاز است.");

                        rp.RuleFor(x => x.IsDelete)
                            .Equal(false).WithMessage("نوع درخواست وابسته حذف غیر مجاز است.");

                        rp.RuleFor(x => x.MainPersonId)
                            .NotNull().WithMessage("شخص اصلی اجباری است.")
                            .Must(l => l.Count == 1).WithMessage("تعداد اشخاص اصلی غیر مجاز است.")
                            .ForEach(r =>
                                r.NotEmpty().WithMessage("شناسه شخص اصلی اجباری است.")
                                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه شخص اصلی غیر مجاز است."));

                        rp.RuleFor(x => x.RelatedAgentPersonId)
                            .NotNull().WithMessage("شخص نماینده اجباری است.")
                            .Must(l => l.Count == 1).WithMessage("تعداد اشخاص نماینده غیر مجاز است.")
                            .ForEach(r =>
                                r.NotEmpty().WithMessage("شناسه شخص نماینده اجباری است.")
                                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه شخص نماینده غیر مجاز است."));

                        rp.RuleFor(x => x.RelatedAgentTypeId)
                            .NotNull().WithMessage("نوع وابستگی اجباری است.")
                            .Must(l => l.Count == 1).WithMessage("تعداد انواع وابستگی غیر مجاز است.")
                            .ForEach(r => r.NotEmpty().WithMessage("شناسه نوع وابستگی اجباری است."));

                        rp.RuleFor(x => x.RelatedReliablePersonReasonId)
                            .NotNull().WithMessage("دلیل نیاز به معتمد اجباری است.");
                    })
                    .When(x => x.SignRequestRelatedPersons?.Count > 0);
                });
        }
    }
}
