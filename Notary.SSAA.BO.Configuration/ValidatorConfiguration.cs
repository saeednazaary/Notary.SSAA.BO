using Notary.SSAA.BO.SharedKernel.Enumerations;

namespace Notary.SSAA.BO.Configuration
{
    public class ValidatorConfiguration
    {
        public ValidatorConfiguration()
        {
            this.SetConfigurations();
        }

        private void SetConfigurations()
        {
            string validatorConfigurationString = Settings.ValidatorConfiguration;//System.Configuration.ConfigurationSettings.AppSettings["ValidatorConfiguration"] as string;
            if (string.IsNullOrWhiteSpace(validatorConfigurationString))
            {
                validatorConfigurationString = "D,A,A,A,A,A,A,A,A,A,W,W,W,W,A";
            }

            this.ParseConfigurationString(validatorConfigurationString);
        }

        private RestrictionLevel ParseSingleConfigString(string input)
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

        private void ParseConfigurationString(string input)
        {
            //string appendErrorCode = System.Configuration.ConfigurationSettings.AppSettings["AppendErrorCode"] as string;
            // bool.TryParse ( appendErrorCode, out _appendErrorCode );
            _appendErrorCode = Settings.AppendErrorCode;
            string[] inputArray = input.Split(',');
            if (inputArray.Length < 15)
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
            }
        }

        private bool _appendErrorCode = false;
        private RestrictionLevel _validatorEnabled = RestrictionLevel.Disabled;
        private RestrictionLevel _v1 = RestrictionLevel.Disabled;
        private RestrictionLevel _v2 = RestrictionLevel.Disabled;
        private RestrictionLevel _v3 = RestrictionLevel.Disabled;
        private RestrictionLevel _v4 = RestrictionLevel.Disabled;
        private RestrictionLevel _v5 = RestrictionLevel.Disabled;
        private RestrictionLevel _v6 = RestrictionLevel.Disabled;
        private RestrictionLevel _v7 = RestrictionLevel.Disabled;
        private RestrictionLevel _v8 = RestrictionLevel.Disabled;
        private RestrictionLevel _v9 = RestrictionLevel.Disabled;
        private RestrictionLevel _v10 = RestrictionLevel.Disabled;
        private RestrictionLevel _v11 = RestrictionLevel.Disabled;
        private RestrictionLevel _v12 = RestrictionLevel.Disabled;
        private RestrictionLevel _v13 = RestrictionLevel.Disabled;
        private RestrictionLevel _v14 = RestrictionLevel.Disabled;

        internal bool AppendErrorCode => _appendErrorCode;
        internal RestrictionLevel ValidatorEnabled => _validatorEnabled;
        internal RestrictionLevel V1 => _v1;
        internal RestrictionLevel V2 => _v2;
        internal RestrictionLevel V3 => _v3;
        internal RestrictionLevel V4 => _v4;
        internal RestrictionLevel V5 => _v5;
        internal RestrictionLevel V6 => _v6;
        internal RestrictionLevel V7 => _v7;
        internal RestrictionLevel V8 => _v8;
        internal RestrictionLevel V9 => _v9;
        internal RestrictionLevel V10 => _v10;
        internal RestrictionLevel V11 => _v11;
        internal RestrictionLevel V12 => _v12;
        internal RestrictionLevel V13 => _v13;
        internal RestrictionLevel V14 => _v14;
    }

}
