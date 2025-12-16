using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract
{
    public class SaveDocumentStandardContractValidator : AbstractValidator<SaveDocumentStandardContractCommand>
    {
        public SaveDocumentStandardContractValidator()
        {
            //RuleFor(v => v.IsNew)
            //    .Must(x => x == false).WithMessage("نوع درخواست جدید نمیباشد");

            RuleFor(v => v.IsDelete)
                .Must(x => x == false).WithMessage("نوع درخواست حذف میباشد");

            RuleFor(x => x.DocumentTypeId).Must(x => x != null && x.Count == 1).WithMessage("تعداد نوع سند غیر مجاز است ")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.DocumentAssetTypeId)
                    .NotEmpty().WithMessage("شناسه نوع سند اجباری است ");
                });

            RuleFor(x => x.RequestId)

                .NotEmpty().WithMessage("مقدار شناسه سند اجباری است ")
                .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند غیر مجاز است ")
                .When(x => x.IsNew == false);

            RuleFor(x => (int)x.StateId)
            .InclusiveBetween(0, 9).WithMessage("وضعیت سند نامعتبر است");

            RuleFor(x => x.DocumentPeople)
            .Must(x => x != null)
            .WithMessage("مقدار اشخاص سند غیر مجاز است ")
             .DependentRules(() =>
             {
                 RuleForEach(x => x.DocumentPeople).ChildRules(order =>
                 {

                     order.When(x => x.IsDirty && !x.IsNew, () =>
                     {
                         order.RuleFor(v => v.DocumentId)
                     .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند شخص غیر مجاز است . ")
                     .NotEmpty().WithMessage("شناسه سند اجباری است.");
                     });

                     order.When(x => x.IsNew, () =>
                     {
                         order.RuleFor(v => v.IsDelete && v.IsDirty)
                            .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                     });

                     order.When(x => x.IsDirty && !x.IsNew && !x.IsDelete, () =>
                     {
                         order.RuleFor(v => v.PersonId)
                         .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه شخص غیر مجاز است .")
                         .NotEmpty().WithMessage("شناسه سند اجباری است.");
                     });

                     order.When(x => x.IsDelete, () =>
                     {
                         order.RuleFor(v => v.IsNew)
                            .Must(x => x == false).WithMessage("درخواست نامعتبر است");

                         order.RuleFor(v => v.PersonId)
                         .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه شخص غیر مجاز است .")
                         .NotEmpty().WithMessage("شناسه سند اجباری است.");

                     });

                     order.When(x => x.IsDelete == false && (x.IsDirty == true || x.IsNew == true), () =>
                     {
                         //order.RuleFor(x => x.PersonSexType)
                         //.Must(value => ValidatorHelper.ValidateRangeValue(value, 1, 2)).WithMessage("مقدار فیلد جنسیت غیر مجاز است")
                         //.When(x => x.PersonType == "1" && false)
                         //.Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد جنسیت غیر مجاز است")
                         //.When(x => x.PersonType == "1" && false)
                         //.MaximumLength(1).WithMessage("مقدار جنسیت غیر مجاز میباشد.")
                         //.NotEmpty().WithMessage("جنسیت اجباری است.")
                         // .When(x => x.PersonType == "1" && false);
                         order.RuleFor(x => x.PersonBirthDate)
                         .Must(ValidatorHelper.BeValidPersianDate).WithMessage("مقدار تاریخ تولد غیر مجاز است")
                         .When(x => x.PersonType == "1")
                         .NotEmpty().WithMessage("فیلد تاریخ تولد اجباری است")
                         .When(x => x.IsPersonIranian && x.PersonType == "1");
                         order.RuleFor(x => x.PersonBirthYear)
                 //  .Must(ValidatorHelper.BeValidPersianYear).WithMessage("مقدار سال  تولد غیر مجاز است")
                 ;

                         order.RuleFor(x => x.PersonNationalNo).
                         Length(10).WithMessage("طول شماره ملی غیر مجاز است")
                         .When(x => x.PersonType == "1")
                         .NotEmpty().WithMessage("فیلد شماره ملی اجباری است")
                         .When(x => x.IsPersonIranian && x.PersonType == "1");

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
                         .When(x => x.PersonType == "1")
                         .NotEmpty().WithMessage(" فیلد شماره شناسنامه اجباری است")
                         .When(x => x.IsPersonIranian && x.PersonType == "1");

                         order.RuleFor(x => x.PersonIdentityIssueLocation)
                         .MaximumLength(50).WithMessage("طول محل صدور شناسنامه بیشتر از حد مجاز است ")
                         .NotEmpty().WithMessage(" فیلد محل صدور شناسنامه اجباری است")
                         .When(x => x.IsPersonIranian && x.PersonType == "1");

                         //order.RuleFor(x => x.PersonSerial)
                         //.Must(ValidatorHelper.BeValidNumber).WithMessage("طول سریال شناسنامه بیشتر از حد مجاز است ")
                         //.When(x => x.PersonType == "1")
                         //.MaximumLength(8).WithMessage("طول سریال شناسنامه بیشتر از حد مجاز است ")
                         ////.NotEmpty().WithMessage("فیلد سریال اجباری است")
                         //.When(x => x.IsPersonIranian && x.PersonType == "1");

                         order.RuleFor(x => x.PersonPostalCode)
                         .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد کد پستی غیر مجاز است ")
                         .Length(10).WithMessage("طول کد پستی غیر مجاز است")
                         .NotNull().WithMessage("فیلد کد پستی اجباری است");

                         order.RuleFor(x => x.PersonMobileNo)
                         .Must(ValidatorHelper.BeValidMobileNumber).WithMessage("فرمت شماره موبایل اشتباه است")
                         .Length(11).WithMessage("طول شماره موبایل غیر مجاز است")
                         .NotNull().WithMessage("فیلد شماره موبایل اجباری است");
                         order.RuleFor(x => x.PersonFaxNo)
                        //.Must(ValidatorHelper.BeValidFaxNo).WithMessage("فرمت شماره فکس اشتباه است")
                        .MaximumLength(50).WithMessage("طول شماره فکس غیر مجاز است")
                        .When(x => x.PersonFaxNo.HasValue());
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
                         .MaximumLength(300).WithMessage("طول آدرس بیشتر از حد مجاز است ")
                         .NotNull().WithMessage("فیلد آدرس اجباری است")
                         .When(x => x.PersonAddress.HasValue());

                         order.RuleForEach(x => x.PersonNationalityId)
                         .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار فیلد ملیت غیر مجاز است")
                         .MaximumLength(6).WithMessage("مقدار ملیت غیر مجاز میباشد.")
                         .NotEmpty().WithMessage("ملیت اجباری است. ")
                         .NotNull().WithMessage("ملیت اجباری است. ")
                         .When(x => (x.PersonNationalityId != null && x.PersonNationalityId.Count == 1)).WithMessage("ملیت اجباری است.")
                         .When(x => !x.IsPersonIranian);

                         order.RuleFor(x => x.PersonAlphabetSeri)
                         .Must(x => string.IsNullOrEmpty(x) || (int.TryParse(x, out int alphabet) && alphabet >= 1 && alphabet <= 32))
                         .When(x => x.PersonType == "1")
                         .WithMessage("مقدار سری الفبایی شناسنامه غیر مجاز است")
                         .When(x => x.IsPersonIranian && x.PersonType == "1");

                     });

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
                    order.When(x => x.IsDirty && !x.IsNew, () =>
                    {
                        order.RuleFor(v => v.DocumentId)
                .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند اجباری است ")
                .NotEmpty().WithMessage("شناسه سند اجباری است.");
                    });
                    order.When(x => x.IsNew, () =>
                    {
                        order.RuleFor(v => v.IsDelete)
                           .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                    });

                    order.When(x => x.IsDirty && x.IsNew == false, () =>
                    {
                        order.RuleFor(v => v.RelatedPersonId)
                        .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه شخص اجباری است ")
                        .NotEmpty().WithMessage("شناسه شخص وابسته اجباری است.");
                    });

                    order.When(x => x.IsDelete, () =>
                    {
                        order.RuleFor(v => v.IsNew && v.IsDirty)
                           .Must(x => x == false).WithMessage("درخواست نامعتبر است");

                    });
                    order.When(x => x.IsDelete == false && (x.IsDirty == true || x.IsNew == true), () =>
                    {
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
                        order.RuleFor(x => x.RelatedPersonDescription)
                        .MaximumLength(1000).WithMessage("مقدار توضیحات  بیش از حد مجاز است.");

                    });

                }).When(x => x.DocumentRelatedPeople?.Count > 0);

            });


            RuleFor(x => x.DocumentCosts)
          .Must(x => x != null)
          .WithMessage("مقدار هزینه سند غیر مجاز است ")
          .DependentRules(() =>
          {
              RuleForEach(x => x.DocumentCosts).ChildRules(order =>
              {

                  order.When(x => x.IsDirty && !x.IsNew, () =>
                  {
                      order.RuleFor(v => v.DocumentId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند اجباری است ")
                 .NotEmpty().WithMessage("شناسه سند اجباری است.");
                  });
                  order.When(x => x.IsNew, () =>
                  {
                      order.RuleFor(v => v.IsDelete)
                         .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                  });

                  order.When(x => x.IsDirty && x.IsNew == false, () =>
                  {
                      order.RuleFor(v => v.RequestId)
                      .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه هزینه است ")
                      .NotEmpty().WithMessage("شناسه هزینه اجباری است.");
                  });

                  order.When(x => x.IsDelete, () =>
                  {
                      order.RuleFor(v => v.IsNew && v.IsDirty)
                         .Must(x => x == false).WithMessage("درخواست نامعتبر است");

                  });
                  order.RuleFor(x => x.RequestPrice)
                .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار هزینه نامعتبر است ")
                .MaximumLength(15).WithMessage("مقدار هزینه بیش از حد مجاز است ")
                .NotEmpty().WithMessage("مقدار هزینه اجباری است");
              }).When(x => x.DocumentCosts?.Count > 0);

          });
            RuleFor(x => x.DocumentPayments)
     .Must(x => x != null)
     .WithMessage("مقدار هزینه پرداخت غیرمجاز است")
     .DependentRules(() =>
     {
         RuleForEach(x => x.DocumentPayments).ChildRules(order =>
         {
             order.RuleFor(v => v.DocumentId)
             .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند اجباری است ")
             .NotEmpty().WithMessage("شناسه سند اجباری است.");

             order.When(x => x.IsNew, () =>
             {
                 order.RuleFor(v => v.IsDelete)
                    .Must(x => x == false).WithMessage("درخواست نامعتبر است");
             });

             order.When(x => x.IsDirty && x.IsNew == false, () =>
             {
                 order.RuleFor(v => v.Id)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه پرداخت نامعتبر است ")
                 .NotEmpty().WithMessage("شناسه پرداخت اجباری است.");
             });

             order.When(x => x.IsDelete, () =>
             {
                 order.RuleFor(v => v.IsNew && v.IsDirty)
                    .Must(x => x == false).WithMessage("درخواست نامعتبر است");
             });


             order.When(x => !x.BankCounterInfo.IsNullOrEmpty(), () =>
             {
                 order.RuleFor(x => x.BankCounterInfo.ToString())
                 .MaximumLength(200).WithMessage("مقدار کد و نام شعبه بانک محل پرداخت بیش از حد مجاز است ");
             });

             order.When(x => !x.HowToQuotation.IsNullOrEmpty(), () =>
             {
                 order.RuleFor(x => x.HowToQuotation.ToString())
                 .MaximumLength(2000).WithMessage("مقدار شیوه تسهیم بیش از حد مجاز است ");
             });

             order.When(x => !x.CardNo.IsNullOrEmpty(), () =>
             {
                 order.RuleFor(x => x.CardNo.ToString())
                .MaximumLength(100).WithMessage("مقدار شماره کارت بیش از حد مجاز است ");
             });

             order.RuleFor(x => x.Price.ToString())
             .Must(ValidatorHelper.BeValidNumber).WithMessage("مقدار هزینه نامعتبر است ")
             .MaximumLength(15).WithMessage("مقدار هزینه بیش از حد مجاز است ")
             .NotEmpty().WithMessage("مقدار هزینه اجباری است");

             order.RuleFor(x => x.IsReused)
             .MaximumLength(1).WithMessage("مقدار آيا از پرداخت هاي اسناد بي اثر شده قبلي استفاده مجدد شده است؟ بیش از حد مجاز است ")
             .NotNull().WithMessage("مقدار آيا از پرداخت هاي اسناد بي اثر شده قبلي استفاده مجدد شده است؟ اجباری است");

             order.RuleFor(x => x.No)
             .MaximumLength(50).WithMessage("مقدار شناسه يکتا بیش از حد مجاز است ")
             .NotNull().WithMessage("مقدار شناسه يکتا قبض پرداخت اجباری است");

             order.RuleFor(x => x.HowToPay)
             .MaximumLength(1).WithMessage("مقدار شناسه شيوه پرداخت بیش از حد مجاز است ")
             .NotNull().WithMessage("مقدار شناسه شيوه پرداخت اجباری است");

             order.RuleFor(x => x.PaymentType)
             .MaximumLength(1000).WithMessage("مقدار ابزار پرداخت الکترونيک بیش از حد مجاز است ")
             .NotNull().WithMessage("مقدار ابزار پرداخت الکترونيک اجباری است");

             order.RuleFor(x => x.PaymentDate)
             .MaximumLength(10).WithMessage("مقدار تاريخ پرداخت بیش از حد مجاز است ")
             .NotNull().WithMessage("مقدار تاريخ پرداخت اجباری است");

             order.RuleFor(x => x.PaymentNo).NotNull()
             .MaximumLength(100).WithMessage("مقدار شماره مرجع تراکنش پرداخت بیش از حد مجاز است ")
             .NotNull().WithMessage("مقدار شماره مرجع تراکنش پرداخت اجباری است");
         }
         ).When(x => x.DocumentPayments?.Count > 0);
     });

            RuleFor(x => x.DocumentSms)
     .Must(x => x != null)
     .WithMessage("مقدار پیامک غیرمجاز است")
     .DependentRules(() =>
     {
         RuleForEach(x => x.DocumentSms).ChildRules(order =>
         {
             order.When(x => x.IsDirty && !x.IsNew, () =>
             {
                 order.RuleFor(v => v.DocumentId)
             .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند شخص غیر مجاز است . ")
             .NotEmpty().WithMessage("شناسه سند اجباری است.");
             });

             order.When(x => x.IsNew, () =>
             {
                 order.RuleFor(v => v.IsDelete && v.IsDirty)
                    .Must(x => x == false).WithMessage("درخواست نامعتبر است");
             });

             order.When(x => x.IsDirty && !x.IsNew && !x.IsDelete, () =>
             {
                 order.RuleFor(v => v.Id)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه پیامک غیر مجاز است .")
                 .NotEmpty().WithMessage("شناسه سند اجباری است.");
             });

             order.When(x => x.IsDelete, () =>
             {
                 order.RuleFor(v => v.IsNew)
                    .Must(x => x == false).WithMessage("درخواست نامعتبر است");

                 order.RuleFor(v => v.Id)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه پیامک غیر مجاز است .")
                 .NotEmpty().WithMessage("شناسه سند اجباری است.");

             });
             order.When(x => x.IsDelete == false && (x.IsDirty == true || x.IsNew == true), () =>
             {
                 order.RuleFor(x => x.ReceiverName)
                .MaximumLength(50).WithMessage("مقدار نام گیرنده پیامک بیش از حد مجاز است ")
                .NotEmpty().WithMessage("نام گیرنده پیامک  اجباری است");
                 order.RuleFor(x => x.SmsText)
                .MaximumLength(500).WithMessage("مقدار متن پیامک بیش از حد مجاز است ")
                .NotEmpty().WithMessage("متن پیامک  اجباری است");
                 order.RuleFor(x => x.MobileNo)
                .MaximumLength(11).WithMessage("شماره موبایل گیرنده پیامک بیش از حد مجاز است ")
                .NotEmpty().WithMessage("شماره موبایل  پیامک  اجباری است");
             });

         }
         ).When(x => x.DocumentSms?.Count > 0);
     });


            RuleFor(x => x.DocumentCases)
   .Must(x => x != null)
   .WithMessage("مقدار مورد معامله غیر مجاز است ")
   .DependentRules(() =>
   {
       RuleForEach(x => x.DocumentCases).ChildRules(order =>
       {
           order.When(x => x.IsDirty && !x.IsNew, () =>
           {
               order.RuleFor(v => v.DocumentId)
.Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند اجباری است ")
.NotEmpty().WithMessage("شناسه سند اجباری است.");
           });


           order.When(x => x.IsNew, () =>
           {
               order.RuleFor(v => v.IsDelete)
                  .Must(x => x == false).WithMessage("درخواست نامعتبر است");
           });

           order.When(x => x.IsDirty && x.IsNew == false, () =>
           {
               order.RuleFor(v => v.DocumentCaseId)
               .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه مورد معامله است ")
               .NotEmpty().WithMessage("شناسه مورد معامله اجباری است.");
           });

           order.When(x => x.IsDelete, () =>
           {
               order.RuleFor(v => v.IsNew && v.IsDirty)
                  .Must(x => x == false).WithMessage("درخواست نامعتبر است");

           });
           order.RuleFor(x => x.Title)
                     .MaximumLength(50).WithMessage("طول عنوان بیشتر از حد مجاز است ")
                     .NotEmpty().WithMessage("فیلد عنوان اجباری است");

       }).When(x => x.DocumentCases?.Count > 0);

   });

            RuleFor(x => x.DocumentEstates)
            .Must(x => x != null)
            .WithMessage("مقدار مورد معامله ملکی غیر مجاز است ")
            .DependentRules(() =>
            {
                RuleForEach(x => x.DocumentEstates).ChildRules(order =>
                {
                    order.When(x => x.IsDirty && !x.IsNew, () =>
                    {
                        order.RuleFor(v => v.DocumentId)
.Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند غیر مجاز است . ")
.NotEmpty().WithMessage("شناسه سند اجباری است.");
                    });


                    order.When(x => x.IsNew, () =>
                    {
                        order.RuleFor(v => v.IsDelete && v.IsDirty)
                           .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                    });

                    order.When(x => x.IsDirty && !x.IsNew && !x.IsDelete, () =>
                    {
                        order.RuleFor(v => v.EstateId)
                        .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه معامله ملک غیر مجاز است .")
                        .NotEmpty().WithMessage("شناسه سند اجباری است.");
                    });

                    order.When(x => x.IsDelete, () =>
                    {
                        order.RuleFor(v => v.IsNew)
                           .Must(x => x == false).WithMessage("درخواست نامعتبر است");

                        order.RuleFor(v => v.EstateId)
                        .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه معامله ملک غیر مجاز است .")
                        .NotEmpty().WithMessage("شناسه سند اجباری است.");

                    });


                    //
                    order.When(x => x.IsDelete == false && (x.IsDirty == true || x.IsNew == true), () =>
                    {
                        order.RuleFor(x => x.DocumentEstateTypeId)

                .Must(x => x != null && x.Count <= 1).WithMessage("نوع مورد ثبتی  غیر مجاز میباشد ").ChildRules(order =>
                {
                    order.RuleForEach(x => x)
                   .MaximumLength(2).WithMessage("طول نوع مورد ثبتی بیشتر از حد مجاز است ");
                });

                        order.RuleFor(x => x.DocumentUnitId)
                       .Must(x => x != null && x.Count <= 1).WithMessage("ناحیه ثبتی  غیر مجاز میباشد ").ChildRules(order =>
                       {
                           order.RuleForEach(x => x)
                        .MaximumLength(5).WithMessage("طول ناحیه ثبتی بیشتر از حد مجاز است ");
                       });

                        order.RuleFor(x => x.DocumentEstateSectionId)
                       .Must(x => x != null && x.Count <= 1).WithMessage("بخش ثبتی  غیر مجاز میباشد ").ChildRules(order =>
                       {
                           order.RuleForEach(x => x)
                       .MaximumLength(10).WithMessage("طول بخش ثبتی بیشتر از حد مجاز است ");
                       });

                        order.RuleFor(x => x.DocumentEstateSubSectionId)
                     .Must(x => x != null && x.Count <= 1).WithMessage("حوزه ثبتی  غیر مجاز میباشد ").ChildRules(order =>
                     {
                         order.RuleForEach(x => x)
                          .MaximumLength(10).WithMessage("طول حوزه ثبتی بیشتر از حد مجاز است ");
                     });

                        order.RuleFor(x => x.GeoLocationId)
                      .Must(x => x != null && x.Count <= 1).WithMessage("محل جغرافیایی  غیر مجاز میباشد ").ChildRules(order =>
                      {
                          order.RuleForEach(x => x)
                           .MaximumLength(6).WithMessage("طول محل جغرافیایی بیشتر از حد مجاز است ");
                      });
                        order.RuleFor(x => x.Description)
     .MaximumLength(2000).WithMessage("طول ملاحظات بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.QuotaText)
     .MaximumLength(2000).WithMessage("سهم متن بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.Area)
    .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد مساحت غیر مجاز است ")
    .MaximumLength(20).WithMessage("طول مساحت بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.PostalCode)
     .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد کد پستی غیر مجاز است ")
     .Length(10).WithMessage("طول کد پستی غیر مجاز است")
     .When(x => x.PostalCode != null);
                        order.RuleFor(x => x.BasicPlaque)
    .MaximumLength(100).WithMessage("طول پلاک اصلی بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SecondaryPlaque)
    .MaximumLength(100).WithMessage("طول پلاک فرعی بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.PlaqueText)
    .MaximumLength(500).WithMessage("طول پلاک متنی بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.DivFromBasicPlaque)
    .MaximumLength(100).WithMessage("طول مفروز و مجزی از اصلی بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.DivFromSecondaryPlaque)
    .MaximumLength(100).WithMessage("طول مفروز و مجزی از فرعی بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.ImmovaleType)
    .MaximumLength(2).WithMessage("طول نوع ملک بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.LocationType)
    .MaximumLength(1).WithMessage("طول نوع محل استقرار بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.Piece)
    .MaximumLength(50).WithMessage("طول شماره قطعه بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.Block)
    .MaximumLength(10).WithMessage("طول  بلوک بیشتر از حد مجاز است ");



                        order.RuleFor(x => x.Floor)
    .MaximumLength(20).WithMessage("طول طبقه بیشتر از حد مجاز است ");

                        order.RuleFor(x => x.Direction)
        .MaximumLength(500).WithMessage("طول سمت بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.PostalCode)
                        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد کدپستی غیر مجاز است ")
        .Length(10).WithMessage("طول کدپستی غیر  مجاز است ").When(x => x.PostalCode != null);
                        order.RuleFor(x => x.Address)
        .MaximumLength(1000).WithMessage("طول نشاني بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.AreaDescription)
        .MaximumLength(2000).WithMessage("طول توضيحات مساحت بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.RegisterCaseTitle)
        .MaximumLength(100).WithMessage("طول عنوان مورد ثبتي بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.EvacuatedDate)
        .MaximumLength(10).WithMessage("طول تاريخ تخليه بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.EvacuationDescription)
        .MaximumLength(2000).WithMessage("طول توضيحات در مورد وضعيت تخليه بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.Price)
        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد مبلغ سند غیر مجاز است ")
        .MaximumLength(15).WithMessage("طول مبلغ سند بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.RegionalPrice)
        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد ارزش منطقه اي ملک غیر مجاز است ")
        .MaximumLength(15).WithMessage("طول ارزش منطقه اي ملک بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.FieldOrGrandee)
        .MaximumLength(1).WithMessage("طول عرصه يا اعيان بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.OwnershipType)
        .MaximumLength(1).WithMessage("طول نوع مالکيت بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.MunicipalityNo)
        .MaximumLength(50).WithMessage("طول شماره پايان کار شهرداري بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.MunicipalityDate)
        .Length(10).WithMessage("طول تاريخ پايان کار شهرداري غیر مجاز مجاز است ").When(x => x.MunicipalityDate != null);
                        order.RuleFor(x => x.MunicipalityIssuer)
        .MaximumLength(10).WithMessage("طول مرجع صدور پايان کار شهرداري بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SeparationType)
        .MaximumLength(1).WithMessage("طول نوع تفکيک بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SeparationNo)
        .MaximumLength(50).WithMessage("طول شماره صورتمجلس تفکيکي بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SeparationDate)
        .MaximumLength(10).WithMessage("طول تاريخ صورتمجلس تفکيکي بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SeparationIssuer)
        .MaximumLength(1000).WithMessage("طول مرجع صدور صورتمجلس تفکيکي بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.AttachmentType)
        .MaximumLength(2).WithMessage("طول نوع منضم بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.AttachmentTypeOthers)
        .MaximumLength(100).WithMessage("طول نوع منضم - ساير بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.ReceiverBasicPlaque)
        .MaximumLength(100).WithMessage("طول پلاک ثبتي اصلي بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.ReceiverSecondaryPlaque)
        .MaximumLength(100).WithMessage("طول  پلاک ثبتي فرعي بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.ReceiverPlaqueText)
        .MaximumLength(500).WithMessage("طول پلاک متني بیشتر از حد مجاز است ");
                        //                order.RuleFor(x => x.ReceiverBasicPlaqueHasRemain)
                        //.MaximumLength(1).WithMessage("طول شماره بلوک بیشتر از حد مجاز است ");
                        //                order.RuleFor(x => x.ReceiverSecondaryPlaqueHasRemain)
                        //.MaximumLength(1).WithMessage("طول شماره بلوک بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.ReceiverDivFromBasicPlaque)
        .MaximumLength(100).WithMessage("طول مفروز و مجزي از اصلي بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.ReceiverDivFromSecondaryPlaque)
        .MaximumLength(100).WithMessage("طول مفروز و مجزي از فرعي بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.GrandeeExceptionType)
        .MaximumLength(1).WithMessage("طول نوع استثناء بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.GrandeeExceptionDetailQuota)
        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد جزء استثناء غیر مجاز است ")
        .MaximumLength(20).WithMessage("طول جزء استثناء بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.GrandeeExceptionTotalQuota)
        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد کل استثناء غیر مجاز است ")
        .MaximumLength(20).WithMessage("طول کل استثناء بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SomnyehRobeyehFieldGrandee)
        .MaximumLength(1).WithMessage("طول عرصه يا اعيان بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SomnyehRobeyehActionType)
        .MaximumLength(1).WithMessage("طول نوع عمل بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.OwnershipDetailQuota)
        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد جزء سهم مورد مالکيت غیر مجاز است ")
        .MaximumLength(20).WithMessage("طول جزء سهم مورد مالکيت بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.OwnershipTotalQuota)
        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد کل سهم مورد مالکيت غیر مجاز است ")
        .MaximumLength(20).WithMessage("طول کل سهم مورد مالکيت بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SellDetailQuota)
        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد جزء سهم مورد معامله غیر مجاز است ")
        .MaximumLength(20).WithMessage("طول جزء سهم مورد معامله بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.SellTotalQuota)
        .Must(ValidatorHelper.BeValidNumberNullable).WithMessage("مقدار فیلد کل سهم مورد معامله غیر مجاز است ")
        .MaximumLength(20).WithMessage("طول کل سهم مورد معامله بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.QuotaText)
        .MaximumLength(2000).WithMessage("طول متن سهم بیشتر از حد مجاز است ");
                        order.RuleFor(x => x.Description)
        .MaximumLength(2000).WithMessage("طول ملاحظات بیشتر از حد مجاز است ");
                    });

                }).When(x => x.DocumentEstates?.Count > 0);

            });

            RuleFor(x => x.DocumentVehicles)
                .Must(x => x != null)
                .WithMessage("مقدار معامله خودرو غیر مجاز است")
                .DependentRules(() =>
                    RuleForEach(x => x.DocumentVehicles)
            .ChildRules(vehicle =>
            {
                vehicle.When(x => x.IsDirty && !x.IsNew, () =>
                {
                    vehicle.RuleFor(v => v.DocumentId)
.Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند غیر مجاز است . ")
.NotEmpty().WithMessage("شناسه سند اجباری است.");
                });


                vehicle.When(x => x.IsNew, () =>
                {
                    vehicle.RuleFor(v => v.IsDelete && v.IsDirty)
                       .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                });

                vehicle.When(x => x.IsDirty && !x.IsNew && !x.IsDelete, () =>
                {
                    vehicle.RuleFor(v => v.VehicleId)
                    .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه معامله خودرو غیر مجاز است .")
                    .NotEmpty().WithMessage("شناسه سند اجباری است.");
                });

                vehicle.When(x => x.IsDelete, () =>
                {
                    vehicle.RuleFor(v => v.IsNew)
                       .Must(x => x == false).WithMessage("درخواست نامعتبر است");

                    vehicle.RuleFor(v => v.VehicleId)
                    .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه معامله خودرو غیر مجاز است .")
                    .NotEmpty().WithMessage("شناسه سند اجباری است.");

                });


                //
                vehicle.When(x => x.IsDelete == false && (x.IsDirty == true || x.IsNew == true), () =>
                {

                    vehicle.RuleFor(v => v.MadeInYear)
                    .Must(ValidatorHelper.BeValidPersianYear).WithMessage("سال ساخت خودرو باید معتبر باشد")
                    .When(v => v.MadeInYear != null);
                    vehicle.RuleFor(x => x.DocumentVehicleTypeId)
                   .Must(x => x != null && x.Count <= 1).WithMessage("شناسه نوع وسيله نقليه  غیر مجاز میباشد ").ChildRules(vehicle =>
                   {
                       vehicle.RuleForEach(x => x)
                        .MaximumLength(3).WithMessage("طول شناسه نوع وسيله نقليه بیشتر از حد مجاز است ");
                   });
                    vehicle.RuleFor(x => x.DocumentVehicleTypeId)
                    .Must(x => x != null && x.Count <= 1).WithMessage("شناسه سيستم وسيله نقليه  غیر مجاز میباشد ").ChildRules(vehicle =>
                    {
                        vehicle.RuleForEach(x => x)
                     .MaximumLength(8).WithMessage("طول شناسه سيستم وسيله نقليه بیشتر از حد مجاز است ");
                    });
                    vehicle.RuleFor(x => x.DocumentVehicleTypeId)
.Must(x => x != null && x.Count <= 1).WithMessage("شناسه تيپ وسيله نقليه  غیر مجاز میباشد ").ChildRules(vehicle =>
{
    vehicle.RuleForEach(x => x)
 .MaximumLength(10).WithMessage("طول شناسه تيپ وسيله نقليه بیشتر از حد مجاز است ");
});
                    vehicle.RuleFor(x => x.Type)
                    .MaximumLength(200).WithMessage("طول فیلد نوع وسيله نقليه بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.System)
                    .MaximumLength(200).WithMessage("طول فیلد سیستم وسيله نقليه بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.Model)
                    .MaximumLength(200).WithMessage("طول فیلد مدل وسيله نقليه بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.Tip)
                    .MaximumLength(200).WithMessage("طول فیلد تیپ وسيله نقليه بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.EngineNo)
                    .MaximumLength(50).WithMessage("طول فیلد شماره موتور بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.ChassisNo)
.MaximumLength(50).WithMessage("طول فیلد شماره شاسي بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.EngineCapacity)
.MaximumLength(50).WithMessage("طول فیلد حجم موتور بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.Color)
.MaximumLength(50).WithMessage("طول فیلد رنگ خودرو بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.CylinderCount)
.MaximumLength(5).WithMessage("طول فیلد تعداد سيلندر بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.CardNo)
.MaximumLength(50).WithMessage("طول فیلد شماره کارت وسيله نقليه بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.DutyFicheNo)
.MaximumLength(50).WithMessage("طول فیلد شماره فيش پرداخت عوارض بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.FuelCardNo)
.MaximumLength(50).WithMessage("طول فیلد شماره کارت سوخت بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.InssuranceCo)
.MaximumLength(100).WithMessage("طول فیلد شرکت بيمه کننده بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.InssuranceNo)
.MaximumLength(100).WithMessage("طول فیلد شماره بيمه شخص ثالث بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.OwnershipPrintedDocumentNo)
.MaximumLength(100).WithMessage("طول فیلد شماره چاپي شناسنامه مالکيت بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.OldDocumentNo)
.MaximumLength(100).WithMessage("طول فیلد شماره سند قبلي بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.OldDocumentIssuer)
.MaximumLength(200).WithMessage("طول فیلد مرجع صادرکننده سند قبلي بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.OldDocumentDate)
                    .Must(ValidatorHelper.BeValidPersianDate).WithMessage(" تاريخ سند قبلي غیرمجاز است ")
                    .When(x => x.OldDocumentDate != null);
                    vehicle.RuleFor(x => x.NumberingLocation)
.MaximumLength(100).WithMessage("طول محل شماره گذاري بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueNo1Seller)
.MaximumLength(10).WithMessage("طول بخش اول عددي شماره انتظامي فروشنده بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueNo2Seller)
.MaximumLength(10).WithMessage("طول بخش دوم عددي شماره انتظامي فروشنده بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueSeriSeller)
.MaximumLength(10).WithMessage("طول سري شماره انتظامي فروشنده بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueNoAlphaSeller)
.MaximumLength(2).WithMessage("طول بخش حرفي شماره انتظامي فروشنده بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueSeller)
.MaximumLength(50).WithMessage("طول شماره انتظامي فروشنده بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueNo1Buyer)
.MaximumLength(10).WithMessage("طول بخش اول عددي شماره انتظامي خريدار بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueNo2Buyer)
.MaximumLength(10).WithMessage("طول بخش دوم عددي شماره انتظامي خريدار بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueSeriBuyer)
.MaximumLength(10).WithMessage("طول سري شماره انتظامي خريدار بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueNoAlphaBuyer)
.MaximumLength(2).WithMessage("طول بخش حرفي شماره انتظامي خريدار بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.PlaqueBuyer)
.MaximumLength(50).WithMessage("طول شماره انتظامي خريدار بیشتر از حد مجاز است ");
                    vehicle.RuleFor(x => x.Price)
.MaximumLength(15).WithMessage("طول مبلغ سند بیشتر از حد مجاز است ")
.Must(ValidatorHelper.BeValidNumberNullable).WithMessage(" مبلغ سند غیرمجاز است ");
                    vehicle.RuleFor(x => x.OwnershipType)
.MaximumLength(50).WithMessage("طول شماره انتظامي خريدار بیشتر از حد مجاز است ");
                    //TODO
                    // Rules for nested collections
                    vehicle.RuleForEach(v => v.DocumentVehicleQuotums)
                    .ChildRules(quotum =>
                    {
                        quotum.RuleFor(v => v.DocumentVehicleId)
                        .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه سند غیر مجاز است . ")
                        .NotEmpty().WithMessage("شناسه سند اجباری است.");

                        quotum.When(x => x.IsNew, () =>
                        {
                            quotum.RuleFor(v => v.IsDelete && v.IsDirty)
                               .Must(x => x == false).WithMessage("درخواست نامعتبر است");
                        });
                        quotum.When(x => x.IsDirty && !x.IsNew && !x.IsDelete, () =>
                        {
                            quotum.RuleFor(v => v.DocumentVehicleQuotumId)
                            .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه معامله ملک غیر مجاز است .")
                            .NotEmpty().WithMessage("شناسه سند اجباری است.");
                        });

                        quotum.When(x => x.IsDelete, () =>
                        {
                            quotum.RuleFor(v => v.IsNew)
                               .Must(x => x == false).WithMessage("درخواست نامعتبر است");

                            quotum.RuleFor(v => v.DocumentVehicleQuotumId)
                            .Must(ValidatorHelper.BeValidGuid).WithMessage("مقدار شناسه معامله ملک غیر مجاز است .")
                            .NotEmpty().WithMessage("شناسه سند اجباری است.");

                        });
                        quotum.When(x => x.IsDelete == false && x.IsDirty == true, () =>
                        {
                            quotum.RuleFor(q => q.DetailQuota)
                            .Must(ValidatorHelper.BeValidNumber).WithMessage("سهم جزئی سهمیه باید عدد اعشاری معتبر باشد");
                            // ... other rules for quotum
                        });
                    });

                    // ... rules for DocumentVehicleQuotaDetails
                });
            })
);


        }
    }
}

