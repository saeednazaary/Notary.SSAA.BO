using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class MeasurementUnitTypeByIdQuery : BaseQueryRequest<ApiResult<MeasurementUnitTypeByIdViewModel>>
    {
        public MeasurementUnitTypeByIdQuery(string[] idList)
        {
            IdList = idList;
        }
        public string[] IdList { get; set; }

    }
}
