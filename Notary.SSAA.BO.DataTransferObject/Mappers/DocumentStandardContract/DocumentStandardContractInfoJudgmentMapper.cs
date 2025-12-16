namespace Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract
{
    using Mapster;
    using Microsoft.IdentityModel.Tokens;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractInfoJudgmentMapper" />
    /// </summary>
    public static class DocumentStandardContractInfoJudgmentMapper
    {
        /// <summary>
        /// The MapToDocumentStandardContractInfoJudgmentViewModel
        /// </summary>
        /// <param name="databaseResult">The databaseResult<see cref="Domain.Entities.DocumentInfoJudgement"/></param>
        /// <returns>The <see cref="DocumentStandardContractInfoJudgmentViewModel"/></returns>
        public static DocumentStandardContractInfoJudgmentViewModel MapToDocumentStandardContractInfoJudgmentViewModel ( Domain.Entities.DocumentInfoJudgement databaseResult )
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Domain.Entities.DocumentInfoJudgement, DocumentStandardContractInfoJudgmentViewModel> ()
                .Map ( dest => dest.RequestId, src => src.Id.ToString () )
                .Map ( dest => dest.DocumentId, src => src.Document.Id.ToString () )
                .Map ( dest => dest.IssueNo, src => src.IssueNo )
                .Map ( dest => dest.CaseClassifyNo, src => src.CaseClassifyNo )
                .Map ( dest => dest.IssueDate, src => src.IssueDate )
                .Map ( dest => dest.IssuerName, src => src.IssuerName )
                 .Map ( dest => dest.DocumentJudgmentTypeId, src => src.DocumentJudgementTypeId != null
                            ? new List<string> { src.DocumentJudgementTypeId.ToString () }
                            : new List<string> () );
            return databaseResult.Adapt<DocumentStandardContractInfoJudgmentViewModel> ( config );
        }

        /// <summary>
        /// The MapToDocumentStandardContractInfoJudgment
        /// </summary>
        /// <param name="documentInfoJudgment">The documentInfoJudgment<see cref="DocumentInfoJudgement"/></param>
        /// <param name="viewModel">The viewModel<see cref="DocumentStandardContractInfoJudgmentViewModel"/></param>
        public static void MapToDocumentStandardContractInfoJudgment ( ref DocumentInfoJudgement documentInfoJudgment, DocumentStandardContractInfoJudgmentViewModel viewModel , bool? isRemoteRequest = false)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<DocumentStandardContractInfoJudgmentViewModel, DocumentInfoJudgement> ()
                .Map ( dest => dest.Id, src =>
                isRemoteRequest == true ? src.RequestId.ToGuid() : (src.IsNew == true ? Guid.NewGuid() : src.RequestId.ToGuid())
                 )
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
