using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.ServiceHandler.ExternalService.Estate
{
    public class GetTehranTaxBlockRadifServiceHandler : BaseServiceHandler<GetTehranTaxBlockRadifInput, ApiResult<EstateTaxInquiryServiceBlockRadifObject>>
    {
        private readonly IEstateTaxInquiryRepository _estatetaxInquiryRepository;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetTehranTaxBlockRadifServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IEstateTaxInquiryRepository estateTaxInquiryRepository, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService, logger)
        {

            _estatetaxInquiryRepository = estateTaxInquiryRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(GetTehranTaxBlockRadifInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<EstateTaxInquiryServiceBlockRadifObject>> ExecuteAsync(GetTehranTaxBlockRadifInput request, CancellationToken cancellationToken)
        {

            ApiResult<EstateTaxInquiryServiceBlockRadifObject> apiResult = new();
            request.ServiceID = "473514";
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/";
            var serviceUrl = mainUrl + "EstateService/GetTehranTaxBlockRadif";
            var result = await _httpEndPointCaller.CallPostApiAsync<EstateTaxInquiryServiceBlockRadifObject, GetTehranTaxBlockRadifInput>(
            new HttpEndpointRequest<GetTehranTaxBlockRadifInput>(serviceUrl, _userService.UserApplicationContext.Token, request), cancellationToken);
            if (result.IsSuccess)
            {

                apiResult.Data = result.Data;
            }
            else
            {

                apiResult.Data = new EstateTaxInquiryServiceBlockRadifObject() { ErrorID = -1, ErrorMessage = "خطا در اجرای سرویس رخ داده است" };
            }

            return apiResult;
        }

    }
}
