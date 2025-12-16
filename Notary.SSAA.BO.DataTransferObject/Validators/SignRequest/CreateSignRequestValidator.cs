using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.SharedKernel.Constants;
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
        private void ConfigureBaseRules()
        {
            RuleFor(v => v.IsNew)
                .Equal(true)
                .WithMessage(SystemMessagesConstant.Request_IsNew_Invalid);

            RuleFor(v => v.IsDelete)
                .Equal(false)
                .WithMessage(SystemMessagesConstant.Request_IsDelete_Invalid);

            RuleFor(v => v.SignRequestId)
                .Must(ValidatorHelper.BeValidGuid)
                .WithMessage(SystemMessagesConstant.SignRequest_IdInvalid);
        }

        private void ConfigureSubjectRules()
        {
            RuleFor(x => x.SignRequestSubjectId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(SystemMessagesConstant.Subject_Required)
                .Must(list => list.Count == 1)
                    .WithMessage(SystemMessagesConstant.Subject_CountInvalid)
                .ForEach(r =>
                    r.NotEmpty()
                     .WithMessage(SystemMessagesConstant.Subject_IdRequired));
        }

        private void ConfigurePersonsRules()
        {
            RuleFor(x => x.SignRequestPersons)
                .NotNull()
                .WithMessage(SystemMessagesConstant.Persons_ListInvalid)
                .DependentRules(() =>
                {
                    RuleForEach(x => x.SignRequestPersons).ChildRules(person =>
                    {
                        person.RuleFor(v => v.IsNew)
                            .Equal(true)
                            .WithMessage(SystemMessagesConstant.Person_NotNew);

                        person.RuleFor(v => v.IsDelete)
                            .Equal(false)
                            .WithMessage(SystemMessagesConstant.Person_DeleteNotAllowed);

                        // ── Base person info ──
                        person.RuleFor(x => x.PersonName)
                            .NotEmpty()
                                .WithMessage(SystemMessagesConstant.PersonName_Required)
                            .MaximumLength(150)
                                .WithMessage(SystemMessagesConstant.PersonName_MaxLength);

                        person.RuleFor(x => x.PersonFamily)
                            .NotEmpty()
                                .WithMessage(SystemMessagesConstant.PersonFamily_Required)
                            .MaximumLength(100)
                                .WithMessage(SystemMessagesConstant.PersonFamily_MaxLength);

                        person.RuleFor(x => x.PersonFatherName)
                            .NotEmpty()
                                .WithMessage(SystemMessagesConstant.PersonFatherName_Required)
                            .MaximumLength(100)
                                .WithMessage(SystemMessagesConstant.PersonFatherName_MaxLength);

                        // ── Sex Type ──
                        person.RuleFor(x => x.PersonSexType)
                            .Cascade(CascadeMode.Stop)
                            .NotEmpty()
                                .WithMessage(SystemMessagesConstant.PersonSex_Required)
                            .Must(v => ValidatorHelper.ValidateRangeValue(v, 1, 2))
                                .WithMessage(SystemMessagesConstant.PersonSex_Invalid)
                            .Must(ValidatorHelper.BeValidNumber)
                                .WithMessage(SystemMessagesConstant.PersonSex_Invalid);

                        // ── Iranian persons validations ──
                        person.When(x => x.IsPersonIranian, () =>
                        {
                            person.RuleFor(x => x.PersonNationalNo)
                                .NotEmpty()
                                    .WithMessage(SystemMessagesConstant.NationalNo_Required)
                                .Length(10)
                                    .WithMessage(SystemMessagesConstant.NationalNo_LengthInvalid);

                            person.RuleFor(x => x.PersonBirthDate)
                                .NotEmpty()
                                    .WithMessage(SystemMessagesConstant.BirthDate_Required)
                                .Must(ValidatorHelper.BeValidPersianDate)
                                    .WithMessage(SystemMessagesConstant.BirthDate_InvalidFormat);

                            person.RuleFor(x => x.PersonIdentityNo)
                                .NotEmpty()
                                    .WithMessage(SystemMessagesConstant.IdentityNo_Required)
                                .MaximumLength(10)
                                    .WithMessage(SystemMessagesConstant.IdentityNo_MaxLength)
                                .Must(ValidatorHelper.BeValidNumber)
                                    .WithMessage(SystemMessagesConstant.IdentityNo_Invalid);

                            person.RuleFor(x => x.PersonIdentityIssueLocation)
                                .NotEmpty()
                                    .WithMessage(SystemMessagesConstant.IdentityIssueLocation_Required)
                                .MaximumLength(60)
                                    .WithMessage(SystemMessagesConstant.IdentityIssueLocation_MaxLength);
                        });

                        // ── Non-Iranian persons ──
                        person.When(x => !x.IsPersonIranian, () =>
                        {
                            person.RuleFor(x => x.PersonNationalityId)
                                .NotNull()
                                    .WithMessage(SystemMessagesConstant.Nationality_Required)
                                .Must(l => l.Count == 1)
                                    .WithMessage(SystemMessagesConstant.Nationality_CountInvalid)
                                .ForEach(r =>
                                    r.NotEmpty()
                                        .WithMessage(SystemMessagesConstant.NationalityId_Required)
                                     .Must(ValidatorHelper.BeValidNumber)
                                        .WithMessage(SystemMessagesConstant.NationalityId_Invalid));
                        });

                        // ── Contact info ──
                        person.RuleFor(x => x.PersonMobileNo)
                            .Cascade(CascadeMode.Stop)
                            .NotEmpty()
                                .WithMessage(SystemMessagesConstant.Mobile_Required)
                            .Must(ValidatorHelper.BeValidMobileNumber)
                                .WithMessage(SystemMessagesConstant.Mobile_Invalid);

                        person.When(x => x.PersonTel.HasValue(), () =>
                        {
                            person.RuleFor(x => x.PersonTel)
                                .Must(ValidatorHelper.BeValidNumber)
                                    .WithMessage(SystemMessagesConstant.Tel_Invalid)
                                .MaximumLength(15)
                                    .WithMessage(SystemMessagesConstant.Tel_MaxLength);
                        });

                        person.When(x => x.PersonEmail.HasValue(), () =>
                        {
                            person.RuleFor(x => x.PersonEmail)
                                .EmailAddress()
                                    .WithMessage(SystemMessagesConstant.Email_Invalid)
                                .MaximumLength(150)
                                    .WithMessage(SystemMessagesConstant.Email_MaxLength);
                        });

                        // ── Address ──
                        person.RuleFor(x => x.PersonAddress)
                            .Cascade(CascadeMode.Stop)
                            .NotEmpty()
                                .WithMessage(SystemMessagesConstant.Address_Required)
                            .MinimumLength(1)
                                .WithMessage(SystemMessagesConstant.Address_TooShort)
                            .MaximumLength(4000)
                                .WithMessage(SystemMessagesConstant.Address_MaxLength);

                        // ── Postal Code ──
                        person.RuleFor(x => x.PersonPostalCode)
                            .Cascade(CascadeMode.Stop)
                            .NotEmpty()
                                .WithMessage(SystemMessagesConstant.PostalCode_Required)
                            .Length(10)
                                .WithMessage(SystemMessagesConstant.PostalCode_LengthInvalid)
                            .Must(ValidatorHelper.BeValidNumber)
                                .WithMessage(SystemMessagesConstant.PostalCode_Invalid);

                        // ── Description optional ──
                        person.When(x => x.PersonDescription.HasValue(), () =>
                        {
                            person.RuleFor(x => x.PersonDescription)
                                .MaximumLength(2000)
                                .WithMessage(SystemMessagesConstant.Description_MaxLength);
                        });
                    })
                    .When(x => x.SignRequestPersons?.Count > 0);
                });
        }

        private void ConfigureRelatedPersonsRules()
        {
            RuleFor(x => x.SignRequestRelatedPersons)
                .NotNull()
                .WithMessage(SystemMessagesConstant.RelatedPersons_ListInvalid)
                .DependentRules(() =>
                {
                    RuleForEach(x => x.SignRequestRelatedPersons).ChildRules(rp =>
                    {
                        rp.RuleFor(v => v.SignRequestId)
                            .NotEmpty()
                                .WithMessage(SystemMessagesConstant.Subject_IdRequired)
                            .Must(ValidatorHelper.BeValidGuid)
                                .WithMessage(SystemMessagesConstant.SignRequest_IdInvalid);

                        rp.RuleFor(x => x.IsDelete)
                            .Equal(false)
                            .WithMessage(SystemMessagesConstant.Related_DeleteNotAllowed);

                        rp.RuleFor(x => x.MainPersonId)
                            .NotNull()
                                .WithMessage(SystemMessagesConstant.MainPerson_Required)
                            .Must(l => l.Count == 1)
                                .WithMessage(SystemMessagesConstant.MainPerson_CountInvalid)
                            .ForEach(r =>
                                r.NotEmpty()
                                    .WithMessage(SystemMessagesConstant.MainPersonId_Required)
                                 .Must(ValidatorHelper.BeValidGuid));

                        rp.RuleFor(x => x.RelatedAgentPersonId)
                            .NotNull()
                                .WithMessage(SystemMessagesConstant.AgentPerson_Required)
                            .Must(l => l.Count == 1)
                                .WithMessage(SystemMessagesConstant.AgentPerson_CountInvalid)
                            .ForEach(r =>
                                r.NotEmpty()
                                    .WithMessage(SystemMessagesConstant.AgentPersonId_Required)
                                 .Must(ValidatorHelper.BeValidGuid));

                        rp.RuleFor(x => x.RelatedAgentTypeId)
                            .NotNull()
                                .WithMessage(SystemMessagesConstant.AgentType_Required)
                            .Must(l => l.Count == 1)
                                .WithMessage(SystemMessagesConstant.AgentType_CountInvalid)
                            .ForEach(r =>
                                r.NotEmpty()
                                    .WithMessage(SystemMessagesConstant.AgentType_Required));

                        rp.RuleFor(x => x.RelatedReliablePersonReasonId)
                            .NotNull()
                            .WithMessage(SystemMessagesConstant.ReliableReason_Required);
                    })
                    .When(x => x.SignRequestRelatedPersons?.Count > 0);
                });
        }
    }
}
