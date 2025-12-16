using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class ParallelSabtAhvalServiceInput : BaseExternalRequest<ApiResult<List<SabtAhvalServiceViewModel>>>
    {
        public string ClientId { get; set; }
        public List<ParallelSabtAhvalItem> PersonItems { get; set; }
    }
    public class ParallelSabtAhvalItem
    {
        public string BirthDate { get; set; }
        public string NationalNo { get; set; }
    }
}
