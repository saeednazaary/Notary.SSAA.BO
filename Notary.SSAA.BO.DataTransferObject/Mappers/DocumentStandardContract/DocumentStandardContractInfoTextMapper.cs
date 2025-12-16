namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractInfoTextMapper" />
    /// </summary>
    public static class DocumentStandardContractInfoTextMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractInfoTextViewModel
        /// </summary>
        /// <param name="databaseResult">The databaseResult<see cref="Domain.Entities.DocumentInfoText"/></param>
        /// <returns>The <see cref="DocumentStandardContractInfoTextViewModel"/></returns>
        public static DocumentStandardContractInfoTextViewModel MapToDocumentStandardContractInfoTextViewModel ( Domain.Entities.DocumentInfoText databaseResult )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.DocumentInfoText, DocumentStandardContractInfoTextViewModel> ()
                .Map ( dest => dest.DocumentInfoTextId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentText, src => src.DocumentText )
                .Map ( dest => dest.Description, src => src.Description )
                .Map ( dest => dest.DocumentId, src => src.Document.Id.ToString () )
                .Map ( dest => dest.DocumentDescription, src => src.DocumentDescription )
                .Map ( dest => dest.LegalText, src => src.LegalText );
            return databaseResult.Adapt<DocumentStandardContractInfoTextViewModel> ( config );
        }

        /// <summary>
        /// The MapToDocumentStandardContractInfoText
        /// </summary>
        /// <param name="documentInfoText">The documentInfoText<see cref="DocumentInfoText"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractInfoTextViewModel"/></param>
        public static void MapToDocumentStandardContractInfoText ( ref DocumentInfoText documentInfoText, DocumentStandardContractInfoTextViewModel viewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractInfoTextViewModel, DocumentInfoText>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.DocumentInfoTextId.ToGuid())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.DocumentDescription, src => src.DocumentDescription)
                .Map(dest => dest.LegalText, src => src.LegalText)
                .Map(dest => dest.DocumentText, src => src.DocumentText)
                .Ignore(src => src.ScriptoriumId)
                .Ignore(src => src.Ilm);
             //  .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( documentInfoText, config );
        }
    }
}
