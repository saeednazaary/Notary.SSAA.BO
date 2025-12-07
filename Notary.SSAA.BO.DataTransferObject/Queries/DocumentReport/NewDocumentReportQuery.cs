using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentReport;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport;



namespace Notary.SSAA.BO.DataTransferObject.Queries.DocumentReport
{
    public class NewDocumentReportQuery : BaseQueryRequest<ApiResult<DocumentReportViewModels>>
    {
        public NewDocumentReportQuery(string id ,string commandName)
        {
            Id = id;
            CommandName = commandName;
        }
        public string Id { get; set; }
        public string CommandName { get; set; }

    }
 
}
