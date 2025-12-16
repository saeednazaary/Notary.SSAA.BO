namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractVehicleMapper" />
    /// </summary>
    public static class DocumentStandardContractVehicleMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractVehicle
        /// </summary>
        /// <param name="DocumentVehicle">The DocumentVehicle<see cref="DocumentVehicle"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractVehicleViewModel"/></param>
        public static void MapToDocumentStandardContractVehicle(ref DocumentVehicle DocumentVehicle, DocumentStandardContractVehicleViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractVehicleViewModel, DocumentVehicle>()

                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.VehicleId.ToGuid())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.RowNo, src => src.RowNo.ToByte())
                .Map(dest => dest.IsInTaxList, src => src.IsInTaxList.ToYesNo())
                .Map(dest => dest.IsVehicleNumbered, src => src.IsVehicleNumbered.ToYesNo())
                .Map(dest => dest.DocumentVehicleTipId, src => src.DocumentVehicleTipId.Count > 0 ? src.DocumentVehicleTipId.First() : null)
                .Map(dest => dest.DocumentVehicleTypeId, src => src.DocumentVehicleTypeId.Count > 0 ? src.DocumentVehicleTypeId.First() : null)
                .Map(dest => dest.DocumentVehicleSystemId, src => src.DocumentVehicleSystemId.Count > 0 ? src.DocumentVehicleSystemId.First() : null)
                .Map(dest => dest.MadeInIran, src => src.MadeInIran.ToYesNo())
                .Map(dest => dest.MadeInYear, src => src.MadeInYear)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.System, src => src.System)
                .Map(dest => dest.Tip, src => src.Tip)
                .Map(dest => dest.Model, src => src.Model)
                .Map(dest => dest.EngineNo, src => src.EngineNo)
                .Map(dest => dest.ChassisNo, src => src.ChassisNo)
                .Map(dest => dest.EngineCapacity, src => src.EngineCapacity)
                .Map(dest => dest.Color, src => src.Color)
                .Map(dest => dest.CylinderCount, src => src.CylinderCount.ToNullableByte())
                .Map(dest => dest.CardNo, src => src.CardNo)
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DutyFicheNo, src => src.DutyFicheNo)
                .Map(dest => dest.FuelCardNo, src => src.FuelCardNo)
                .Map(dest => dest.OtherInfo, src => src.OtherInfo)
                .Map(dest => dest.InssuranceCo, src => src.InssuranceCo)
                .Map(dest => dest.InssuranceNo, src => src.InssuranceNo)
                .Map(dest => dest.OwnershipPrintedDocumentNo, src => src.OwnershipPrintedDocumentNo)
                .Map(dest => dest.OldDocumentNo, src => src.OldDocumentNo)
                .Map(dest => dest.OldDocumentIssuer, src => src.OldDocumentIssuer)
                .Map(dest => dest.OldDocumentDate, src => src.OldDocumentDate)
                .Map(dest => dest.NumberingLocation, src => src.NumberingLocation)
                .Map(dest => dest.PlaqueNo1Seller, src => src.PlaqueNo1Seller.ToNullableInt())
                .Map(dest => dest.PlaqueNo2Seller, src => src.PlaqueNo2Seller.ToNullableInt())
                .Map(dest => dest.PlaqueSeriSeller, src => src.PlaqueSeriSeller.ToNullableInt())
                .Map(dest => dest.PlaqueNoAlphaSeller, src => src.PlaqueNoAlphaSeller)
                .Map(dest => dest.PlaqueSeller, src => src.PlaqueSeller)
                .Map(dest => dest.PlaqueNo1Buyer, src => src.PlaqueNo1Buyer.ToNullableInt())
                .Map(dest => dest.PlaqueNo2Buyer, src => src.PlaqueNo2Buyer.ToNullableInt())
                .Map(dest => dest.PlaqueSeriBuyer, src => src.PlaqueSeriBuyer.ToNullableInt())
                .Map(dest => dest.PlaqueNoAlphaBuyer, src => src.PlaqueNoAlphaBuyer)
                .Map(dest => dest.PlaqueBuyer, src => src.PlaqueBuyer)
                .Map(dest => dest.OwnershipType, src => src.OwnershipType)
                .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota.ToNullableDecimal())
                .Map(dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota.ToNullableInt())
                .Map(dest => dest.SellDetailQuota, src => src.SellDetailQuota.ToNullableDecimal())
                .Map(dest => dest.SellTotalQuota, src => src.SellTotalQuota.ToNullableInt())
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.DocumentVehicleQuota, src => MapToDocumentStandardContractVehicleQuotas(src.DocumentVehicleQuotums))
                .Map(dest => dest.DocumentVehicleQuotaDetails, src => MapToDocumentStandardContractVehicleQuotaDetails(src.DocumentVehicleQuotaDetails))
                .Map(dest => dest.Price, src => src.Price.ToNullableLong())

                .Ignore(src => src.Ilm);
            //.IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            // Apply the configuration
            config.Compile();
            // Perform the mapping by reference
            viewModel.Adapt(DocumentVehicle, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehiclesViewModel
        /// </summary>
        /// <param name="documentVehicles">The documentVehicles<see cref="List{DocumentVehicle}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractVehicleViewModel}"/></returns>
        public static List<DocumentStandardContractVehicleViewModel> MapToDocumentStandardContractVehiclesViewModel(
List<DocumentVehicle> documentVehicles)
        {
            List<DocumentStandardContractVehicleViewModel> documentVehiclesViewModel =
                new List<DocumentStandardContractVehicleViewModel>();
            documentVehicles.ForEach(sp =>
            {
                documentVehiclesViewModel.Add(MapToDocumentStandardContractVehicleViewModel(sp));
            });
            return documentVehiclesViewModel;
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleViewModel
        /// </summary>
        /// <param name="documentVehicle">The documentVehicle<see cref="DocumentVehicle"/></param>
        /// <returns>The <see cref="DocumentStandardContractVehicleViewModel"/></returns>
        public static DocumentStandardContractVehicleViewModel MapToDocumentStandardContractVehicleViewModel(
DocumentVehicle documentVehicle
)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentVehicle, DocumentStandardContractVehicleViewModel>()
                  .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.VehicleId, src => src.Id.ToString())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToString())
                .Map(dest => dest.RowNo, src => src.RowNo.ToString())
                .Map(dest => dest.DocumentVehicleTipId, src => src.DocumentVehicleTipId != null
                            ? new List<string> { src.DocumentVehicleTipId.ToString() }
                            : new List<string>())
                 .Map(dest => dest.DocumentVehicleTypeId, src => src.DocumentVehicleTypeId != null
                            ? new List<string> { src.DocumentVehicleTypeId.ToString() }
                            : new List<string>())
                            .Map(dest => dest.DocumentVehicleSystemId, src => src.DocumentVehicleSystemId != null
                            ? new List<string> { src.DocumentVehicleSystemId.ToString() }
                            : new List<string>())
                .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota != null ? src.OwnershipDetailQuota.ToString() : null)
                 .Map(dest => dest.MadeInIran, src => src.MadeInIran.ToBoolean())
                .Map(dest => dest.MadeInYear, src => src.MadeInYear != null ? src.MadeInYear.ToString() : null)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.System, src => src.System)
                .Map(dest => dest.Tip, src => src.Tip)
                .Map(dest => dest.Model, src => src.Model)
                .Map(dest => dest.EngineNo, src => src.EngineNo)
                .Map(dest => dest.DocumentVehicleTipTitle, src => src.IsInTaxList == "1" ? (src.DocumentVehicleTip != null ? src.DocumentVehicleTip.Title : string.Empty) : string.Empty)
                .Map(dest => dest.DocumentVehicleTypeTitle, src => src.IsInTaxList == "1" ? (src.DocumentVehicleType != null ? src.DocumentVehicleType.Title : string.Empty) : string.Empty)
                .Map(dest => dest.DocumentVehicleSystemTitle, src => src.IsInTaxList == "1" ? (src.DocumentVehicleSystem != null ? src.DocumentVehicleSystem.Title : string.Empty) : string.Empty)
                .Map(dest => dest.ChassisNo, src => src.ChassisNo)
                .Map(dest => dest.EngineCapacity, src => src.EngineCapacity)
                .Map(dest => dest.Color, src => src.Color)
                .Map(dest => dest.CylinderCount, src => src.CylinderCount != null ? src.CylinderCount.ToString() : null)
                .Map(dest => dest.CardNo, src => src.CardNo)
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DutyFicheNo, src => src.DutyFicheNo)
                .Map(dest => dest.FuelCardNo, src => src.FuelCardNo)
                .Map(dest => dest.OtherInfo, src => src.OtherInfo)
                .Map(dest => dest.InssuranceCo, src => src.InssuranceCo)
                .Map(dest => dest.IsInTaxList, src => src.IsInTaxList.ToBoolean())
                .Map(dest => dest.InssuranceNo, src => src.InssuranceNo)
                .Map(dest => dest.OwnershipPrintedDocumentNo, src => src.OwnershipPrintedDocumentNo)
                .Map(dest => dest.OldDocumentNo, src => src.OldDocumentNo)
                .Map(dest => dest.OldDocumentIssuer, src => src.OldDocumentIssuer)
                .Map(dest => dest.OldDocumentDate, src => src.OldDocumentDate)
                .Map(dest => dest.IsVehicleNumbered, src => src.IsVehicleNumbered.ToBoolean())
                .Map(dest => dest.NumberingLocation, src => src.NumberingLocation)
                .Map(dest => dest.PlaqueNo1Seller, src => src.PlaqueNo1Seller != null ? src.PlaqueNo1Seller.ToString() : null)
                .Map(dest => dest.PlaqueNo2Seller, src => src.PlaqueNo2Seller != null ? src.PlaqueNo2Seller.ToString() : null)
                .Map(dest => dest.PlaqueSeriSeller, src => src.PlaqueSeriSeller != null ? src.PlaqueSeriSeller.ToString() : null)
                .Map(dest => dest.PlaqueNoAlphaSeller, src => src.PlaqueNoAlphaSeller)
                .Map(dest => dest.PlaqueSeller, src => src.PlaqueSeller)
                .Map(dest => dest.PlaqueNo1Buyer, src => src.PlaqueNo1Buyer != null ? src.PlaqueNo1Buyer.ToString() : null)
                .Map(dest => dest.PlaqueNo2Buyer, src => src.PlaqueNo2Buyer != null ? src.PlaqueNo2Buyer.ToString() : null)
                .Map(dest => dest.PlaqueSeriBuyer, src => src.PlaqueSeriBuyer != null ? src.PlaqueSeriBuyer.ToString() : null)
                .Map(dest => dest.PlaqueNoAlphaBuyer, src => src.PlaqueNoAlphaBuyer)
                .Map(dest => dest.PlaqueBuyer, src => src.PlaqueBuyer)
                .Map(dest => dest.OwnershipType, src => src.OwnershipType)
                .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota != null ? src.OwnershipDetailQuota.ToString() : null)
                .Map(dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota != null ? src.OwnershipTotalQuota.ToString() : null)
                .Map(dest => dest.SellDetailQuota, src => src.SellDetailQuota != null ? src.SellDetailQuota.ToString() : null)
                .Map(dest => dest.SellTotalQuota, src => src.SellTotalQuota != null ? src.SellTotalQuota.ToString() : null)
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.DocumentVehicleQuotums, src => MapToDocumentStandardContractVehicleQuotaViewModels(src.DocumentVehicleQuota))
                .Map(dest => dest.DocumentVehicleQuotaDetails, src => MapToDocumentStandardContractVehicleQuotaDetailViewModels(src.DocumentVehicleQuotaDetails))
                .Map(dest => dest.Description, src => src.Description)

                .Map(dest => dest.Price, src => src.Price != null ? src.Price.ToString() : null);

            config.Compile();
            var documentVehicleViewModel = documentVehicle.Adapt<DocumentStandardContractVehicleViewModel>(config);
            return documentVehicleViewModel;
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleQuotas
        /// </summary>
        /// <param name="documentVehicleQuotaDetailViewModels">The documentVehicleQuotaDetailViewModels<see cref="IList{DocumentStandardContractVehicleQuotumViewModel}"/></param>
        /// <returns>The <see cref="List{DocumentVehicleQuotum}"/></returns>
        public static List<DocumentVehicleQuotum> MapToDocumentStandardContractVehicleQuotas(IList<DocumentStandardContractVehicleQuotumViewModel> documentVehicleQuotaDetailViewModels)
        {
            List<DocumentVehicleQuotum> documentVehicleQuotums = new List<DocumentVehicleQuotum>();
            if (documentVehicleQuotaDetailViewModels.Count > 0)
            {
                for (int i = 0; i < documentVehicleQuotaDetailViewModels.Count; i++)
                {

                    DocumentVehicleQuotum documentVehicleQuotum = new();
                    MapToDocumentStandardContractVehicleQuotum(ref documentVehicleQuotum, documentVehicleQuotaDetailViewModels[i]);
                    documentVehicleQuotum.Ilm = DocumentConstants.CreateIlm;
                    documentVehicleQuotums.Add(documentVehicleQuotum);

                }
            }

            return documentVehicleQuotums;
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleQuotaViewModels
        /// </summary>
        /// <param name="documentVehicleQuotaDetails">The documentVehicleQuotaDetails<see cref="ICollection{DocumentVehicleQuotum}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractVehicleQuotumViewModel}"/></returns>
        public static List<DocumentStandardContractVehicleQuotumViewModel> MapToDocumentStandardContractVehicleQuotaViewModels(ICollection<DocumentVehicleQuotum> documentVehicleQuotaDetails)
        {
            List<DocumentStandardContractVehicleQuotumViewModel> documentVehicleQuotumViewModels = new List<DocumentStandardContractVehicleQuotumViewModel>();
            if (documentVehicleQuotaDetails.Count > 0)
            {
                for (int i = 0; i < documentVehicleQuotaDetails.Count; i++)
                {

                    DocumentStandardContractVehicleQuotumViewModel documentVehicleQuotumViewModel = new();
                    MapToDocumentStandardContractVehicleQuotumViewModel(ref documentVehicleQuotumViewModel, documentVehicleQuotaDetails.ElementAt(i));
                    documentVehicleQuotumViewModels.Add(documentVehicleQuotumViewModel);

                }
            }

            return documentVehicleQuotumViewModels;
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleQuotaDetails
        /// </summary>
        /// <param name="documentVehicleQuotaDetailViewModels">The documentVehicleQuotaDetailViewModels<see cref="IList{DocumentStandardContractVehicleQuotaDetailViewModel}"/></param>
        /// <returns>The <see cref="List{DocumentVehicleQuotaDetail}"/></returns>
        public static List<DocumentVehicleQuotaDetail> MapToDocumentStandardContractVehicleQuotaDetails(IList<DocumentStandardContractVehicleQuotaDetailViewModel> documentVehicleQuotaDetailViewModels)
        {
            List<DocumentVehicleQuotaDetail> documentVehicleQuotaDetails = new List<DocumentVehicleQuotaDetail>();
            if (documentVehicleQuotaDetailViewModels.Count > 0)
            {
                for (int i = 0; i < documentVehicleQuotaDetailViewModels.Count; i++)
                {

                    DocumentVehicleQuotaDetail documentVehicleQuotaDetail = new();
                    MapToDocumentStandardContractVehicleQuotaDetail(ref documentVehicleQuotaDetail, documentVehicleQuotaDetailViewModels[i]);
                    documentVehicleQuotaDetail.Ilm = DocumentConstants.CreateIlm;
                    documentVehicleQuotaDetails.Add(documentVehicleQuotaDetail);

                }
            }

            return documentVehicleQuotaDetails;
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleQuotaDetailViewModels
        /// </summary>
        /// <param name="documentVehicleQuotaDetails">The documentVehicleQuotaDetails<see cref="ICollection{DocumentVehicleQuotaDetail}"/></param>
        /// <returns>The <see cref="List{DocumentStandardContractVehicleQuotaDetailViewModel}"/></returns>
        public static List<DocumentStandardContractVehicleQuotaDetailViewModel> MapToDocumentStandardContractVehicleQuotaDetailViewModels(ICollection<DocumentVehicleQuotaDetail> documentVehicleQuotaDetails)
        {
            List<DocumentStandardContractVehicleQuotaDetailViewModel> documentVehicleQuotaDetailViewModels = new List<DocumentStandardContractVehicleQuotaDetailViewModel>();
            if (documentVehicleQuotaDetails.Count > 0)
            {
                for (int i = 0; i < documentVehicleQuotaDetails.Count; i++)
                {

                    DocumentStandardContractVehicleQuotaDetailViewModel documentVehicleQuotaDetailViewModel = new();
                    MapToDocumentStandardContractVehicleQuotaDetailViewModel(ref documentVehicleQuotaDetailViewModel, documentVehicleQuotaDetails.ElementAt(i));
                    documentVehicleQuotaDetailViewModels.Add(documentVehicleQuotaDetailViewModel);

                }
            }

            return documentVehicleQuotaDetailViewModels;
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleQuotaDetail
        /// </summary>
        /// <param name="DocumentVehicleQuotaDetail">The DocumentVehicleQuotaDetail<see cref="DocumentVehicleQuotaDetail"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractVehicleQuotaDetailViewModel"/></param>
        public static void MapToDocumentStandardContractVehicleQuotaDetail(ref DocumentVehicleQuotaDetail DocumentVehicleQuotaDetail, DocumentStandardContractVehicleQuotaDetailViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractVehicleQuotaDetailViewModel, DocumentVehicleQuotaDetail>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.DocumentVehicleQuotaDetailId.ToGuid())
                .Map(dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToGuid())
                //.Map(dest => dest.RowNo, src => src.RowNo.ToByte())
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonBuyerId, src => src.DocumentPersonBuyerId.Count > 0 ? src.DocumentPersonBuyerId.First().ToNullableGuid() : null)
                .Map(dest => dest.DocumentPersonSellerId, src => src.DocumentPersonSellerId.Count > 0 ? src.DocumentPersonSellerId.First().ToNullableGuid() : null)
                .Map(dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToGuid())
                .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota.ToNullableDecimal())
                .Map(dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota.ToNullableDecimal())
                .Map(dest => dest.SellDetailQuota, src => src.SellDetailQuota.ToNullableDecimal())
                .Map(dest => dest.SellTotalQuota, src => src.SellTotalQuota.ToNullableDecimal())
                .Ignore(src => src.Ilm)
                .Ignore(src => src.ScriptoriumId)

               .IgnoreIf((src, dest) => src.IsNew, dest => dest.DocumentVehicleId)
                .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(DocumentVehicleQuotaDetail, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleQuotum
        /// </summary>
        /// <param name="DocumentVehicleQuotum">The DocumentVehicleQuotum<see cref="DocumentVehicleQuotum"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractVehicleQuotumViewModel"/></param>
        public static void MapToDocumentStandardContractVehicleQuotum(ref DocumentVehicleQuotum DocumentVehicleQuotum, DocumentStandardContractVehicleQuotumViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractVehicleQuotumViewModel, DocumentVehicleQuotum>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.DocumentVehicleQuotumId.ToGuid())
                .Map(dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToGuid())
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonId, src => src.DocumentPersonId.Count > 0 ? src.DocumentPersonId.First() : null)
                .Map(dest => dest.DetailQuota, src => src.DetailQuota.ToNullableDecimal())
                .Map(dest => dest.TotalQuota, src => src.TotalQuota.ToNullableDecimal())
                .Ignore(src => src.ScriptoriumId)
                .Ignore(src => src.Ilm)
                 .IgnoreNonMapped(true);
            config.Compile();
            viewModel.Adapt(DocumentVehicleQuotum, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleQuotumViewModel
        /// </summary>
        /// <param name="DocumentStandardContractVehicleQuotumViewModel">The DocumentStandardContractVehicleQuotumViewModel<see cref="DocumentStandardContractVehicleQuotumViewModel"/></param>
        /// <param name="documentVehicleQuotum">The documentVehicleQuotum<see cref="DocumentVehicleQuotum"/></param>
        public static void MapToDocumentStandardContractVehicleQuotumViewModel(ref DocumentStandardContractVehicleQuotumViewModel DocumentStandardContractVehicleQuotumViewModel, DocumentVehicleQuotum documentVehicleQuotum)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentVehicleQuotum, DocumentStandardContractVehicleQuotumViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.DocumentVehicleQuotumId, src => src.Id.ToString())
                .Map(dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToString())
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonId, src => src.DocumentPersonId != null
                            ? new List<string> { src.DocumentPersonId.ToString() } : new List<string>())
                .Map(dest => dest.DetailQuota, src => src.DetailQuota)
                .Map(dest => dest.TotalQuota, src => src.TotalQuota);
            config.Compile();
            documentVehicleQuotum.Adapt(DocumentStandardContractVehicleQuotumViewModel, config);
        }

        /// <summary>
        /// The MapToDocumentStandardContractVehicleQuotaDetailViewModel
        /// </summary>
        /// <param name="DocumentStandardContractVehicleQuotaDetailViewModel">The DocumentStandardContractVehicleQuotaDetailViewModel<see cref="DocumentStandardContractVehicleQuotaDetailViewModel"/></param>
        /// <param name="documentVehicleQuotaDetail">The documentVehicleQuotaDetail<see cref="DocumentVehicleQuotaDetail"/></param>
        public static void MapToDocumentStandardContractVehicleQuotaDetailViewModel(ref DocumentStandardContractVehicleQuotaDetailViewModel DocumentStandardContractVehicleQuotaDetailViewModel, DocumentVehicleQuotaDetail documentVehicleQuotaDetail)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentVehicleQuotaDetail, DocumentStandardContractVehicleQuotaDetailViewModel>()
                .Map(dest => dest.IsNew, src => false)
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.IsValid, src => true)
                .Map(dest => dest.DocumentVehicleQuotaDetailId, src => src.Id.ToString())
                .Map(dest => dest.DocumentVehicleId, src => src.DocumentVehicleId.ToString())
                .Map(dest => dest.QuotaText, src => src.QuotaText)
                .Map(dest => dest.DocumentPersonSellerId, src => src.DocumentPersonSellerId != null
                            ? new List<string> { src.DocumentPersonSellerId.ToString() }
                            : new List<string>())
                .Map(dest => dest.DocumentPersonBuyerId, src => src.DocumentPersonBuyerId != null
                            ? new List<string> { src.DocumentPersonBuyerId.ToString() }
                            : new List<string>())
                .Map(dest => dest.OwnershipDetailQuota, src => src.OwnershipDetailQuota)
                .Map(dest => dest.OwnershipTotalQuota, src => src.OwnershipTotalQuota);

            config.Compile();
            documentVehicleQuotaDetail.Adapt(DocumentStandardContractVehicleQuotaDetailViewModel, config);
        }
    }
}
