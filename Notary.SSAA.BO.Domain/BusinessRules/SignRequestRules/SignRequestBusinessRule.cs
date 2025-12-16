using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules
{
    public static class SignRequestBusinessRule
    {
        public static decimal TotalPrice(SignRequest signRequest)
        {
            decimal totalPrice = 0;
            return totalPrice;
        }
        public static bool IsGetNationalNoOnWorkTime(DateTime currentDateTime, string dayOfWeek, List<SsrConfig> ssrConfigs)
        {
            var sabteAhvalConfig = ssrConfigs
                .FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestWorkTimeConfigConstants.GetNationalNo);
            if (sabteAhvalConfig != null && sabteAhvalConfig.ConditionType == "1")
            {
                foreach (var item in sabteAhvalConfig.SsrConfigConditionTimes.Where(x=>x.DayOfWeek==dayOfWeek).ToList())
                {
                    var fromTime = TimeSpan.Parse(item.FromTime);
                    var toTime = TimeSpan.Parse(item.ToTime);

                    if (currentDateTime.TimeOfDay >= fromTime && currentDateTime.TimeOfDay <= toTime)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        public static bool IsGetFingerprintOnWorkTime(DateTime currentDateTime, string dayOfWeek, List<SsrConfig> ssrConfigs)
        {
            var sabteAhvalConfig = ssrConfigs
                .FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestWorkTimeConfigConstants.GetFingerprint);
            if (sabteAhvalConfig != null && sabteAhvalConfig.ConditionType == "1")
            {
                foreach (var item in sabteAhvalConfig.SsrConfigConditionTimes.Where(x => x.DayOfWeek == dayOfWeek).ToList())
                {
                    var fromTime = TimeSpan.Parse(item.FromTime);
                    var toTime = TimeSpan.Parse(item.ToTime);

                    if (currentDateTime.TimeOfDay >= fromTime && currentDateTime.TimeOfDay <= toTime)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        public static bool CheckSignRequestConfig(List<SsrConfig> signRequestConfigs)
        {
            List<string> configList = new()
            {
                SignRequestConfigConstants.SanaConfig,
                SignRequestConfigConstants.ShahkarConfig,
                SignRequestConfigConstants.AmlakEskanConfig,
                SignRequestConfigConstants.TFAConfig,
                SignRequestConfigConstants.SabteAhvalConfig,
                SignRequestWorkTimeConfigConstants.GetFingerprint,
                SignRequestWorkTimeConfigConstants.GetNationalNo,
                //SignRequestConfigConstants.WorkPermit
            };

            return configList.All(cfg =>
                signRequestConfigs.Any(x => x.SsrConfigSubject.Code == cfg));
        }
        public static bool CheckWorkPermit(List<SsrConfig> signRequestConfigs, string currentScriptoriumId)
        {
            var signRequestWorkPermit = signRequestConfigs.FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestConfigConstants.WorkPermit);
            if (signRequestWorkPermit is not null)
            {
                if (signRequestWorkPermit.SsrConfigConditionScrptrms.Any(x => x.ScriptoriumId == currentScriptoriumId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
