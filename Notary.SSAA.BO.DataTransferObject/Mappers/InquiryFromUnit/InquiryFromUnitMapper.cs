using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.InquiryFromUnit;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.InquiryFromUnit;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.InquiryFromUnit
{
    public static class InquiryFromUnitMapper
    {
        public static InquiryFromUnitGridViewModel ToViewModel(InquiryFromUnitGrid entity)
        {
            InquiryFromUnitGridViewModel inquiryFromUnitGridViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<InquiryFromUnitGrid, InquiryFromUnitGridViewModel>()
                .Ignore(src => src.SelectedItems)
                .Ignore(src => src.GridItems);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            inquiryFromUnitGridViewModel = entity.Adapt(inquiryFromUnitGridViewModel, config);

            return inquiryFromUnitGridViewModel;
        }
        public static InquiryFromUnitViewModel ToViewModel(Domain.Entities.InquiryFromUnit entity)
        {
            InquiryFromUnitViewModel inquiryFromUnitGridViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<Domain.Entities.InquiryFromUnit, InquiryFromUnitViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.InquiryUnitId, src => new List<string> { src.UnitId })
                .Map(dest => dest.InquiryFromUnitTypeId, src => new List<string> { src.InquiryFromUnitTypeId })
                .Map(dest => dest.InquiryFromUnitId, src => src.Id.ToString())
                .Map(dest => dest.InquiryNo, src => src.InquiryNo)
                .Map(dest => dest.InquiryDate, src => src.InquiryDate)
                .Map(dest => dest.InquiryStatementNo, src => src.StatementNo)
                .Map(dest => dest.InquiryText, src => src.InquiryText)
                .Map(dest => dest.InquiryFromUnitPersons, src => MapToViewModelList(src.InquiryFromUnitPeople.ToList()))
                .Map(dest => dest.InquiryReplyText, src => src.ReplyText)
                .Map(dest => dest.InquiryItemDescription, src => src.ItemDescription);
            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = entity.Adapt(inquiryFromUnitGridViewModel, config);

            return inquiryFromUnitGridViewModel;
        }
        public static InquiryFromUnitPersonViewModel ToViewModel(Domain.Entities.InquiryFromUnitPerson entity)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.InquiryFromUnitPerson, InquiryFromUnitPersonViewModel>().IgnoreNullValues(true)
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.PersonId, src => src.Id.ToString())
                .Map(dest => dest.InquiryFromUnitId, src => src.InquiryFromUnitId.ToString())
                .Map(dest => dest.RowNo, src => src.RowNo.ToString())
                .Map(dest => dest.IsPersonOriginal, src => src.IsOriginal.ToBoolean())
                .Map(dest => dest.IsPersonRelated, src => src.IsRelated.ToBoolean())
                .Map(dest => dest.IsPersonIranian, src => src.IsIranian.ToBoolean())
                .Map(dest => dest.PersonNationalityId, src => src.NationalityId.HasValue ? new List<string> { src.NationalityId.ToString().Trim() } : new List<string>())
                .Map(dest => dest.PersonNationalNo, src => src.NationalNo)
                .Map(dest => dest.PersonName, src => src.Name)
                .Map(dest => dest.PersonFamily, src => src.Family)
                .Map(dest => dest.PersonBirthDate, src => src.BirthDate)
                .Map(dest => dest.PersonSexType, src => src.SexType)
                .Map(dest => dest.PersonFatherName, src => src.FatherName)
                .Map(dest => dest.PersonIdentityIssueLocation, src => src.IdentityIssueLocation)
                .Map(dest => dest.PersonIdentityNo, src => src.IdentityNo)
                .Map(dest => dest.PersonAlphabetSeri, src => src.SeriAlpha)
                .Map(dest => dest.PersonSeri, src => src.Seri)
                .Map(dest => dest.PersonSerial, src => src.Serial)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.IsPersonSabteAhvalChecked, src => src.IsSabtahvalChecked.ToBoolean())
                .Map(dest => dest.IsPersonSabteAhvalCorrect, src => src.IsSabtahvalCorrect.ToNullabbleBoolean())
                .Map(dest => dest.IsPersonAlive, src => src.IsAlive.ToNullabbleBoolean())
                .Map(dest => dest.PersonMobileNo, src => src.MobileNo)
                .Map(dest => dest.PersonEmail, src => src.Email)
                .Map(dest => dest.PersonPostalCode, src => src.PostalCode)
                .Map(dest => dest.PersonAddress, src => src.Address)
                .Map(dest => dest.PersonTel, src => src.Tel)
                .Map(dest => dest.PersonDescription, src => src.Description)
                .Map(dest => dest.LegalPersonTypeId, src => src.LegalpersonTypeId.IsNullOrWhiteSpace() ? new List<string> { src.NationalityId.ToString().Trim() } : new List<string>())
                .Map(dest => dest.PersonEmail, src => src.Email)
                .Map(dest => dest.PersonPostalCode, src => src.PostalCode)
                .Map(dest => dest.PersonAddress, src => src.Address)
                .Map(dest => dest.PersonTel, src => src.Tel)
                .Map(dest => dest.PersonDescription, src => src.Description)
                .Map(dest => dest.CompanyRegisterDate, src => src.CompanyRegisterDate)
                .Map(dest => dest.CompanyRegisterNo, src => src.CompanyRegisterNo);

            // Apply the configuration
            config.Compile();
            var test = entity.Adapt<InquiryFromUnitPersonViewModel>(config);
            // Perform the mapping
            return entity.Adapt<InquiryFromUnitPersonViewModel>(config);

        }
        public static InquiryFromUnitViewModeltem ToItem(InquiryFromUnitGridItem entity)
        {
            InquiryFromUnitViewModeltem documentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<InquiryFromUnitViewModeltem, InquiryFromUnitGridItem>();

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = entity.Adapt(documentTemplateViewModel, config);

            return documentTemplateViewModel;
        }
        private static List<InquiryFromUnitPersonViewModel> MapToViewModelList(List<Domain.Entities.InquiryFromUnitPerson> inquiryFromUnitPersons)
        {
            List<InquiryFromUnitPersonViewModel> signRequestPersonViewModels = new();
            inquiryFromUnitPersons.ForEach(sp =>
            {
                signRequestPersonViewModels.Add(ToViewModel(sp));
            });
            return signRequestPersonViewModels;

        }
        public static void ToEntity(ref Domain.Entities.InquiryFromUnit entity, CreateInquiryFromUnitCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<CreateInquiryFromUnitCommand, Domain.Entities.InquiryFromUnit>()

                .Map(dest => dest.UnitId, src => src.InquiryUnitId.First())
                .Map(dest => dest.InquiryFromUnitTypeId, src => src.InquiryFromUnitTypeId.First())
                .Map(dest => dest.StatementNo, src => src.InquiryStatementNo)
                .Map(dest => dest.InquiryText, src => src.InquiryText)
                .Map(dest => dest.ItemDescription, src => src.InquiryItemDescription)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static void ToEntity(ref Domain.Entities.InquiryFromUnit entity, UpdateInquiryFromUnitCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<UpdateInquiryFromUnitCommand, Domain.Entities.InquiryFromUnit>()

                .Map(dest => dest.UnitId, src => src.InquiryUnitId.First())
                .Map(dest => dest.Id, src => src.InquiryFromUnitId.ToGuid())
                .Map(dest => dest.InquiryFromUnitTypeId, src => src.InquiryFromUnitTypeId.First())
                .Map(dest => dest.StatementNo, src => src.InquiryStatementNo)
                .Map(dest => dest.InquiryText, src => src.InquiryText)
                .Map(dest => dest.ItemDescription, src => src.InquiryItemDescription)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static void ToEntity(ref Domain.Entities.InquiryFromUnitPerson entity, InquiryFromUnitPersonViewModel viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<InquiryFromUnitPersonViewModel, Domain.Entities.InquiryFromUnitPerson>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.PersonId.ToGuid())
                .Map(dest => dest.InquiryFromUnit, src => src.InquiryFromUnitId.ToGuid())
                .Map(dest => dest.RowNo, src => src.RowNo.ToShort())
                .Map(dest => dest.IsRelated, src => src.IsPersonRelated.ToYesNo())
                .Map(dest => dest.IsIranian, src => src.IsPersonIranian.ToYesNo())
                .Map(dest => dest.NationalityId, src => src.PersonNationalityId.Count > 0 ? src.PersonNationalityId.First().ToNullableInt() : null)
                .Map(dest => dest.NationalNo, src => src.PersonNationalNo)
                .Map(dest => dest.Name, src => src.PersonName)
                .Map(dest => dest.Family, src => src.PersonFamily)
                .Map(dest => dest.BirthDate, src => src.PersonBirthDate)
                .Map(dest => dest.SexType, src => src.PersonSexType)
                .Map(dest => dest.FatherName, src => src.PersonFatherName)
                .Map(dest => dest.IdentityIssueLocation, src => src.PersonIdentityIssueLocation)
                .Map(dest => dest.IdentityNo, src => src.PersonIdentityNo)
                .Map(dest => dest.SeriAlpha, src => src.PersonAlphabetSeri)
                .Map(dest => dest.Seri, src => src.PersonSeri)
                .Map(dest => dest.Serial, src => src.PersonSerial)
                //.Map(dest => dest.SanaState, src => src.IsPersonSanaChecked.ToYesNo())
                .Map(dest => dest.IsOriginal, src => src.IsPersonOriginal.ToYesNo())
                //.Map(dest => dest.MobileNoState, src => src.PersonMobileNoState.ToYesNo())
                .Map(dest => dest.IsSabtahvalChecked, src => src.IsPersonSabteAhvalChecked.ToYesNo())
                .Map(dest => dest.IsSabtahvalCorrect, src => src.IsPersonSabteAhvalCorrect.ToYesNo())
                .Map(dest => dest.IsAlive, src => src.IsPersonAlive.ToYesNo())
                .Map(dest => dest.MobileNo, src => src.PersonMobileNo)
                .Map(dest => dest.Email, src => src.PersonEmail)
                .Map(dest => dest.PostalCode, src => src.PersonPostalCode)
                .Map(dest => dest.Address, src => src.PersonAddress)
                .Map(dest => dest.Tel, src => src.PersonTel)
                .Map(dest => dest.Description, src => src.PersonDescription)
                .IgnoreIf((src, dest) => src.IsNew, dest => dest.InquiryFromUnitId)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.CompanyRegisterDate, src => src.CompanyRegisterDate)
                //.Map(dest => dest.CompanyRegisterLocation, src => src.)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .Map(dest => dest.PersonType, src => src.PersonType)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }


    }
}
