

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SsrConfig
{
    public class SsrConfigViewModel
    {
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public string ConfigId { get; set; }
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
        public string ConfirmDate { get; set; }
        public string ConfirmTime { get; set; }
        public string Confirmer { get; set; }
        public string IsConfirmed { get; set; }
        public string ConfigType { get; set; }
    }
}
