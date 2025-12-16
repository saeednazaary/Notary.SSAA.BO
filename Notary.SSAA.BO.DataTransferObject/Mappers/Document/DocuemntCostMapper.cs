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
    /// Defines the <see cref="DocuemntCostMapper" />
    /// </summary>
    public static class DocuemntCostMapper
    {
        /// <summary>
        /// The MapToDocumentCostsViewModel
        /// </summary>
        /// <param name="documentCosts">The documentCosts<see cref="List{DocumentCost}"/></param>
        /// <param name="documentCostsUnchanged">The documentCostsUnchanged<see cref="List{DocumentCostUnchanged}"/></param>
        /// <returns>The <see cref="List{DocumentCostViewModel}"/></returns>
        public static List<DocumentCostViewModel> MapToDocumentCostsViewModel (
List<DocumentCost> documentCosts, List<DocumentCostUnchanged> documentCostsUnchanged
)
        {
            List<DocumentCostViewModel> documentCostsViewModel =
                new List<DocumentCostViewModel>();
            documentCosts.ForEach ( sp =>
            {
                documentCostsViewModel.Add ( MapToDocumentCostViewModel ( sp, documentCostsUnchanged.FirstOrDefault ( s => s.CostTypeId == sp.CostTypeId ) )
                );
            } );

            return documentCostsViewModel;
        }

        /// <summary>
        /// The MapToDocumentCostViewModel
        /// </summary>
        /// <param name="documentCost">The documentCost<see cref="DocumentCost"/></param>
        /// <param name="costUnchanged">The costUnchanged<see cref="DocumentCostUnchanged"/></param>
        /// <returns>The <see cref="DocumentCostViewModel"/></returns>
        public static DocumentCostViewModel MapToDocumentCostViewModel (
DocumentCost documentCost, DocumentCostUnchanged costUnchanged
)
        {
            var config = new TypeAdapterConfig();
            if ( costUnchanged != null )
            {
                config
                .NewConfig<(DocumentCost, DocumentCostUnchanged), DocumentCostViewModel> ()
                .Map ( dest => dest.IsNew, src => false )
                .Map ( dest => dest.IsDelete, src => false )
                .Map ( dest => dest.IsDirty, src => false )
                .Map ( dest => dest.IsValid, src => true )
                .Map ( dest => dest.RequestId, src => src.Item1 != null ? src.Item1.Id.ToString () : null )
                .Map ( dest => dest.DocumentId, src => src.Item1 != null ? src.Item1.DocumentId.ToString () : null )
                .Map ( dest => dest.RequestChangeReason, src => src.Item1 != null ? src.Item1.ChangeReason : null )
                .Map ( dest => dest.RequestPrice, src => src.Item1 != null ? src.Item1.Price.ToString () : null )
                .Map ( dest => dest.RequestPriceUnchanged, src => src.Item2 != null ? src.Item2.Price.ToString () : null )
                .Map ( dest => dest.RequestUnchangedId, src => src.Item2 != null ? src.Item2.Id.ToString () : null )
                .Map ( dest => dest.RequestCostTypeTitle, src => src.Item1 != null ? src.Item1.CostType != null ? src.Item1.CostType.Title : "" : null )
                .Map ( dest => dest.RequestChangeReason, src => src.Item1 != null ? src.Item1.ChangeReason : null )
                .Map ( dest => dest.RequestDescription, src => src.Item1 != null ? src.Item1.Description : null )
                .Map ( dest => dest.recordDateTime, src => src.Item1 != null ? src.Item1.RecordDate : DateTime.Now )
                 .Map (
                    dest => dest.RequestCostTypeId,
                            src =>
                   src.Item1 != null ? src.Item1.CostTypeId != null
                    ? new List<string> { src.Item1.CostTypeId.ToString () }
                    : new List<string> () : new List<string> ()
                );

                config.Compile ();
                var documentCostViewModel = (documentCost, costUnchanged).Adapt<DocumentCostViewModel>(config);
                return documentCostViewModel;
            }
            else
            {
                config
            .NewConfig<DocumentCost, DocumentCostViewModel> ()
            .Map ( dest => dest.IsNew, src => false )
            .Map ( dest => dest.IsDelete, src => false )
            .Map ( dest => dest.IsDirty, src => false )
            .Map ( dest => dest.IsValid, src => true )
            .Map ( dest => dest.RequestId, src => src.Id.ToString () )
            .Map ( dest => dest.DocumentId, src => src.DocumentId.ToString () )
            .Map ( dest => dest.RequestChangeReason, src => src.ChangeReason )
            .Map ( dest => dest.RequestPrice, src => src.Price.ToString () )
            .Map ( dest => dest.RequestCostTypeTitle, src => src.CostType != null ? src.CostType.Title : "" )
            .Map ( dest => dest.RequestDescription, src => src.Description )
            .Map ( dest => dest.recordDateTime, src => src.RecordDate )
             .Map (
                dest => dest.RequestCostTypeId,
                        src =>
              src.CostTypeId != null
                ? new List<string> { src.CostTypeId.ToString () }
                : new List<string> ()
            );

                config.Compile ();
                var documentCostViewModel = documentCost.Adapt<DocumentCostViewModel>(config);
                return documentCostViewModel;
            }
        }

        /// <summary>
        /// The MapToDocumentCost
        /// </summary>
        /// <param name="DocumentCost">The DocumentCost<see cref="DocumentCost"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentCostViewModel"/></param>
        public static void MapToDocumentCost ( ref DocumentCost DocumentCost, DocumentCostViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentCostViewModel, DocumentCost> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.RequestId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.ChangeReason, src => src.RequestChangeReason )
                .Map ( dest => dest.Price, src => src.RequestPrice.ToNullableLong () )
                .Map ( dest => dest.ChangeReason, src => src.RequestChangeReason )
                .Map ( dest => dest.Description, src => src.RequestDescription )
                .Map ( dest => dest.CostTypeId, src => src.RequestCostTypeId.Count > 0 ? src.RequestCostTypeId.First () : null )
                .Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm )
               .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( DocumentCost, config );
        }

        /// <summary>
        /// The MapToDocumentCostUnchanged
        /// </summary>
        /// <param name="DocumentCostUnchanged">The DocumentCostUnchanged<see cref="DocumentCostUnchanged"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentCostViewModel"/></param>
        public static void MapToDocumentCostUnchanged ( ref DocumentCostUnchanged DocumentCostUnchanged, DocumentCostViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentCostViewModel, DocumentCostUnchanged> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.RequestUnchangedId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.Price, src => src.RequestPriceUnchanged.ToNullableLong () )
                .Map ( dest => dest.CostTypeId, src => src.RequestCostTypeId.Count > 0 ? src.RequestCostTypeId.First () : null )
                .Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm )
               .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( DocumentCostUnchanged, config );
        }
    }
}
