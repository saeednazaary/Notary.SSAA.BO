using MediatR;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Estate
{
    public class ParseConfigValueQueryHandler : BaseQueryHandler<ParseConfigValueQuery, ApiResult<ParseConfigValueViewModel>>
    {
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        public ParseConfigValueQueryHandler(IMediator mediator, IUserService userService)
     : base(mediator, userService)
        {
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(ParseConfigValueQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected async override Task<ApiResult<ParseConfigValueViewModel>> RunAsync(ParseConfigValueQuery request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult<ParseConfigValueViewModel>
            {
                Data = new ParseConfigValueViewModel()
            };
            var result = await ParseConfigValue(request.Value, cancellationToken);
            apiResult.Data.Result = result.ToString().ToLower();
            return apiResult;
        }       
        private async Task<bool> ParseConfigValue(string ConfigValue, CancellationToken cancellationToken)
        {
            var scriptoriumList = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { _userService.UserApplicationContext.BranchAccess.BranchCode }, cancellationToken);
            if (scriptoriumList == null || scriptoriumList.ScriptoriumList == null || scriptoriumList.ScriptoriumList.Count == 0)
            {
                throw new Exception("دفترخانه جاری در اطلاعات پایه سامانه یافت نشد");
            }
            var scriptorium = scriptoriumList.ScriptoriumList[0];           
            string masterConfigValue = ConfigValue;
            if (string.IsNullOrWhiteSpace(masterConfigValue))
                return true;
            if (masterConfigValue == "*" || masterConfigValue.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            if (masterConfigValue == "0" || masterConfigValue.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;

            string[] masterConfigSectionsCollection;
            if (masterConfigValue.Contains('|'))
                masterConfigSectionsCollection = masterConfigValue.Split('|');
            else
                masterConfigSectionsCollection = new string[] { masterConfigValue };
            Dictionary<string, string> configCoupleCollection = new Dictionary<string, string>();
            foreach (string theOneMasterSection in masterConfigSectionsCollection)
            {
                if (theOneMasterSection == "*")
                    return true;
                string key;
                string value = null;
                string[] theOneMasterSectionParts;

                if (theOneMasterSection.Contains(':'))
                {
                    theOneMasterSectionParts = theOneMasterSection.Split(':');
                    value = theOneMasterSectionParts[1];
                }
                else
                    theOneMasterSectionParts = new string[] { theOneMasterSection };

                key = theOneMasterSectionParts[0];

                configCoupleCollection.Add(key, value);
            }

            foreach (string key in configCoupleCollection.Keys)
            {
                if (key == "E" && configCoupleCollection[key] == "0")
                    return false;

                bool isDenyingKey = false;
                if (key.Contains('-'))
                {
                    isDenyingKey = true;
                }
                if (scriptorium != null)
                {
                    if (isDenyingKey && key == scriptorium.Unit.LevelCode.Substring(0, 4))
                        return false;

                    if (key == "*")
                        return true;

                    if (key != scriptorium.Unit.LevelCode.Substring(0, 4))
                        continue;
                }
                if (configCoupleCollection[key] == null || configCoupleCollection[key] == "*")
                    return true;

                string[] subLevels = null;
                if (configCoupleCollection[key].Contains(','))
                    subLevels = configCoupleCollection[key].Split(',');
                else
                    subLevels = new string[] { configCoupleCollection[key] };

                foreach (string theOneSubLevel in subLevels)
                {
                    bool returnValue = true;
                    if (theOneSubLevel.Contains('-'))
                    {
                        returnValue = true;                        
                    }
                    if (scriptorium != null)
                    {
                        if (theOneSubLevel == scriptorium.Code)
                            return returnValue;
                        else if (theOneSubLevel == "*")
                            return true;
                    }
                    if (scriptorium.Unit != null)
                    {
                        if (theOneSubLevel == scriptorium.Unit.Code)
                            return returnValue;
                        else if (theOneSubLevel == "*")
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
