namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractSearchMapper" />
    /// </summary>
    public static class DocumentStandardContractSearchMapper
    {
        /// <summary>
        /// The DocumentCaseGridToViewModel
        /// </summary>
        /// <param name="entity">The entity<see cref="DocumentCaseGrid"/></param>
        /// <returns>The <see cref="DocumentStandardContractCaseGridViewModel"/></returns>
        public static DocumentStandardContractCaseGridViewModel DocumentCaseGridToViewModel ( DocumentCaseGrid entity )
        {
            DocumentStandardContractCaseGridViewModel documentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<DocumentCaseGrid, DocumentStandardContractCaseGridViewModel> ();

            config.Compile ();

            _ = entity.Adapt ( documentTemplateViewModel, config );

            return documentTemplateViewModel;
        }

        /// <summary>
        /// The ToViewModel
        /// </summary>
        /// <param name="entity">The entity<see cref="DocumentGrid"/></param>
        /// <returns>The <see cref="DocumentStandardContractGridViewModel"/></returns>
        public static DocumentStandardContractGridViewModel ToViewModel ( DocumentGrid entity )
        {
            DocumentStandardContractGridViewModel documentTemplateViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<DocumentGrid, DocumentStandardContractGridViewModel> ();

            config.Compile ();

            _ = entity.Adapt ( documentTemplateViewModel, config );

            return documentTemplateViewModel;
        }
    }
}
