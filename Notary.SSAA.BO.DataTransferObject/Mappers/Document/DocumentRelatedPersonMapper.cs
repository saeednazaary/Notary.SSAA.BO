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
    /// Defines the <see cref="DocumentRelatedPersonMapper" />
    /// </summary>
    public static class DocumentRelatedPersonMapper
    {
        /// <summary>
        /// The MapToDocumentRelatedPerson
        /// </summary>
        /// <param name="documentPersonRelated">The documentPersonRelated<see cref="DocumentPersonRelated"/></param>
        /// <param name="documentRelatedPersonViewModel">The documentRelatedPersonViewModel<see cref="DocumentRelatedPersonViewModel"/></param>
        public static void MapToDocumentRelatedPerson ( ref DocumentPersonRelated documentPersonRelated, DocumentRelatedPersonViewModel documentRelatedPersonViewModel )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<DocumentRelatedPersonViewModel, DocumentPersonRelated> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.RelatedPersonId.ToGuid () )
                .Map ( dest => dest.RowNo, src => src.RowNo.ToByte () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.MainPersonId, src => src.MainPersonId.First ().ToGuid () )
                .Map ( dest => dest.AgentPersonId, src => src.RelatedAgentPersonId.First ().ToGuid () )
                .Map ( dest => dest.AgentTypeId, src => src.RelatedAgentTypeId.First () )
                .Map ( dest => dest.IsAgentDocumentAbroad, src => src.IsRelatedAgentDocumentAbroad.ToYesNo () )
                .Map ( dest => dest.AgentDocumentCountryId, src => src.RelatedAgentDocumentCountryId != null ? src.RelatedAgentDocumentCountryId.FirstOrDefault ().ToNullableInt () : null )
                .Map ( dest => dest.IsRelatedDocumentInSsar, src => src.IsRelatedDocumentInSSAR.ToYesNo () )
                .Map ( dest => dest.AgentDocumentNo, src => src.RelatedAgentDocumentNo )
                .Map ( dest => dest.AgentDocumentDate, src => src.RelatedAgentDocumentDate )
                .Map ( dest => dest.AgentDocumentIssuer, src => src.RelatedAgentDocumentIssuer )
                .Map ( dest => dest.AgentDocumentSecretCode, src => src.RelatedAgentDocumentSecretCode )
                .Map ( dest => dest.AgentDocumentScriptoriumId, src => src.RelatedAgentDocumentScriptoriumId != null ? src.RelatedAgentDocumentScriptoriumId.FirstOrDefault () : null )
                .Map ( dest => dest.IsLawyer, src => src.IsRelatedPersonLawyer.ToYesNo () )
                .Map ( dest => dest.ReliablePersonReasonId, src => src.RelatedReliablePersonReasonId != null ? src.RelatedReliablePersonReasonId.FirstOrDefault () : null )
                .Map ( dest => dest.Description, src => src.RelatedPersonDescription )
                .Ignore ( dest => dest.Ilm )
                 .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );

            config.Compile ();
            documentRelatedPersonViewModel.Adapt ( documentPersonRelated, config );
        }

        /// <summary>
        /// The MapToDocumentRelatedPeopleViewModel
        /// </summary>
        /// <param name="documentPeople">The documentPeople<see cref="List{DocumentPersonRelated}"/></param>
        /// <returns>The <see cref="List{DocumentRelatedPersonViewModel}"/></returns>
        public static List<DocumentRelatedPersonViewModel> MapToDocumentRelatedPeopleViewModel (
            List<DocumentPersonRelated> documentPeople
        )
        {
            List<DocumentRelatedPersonViewModel> documentPeopleViewModel =
                new List<DocumentRelatedPersonViewModel>();
            documentPeople.ForEach ( sp =>
            {
                documentPeopleViewModel.Add ( MapToDocumentRelatedPersonViewModel ( sp ) );
            } );
            return documentPeopleViewModel;
        }

        /// <summary>
        /// The MapToDocumentRelatedPersonViewModel
        /// </summary>
        /// <param name="documentPersonRelated">The documentPersonRelated<see cref="DocumentPersonRelated"/></param>
        /// <returns>The <see cref="DocumentRelatedPersonViewModel"/></returns>
        public static DocumentRelatedPersonViewModel MapToDocumentRelatedPersonViewModel (
       DocumentPersonRelated documentPersonRelated
   )
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<DocumentPersonRelated, DocumentRelatedPersonViewModel> ()
                .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.RelatedPersonId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToString () )
                .Map (
                    dest => dest.MainPersonId,
                            src =>
                    src.MainPersonId != null
                    ? new List<string> { src.MainPersonId.ToString () }
                    : new List<string> ()
                )
                .Map (
                    dest => dest.RelatedAgentPersonId,

                            src =>
                    src.AgentPersonId != null
                    ? new List<string> { src.AgentPersonId.ToString () }
                    : new List<string> ()
                )
                .Map (
                    dest => dest.RelatedAgentTypeId,

                            src =>
                    src.AgentTypeId != null
                    ? new List<string> { src.AgentTypeId.ToString () }
                    : new List<string> ()
                )
                .Map (
                    dest => dest.RelatedAgentDocumentCountryId,
                    src =>
                        src.AgentDocumentCountryId != null
                            ? new List<string> { src.AgentDocumentCountryId.ToString () }
                            : new List<string> ()
                )
                .Map (
                    dest => dest.IsRelatedAgentDocumentAbroad,
                    src => src.IsAgentDocumentAbroad.ToBoolean ()
                )
                .Map (
                    dest => dest.IsRelatedDocumentInSSAR,
                    src => src.IsRelatedDocumentInSsar.ToBoolean ()
                )
                .Map ( dest => dest.RelatedAgentDocumentNo, src => src.AgentDocumentNo )
                .Map ( dest => dest.RowNo, src => src.RowNo.ToString () )
                .Map ( dest => dest.RelatedAgentDocumentDate, src => src.AgentDocumentDate )
                .Map ( dest => dest.RelatedAgentDocumentIssuer, src => src.AgentDocumentIssuer )
                .Map ( dest => dest.RelatedAgentTypeTitle, src => src.AgentType.Title )
                .Map (
                    dest => dest.RelatedAgentDocumentSecretCode,
                    src => src.AgentDocumentSecretCode
                )
                .Map (
                    dest => dest.RelatedAgentDocumentScriptoriumId,
                    src =>
                        src.AgentDocumentScriptoriumId != null
                            ? new List<string> { src.AgentDocumentScriptoriumId.ToString () }
                            : new List<string> ()
                )
                .Map ( dest => dest.IsRelatedPersonLawyer, src => src.IsLawyer.ToBoolean () )
                .Map (
                    dest => dest.RelatedReliablePersonReasonId,
                    src =>
                        src.ReliablePersonReasonId != null
                            ? new List<string> { src.ReliablePersonReasonId.ToString () }
                            : new List<string> ()
                )
                .Map ( dest => dest.RelatedPersonDescription, src => src.Description );
            config.Compile ();

            var documentRelatedPersonViewModel = new DocumentRelatedPersonViewModel();
            documentRelatedPersonViewModel =
                documentPersonRelated.Adapt<DocumentRelatedPersonViewModel> ( config );

            return documentRelatedPersonViewModel;
        }
    }
}
