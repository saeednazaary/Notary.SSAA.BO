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
    public class GetSignValueListQueryHandler : BaseQueryHandler<GetSignValueListQuery, ApiResult<List<GetSignValueViewModel>>>
    {

       
        public GetSignValueListQueryHandler(IMediator mediator, IUserService userService,
            IDigitalSignatureConfigurationRepository digitalSignatureConfigurationRepository, IServiceProvider serviceProvider)
            : base(mediator, userService)
        {

        }
        protected override bool HasAccess(GetSignValueListQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<List<GetSignValueViewModel>>> RunAsync(GetSignValueListQuery request, CancellationToken cancellationToken)
        {

            ApiResult<List<GetSignValueViewModel>> apiResult = new()
            {
                statusCode = ApiResultStatusCode.Success,
                IsSuccess = true,
                Data = new List<GetSignValueViewModel>()
            };
            if (request.IdList == null || request.IdList.Count == 0)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("لیست شناسه های امضای دیجیتال خالی می باشد");
                return apiResult;
            }

            foreach (var x in request.IdList)
            {
                var input = new GetSignValueQuery() { Id = x };
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
