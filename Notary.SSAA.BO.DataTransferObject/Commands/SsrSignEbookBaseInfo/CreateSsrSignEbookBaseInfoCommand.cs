using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SsrSignEbookBaseInfo
{
    public class CreateSsrSignEbookBaseInfoCommand : BaseCommandRequest<ApiResult>
    {
        public CreateSsrSignEbookBaseInfoCommand()
        {
        }
        public string NumberOfBooks { get; set; }
        public string LastRegisterVolumeNo { get; set; }
        public string LastRegisterPaperNo { get; set; }
        public string LastClassifyNo { get; set; }
    }
}
