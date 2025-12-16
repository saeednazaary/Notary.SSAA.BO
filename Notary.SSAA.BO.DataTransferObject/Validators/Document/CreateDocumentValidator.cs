using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public sealed class CreateDocumentValidator
        : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentValidator()
        {
            RuleFor(v => v.IsNew)
                .Must(x => x)
                .WithMessage(SystemMessagesConstant.Request_IsNew_Invalid);

            RuleFor(v => v.IsDelete)
                .Must(x => !x)
                .WithMessage(SystemMessagesConstant.Request_IsDelete_Invalid);

            RuleFor(x => x.DocumentTypeId)
                .Must(x => x != null && x.Count == 1)
                .WithMessage(SystemMessagesConstant.Document_Type_Count_Invalid)
                .DependentRules(() =>
                {
                    RuleForEach(x => x.DocumentAssetTypeId)
                        .NotEmpty()
                        .WithMessage(SystemMessagesConstant.Document_TypeId_Required);
                });

            RuleFor(x => x.DocumentPeople)
                .Must(x => x != null)
                .WithMessage(SystemMessagesConstant.Document_People_Invalid)
                .DependentRules(() =>
                {
                    RuleForEach(x => x.DocumentPeople)
                        .ChildRules(person =>
                        {
                            person.When(x => x.IsNew, () =>
                            {
                                person.RuleFor(v => v.IsDelete && v.IsDirty)
                                    .Must(x => !x)
                                    .WithMessage(SystemMessagesConstant.Request_Invalid);
                            });

                            person.RuleFor(x => x.PersonSexType)
                                .Must(value => ValidatorHelper.ValidateRangeValue(value, 1, 2))
                                    .WithMessage(SystemMessagesConstant.Person_SexType_Invalid)
                                .Must(ValidatorHelper.BeValidNumber)
                                    .WithMessage(SystemMessagesConstant.Person_SexType_Invalid)
                                .MaximumLength(1)
                                    .WithMessage(SystemMessagesConstant.Person_SexType_Invalid)
                                .NotEmpty()
                                    .WithMessage(SystemMessagesConstant.Person_Sex_Required);

                            person.RuleFor(x => x.PersonBirthDate)
                                .Must(ValidatorHelper.BeValidPersianDate)
                                    .WithMessage(SystemMessagesConstant.Person_BirthDate_Invalid)
                                .NotEmpty()
                                    .WithMessage(SystemMessagesConstant.Person_BirthDate_Required)
                                .When(x => x.PersonType == "1");
                        });
                });

            RuleFor(x => x.DocumentRelatedPeople)
                .Must(x => x != null)
                .WithMessage(SystemMessagesConstant.Document_RelatedPeople_Invalid)
                .DependentRules(() =>
                {
                    RuleForEach(x => x.DocumentRelatedPeople)
                        .ChildRules(rp =>
                        {
                            rp.RuleFor(x => x.DocumentId)
                                .Must(ValidatorHelper.BeValidGuid)
                                    .WithMessage(SystemMessagesConstant.Related_DocumentId_Invalid)
                                .NotEmpty()
                                    .WithMessage(SystemMessagesConstant.Related_DocumentId_Required);

                            rp.When(x => x.IsNew, () =>
                            {
                                rp.RuleFor(x => x.IsDelete)
                                    .Must(x => !x)
                                    .WithMessage(SystemMessagesConstant.Request_Invalid);
                            });
                        });
                });
        }
    }
}

