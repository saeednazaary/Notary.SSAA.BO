namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="DocumentInquiriesMapper" />
    /// </summary>
    public static class DocumentInquiriesMapper
    {
        /// <summary>
        /// The MapToDocumentInquiriesViewModel
        /// </summary>
        /// <param name="documentInquires">The documentInquires<see cref="List{DocumentInquiry}"/></param>
        /// <returns>The <see cref="List{DocumentInquiryViewModel}"/></returns>
        public static List<DocumentInquiryViewModel> MapToDocumentInquiriesViewModel (
List<DocumentInquiry> documentInquires )
        {
            List<DocumentInquiryViewModel> documentInquiriesViewModel =
               new List<DocumentInquiryViewModel>();
            documentInquires.ForEach ( sp =>
            {
                documentInquiriesViewModel.Add ( MapToDocumentInquiryViewModel ( sp ) );
            } );
            return documentInquiriesViewModel;
        }

        /// <summary>
        /// The MapToDocumentInquiryViewModel
        /// </summary>
        /// <param name="documentRelation">The documentRelation<see cref="DocumentInquiry"/></param>
        /// <returns>The <see cref="DocumentInquiryViewModel"/></returns>
        public static DocumentInquiryViewModel MapToDocumentInquiryViewModel (
DocumentInquiry documentRelation
)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentInquiry, DocumentInquiryViewModel> ()
                .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.DocumentInquiryId, src => src.Id.ToString () )
                .Map ( dest => dest.EstateInquiriesId, src => src.EstateInquiriesId)
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToString () )
                .Map ( dest => dest.Conditions, src => src.Conditions )
                .Map ( dest => dest.DocumentInquiryOrganizationId, src =>
                    src.DocumentInquiryOrganizationId != null
                    ? new List<string> { src.DocumentInquiryOrganizationId.ToString () }
                    : new List<string> () )
                .Map ( dest => dest.ExpireDate, src => src.ExpireDate )
                .Map ( dest => dest.ReplyNo, src => src.ReplyNo )
                .Map ( dest => dest.ReplyText, src => src.ReplyText )
                .Map ( dest => dest.ReplyQuotaText, src => src.ReplyQuotaText )
                .Map ( dest => dest.ReplyType, src => src.ReplyType )
                .Map ( dest => dest.ReplyDate, src => src.ReplyDate )
                .Map ( dest => dest.RequestDate, src => src.RequestDate )
                .Map ( dest => dest.RequestNo, src => src.RequestNo )
                .Map ( dest => dest.ReplyType, src => src.ReplyType )
                .Map ( dest => dest.ReplyDate, src => src.ReplyDate )
                .Map ( dest => dest.ReplyTotalQuota, src => src.ReplyTotalQuota )
                .Map ( dest => dest.ReplyDetailQuota, src => src.ReplyDetailQuota)
                .Map ( dest => dest.RequestText, src => src.RequestText.toString () )
                .Map ( dest => dest.State, src => src.State )
                .Map ( dest => dest.RequestNo, src => src.RequestNo )
                .Map ( dest => dest.RequestDate, src => src.RequestDate )
                .Map ( dest => dest.DocumentInquiryOrganizationTitle, src => src.DocumentInquiryOrganization != null ? src.DocumentInquiryOrganization.Title : "" );

            config.Compile ();

            var documentInquiryViewModel = new DocumentInquiryViewModel();
            documentInquiryViewModel =
                documentRelation.Adapt<DocumentInquiryViewModel> ( config );

            return documentInquiryViewModel;
        }

        /// <summary>
        /// The MapToDocumentInquiry
        /// </summary>
        /// <param name="documentInquiry">The documentInquiry<see cref="DocumentInquiry"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentInquiryViewModel"/></param>
        public static void MapToDocumentInquiry ( ref DocumentInquiry documentInquiry, DocumentInquiryViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentInquiryViewModel, DocumentInquiry> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.DocumentInquiryId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.Conditions, src => src.Conditions )
                .Map ( dest => dest.DocumentInquiryOrganizationId, src => src.DocumentInquiryOrganizationId.Count () > 0 ? src.DocumentInquiryOrganizationId.First () : null )
                .Map ( dest => dest.ExpireDate, src => src.ExpireDate )
                .Map ( dest => dest.InquiryIssuer, src => src.InquiryIssuer )
                .Map ( dest => dest.ReplyDate, src => src.ReplyDate )
                .Map ( dest => dest.ReplyDetailQuota, src => src.ReplyDetailQuota)
                .Map ( dest => dest.ReplyNo, src => src.ReplyNo )
                .Map ( dest => dest.ReplyQuotaText, src => src.ReplyQuotaText.ToNullableDecimal() )
                .Map ( dest => dest.ReplyText, src => src.ReplyText )
                .Map ( dest => dest.ReplyTotalQuota, src => src.ReplyTotalQuota )
                .Map ( dest => dest.ReplyType, src => src.ReplyType )
                .Map ( dest => dest.RequestDate, src => src.RequestDate )
                .Map ( dest => dest.RequestNo, src => src.RequestNo )
                .Map ( dest => dest.RequestDate, src => src.RequestDate )
                .Map ( dest => dest.RequestText, src => src.RequestText.toByteArray () )
                .Map ( dest => dest.State, src => src.State )
                .Map ( dest => dest.RequestText, src => src.RequestText )
                .Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm );
               //.IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( documentInquiry, config );
        }
    }

}
