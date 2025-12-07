using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class GetEstateSepartionInfoInput : BaseExternalRequest<ApiResult<GetEstateSeparationInfoResponse>>
    {
        public GetEstateSepartionInfoInput()
        {
            ClientId = "SSAR";
        }
        public string ConsumerPassword { get; set; }
        public string ConsumerUsername { get; set; }
        public string Basic { get; set; }
        public string Secondary { get; set; }
        public string SectionSSAACode { get; set; }
        public string SubSectionSSAACode { get; set; }
        public string UnitId { get; set; }
        public string ReceiverCmsorganizationId { get; set; }
        public string RequestDateTime { get; set; }
        public string ClientId { get; set; }
    }
}
