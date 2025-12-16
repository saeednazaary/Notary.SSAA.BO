namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Utilities.Extensions;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractTypeMapper" />
    /// </summary>
    public static class DocumentStandardContractTypeMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractTypeViewModel
        /// </summary>
        /// <param name="databaseResult">The databaseResult<see cref="Domain.Entities.DocumentType"/></param>
        /// <returns>The <see cref="DocumentStandardContractTypeViewModel"/></returns>
        public static DocumentStandardContractTypeViewModel MapToDocumentStandardContractTypeViewModel(Domain.Entities.DocumentType databaseResult)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.DocumentType, DocumentStandardContractTypeViewModel>()
                .Map(dest => dest.DocumentTypeId, src => src.Id.ToString())
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Code, src => src.Code)
                .Map(dest => dest.DocumentTextWritingAllowed, src => src.DocumentTextWritingAllowed.ToNullabbleBoolean())
                .Map(dest => dest.AssetIsRequired, src => src.AssetIsRequired.ToNullabbleBoolean())
                .Map(dest => dest.AssetTypeIsRequired, src => src.AssetTypeIsRequired.ToNullabbleBoolean())
                .Map(dest => dest.EstateInquiryIsRequired, src => src.EstateInquiryIsRequired.ToNullabbleBoolean())
                .Map(dest => dest.HasCount, src => src.HasCount.ToNullabbleBoolean())
                .Map(dest => dest.HasEstateAttachments, src => src.HasEstateAttachments.ToNullabbleBoolean())
                .Map(dest => dest.HasEstateInquiry, src => src.HasEstateInquiry.ToNullabbleBoolean())
                .Map(dest => dest.HasRelatedDocument, src => src.HasRelatedDocument.ToNullabbleBoolean())
                .Map(dest => dest.HasSubject, src => src.HasSubject.ToNullabbleBoolean())
                .Map(dest => dest.CaseTitle, src => src.CaseTitle)
                .Map(dest => dest.WealthType, src => src.WealthType.ToNullabbleBoolean())
                .Map(dest => dest.HasAsset, src => src.HasAsset.ToNullabbleBoolean())
                .Map(dest => dest.IsSupportive, src => src.IsSupportive.ToNullabbleBoolean())
                .Map(dest => dest.GeneralPersonPostTitle, src => src.GeneralPersonPostTitle)
                .Map(dest => dest.HasAssetType, src => src.HasAssetType.ToNullabbleBoolean())
                .Map(dest => dest.HasNonregisteredEstate, src => src.HasNonregisteredEstate.ToNullabbleBoolean())
                .Map(dest => dest.SubjectIsRequired, src => src.SubjectIsRequired.ToNullabbleBoolean())
                .Map(dest => dest.DocumentTypeGroupOneCode, src => src.DocumentTypeGroup1 != null ? src.DocumentTypeGroup1.Code : null)
                .Map(dest => dest.DocumentTypeGroupTwoCode, src => src.DocumentTypeGroup2 != null ? src.DocumentTypeGroup2.Code : null);
            return databaseResult.Adapt<DocumentStandardContractTypeViewModel>(config);
        }
    }
}
