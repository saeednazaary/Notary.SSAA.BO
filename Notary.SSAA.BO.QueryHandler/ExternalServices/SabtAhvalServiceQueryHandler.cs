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

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class SabtAhvalServiceQueryHandler : BaseQueryHandler<SabtAhvalServiceQuery, ApiResult<SabtAhvalServiceViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public SabtAhvalServiceQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(SabtAhvalServiceQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis)
                || userRoles.Contains(RoleConstants.Customer);
        }

        protected async override Task<ApiResult<SabtAhvalServiceViewModel>> RunAsync(SabtAhvalServiceQuery request, CancellationToken cancellationToken)
        {

            string mainUrl = _configuration.GetValue(typeof(string), "InternalGatewayUrl").ToString() + "ExternalServices/v1/";
            var queryParameters = new Dictionary<string, string>();
            queryParameters.Add("NationalNo", request.NationalNo);
            queryParameters.Add("BirthDate", request.BirthDate);
            queryParameters.Add("ClientId", request.ClientId);
            var httpRequest = new HttpEndpointRequest(mainUrl + "SabtAhvalService", _userService.UserApplicationContext.Token, queryParameters);

            var result = await _httpEndPointCaller.CallGetApiAsync<SabtAhvalServiceViewModel>(httpRequest
              , cancellationToken);

            if (result.IsSuccess)
            {
                if (result.Data != null)
                {
                    var geolocation = await GetGeolocationByNationalityCode(request.NationalNo, cancellationToken);
                    if (geolocation != null)
                    {
                        if (geolocation.Geolocation != null)
                            result.Data.IssueGeolocationId = new List<string>() { geolocation.Geolocation.Id };
                        result.Data.IdentityIssueLocation2 = geolocation.SabtAhvalUnitName;
                    }
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


