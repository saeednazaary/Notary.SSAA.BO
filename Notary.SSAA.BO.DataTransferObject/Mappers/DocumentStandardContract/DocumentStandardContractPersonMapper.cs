namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Azure.Core;
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractPersonMapper" />
    /// </summary>
    public static class DocumentStandardContractPersonMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractPerson
        /// </summary>
        /// <param name="DocumentPerson">The DocumentPerson<see cref="DocumentPerson"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractPersonViewModel"/></param>
        public static void MapToDocumentStandardContractPerson(ref DocumentPerson DocumentPerson, DocumentStandardContractPersonViewModel viewModel, bool? isRemoteRequest = false)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractPersonViewModel, DocumentPerson>()
                .Map(dest => dest.Id, src => isRemoteRequest == true ? src.PersonId.ToGuid() : (src.IsNew == true ? Guid.NewGuid() : src.PersonId.ToGuid()))
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.RowNo, src => src.RowNo.ToShort())
                .Map(dest => dest.IsRelated, src => src.IsPersonRelated.ToYesNo())
                .Map(dest => dest.IsIranian, src => src.IsPersonIranian.ToYesNo())
                .Map(dest => dest.NationalityId, src => src.PersonNationalityId.Count > 0 ? src.PersonNationalityId.First().ToNullableInt() : null)
                .Map(dest => dest.NationalNo, src => src.PersonNationalNo)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.HasSmartCard, src => src.HasSmartCard.ToYesNo())
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.BirthYear, src => src.PersonBirthYear)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                .Map(dest => dest.IdentityIssueLocation, src => src.PersonIdentityIssueLocation)
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.SeriAlpha, src => src.PersonAlphabetSeri)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.Serial, src => src.PersonSerial)
                .Map(dest => dest.SanaState, src => src.IsPersonSanaChecked.ToYesNo())
                .Map(dest => dest.IsOriginal, src => src.IsPersonOriginal.ToYesNo())
                .Map(dest => dest.MobileNoState, src => src.PersonMobileNoState.ToYesNo())
                .Map(dest => dest.AmlakEskanState, src => src.AmlakEskanState.ToYesNo())
                .Map(dest => dest.IsSabtahvalChecked, src => src.IsPersonSabteAhvalChecked.ToYesNo())
                .Map(dest => dest.IsSabtahvalCorrect, src => src.IsPersonSabteAhvalCorrect.ToYesNo())
                .Map(dest => dest.IsAlive, src => src.IsPersonAlive.ToYesNo())
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.CompanyTypeId, src => src.PersonCompanyTypeId.Count > 0 ? src.PersonCompanyTypeId.First() : null)
                .Map(dest => dest.LegalpersonNatureId, src => src.PersonLegalPersonNatureid.Count > 0 ? src.PersonLegalPersonNatureid.First() : null)
                .Map(dest => dest.LegalpersonTypeId, src => src.PersonLegalPersonTypeId.Count > 0 ? src.PersonLegalPersonTypeId.First() : null)
                .Map(dest => dest.IdentityIssueGeoLocationId, src => src.PersonIdentityIssueGeoLocationId.Count > 0 ? src.PersonIdentityIssueGeoLocationId.First().ToNullableInt() : null)
                .Map(dest => dest.DocumentPersonTypeId, src => src.RequestPersonTypeId.Count > 0 ? src.RequestPersonTypeId.First() : null)
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.AddressType, src => src.PersonAddressType)

                .Map(dest => dest.Tel, src => src.PersonTel)
                .Map(dest => dest.FaxNo, src => src.PersonFaxNo)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .Map(dest => dest.PassportNo, src => src.PersonPassportNo)
                .Ignore(src => src.Ilm)
                .Ignore(src => src.IsFingerprintGotten)
               // .IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentId)
                ;
            config.Compile();
            viewModel.Adapt(DocumentPerson, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractPeopleViewModel
        /// </summary>
        /// <param name="documentPeople">The documentPeople<see cref="List{DocumentPerson}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractPersonViewModel}"/></returns>
        public static List<DocumentStandardContractPersonViewModel> MapToDocumentStandardContractPeopleViewModel(
        List<DocumentPerson> documentPeople
    )
        {
            List<DocumentStandardContractPersonViewModel> documentPeopleViewModel =
                new List<DocumentStandardContractPersonViewModel>();
            documentPeople.ForEach(sp =>
            {
                documentPeopleViewModel.Add(MapToDocumentStandardContractPersonViewModel(sp));
            });
            return documentPeopleViewModel;
        }

        /// <summary>
        /// The MapToDocumentStandardContractPersonViewModel
        /// </summary>
        /// <param name="documentPerson">The documentPerson<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="DocumentStandardContractPersonViewModel"/></returns>
        public static DocumentStandardContractPersonViewModel MapToDocumentStandardContractPersonViewModel(
          DocumentPerson documentPerson
      )
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentPerson, DocumentStandardContractPersonViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.PersonId, src => src.Id.ToString())
                .Map(dest => dest.PersonType, src => src.PersonType.ToString())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToString())
                .Map(dest => dest.RowNo, src => src.RowNo.ToString())
                .Map(dest => dest.IsPersonOriginal, src => src.IsOriginal.ToBoolean())
                .Map(dest => dest.IsPersonRelated, src => src.IsRelated.ToBoolean())
                .Map(dest => dest.IsPersonIranian, src => src.IsIranian.ToBoolean())
                .Map(
                    dest => dest.PersonNationalityId,
                    src =>
                        src.NationalityId.HasValue
                            ? new List<string> { src.NationalityId.ToString().Trim() }
                            : new List<string>()
                )
                          .Map(
                    dest => dest.PersonIdentityIssueGeoLocationId,
                    src =>
                        src.IdentityIssueGeoLocationId.HasValue
                            ? new List<string> { src.IdentityIssueGeoLocationId.ToString().Trim() }
                            : new List<string>()
                )
                        .Map(
                    dest => dest.PersonLegalPersonNatureid,
                    src =>
                  src.LegalpersonNatureId != null
                    ? new List<string> { src.LegalpersonNatureId.ToString() }
                    : new List<string>()
                )
                   .Map(
                    dest => dest.PersonLegalPersonTypeId,
                     src =>
                    src.LegalpersonTypeId != null
                    ? new List<string> { src.LegalpersonTypeId.ToString() }
                    : new List<string>()
                )
                  .Map(
                    dest => dest.PersonCompanyTypeId,

                        src =>
                    src.CompanyTypeId != null
                    ? new List<string> { src.CompanyTypeId.ToString() }
                    : new List<string>()
                )
                     .Map(
                    dest => dest.RequestPersonTypeId,

                        src =>
                    src.DocumentPersonTypeId != null
                    ? new List<string> { src.DocumentPersonTypeId.ToString() }
                    : new List<string>()
                )

                .Map(dest => dest.PersonNationalNo, src => src.NationalNo)
                .Map(dest => dest.PersonName, src => src.Name)
                .Map(dest => dest.HasSmartCard, src => src.HasSmartCard.ToNullableYesNo())
                .Map(dest => dest.PersonFamily, src => src.Family)
                .Map(dest => dest.PersonBirthDate, src => src.BirthDate)
                .Map(dest => dest.PersonBirthYear, src => src.BirthYear)
                .Map(dest => dest.PersonSexType, src => src.SexType)
                .Map(dest => dest.PersonFatherName, src => src.FatherName)
                .Map(dest => dest.PersonIdentityIssueLocation, src => src.IdentityIssueLocation)
                .Map(dest => dest.PersonIdentityNo, src => src.IdentityNo)
                .Map(dest => dest.PersonAlphabetSeri, src => src.SeriAlpha)
                .Map(dest => dest.PersonSeri, src => src.Seri)
                .Map(dest => dest.PersonSerial, src => src.Serial)
                .Map(dest => dest.IsPersonSanaChecked, src => src.SanaState.ToNullabbleBoolean())
                .Map(dest => dest.PersonMobileNoState, src => src.MobileNoState.ToNullabbleBoolean())
                .Map(dest => dest.AmlakEskanState, src => src.AmlakEskanState.ToNullabbleBoolean())
                .Map(
                    dest => dest.IsPersonSabteAhvalChecked,
                    src => src.IsSabtahvalChecked.ToNullabbleBoolean()
                )
                .Map(
                    dest => dest.IsPersonSabteAhvalCorrect,
                    src => src.IsSabtahvalCorrect.ToNullabbleBoolean()
                )
                .Map(dest => dest.IsPersonAlive, src => src.IsAlive.ToNullabbleBoolean())
                .Map(dest => dest.PersonMobileNo, src => src.MobileNo)
                .Map(dest => dest.PersonEmail, src => src.Email)
                .Map(dest => dest.PersonPostalCode, src => src.PostalCode)
                .Map(dest => dest.PersonAddress, src => src.Address)
                .Map(dest => dest.PersonAddressType, src => src.AddressType)
                .Map(dest => dest.PersonTel, src => src.Tel)
                .Map(dest => dest.PersonFaxNo, src => src.FaxNo)
                .Map(dest => dest.PersonDescription, src => src.Description)
                .Map(dest => dest.CompanyRegisterLocation, src => src.CompanyRegisterLocation)
                .Map(dest => dest.HasGrowthJudgment, src => src.HasGrowthJudgment.ToNullabbleBoolean())
                .Map(dest => dest.IsMartyrApplicant, src => src.IsMartyrApplicant.ToNullabbleBoolean())
                .Map(dest => dest.GrowthDescription, src => src.GrowthDescription)
                .Map(dest => dest.GrowthLetterDate, src => src.GrowthLetterDate)
                .Map(dest => dest.GrowthLetterNo, src => src.GrowthLetterNo)
                .Map(dest => dest.GrowthJudgmentDate, src => src.GrowthJudgmentDate)
                .Map(dest => dest.GrowthJudgmentNo, src => src.GrowthJudgmentNo)
                .Map(dest => dest.CompanyRegisterNo, src => src.CompanyRegisterNo)
                .Map(dest => dest.CompanyRegisterDate, src => src.CompanyRegisterDate)
                .Map(dest => dest.LastLegalPaperNo, src => src.LastLegalPaperNo)
                .Map(dest => dest.LastLegalPaperDate, src => src.LastLegalPaperDate)
                .Map(dest => dest.PersonPassportNo, src => src.PassportNo)
                .Map(dest => dest.EstateInquiryId, src => src.EstateInquiryId);
            config.Compile();

            var documentPersonViewModel = documentPerson.Adapt<DocumentStandardContractPersonViewModel>(config);
            return documentPersonViewModel;
        }
    }
}
