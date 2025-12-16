using Mapster;
using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Circular;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Circular;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.ExternalServices.Circular
{
    internal sealed class CircularTypeServieLookupQueryHandler : BaseQueryHandler<CircularTypeLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;

        public CircularTypeServieLookupQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller) : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;

        }

        protected override bool HasAccess(CircularTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(CircularTypeLookupQuery request, CancellationToken cancellationToken)
        {
            var BOInput = request.Adapt<CircularTypeServiceInput>();
            var result = await _mediator.Send(BOInput, cancellationToken);

            if (result.IsSuccess)
            {
                return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result.Data, new List<string> { SystemMessagesConstant.Operation_Successful });
            }
            else
            {
                return new ApiResult<BaseLookupRepositoryObject>(false, ApiResultStatusCode.ServerError, null, new List<string> { "ارتباط  با سرویس سازمان ثبت برقرار نشد" });
            }

        }
    }
}
