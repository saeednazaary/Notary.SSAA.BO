using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    public class SignRequestElectronicBookPageQueryHandler : BaseQueryHandler<SignRequestElectronicBookPageQuery, ApiResult<SignRequestElectronicBookPageViewModel>>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly ISignRequestFileRepository _signRequestFileRepository;
        public SignRequestElectronicBookPageQueryHandler(IMediator mediator, IUserService userService,ISignRequestRepository signRequestRepository, ISignRequestFileRepository signRequestFileRepository)
            : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _signRequestFileRepository = signRequestFileRepository ?? throw new ArgumentNullException(nameof(signRequestFileRepository));
        }
        protected override bool HasAccess(SignRequestElectronicBookPageQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SignRequestElectronicBookPageViewModel>> RunAsync(SignRequestElectronicBookPageQuery request, CancellationToken cancellationToken)
        {
            SignRequestElectronicBookPageViewModel result = new();
            ApiResult<SignRequestElectronicBookPageViewModel> apiResult = new();
            Domain.Entities.SignRequest signRequest;
            int ClassifyNo = 0;
            if (!string.IsNullOrEmpty( request.PersonSignClassifyNo))
            {
                ClassifyNo = Convert.ToInt32(request.PersonSignClassifyNo);
            }
                (signRequest, decimal FindPageNumber )= await _signRequestRepository.GetSignRequestBookPage(_userService.UserApplicationContext.BranchAccess.BranchCode,
                    request.PageNumber, request.SignRequestNationalNo, ClassifyNo, cancellationToken);

            if (signRequest != null)
            {
                result = SignRequestMapper.ToElectronicBookPageViewModel(signRequest);
                result.SignRequestElectronicBookPagePersons = result.SignRequestElectronicBookPagePersons.OrderBy(x => x.RowNo).ToList();
                result.PageNumber = FindPageNumber;
                SignRequestFile signRequestFile = await _signRequestFileRepository.GetSignRequestFile(signRequest.Id, cancellationToken);
                if (signRequestFile?.ScanFile == null)
                {
                    result.SignRequestImageFile = null;
                }
                else
                {
                    result.SignRequestImageFile = $"data:image/png;base64,{Convert.ToBase64String(signRequestFile.ScanFile)}";
                }

                apiResult.Data = result;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add(SystemMessagesConstant.Operation_Successful);
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("گواهی امضا مربوطه پیدا نشد");
            }
                return apiResult;
            }
        

    }
}
