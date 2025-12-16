using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveSupport;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExecutiveSupport
{
    public class GetExecutiveSupportByNoQuery : BaseQueryRequest<ApiResult<GetExecutiveSupportByNoResponseViewModel>>
    {
        [System.ComponentModel.DisplayName("شماره اجرائیه")]
        [System.ComponentModel.Description("شماره اجرائیه")]
        public string ExecutiveNo { get; set; }

        [System.ComponentModel.DisplayName("شماره پرونده اجرا")]
        [System.ComponentModel.Description("شماره پرونده اجرا")]
        public string CaseNo { get; set; }
    }

}
