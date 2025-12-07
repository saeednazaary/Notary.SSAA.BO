using Mapster;
using Microsoft.IdentityModel.Tokens;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.DataTransferObject.Commands.RelatedDocument;
using Notary.SSAA.BO.DataTransferObject.ViewModels.RelatedDocument;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.RelatedDocument
{
    public class RelatedDocumentMapper
    {
        #region ToEntity
        public static void ToEntity(ref Domain.Entities.Document masterEntity, CreateRelatedDocumentCommand request)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<CreateRelatedDocumentCommand, Domain.Entities.Document>()
                .Map(dest => dest.RelatedDocumentDate, src => src.RelatedDocumentDate)
                .Map(dest => dest.RelatedDocumentId, src => src.RelatedDocumentId.ToNullableGuid())
                .Map(dest => dest.RelatedDocumentTypeId, src => src.RelatedDocumentTypeId.FirstOrDefault())
                .Map(dest => dest.RelatedDocumentIsInSsar, src => src.IsRequestInSsar.ToYesNo())
                .Map(dest => dest.RelatedDocumentNo, src => src.RelatedDocumentNo)
                .Map(dest => dest.RelatedDocumentSecretCode, src => src.RelatedDocumentSecretCode)
                .Map(dest => dest.RelatedScriptoriumId, src => src.RelatedScriptoriumId)

                .Map(dest => dest.BookPapersNo, src => src.BookPapersNo)
                .Map(dest => dest.BookVolumeNo, src => src.BookVolumeNo)
                .Map(dest => dest.WriteInBookDate, src => src.WriteInBookDate)
                .Map(dest => dest.Price, src => !src.Price.IsNullOrWhiteSpace() ? src.Price.ToNullableLong() : null)
                .Map(dest => dest.ClassifyNo, src => !src.ClassifyNo.IsNullOrWhiteSpace() ? src.ClassifyNo.ToNullableInt() : null)
                .Map(dest => dest.DocumentDate, src => src.DocumentDate)
                .Map(dest => dest.DocumentSecretCode, src => src.DocumentSecretCode)
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId.FirstOrDefault())
                .Map(dest => dest.DocumentTypeTitle, src => src.DocumentTypeTitle)
                .Map(dest => dest.NationalNo, src => src.NationalNo)
                .IgnoreNonMapped(true);
            config.Compile();

            request.Adapt(masterEntity, config);
        }

        public static void ToEntity(ref Domain.Entities.Document masterEntity, UpdateRelatedDocumentCommand request)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<UpdateRelatedDocumentCommand, Domain.Entities.Document>()
                .Map(dest => dest.RelatedDocumentId, src => src.RelatedDocumentId.ToNullableGuid())
                .Map(dest => dest.RelatedDocumentSecretCode, src => src.RelatedDocumentSecretCode)
                .Map(dest => dest.RelatedDocumentTypeId, src => src.RelatedDocumentTypeId.FirstOrDefault())
                .Map(dest => dest.RelatedDocumentIsInSsar, src => src.IsRequestInSsar.ToYesNo())
                .Map(dest => dest.RelatedDocumentNo,src => src.RelatedDocumentNo)
                .Map(dest => dest.RelatedDocumentDate ,src => src.RelatedDocumentDate)
                .Map(dest => dest.BookPapersNo, src => src.BookPapersNo)
                .Map(dest => dest.BookVolumeNo, src => src.BookVolumeNo)
                .Map(dest => dest.WriteInBookDate, src => src.WriteInBookDate)
                .Map(dest => dest.Price, src => src.Price.ToNullableLong())
                .Map(dest => dest.ClassifyNo, src => src.ClassifyNo.ToNullableInt())
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId.FirstOrDefault())
                .Map(dest => dest.DocumentTypeTitle, src => src.DocumentTypeTitle)
                .IgnoreNonMapped(true);
            config.Compile();
            request.Adapt(masterEntity, config);
        }




        public static void MapToRelatedDocumentPerson(ref DocumentPerson Person, RelatedDocumentPersonViewModel relatedDocumentPersonViewModel)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<RelatedDocumentPersonViewModel, DocumentPerson>()
                .Map(dest => dest.Tel, src => src.Tel)
                .Map(dest => dest.PostalCode, src => src.PostalCode)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.MobileNo, src => src.MobileNo)
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.Id, src => src.PersonId.FirstOrDefault())
                .IgnoreNonMapped(true);
            config.Compile();

            relatedDocumentPersonViewModel.Adapt(Person, config);
        }

        public static void ToEntity(ref DocumentInfoText documentInfoText, RelatedDocumentInfoTextViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<RelatedDocumentInfoTextViewModel, DocumentInfoText>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.DocumentInfoTextId.ToGuid())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.DocumentDescription, src => src.DocumentDescription)
                .Map(dest => dest.LegalText, src => src.LegalText)
                .Map(dest => dest.DocumentText, src => src.DocumentText)
                .Ignore(src => src.ScriptoriumId)
                .Ignore(src => src.Ilm)
               .IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentId);
            // Apply the configuration
            config.Compile();
            // Perform the mapping by reference
            viewModel.Adapt(documentInfoText, config);
        }


        public static void ToEntity(ref DocumentInfoJudgement documentInfoText, RelatedDocumentInfoJudgmentViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<RelatedDocumentInfoJudgmentViewModel, DocumentInfoJudgement>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.RequestId.ToGuid())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.IssueNo, src => src.IssueNo)
                .Map(dest => dest.CaseClassifyNo, src => src.CaseClassifyNo)
                .Map(dest => dest.IssueDate, src => src.IssueDate)
                .Map(dest => dest.IssuerName, src => src.IssuerName)
