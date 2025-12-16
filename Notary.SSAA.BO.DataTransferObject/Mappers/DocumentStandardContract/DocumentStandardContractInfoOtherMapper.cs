namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractInfoOtherMapper" />
    /// </summary>
    public static class DocumentStandardContractInfoOtherMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractInfoOtherViewModel
        /// </summary>
        /// <param name="databaseResult">The databaseResult<see cref="Domain.Entities.DocumentInfoOther"/></param>
        /// <returns>The <see cref="DocumentStandardContractInfoOtherViewModel"/></returns>
        public static DocumentStandardContractInfoOtherViewModel MapToDocumentStandardContractInfoOtherViewModel ( Domain.Entities.DocumentInfoOther databaseResult )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.DocumentInfoOther, DocumentStandardContractInfoOtherViewModel> ()
                .Map ( dest => dest.DocumentInfoOtherId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentId, src => src.Document.Id.ToString () )
                .Map ( dest => dest.EndDateTime, src => src.EndDateTime )
                .Map ( dest => dest.MortageDuration, src => src.MortageDuration != null ? src.MortageDuration.ToString () : null )
                .Map ( dest => dest.PaperCount, src => src.PaperCount != null ? src.PaperCount.ToString () : null )
                .Map ( dest => dest.HagheEntefae, src => src.HagheEntefae.ToNullableInt () )
                .Map ( dest => dest.VaghfType, src => src.VaghfType )
                .Map ( dest => dest.RegisterCount, src => src.RegisterCount != null ? src.RegisterCount.ToString () : null )
                .Map ( dest => dest.DividedSectionsCount, src => src.DividedSectionsCount != null ? src.DividedSectionsCount.ToString () : null )
                .Map ( dest => dest.PeaceDuration, src => src.PeaceDuration )
                .Map ( dest => dest.RentStartDate, src => src.RentStartDate )
                .Map ( dest => dest.RentDuration, src => src.RentDuration )
                .Map ( dest => dest.AdvocacyEndDate, src => src.AdvocacyEndDate )
                .Map ( dest => dest.EndDateTime, src => src.EndDateTime )
                .Map ( dest => dest.ApplicationReason, src => src.ApplicationReason )
                .Map ( dest => dest.Title, src => src.Title )
                .Map ( dest => dest.ExecutiveTypeId, src => src.ExecutiveTypeId )
                .Map ( dest => dest.XexecutiveId, src => src.XexecutiveId )
                .Map ( dest => dest.XexecutiveOldId, src => src.XexecutiveOldId )
                .Map ( dest => dest.DocumentTypeSubjectTitle, src => src.DocumentTypeSubject != null ? src.DocumentTypeSubject.Title : null )
                .Map ( dest => dest.ExecutiveReceiverUnitId, src => src.ExecutiveReceiverUnitId )
                .Map ( dest => dest.RecordDate, src => src.RecordDate.ToString () )
                .Map ( dest => dest.HasTime, src => src.HasTime.ToNullabbleBoolean () )
                .Map ( dest => dest.HasAdvocacyToOthersPermit, src => src.HasAdvocacyToOthersPermit.ToNullabbleBoolean () )
                .Map ( dest => dest.HasMultipleAdvocacyPermit, src => src.HasMultipleAdvocacyPermit.ToNullabbleBoolean () )
                .Map ( dest => dest.HasDismissalPermit, src => src.HasDismissalPermit.ToNullabbleBoolean () )
                .Map ( dest => dest.IsKadastr, src => src.IsKadastr.ToNullabbleBoolean () )
                .Map ( dest => dest.IsRelatedToDivisionCommission, src => src.IsRelatedToDivisionCommission.ToNullabbleBoolean () )
                .Map ( dest => dest.IsDocumentBrief, src => src.IsDocumentBrief.ToNullabbleBoolean () )
                .Map ( dest => dest.IsRentWithSarghofli, src => src.IsRentWithSarghofli.ToNullabbleBoolean () )
                .Map ( dest => dest.IsPeaceForLifetime, src => src.IsPeaceForLifetime.ToNullabbleBoolean () )
                .Map ( dest => dest.DocumentTypeSubjectId, src => src.DocumentTypeSubjectId != null
                            ? new List<string> { src.DocumentTypeSubjectId.ToString () }
                            : new List<string> () )
                .Map ( dest => dest.DocumentAssetTypeId, src => src.DocumentAssetTypeId != null
                            ? new List<string> { src.DocumentAssetTypeId.ToString () }
                            : new List<string> () )
                .Map ( dest => dest.MortageTimeUnitId, src => src.MortageTimeUnitId != null
                            ? new List<string> { src.MortageTimeUnitId.ToString () }
                            : new List<string> () );
            return databaseResult.Adapt<DocumentStandardContractInfoOtherViewModel> ( config );
        }

        /// <summary>
        /// The MapToDocumentStandardContractInfoOther
        /// </summary>
        /// <param name="documentInfoOther">The documentInfoOther<see cref="DocumentInfoOther"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractInfoOtherViewModel"/></param>
        public static void MapToDocumentStandardContractInfoOther ( ref DocumentInfoOther documentInfoOther, DocumentStandardContractInfoOtherViewModel viewModel, bool? isRemoteRequest = false)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractInfoOtherViewModel, DocumentInfoOther> ()
                  .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.DocumentInfoOtherId.ToGuid())

                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.EndDateTime, src => src.EndDateTime )
                .Map ( dest => dest.MortageDuration, src => src.MortageDuration.ToNullableInt () )
                .Map ( dest => dest.PaperCount, src => src.PaperCount.ToNullableByte () )
                .Map ( dest => dest.HagheEntefae, src => src.HagheEntefae.ToString () )
                .Map ( dest => dest.VaghfType, src => src.VaghfType )
                .Map ( dest => dest.RegisterCount, src => src.RegisterCount.ToNullableInt () )
                .Map ( dest => dest.DividedSectionsCount, src => src.DividedSectionsCount.ToNullableByte () )
                .Map ( dest => dest.PeaceDuration, src => src.PeaceDuration )
                .Map ( dest => dest.RentStartDate, src => src.RentStartDate )
                .Map ( dest => dest.RentDuration, src => src.RentDuration )
                .Map ( dest => dest.AdvocacyEndDate, src => src.AdvocacyEndDate )
                .Map ( dest => dest.EndDateTime, src => src.EndDateTime )
                .Map ( dest => dest.ApplicationReason, src => src.ApplicationReason )
                .Map ( dest => dest.Title, src => src.Title )
                .Map ( dest => dest.DocumentAssetTypeId, src => src.DocumentAssetTypeId.Count > 0 ? src.DocumentAssetTypeId.First () : null )
                .Map ( dest => dest.ExecutiveTypeId, src => src.ExecutiveTypeId )
                .Map ( dest => dest.XexecutiveId, src => src.XexecutiveId )
                .Map ( dest => dest.XexecutiveOldId, src => src.XexecutiveOldId )
                .Map ( dest => dest.ExecutiveReceiverUnitId, src => src.ExecutiveReceiverUnitId )
                .Map ( dest => dest.HasTime, src => src.HasTime.ToYesNo () )
                .Map ( dest => dest.HasAdvocacyToOthersPermit, src => src.HasAdvocacyToOthersPermit.ToYesNo () )
                .Map ( dest => dest.HasMultipleAdvocacyPermit, src => src.HasMultipleAdvocacyPermit.ToYesNo () )
                .Map ( dest => dest.HasDismissalPermit, src => src.HasDismissalPermit.ToYesNo () )
                .Map ( dest => dest.IsKadastr, src => src.IsKadastr.ToYesNo () )
                .Map ( dest => dest.IsRelatedToDivisionCommission, src => src.IsRelatedToDivisionCommission.ToYesNo () )
                .Map ( dest => dest.IsDocumentBrief, src => src.IsDocumentBrief.ToYesNo () )
                .Map ( dest => dest.IsRentWithSarghofli, src => src.IsRentWithSarghofli.ToYesNo () )
                .Map ( dest => dest.IsPeaceForLifetime, src => src.IsPeaceForLifetime.ToYesNo () )
                .Map ( dest => dest.DocumentTypeSubjectId, src => src.DocumentTypeSubjectId.Count > 0 ? src.DocumentTypeSubjectId.First () : null )
                .Map ( dest => dest.MortageTimeUnitId, src => src.MortageTimeUnitId.Count > 0 ? src.MortageTimeUnitId.First () : null )
                .Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm )
               //.IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId )
               ;
            config.Compile ();
            viewModel.Adapt ( documentInfoOther, config );
        }
    }
}
