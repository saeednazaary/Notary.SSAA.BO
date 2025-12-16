using Notary.SSAA.BO.Domain.BusinessRules.GeneralRules;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq;


namespace Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules
{
    public static class SignRequestPersonBusinessRule
    {
        public static bool CheckRelatedPersonExists(SignRequestPerson signRequestPerson, ICollection<SignRequestPersonRelated> signRequestPersonRelated)
        {
            bool isValid = true;

            if (signRequestPerson.IsRelated == "1")
            {
                if (!signRequestPersonRelated.Any(x => x.AgentPersonId == signRequestPerson.Id))
                    isValid = false;
            }
            return isValid;
        }

        public static string CheckRelatedPersonExists(ICollection<SignRequestPerson> signRequestPerson, ICollection<SignRequestPersonRelated> signRequestPersonRelated)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;
            foreach (var item in signRequestPerson)
            {
                if (item.IsRelated == "1")
                {
                    if (!signRequestPersonRelated.Any(x => x.AgentPersonId == item.Id))
                    {
                        if (!isValid)
                            result += " و ";

                        result += item.Name + " " + item.Family;
                        isValid = false;

                    }
                }
            }
            result += " وضعیت اشخاص وابسته آنها معتبر نیست . ";
            if (isValid) return string.Empty;
            else return result;
        }


