using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class GetEstateGeoJsonServiceInput : BaseExternalRequest<ApiResult<GetEstateGeoJsonViewModel>>
    {
        public GetEstateGeoJsonServiceInput()
        {
            UnitId = string.Empty;
            SectionCode = string.Empty;
            SubSectionCode = string.Empty;
            Username = "sabtAnii";
            Password = "ani@254@255";
            ClientId = "SSAR";
        }
        public string ClientId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UnitId { get; set; }
        public string SectionCode { get; set; }
        public string SubSectionCode { get; set; }
        public int PlakOrginal { get; set; }
        public int PlaqkSub { get; set; }
    }
}
