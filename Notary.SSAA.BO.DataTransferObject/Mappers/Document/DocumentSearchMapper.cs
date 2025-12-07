namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

    /// <summary>
    /// Defines the <see cref="DocumentSearchMapper" />
    /// </summary>
    public static class DocumentSearchMapper
    {
        /// <summary>
        /// The DocumentCaseGridToViewModel
        /// </summary>
        /// <param name="entity">The entity<see cref="DocumentCaseGrid"/></param>
        /// <returns>The <see cref="DocumentCaseGridViewModel"/></returns>
        public static DocumentCaseGridViewModel DocumentCaseGridToViewModel ( DocumentCaseGrid entity )
        {
            DocumentCaseGridViewModel documentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<DocumentCaseGrid, DocumentCaseGridViewModel> ();

            config.Compile ();

            _ = entity.Adapt ( documentTemplateViewModel, config );

            return documentTemplateViewModel;
        }

        /// <summary>
        /// The ToViewModel
        /// </summary>
        /// <param name="entity">The entity<see cref="DocumentGrid"/></param>
        /// <returns>The <see cref="DocumentGridViewModel"/></returns>
        public static DocumentGridViewModel ToViewModel ( DocumentGrid entity )
        {
            DocumentGridViewModel documentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<DocumentGrid, DocumentGridViewModel> ();

            config.Compile ();

            _ = entity.Adapt ( documentTemplateViewModel, config );

            return documentTemplateViewModel;
        }
    }
}