        public static bool CheckSaghir(SignRequestPerson SaghirPerson, ICollection<SignRequestPerson> signRequestPerson, ICollection<SignRequestPersonRelated> signRequestPersonRelated, string currentPersianDateTime)
        {
            bool isValid = true;

            if (SaghirPerson.BirthDate.GetDateTimeDistance(currentPersianDateTime).Seconds < 0)
            {
                var foundRelated = signRequestPersonRelated.Where(x => x.AgentPersonId == SaghirPerson.Id).FirstOrDefault();

                if (foundRelated is not null)
                {
                    if (!signRequestPerson.Any(x => x.Id == foundRelated.MainPersonId))
                        isValid = false;
                }
                else
                {
                    isValid = false;
                }

            }
            return isValid;
        }
        public static string CheckSaghir(ICollection<SignRequestPerson> signRequestPerson, ICollection<SignRequestPersonRelated> signRequestPersonRelated, string currentPersianDateTime)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;
            foreach (var item in signRequestPerson)
            {

                if (item.BirthDate.GetDateTimeDistance(currentPersianDateTime).Seconds < 0)
                {
                    if (!signRequestPersonRelated.Any(x => x.MainPersonId == item.Id))
                    {
                        isValid = false;
                        result += item.Name + " " + item.Family + " و ";

                    }
                }
            }
            result += " صغیر هستند و وابسته آنها موجو نیست . ";
            if (isValid)
            {
                return string.Empty;
            }
            else
            {
                return result;

            }
        }
        public static string CheckSabteAhval(
               ICollection<SignRequestPerson> signRequestPersons,
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
                    if (item.IsIranian == "1")
                    {
                        if (item.IsSabtahvalChecked != "1" || item.IsSabtahvalCorrect != "1")
                        {
                            if (!isValid)
                                result += " و ";

                            result += item.Name + " " + item.Family;
                            isValid = false;
                        }
                    }
                }
            }

            result += " وضعیت ثبت احوال آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }

        public static string CheckSana(
            ICollection<SignRequestPerson> signRequestPersons,
            List<Domain.Entities.SsrConfig> ssrConfigs)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;

            var sanaConfig = ssrConfigs
                .FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestConfigConstants.SanaConfig);

            if (sanaConfig != null && sanaConfig.ConditionType == "1")
            {
                foreach (var item in signRequestPersons)
                {
                    if (item.SanaState != "1")
                    {
                        if (!isValid)
                            result += " و ";

                        result += item.Name + " " + item.Family;
                        isValid = false;
                    }
                }
            }

            result += " وضعیت ثنا آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }



        public static string CheckShahkar(
            ICollection<SignRequestPerson> signRequestPersons,
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
                    if (item.MobileNoState != "1")
                    {
                        if (!isValid)
                            result += " و ";

                        result += item.Name + " " + item.Family;
                        isValid = false;
                    }
                }
            }

            result += " وضعیت شاهکار آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }

        public static string CheckAmlakEskan(
            ICollection<SignRequestPerson> signRequestPersons,
            List<Domain.Entities.SsrConfig> ssrConfigs)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;

            var amlakEskanConfig = ssrConfigs
                .FirstOrDefault(x => x.SsrConfigSubject.Code == SignRequestConfigConstants.AmlakEskanConfig);

            if (amlakEskanConfig != null && amlakEskanConfig.ConditionType == "1")
            {
                foreach (var item in signRequestPersons)
                {
                    if (item.AmlakEskanState != "1")
                    {
                        if (!isValid)
                            result += " و ";

                        result += item.Name + " " + item.Family;
                        isValid = false;
                    }
                }
            }

            result += " وضعیت املاک واسکان آنها معتبر نیست . ";
            return isValid ? string.Empty : result;
        }
        public static string CheckMobileNo(ICollection<SignRequestPerson> signRequestPerson)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;
            foreach (var item in signRequestPerson)
            {
                if (!GeneralBusinessRule.IsValidMobileNo(item.MobileNo))
                {
                    if (!isValid)
                        result += " و ";

                    result += item.Name + " " + item.Family;
                    isValid = false;
                }
            }
            result += " شماره تلفن آنها معتبر نیست . ";
            if (isValid) return string.Empty;
            else return result;
        }
        public static string CheckPostalCode(ICollection<SignRequestPerson> signRequestPerson)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;
            foreach (var item in signRequestPerson)
            {
                if (!GeneralBusinessRule.IsValidPostalCode(item.PostalCode))
                {
                    if (!isValid)
                        result += " و ";

                    result += item.Name + " " + item.Family;
                    isValid = false;
                }
            }
            result += " کد پستی آنها معتبر نیست . ";
            if (isValid) return string.Empty;
            else return result;
        }
        public static string CheckInheritorExists(ICollection<SignRequestPerson> signRequestPerson, ICollection<SignRequestPersonRelated> signRequestPersonRelated)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;
            foreach (var item in signRequestPerson)
            {
                if (item.IsAlive == "2")
                    if (!SignRequestRelatedPersonBusinessRule.CheckInheritorExists(signRequestPersonRelated, item.Id))
                    {
                        if (!isValid)
                            result += " و ";

                        result += item.Name + " " + item.Family;
                        isValid = false;
                    }
            }
            result += " وضعیت وارثین آنها معتبر نیست . ";
            if (isValid) return string.Empty;
            else return result;
        }
        public static ICollection<SignRequestPerson> FingerprintPersons(ICollection<SignRequestPerson> signRequestPerson)
        {
            return signRequestPerson;
        }
        public static bool CheckFingerprintPerson(SignRequestPerson signRequestPerson)
        {
            bool isValid = false;

            if (signRequestPerson.IsFingerprintGotten == "1")
                isValid = true;

            return isValid;

        }
        public static string CheckFingerprintPerson(ICollection<SignRequestPerson> signRequestPerson, ICollection<SignRequestPersonRelated> signRequestRelatedPerson)
        {
            string result = " برای شخص/اشخاص: ";
            bool isValid = true;
            var notCheckPersonList = new List<Guid?>();
            foreach (var item in signRequestRelatedPerson)
            {
                switch (item.AgentTypeId)
                {
                    case "2":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "3":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "5":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "7":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "14":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "18":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "10":
                        if (item.ReliablePersonReasonId == "7")
                            notCheckPersonList.Add(item.MainPersonId);

                        break;
                    default:
                        break;
                }
            }
            notCheckPersonList = notCheckPersonList.Distinct().ToList();

            foreach (var item in signRequestPerson)
            {
                if (!notCheckPersonList.Exists(x => x == item.Id))
                {
                    if (item.IsFingerprintGotten != "1")
                    {
                        if (!isValid)
                            result += " و ";

                        result += item.Name + " " + item.Family;
                        isValid = false;
                    }
                }
            }
            result += " اثر انگشت ثبت نشده است. ";
            result += "در صورت عدم  امکان اخذ اثر انگشت بایستی مطابق تبصره 2 ماده 4 شیوه نامه بهره برداری از دفتر الکترونیک اقدام شود.";
            if (isValid) return string.Empty;
            else return result;


        }
        public static string CheckPersonAlive(ICollection<SignRequestPerson> signRequestPerson)
        {
            string result = " شخص/اشخاص ";
            bool isValid = true;
            foreach (var item in signRequestPerson)
            {
                if (item.IsAlive != "1")
                {
                    if (!isValid)
                        result += " و ";

                    result += item.Name + " " + item.Family;
                    isValid = false;
                }
            }
            result += " اثرانگشت آنها موجود نیست. ";
            if (isValid) return string.Empty;
            else return result;
        }
    }
}

