using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons
{
    public static class CalculateFieldUtility
    {
        public static async Task<(bool, string, DocumentElectronicBookBaseinfo)> IsENoteBookBaseInfoInitialized(
            IDocumentElectronicBookBaseInfoRepository documentElectronicBookBaseInfoRepository,
            IUserService userService,
            CancellationToken cancellationToken)
        {
            DocumentElectronicBookBaseinfo theDigitalBookBaseInfo = null;
            string digitalBookBaseInfoObjectId = null;
            var currentScriptoriumDigitalBookBaseInfos = await documentElectronicBookBaseInfoRepository.GetElectronicBooks(
                new List<string> { },
                cancellationToken,
                userService.UserApplicationContext.BranchAccess.BranchCode);

            if (currentScriptoriumDigitalBookBaseInfos != null && currentScriptoriumDigitalBookBaseInfos.Count > 0)
            {
                theDigitalBookBaseInfo = currentScriptoriumDigitalBookBaseInfos.ElementAt(0);
                digitalBookBaseInfoObjectId = theDigitalBookBaseInfo.Id.ToString();
                if (
                    theDigitalBookBaseInfo == null ||
                    theDigitalBookBaseInfo.NumberOfBooksAgent == null ||
                    theDigitalBookBaseInfo.NumberOfBooksArzi == null ||
                    theDigitalBookBaseInfo.NumberOfBooksJari == null ||
                    theDigitalBookBaseInfo.NumberOfBooksOghaf == null ||
                    theDigitalBookBaseInfo.NumberOfBooksOthers == null ||
                    theDigitalBookBaseInfo.NumberOfBooksRahni == null ||
                    theDigitalBookBaseInfo.NumberOfBooksVehicle == null
                )
                    return (false, digitalBookBaseInfoObjectId, theDigitalBookBaseInfo);
            }
            else
            {
                return (true, digitalBookBaseInfoObjectId, theDigitalBookBaseInfo);
            }

            return (false, digitalBookBaseInfoObjectId, theDigitalBookBaseInfo);
        }

        public static async Task<(bool, long)> IsClassifyNoEditable(
            Document theCurrentRequest,
            ClientConfiguration clientConfiguration,
            IDocumentRepository documentRepository,
            IDocumentElectronicBookRepository documentElectronicBookRepository,
            IUserService userService,
            CancellationToken cancellationToken)
        {
            long relatedDocClassifyNo = 0;
            if (!clientConfiguration.IsENoteBookAutoClassifyNoEnabled)
                return (true, relatedDocClassifyNo);

            if (theCurrentRequest.DocumentType.IsSupportive == YesNo.No.ToString())
                return (false, relatedDocClassifyNo);

            //در تمامی اسناد خدمات تبعی بجز اسناد از نوع (اخطاریه، قبض سپرده و گواهي علت عدم انجام معامله) لازم است که شماره ترتیب سند غیر قابل تغییر بوده و خودکار پر شود.
            List<string> editableDocTypes = new List<string>() { "008", "002", "0012", "009" };
            if (
                theCurrentRequest.DocumentType.IsSupportive == YesNo.Yes.ToString() &&
                !editableDocTypes.Contains(theCurrentRequest.DocumentTypeId) &&
                theCurrentRequest.RelatedDocumentIsInSsar != YesNo.Yes.GetString()
                )
            {
                relatedDocClassifyNo = (!string.IsNullOrWhiteSpace(theCurrentRequest.RelatedDocumentNo)) ? long.Parse(theCurrentRequest.RelatedDocumentNo) : 0;
                return (false, relatedDocClassifyNo);
            }

            List<string> notEditableDocTypes = new List<string>() { "004", "005", "006", "007", "0034", "0022" };
            if (!notEditableDocTypes.Contains(theCurrentRequest.DocumentTypeId))
                return (true, relatedDocClassifyNo);

            if (theCurrentRequest.State == NotaryRegServiceReqState.Created.GetString() ||
                theCurrentRequest.State == NotaryRegServiceReqState.CostPaid.GetString() ||
                theCurrentRequest.State == NotaryRegServiceReqState.SetNationalDocumentNo.GetString()
                )
                return (false, relatedDocClassifyNo);

            if (
                theCurrentRequest.RelatedDocumentIsInSsar == YesNo.Yes.GetString() &&
                !string.IsNullOrWhiteSpace(theCurrentRequest.RelatedDocumentNo) &&
                theCurrentRequest.RelatedDocumentNo.Length >= 18 &&
                !editableDocTypes.Contains(theCurrentRequest.DocumentTypeId)
                )
            {
                DocumentElectronicBook theDigitalBookDocEntity = await documentElectronicBookRepository.GetDocumentElectronicBook(theCurrentRequest.RelatedDocumentNo, cancellationToken);

                if (theDigitalBookDocEntity == null)
                {
                    int? result = await documentRepository.GetClassifyNoDocument(
                        theCurrentRequest.RelatedDocumentNo,
                        userService.UserApplicationContext.BranchAccess.BranchCode,
                        cancellationToken);

                    if (result == null)
                    {
                        if (theCurrentRequest.State == NotaryRegServiceReqState.CostPaid.GetString())
                            return (true, relatedDocClassifyNo);
                        else
                            return (false, relatedDocClassifyNo);
                    }
                    else
                    {
                        relatedDocClassifyNo = long.Parse(result.ToString());
                        return (false, relatedDocClassifyNo);
                    }
                }
                else
                {
                    relatedDocClassifyNo = (long)theDigitalBookDocEntity.ClassifyNo;
                    return (false, relatedDocClassifyNo);
                }
            }
            else
            {
                return (true, relatedDocClassifyNo);
            }
        }

        public static bool IsCurrentOragnizationPermitted(
            string configKey,
            IUserService userService,
            string masterConfigString)
        {
            if (string.IsNullOrWhiteSpace(masterConfigString))
                return true;

            if (masterConfigString == "*" || masterConfigString.ToLower() == "true")
                return true;

            if (masterConfigString == "0" || masterConfigString.ToLower() == "false")
                return false;

            string[] masterConfigSectionsCollection = null;

            if (masterConfigString.Contains("|"))
                masterConfigSectionsCollection = masterConfigString.Split('|');
            else
                masterConfigSectionsCollection = new string[] { masterConfigString };

            List<ConfigCouple> configCoupleCollection = new List<ConfigCouple>();
            foreach (string theOneMasterSection in masterConfigSectionsCollection)
            {
                if (theOneMasterSection == "*")
                    return true;

                ConfigCouple configCouple = new ConfigCouple();
                string[] theOneMasterSectionParts = null;

                if (theOneMasterSection.Contains(":"))
                {
                    theOneMasterSectionParts = theOneMasterSection.Split(':');
                    configCouple.Value = theOneMasterSectionParts[1];
                }
                else
                    theOneMasterSectionParts = new string[] { theOneMasterSection };

                configCouple.Key = theOneMasterSectionParts[0];

                configCoupleCollection.Add(configCouple);
            }

            foreach (ConfigCouple theOneConfigCouple in configCoupleCollection)
            {
                if (theOneConfigCouple.Key == "E" && theOneConfigCouple.Value == "0")
                    return false;

                bool isDenyingKey = false;
                if (theOneConfigCouple.Key.Contains("-"))
                {
                    theOneConfigCouple.Key = theOneConfigCouple.Key.Replace("-", "");
                    isDenyingKey = true;
                }

                if (userService.UserApplicationContext.BranchAccess.BranchCode != null && userService.UserApplicationContext.ScriptoriumInformation != null)
                {
                    if (isDenyingKey && theOneConfigCouple.Key == userService.UserApplicationContext.ScriptoriumInformation.Unit.LevelCode.Substring(0, 4))
                        return false;

                    if (theOneConfigCouple.Key == "*")
                        return true;

                    if (theOneConfigCouple.Key != userService.UserApplicationContext.ScriptoriumInformation.Unit.LevelCode.Substring(0, 4))
                        continue;
                }

                if (theOneConfigCouple.Value == null || theOneConfigCouple.Value == "*")
                    return true;

                string[] subLevels = null;

                if (theOneConfigCouple.Value.Contains(","))
                    subLevels = theOneConfigCouple.Value.Split(',');
                else
                    subLevels = new string[] { theOneConfigCouple.Value };

                var scriptoriumCode = userService.UserApplicationContext?.ScriptoriumInformation?.Code;
                if (string.IsNullOrEmpty(scriptoriumCode))
                    return false;

                for (int i = 0; i < subLevels.Length; i++)
                {
                    subLevels[i] = subLevels[i].Replace("-", "");

                    if (subLevels[i] == scriptoriumCode || subLevels[i] == "*")
                        return true;
                }
            }

            return false;
        }

        private class ConfigCouple
        {
            internal string Key { get; set; }
            internal string Value { get; set; }
        }
    }
}