namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DocumentCaseMapper" />
    /// </summary>
    public static class DocumentCaseMapper
    {
        /// <summary>
        /// The MapToDocumentCase
        /// </summary>
        /// <param name="DocumentCase">The DocumentCase<see cref="DocumentCase"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentCaseViewModel"/></param>
        public static void MapToDocumentCase ( ref DocumentCase DocumentCase, DocumentCaseViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentCaseViewModel, DocumentCase> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.DocumentCaseId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.RowNo, src => src.RowNo.ToByte () )
                .Map ( dest => dest.Description, src => src.Description )
                .Map ( dest => dest.LegacyId, src => src.LegacyId )
                .Map ( dest => dest.Title, src => src.Title )
                .Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm )
               .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( DocumentCase, config );
        }

        /// <summary>
        /// The MapToDocumentCaseViewModel
        /// </summary>
        /// <param name="documentCase">The documentCase<see cref="DocumentCase"/></param>
        /// <returns>The <see cref="DocumentCaseViewModel"/></returns>
        public static DocumentCaseViewModel MapToDocumentCaseViewModel ( DocumentCase documentCase
)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentCase, DocumentCaseViewModel> ()
                .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.DocumentCaseId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToString () )
                .Map ( dest => dest.RowNo, src => src.RowNo.ToString () )
                .Map ( dest => dest.Description, src => src.Description )
                .Map ( dest => dest.LegacyId, src => src.LegacyId )
                .Map ( dest => dest.Title, src => src.Title )
                .Map ( dest => dest.ScriptoriumId, src => src.ScriptoriumId );

            config.Compile ();

            var documentCaseViewModel = documentCase.Adapt<DocumentCaseViewModel>(config);
            return documentCaseViewModel;
        }

        /// <summary>
        /// The MapToDocumentCasesViewModel
        /// </summary>
        /// <param name="documentCases">The documentCases<see cref="List{DocumentCase}"/></param>
        /// <returns>The <see cref="List{DocumentCaseViewModel}"/></returns>
        public static List<DocumentCaseViewModel> MapToDocumentCasesViewModel (
List<DocumentCase> documentCases
)
        {
            List<DocumentCaseViewModel> documentCasesViewModel =
                new List<DocumentCaseViewModel>();
            documentCases.ForEach ( sp =>
            {
                documentCasesViewModel.Add ( MapToDocumentCaseViewModel ( sp ) );
            } );
            return documentCasesViewModel;
        }
    }

}
