using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.BusinessRules.GeneralRules;
using Notary.SSAA.BO.SharedKernel.Constants;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SignRequest
{
    public static class UpdateSignRequestConfigValidator
    {
        public static string CheckSana(ICollection<SignRequestPersonViewModel> signRequestPersons, List<Domain.Entities.SsrConfig> ssrConfigs)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;
            var sanaConfig = ssrConfigs.FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestConfigConstants.SanaConfig);
            if (sanaConfig != null && sanaConfig.ConditionType == "1")
            {
                foreach (var item in signRequestPersons)
                {
                    // Here we assume "سنا معتبر" means IsPersonSanaChecked is true
                    if (item.IsPersonSanaChecked != true)
                    {
                        if (!isValid)
                            result += " و ";

                        result += $"{item.PersonName} {item.PersonFamily}";
                        isValid = false;
                    }
                }
            }
            result += " وضعیت ثنا آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }
        public static string CheckShahkar(
            ICollection<SignRequestPersonViewModel> signRequestPersons,
            List<Domain.Entities.SsrConfig> ssrConfigs)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;

            var shahkarConfig = ssrConfigs
                .FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestConfigConstants.ShahkarConfig);

            if (shahkarConfig != null && shahkarConfig.ConditionType == "1")
            {
                foreach (var item in signRequestPersons)
                {
                    // In ViewModel, PersonMobileNoState is nullable bool; true means valid
                    if (item.PersonMobileNoState != true)
                    {
                        if (!isValid)
                            result += " و ";

                        result += $"{item.PersonName} {item.PersonFamily}";
                        isValid = false;
                    }
                }
            }

            result += " وضعیت شاهکار آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }
        public static string CheckAmlakEskan(
            ICollection<SignRequestPersonViewModel> signRequestPersons,
            List<Domain.Entities.SsrConfig> ssrConfigs)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;

            var amlakConfig = ssrConfigs
                .FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestConfigConstants.AmlakEskanConfig);

            if (amlakConfig != null && amlakConfig.ConditionType == "1")
            {
                foreach (var item in signRequestPersons)
                {
                    // In ViewModel, AmlakEskanState is nullable bool; true means valid
                    if (item.AmlakEskanState != true)
                    {
                        if (!isValid)
                            result += " و ";

                        result += $"{item.PersonName} {item.PersonFamily}";
                        isValid = false;
                    }
                }
            }

            result += " وضعیت املاک واسکان آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }
        public static string CheckPostalCode(
            ICollection<SignRequestPersonViewModel> signRequestPersons,
            List<Domain.Entities.SsrConfig> ssrConfigs)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;

            var postalCodeConfig = ssrConfigs
                .FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestConfigConstants.PostConfig);

            if (postalCodeConfig != null && postalCodeConfig.ConditionType == "1")
            {
                foreach (var item in signRequestPersons)
                {
                    if (!GeneralBusinessRule.IsValidPostalCode(item.PersonPostalCode))
                    {
                        if (!isValid)
                            result += " و ";

                        result += item.PersonName + " " + item.PersonFamily;
                        isValid = false;
                    }
                }
            }

            result += " کد پستی آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }

        public static string CheckSabteAhval(ICollection<SignRequestPersonViewModel> signRequestPersons,
            List<Domain.Entities.SsrConfig> ssrConfigs)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;

            var sabteAhvalConfig = ssrConfigs
                .FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestConfigConstants.SabteAhvalConfig);

            if (sabteAhvalConfig != null && sabteAhvalConfig.ConditionType == "1")
            {
                foreach (var item in signRequestPersons)
                {
                    if (item.IsPersonIranian)
                    {
                        if (item.IsPersonSabteAhvalChecked ==false|| item.IsPersonSabteAhvalCorrect ==false)
                        {
                            if (!isValid)
                                result += " و ";

                            result += item.PersonName + " " + item.PersonFamily;
                            isValid = false;
                        }
                    }
                }
            }

            result += " وضعیت ثبت احوال آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }

        public static string CheckPersonAlive(
            ICollection<SignRequestPersonViewModel> signRequestPersons)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;


            foreach (var item in signRequestPersons)
            {
                if (item.IsPersonAlive.HasValue && item.IsPersonAlive.Value == false)
                {
                    if (!isValid)
                        result += " و ";

                    result += $"{item.PersonName} {item.PersonFamily}";
                    isValid = false;
                }
            }
            result += " در قید حیات نمی‌باشد.";
            return isValid ? string.Empty : result;
        }


    }
}
