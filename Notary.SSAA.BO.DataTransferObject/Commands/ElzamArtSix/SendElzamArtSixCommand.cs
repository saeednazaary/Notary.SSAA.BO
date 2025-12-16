using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.ElzamArtSix
{
    public class SendElzamArtSixCommand : BaseCommandRequest<ApiResult>
    {
        public SendElzamArtSixCommand()
        {
        }        
        public string Id { get; set; }        
    }
    

}




