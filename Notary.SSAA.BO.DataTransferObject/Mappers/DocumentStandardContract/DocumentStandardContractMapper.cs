using Notary.SSAA.BO.SharedKernel.Enumerations;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractMapper" />
    /// </summary>
    public static class DocumentStandardContractMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContract
        /// </summary>
        /// <param name="entity">The entity<see cref="Domain.Entities.Document"/></param>
        /// <param name="viewModel">The viewModel<see cref="CreateDocumentStandardContractCommand"/></param>
        public static void MapToDocumentStandardContract(ref Domain.Entities.Document entity, CreateDocumentStandardContractCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<CreateDocumentStandardContractCommand, Domain.Entities.Document>()
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId.First())
                .Map(dest => dest.WriteInBookDate, src => src.RequestWriteInBookDate)
                .Map(dest => dest.NationalNo, src => src.RequestNationalNo)
                .Map(dest => dest.ClassifyNo, src => src.RequestClassifyNo)
                .Map(dest => dest.BookPapersNo, src => src.RequestBookPapersNo)
                .Map(dest => dest.BookVolumeNo, src => src.RequestBookVolumeNo)
                .Map(dest => dest.DocumentSecretCode, src => src.DocumentSecretCode)
                .Map(dest => dest.CurrencyTypeId, src => src.RequestCurrencyTypeId != null ? src.RequestCurrencyTypeId.FirstOrDefault().ToNullableInt() : null)
                .Map(dest => dest.Price, src => src.RequestPrice)
                .Map(dest => dest.SabtPrice, src => src.RequestSabtPrice)
                .Map(dest => dest.RegionalPrice, src => src.RequestRegionPrice)
                .Map(dest => dest.HowToPay, src => src.RequestHowToPay)
                .Map(dest => dest.DocumentTypeTitle, src => src.DocumentInfoOther != null ? src.DocumentInfoOther.DocumentTypeTitle : null)
                .Map(dest => dest.IsBasedJudgment, src => src.IsRequestBasedJudgment.ToYesNo())
                .Map(dest => dest.IsRahProcessed, src => src.IsRequestRahProcessed.ToYesNo())
                .Map(dest => dest.IsCostCalculateConfirmed, src => src.IsRequestCostCalculateConfirmed.ToYesNo())
                .Map(dest => dest.IsCostPaymentConfirmed, src => src.IsRequestCostPaymentConfirmed.ToYesNo())
                .Map(dest => dest.IsFinalVerificationVisited, src => src.IsRequestFinalVerificationVisited.ToYesNo())
                .Map(dest => dest.IsSentToTaxOrganization, src => src.IsRequestSentToTaxOrganization.ToYesNo())
                .Map(dest => dest.DocumentEstateDealSummarySelecteds, src => src.IsRequestSentToTaxOrganization.ToYesNo())
                .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(entity, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContract
        /// </summary>
        /// <param name="entity">The entity<see cref="Domain.Entities.Document"/></param>
        /// <param name="viewModel">The viewModel<see cref="SaveDocumentStandardContractCommand"/></param>
        public static void MapToDocumentStandardContract(ref Domain.Entities.Document entity, SaveDocumentStandardContractCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<SaveDocumentStandardContractCommand, Domain.Entities.Document>()
                .Map(dest => dest.ScriptoriumId, src => src.RequestScriptoriumId)
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId.First())
                .Map(dest => dest.State, src => src.StateId.GetString())
                .Map(dest => dest.WriteInBookDate, src => src.RequestWriteInBookDate)
                // .Map(dest => dest.NationalNo, src => src.RequestNationalNo)
                .Map(dest => dest.ClassifyNo, src => src.RequestClassifyNo.ToNullableInt())
                .Map(dest => dest.CurrencyTypeId, src => src.RequestCurrencyTypeId != null ? src.RequestCurrencyTypeId.FirstOrDefault().ToNullableInt() : null)
                .Map(dest => dest.Price, src => src.RequestPrice)
                .Map(dest => dest.SabtPrice, src => src.RequestSabtPrice)
                .Map(dest => dest.RegionalPrice, src => src.RequestRegionPrice)
                .Map(dest => dest.HowToPay, src => src.RequestHowToPay)
                .Map(dest => dest.BookPapersNo, src => src.RequestBookPapersNo)
                .Map(dest => dest.BookVolumeNo, src => src.RequestBookVolumeNo)
                .Map(dest => dest.IsBasedJudgment, src => src.IsRequestBasedJudgment.ToYesNo())
                .Map(dest => dest.IsRahProcessed, src => src.IsRequestRahProcessed.ToYesNo())
                .Map(dest => dest.IsCostCalculateConfirmed, src => src.IsRequestCostCalculateConfirmed.ToYesNo())
                .Map(dest => dest.IsCostPaymentConfirmed, src => src.IsRequestCostPaymentConfirmed.ToYesNo())
                .Map(dest => dest.IsFinalVerificationVisited, src => src.IsRequestFinalVerificationVisited.ToYesNo())
                .Map(dest => dest.IsSentToTaxOrganization, src => src.IsRequestSentToTaxOrganization.ToYesNo())
                .Map(dest => dest.IsRegistered, src => src.IsRegistered.ToYesNo())
                .Map(dest => dest.RemoteRequestId, src => src.RemoteRequestId.ToNullableGuid())
                .Map(dest => dest.IsRemoteRequest, src => src.IsRemoteRequest.ToYesNo())
                .IgnoreNonMapped(true);

            config.Compile();

            viewModel.Adapt(entity, config);
        }








        /// <summary>
        /// The MapDocumentToDocumentStandardContractViewModel
        /// </summary>
        /// <param name="document">The document<see cref="Domain.Entities.Document"/></param>
        /// <returns>The <see cref="DocumentStandardContractViewModel"/></returns>
        public static DocumentStandardContractViewModel MapDocumentStandardContractToDocumentStandardContractViewModel(
            Domain.Entities.Document document
        )
        {
            //var config = new TypeAdapterConfig();
            TypeAdapterConfig<Domain.Entities.Document, DocumentStandardContractViewModel>
                  .NewConfig()
            //config.NewConfig<Domain.Entities.Document, DocumentStandardContractViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.RequestId, src => src.Id)
                .Map(dest => dest.RequestNo, src => src.RequestNo)
                .Map(dest => dest.RequestDate, src => src.RequestDate)
                .Map(dest => dest.RequestScriptoriumId, src => src.ScriptoriumId)
                .Map(dest => dest.RequestLegacyId, src => src.LegacyId)
                .Map(dest => dest.DocumentTypeId, src => src.DocumentTypeId != null ? new List<string> { src.DocumentTypeId.ToString() } : new List<string>())
                .Map(dest => dest.DocumentSubjectTypeId, src => src.DocumentInfoOther != null ? (src.DocumentInfoOther.DocumentTypeSubjectId != null ? new List<string> { src.DocumentInfoOther.DocumentTypeSubjectId.ToString() } : new List<string>()) : new List<string>())
                .Map(dest => dest.DocumentTypeGroupTwoId, src => src.DocumentType != null ? new List<string> { src.DocumentType.DocumentTypeGroup2Id } : new List<string> { })  //( src.DocumentType.DocumentTypeGroup2Id != null ? new List<string> { src.DocumentType.DocumentTypeGroup2Id.ToString () } : new List<string> () ) : new List<string> () )
                .Map(dest => dest.DocumentTypeGroupOneId, src => src.DocumentType != null ? new List<string> { src.DocumentType.DocumentTypeGroup1Id } : new List<string> { })  //( src.DocumentType.DocumentTypeGroup1Id != null ? new List<string> { src.DocumentType.DocumentTypeGroup1Id.ToString () } : new List<string> () ) : new List<string> () )
                .Map(dest => dest.RequestDate, src => src.RequestDate)
                .Map(dest => dest.DocumentDate, src => src.DocumentDate)
                .Map(dest => dest.RequestWriteInBookDate, src => src.WriteInBookDate)
                .Map(dest => dest.GetDocumentNoDate, src => src.GetDocumentNoDate)
                .Map(dest => dest.RequestSignDate, src => src.SignDate)
                .Map(dest => dest.RequestTime, src => src.RequestTime)
                .Map(dest => dest.RequestSignTime, src => src.SignTime)
                .Map(dest => dest.RequestNationalNo, src => src.NationalNo)
                .Map(dest => dest.RequestClassifyNo, src => src.ClassifyNo)
                .Map(dest => dest.RequestCurrencyTypeId, src => src.CurrencyTypeId != null ? new List<string> { src.CurrencyTypeId.ToString() } : new List<string>())
                .Map(dest => dest.RequestPrice, src => src.Price)
                .Map(dest => dest.RequestSabtPrice, src => src.SabtPrice)
                .Map(dest => dest.RequestRegionPrice, src => src.RegionalPrice)
                .Map(dest => dest.RequestHowToPay, src => src.HowToPay)
                .Map(dest => dest.RequestPrice, src => src.Price)
                .Map(dest => dest.DocumentState, src => src.State != null ? ((SharedKernel.Enumerations.NotaryRegServiceReqState)src.State.ToNullableInt()).GetEnumDescription() : null)
                .Map(dest => dest.StateId, src => (Int32.Parse(src.State)))
                .Map(dest => dest.RequestSabtPrice, src => src.SabtPrice)
                .Map(dest => dest.RequestBookPapersNo, src => src.BookPapersNo)
                .Map(dest => dest.RequestBookVolumeNo, src => src.BookVolumeNo)
                .Map(dest => dest.DocumentSecretCode, src => src.DocumentSecretCode)
                .Map(dest => dest.DocumentTypeTitle, src => src.DocumentTypeTitle)
                .Map(dest => dest.IsRequestBasedJudgment, src => src.IsBasedJudgment.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestRahProcessed, src => src.IsRahProcessed.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestCostCalculateConfirmed, src => src.IsCostCalculateConfirmed.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestCostPaymentConfirmed, src => src.IsCostPaymentConfirmed.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestFinalVerificationVisited, src => src.IsFinalVerificationVisited.ToNullabbleBoolean())
                .Map(dest => dest.IsRequestSentToTaxOrganization, src => src.IsSentToTaxOrganization.ToNullabbleBoolean())
                .Map(dest => dest.IsRegistered, src => src.IsRegistered.ToYesNo())
                 .Map(dest => dest.RemoteRequestId, src => src.RemoteRequestId.ToString())
                .Map(dest => dest.IsRemoteRequest, src => src.IsRemoteRequest.ToNullabbleBoolean())


                .Map(
                    dest => dest.DocumentPeople,
                    src => DocumentStandardContractPersonMapper.MapToDocumentStandardContractPeopleViewModel(src.DocumentPeople.ToList())
                )
                .Map(
                    dest => dest.DocumentRelatedPeople,
                    src =>
                       DocumentStandardContractRelatedPersonMapper.MapToDocumentStandardContractRelatedPeopleViewModel(
                            src.DocumentPersonRelatedDocuments.ToList()
                        )
                )
                .Map(
                    dest => dest.DocumentCases,
                    src => DocumentStandardContractCaseMapper.MapToDocumentStandardContractCasesViewModel(src.DocumentCases.ToList())
                )
                  .Map(
                    dest => dest.DocumentEstates,
                    src => DocumentStandardContractEstateMapper.MapToDocumentStandardContractEstatesViewModel(src.DocumentEstates.ToList())
                )
                 .Map(
                    dest => dest.DocumentVehicles,
                    src => DocumentStandardContractVehicleMapper.MapToDocumentStandardContractVehiclesViewModel(src.DocumentVehicles.ToList())
                )
                   .Map(
                    dest => dest.DocumentRelations,
                    src => DocumentStandardContractRelationMapper.MapToDocumentStandardContractRelationsViewModel(src.DocumentRelationDocuments.ToList())
                )
                  .Map(
                    dest => dest.DocumentInfoText,
                    src => DocumentStandardContractInfoTextMapper.MapToDocumentStandardContractInfoTextViewModel(src.DocumentInfoText)
                )
                   .Map(
                    dest => dest.DocumentInfoConfirm,
                    src => DocumentStandardContractInfoConfirmMapper.MapToDocumentStandardContractInfoConfirmViewModel(src.DocumentInfoConfirm)
                )
                  .Map(
                    dest => dest.DocumentInfoOther,
                    src => DocumentStandardContractInfoOtherMapper.MapToDocumentStandardContractInfoOtherViewModel(src.DocumentInfoOther)
                )
                        .Map(
                    dest => dest.RequestType,
                    src => DocumentStandardContractTypeMapper.MapToDocumentStandardContractTypeViewModel(src.DocumentType)

                )
                    .Map(
                    dest => dest.DocumentInfoJudgment,
                    src => DocumentStandardContractInfoJudgmentMapper.MapToDocumentStandardContractInfoJudgmentViewModel(src.DocumentInfoJudgement)
                )
                   .Map(
                    dest => dest.DocumentCosts,
                    src => DocuemntStandardContractCostMapper.MapToDocumentStandardContractCostsViewModel(src.DocumentCosts.ToList(), src.DocumentCostUnchangeds.ToList())
                ).Map(
                    dest => dest.DocumentPayments,
                    src => DocumentStandardContractPaymentMapper.MapToDocumentStandardContractPaymentsViewModel(src.DocumentPayments.ToList())
                )
                    .Map(
                    dest => dest.DocumentInquiries,
                    src => DocumentStandardContractInquiriesMapper.MapToDocumentStandardContractInquiriesViewModel(src.DocumentInquiries.ToList())
                )
                       .Map(
                    dest => dest.DocumentSms,
                    src => DocumentStandardContractSmsMapper.MapToDocumentStandardContractSmsViewModel(src.DocumentSms.ToList())
                )
                ;

            //config.Compile();
            //var documentViewModel = new DocumentStandardContractViewModel();
            //document.Adapt(documentViewModel, config);
            var documentViewModel = document.Adapt<DocumentStandardContractViewModel>();

            return documentViewModel;
        }



        /// <summary>
        /// The MapDocumentToSaveDocumentStandardContractCommand
        /// </summary>
        /// <param name="document">The document<see cref="Domain.Entities.Document"/></param>
        /// <returns>The <see cref="SaveDocumentStandardContractCommand"/></returns>
        public static SaveDocumentStandardContractCommand MapDocumentToSaveDocumentStandardContractCommand(
            Domain.Entities.Document document
        )
        {
            TypeAdapterConfig<Domain.Entities.Document, SaveDocumentStandardContractCommand>
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
                    src => DocumentStandardContractInfoOtherMapper.MapToDocumentStandardContractInfoOtherViewModel(src.DocumentInfoOther)
                )

              .IgnoreNonMapped(true);

            var documentViewModel = document.Adapt<SaveDocumentStandardContractCommand>();
            return documentViewModel;
        }


    }
}
