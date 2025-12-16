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
    /// Defines the <see cref="DocumentRelationMapper" />
    /// </summary>
    public static class DocumentRelationMapper
    {
        /// <summary>
        /// The MapToDocumentRelationsViewModel
        /// </summary>
        /// <param name="documentRelation">The documentRelation<see cref="List{DocumentRelation}"/></param>
        /// <returns>The <see cref="List{DocumentRelationViewModel}"/></returns>
        public static List<DocumentRelationViewModel> MapToDocumentRelationsViewModel (
List<DocumentRelation> documentRelation
)
        {
            List<DocumentRelationViewModel> documentRelationViewModel =
                new List<DocumentRelationViewModel>();
            documentRelation.ForEach ( sp =>
            {
                documentRelationViewModel.Add ( MapToDocumentRelationViewModel ( sp ) );
            } );
            return documentRelationViewModel;
        }

        /// <summary>
        /// The MapToDocumentRelationViewModel
        /// </summary>
        /// <param name="documentRelation">The documentRelation<see cref="DocumentRelation"/></param>
        /// <returns>The <see cref="DocumentRelationViewModel"/></returns>
        public static DocumentRelationViewModel MapToDocumentRelationViewModel (
DocumentRelation documentRelation
)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentRelation, DocumentRelationViewModel> ()
                .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.RelatedtId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToString () )

                .Map ( dest => dest.RequestNo, src => src.RelatedDocumentNo )
                .Map ( dest => dest.RequestDate, src => src.RelatedDocumentDate )
                .Map ( dest => dest.RequestRecordDate, src => src.RecordDate )
                .Map ( dest => dest.RequestSecretCode, src => src.RelatedDocumentSecretCode )
                .Map ( dest => dest.RelatedDocumentScriptorium, src => src.RelatedDocumentScriptorium )
                  .Map (
                    dest => dest.IsRequestAbroad,
                    src => src.IsRelatedDocAbroad.ToNullabbleBoolean ()
                )
                    .Map (
                    dest => dest.IsRequestInSsar,
                    src => src.RelatedDocumentIsInSsar.ToNullabbleBoolean ()
                )

                 .Map (
                    dest => dest.RelatedDocumentTypeId,
                            src =>
                    src.RelatedDocumentTypeId != null
                    ? new List<string> { src.RelatedDocumentTypeId.ToString () }
                    : new List<string> ()
                )
                  .Map (
                    dest => dest.ScriptoriumId,
                            src =>
                    src.ScriptoriumId != null
                    ? new List<string> { src.ScriptoriumId.ToString () }
                    : new List<string> ()
                )
                   .Map (
                    dest => dest.RelatedDocAbroadCountryId,
                            src =>
                    src.RelatedDocAbroadCountryId != null
                    ? new List<string> { src.RelatedDocAbroadCountryId.ToString () }
                    : new List<string> ()
                )
                  .Map ( dest => dest.ScriptoriumId, src => src.ScriptoriumId );

            config.Compile ();

            var documentRelationViewModel = new DocumentRelationViewModel();
            documentRelationViewModel =
                documentRelation.Adapt<DocumentRelationViewModel> ( config );

            return documentRelationViewModel;
        }

        /// <summary>
        /// The MapToDocumentRelation
        /// </summary>
        /// <param name="DocumentRelation">The DocumentRelation<see cref="DocumentRelation"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentRelationViewModel"/></param>
        public static void MapToDocumentRelation ( ref DocumentRelation DocumentRelation, DocumentRelationViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentRelationViewModel, DocumentRelation> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.RelatedtId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.RelatedDocumentNo, src => src.RequestNo )
                .Map ( dest => dest.RelatedDocumentDate, src => src.RequestDate )
                .Map ( dest => dest.RelatedDocumentSecretCode, src => src.RequestSecretCode )
                .Map ( dest => dest.IsRelatedDocAbroad, src => src.IsRequestAbroad.ToYesNo () )
                .Map ( dest => dest.RelatedDocumentIsInSsar, src => src.IsRequestInSsar.ToYesNo () )
                .Map ( dest => dest.RelatedDocumentTypeId, src => src.RelatedDocumentTypeId.Count > 0 ? src.RelatedDocumentTypeId.First () : null )
                .Map ( dest => dest.RelatedDocumentScriptorium, src => src.RelatedDocumentScriptorium )
                .Map ( dest => dest.RelatedDocAbroadCountryId, src => src.RelatedDocAbroadCountryId.Count > 0 ? src.RelatedDocAbroadCountryId.First () : null )
                .Ignore ( src => src.ScriptoriumId )
                .Map ( dest => dest.RelatedScriptoriumId, src => src.ScriptoriumId.Count > 0 ? src.ScriptoriumId.First () : null )
                .Ignore ( src => src.Ilm )
               .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( DocumentRelation, config );
        }

        /// <summary>
        /// The MapToDocumentMainRelation
        /// </summary>
        /// <param name="document">The document<see cref="Notary.SSAA.BO.Domain.Entities.Document"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentRelationViewModel"/></param>
        public static void MapToDocumentMainRelation ( ref Notary.SSAA.BO.Domain.Entities.Document document, DocumentRelationViewModel viewModel )
        {

            document.RelatedDocumentNo = viewModel.RequestNo;
            document.RelatedDocumentDate = viewModel.RequestDate;
            document.RelatedDocumentSecretCode = viewModel.RequestSecretCode;
            document.IsRelatedDocAbroad = viewModel.IsRequestAbroad.ToYesNo ();
            document.RelatedDocumentIsInSsar = viewModel.IsRequestInSsar.ToYesNo ();
            document.RelatedDocumentTypeId = viewModel.RelatedDocumentTypeId.Count > 0 ? viewModel.RelatedDocumentTypeId.First () : null;
            document.RelatedDocumentScriptorium = viewModel.RelatedDocumentScriptorium;
            document.RelatedDocAbroadCountryId = viewModel.RelatedDocAbroadCountryId.Count > 0 ? int.Parse ( viewModel.RelatedDocAbroadCountryId.First () ) : null;
            document.RelatedScriptoriumId = viewModel.ScriptoriumId.Count > 0 ? viewModel.ScriptoriumId.First () : null;
        }
    }

}
