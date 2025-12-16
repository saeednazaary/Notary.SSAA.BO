namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;

    /// <summary>
    /// Defines the <see cref="DocumentInfoConfirmMapper" />
    /// </summary>
    public static class DocumentInfoConfirmMapper
    {
        /// <summary>
        /// The MapToDocumentInfoConfirmViewModel
        /// </summary>
        /// <param name="databaseResult">The databaseResult<see cref="Domain.Entities.DocumentInfoConfirm"/></param>
        /// <returns>The <see cref="DocumentInfoConfirmViewModel"/></returns>
        public static DocumentInfoConfirmViewModel MapToDocumentInfoConfirmViewModel ( Domain.Entities.DocumentInfoConfirm databaseResult )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<DocumentInfoConfirm, DocumentInfoConfirmViewModel> ()
                   .Map ( dest => dest.DocumentInfoConfirmId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToString () )
                .Map ( dest => dest.CreatorNameFamily, src => src.CreatorNameFamily )
                .Map ( dest => dest.CreateDate, src => src.CreateDate )
                .Map ( dest => dest.SardaftarNameFamily, src => src.SardaftarNameFamily)
                .Map ( dest => dest.SardaftarConfirmTime, src => src.SardaftarConfirmTime)
                .Map ( dest => dest.SardaftarConfirmDate, src => src.SardaftarConfirmDate)
                .Map ( dest => dest.DaftaryarNameFamily, src => src.DaftaryarNameFamily)
                .Map ( dest => dest.DaftaryarConfirmDate, src => src.DaftaryarConfirmDate)
                .Map ( dest => dest.DaftaryarConfirmTime, src => src.DaftaryarConfirmTime)
                .Map ( dest => dest.CreateTime, src => src.CreateTime );

            return ( databaseResult ).Adapt<DocumentInfoConfirmViewModel> ( config );
        }

        /// <summary>
        /// The MapToDocumentInfoConfirm
        /// </summary>
        /// <param name="documentInfoConfirm">The documentInfoConfirm<see cref="DocumentInfoConfirm"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentInfoConfirmViewModel"/></param>
        public static void MapToDocumentInfoConfirm ( ref DocumentInfoConfirm documentInfoConfirm, DocumentInfoConfirmViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentInfoConfirmViewModel, DocumentInfoConfirm> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.DocumentInfoConfirmId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm )
               .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( documentInfoConfirm, config );
        }
    }
}
