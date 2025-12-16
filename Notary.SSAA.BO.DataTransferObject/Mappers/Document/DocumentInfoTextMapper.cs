namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;

    /// <summary>
    /// Defines the <see cref="DocumentInfoTextMapper" />
    /// </summary>
    public static class DocumentInfoTextMapper
    {
        /// <summary>
        /// The MapToDocumentInfoTextViewModel
        /// </summary>
        /// <param name="databaseResult">The databaseResult<see cref="Domain.Entities.DocumentInfoText"/></param>
        /// <returns>The <see cref="DocumentInfoTextViewModel"/></returns>
        public static DocumentInfoTextViewModel MapToDocumentInfoTextViewModel ( Domain.Entities.DocumentInfoText databaseResult )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.DocumentInfoText, DocumentInfoTextViewModel> ()
                .Map ( dest => dest.DocumentInfoTextId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentText, src => src.DocumentText )
                .Map ( dest => dest.Description, src => src.Description )
                .Map ( dest => dest.DocumentId, src => src.Document.Id.ToString () )
                .Map ( dest => dest.DocumentDescription, src => src.DocumentDescription )
                .Map ( dest => dest.LegalText, src => src.LegalText );
            return databaseResult.Adapt<DocumentInfoTextViewModel> ( config );
        }

        /// <summary>
        /// The MapToDocumentInfoText
        /// </summary>
        /// <param name="documentInfoText">The documentInfoText<see cref="DocumentInfoText"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentInfoTextViewModel"/></param>
        public static void MapToDocumentInfoText ( ref DocumentInfoText documentInfoText, DocumentInfoTextViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentInfoTextViewModel, DocumentInfoText> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.DocumentInfoTextId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.Description, src => src.Description )
                .Map ( dest => dest.DocumentDescription, src => src.DocumentDescription )
                .Map ( dest => dest.LegalText, src => src.LegalText )
                .Map ( dest => dest.DocumentText, src => src.DocumentText )
                .Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm )
               .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( documentInfoText, config );
        }
    }
}
