using Notary.SSAA.BO.SharedKernel.Enumerations;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.Commands.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DocumentMapper" />
    /// </summary>
    public static class DocumentMapper
    {
        /// <summary>
        /// The MapToDocument
        /// </summary>
        /// <param name="entity">The entity<see cref="Domain.Entities.Document"/></param>
        /// <param name="viewModel">The viewModel<see cref="CreateDocumentCommand"/></param>
        public static void MapToDocument ( ref Domain.Entities.Document entity, CreateDocumentCommand viewModel )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<CreateDocumentCommand, Domain.Entities.Document> ()
                .Map ( dest => dest.DocumentTypeId, src => src.DocumentTypeId.First () )
                .Map ( dest => dest.WriteInBookDate, src => src.RequestWriteInBookDate )
                .Map ( dest => dest.NationalNo, src => src.RequestNationalNo )
                .Map ( dest => dest.ClassifyNo, src => src.RequestClassifyNo )
                .Map ( dest => dest.BookPapersNo, src => src.RequestBookPapersNo )
                .Map ( dest => dest.BookVolumeNo, src => src.RequestBookVolumeNo )
                .Map ( dest => dest.DocumentSecretCode, src => src.DocumentSecretCode )
                .Map ( dest => dest.CurrencyTypeId, src => src.RequestCurrencyTypeId != null ? src.RequestCurrencyTypeId.FirstOrDefault ().ToNullableInt () : null )
                .Map ( dest => dest.Price, src => src.RequestPrice )
                .Map ( dest => dest.SabtPrice, src => src.RequestSabtPrice )
                .Map ( dest => dest.RegionalPrice, src => src.RequestRegionPrice )
                .Map ( dest => dest.HowToPay, src => src.RequestHowToPay )
                .Map ( dest => dest.DocumentTypeTitle, src => src.DocumentInfoOther != null ? src.DocumentInfoOther.DocumentTypeTitle : null )
                .Map ( dest => dest.IsBasedJudgment, src => src.IsRequestBasedJudgment.ToYesNo () )
                .Map ( dest => dest.IsRahProcessed, src => src.IsRequestRahProcessed.ToYesNo () )
                .Map ( dest => dest.IsCostCalculateConfirmed, src => src.IsRequestCostCalculateConfirmed.ToYesNo () )
                .Map ( dest => dest.IsCostPaymentConfirmed, src => src.IsRequestCostPaymentConfirmed.ToYesNo () )
                .Map ( dest => dest.IsFinalVerificationVisited, src => src.IsRequestFinalVerificationVisited.ToYesNo () )
                .Map ( dest => dest.IsSentToTaxOrganization, src => src.IsRequestSentToTaxOrganization.ToYesNo () )
                .Map ( dest => dest.DocumentEstateDealSummarySelecteds, src => src.IsRequestSentToTaxOrganization.ToYesNo () )
                .IgnoreNonMapped ( true );
            config.Compile ();
            viewModel.Adapt ( entity, config );
        }

        /// <summary>
        /// The MapToDocument
        /// </summary>
        /// <param name="entity">The entity<see cref="Domain.Entities.Document"/></param>
        /// <param name="viewModel">The viewModel<see cref="SaveDocumentCommand"/></param>
        public static void MapToDocument ( ref Domain.Entities.Document entity, SaveDocumentCommand viewModel )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<SaveDocumentCommand, Domain.Entities.Document> ()
                .Map(dest => dest.ScriptoriumId, src => src.RequestScriptoriumId)
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId.First())
                .Map(dest => dest.State, src => src.StateId.GetString())
                .Map(dest => dest.WriteInBookDate, src => src.RequestWriteInBookDate)
               // .Map(dest => dest.NationalNo, src => src.RequestNationalNo)
                .Map(dest => dest.ClassifyNo, src => src.RequestClassifyNo.ToNullableInt())
                .Map ( dest => dest.CurrencyTypeId, src => src.RequestCurrencyTypeId != null ? src.RequestCurrencyTypeId.FirstOrDefault ().ToNullableInt () : null )
                .Map ( dest => dest.Price, src => src.RequestPrice )
                .Map ( dest => dest.SabtPrice, src => src.RequestSabtPrice )
                .Map ( dest => dest.RegionalPrice, src => src.RequestRegionPrice )
                .Map ( dest => dest.HowToPay, src => src.RequestHowToPay )
                .Map ( dest => dest.BookPapersNo, src => src.RequestBookPapersNo )
                .Map ( dest => dest.BookVolumeNo, src => src.RequestBookVolumeNo )
                .Map ( dest => dest.IsBasedJudgment, src => src.IsRequestBasedJudgment.ToYesNo () )
                .Map ( dest => dest.IsRahProcessed, src => src.IsRequestRahProcessed.ToYesNo () )
                .Map ( dest => dest.IsCostCalculateConfirmed, src => src.IsRequestCostCalculateConfirmed.ToYesNo () )
                .Map ( dest => dest.IsCostPaymentConfirmed, src => src.IsRequestCostPaymentConfirmed.ToYesNo () )
                .Map ( dest => dest.IsFinalVerificationVisited, src => src.IsRequestFinalVerificationVisited.ToYesNo () )
                .Map ( dest => dest.IsSentToTaxOrganization, src => src.IsRequestSentToTaxOrganization.ToYesNo () )
                .Map ( dest => dest.IsRegistered, src => src.IsRegistered.ToYesNo () )
                .Map ( dest => dest.RemoteRequestId, src => src.RemoteRequestId.ToNullableGuid () )
                .Map ( dest => dest.IsRemoteRequest, src => src.IsRemoteRequest.ToYesNo ())
                .IgnoreNonMapped ( true );

            config.Compile ();

            viewModel.Adapt ( entity, config );
        }








        /// <summary>
        /// The MapDocumentToDocumentViewModel
        /// </summary>
        /// <param name="document">The document<see cref="Domain.Entities.Document"/></param>
        /// <returns>The <see cref="DocumentViewModel"/></returns>
        public static DocumentViewModel MapDocumentToDocumentViewModel (
            Domain.Entities.Document document
        )
        {
            //var config = new TypeAdapterConfig();
            TypeAdapterConfig<Domain.Entities.Document, DocumentViewModel>
                  .NewConfig()
            //config.NewConfig<Domain.Entities.Document, DocumentViewModel>()
                .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.RequestId, src => src.Id )
                .Map ( dest => dest.RequestNo, src => src.RequestNo )
                .Map ( dest => dest.RequestDate, src => src.RequestDate )
                .Map ( dest => dest.RequestScriptoriumId, src => src.ScriptoriumId )
                .Map ( dest => dest.RequestLegacyId, src => src.LegacyId )
                .Map ( dest => dest.DocumentTypeId, src => src.DocumentTypeId != null ? new List<string> { src.DocumentTypeId.ToString () } : new List<string> () )
                .Map ( dest => dest.DocumentSubjectTypeId, src => src.DocumentInfoOther != null ? ( src.DocumentInfoOther.DocumentTypeSubjectId != null ? new List<string> { src.DocumentInfoOther.DocumentTypeSubjectId.ToString () } : new List<string> () ) : new List<string> () )
                .Map ( dest => dest.DocumentTypeGroupTwoId, src => src.DocumentType != null ? new List<string> { src.DocumentType.DocumentTypeGroup2Id }:new List<string> { })  //( src.DocumentType.DocumentTypeGroup2Id != null ? new List<string> { src.DocumentType.DocumentTypeGroup2Id.ToString () } : new List<string> () ) : new List<string> () )
                .Map ( dest => dest.DocumentTypeGroupOneId, src => src.DocumentType != null ? new List<string> { src.DocumentType.DocumentTypeGroup1Id } : new List<string> { } )  //( src.DocumentType.DocumentTypeGroup1Id != null ? new List<string> { src.DocumentType.DocumentTypeGroup1Id.ToString () } : new List<string> () ) : new List<string> () )
                .Map ( dest => dest.RequestDate, src => src.RequestDate )
                .Map ( dest => dest.DocumentDate, src => src.DocumentDate )
                .Map ( dest => dest.RequestWriteInBookDate, src => src.WriteInBookDate )
                .Map ( dest => dest.GetDocumentNoDate, src => src.GetDocumentNoDate )
                .Map ( dest => dest.RequestSignDate, src => src.SignDate )
                .Map ( dest => dest.RequestTime, src => src.RequestTime )
                .Map ( dest => dest.RequestSignTime, src => src.SignTime )
                .Map ( dest => dest.RequestNationalNo, src => src.NationalNo )
                .Map ( dest => dest.RequestClassifyNo, src => src.ClassifyNo )
                .Map ( dest => dest.RequestCurrencyTypeId, src => src.CurrencyTypeId != null ? new List<string> { src.CurrencyTypeId.ToString () } : new List<string> () )
                .Map ( dest => dest.RequestPrice, src => src.Price )
                .Map ( dest => dest.RequestSabtPrice, src => src.SabtPrice )
                .Map ( dest => dest.RequestRegionPrice, src => src.RegionalPrice )
                .Map ( dest => dest.RequestHowToPay, src => src.HowToPay )
                .Map ( dest => dest.RequestPrice, src => src.Price )
                .Map ( dest => dest.DocumentState, src => src.State != null ? ( ( SharedKernel.Enumerations.NotaryRegServiceReqState ) src.State.ToNullableInt () ).GetEnumDescription () : null )
                .Map ( dest => dest.StateId, src => ( Int32.Parse( src.State ) ) )
                .Map ( dest => dest.RequestSabtPrice, src => src.SabtPrice )
                .Map ( dest => dest.RequestBookPapersNo, src => src.BookPapersNo )
                .Map ( dest => dest.RequestBookVolumeNo, src => src.BookVolumeNo )
                .Map ( dest => dest.DocumentSecretCode, src => src.DocumentSecretCode )
                .Map ( dest => dest.DocumentTypeTitle, src => src.DocumentTypeTitle )
                .Map ( dest => dest.IsRequestBasedJudgment, src => src.IsBasedJudgment.ToNullabbleBoolean () )
                .Map ( dest => dest.IsRequestRahProcessed, src => src.IsRahProcessed.ToNullabbleBoolean () )
                .Map ( dest => dest.IsRequestCostCalculateConfirmed, src => src.IsCostCalculateConfirmed.ToNullabbleBoolean () )
                .Map ( dest => dest.IsRequestCostPaymentConfirmed, src => src.IsCostPaymentConfirmed.ToNullabbleBoolean () )
                .Map ( dest => dest.IsRequestFinalVerificationVisited, src => src.IsFinalVerificationVisited.ToNullabbleBoolean () )
                .Map ( dest => dest.IsRequestSentToTaxOrganization, src => src.IsSentToTaxOrganization.ToNullabbleBoolean () )
                .Map ( dest => dest.IsRegistered, src => src.IsRegistered.ToYesNo () )
                 .Map(dest => dest.RemoteRequestId, src => src.RemoteRequestId.ToString())
                .Map(dest => dest.IsRemoteRequest, src => src.IsRemoteRequest.ToNullabbleBoolean())


                .Map (
                    dest => dest.DocumentPeople,
                    src => DocumentPersonMapper.MapToDocumentPeopleViewModel ( src.DocumentPeople.ToList () )
                )
                .Map (
                    dest => dest.DocumentRelatedPeople,
                    src =>
                       DocumentRelatedPersonMapper.MapToDocumentRelatedPeopleViewModel (
                            src.DocumentPersonRelatedDocuments.ToList ()
                        )
                )
                .Map (
                    dest => dest.DocumentCases,
                    src => DocumentCaseMapper.MapToDocumentCasesViewModel ( src.DocumentCases.ToList () )
                )
                  .Map (
                    dest => dest.DocumentEstates,
                    src => DocumentEstateMapper.MapToDocumentEstatesViewModel ( src.DocumentEstates.ToList () )
                )
                 .Map (
                    dest => dest.DocumentVehicles,
                    src => DocumentVehicleMapper.MapToDocumentVehiclesViewModel ( src.DocumentVehicles.ToList () )
                )
                   .Map (
                    dest => dest.DocumentRelations,
                    src => DocumentRelationMapper.MapToDocumentRelationsViewModel ( src.DocumentRelationDocuments.ToList () )
                )
                  .Map (
                    dest => dest.DocumentInfoText,
                    src => DocumentInfoTextMapper.MapToDocumentInfoTextViewModel ( src.DocumentInfoText )
                )
                   .Map (
                    dest => dest.DocumentInfoConfirm,
                    src => DocumentInfoConfirmMapper.MapToDocumentInfoConfirmViewModel ( src.DocumentInfoConfirm )
                )
                  .Map (
                    dest => dest.DocumentInfoOther,
                    src => DocumentInfoOtherMapper.MapToDocumentInfoOtherViewModel ( src.DocumentInfoOther )
                )
                        .Map (
                    dest => dest.RequestType,
                    src => DocumentTypeMapper.MapToDocumentTypeViewModel ( src.DocumentType )

                )
                    .Map (
                    dest => dest.DocumentInfoJudgment,
                    src => DocumentInfoJudgmentMapper.MapToDocumentInfoJudgmentViewModel ( src.DocumentInfoJudgement )
                )
                   .Map (
                    dest => dest.DocumentCosts,
                    src => DocuemntCostMapper.MapToDocumentCostsViewModel ( src.DocumentCosts.ToList (), src.DocumentCostUnchangeds.ToList () )
                )  .Map(
                    dest => dest.DocumentPayments,
                    src => DocumentPaymentMapper.MapToDocumentPaymentsViewModel(src.DocumentPayments.ToList())
                )
                    .Map (
                    dest => dest.DocumentInquiries,
                    src => DocumentInquiriesMapper.MapToDocumentInquiriesViewModel ( src.DocumentInquiries.ToList () )
                )
                       .Map(
                    dest => dest.DocumentSms,
                    src => DocumentSmsMapper.MapToDocumentSmsViewModel(src.DocumentSms.ToList())
                )
                ;

            //config.Compile();
            //var documentViewModel = new DocumentViewModel();
            //document.Adapt(documentViewModel, config);
            var documentViewModel = document.Adapt<DocumentViewModel>();

            return documentViewModel;
        }



        /// <summary>
        /// The MapDocumentToSaveDocumentCommand
        /// </summary>
        /// <param name="document">The document<see cref="Domain.Entities.Document"/></param>
        /// <returns>The <see cref="SaveDocumentCommand"/></returns>
        public static SaveDocumentCommand MapDocumentToSaveDocumentCommand(
            Domain.Entities.Document document
        )
        {
            TypeAdapterConfig<Domain.Entities.Document, SaveDocumentCommand>
                .NewConfig()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => false)
                .Map(dest => dest.RequestId, src => src.Id.ToString())
                .Map(dest => dest.RequestNo, src => src.RequestNo)
                .Map(dest => dest.RequestDate, src => src.RequestDate)
                .Map(dest => dest.RequestScriptoriumId, src => src.ScriptoriumId)
                .Map(dest => dest.RequestLegacyId, src => src.LegacyId)
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId != null ? new List<string> { src.DocumentTypeId.ToString() } : new List<string>())
                .Map(dest => dest.RequestDate, src => src.RequestDate)
                .Map(dest => dest.DocumentDate, src => src.DocumentDate)
                .Map(dest => dest.RequestWriteInBookDate, src => src.WriteInBookDate)
                .Map(dest => dest.GetDocumentNoDate, src => src.GetDocumentNoDate)
                .Map(dest => dest.RequestSignDate, src => src.SignDate)
                .Map(dest => dest.RequestTime, src => src.RequestTime)
                .Map(dest => dest.RequestSignTime, src => src.SignTime)
                .Map(dest => dest.RequestNationalNo, src => src.NationalNo)
                .Map(dest => dest.RequestClassifyNo, src => src.ClassifyNo.To_String())
                .Map(dest => dest.RequestCurrencyTypeId, src => src.CurrencyTypeId != null ? new List<string> { src.CurrencyTypeId.ToString() } : new List<string>())
                .Map(dest => dest.RequestPrice, src => src.Price != null ? src.Price.ToString() : null)
                .Map(dest => dest.RequestRegionPrice, src => src.RegionalPrice)
                .Map(dest => dest.RequestHowToPay, src => src.HowToPay)
                .Map(dest => dest.DocumentState, src => src.State != null ? ((SharedKernel.Enumerations.NotaryRegServiceReqState)src.State.ToNullableInt()).GetEnumDescription() : null)
                .Map(dest => dest.StateId, src => (Int32.Parse(src.State)))
                .Map(dest => dest.RequestSabtPrice, src => src.SabtPrice != null ? src.SabtPrice.ToString() : null)
                .Map(dest => dest.RequestBookPapersNo, src => src.BookPapersNo)
                .Map(dest => dest.RequestBookVolumeNo, src => src.BookVolumeNo)
                .Map(dest => dest.DocumentSecretCode, src => src.DocumentSecretCode)
                .Map(dest => dest.IsRequestBasedJudgment, src => src.IsBasedJudgment.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestRahProcessed, src => src.IsRahProcessed.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestCostCalculateConfirmed, src => src.IsCostCalculateConfirmed.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestCostPaymentConfirmed, src => src.IsCostPaymentConfirmed.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestFinalVerificationVisited, src => src.IsFinalVerificationVisited.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestSentToTaxOrganization, src => src.IsSentToTaxOrganization.ToNullabbleBoolean())
                .Map(dest => dest.IsRegistered, src => src.IsBasedJudgment.ToNullabbleBoolean())

                  .Map(
                    dest => dest.DocumentInfoOther,
                    src => DocumentInfoOtherMapper.MapToDocumentInfoOtherViewModel(src.DocumentInfoOther)
                )
      
              .IgnoreNonMapped(true);

            var documentViewModel = document.Adapt<SaveDocumentCommand>();
            return documentViewModel;
        }


    }
}
