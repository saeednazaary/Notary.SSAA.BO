namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractInfoConfirmMapper" />
    /// </summary>
    public static class DocumentStandardContractInfoConfirmMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractInfoConfirmViewModel
        /// </summary>
        /// <param name="databaseResult">The databaseResult<see cref="Domain.Entities.DocumentInfoConfirm"/></param>
        /// <returns>The <see cref="DocumentStandardContractInfoConfirmViewModel"/></returns>
        public static DocumentStandardContractInfoConfirmViewModel MapToDocumentStandardContractInfoConfirmViewModel ( Domain.Entities.DocumentInfoConfirm databaseResult )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<DocumentInfoConfirm, DocumentStandardContractInfoConfirmViewModel> ()
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

            return ( databaseResult ).Adapt<DocumentStandardContractInfoConfirmViewModel> ( config );
        }

        /// <summary>
        /// The MapToDocumentStandardContractInfoConfirm
        /// </summary>
        /// <param name="documentInfoConfirm">The documentInfoConfirm<see cref="DocumentInfoConfirm"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractInfoConfirmViewModel"/></param>
        public static void MapToDocumentStandardContractInfoConfirm ( ref DocumentInfoConfirm documentInfoConfirm, DocumentStandardContractInfoConfirmViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractInfoConfirmViewModel, DocumentInfoConfirm>()
                .Map(dest => dest.Id, src => src.IsNew ? Guid.NewGuid() : src.DocumentInfoConfirmId.ToGuid())
                .Map(dest => dest.DocumentId, src => src.DocumentId.ToGuid())
                .Ignore(src => src.ScriptoriumId)
                .Ignore(src => src.Ilm);
             //  .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( documentInfoConfirm, config );
        }
    }
}
