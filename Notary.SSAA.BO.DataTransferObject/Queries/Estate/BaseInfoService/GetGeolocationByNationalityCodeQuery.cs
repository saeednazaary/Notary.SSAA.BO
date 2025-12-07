
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class GetGeolocationByNationalityCodeQuery : BaseQueryRequest<ApiResult<GetGeolocationByNationalityCodeViewModel>>
    {
        public GetGeolocationByNationalityCodeQuery(string nationalityCode)
        {
            NationalityCode = nationalityCode;
        }
        public string NationalityCode { get; set; }
    }
}
