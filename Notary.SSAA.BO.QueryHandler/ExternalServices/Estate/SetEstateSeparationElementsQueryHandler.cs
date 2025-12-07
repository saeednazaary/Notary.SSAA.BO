using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.ExternalServices.Estate
{
    public class SetEstateSeparationElementsQueryHandler : BaseQueryHandler<SetEstateSeparationElementsOwnershipQuery, ApiResult>
    {
        
        public SetEstateSeparationElementsQueryHandler(IMediator mediator, IUserService userService)
            : base(mediator, userService)
        {

        }
        protected override bool HasAccess(SetEstateSeparationElementsOwnershipQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult> RunAsync(SetEstateSeparationElementsOwnershipQuery request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult() { IsSuccess = false, statusCode = ApiResultStatusCode.Success };


            var input = new SetEstateSeparationElementsOwnershipInput() { SetEstateSeparationElementsOwnershipQuery = request };
            var output = await _mediator.Send(input, cancellationToken);
            if (output.IsSuccess)
            {
                if (output.Data != null && output.Data.Result)
                    apiResult.IsSuccess = true;
                else
                {
                    apiResult.IsSuccess = false;
                    if (output.Data != null && !string.IsNullOrWhiteSpace(output.Data.ErrorMessage))
                        apiResult.message.Add(output.Data.ErrorMessage);
                    else
                        apiResult.message.Add("خطا در ارسال اطلاعات تقسیم نامه به سامانه املاک رخ داده است");
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                if (output.message.Count > 0)
                    apiResult.message.AddRange(output.message);
                else
                    apiResult.message.Add("خطا در ارسال اطلاعات تقسیم نامه به سامانه املاک رخ داده است");
            }

            return apiResult;
        }
    }
}
