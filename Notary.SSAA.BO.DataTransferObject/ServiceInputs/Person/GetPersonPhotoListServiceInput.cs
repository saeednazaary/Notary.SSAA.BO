using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person
{
    public class GetPersonPhotoListServiceInput : BaseExternalRequest<ApiResult<GetPersonPhotoListViewModel>>
    {
        public IList<string> NationalNos { get; set; }
        public string ClientId { get; set; } = "SSAR";

    }
}