.Map(dest => dest.DocumentJudgementTypeId,
     src => !src.DocumentJudgmentTypeId.IsNullOrEmpty()
            ? src.DocumentJudgmentTypeId.FirstOrDefault()
            : null).Ignore(src => src.ScriptoriumId)
                .Ignore(src => src.Ilm)
               .IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentId);
            // Apply the configuration
            config.Compile();
            // Perform the mapping by reference
            viewModel.Adapt(documentInfoText, config);
        }


        #endregion

        public static RelatedDocumentInfoJudgmentViewModel MapToRelatedDocumentInfoJudgmentViewModel(DocumentInfoJudgement databaseResult)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentInfoJudgement, RelatedDocumentInfoJudgmentViewModel>()
                .Map(dest => dest.RequestId, src => src.Id.ToString())
                .Map(dest => dest.DocumentId, src => src.Document.Id.ToString())
                .Map(dest => dest.IssueNo, src => src.IssueNo)
                .Map(dest => dest.CaseClassifyNo, src => src.CaseClassifyNo)
                .Map(dest => dest.IssueDate, src => src.IssueDate)
                .Map(dest => dest.IssuerName, src => src.IssuerName)
                 .Map(dest => dest.DocumentJudgmentTypeId, src => src.DocumentJudgementTypeId != null
                            ? new List<string> { src.DocumentJudgementTypeId.ToString() }
                            : new List<string>());
            return databaseResult.Adapt<RelatedDocumentInfoJudgmentViewModel>(config);
        }

        public static RelatedDocumentInfoTextViewModel MapToRelatedDocumentInfoTextViewModel(DocumentInfoText databaseResult)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<DocumentInfoText, RelatedDocumentInfoTextViewModel>()
                .Map(dest => dest.DocumentInfoTextId, src => src.Id.ToString())
                .Map(dest => dest.DocumentText, src => src.DocumentText)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.DocumentId, src => src.Document.Id.ToString())
                .Map(dest => dest.DocumentDescription, src => src.DocumentDescription)
                .Map(dest => dest.LegalText, src => src.LegalText);
            return databaseResult.Adapt<RelatedDocumentInfoTextViewModel>(config);
        }

        public static DocumentDetailViewModel ToDocumentDetailViewModel(Domain.Entities.Document document)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.Document, DocumentDetailViewModel>().IgnoreNullValues(true)

                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.RequestDocumentId, src => src.Id.ToString())
                .Map(dest => dest.RelatedDocumentId, src => src.RelatedDocumentId != null ? src.RelatedDocumentId.ToString() : null)
                .Map(dest => dest.RequestNo, src => src.RequestNo)
                .Map(dest => dest.DocumentDate, src => src.RequestDate)
                .Map(dest => dest.DocumentSecretCode, src => src.DocumentSecretCode)
                .Map(dest => dest.RelatedDocumentNo, src => src.RelatedDocumentNo)
                .Map(dest => dest.RelatedDocumentDate, src => src.RelatedDocumentDate)
                .Map(dest => dest.RelatedDocumentSecretCode, src => src.RelatedDocumentSecretCode)
                .Map(dest => dest.RelatedDocumentTypeId, src => src.RelatedDocumentTypeId != null
                            ? new List<string> { src.RelatedDocumentTypeId.ToString() }
                            : new List<string>())
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId != null
                            ? new List<string> { src.DocumentTypeId.ToString() }
                            : new List<string>())
                .Map(dest => dest.RelatedScriptoriumId, src => src.RelatedScriptoriumId)
                .Map(dest => dest.ScriptoriumId, src => src.ScriptoriumId)
                .Map(dest => dest.DocumentTypeTitle, src => src.DocumentTypeTitle)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.RegisterCount, src => src.DocumentInfoOther != null ? src.DocumentInfoOther.RegisterCount : null)
                .Map(dest => dest.BookPapersNo, src => src.BookPapersNo)
                .Map(dest => dest.IsRequestInSsar, src => src.RelatedDocumentIsInSsar.ToYesNo())
                .Map(dest => dest.BookVolumeNo, src => src.BookVolumeNo)
                .Map(dest => dest.WriteInBookDate, src => src.WriteInBookDate)
                .Map(dest => dest.ClassifyNo, src => src.ClassifyNo)
