namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="DocumentVehicleMapper" />
    /// </summary>
    public static class DocumentVehicleMapper
    {
        /// <summary>
        /// The MapToDocumentVehicle
        /// </summary>
        /// <param name="DocumentVehicle">The DocumentVehicle<see cref="DocumentVehicle"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentVehicleViewModel"/></param>
        public static void MapToDocumentVehicle ( ref DocumentVehicle DocumentVehicle, DocumentVehicleViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentVehicleViewModel, DocumentVehicle> ()

                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.VehicleId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.RowNo, src => src.RowNo.ToByte () )
                .Map ( dest => dest.IsInTaxList, src => src.IsInTaxList.ToYesNo () )
                .Map ( dest => dest.IsVehicleNumbered, src => src.IsVehicleNumbered.ToYesNo () )
                .Map ( dest => dest.DocumentVehicleTipId, src => src.DocumentVehicleTipId.Count > 0 ? src.DocumentVehicleTipId.First () : null )
                .Map ( dest => dest.DocumentVehicleTypeId, src => src.DocumentVehicleTypeId.Count > 0 ? src.DocumentVehicleTypeId.First () : null )
                .Map ( dest => dest.DocumentVehicleSystemId, src => src.DocumentVehicleSystemId.Count > 0 ? src.DocumentVehicleSystemId.First () : null )
                .Map ( dest => dest.MadeInIran, src => src.MadeInIran.ToYesNo () )
                .Map ( dest => dest.MadeInYear, src => src.MadeInYear )
                .Map ( dest => dest.Type, src => src.Type )
                .Map ( dest => dest.System, src => src.System )
                .Map ( dest => dest.Tip, src => src.Tip )
                .Map ( dest => dest.Model, src => src.Model )
                .Map ( dest => dest.EngineNo, src => src.EngineNo )
                .Map ( dest => dest.ChassisNo, src => src.ChassisNo )
                .Map ( dest => dest.EngineCapacity, src => src.EngineCapacity )
                .Map ( dest => dest.Color, src => src.Color )
                .Map ( dest => dest.CylinderCount, src => src.CylinderCount.ToNullableByte () )
                .Map ( dest => dest.CardNo, src => src.CardNo )
                .Map ( dest => dest.QuotaText, src => src.QuotaText )
                .Map ( dest => dest.DutyFicheNo, src => src.DutyFicheNo )
                .Map ( dest => dest.FuelCardNo, src => src.FuelCardNo )
                .Map ( dest => dest.OtherInfo, src => src.OtherInfo )
                .Map ( dest => dest.InssuranceCo, src => src.InssuranceCo )
                .Map ( dest => dest.InssuranceNo, src => src.InssuranceNo )
                .Map ( dest => dest.OwnershipPrintedDocumentNo, src => src.OwnershipPrintedDocumentNo )
                .Map ( dest => dest.OldDocumentNo, src => src.OldDocumentNo )
                .Map ( dest => dest.OldDocumentIssuer, src => src.OldDocumentIssuer )
                .Map ( dest => dest.OldDocumentDate, src => src.OldDocumentDate )
                .Map ( dest => dest.NumberingLocation, src => src.NumberingLocation )
                .Map ( dest => dest.PlaqueNo1Seller, src => src.PlaqueNo1Seller.ToNullableInt () )
                .Map ( dest => dest.PlaqueNo2Seller, src => src.PlaqueNo2Seller.ToNullableInt () )
                .Map ( dest => dest.PlaqueSeriSeller, src => src.PlaqueSeriSeller.ToNullableInt () )
                .Map ( dest => dest.PlaqueNoAlphaSeller, src => src.PlaqueNoAlphaSeller )
                .Map ( dest => dest.PlaqueSeller, src => src.PlaqueSeller )
                .Map ( dest => dest.PlaqueNo1Buyer, src => src.PlaqueNo1Buyer.ToNullableInt () )
                .Map ( dest => dest.PlaqueNo2Buyer, src => src.PlaqueNo2Buyer.ToNullableInt () )
                .Map ( dest => dest.PlaqueSeriBuyer, src => src.PlaqueSeriBuyer.ToNullableInt () )
                .Map ( dest => dest.PlaqueNoAlphaBuyer, src => src.PlaqueNoAlphaBuyer )
                .Map ( dest => dest.PlaqueBuyer, src => src.PlaqueBuyer )
                .Map ( dest => dest.OwnershipType, src => src.OwnershipType )
                .Map ( dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota.ToNullableDecimal () )
                .Map ( dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota.ToNullableInt () )
                .Map ( dest => dest.SellDetailQuota, src => src.SellDetailQuota.ToNullableDecimal () )
                .Map ( dest => dest.SellTotalQuota, src => src.SellTotalQuota.ToNullableInt () )
                .Map ( dest => dest.Description, src => src.Description )
                .Map ( dest => dest.DocumentVehicleQuota, src => MapToDocumentVehicleQuotas ( src.DocumentVehicleQuotums ) )
                .Map ( dest => dest.DocumentVehicleQuotaDetails, src => MapToDocumentVehicleQuotaDetails ( src.DocumentVehicleQuotaDetails ) )
                .Map ( dest => dest.Price, src => src.Price.ToNullableLong () )

                .Ignore ( src => src.Ilm );
               //.IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            // Apply the configuration
            config.Compile ();
            // Perform the mapping by reference
            viewModel.Adapt ( DocumentVehicle, config );
        }

        /// <summary>
        /// The MapToDocumentVehiclesViewModel
        /// </summary>
        /// <param name="documentVehicles">The documentVehicles<see cref="List{DocumentVehicle}"/></param>
        /// <returns>The <see cref="List{DocumentVehicleViewModel}"/></returns>
        public static List<DocumentVehicleViewModel> MapToDocumentVehiclesViewModel (
List<DocumentVehicle> documentVehicles )
        {
            List<DocumentVehicleViewModel> documentVehiclesViewModel =
                new List<DocumentVehicleViewModel>();
            documentVehicles.ForEach ( sp =>
            {
                documentVehiclesViewModel.Add ( MapToDocumentVehicleViewModel ( sp ) );
            } );
            return documentVehiclesViewModel;
        }

        /// <summary>
        /// The MapToDocumentVehicleViewModel
        /// </summary>
        /// <param name="documentVehicle">The documentVehicle<see cref="DocumentVehicle"/></param>
        /// <returns>The <see cref="DocumentVehicleViewModel"/></returns>
        public static DocumentVehicleViewModel MapToDocumentVehicleViewModel (
DocumentVehicle documentVehicle
)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentVehicle, DocumentVehicleViewModel> ()
                  .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.VehicleId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToString () )
                .Map ( dest => dest.RowNo, src => src.RowNo.ToString () )
                .Map ( dest => dest.DocumentVehicleTipId, src => src.DocumentVehicleTipId != null
                            ? new List<string> { src.DocumentVehicleTipId.ToString () }
                            : new List<string> () )
                 .Map ( dest => dest.DocumentVehicleTypeId, src => src.DocumentVehicleTypeId != null
                            ? new List<string> { src.DocumentVehicleTypeId.ToString () }
                            : new List<string> () )
                            .Map ( dest => dest.DocumentVehicleSystemId, src => src.DocumentVehicleSystemId != null
                            ? new List<string> { src.DocumentVehicleSystemId.ToString () }
                            : new List<string> () )
                .Map ( dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota != null ? src.OwnershipDetailQuota.ToString () : null )
                 .Map ( dest => dest.MadeInIran, src => src.MadeInIran.ToBoolean () )
                .Map ( dest => dest.MadeInYear, src => src.MadeInYear != null ? src.MadeInYear.ToString () : null )
                .Map ( dest => dest.Type, src => src.Type )
                .Map ( dest => dest.System, src => src.System )
                .Map ( dest => dest.Tip, src => src.Tip )
                .Map ( dest => dest.Model, src => src.Model )
                .Map ( dest => dest.EngineNo, src => src.EngineNo )
                .Map ( dest => dest.DocumentVehicleTipTitle, src => src.IsInTaxList == "1" ? ( src.DocumentVehicleTip != null ? src.DocumentVehicleTip.Title : string.Empty ) : string.Empty )
                .Map ( dest => dest.DocumentVehicleTypeTitle, src => src.IsInTaxList == "1" ? ( src.DocumentVehicleType != null ? src.DocumentVehicleType.Title : string.Empty ) : string.Empty )
                .Map ( dest => dest.DocumentVehicleSystemTitle, src => src.IsInTaxList == "1" ? ( src.DocumentVehicleSystem != null ? src.DocumentVehicleSystem.Title : string.Empty ) : string.Empty )
                .Map ( dest => dest.ChassisNo, src => src.ChassisNo )
                .Map ( dest => dest.EngineCapacity, src => src.EngineCapacity )
                .Map ( dest => dest.Color, src => src.Color )
                .Map ( dest => dest.CylinderCount, src => src.CylinderCount != null ? src.CylinderCount.ToString () : null )
                .Map ( dest => dest.CardNo, src => src.CardNo )
                .Map ( dest => dest.QuotaText, src => src.QuotaText )
                .Map ( dest => dest.DutyFicheNo, src => src.DutyFicheNo )
                .Map ( dest => dest.FuelCardNo, src => src.FuelCardNo )
                .Map ( dest => dest.OtherInfo, src => src.OtherInfo )
                .Map ( dest => dest.InssuranceCo, src => src.InssuranceCo )
                .Map ( dest => dest.IsInTaxList, src => src.IsInTaxList.ToBoolean () )
                .Map ( dest => dest.InssuranceNo, src => src.InssuranceNo )
                .Map ( dest => dest.OwnershipPrintedDocumentNo, src => src.OwnershipPrintedDocumentNo )
                .Map ( dest => dest.OldDocumentNo, src => src.OldDocumentNo )
                .Map ( dest => dest.OldDocumentIssuer, src => src.OldDocumentIssuer )
                .Map ( dest => dest.OldDocumentDate, src => src.OldDocumentDate )
                .Map ( dest => dest.IsVehicleNumbered, src => src.IsVehicleNumbered.ToBoolean () )
                .Map ( dest => dest.NumberingLocation, src => src.NumberingLocation )
                .Map ( dest => dest.PlaqueNo1Seller, src => src.PlaqueNo1Seller != null ? src.PlaqueNo1Seller.ToString () : null )
                .Map ( dest => dest.PlaqueNo2Seller, src => src.PlaqueNo2Seller != null ? src.PlaqueNo2Seller.ToString () : null )
                .Map ( dest => dest.PlaqueSeriSeller, src => src.PlaqueSeriSeller != null ? src.PlaqueSeriSeller.ToString () : null )
                .Map ( dest => dest.PlaqueNoAlphaSeller, src => src.PlaqueNoAlphaSeller )
                .Map ( dest => dest.PlaqueSeller, src => src.PlaqueSeller )
                .Map ( dest => dest.PlaqueNo1Buyer, src => src.PlaqueNo1Buyer != null ? src.PlaqueNo1Buyer.ToString () : null )
                .Map ( dest => dest.PlaqueNo2Buyer, src => src.PlaqueNo2Buyer != null ? src.PlaqueNo2Buyer.ToString () : null )
                .Map ( dest => dest.PlaqueSeriBuyer, src => src.PlaqueSeriBuyer != null ? src.PlaqueSeriBuyer.ToString () : null )
                .Map ( dest => dest.PlaqueNoAlphaBuyer, src => src.PlaqueNoAlphaBuyer )
                .Map ( dest => dest.PlaqueBuyer, src => src.PlaqueBuyer )
                .Map ( dest => dest.OwnershipType, src => src.OwnershipType )
                .Map ( dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota != null ? src.OwnershipDetailQuota.ToString () : null )
                .Map ( dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota != null ? src.OwnershipTotalQuota.ToString () : null )
                .Map ( dest => dest.SellDetailQuota, src => src.SellDetailQuota != null ? src.SellDetailQuota.ToString () : null )
                .Map ( dest => dest.SellTotalQuota, src => src.SellTotalQuota != null ? src.SellTotalQuota.ToString () : null )
                .Map ( dest => dest.QuotaText, src => src.QuotaText )
                .Map ( dest => dest.Description, src => src.Description )
                .Map ( dest => dest.DocumentVehicleQuotums, src => MapToDocumentVehicleQuotaViewModels ( src.DocumentVehicleQuota ) )
                .Map ( dest => dest.DocumentVehicleQuotaDetails, src => MapToDocumentVehicleQuotaDetailViewModels ( src.DocumentVehicleQuotaDetails ) )
                .Map ( dest => dest.Description, src => src.Description )

                .Map ( dest => dest.Price, src => src.Price != null ? src.Price.ToString () : null );

            config.Compile ();
            var documentVehicleViewModel = documentVehicle.Adapt<DocumentVehicleViewModel>(config);
            return documentVehicleViewModel;
        }

        /// <summary>
        /// The MapToDocumentVehicleQuotas
        /// </summary>
        /// <param name="documentVehicleQuotaDetailViewModels">The documentVehicleQuotaDetailViewModels<see cref="IList{DocumentVehicleQuotumViewModel}"/></param>
        /// <returns>The <see cref="List{DocumentVehicleQuotum}"/></returns>
        public static List<DocumentVehicleQuotum> MapToDocumentVehicleQuotas ( IList<DocumentVehicleQuotumViewModel> documentVehicleQuotaDetailViewModels )
        {
            List < DocumentVehicleQuotum > documentVehicleQuotums=new List<DocumentVehicleQuotum>();
            if ( documentVehicleQuotaDetailViewModels.Count > 0 )
            {
                for ( int i = 0; i < documentVehicleQuotaDetailViewModels.Count; i++ )
                {

                    DocumentVehicleQuotum documentVehicleQuotum = new();
                    MapToDocumentVehicleQuotum ( ref documentVehicleQuotum, documentVehicleQuotaDetailViewModels [ i ] );
                    documentVehicleQuotum.Ilm = DocumentConstants.CreateIlm;
                    documentVehicleQuotums.Add ( documentVehicleQuotum );

                }
            }

            return documentVehicleQuotums;
        }

        /// <summary>
        /// The MapToDocumentVehicleQuotaViewModels
        /// </summary>
        /// <param name="documentVehicleQuotaDetails">The documentVehicleQuotaDetails<see cref="ICollection{DocumentVehicleQuotum}"/></param>
        /// <returns>The <see cref="List{DocumentVehicleQuotumViewModel}"/></returns>
        public static List<DocumentVehicleQuotumViewModel> MapToDocumentVehicleQuotaViewModels ( ICollection<DocumentVehicleQuotum> documentVehicleQuotaDetails )
        {
            List < DocumentVehicleQuotumViewModel >documentVehicleQuotumViewModels=new List<DocumentVehicleQuotumViewModel>();
            if ( documentVehicleQuotaDetails.Count > 0 )
            {
                for ( int i = 0; i < documentVehicleQuotaDetails.Count; i++ )
                {

                    DocumentVehicleQuotumViewModel documentVehicleQuotumViewModel = new();
                    MapToDocumentVehicleQuotumViewModel ( ref documentVehicleQuotumViewModel, documentVehicleQuotaDetails.ElementAt ( i ) );
                    documentVehicleQuotumViewModels.Add ( documentVehicleQuotumViewModel );

                }
            }

            return documentVehicleQuotumViewModels;
        }

        /// <summary>
        /// The MapToDocumentVehicleQuotaDetails
        /// </summary>
        /// <param name="documentVehicleQuotaDetailViewModels">The documentVehicleQuotaDetailViewModels<see cref="IList{DocumentVehicleQuotaDetailViewModel}"/></param>
        /// <returns>The <see cref="List{DocumentVehicleQuotaDetail}"/></returns>
        public static List<DocumentVehicleQuotaDetail> MapToDocumentVehicleQuotaDetails ( IList<DocumentVehicleQuotaDetailViewModel> documentVehicleQuotaDetailViewModels )
        {
            List < DocumentVehicleQuotaDetail > documentVehicleQuotaDetails=new List<DocumentVehicleQuotaDetail>();
            if ( documentVehicleQuotaDetailViewModels.Count > 0 )
            {
                for ( int i = 0; i < documentVehicleQuotaDetailViewModels.Count; i++ )
                {

                    DocumentVehicleQuotaDetail documentVehicleQuotaDetail = new();
                    MapToDocumentVehicleQuotaDetail ( ref documentVehicleQuotaDetail, documentVehicleQuotaDetailViewModels [ i ] );
                    documentVehicleQuotaDetail.Ilm = DocumentConstants.CreateIlm;
                    documentVehicleQuotaDetails.Add ( documentVehicleQuotaDetail );

                }
            }

            return documentVehicleQuotaDetails;
        }

        /// <summary>
        /// The MapToDocumentVehicleQuotaDetailViewModels
        /// </summary>
        /// <param name="documentVehicleQuotaDetails">The documentVehicleQuotaDetails<see cref="ICollection{DocumentVehicleQuotaDetail}"/></param>
        /// <returns>The <see cref="List{DocumentVehicleQuotaDetailViewModel}"/></returns>
        public static List<DocumentVehicleQuotaDetailViewModel> MapToDocumentVehicleQuotaDetailViewModels ( ICollection<DocumentVehicleQuotaDetail> documentVehicleQuotaDetails )
        {
            List < DocumentVehicleQuotaDetailViewModel > documentVehicleQuotaDetailViewModels=new List<DocumentVehicleQuotaDetailViewModel>();
            if ( documentVehicleQuotaDetails.Count > 0 )
            {
                for ( int i = 0; i < documentVehicleQuotaDetails.Count; i++ )
                {

                    DocumentVehicleQuotaDetailViewModel documentVehicleQuotaDetailViewModel = new();
                    MapToDocumentVehicleQuotaDetailViewModel ( ref documentVehicleQuotaDetailViewModel, documentVehicleQuotaDetails.ElementAt ( i ) );
                    documentVehicleQuotaDetailViewModels.Add ( documentVehicleQuotaDetailViewModel );

                }
            }

            return documentVehicleQuotaDetailViewModels;
        }

        /// <summary>
        /// The MapToDocumentVehicleQuotaDetail
        /// </summary>
        /// <param name="DocumentVehicleQuotaDetail">The DocumentVehicleQuotaDetail<see cref="DocumentVehicleQuotaDetail"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentVehicleQuotaDetailViewModel"/></param>
        public static void MapToDocumentVehicleQuotaDetail ( ref DocumentVehicleQuotaDetail DocumentVehicleQuotaDetail, DocumentVehicleQuotaDetailViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentVehicleQuotaDetailViewModel, DocumentVehicleQuotaDetail> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.DocumentVehicleQuotaDetailId.ToGuid () )
                .Map ( dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToGuid () )
                //.Map(dest => dest.RowNo, src => src.RowNo.ToByte())
                .Map ( dest => dest.QuotaText, src => src.QuotaText )
                .Map ( dest => dest.DocumentPersonBuyerId, src => src.DocumentPersonBuyerId.Count > 0 ? src.DocumentPersonBuyerId.First ().ToNullableGuid () : null )
                .Map ( dest => dest.DocumentPersonSellerId, src => src.DocumentPersonSellerId.Count > 0 ? src.DocumentPersonSellerId.First ().ToNullableGuid () : null )
                .Map ( dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToGuid () )
                .Map ( dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota.ToNullableDecimal () )
                .Map ( dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota.ToNullableDecimal () )
                .Map ( dest => dest.SellDetailQuota, src => src.SellDetailQuota.ToNullableDecimal () )
                .Map ( dest => dest.SellTotalQuota, src => src.SellTotalQuota.ToNullableDecimal () )
                .Ignore ( src => src.Ilm )
                .Ignore ( src => src.ScriptoriumId )

               .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentVehicleId )
                .IgnoreNonMapped ( true );
            config.Compile ();
            viewModel.Adapt ( DocumentVehicleQuotaDetail, config );
        }

        /// <summary>
        /// The MapToDocumentVehicleQuotum
        /// </summary>
        /// <param name="DocumentVehicleQuotum">The DocumentVehicleQuotum<see cref="DocumentVehicleQuotum"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentVehicleQuotumViewModel"/></param>
        public static void MapToDocumentVehicleQuotum ( ref DocumentVehicleQuotum DocumentVehicleQuotum, DocumentVehicleQuotumViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentVehicleQuotumViewModel, DocumentVehicleQuotum> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.DocumentVehicleQuotumId.ToGuid () )
                .Map ( dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToGuid () )
                .Map ( dest => dest.QuotaText, src => src.QuotaText )
                .Map ( dest => dest.DocumentPersonId, src => src.DocumentPersonId.Count > 0 ? src.DocumentPersonId.First () : null )
                .Map ( dest => dest.DetailQuota, src => src.DetailQuota.ToNullableDecimal () )
                .Map ( dest => dest.TotalQuota, src => src.TotalQuota.ToNullableDecimal () )
                .Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm )
                 .IgnoreNonMapped ( true );
            config.Compile ();
            viewModel.Adapt ( DocumentVehicleQuotum, config );
        }

        /// <summary>
        /// The MapToDocumentVehicleQuotumViewModel
        /// </summary>
        /// <param name="DocumentVehicleQuotumViewModel">The DocumentVehicleQuotumViewModel<see cref="DocumentVehicleQuotumViewModel"/></param>
        /// <param name="documentVehicleQuotum">The documentVehicleQuotum<see cref="DocumentVehicleQuotum"/></param>
        public static void MapToDocumentVehicleQuotumViewModel ( ref DocumentVehicleQuotumViewModel DocumentVehicleQuotumViewModel, DocumentVehicleQuotum documentVehicleQuotum )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentVehicleQuotum, DocumentVehicleQuotumViewModel> ()
                .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.DocumentVehicleQuotumId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToString () )
                .Map ( dest => dest.QuotaText, src => src.QuotaText )
                .Map ( dest => dest.DocumentPersonId, src => src.DocumentPersonId != null
                            ? new List<string> { src.DocumentPersonId.ToString () } : new List<string> () )
                .Map ( dest => dest.DetailQuota, src => src.DetailQuota )
                .Map ( dest => dest.TotalQuota, src => src.TotalQuota );
            config.Compile ();
            documentVehicleQuotum.Adapt ( DocumentVehicleQuotumViewModel, config );
        }

        /// <summary>
        /// The MapToDocumentVehicleQuotaDetailViewModel
        /// </summary>
        /// <param name="DocumentVehicleQuotaDetailViewModel">The DocumentVehicleQuotaDetailViewModel<see cref="DocumentVehicleQuotaDetailViewModel"/></param>
        /// <param name="documentVehicleQuotaDetail">The documentVehicleQuotaDetail<see cref="DocumentVehicleQuotaDetail"/></param>
        public static void MapToDocumentVehicleQuotaDetailViewModel ( ref DocumentVehicleQuotaDetailViewModel DocumentVehicleQuotaDetailViewModel, DocumentVehicleQuotaDetail documentVehicleQuotaDetail )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentVehicleQuotaDetail, DocumentVehicleQuotaDetailViewModel> ()
                .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.DocumentVehicleQuotaDetailId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToString () )
                .Map ( dest => dest.QuotaText, src => src.QuotaText )
                .Map ( dest => dest.DocumentPersonSellerId, src => src.DocumentPersonSellerId != null
                            ? new List<string> { src.DocumentPersonSellerId.ToString () }
                            : new List<string> () )
                .Map ( dest => dest.DocumentPersonBuyerId, src => src.DocumentPersonBuyerId != null
                            ? new List<string> { src.DocumentPersonBuyerId.ToString () }
                            : new List<string> () )
                .Map ( dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota )
                .Map ( dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota );

            config.Compile ();
            documentVehicleQuotaDetail.Adapt ( DocumentVehicleQuotaDetailViewModel, config );
        }
    }
}
