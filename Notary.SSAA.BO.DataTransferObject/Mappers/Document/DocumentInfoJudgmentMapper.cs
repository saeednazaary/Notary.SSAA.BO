namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Microsoft.IdentityModel.Tokens;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;

    /// <summary>
    /// Defines the <see cref="DocumentInfoJudgmentMapper" />
    /// </summary>
    public static class DocumentInfoJudgmentMapper
    {
        /// <summary>
        /// The MapToDocumentInfoJudgmentViewModel
        /// </summary>
        /// <param name="databaseResult">The databaseResult<see cref="Domain.Entities.DocumentInfoJudgement"/></param>
        /// <returns>The <see cref="DocumentInfoJudgmentViewModel"/></returns>
        public static DocumentInfoJudgmentViewModel MapToDocumentInfoJudgmentViewModel ( Domain.Entities.DocumentInfoJudgement databaseResult )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Domain.Entities.DocumentInfoJudgement, DocumentInfoJudgmentViewModel> ()
                .Map ( dest => dest.RequestId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentId, src => src.Document.Id.ToString () )
                .Map ( dest => dest.IssueNo, src => src.IssueNo )
                .Map ( dest => dest.CaseClassifyNo, src => src.CaseClassifyNo )
                .Map ( dest => dest.IssueDate, src => src.IssueDate )
                .Map ( dest => dest.IssuerName, src => src.IssuerName )
                 .Map ( dest => dest.DocumentJudgmentTypeId, src => src.DocumentJudgementTypeId != null
                            ? new List<string> { src.DocumentJudgementTypeId.ToString () }
                            : new List<string> () );
            return databaseResult.Adapt<DocumentInfoJudgmentViewModel> ( config );
        }

        /// <summary>
        /// The MapToDocumentInfoJudgment
        /// </summary>
        /// <param name="documentInfoJudgment">The documentInfoJudgment<see cref="DocumentInfoJudgement"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentInfoJudgmentViewModel"/></param>
        public static void MapToDocumentInfoJudgment ( ref DocumentInfoJudgement documentInfoJudgment, DocumentInfoJudgmentViewModel viewModel )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentInfoJudgmentViewModel, DocumentInfoJudgement> ()
                .Map ( dest => dest.Id, src => src.IsNew ? Guid.NewGuid () : src.RequestId.ToGuid () )
                .Map ( dest => dest.DocumentId, src => src.DocumentId.ToGuid () )
                .Map ( dest => dest.IssueNo, src => src.IssueNo )
                .Map ( dest => dest.CaseClassifyNo, src => src.CaseClassifyNo )
                .Map ( dest => dest.IssueDate, src => src.IssueDate )
                .Map ( dest => dest.IssuerName, src => src.IssuerName )
                .Map(dest => dest.DocumentJudgementTypeId,src => !src.DocumentJudgmentTypeId.IsNullOrEmpty()? src.DocumentJudgmentTypeId.First(): null).Ignore ( src => src.ScriptoriumId )
                .Ignore ( src => src.Ilm )
               .IgnoreIf ( ( src, dest ) => src.IsNew, dest => dest.DocumentId );
            config.Compile ();
            viewModel.Adapt ( documentInfoJudgment, config );
        }
    }
}
