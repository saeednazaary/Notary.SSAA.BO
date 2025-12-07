namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentModifySearch
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

    /// <summary>
    /// Defines the <see cref="DocumentModifySearchMapper" />
    /// </summary>
    public static class DocumentModifySearchMapper
    {


        /// <summary>
        /// The ToViewModel
        /// </summary>
        /// <param name="entity">The entity<see cref="DocumentModifyGrid"/></param>
        /// <returns>The <see cref="DocumentModifyGridViewModel"/></returns>
        public static DocumentModifyGridViewModel ToViewModel ( DocumentModifyGrid entity )
        {
            DocumentModifyGridViewModel documentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<DocumentModifyGrid, DocumentModifyGridViewModel> ();

            config.Compile ();

            _ = entity.Adapt ( documentTemplateViewModel, config );

            return documentTemplateViewModel;
        }
    }
}
