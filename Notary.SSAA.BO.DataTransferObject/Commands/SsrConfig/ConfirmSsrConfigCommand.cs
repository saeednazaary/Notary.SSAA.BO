using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SsrConfig
{
    public class ConfirmSsrConfigCommand : BaseCommandRequest<ApiResult>
    {
        public IList<string> SsrConfigMainSubjectId { get; set; }
        public IList<string> SsrConfigSubjectId { get; set; }
        public string ConfigStartTime { get; set; }
        public string ConfigStartDate { get; set; }
        public string ConfigEndTime { get; set; }
        public string ConfigEndDate { get; set; }
        public string ConfigValue { get; set; }
        public string ConditionValue { get; set; }
        public string ConditionType { get; set; }
        public string ActionType { get; set; }
        public string ConfigId { get; set; }

    }
}
