namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractCaseMapper" />
    /// </summary>
    public static class DocumentStandardContractCaseMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractCase
        /// </summary>
        /// <param name="DocumentCase">The DocumentCase<see cref="DocumentCase"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractCaseViewModel"/></param>
        public static void MapToDocumentStandardContractCase ( ref DocumentCase DocumentCase, DocumentStandardContractCaseViewModel viewModel, bool? isRemoteRequest = false)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractCaseViewModel, DocumentCase> ()
                .Map ( dest => dest.Id, src => isRemoteRequest == true ? src.DocumentCaseId.ToGuid() : (src.IsNew == true ? Guid.NewGuid() : src.DocumentCaseId.ToGuid()))
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid())
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
        /// The MapToDocumentStandardContractCaseViewModel
        /// </summary>
        /// <param name="documentCase">The documentCase<see cref="DocumentCase"/></param>
        /// <returns>The <see cref="DocumentStandardContractCaseViewModel"/></returns>
        public static DocumentStandardContractCaseViewModel MapToDocumentStandardContractCaseViewModel ( DocumentCase documentCase
)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentCase, DocumentStandardContractCaseViewModel> ()
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

            var documentCaseViewModel = documentCase.Adapt<DocumentStandardContractCaseViewModel>(config);
            return documentCaseViewModel;
        }

        /// <summary>
        /// The MapToDocumentStandardContractCasesViewModel
        /// </summary>
        /// <param name="documentCases">The documentCases<see cref="List{DocumentCase}"/></param>
        /// <returns>The <see cref="List{DocumentCaseViewModel}"/></returns>
        public static List<DocumentStandardContractCaseViewModel> MapToDocumentStandardContractCasesViewModel (
List<DocumentCase> documentCases
)
        {
            List<DocumentStandardContractCaseViewModel> documentCasesViewModel =
                new List<DocumentStandardContractCaseViewModel>();
            documentCases.ForEach ( sp =>
            {
                documentCasesViewModel.Add ( MapToDocumentStandardContractCaseViewModel ( sp ) );
            } );
            return documentCasesViewModel;
        }
    }

}
