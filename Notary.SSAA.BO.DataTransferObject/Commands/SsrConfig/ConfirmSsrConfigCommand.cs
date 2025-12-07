using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SsrConfig
{
    public class ConfirmSsrConfigCommand : BaseCommandRequest<ApiResult>
    {
        public string ConfigId { get; set; }

    }
}
