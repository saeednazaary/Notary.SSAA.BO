

using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace SSAA.Notary.DataTransferObject.Commands.SignRequest
{
    public class CreateSignRequestFileEdmCommand : BaseCommandRequest <ApiResult>
    {
        public Guid SignRequestId { get; set; }
        public string EDMVersion { get; set; }
        public string EDMId {  get; set; } 
        public string ScanFile {  get; set; }
    }
}
