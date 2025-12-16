namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Stimulsoft.Report.Func;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractEstateMapper" />
    /// </summary>
    public static class DocumentStandardContractEstateMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractEstatesViewModel
        /// </summary>
        /// <param name="documentEstates">The documentEstates<see cref="List{DocumentEstate}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractEstateViewModel}"/></returns>
        public static List<DocumentStandardContractEstateViewModel> MapToDocumentStandardContractEstatesViewModel(
List<DocumentEstate> documentEstates
)
        {
            List<DocumentStandardContractEstateViewModel> documentEstatesViewModel =
                new List<DocumentStandardContractEstateViewModel>();
            documentEstates.ForEach(sp =>
            {
                documentEstatesViewModel.Add(MapToDocumentStandardContractEstateViewModel(sp));
            });
            return documentEstatesViewModel;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateViewModel
        /// </summary>
        /// <param name="documentEstate">The documentEstate<see cref="DocumentEstate"/></param>
        /// <returns>The <see cref="DocumentStandardContractEstateViewModel"/></returns>
        public static DocumentStandardContractEstateViewModel MapToDocumentStandardContractEstateViewModel(DocumentEstate documentEstate)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentEstate, DocumentStandardContractEstateViewModel>()
                  .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.EstateId, src => src.Id.ToString())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToString())
                .Map(dest => dest.RowNo, src => src.RowNo.ToString())
                .Map(dest => dest.IsAttachment, src => src.IsAttachment.ToNullabbleBoolean())
                .Map(dest => dest.IsEvacuated, src => src.IsEvacuated.ToNullabbleBoolean())
                .Map(dest => dest.IsRemortage, src => src.IsRemortage.ToNullabbleBoolean())
                .Map(dest => dest.IsRegistered, src => src.IsRegistered.ToNullabbleBoolean())
                .Map(dest => dest.IsProportionateQuota, src => src.IsProportionateQuota.ToNullabbleBoolean())
                .Map(dest => dest.IsFacilitationLaw, src => src.IsFacilitationLaw.ToNullabbleBoolean())
                .Map(dest => dest.MunicipalityNo, src => src.MunicipalityNo)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.PlaqueText, src => src.PlaqueText)
                .Map(dest => dest.GrandeeExceptionType, src => src.GrandeeExceptionType)
                .Map(dest => dest.Area, src => src.Area != null ? src.Area.ToString() : null)
                .Map(dest => dest.ReceiverPlaqueText, src => src.ReceiverPlaqueText)
                .Map(dest => dest.DivFromBasicPlaque, src => src.DivFromBasicPlaque)
                .Map(dest => dest.EstateInquiryId, src => src.EstateInquiryId)
                .Map(dest => dest.DivFromSecondaryPlaque, src => src.DivFromSecondaryPlaque)
                .Map(dest => dest.EvacuatedDate, src => src.EvacuatedDate)
                .Map(dest => dest.EvacuationDescription, src => src.EvacuationDescription)
                .Map(dest => dest.EvacuationPapers, src => src.EvacuationPapers)
                .Map(dest => dest.FieldOrGrandee, src => src.FieldOrGrandee)
                .Map(dest => dest.Floor, src => src.Floor)
                .Map(dest => dest.GrandeeExceptionDetailQuota, src => src.GrandeeExceptionDetailQuota != null ? src.GrandeeExceptionDetailQuota.ToString() : null)
                .Map(dest => dest.GrandeeExceptionTotalQuota, src => src.GrandeeExceptionTotalQuota != null ? src.GrandeeExceptionTotalQuota.ToString() : null)
                .Map(dest => dest.ImmovaleType, src => src.ImmovaleType)
                .Map(dest => dest.Limits, src => src.Limits)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.SomnyehRobeyehFieldGrandee, src => src.SomnyehRobeyehFieldGrandee)
                .Map(dest => dest.SomnyehRobeyehActionType, src => src.SomnyehRobeyehActionType)
                .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota != null ? src.OwnershipDetailQuota.ToString() : null)
                .Map(dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota != null ? src.OwnershipTotalQuota.ToString() : null)
                .Map(dest => dest.SellDetailQuota, src => src.SellDetailQuota != null ? src.SellDetailQuota.ToString() : null)
                .Map(dest => dest.SellTotalQuota, src => src.SellTotalQuota != null ? src.SellTotalQuota.ToString() : null)
                .Map(dest => dest.ReceiverBasicPlaqueHasRemain, src => src.ReceiverBasicPlaqueHasRemain)
                .Map(dest => dest.ReceiverSecondaryPlaqueHasRemain, src => src.ReceiverSecondaryPlaqueHasRemain)
                .Map(dest => dest.ReceiverDivFromBasicPlaque, src => src.ReceiverDivFromBasicPlaque)
                .Map(dest => dest.ReceiverDivFromSecondaryPlaque, src => src.ReceiverDivFromSecondaryPlaque)
                .Map(dest => dest.AttachmentType, src => src.AttachmentType)
                .Map(dest => dest.AttachmentSpecifications, src => src.AttachmentSpecifications)
                .Map(dest => dest.AttachmentTypeOthers, src => src.AttachmentTypeOthers)
                .Map(dest => dest.ReceiverBasicPlaque, src => src.ReceiverBasicPlaque)
                .Map(dest => dest.ReceiverSecondaryPlaque, src => src.ReceiverSecondaryPlaque)
                .Map(dest => dest.MunicipalityDate, src => src.MunicipalityDate)
                .Map(dest => dest.MunicipalityIssuer, src => src.MunicipalityIssuer)
                .Map(dest => dest.CommitmentPrice, src => src.CommitmentPrice != null ? src.CommitmentPrice.ToString() : null)
                .Map(dest => dest.SeparationText, src => src.SeparationText)
                .Map(dest => dest.SeparationType, src => src.SeparationType)
                .Map(dest => dest.SeparationNo, src => src.SeparationNo)
                .Map(dest => dest.SeparationDate, src => src.SeparationDate)
                .Map(dest => dest.SeparationIssuer, src => src.SeparationIssuer)
                .Map(dest => dest.OldSaleDescription, src => src.OldSaleDescription)
                .Map(dest => dest.AreaDescription, src => src.AreaDescription)
                .Map(dest => dest.LocationType, src => src.LocationType)
                .Map(dest => dest.Rights, src => src.Rights)
                .Map(dest => dest.Piece, src => src.Piece)
                .Map(dest => dest.Block, src => src.Block)
                .Map(dest => dest.SecondaryPlaque, src => src.SecondaryPlaque)
                .Map(dest => dest.RegisterCaseTitle, src => src.RegisterCaseTitle)
                .Map(dest => dest.AttachmentDescription, src => src.AttachmentDescription)
                .Map(dest => dest.SeparationDescription, src => src.SeparationDescription)
                .Map(dest => dest.OwnershipType, src => src.OwnershipType)
                .Map(dest => dest.RegionalPrice, src => src.RegionalPrice != null ? src.RegionalPrice.ToString() : null)
                .Map(dest => dest.Price, src => src.Price != null ? src.Price.ToString() : null)
                .Map(dest => dest.Commons, src => src.Commons)
                .Map(dest => dest.PostalCode, src => src.PostalCode)
                .Map(dest => dest.Direction, src => src.Direction)
                .Map(dest => dest.BasicPlaque, src => src.BasicPlaque)
                .Map(dest => dest.SecondaryPlaqueHasRemain, src => src.SecondaryPlaqueHasRemain)
                .Map(dest => dest.BasicPlaqueHasRemain, src => src.BasicPlaqueHasRemain)
                .Map(dest => dest.GeoLocationId, src => src.GeoLocationId != null
                ? new List<string> { src.GeoLocationId.ToString() }
                : new List<string>())

                               .Map(dest => dest.DocumentEstateSubSectionId, src => src.EstateSubsectionId != null
                ? new List<string> { src.EstateSubsectionId.ToString() }
                : new List<string>())

                .Map(dest => dest.DocumentEstateTypeId, src => src.DocumentEstateTypeId != null
                ? new List<string> { src.DocumentEstateTypeId.ToString() }
                : new List<string>())
                 .Map(dest => dest.DocumentEstateSectionId, src => src.EstateSectionId != null
                ? new List<string> { src.EstateSectionId.ToString() }
                : new List<string>())
                 .Map(dest => dest.DocumentUnitId, src => src.UnitId != null
                ? new List<string> { src.UnitId.ToString() }
                : new List<string>())
                 .Map(
                    dest => dest.DocumentEstateOwnerShips,
                    src => MapToDocumentStandardContractEstateOwnerShipsViewModel(src.DocumentEstateOwnershipDocuments.ToList())
                )
                  .Map(
                    dest => dest.DocumentEstateAttachments,
                    src => MapToDocumentStandardContractEstateAttachmentsViewModel(src.DocumentEstateAttachments.ToList())
                )
                  .Map(
                    dest => dest.DocumentEstateSeparationPieces,
                    src => MapToDocumentStandardContractEstateSeparationPiecesViewModel(src.DocumentEstateSeparationPieces.ToList())
                )
                .Map(dest => dest.DocumentEstateQuotums, src => MapToDocumentStandardContractEstateQuotaViewModels(src.DocumentEstateQuota))
                .Map(dest => dest.DocumentEstateQuotaDetails, src => MapToDocumentStandardContractEstateQuotaDetailViewModels(src.DocumentEstateQuotaDetails));

            config.Compile();

            var documentEstateViewModel = documentEstate.Adapt<DocumentStandardContractEstateViewModel>(config);
            return documentEstateViewModel;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateOwnerShipsViewModel
        /// </summary>
        /// <param name="documentEstateOwnerShips">The documentEstateOwnerShips<see cref="List{DocumentEstateOwnershipDocument}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractEstateOwnerShipViewModel}"/></returns>
        private static List<DocumentStandardContractEstateOwnerShipViewModel> MapToDocumentStandardContractEstateOwnerShipsViewModel(
List<DocumentEstateOwnershipDocument> documentEstateOwnerShips
)
        {
            List<DocumentStandardContractEstateOwnerShipViewModel> documentEstateOwnerShipViewModel =
                new List<DocumentStandardContractEstateOwnerShipViewModel>();
            documentEstateOwnerShips.ForEach(sp =>
            {
                documentEstateOwnerShipViewModel.Add(MapToDocumentStandardContractEstateOwnerShipViewModel(sp));
            });
            return documentEstateOwnerShipViewModel.OrderBy(x => x.RowNo).ToList();
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateOwnerShipViewModel
        /// </summary>
        /// <param name="documentEstateOwnerShip">The documentEstateOwnerShip<see cref="DocumentEstateOwnershipDocument"/></param>
        /// <returns>The <see cref="DocumentStandardContractEstateOwnerShipViewModel"/></returns>
        public static DocumentStandardContractEstateOwnerShipViewModel MapToDocumentStandardContractEstateOwnerShipViewModel(
DocumentEstateOwnershipDocument documentEstateOwnerShip
)
        {

            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentEstateOwnershipDocument, DocumentStandardContractEstateOwnerShipViewModel>()
                  .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.EstateOwnerShipId, src => src.Id.ToString())
                .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToString())
                .Map(dest => dest.RowNo, src => src.RowNo.ToString())

                .Map(dest => dest.OwnerShipDocumentPersonId, src => src.DocumentPersonId != null ? src.DocumentPersonId.MapToStringArray() : new List<string>())
                .Map(dest => dest.OwnerShipSpecificationsText, src => src.SpecificationsText)
                .Map(dest => dest.OwnerShipSabtStateReportUnitName, src => src.SabtStateReportUnitName)
                .Map(dest => dest.OwnerShipDealSummaryText, src => src.DealSummaryText)
                .Map(dest => dest.OwnerShipDescription, src => src.Description)
                .Map(dest => dest.OwnershipDocumentType, src => src.OwnershipDocumentType)
                .Map(dest => dest.OwnerShipDocumentPersonFullName, src => src.DocumentPerson != null ? src.DocumentPerson.Name + " " + src.DocumentPerson.Family : null)
                .Map(dest => dest.EstateInquiriesId, src => src.EstateInquiriesId)
                .Map(dest => dest.EstateBookNo, src => src.EstateBookNo)
                .Map(dest => dest.EstateBookPageNo, src => src.EstateBookPageNo)
                .Map(dest => dest.MortgageText, src => src.MortgageText)
                .Map(dest => dest.EstateElectronicPageNo, src => src.EstateElectronicPageNo)
                .Map(dest => dest.EstateDocumentNo, src => src.EstateDocumentNo)
                .Map(dest => dest.EstateIsReplacementDocument, src => src.EstateIsReplacementDocument)
                .Map(dest => dest.NotaryDocumentDate, src => src.NotaryDocumentDate)
                .Map(dest => dest.NotaryDocumentNo, src => src.NotaryDocumentNo)
                .Map(dest => dest.NotaryLocation, src => src.NotaryLocation)
                .Map(dest => dest.NotaryNo, src => src.NotaryNo)
                .Map(dest => dest.EstateDocumentType, src => src.EstateDocumentType)
                .Map(dest => dest.SabtStateReportDate, src => src.SabtStateReportDate)
                .Map(dest => dest.SabtStateReportNo, src => src.SabtStateReportNo)
                .Map(dest => dest.EstateBookType, src => src.EstateBookType)
                .Map(dest => dest.EstateSabtNo, src => src.EstateSabtNo)
                .Map(dest => dest.EstateSeridaftarId, src => src.EstateSeridaftarId != null ? src.EstateSeridaftarId.MapToStringArray() : new List<string>())



              ;
            config.Compile();

            var documentEstateOwnerShipViewModel = documentEstateOwnerShip.Adapt<DocumentStandardContractEstateOwnerShipViewModel>(config);
            return documentEstateOwnerShipViewModel;
        }

        /// <summary>
        /// <summary>
        /// The MapToDocumentStandardContractEstateAttachmentViewModel
        /// </summary>
        /// <param name="documentEstateAttachments">The documentEstateAttachments<see cref="List{DocumentEstatAttachment}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractEstateAttachmentViewModel}"/></returns>
        private static List<DocumentStandardContractEstateAttachmentViewModel> MapToDocumentStandardContractEstateAttachmentsViewModel(
List<DocumentEstateAttachment> documentEstateAttachments
)
        {
            List<DocumentStandardContractEstateAttachmentViewModel> documentEstateAttachmentsViewModel =
                new List<DocumentStandardContractEstateAttachmentViewModel>();
            if (documentEstateAttachments.Count > 0)
            {
                documentEstateAttachments.ForEach(sp =>
                {
                    documentEstateAttachmentsViewModel.Add(MapToDocumentStandardContractEstateAttachmentViewModel(sp));
                });
            }
            return documentEstateAttachmentsViewModel.OrderBy(x => x.RowNo).ToList();
        }


        /// <summary>
        /// The MapTodocumentEstateAttachmentViewModel
        /// </summary>
        /// <param name="documentEstateAttachment">The documentEstateAttachment<see cref="DocumentEstateAttachment"/></param>
        /// <returns>The <see cref="DocumentStandardContractEstateAttachmentViewModel"/></returns>
        public static DocumentStandardContractEstateAttachmentViewModel MapToDocumentStandardContractEstateAttachmentViewModel(
Domain.Entities.DocumentEstateAttachment documentEstateAttachment
)
        {

            var config = new TypeAdapterConfig();
            config
            .NewConfig<DocumentEstateAttachment, DocumentStandardContractEstateAttachmentViewModel>()
            .Map(dest => dest.IsNew, src => false)
            .Map(dest => dest.IsDelete, src => false)
            .Map(dest => dest.IsDirty, src => false)
            .Map(dest => dest.IsValid, src => true)
            .Map(dest => dest.EstateAttachmentId, src => src.Id.ToString())
            .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToString())
            .Map(dest => dest.RowNo, src => src.RowNo.ToString())
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AttachmentArea, src => src.Area.To_String())
            .Map(dest => dest.AttachmentNo, src => src.No)
            .Map(dest => dest.AttachmentLocation, src => src.Location)
            .Map(dest => dest.AttachmentType, src => src.AttachmentType)
              ;
            config.Compile();

            var documentEstateAttachmentViewModel = documentEstateAttachment.Adapt<DocumentStandardContractEstateAttachmentViewModel>(config);
            return documentEstateAttachmentViewModel;
        }


        /// <summary>
        /// The MapToDocumentStandardContractEstateSeparationPieceViewModel
        /// </summary>
        /// <param name="documentEstateSeparationPieces">The documentEstateSeparationPieces<see cref="List{DocumentEstatSeparationPiece}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractEstateSeparationPieceViewModel}"/></returns>
        private static List<DocumentStandardContractEstateSeparationPieceViewModel> MapToDocumentStandardContractEstateSeparationPiecesViewModel(
List<DocumentEstateSeparationPiece> documentEstateSeparationPieces
)
        {
            List<DocumentStandardContractEstateSeparationPieceViewModel> documentEstateSeparationPiecesViewModel =
                new List<DocumentStandardContractEstateSeparationPieceViewModel>();
            if (documentEstateSeparationPieces.Count > 0)
            {
                documentEstateSeparationPieces.ForEach(sp =>
                {
                    documentEstateSeparationPiecesViewModel.Add(MapToDocumentStandardContractEstateSeparationPieceViewModel(sp));
                });
            }
            return documentEstateSeparationPiecesViewModel.ToList();
        }


        /// <summary>
        /// The MapTodocumentSeparationPieceViewModel
        /// </summary>
        /// <param name="documentEstateSeparationPiece">The documentEstateSeparationPiece<see cref="DocumentEstateSeparationPiece"/></param>
        /// <returns>The <see cref="DocumentStandardContractEstateSeparationPieceViewModel"/></returns>
        public static DocumentStandardContractEstateSeparationPieceViewModel MapToDocumentStandardContractEstateSeparationPieceViewModel(
Domain.Entities.DocumentEstateSeparationPiece documentEstateSeparationPiece
)
        {
            var config = new TypeAdapterConfig();

            // Map base properties
            config.NewConfig<DocumentEstateSeparationPiece, DocumentStandardContractEstateSeparationPieceViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)

            // Map entity properties to view model properties
           .Map(dest => dest.EstateSeparationPieceId, src => src.Id.ToString())
           .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToString())
           .Map(dest => dest.Description, src => src.Description)
           .Map(dest => dest.PieceNo, src => src.PieceNo)
           .Map(dest => dest.FloorNo, src => src.Floor)
           .Map(dest => dest.Area, src => src.Area)
           .Map(dest => dest.BasicPlaque, src => src.BasicPlaque)
           .Map(dest => dest.SecondaryPlaqueNo, src => src.SecondaryPlaque)
           .Map(dest => dest.DivFromBasicPlaque, src => src.DividedFromSecondaryPlaque)
           .Map(dest => dest.DivFromSecondaryPlaque, src => src.DividedFromSecondaryPlaque)
           .Map(dest => dest.Commons, src => src.Commons)
           .Map(dest => dest.Rights, src => src.Rights)
           .Map(dest => dest.Direction, src => src.Direction)
           .Map(dest => dest.PlaqueText, src => src.PlaqueText)
                 .Map(dest => dest.PieceTypeId, src => src.EstatePieceTypeId != null
            ? new List<string> { src.EstatePieceTypeId.ToString() }
            : new List<string>())
           .Map(dest => dest.PieceTypeTitle, src => src.EstatePieceType == null ? null : src.EstatePieceType.Title)
           .Map(dest => dest.PieceKindId, src => src.DocumentEstateSeparationPieceKindId)
           .Map(dest => dest.PieceKindTitle, src => src.DocumentEstateSeparationPieceKind.Title)
            // Map lists of strings
             .Map(dest => dest.MeasurementUnitTypeId, src => src.MeasurementUnitTypeId != null
            ? new List<string> { src.MeasurementUnitTypeId.ToString() }
            : new List<string>())
           .Map(dest => dest.HasOwner, src => src.HasOwner)
           .Map(dest => dest.HasOwnerTitle, src => src.HasOwner.ToYesNoTitle())

            // Map boundary properties
            .Map(dest => dest.NorthLimits, src => src.NorthLimits)
            .Map(dest => dest.EasternLimits, src => src.EasternLimits)
            .Map(dest => dest.SouthLimits, src => src.SouthLimits)
            .Map(dest => dest.WesternLimits, src => src.WesternLimits)

            // Map ParkingId and AnbariId lists
            .Map(dest => dest.ParkingId, src => src.ParkingId != null
                    ? new List<string> { src.ParkingId.ToString() }
                    : new List<string>())
                .Map(dest => dest.AnbariId, src => src.AnbariId != null
                    ? new List<string> { src.AnbariId.ToString() }
                    : new List<string>())

                // Map DocumentSeparationPiecesAnbaries
                .Map(
            dest => dest.DocumentSeparationPiecesAnbaries,
            src => src.InverseAnbari.Select(x => x.Id.ToString()).ToList()
              //MapToDocumentStandardContractSeparationPiecesAnbariesViewModel(src.InverseAnbari.ToList())
              )

              // Map DocumentSeparationPiecesParkings

              .Map(
            dest => dest.DocumentSeparationPiecesParkings,
            src => src.InverseParking.Select(x => x.Id.ToString()).ToList()
            //MapToDocumentStandardContractSeparationPiecesParkingsViewModel(src.InverseParking.ToList())
            )
                  // Map DocumentEstateSeparationPiecesQuotaList

                  .Map(
                    dest => dest.DocumentEstateSeparationPiecesQuotaList,
                    src => MapToDocumentStandardContractEstateSeparationPiecesQuotaListViewModel(src.DocumentEstateSeparationPiecesQuota.ToList()));
            config.Compile();
            var documentEstateSeparationPieceViewModel = documentEstateSeparationPiece.Adapt<DocumentStandardContractEstateSeparationPieceViewModel>(config);
            return documentEstateSeparationPieceViewModel;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateSeparationPiecesQuotaListViewModel
        /// </summary>
        /// <param name="documentEstateSeparationPiecesQuotaList">The documentEstateSeparationPieces<see cref="List{DocumentEstateSeparationPiecesQuotum}"/></param>
        /// <returns>The <see cref="List{MapToDocumentStandardContractEstateSeparationPiecesQuotaListViewModel}"/></returns>
        private static List<DocumentStandardContractEstateSeparationPiecesQuotaViewModel> MapToDocumentStandardContractEstateSeparationPiecesQuotaListViewModel(
List<DocumentEstateSeparationPiecesQuotum> documentEstateSeparationPieces
)
        {
            List<DocumentStandardContractEstateSeparationPiecesQuotaViewModel> documentEstateSeparationPiecesViewModel =
                new List<DocumentStandardContractEstateSeparationPiecesQuotaViewModel>();
            if (documentEstateSeparationPieces.Count > 0)
            {
                documentEstateSeparationPieces.ForEach(sp =>
                {
                    documentEstateSeparationPiecesViewModel.Add(MapToDocumentStandardContractEstateSeparationPiecesQuotaViewModel(sp));
                });
            }
            return documentEstateSeparationPiecesViewModel.ToList();
        }

        /// <summary>
        /// Maps a DocumentEstateSeparationPiecesQuota entity to a DocumentStandardContractEstateSeparationPiecesQuotaViewModel.
        /// </summary>
        /// <param name="documentEstateSeparationPiecesQuota">The entity to map.</param>
        /// <returns>The mapped view model.</returns>
        private static DocumentStandardContractEstateSeparationPiecesQuotaViewModel MapToDocumentStandardContractEstateSeparationPiecesQuotaViewModel(
            Domain.Entities.DocumentEstateSeparationPiecesQuotum documentEstateSeparationPiecesQuota)
        {
            var config = new TypeAdapterConfig();

            // Map base properties
            config.NewConfig<DocumentEstateSeparationPiecesQuotum, DocumentStandardContractEstateSeparationPiecesQuotaViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)

            // Map entity properties to view model properties
           .Map(dest => dest.EstateSeparationPieceId, src => src.DocumentEstateSeparationPiecesId.ToString())
           .Map(dest => dest.EstateSeparationPiecesQuotaId, src => src.Id.ToString())
           .Map(dest => dest.PersonSeparationId, src => src.DocumentPersonId != null
            ? new List<string> { src.DocumentPersonId.ToString() }
            : new List<string>())
           .Map(dest => dest.DSUPersonConditionText, src => src.DealSummaryPersonConditions)
           .Map(dest => dest.PersonName, src => src.DocumentPerson.Name)
           .Map(dest => dest.PersonFamily, src => src.DocumentPerson.Family)
           .Map(dest => dest.PersonNationalNo, src => src.DocumentPerson.NationalNo)
           .Map(dest => dest.DetailQuota, src => src.DetailQuota.To_String())
           .Map(dest => dest.TotalQuota, src => src.TotalQuota.To_String())
           .Map(dest => dest.QuotaText, src => src.QuotaText);

            // Compile the configuration
            config.Compile();

            // Adapt the entity to the view model
            var documentEstateSeparationPiecesQuotaViewModel = documentEstateSeparationPiecesQuota.Adapt<DocumentStandardContractEstateSeparationPiecesQuotaViewModel>(config);

            return documentEstateSeparationPiecesQuotaViewModel;
        }
        /// The MapToDocumentStandardContractEstate
        /// </summary>
        /// <param name="DocumentEstate">The DocumentEstate<see cref="DocumentEstate"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractEstateViewModel"/></param>
        public static void MapToDocumentStandardContractEstate(ref DocumentEstate DocumentEstate, DocumentStandardContractEstateViewModel viewModel, bool? isRemoteRequest = false)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractEstateViewModel, DocumentEstate>()
            .Map(dest => dest.Id, src => isRemoteRequest == true ? src.EstateId.ToGuid() : (src.IsNew == true ? Guid.NewGuid() : src.EstateId.ToGuid())
            
            
            )
            .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
            .Map(dest => dest.RowNo, src => src.RowNo.ToByte())
            .Map(dest => dest.IsAttachment, src => src.IsAttachment.ToYesNo())
            .Map(dest => dest.IsEvacuated, src => src.IsEvacuated.ToYesNo())
            .Map(dest => dest.IsRemortage, src => src.IsRemortage.ToYesNo())
            .Map(dest => dest.IsRegistered, src => src.IsRegistered.ToYesNo())
            .Map(dest => dest.IsProportionateQuota, src => src.IsProportionateQuota.ToYesNo())
            .Map(dest => dest.IsFacilitationLaw, src => src.IsFacilitationLaw.ToYesNo())
            .Map(dest => dest.MunicipalityNo, src => src.MunicipalityNo)
            .Map(dest => dest.Address, src => src.Address)
            .Map(dest => dest.QuotaText, src => src.QuotaText)
            .Map(dest => dest.PlaqueText, src => src.PlaqueText)
            .Map(dest => dest.GrandeeExceptionType, src => src.GrandeeExceptionType)
            .Map(dest => dest.Area, src => src.Area.ToNullableDecimal())
            .Map(dest => dest.ReceiverPlaqueText, src => src.ReceiverPlaqueText)
            .Map(dest => dest.DivFromBasicPlaque, src => src.DivFromBasicPlaque)
            .Map(dest => dest.DivFromSecondaryPlaque, src => src.DivFromSecondaryPlaque)
            .Map(dest => dest.EvacuatedDate, src => src.EvacuatedDate)
            .Map(dest => dest.EvacuationDescription, src => src.EvacuationDescription)
            .Map(dest => dest.EvacuationPapers, src => src.EvacuationPapers)
            .Map(dest => dest.FieldOrGrandee, src => src.FieldOrGrandee)
            .Map(dest => dest.Floor, src => src.Floor)
            .Map(dest => dest.GeoLocationId, src => src.GeoLocationId.Count > 0 ? src.GeoLocationId.First().ToNullableInt() : null)
            .Map(dest => dest.GrandeeExceptionDetailQuota, src => src.GrandeeExceptionDetailQuota.ToNullableDecimal())
            .Map(dest => dest.GrandeeExceptionTotalQuota, src => src.GrandeeExceptionTotalQuota.ToNullableDecimal())
            .Map(dest => dest.ImmovaleType, src => src.ImmovaleType)
            .Map(dest => dest.Limits, src => src.Limits)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.SomnyehRobeyehFieldGrandee, src => src.SomnyehRobeyehFieldGrandee)
            .Map(dest => dest.SomnyehRobeyehActionType, src => src.SomnyehRobeyehActionType)
            .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota.ToNullableDecimal())
            .Map(dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota.ToNullableDecimal())
            .Map(dest => dest.SellDetailQuota, src => src.SellDetailQuota.ToNullableDecimal())
            .Map(dest => dest.SellTotalQuota, src => src.SellTotalQuota.ToNullableDecimal())
            .Map(dest => dest.ReceiverBasicPlaqueHasRemain, src => src.ReceiverBasicPlaqueHasRemain)
            .Map(dest => dest.ReceiverSecondaryPlaqueHasRemain, src => src.ReceiverSecondaryPlaqueHasRemain)
            .Map(dest => dest.ReceiverDivFromBasicPlaque, src => src.ReceiverDivFromBasicPlaque)
            .Map(dest => dest.ReceiverDivFromSecondaryPlaque, src => src.ReceiverDivFromSecondaryPlaque)
            .Map(dest => dest.AttachmentType, src => src.AttachmentType)
            .Map(dest => dest.AttachmentSpecifications, src => src.AttachmentSpecifications)
            .Map(dest => dest.AttachmentTypeOthers, src => src.AttachmentTypeOthers)
            .Map(dest => dest.ReceiverBasicPlaque, src => src.ReceiverBasicPlaque)
            .Map(dest => dest.ReceiverSecondaryPlaque, src => src.ReceiverSecondaryPlaque)
            .Map(dest => dest.MunicipalityDate, src => src.MunicipalityDate)
            .Map(dest => dest.MunicipalityIssuer, src => src.MunicipalityIssuer)
            .Map(dest => dest.CommitmentPrice, src => src.CommitmentPrice.ToNullableInt())
            .Map(dest => dest.SeparationText, src => src.SeparationText)
            .Map(dest => dest.SeparationType, src => src.SeparationType)
            .Map(dest => dest.SeparationNo, src => src.SeparationNo)
            .Map(dest => dest.SeparationDate, src => src.SeparationDate)
            .Map(dest => dest.SeparationIssuer, src => src.SeparationIssuer)
            .Map(dest => dest.OldSaleDescription, src => src.OldSaleDescription)
            .Map(dest => dest.AreaDescription, src => src.AreaDescription)
            .Map(dest => dest.LocationType, src => src.LocationType)
            .Map(dest => dest.Rights, src => src.Rights)
            .Map(dest => dest.Piece, src => src.Piece)
            .Map(dest => dest.Block, src => src.Block)
            .Map(dest => dest.SecondaryPlaque, src => src.SecondaryPlaque)
            .Map(dest => dest.RegisterCaseTitle, src => src.RegisterCaseTitle)
            .Map(dest => dest.AttachmentDescription, src => src.AttachmentDescription)
            .Map(dest => dest.SeparationDescription, src => src.SeparationDescription)
            .Map(dest => dest.OwnershipType, src => src.OwnershipType)
            .Map(dest => dest.RegionalPrice, src => src.RegionalPrice.ToNullableLong())
            .Map(dest => dest.Price, src => src.Price.ToNullableLong())
            .Map(dest => dest.Commons, src => src.Commons)
            .Map(dest => dest.PostalCode, src => src.PostalCode)
            .Map(dest => dest.Direction, src => src.Direction)
            .Map(dest => dest.BasicPlaque, src => src.BasicPlaque)
            .Map(dest => dest.SecondaryPlaqueHasRemain, src => src.SecondaryPlaqueHasRemain)
            .Map(dest => dest.BasicPlaqueHasRemain, src => src.BasicPlaqueHasRemain)
            .Map(dest => dest.DocumentEstateTypeId, src => src.DocumentEstateTypeId.Count > 0 ? src.DocumentEstateTypeId.First() : null)
            .Map(dest => dest.UnitId, src => src.DocumentUnitId.Count > 0 ? src.DocumentUnitId.First() : null)
            .Map(dest => dest.EstateSectionId, src => src.DocumentEstateSectionId.Count > 0 ? src.DocumentEstateSectionId.First() : null)
            .Map(dest => dest.EstateSubsectionId, src => src.DocumentEstateSubSectionId.Count > 0 ? src.DocumentEstateSubSectionId.First() : null)
                //  .Map(dest => dest.DocumentEstateAttachments, src => MapToDocumentStandardContractEstateAttachments(src.DocumentEstateAttachments))

                .Ignore(src => src.Ilm)
              // .IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentId)
               .IgnoreNonMapped(true);
            // Apply the configuration
            config.Compile();
            // Perform the mapping by reference
            viewModel.Adapt(DocumentEstate, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstate
        /// </summary>
        /// <param name="DocumentEstate">The DocumentEstate<see cref="DocumentEstateOwnershipDocument"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractEstateOwnerShipViewModel"/></param>
        public static void MapToDocumentStandardContractEstate(ref DocumentEstateOwnershipDocument DocumentEstate, DocumentStandardContractEstateOwnerShipViewModel viewModel, bool? isRemoteRequest = false)
        {
            var m = viewModel;

            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractEstateOwnerShipViewModel, DocumentEstateOwnershipDocument>()
                .Map(dest => dest.Id, src =>
                isRemoteRequest == true ? src.EstateOwnerShipId.ToGuid() : (src.IsNew == true ? Guid.NewGuid() : src.EstateOwnerShipId.ToGuid())
                )
                .Map(dest => dest.RowNo, src => src.RowNo.ToByte())
                .Map(dest => dest.DocumentPersonId, src => src.OwnerShipDocumentPersonId.Count > 0 ? src.OwnerShipDocumentPersonId.First() : null)
                .Map(dest => dest.SpecificationsText, src => src.OwnerShipSpecificationsText)
                .Map(dest => dest.SabtStateReportUnitName, src => src.OwnerShipSabtStateReportUnitName)
                .Map(dest => dest.DealSummaryText, src => src.OwnerShipDealSummaryText)
                .Map(dest => dest.Description, src => src.OwnerShipDescription)
                .Map(dest => dest.OwnershipDocumentType, src => src.OwnershipDocumentType)
                .Map(dest => dest.EstateSeridaftarId, src => src.EstateSeridaftarId != null && src.EstateSeridaftarId.Count > 0 ? src.EstateSeridaftarId.First() : null)

                .Ignore(src => src.Ilm);
               //.IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentEstateId);
            config.Compile();
            viewModel.Adapt(DocumentEstate, config);
        }
        /// <summary>
        /// The MapToDocumentStandardContractEstate
        /// </summary>
        /// <param name="DocumentEstate">The DocumentEstate<see cref="DocumentEstateSeparationPiece"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractEstateOwnerShipViewModel"/></param>
        public static void MapToDocumentStandardContractEstate(ref DocumentEstateSeparationPiece DocumentEstate, DocumentStandardContractEstateSeparationPieceViewModel viewModel)
        {
            var m = viewModel;

            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractEstateSeparationPieceViewModel, DocumentEstateSeparationPiece>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.EstateSeparationPieceId.ToGuid())
                .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToGuid())
           .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToString())
           .Map(dest => dest.Description, src => src.Description)
           .Map(dest => dest.PieceNo, src => src.PieceNo)
           .Map(dest => dest.Floor, src => src.FloorNo)
           .Map(dest => dest.Area, src => src.Area)
           .Map(dest => dest.BasicPlaque, src => src.BasicPlaque)
           .Map(dest => dest.SecondaryPlaque, src => src.SecondaryPlaqueNo)
           .Map(dest => dest.DividedFromSecondaryPlaque, src => src.DivFromBasicPlaque)
           .Map(dest => dest.DividedFromSecondaryPlaque, src => src.DivFromSecondaryPlaque)
           .Map(dest => dest.Commons, src => src.Commons)
           .Map(dest => dest.Rights, src => src.Rights)
           .Map(dest => dest.Direction, src => src.Direction)
           .Map(dest => dest.PlaqueText, src => src.PlaqueText)
            .Map(dest => dest.EstatePieceTypeId, src => src.PieceTypeId.Count > 0 ? src.PieceTypeId.First() : null)
           .Map(dest => dest.DocumentEstateSeparationPieceKindId, src => src.PieceKindId)
            // Map lists of strings
           .Map(dest => dest.MeasurementUnitTypeId, src => src.MeasurementUnitTypeId.Count > 0 ? src.MeasurementUnitTypeId.First() : null)
           .Map(dest => dest.HasOwner, src => src.HasOwner)
            // Map boundary properties
            .Map(dest => dest.NorthLimits, src => src.NorthLimits)
            .Map(dest => dest.EasternLimits, src => src.EasternLimits)
            .Map(dest => dest.SouthLimits, src => src.SouthLimits)
            .Map(dest => dest.WesternLimits, src => src.WesternLimits)

            // Map ParkingId and AnbariId lists
            .Map(dest => dest.ParkingId, src => src.ParkingId.Count > 0 ? src.ParkingId.First().ToNullableGuid() : null)
            .Map(dest => dest.AnbariId, src => src.AnbariId.Count > 0 ? src.AnbariId.First().ToNullableGuid() : null)
                .Ignore(src => src.Ilm)
               //.IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentEstateId)
               .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(DocumentEstate, config);
        }
        /// <summary>
        /// The MapToDocumentStandardContractEstateAttachments
        /// </summary>
        /// <param name="documentEstateAttachmentViewModels">The documentEstateAttachmentViewModels<see cref="IList{DocumentStandardContractEstateAttachmentViewModel}"/></param>
        /// <returns>The <see cref="List{DocumentEstateAttachment}"/></returns>
        public static List<Domain.Entities.DocumentEstateAttachment> MapToDocumentStandardContractEstateAttachments(IList<DocumentStandardContractEstateAttachmentViewModel> documentEstateAttachmentViewModels)
        {
            List<Domain.Entities.DocumentEstateAttachment> documentEstateAttachments = new List<Domain.Entities.DocumentEstateAttachment>();
            if (documentEstateAttachmentViewModels.Count > 0)
            {
                for (int i = 0; i < documentEstateAttachmentViewModels.Count; i++)
                {

                    Domain.Entities.DocumentEstateAttachment documentEstateAttachment = new();
                    MapToDocumentStandardContractEstateAttachment(ref documentEstateAttachment, documentEstateAttachmentViewModels[i]);
                    documentEstateAttachment.Ilm = DocumentConstants.CreateIlm;
                    documentEstateAttachments.Add(documentEstateAttachment);

                }
            }

            return documentEstateAttachments;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateAttachment
        /// </summary>
        /// <param name="DocumentEstateAttachment">The DocumentEstateAttachment<see cref="DocumentEstateAttachment"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractEstateAttachmentViewModel"/></param>
        public static void MapToDocumentStandardContractEstateAttachment(ref DocumentEstateAttachment DocumentEstateAttachment, DocumentStandardContractEstateAttachmentViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractEstateAttachmentViewModel, DocumentEstateAttachment>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.EstateAttachmentId.ToGuid())
                .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToGuid())
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Area, src => src.AttachmentArea.ToNullableDecimal())
                .Map(dest => dest.No, src => src.AttachmentNo)
                .Map(dest => dest.Location, src => src.AttachmentLocation)
                .Map(dest => dest.AttachmentType, src => src.AttachmentType)
                .Map(dest => dest.RowNo, src => src.RowNo.ToShort())
                .Ignore(src => src.ScriptoriumId)
                .Ignore(src => src.Ilm)
                 .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(DocumentEstateAttachment, config);
        }







        /// 





        /// <summary>
        /// The MapToDocumentStandardContractEstateQuotas
        /// </summary>
        /// <param name="documentEstateQuotaDetailViewModels">The documentEstateQuotaDetailViewModels<see cref="IList{DocumentStandardContractEstateQuotumViewModel}"/></param>
        /// <returns>The <see cref="List{DocumentEstateQuotum}"/></returns>
        public static List<DocumentEstateQuotum> MapToDocumentStandardContractEstateQuotas(IList<DocumentStandardContractEstateQuotumViewModel> documentEstateQuotaDetailViewModels)
        {
            List<DocumentEstateQuotum> documentEstateQuotums = new List<DocumentEstateQuotum>();
            if (documentEstateQuotaDetailViewModels.Count > 0)
            {
                for (int i = 0; i < documentEstateQuotaDetailViewModels.Count; i++)
                {

                    DocumentEstateQuotum documentEstateQuotum = new();
                    MapToDocumentStandardContractEstateQuotum(ref documentEstateQuotum, documentEstateQuotaDetailViewModels[i]);
                    documentEstateQuotum.Ilm = DocumentConstants.CreateIlm;
                    documentEstateQuotums.Add(documentEstateQuotum);

                }
            }

            return documentEstateQuotums;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateSeparationPiecesQuotas
        /// </summary>
        /// <param name="DocumentStandardContractEstateSeparationPiecesQuotaViewModel">The DocumentStandardContractEstateSeparationPiecesQuotaViewModel<see cref="IList{DocumentStandardContractEstateSeparationPiecesQuotaViewModel}"/></param>
        /// <returns>The <see cref="List{DocumentEstateSeparationPiecesQuotum}"/></returns>
        public static List<DocumentEstateSeparationPiecesQuotum> MapToDocumentStandardContractEstateSeparationPiecesQuotas(IList<DocumentStandardContractEstateSeparationPiecesQuotaViewModel> documentEstateQuotaDetailViewModels)
        {
            List<DocumentEstateSeparationPiecesQuotum> documentEstateQuotums = new List<DocumentEstateSeparationPiecesQuotum>();
            if (documentEstateQuotaDetailViewModels.Count > 0)
            {
                for (int i = 0; i < documentEstateQuotaDetailViewModels.Count; i++)
                {

                    DocumentEstateSeparationPiecesQuotum documentEstateQuotum = new();
                    MapToDocumentStandardContractEstateSeparationPiecesQuotam(ref documentEstateQuotum, documentEstateQuotaDetailViewModels[i]);
                    documentEstateQuotum.Ilm = DocumentConstants.CreateIlm;
                    documentEstateQuotums.Add(documentEstateQuotum);

                }
            }

            return documentEstateQuotums;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateQuotaViewModels
        /// </summary>
        /// <param name="documentEstateQuotaDetails">The documentEstateQuotaDetails<see cref="ICollection{DocumentEstateQuotum}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractEstateQuotumViewModel}"/></returns>
        public static List<DocumentStandardContractEstateQuotumViewModel> MapToDocumentStandardContractEstateQuotaViewModels(ICollection<DocumentEstateQuotum> documentEstateQuotaDetails)
        {
            List<DocumentStandardContractEstateQuotumViewModel> documentEstateQuotumViewModels = new List<DocumentStandardContractEstateQuotumViewModel>();
            if (documentEstateQuotaDetails.Count > 0)
            {
                for (int i = 0; i < documentEstateQuotaDetails.Count; i++)
                {

                    DocumentStandardContractEstateQuotumViewModel documentEstateQuotumViewModel = new();
                    MapToDocumentStandardContractEstateQuotumViewModel(ref documentEstateQuotumViewModel, documentEstateQuotaDetails.ElementAt(i));
                    documentEstateQuotumViewModels.Add(documentEstateQuotumViewModel);

                }
            }

            return documentEstateQuotumViewModels;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateQuotaDetails
        /// </summary>
        /// <param name="documentEstateQuotaDetailViewModels">The documentEstateQuotaDetailViewModels<see cref="IList{DocumentStandardContractEstateQuotaDetailViewModel}"/></param>
        /// <returns>The <see cref="List{DocumentEstateQuotaDetail}"/></returns>
        public static List<DocumentEstateQuotaDetail> MapToDocumentStandardContractEstateQuotaDetails(IList<DocumentStandardContractEstateQuotaDetailViewModel> documentEstateQuotaDetailViewModels)
        {
            List<DocumentEstateQuotaDetail> documentEstateQuotaDetails = new List<DocumentEstateQuotaDetail>();
            if (documentEstateQuotaDetailViewModels.Count > 0)
            {
                for (int i = 0; i < documentEstateQuotaDetailViewModels.Count; i++)
                {

                    DocumentEstateQuotaDetail documentEstateQuotaDetail = new();
                    MapToDocumentStandardContractEstateQuotaDetail(ref documentEstateQuotaDetail, documentEstateQuotaDetailViewModels[i]);
                    documentEstateQuotaDetail.Ilm = DocumentConstants.CreateIlm;
                    documentEstateQuotaDetails.Add(documentEstateQuotaDetail);

                }
            }

            return documentEstateQuotaDetails;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateQuotaDetailViewModels
        /// </summary>
        /// <param name="documentEstateQuotaDetails">The documentEstateQuotaDetails<see cref="ICollection{DocumentEstateQuotaDetail}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractEstateQuotaDetailViewModel}"/></returns>
        public static List<DocumentStandardContractEstateQuotaDetailViewModel> MapToDocumentStandardContractEstateQuotaDetailViewModels(ICollection<DocumentEstateQuotaDetail> documentEstateQuotaDetails)
        {
            List<DocumentStandardContractEstateQuotaDetailViewModel> documentEstateQuotaDetailViewModels = new List<DocumentStandardContractEstateQuotaDetailViewModel>();
            if (documentEstateQuotaDetails.Count > 0)
            {
                for (int i = 0; i < documentEstateQuotaDetails.Count; i++)
                {

                    DocumentStandardContractEstateQuotaDetailViewModel documentEstateQuotaDetailViewModel = new();
                    MapToDocumentStandardContractEstateQuotaDetailViewModel(ref documentEstateQuotaDetailViewModel, documentEstateQuotaDetails.ElementAt(i));
                    documentEstateQuotaDetailViewModels.Add(documentEstateQuotaDetailViewModel);

                }
            }

            return documentEstateQuotaDetailViewModels;
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateQuotaDetail
        /// </summary>
        /// <param name="DocumentEstateQuotaDetail">The DocumentEstateQuotaDetail<see cref="DocumentEstateQuotaDetail"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractEstateQuotaDetailViewModel"/></param>
        public static void MapToDocumentStandardContractEstateQuotaDetail(ref DocumentEstateQuotaDetail DocumentEstateQuotaDetail, DocumentStandardContractEstateQuotaDetailViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractEstateQuotaDetailViewModel, DocumentEstateQuotaDetail>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.DocumentEstateQuotaDetailId.ToGuid())
                .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToGuid())
                //.Map(dest => dest.RowNo, src => src.RowNo.ToByte())
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonBuyerId, src => src.DocumentPersonBuyerId.Count > 0 ? src.DocumentPersonBuyerId.First().ToNullableGuid() : null)
                .Map(dest => dest.DocumentPersonSellerId, src => src.DocumentPersonSellerId.Count > 0 ? src.DocumentPersonSellerId.First().ToNullableGuid() : null)
                .Map(dest => dest.DocumentEstateOwnershipDocumentId, src => src.EstateOwnerShipId.Count > 0 ? src.EstateOwnerShipId.First().ToNullableGuid() : null)
                .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota.ToNullableDecimal())
                .Map(dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota.ToNullableDecimal())
                .Map(dest => dest.DealSummaryPersonConditions, src => src.DealSummaryPersonConditions)
                .Map(dest => dest.SellDetailQuota, src => src.SellDetailQuota.ToNullableDecimal())
                .Map(dest => dest.SellTotalQuota, src => src.SellTotalQuota.ToNullableDecimal())
                .Ignore(src => src.Ilm)
                .Ignore(src => src.ScriptoriumId)

               .IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentEstateId)
                .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(DocumentEstateQuotaDetail, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateQuotum
        /// </summary>
        /// <param name="DocumentEstateQuotum">The DocumentEstateQuotum<see cref="DocumentEstateQuotum"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractEstateQuotumViewModel"/></param>
        public static void MapToDocumentStandardContractEstateQuotum(ref  Notary.SSAA.BO.Domain.Entities.DocumentEstateQuotum DocumentEstateQuotum, DocumentStandardContractEstateQuotumViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractEstateQuotumViewModel, DocumentEstateQuotum>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.DocumentEstateQuotumId.ToGuid())
                .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToGuid())
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonId, src => src.DocumentPersonId.Count > 0 ? src.DocumentPersonId.First() : null)
                .Map(dest => dest.DetailQuota, src => src.DetailQuota.ToNullableDecimal())
                .Map(dest => dest.TotalQuota, src => src.TotalQuota.ToNullableDecimal())
                .Ignore(src => src.ScriptoriumId)
                .Ignore(src => src.Ilm)
                 .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(DocumentEstateQuotum, config);
        }
        /// <summary>
        /// The MapToDocumentStandardContractEstateSeparationPiecesQuotam
        /// </summary>
        /// <param name="DocumentEstateSeparationPiecesQuotum">The DocumentEstateQuotum<see cref="DocumentEstateSeparationPiecesQuotum"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractEstateSeparationPiecesQuotaViewModel"/></param>
        public static void MapToDocumentStandardContractEstateSeparationPiecesQuotam(ref  Notary.SSAA.BO.Domain.Entities.DocumentEstateSeparationPiecesQuotum DocumentEstateQuotum, DocumentStandardContractEstateSeparationPiecesQuotaViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractEstateSeparationPiecesQuotaViewModel, DocumentEstateSeparationPiecesQuotum>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.EstateSeparationPiecesQuotaId.ToGuid())
                .Map(dest => dest.DocumentEstateSeparationPiecesId, src => src.EstateSeparationPieceId.ToGuid())
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonId, src => src.PersonSeparationId.Count > 0 ? src.PersonSeparationId.First() : null)
                .Map(dest => dest.DetailQuota, src => src.DetailQuota.ToNullableDecimal())
                .Map(dest => dest.TotalQuota, src => src.TotalQuota.ToNullableDecimal())
                .Map(dest => dest.DealSummaryPersonConditions, src => src.DSUPersonConditionText)
                .Ignore(src => src.ScriptoriumId)
                .Ignore(src => src.Ilm)
                 .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(DocumentEstateQuotum, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateQuotumViewModel
        /// </summary>
        /// <param name="DocumentStandardContractEstateQuotumViewModel">The DocumentStandardContractEstateQuotumViewModel<see cref="DocumentStandardContractEstateQuotumViewModel"/></param>
        /// <param name="documentEstateQuotum">The documentEstateQuotum<see cref="DocumentEstateQuotum"/></param>
        public static void MapToDocumentStandardContractEstateQuotumViewModel(ref DocumentStandardContractEstateQuotumViewModel DocumentStandardContractEstateQuotumViewModel, DocumentEstateQuotum documentEstateQuotum)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentEstateQuotum, DocumentStandardContractEstateQuotumViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.DocumentEstateQuotumId, src => src.Id.ToString())
                .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToString())
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonId, src => src.DocumentPersonId != null
                            ? new List<string> { src.DocumentPersonId.ToString() } : new List<string>())
                .Map(dest => dest.DetailQuota, src => src.DetailQuota)
                .Map(dest => dest.Family, src => src.DocumentPerson.Family)
                .Map(dest => dest.Name, src => src.DocumentPerson.Name)
                .Map(dest => dest.TotalQuota, src => src.TotalQuota);
            config.Compile();
            documentEstateQuotum.Adapt(DocumentStandardContractEstateQuotumViewModel, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractEstateQuotaDetailViewModel
        /// </summary>
        /// <param name="DocumentStandardContractEstateQuotaDetailViewModel">The DocumentStandardContractEstateQuotaDetailViewModel<see cref="DocumentStandardContractEstateQuotaDetailViewModel"/></param>
        /// <param name="documentEstateQuotaDetail">The documentEstateQuotaDetail<see cref="DocumentEstateQuotaDetail"/></param>
        public static void MapToDocumentStandardContractEstateQuotaDetailViewModel(ref DocumentStandardContractEstateQuotaDetailViewModel DocumentStandardContractEstateQuotaDetailViewModel, DocumentEstateQuotaDetail documentEstateQuotaDetail)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentEstateQuotaDetail, DocumentStandardContractEstateQuotaDetailViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.DocumentEstateQuotaDetailId, src => src.Id.ToString())
                .Map(dest => dest.DocumentEstateId, src => src.DocumentEstateId.ToString())
                .Map(dest => dest.DocumentPersonBuyerName, src => src.DocumentPersonBuyer.Name)
                .Map(dest => dest.DocumentPersonBuyerFamily, src => src.DocumentPersonBuyer.Family)
                .Map(dest => dest.DocumentPersonSellerName, src => src.DocumentPersonBuyer.Name)
                .Map(dest => dest.DocumentPersonSellerFamily, src => src.DocumentPersonBuyer.Family)
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonSellerId, src => src.DocumentPersonSellerId != null
                            ? new List<string> { src.DocumentPersonSellerId.ToString() }
                            : new List<string>())
                .Map(dest => dest.DocumentPersonBuyerId, src => src.DocumentPersonBuyerId != null
                            ? new List<string> { src.DocumentPersonBuyerId.ToString() }
                            : new List<string>())
                .Map(dest => dest.EstateOwnerShipId, src => src.DocumentEstateOwnershipDocumentId != null
                            ? new List<string> { src.DocumentEstateOwnershipDocumentId.ToString() }
                            : new List<string>())
                .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota)
                .Map(dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota)
                .Map(dest => dest.EstateInquiriesId, src => src.EstateInquiriesId)

                .Map(dest => dest.DealSummaryPersonConditions, src => src.DealSummaryPersonConditions);

            config.Compile();
            documentEstateQuotaDetail.Adapt(DocumentStandardContractEstateQuotaDetailViewModel, config);
        }



    }

}
