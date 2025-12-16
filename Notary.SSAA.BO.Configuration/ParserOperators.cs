using Notary.SSAA.BO.SharedKernel.Contracts.Security;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using System.Reflection;

namespace Notary.SSAA.BO.Configuration
{
    public class ParserOperators
    {
        private readonly ScriptoriumInformation _theCurrentCMSOrganization = new();
        private readonly string _currentLevelCode = string.Empty;

        public ParserOperators(ScriptoriumInformation inputCMSOrganization)
        {
            _theCurrentCMSOrganization = inputCMSOrganization;
            if (_theCurrentCMSOrganization != null)
            {
                _currentLevelCode = _theCurrentCMSOrganization.Unit.LevelCode[..4];
            }
        }

        public ParserOperators()
        {
        }

        internal bool IsFingerprintEnabled()
        {
            return IsCurrentOragnizationPermitted("IsFingerprintEnabled", _theCurrentCMSOrganization);
        }

        internal DSUActionLevel IsDSUDealSummaryEnabled()
        {
            string masterConfigString = Settings.DSUDealSummaryAllowedLevels;
            if (masterConfigString == "*")
            {
                return DSUActionLevel.FullFeature;
            }

            if (masterConfigString == "0")
            {
                return DSUActionLevel.None;
            }

            string[] masterConfigSectionsCollection = masterConfigString.Split('|');

            List<ConfigCouple> configCoupleCollection = [];
            foreach (string theOneMasterSection in masterConfigSectionsCollection)
            {
                if (theOneMasterSection == "*")
                {
                    return DSUActionLevel.FullFeature;
                }

                string[] theOneMasterSectionParts = theOneMasterSection.Split(':');
                configCoupleCollection.Add(new ConfigCouple
                {
                    Key = theOneMasterSectionParts[0],
                    Value = theOneMasterSectionParts[1]
                });
            }

            foreach (ConfigCouple theOneConfigCouple in configCoupleCollection)
            {
                if (theOneConfigCouple.Key == "E" && theOneConfigCouple.Value == "0")
                {
                    return DSUActionLevel.None;
                }

                if (theOneConfigCouple.Key == _currentLevelCode)
                {
                    if (theOneConfigCouple.Value == "*")
                    {
                        return DSUActionLevel.FullFeature;
                    }

                    string[] subLevels = Array.Empty<string>();
                    if (!string.IsNullOrEmpty(theOneConfigCouple.Value))
                    {
                        subLevels = theOneConfigCouple.Value.Split(',');
                    }

                    foreach (string theOneSubLevel in subLevels)
                    {
                        if (theOneSubLevel.Contains("@"))
                        {
                            string[] theOneSubLevelParts = theOneSubLevel.Split('@');
                            if (theOneSubLevelParts[0] == _theCurrentCMSOrganization.Code)
                            {
                                return theOneSubLevelParts[1].ToLower() switch
                                {
                                    "none" => DSUActionLevel.None,
                                    "full" => DSUActionLevel.FullFeature,
                                    "select" => DSUActionLevel.SelectInquiry,
                                    "readonly" => DSUActionLevel.MakeReadOnly,
                                    _ => DSUActionLevel.None
                                };
                            }
                        }
                        else if (theOneSubLevel == _theCurrentCMSOrganization.Code)
                        {
                            return DSUActionLevel.FullFeature;
                        }
                    }
                }
            }

            return DSUActionLevel.None;
        }

        internal bool IsSabtAhvalDirectAccessEnabled()
        {
            return Settings.IsSabtAhvalDirectAccessEnabled;
        }

        internal bool IsLeaveSubSystemEnabled(ScriptoriumInformation theCurrentCMSOrganization)
        {
            string leaveSubSystemEnabledUnits = Settings.LeaveSubSystemEnabledUnits;
            if (string.IsNullOrWhiteSpace(leaveSubSystemEnabledUnits))
            {
                return false;
            }

            if (theCurrentCMSOrganization == null)
            {
                return false;
            }

            if (leaveSubSystemEnabledUnits == "*")
            {
                return true;
            }

            string currentRelatedUnitCode = theCurrentCMSOrganization.Unit.LevelCode[..4];
            string[] leaveSubSystemEnabledUnitsCollection = leaveSubSystemEnabledUnits.Split(',');

            foreach (string unit in leaveSubSystemEnabledUnitsCollection)
            {
                if (unit == currentRelatedUnitCode)
                {
                    return true;
                }
            }

            return false;
        }

