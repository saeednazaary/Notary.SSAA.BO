using Notary.SSAA.BO.SharedKernel.Enumerations;

namespace Notary.SSAA.BO.Configuration
{
    public class AgentDocValidatorConfiguration
    {
        public AgentDocValidatorConfiguration()
        {
            this.SetConfigurations();
        }

        private void SetConfigurations()
        {
            string validatorConfigurationString = Settings.AgentDocValidatorConfiguration;
            if (string.IsNullOrWhiteSpace(validatorConfigurationString))
            {
                validatorConfigurationString = "D,A,A,A,A,A,A,A,A,A,W,W,W,W,W,W,W,A,A,W,A";
            }

            this.ParseConfigurationString(validatorConfigurationString);
        }

        public RestrictionLevel ParseSingleConfigString(string input)
        {
            switch (input)
            {
                case "D":
                    return RestrictionLevel.Disabled;
                case "E":
                    return RestrictionLevel.Enabled;
                case "W":
                    return RestrictionLevel.Warning;
                case "A":
                    return RestrictionLevel.Avoidance;
                case "P":
                    return RestrictionLevel.Pass;
                default:
                    return RestrictionLevel.Disabled;
            }
        }

        public void ParseConfigurationString(string input)
        {
            string[] inputArray = input.Split(',');
            if (inputArray.Length < 21)
            {
                _validatorEnabled = RestrictionLevel.Disabled;
                _v1 = RestrictionLevel.Disabled;
                _v2 = RestrictionLevel.Disabled;
                _v3 = RestrictionLevel.Disabled;
                _v4 = RestrictionLevel.Disabled;
                _v5 = RestrictionLevel.Disabled;
                _v6 = RestrictionLevel.Disabled;
                _v7 = RestrictionLevel.Disabled;
                _v8 = RestrictionLevel.Disabled;
                _v9 = RestrictionLevel.Disabled;
                _v10 = RestrictionLevel.Disabled;
                _v11 = RestrictionLevel.Disabled;
                _v12 = RestrictionLevel.Disabled;
                _v13 = RestrictionLevel.Disabled;
                _v14 = RestrictionLevel.Disabled;
                _v15 = RestrictionLevel.Disabled;
                _v16 = RestrictionLevel.Disabled;
                _v17 = RestrictionLevel.Disabled;
                _v18 = RestrictionLevel.Disabled;
                _v19 = RestrictionLevel.Disabled;
                _v20 = RestrictionLevel.Disabled;
            }
            else
            {
                _validatorEnabled = ParseSingleConfigString(inputArray[0]);
                _v1 = ParseSingleConfigString(inputArray[1]);
                _v2 = ParseSingleConfigString(inputArray[2]);
                _v3 = ParseSingleConfigString(inputArray[3]);
                _v4 = ParseSingleConfigString(inputArray[4]);
                _v5 = ParseSingleConfigString(inputArray[5]);
                _v6 = ParseSingleConfigString(inputArray[6]);
                _v7 = ParseSingleConfigString(inputArray[7]);
                _v8 = ParseSingleConfigString(inputArray[8]);
                _v9 = ParseSingleConfigString(inputArray[9]);
                _v10 = ParseSingleConfigString(inputArray[10]);
                _v11 = ParseSingleConfigString(inputArray[11]);
                _v12 = ParseSingleConfigString(inputArray[12]);
                _v13 = ParseSingleConfigString(inputArray[13]);
                _v14 = ParseSingleConfigString(inputArray[14]);
                _v15 = ParseSingleConfigString(inputArray[15]);
                _v16 = ParseSingleConfigString(inputArray[16]);
                _v17 = ParseSingleConfigString(inputArray[17]);
                _v18 = ParseSingleConfigString(inputArray[18]);
                _v19 = ParseSingleConfigString(inputArray[19]);
                _v20 = ParseSingleConfigString(inputArray[20]);
            }
        }

        public bool _appendErrorCode = false;
        public RestrictionLevel _validatorEnabled = RestrictionLevel.Disabled;
        public RestrictionLevel _v1 = RestrictionLevel.Disabled;
        public RestrictionLevel _v2 = RestrictionLevel.Disabled;
        public RestrictionLevel _v3 = RestrictionLevel.Disabled;
        public RestrictionLevel _v4 = RestrictionLevel.Disabled;
        public RestrictionLevel _v5 = RestrictionLevel.Disabled;
        public RestrictionLevel _v6 = RestrictionLevel.Disabled;
        public RestrictionLevel _v7 = RestrictionLevel.Disabled;
        public RestrictionLevel _v8 = RestrictionLevel.Disabled;
        public RestrictionLevel _v9 = RestrictionLevel.Disabled;
        public RestrictionLevel _v10 = RestrictionLevel.Disabled;
        public RestrictionLevel _v11 = RestrictionLevel.Disabled;
        public RestrictionLevel _v12 = RestrictionLevel.Disabled;
        public RestrictionLevel _v13 = RestrictionLevel.Disabled;
        public RestrictionLevel _v14 = RestrictionLevel.Disabled;
        public RestrictionLevel _v15 = RestrictionLevel.Disabled;
        public RestrictionLevel _v16 = RestrictionLevel.Disabled;
        public RestrictionLevel _v17 = RestrictionLevel.Disabled;
        public RestrictionLevel _v18 = RestrictionLevel.Disabled;
        public RestrictionLevel _v19 = RestrictionLevel.Disabled;
        public RestrictionLevel _v20 = RestrictionLevel.Disabled;

        public bool AppendErrorCode => _appendErrorCode;
        public RestrictionLevel ValidatorEnabled => _validatorEnabled;
        public RestrictionLevel V1 => _v1;
        public RestrictionLevel V2 => _v2;
        public RestrictionLevel V3 => _v3;
        public RestrictionLevel V4 => _v4;
        public RestrictionLevel V5 => _v5;
        public RestrictionLevel V6 => _v6;
        public RestrictionLevel V7 => _v7;
        public RestrictionLevel V8 => _v8;
        public RestrictionLevel V9 => _v9;
        public RestrictionLevel V10 => _v10;
        public RestrictionLevel V11 => _v11;
        public RestrictionLevel V12 => _v12;
        public RestrictionLevel V13 => _v13;
        public RestrictionLevel V14 => _v14;
        public RestrictionLevel V15 => _v15;
        public RestrictionLevel V16 => _v16;
        public RestrictionLevel V17 => _v17;
        public RestrictionLevel V18 => _v18;
        public RestrictionLevel V19 => _v19;
        public RestrictionLevel V20 => _v20;
    }

}
