using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class CreateDocumentStandardContractValidator : AbstractValidator<CreateDocumentStandardContractCommand>
    {
        public CreateDocumentStandardContractValidator()
        {
            RuleFor(v => v.IsNew)
                .Must(x => x == true).WithMessage("نوع درخواست جدید نمیباشد");

            RuleFor(v => v.IsDelete)
                .Must(x => x == false).WithMessage("نوع درخواست حذف میباشد");

            RuleFor(x => x.DocumentTypeId).Must(x => x != null && x.Count == 1).WithMessage("تعداد نوع سند غیر مجاز است ")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.DocumentAssetTypeId)
                    .NotEmpty().WithMessage("شناسه نوع سند اجباری است ");
                });
            RuleFor(x => x.DocumentPeople)
            .Must(x => x != null)
            .WithMessage("مقدار اشخاص سند غیر مجاز است ")
             .DependentRules(() =>
             {
                 RuleForEach(x => x.DocumentPeople).ChildRules(order =>
                 {
                     order.When(x => x.IsNew, () =>
                     {
                         order.RuleFor(v => v.IsDelete && v.IsDirty)
                            .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                     });



                     order.RuleFor(x => x.PersonSexType)
                     .Must(value => ValidatorHelper.ValidateRangeValue(value, 1, 2)).WithMessage("مقدار فیلد جنسیت غیر مجاز است")
                     .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد جنسیت غیر مجاز است")
                     .MaximumLength(1).WithMessage("مقدار جنسیت غیر مجاز میباشد.")
                     .NotEmpty().WithMessage("جنسیت اجباری است.");

                     order.RuleFor(x => x.PersonBirthDate)
                     .Must(ValidatorHelper.BeValidPersianDate).WithMessage("مقدار تاریخ تولد غیر مجاز است")
                     .NotEmpty().WithMessage("فیلد تاریخ تولد اجباری است")
                     .When(x => x.PersonType == "1");
                     order.RuleFor(x => x.PersonBirthYear);
                   //.Must(ValidatorHelper.BeValidPersianYear).WithMessage("مقدار سال تولد غیر مجاز است");
                     order.RuleFor(x => x.PersonNationalNo).
                     Length(10).WithMessage("طول شماره ملی غیر مجاز است")
                     .NotEmpty().WithMessage("فیلد شماره ملی اجباری است")
                     .When(x => (x.IsPersonIranian && x.PersonType == "1" ) ||(x.IsPersonIranian && x.PersonType == "2" && x.PersonLegalPersonNatureid.First() == "2"));

                     order.RuleFor(x => x.PersonName).
                     MaximumLength(150).WithMessage("طول نام بیشتر از حد مجاز است ")
                     .NotEmpty().WithMessage("فیلد نام اجباری است")
                     .When(x => x.PersonType == "1");

                     order.RuleFor(x => x.PersonFamily)
                     .MaximumLength(50).WithMessage("طول نام خانوادگی بیشتر از حد مجاز است ")
                     .NotEmpty().WithMessage("فیلد نام خانوادگی اجباری است")
                     .When(x => x.PersonType == "1");
                     order.RuleFor(x => x.PersonFatherName)
                     .MaximumLength(50).WithMessage("طول نام پدر بیشتر از حد مجاز است ")
                     .NotEmpty().WithMessage("فیلد نام پدر اجباری است")
                     .When(x => x.PersonType == "1");
                     order.RuleFor(x => x.PersonIdentityNo)
                     .MaximumLength(10).WithMessage("طول شماره شناسنامه بیشتر از حد مجاز است ")
                     .Must(ValidatorHelper.BeValidNumber).WithMessage("فرمت شماره شناسنامه اشتباه است")
                     .NotEmpty().WithMessage(" فیلد شماره شناسنامه اجباری است")
                     .When(x => x.IsPersonIranian && x.PersonType == "1");

                     order.RuleFor(x => x.PersonIdentityIssueLocation)
                     .MaximumLength(50).WithMessage("طول محل صدور شناسنامه بیشتر از حد مجاز است ")
                     .NotEmpty().WithMessage(" فیلد محل صدور شناسنامه اجباری است")
                     .When(x => x.IsPersonIranian && x.PersonType == "1");

                     order.RuleFor(x => x.PersonSerial)
                     .Must(ValidatorHelper.BeValidNumber).WithMessage("طول سریال شناسنامه بیشتر از حد مجاز است ")
                     .MaximumLength(8).WithMessage("طول سریال شناسنامه بیشتر از حد مجاز است ")
                     .NotEmpty().WithMessage("فیلد سریال اجباری است")
                     .When(x => x.IsPersonIranian && x.PersonType == "1");

                     order.RuleFor(x => x.PersonPostalCode)
                     .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد کد پستی غیر مجاز است ")
                     .Length(10).WithMessage("طول کد پستی غیر مجاز است")
                     .NotNull().WithMessage("فیلد کد پستی اجباری است");

                     order.RuleFor(x => x.PersonMobileNo)
                     .Must(ValidatorHelper.BeValidMobileNumber).WithMessage("فرمت شماره موبایل اشتباه است")
                     .Length(11).WithMessage("طول شماره موبایل غیر مجاز است")
                     .NotNull().WithMessage("فیلد شماره موبایل اجباری است");

                     //order.RuleFor(x => x.PersonTel)
                     //.Must(ValidatorHelper.BeValidNumber).WithMessage("فرمت شماره تلفن اشتباه است.")
                     //.When(x => x.PersonTel.HasValue());

                     order.RuleFor(x => x.PersonEmail)
                     .MaximumLength(100).WithMessage("طول ایمیل بیش از حد مجاز است")
                     .EmailAddress().WithMessage("فرمت ایمیل اشتباه است.")
                     .When(x => x.PersonEmail.HasValue());

                     order.RuleFor(x => x.PersonDescription)
                     .MaximumLength(2000).WithMessage("مقدار توضیحات شخص بیش از حد مجاز است.")
                     .When(x => x.PersonDescription.HasValue());

                     order.RuleFor(x => x.PersonAddress)
                     .MaximumLength(200).WithMessage("طول آدرس بیشتر از حد مجاز است ")
                     .NotNull().WithMessage("فیلد آدرس اجباری است")
                     .When(x => x.PersonAddress.HasValue());
                     order.RuleFor(x => x.CompanyRegisterNo)
                     .MaximumLength(50).WithMessage("طول شماره ثبت بیشتر از حد مجاز است ")
                     .NotNull().WithMessage("فیلد شماره ثبت اجباری است")
                     .When(x => x.CompanyRegisterNo.HasValue() && (x.PersonLegalPersonNatureid.First()=="2" && x.PersonType=="2"));
                     order.RuleForEach(x => x.PersonNationalityId)
                     .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد ملیت غیر مجاز است")
                     .MaximumLength(6).WithMessage("مقدار ملیت غیر مجاز میباشد.")
                     .NotEmpty().WithMessage("ملیت اجباری است. ")
                     .When(x => x.PersonNationalityId.Count == 1).WithMessage("تعداد اشخاص اصلی غیر مجاز میباشد .")
                     .When(x => !x.IsPersonIranian);

                     order.RuleFor(x => x.PersonAlphabetSeri)
                     .Must(x => int.TryParse(x, out int alphabet) && alphabet >= 1 && alphabet <= 32)
                     .WithMessage("مقدار سری الفبایی شناسنامه غیر مجاز است")
                     .When(x => x.IsPersonIranian && x.PersonType == "1");


                     order.RuleFor(x => x.CompanyRegisterDate)
                     .Must(ValidatorHelper.BeValidPersianDate).WithMessage("مقدار تاریخ ثبت غیر مجاز است")
                     .NotEmpty().WithMessage("فیلد تاریخ ثبت اجباری است")
                     .When(x => (x.PersonType == "2" && x.PersonLegalPersonNatureid.First() == "2"));

                     order.RuleFor(x => x.CompanyRegisterLocation)
                    .MaximumLength(50).WithMessage("طول شماره ثبت بیشتر از حد مجاز است ")
                    .NotNull().WithMessage("فیلد شماره ثبت اجباری است")
                     .When(x => x.CompanyRegisterNo.HasValue() && (x.PersonLegalPersonNatureid.First() == "2" && x.PersonType == "2"));

                 }).When(x => x.DocumentPeople?.Count > 0);

             });

            RuleFor(x => x.DocumentRelatedPeople)
            .Must(x => x != null)
            .WithMessage("مقدار اشخاص وابسته سند غیر مجاز است ")
            .DependentRules(() =>
            {
                RuleForEach(x => x.DocumentRelatedPeople).ChildRules(order =>
                {
                    order.RuleFor(v => v.DocumentId)
                    .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند اجباری است ")
                    .NotEmpty().WithMessage("شناسه سند اجباری است.");

                    order.When(x => x.IsNew, () =>
                    {
                        order.RuleFor(v => v.IsDelete)
                           .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                    });

           

                    order.RuleFor(x => x.MainPersonId)
                    .Must(x => x != null && x.Count == 1).WithMessage("تعداد اشخصاص اصلی شخص غیر مجاز میباشد ").ChildRules(order =>
                    {
                        order.RuleForEach(x => x)
                        .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه شخص اصلی غیر مجاز است . ")
                        .NotEmpty().WithMessage("شناسه شخص اصلی اجباری است");
                    });

                    order.RuleFor(x => x.RelatedAgentPersonId)
                    .Must(x => x != null && x.Count == 1).WithMessage("تعداد اشخاص نماینده غیر مجاز میباشد .").ChildRules(order =>
                    {
                        order.RuleForEach(x => x)
                        .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه شخص نماینده غیر مجاز است . ")
                        .NotEmpty().WithMessage("شناسه شخص نماینده اجباری است . ");
                    });

                    order.RuleFor(x => x.RelatedAgentTypeId)
                    .Must(x => x != null && x.Count == 1).WithMessage("تعداد انواع وابستگی غیر مجاز میباشد .").ChildRules(order =>
                    {
                        order.RuleForEach(x => x)
                        .NotEmpty().WithMessage("شناسه نوع وابستگی اجباری است . ");
                    });
                    order.RuleFor(x => x.RelatedReliablePersonReasonId)
                    .NotNull().WithMessage("فیلد دلیل نیاز به معتمد غیر مجاز است . ");

                    order.RuleFor(x => x.RelatedAgentDocumentScriptoriumId)
                    .NotNull().WithMessage("فیلد دفترخانه  غیر مجاز است . ");

                }).When(x => x.DocumentRelatedPeople?.Count > 0);

            });

        }
    }
}

