using MediatR;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    public class SignRequestElectronicBookPageCountQueryHandler : BaseQueryHandler<SignRequestElectronicBookPageCountQuery, ApiResult<SignRequestElectronicBookPageCountViewModel>>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        public SignRequestElectronicBookPageCountQueryHandler(IMediator mediator, IUserService userService,ISignRequestRepository signRequestRepository)
            : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
        }
        protected override bool HasAccess(SignRequestElectronicBookPageCountQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SignRequestElectronicBookPageCountViewModel>> RunAsync(SignRequestElectronicBookPageCountQuery request, CancellationToken cancellationToken)
        {
            SignRequestElectronicBookPageCountViewModel result = new();
            ApiResult<SignRequestElectronicBookPageCountViewModel> apiResult = new();
            int SignElectronicBookCount = 0;
            SignElectronicBookCount = await _signRequestRepository.GetSignRequestElectronicBookTotalCount(_userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (SignElectronicBookCount >0)
            {
                result.SignRequestTotalCount = SignElectronicBookCount;
                apiResult.IsSuccess = true;
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess =true;
                apiResult.message.Add("گواهی امضایی پیدا نشد");
            }
                return apiResult;
            }
        

    }
}
