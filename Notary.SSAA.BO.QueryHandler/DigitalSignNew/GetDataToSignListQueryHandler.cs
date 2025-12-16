using MediatR;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Newtonsoft.Json;
using System.Collections;


namespace Notary.SSAA.BO.QueryHandler.DigitalSignNew
{
    public class GetDataToSignListQueryHandler : BaseQueryHandler<GetSignDataListQuery, ApiResult<List<GetDataToSignViewModel2>>>
    {

       
        public GetDataToSignListQueryHandler(IMediator mediator, IUserService userService,
            IDigitalSignatureConfigurationRepository digitalSignatureConfigurationRepository, IServiceProvider serviceProvider)
            : base(mediator, userService)
        {

        }
        protected override bool HasAccess(GetSignDataListQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<List<GetDataToSignViewModel2>>> RunAsync(GetSignDataListQuery request, CancellationToken cancellationToken)
        {

            ApiResult<List<GetDataToSignViewModel2>> apiResult = new()
            {
                statusCode = ApiResultStatusCode.Success,
                IsSuccess = true,
                Data = new List<GetDataToSignViewModel2>()
            };
            if (request.HandlerInfo == null || request.HandlerInfo.Count == 0)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("لیست مشخصات دریافت کننده های داده ی  امضای دیجیتال خالی می باشد");
                return apiResult;
            }
            request.SetHandlerNames();
            foreach (var x in request.HandlerInfo)
            {
                var input = new GetDataToNewSignQuery() { HandlerName = x.HandlerName, HandlerInput = x.HandlerInput, SignDataQueryHandler = x.SignDataQueryHandler };
                var output = await _mediator.Send(input, cancellationToken);
                if (output.IsSuccess)
                {
                    apiResult.Data.Add(output.Data);
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.AddRange(output.message);
                }
            }
            
            return apiResult;
        }

    }
}
