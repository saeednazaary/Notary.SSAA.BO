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
    public class VerifySignListQueryHandler : BaseQueryHandler<VerifySignListQuery, ApiResult<List<VerifySignViewModel>>>
    {


        public VerifySignListQueryHandler(IMediator mediator, IUserService userService,
            IDigitalSignatureConfigurationRepository digitalSignatureConfigurationRepository, IServiceProvider serviceProvider)
            : base(mediator, userService)
        {

        }
        protected override bool HasAccess(VerifySignListQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<List<VerifySignViewModel>>> RunAsync(VerifySignListQuery request, CancellationToken cancellationToken)
        {

            ApiResult<List<VerifySignViewModel>> apiResult = new()
            {
                statusCode = ApiResultStatusCode.Success,
                IsSuccess = true,
                Data = new List<VerifySignViewModel>()
            };
            if (request.SignList == null || request.SignList.Count == 0)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("لیست امضاهای دیجیتال  خالی می باشد");
                return apiResult;
            }
            request.SetHandlerNames();
            foreach (VerifySignInfo x in request.SignList)
            {
                var input = new VerifyNewSignQuery() { HandlerName = x.HandlerName, HandlerInput = x.HandlerInput, Certificate = x.Certificate, Data = x.Data, MainObjectId = x.MainObjectId, SaveSignValue = x.SaveSignValue, Sign = x.Sign, SignDataQueryHandler = x.SignDataQueryHandler };
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
