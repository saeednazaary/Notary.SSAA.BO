using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrConfig;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.SsrConfig
{
    public class GetSsrConfigByIdQueryHandler : BaseQueryHandler<GetSsrConfigByIdQuery, ApiResult<SsrConfigViewModel>>
    {
        private IRepository<Domain.Entities.SsrConfig> _ssrConfigRepository;


        public GetSsrConfigByIdQueryHandler(IMediator mediator, IUserService userService, IRepository<Domain.Entities.SsrConfig> ssrConfigRepository)
            : base(mediator, userService)
        {
            _ssrConfigRepository = ssrConfigRepository;

        }
        protected override bool HasAccess(GetSsrConfigByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.SSAAAdmin) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SsrConfigViewModel>> RunAsync(GetSsrConfigByIdQuery request, CancellationToken cancellationToken)
        {

            SsrConfigViewModel result = new();
            ApiResult<SsrConfigViewModel> apiResult = new();
            Domain.Entities.SsrConfig ssrConfig = await _ssrConfigRepository.GetAsync(s => s.Id == request.SsrConfigId.ToGuid(), cancellationToken);


            if (ssrConfig != null)
            {


                result = SsrConfigMapper.ToViewModel(ssrConfig);
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("اطلاعات پایه دفتر الكترونیك گواهی امضاء مربوطه یافت نشد");
            }
            return apiResult; ;
        }

    }
}
