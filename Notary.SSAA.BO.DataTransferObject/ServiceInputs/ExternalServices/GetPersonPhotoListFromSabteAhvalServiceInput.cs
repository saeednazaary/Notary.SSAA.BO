using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.ExternalServices.WebApi.Models.RequestsModel.Person
{

    public class GetPersonPhotoListFromSabteAhvalServiceInput : BaseExternalRequest<ApiResult<GetPersonPhotoListViewModel>>
    {
        public string ClientId { get; set; } = "SSAR-BO";
        public string MainObjectId { get; set; } 

        public IList<PersonPhotoListItem> Persons { get; set; }

    }
    public class PersonPhotoListItem
    {
        public string NationalNo { get; set; }
        public string BirthDate { get; set; }
    }
}
