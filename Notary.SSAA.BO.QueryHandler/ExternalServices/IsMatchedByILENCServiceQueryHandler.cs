using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.Constants;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class IsMatchedByILENCServiceQueryHandler : BaseQueryHandler<IsMatchedByILENCServiceQuery, ApiResult<ILENCServiceViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public IsMatchedByILENCServiceQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(IsMatchedByILENCServiceQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<ILENCServiceViewModel>> RunAsync(IsMatchedByILENCServiceQuery request, CancellationToken cancellationToken)
        {
            
            string mainUrl = _configuration.GetValue(typeof(string), "InternalGatewayUrl").ToString()+ "ExternalServices/v1/";

            var queryParameters = new Dictionary<string, string>();
            queryParameters.Add("NationalNo", request.NationalNo);
            queryParameters.Add("ClientId", "SSAR");
            var httpRequest = new HttpEndpointRequest(mainUrl + "ILENCService", _userService.UserApplicationContext.Token, queryParameters);

            var result = await _httpEndPointCaller.CallGetApiAsync<ILENCServiceViewModel>(httpRequest
              , cancellationToken);
            if (result.IsSuccess)
            {
                if (result.Data != null)
                {
                    if ((string.IsNullOrWhiteSpace(request.Name) && !string.IsNullOrWhiteSpace(result.Data.TheCCompany.Name)) || (!string.IsNullOrWhiteSpace(request.Name) && string.IsNullOrWhiteSpace(result.Data.TheCCompany.Name)) || request.Name.NormalizeTextChars(false) != result.Data.TheCCompany.Name.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("نام  شخص حقوقی با سامانه اشخاص حقوقی مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.RegisterNo) && !string.IsNullOrWhiteSpace(result.Data.TheCCompany.RegisterNumber)) || (!string.IsNullOrWhiteSpace(request.RegisterNo) && string.IsNullOrWhiteSpace(result.Data.TheCCompany.RegisterNumber)) || request.RegisterNo.NormalizeTextChars(false) != result.Data.TheCCompany.RegisterNumber.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("شماره ثبت شخص حقوقی با سامانه اشخاص حقوقی مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.RegisterDate) && !string.IsNullOrWhiteSpace(result.Data.TheCCompany.RegisterDate)) || (!string.IsNullOrWhiteSpace(request.RegisterDate) && string.IsNullOrWhiteSpace(result.Data.TheCCompany.RegisterDate)) || request.RegisterDate != result.Data.TheCCompany.RegisterDate)
                    {
                        result.IsSuccess = false;
                        result.message.Add("تاریخ ثبت شخص حقوقی با سامانه اشخاص حقوقی مطابقت ندارد");
                    }
                    if (!result.IsSuccess)
                    {
                        result.Data = null;
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.message.Add("شخص حقوقی در سامانه اشخاص حقوقی یافت نشد");
                }
            }
            return result;
        }

    }
}