        internal bool IsMechanizedExcApplEnabled(ScriptoriumInformation theCurrentCMSOrganization)
        {
            string mechanizedExcApplEnabledUnits = Settings.MechanizedExcApplEnabledUnits;
            if (string.IsNullOrWhiteSpace(mechanizedExcApplEnabledUnits))
            {
                return false;
            }

            if (theCurrentCMSOrganization == null)
            {
                return false;
            }

            if (mechanizedExcApplEnabledUnits == "*")
            {
                return true;
            }

            string currentRelatedUnitCode = theCurrentCMSOrganization.Unit.LevelCode[..4];
            string[] mechanizedExcApplEnabledUnitsCollection = mechanizedExcApplEnabledUnits.Split(',');

            foreach (string unit in mechanizedExcApplEnabledUnitsCollection)
            {
                if (unit == currentRelatedUnitCode)
                {
                    return true;
                }
            }

            return false;
        }

        internal bool IsCurrentOragnizationPermittedOld(string configKey, ScriptoriumInformation theCurrentCMSOrganization)
        {
            string? masterConfigString = GetStaticPropertyValue(typeof(Settings), configKey) as string;

            if (string.IsNullOrWhiteSpace(masterConfigString))
            {
                return true;
            }

            if (masterConfigString == "*" || masterConfigString.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (masterConfigString == "0" || masterConfigString.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            string[] masterConfigSectionsCollection = masterConfigString.Contains('|')
                ? masterConfigString.Split('|')
                : new[] { masterConfigString };

            List<ConfigCouple> configCoupleCollection = [];
            foreach (string theOneMasterSection in masterConfigSectionsCollection)
            {
                if (theOneMasterSection == "*")
                {
                    return true;
                }

                string[] theOneMasterSectionParts;
                ConfigCouple configCouple = new();

                if (theOneMasterSection.Contains(':'))
                {
                    theOneMasterSectionParts = theOneMasterSection.Split(':');
                    configCouple.Value = theOneMasterSectionParts[1];
                }
                else
                {
                    theOneMasterSectionParts = new[] { theOneMasterSection };
                }

                configCouple.Key = theOneMasterSectionParts[0];
                configCoupleCollection.Add(configCouple);
            }

            foreach (ConfigCouple theOneConfigCouple in configCoupleCollection)
            {
                if (theOneConfigCouple.Key == "E" && theOneConfigCouple.Value == "0")
                {
                    return false;
                }

                bool isDenyingKey = false;
                if (theOneConfigCouple.Key.Contains("-"))
                {
                    theOneConfigCouple.Key = theOneConfigCouple.Key.Replace("-", "");
                    isDenyingKey = true;
                }

                if (theCurrentCMSOrganization?.Unit != null)
                {
                    if (isDenyingKey && theOneConfigCouple.Key == theCurrentCMSOrganization.Unit.LevelCode[..4])
                    {
                        return false;
                    }

                    if (theOneConfigCouple.Key == "*")
                    {
                        return true;
                    }

                    if (theOneConfigCouple.Key != theCurrentCMSOrganization.Unit.LevelCode[..4])
                    {
                        continue;
                    }
                }

                if (theOneConfigCouple.Value == null || theOneConfigCouple.Value == "*")
                {
                    return true;
                }

                string[] subLevels = theOneConfigCouple.Value.Contains(',')
                    ? theOneConfigCouple.Value.Split(',')
                    : new[] { theOneConfigCouple.Value };

                foreach (string theOneSubLevel in subLevels)
                {
                    bool returnValue = true;
                    string cleanSubLevel = theOneSubLevel;
                    if (theOneSubLevel.Contains("-"))
                    {
                        cleanSubLevel = theOneSubLevel.Replace("-", "");
                    }

                    if (theCurrentCMSOrganization != null)
                    {
                        if (cleanSubLevel == theCurrentCMSOrganization.Code)
                        {
                            return returnValue;
                        }

                        if (cleanSubLevel == "*")
                        {
                            return true;
                        }

                        if (cleanSubLevel == theCurrentCMSOrganization.Unit.Code)
                        {
                            return returnValue;
                        }
                    }
                }
            }

            return false;
        }

        public static object? GetStaticPropertyValue(Type staticClassType, string propertyName)
        {
            if (staticClassType == null)
            {
                return null;
            }

            PropertyInfo? property = staticClassType.GetProperty(propertyName);
            return property?.GetValue(null);
        }

        internal bool IsCurrentOragnizationPermitted(string configKey, ScriptoriumInformation theCurrentCMSOrganization)
        {
            string? masterConfigString = GetStaticPropertyValue(typeof(Settings), configKey) as string;

            if (string.IsNullOrWhiteSpace(masterConfigString))
            {
                return true;
            }

            if (masterConfigString == "*" || masterConfigString.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (masterConfigString == "0" || masterConfigString.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            string[] masterConfigSectionsCollection = masterConfigString.Contains("|")
                ? masterConfigString.Split('|')
                : new[] { masterConfigString };

            List<ConfigCouple> configCoupleCollection = [];
            foreach (string theOneMasterSection in masterConfigSectionsCollection)
            {
                if (theOneMasterSection == "*")
                {
                    return true;
                }

                string[] theOneMasterSectionParts;
                ConfigCouple configCouple = new();

                if (theOneMasterSection.Contains(":"))
                {
                    theOneMasterSectionParts = theOneMasterSection.Split(':');
                    configCouple.Value = theOneMasterSectionParts[1];
                }
                else
                {
                    theOneMasterSectionParts = new[] { theOneMasterSection };
                }

                configCouple.Key = theOneMasterSectionParts[0];
                configCoupleCollection.Add(configCouple);
            }

            foreach (ConfigCouple theOneConfigCouple in configCoupleCollection)
            {
                if (theOneConfigCouple.Key == "E" && theOneConfigCouple.Value == "0")
                {
                    return false;
                }

                if (theOneConfigCouple.Key == "E" && theOneConfigCouple.Value == "00")
                {
                    // EnableExceptExp mode
                    bool isDenyingKey = false;
                    if (theOneConfigCouple.Key.Contains("-"))
                    {
                        theOneConfigCouple.Key = theOneConfigCouple.Key.Replace("-", "");
                        isDenyingKey = true;
                    }

                    if (theCurrentCMSOrganization != null)
                    {
                        if (isDenyingKey && theOneConfigCouple.Key == theCurrentCMSOrganization.Unit.LevelCode[..4])
                        {
                            return false;
                        }

                        if (theOneConfigCouple.Key == "*")
                        {
                            return true;
                        }

                        if (theOneConfigCouple.Key != theCurrentCMSOrganization.Unit.LevelCode[..4])
                        {
                            continue;
                        }
                    }

                    if (theOneConfigCouple.Value == null || theOneConfigCouple.Value == "*")
                    {
                        return true;
                    }

                    string[] subLevels = theOneConfigCouple.Value.Contains(',')
                        ? theOneConfigCouple.Value.Split(',')
                        : new[] { theOneConfigCouple.Value };

                    foreach (string theOneSubLevel in subLevels)
                    {
                        string cleanSubLevel = theOneSubLevel.Contains("-")
                            ? theOneSubLevel.Replace("-", "")
                            : theOneSubLevel;

                        if (theCurrentCMSOrganization != null && cleanSubLevel == theCurrentCMSOrganization.Code)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            foreach (ConfigCouple theOneConfigCouple in configCoupleCollection)
            {
                if (theOneConfigCouple.Key == "E" && theOneConfigCouple.Value == "0")
                {
                    return false;
                }

                bool isDenyingKey = false;
                if (theOneConfigCouple.Key.Contains("-"))
                {
                    theOneConfigCouple.Key = theOneConfigCouple.Key.Replace("-", "");
                    isDenyingKey = true;
                }

                if (theCurrentCMSOrganization != null)
                {
                    if (isDenyingKey && theOneConfigCouple.Key == theCurrentCMSOrganization.Unit.LevelCode[..4])
                    {
                        return false;
                    }

                    if (theOneConfigCouple.Key == "*")
                    {
                        return true;
                    }

                    if (theOneConfigCouple.Key != theCurrentCMSOrganization.Unit.LevelCode[..4])
                    {
                        continue;
                    }
                }

                if (theOneConfigCouple.Value == null || theOneConfigCouple.Value == "*")
                {
                    return true;
                }

                string[] subLevels = theOneConfigCouple.Value.Contains(',')
                    ? theOneConfigCouple.Value.Split(',')
                    : new[] { theOneConfigCouple.Value };

                foreach (string theOneSubLevel in subLevels)
                {
                    bool returnValue = true;
                    string cleanSubLevel = theOneSubLevel;
                    if (theOneSubLevel.Contains("-"))
                    {
                        cleanSubLevel = theOneSubLevel.Replace("-", "");
                    }

                    if (theCurrentCMSOrganization != null)
                    {
                        if (cleanSubLevel == theCurrentCMSOrganization.Code)
                        {
                            return returnValue;
                        }

                        if (cleanSubLevel == "*")
                        {
                            return true;
                        }

                        if (cleanSubLevel == theCurrentCMSOrganization.Unit.Code)
                        {
                            return returnValue;
                        }
                    }
                }
            }

            return false;
        }

        internal class ConfigCouple
        {
            internal string? Key { get; set; }
            internal string? Value { get; set; }
        }
    }
}
