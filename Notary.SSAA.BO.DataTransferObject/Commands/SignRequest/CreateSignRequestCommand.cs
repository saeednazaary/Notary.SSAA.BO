using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SignRequest
{
    public class CreateSignRequestCommand : BaseCommandRequest<ApiResult>
    {
        public CreateSignRequestCommand()
        {
            SignRequestPersons = new List<SignRequestPersonViewModel>();
            SignRequestRelatedPersons = new List<ToRelatedPersonViewModel>();
        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public string SignRequestSignText { get; set; }
        public string SignRequestId { get; set; }
        public IList<string> SignRequestGetterId { get; set; }
        public IList<string> SignRequestSubjectId { get; set; }
        public IList<SignRequestPersonViewModel> SignRequestPersons { get; set; }
        public IList<ToRelatedPersonViewModel> SignRequestRelatedPersons { get; set; }
        public bool IsRemoteRequest { get; set; }
        public string RemoteRequestId { get; set; }
    }
}
