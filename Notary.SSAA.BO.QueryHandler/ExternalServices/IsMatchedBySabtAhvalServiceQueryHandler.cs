using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.Constants;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class IsMatchedBySabtAhvalServiceQueryHandler : BaseQueryHandler<IsMatchedBySabtAhvalServiceQuery, ApiResult<SabtAhvalServiceViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public IsMatchedBySabtAhvalServiceQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(IsMatchedBySabtAhvalServiceQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis)
                || userRoles.Contains(RoleConstants.Customer);
        }

        protected async override Task<ApiResult<SabtAhvalServiceViewModel>> RunAsync(IsMatchedBySabtAhvalServiceQuery request, CancellationToken cancellationToken)
        {

            string mainUrl = _configuration.GetValue(typeof(string), "InternalGatewayUrl").ToString() + "ExternalServices/v1/";
            var queryParameters = new Dictionary<string, string>();
            queryParameters.Add("NationalNo", request.NationalNo);
            queryParameters.Add("BirthDate", request.BirthDate);
            queryParameters.Add("ClientId", "SSAR");
            var httpRequest = new HttpEndpointRequest(mainUrl + "SabtAhvalService", _userService.UserApplicationContext.Token, queryParameters);

            var result = await _httpEndPointCaller.CallGetApiAsync<SabtAhvalServiceViewModel>(httpRequest
              , cancellationToken);

            if (result.IsSuccess)
            {
                if (result.Data != null)
                {
                    if ((string.IsNullOrWhiteSpace(request.Name) && !string.IsNullOrWhiteSpace(result.Data.Name)) || (!string.IsNullOrWhiteSpace(request.Name) && string.IsNullOrWhiteSpace(result.Data.Name)) || request.Name.NormalizeTextChars(false) != result.Data.Name.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("نام شخص با ثبت احوال مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.Family) && !string.IsNullOrWhiteSpace(result.Data.Family)) || (!string.IsNullOrWhiteSpace(request.Family) && string.IsNullOrWhiteSpace(result.Data.Family)) || request.Family.NormalizeTextChars(false) != result.Data.Family.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("نام خانوادگی شخص با ثبت احوال مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.FatherName) && !string.IsNullOrWhiteSpace(result.Data.FatherName)) || (!string.IsNullOrWhiteSpace(request.FatherName) && string.IsNullOrWhiteSpace(result.Data.FatherName)) || request.FatherName.NormalizeTextChars(false) != result.Data.FatherName.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("نام پدر شخص با ثبت احوال مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.SexType) && !string.IsNullOrWhiteSpace(result.Data.SexType)) || (!string.IsNullOrWhiteSpace(request.SexType) && string.IsNullOrWhiteSpace(result.Data.SexType)) || request.SexType != result.Data.SexType)
                    {
                        result.IsSuccess = false;
                        result.message.Add("جنسیت شخص با ثبت احوال مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.IdentityNo) && !string.IsNullOrWhiteSpace(result.Data.ShenasnameNo)) || (!string.IsNullOrWhiteSpace(request.IdentityNo) && string.IsNullOrWhiteSpace(result.Data.ShenasnameNo)) || request.IdentityNo.NormalizeTextChars(false) != result.Data.ShenasnameNo.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("شماره شناسنامه شخص با ثبت احوال مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.Seri) && !string.IsNullOrWhiteSpace(result.Data.ShenasnameSeri)) || (!string.IsNullOrWhiteSpace(request.Seri) && string.IsNullOrWhiteSpace(result.Data.ShenasnameSeri)) || request.Seri.NormalizeTextChars(false) != result.Data.ShenasnameSeri.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("شماره سری شناسنامه شخص با ثبت احوال مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.Serial) && !string.IsNullOrWhiteSpace(result.Data.ShenasnameSerial)) || (!string.IsNullOrWhiteSpace(request.Serial) && string.IsNullOrWhiteSpace(result.Data.ShenasnameSerial)) || request.Serial.NormalizeTextChars(false) != result.Data.ShenasnameSerial.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("شماره سریال شناسنامه شخص با ثبت احوال مطابقت ندارد");
                    }
                    if ((string.IsNullOrWhiteSpace(request.SeriAlpha) && !string.IsNullOrWhiteSpace(result.Data.AlphabetSeri)) || (!string.IsNullOrWhiteSpace(request.SeriAlpha) && string.IsNullOrWhiteSpace(result.Data.AlphabetSeri)) || request.SeriAlpha.NormalizeTextChars(false) != result.Data.AlphabetSeri.NormalizeTextChars(false))
                    {
                        result.IsSuccess = false;
                        result.message.Add("بخش حروفی شماره سری شناسنامه شخص با ثبت احوال مطابقت ندارد");
                    }
                    if (result.IsSuccess)
                    {
                        var geolocation = await GetGeolocationByNationalityCode(request.NationalNo, cancellationToken);
                        if (geolocation != null)
                        {
                            if (geolocation.Geolocation != null)
                                result.Data.IssueGeolocationId = new List<string>() { geolocation.Geolocation.Id };
                            result.Data.IdentityIssueLocation2 = geolocation.SabtAhvalUnitName;
                        }
                    }
                    else
                        result.Data = null;
                }
                else
                {
                    result.IsSuccess = false;
                    result.message.Add("شخص در سامانه ثبت احوال یافت نشد");
                }
            }
            return result;
        }

        public async Task<GetGeolocationByNationalityCodeViewModel> GetGeolocationByNationalityCode(string nationalityCode, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(new GetGeolocationByNationalityCodeQuery(nationalityCode), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

    }
}