.Map(dest => dest.DocumentEstateId, src =>
    (src.DocumentEstates != null && src.DocumentEstates.Count > 0 && src.DocumentEstates.FirstOrDefault() != null)
        ? new List<string> { src.DocumentEstates.First().Id.ToString() }
        : new List<string>())
.Map(dest => dest.DocumentPerson.Tel, src =>
    (src.DocumentPeople != null && src.DocumentPeople.Count > 0 && src.DocumentPeople.FirstOrDefault() != null)
        ? src.DocumentPeople.First().Tel
        : null)
.Map(dest => dest.DocumentPerson.PostalCode, src =>
    (src.DocumentPeople != null && src.DocumentPeople.Count > 0 && src.DocumentPeople.FirstOrDefault() != null)
        ? src.DocumentPeople.First().PostalCode
        : null)
.Map(dest => dest.DocumentPerson.Address, src =>
    (src.DocumentPeople != null && src.DocumentPeople.Count > 0 && src.DocumentPeople.FirstOrDefault() != null)
        ? src.DocumentPeople.First().Address
        : null)
.Map(dest => dest.DocumentPerson.MobileNo, src =>
    (src.DocumentPeople != null && src.DocumentPeople.Count > 0 && src.DocumentPeople.FirstOrDefault() != null)
        ? src.DocumentPeople.First().MobileNo
        : null)
.Map(dest => dest.DocumentPerson.PersonId, src =>
    (src.DocumentPeople != null && src.DocumentPeople.Count > 0 && src.DocumentPeople.FirstOrDefault() != null)
        ? new List<string> { src.DocumentPeople.First().Id.ToString() }
        : new List<string>())
                .Map(dest => dest.DocumentInfoText, src => MapToRelatedDocumentInfoTextViewModel(src.DocumentInfoText))
                .Map(dest => dest.DocumentInfoJudgment, src => MapToRelatedDocumentInfoJudgmentViewModel(src.DocumentInfoJudgement))
                ;
            // Apply the configuration
            config.Compile();

            // Perform the mapping
            var considerationsDocumentViewModel = document.Adapt<DocumentDetailViewModel>(config);
            return considerationsDocumentViewModel;
        }


    }
}
