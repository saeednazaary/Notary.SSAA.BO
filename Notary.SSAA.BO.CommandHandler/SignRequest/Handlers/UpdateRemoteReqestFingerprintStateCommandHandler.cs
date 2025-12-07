using Mapster;
using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    internal class UpdateRemoteReqestFingerprintStateCommandHandler : BaseCommandHandler<UpdateRemoteRequestFingerprintStateCommand, ApiResult<SignRequestViewModel>>
    {
        private Domain.Entities.SignRequest masterEntity;
        private readonly ISignRequestRepository _signRequestRepository;
        private ApiResult<SignRequestViewModel> apiResult;

        public UpdateRemoteReqestFingerprintStateCommandHandler(IMediator mediator, IUserService userService, ILogger logger, ISignRequestRepository signRequestRepository)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
        }

        protected override async Task<ApiResult<SignRequestViewModel>> ExecuteAsync(UpdateRemoteRequestFingerprintStateCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _signRequestRepository.GetAsync(x => x.ReqNo == request.SignRequestNo, cancellationToken);
            await _signRequestRepository.LoadCollectionAsync(masterEntity, x => x.SignRequestPeople, cancellationToken);
            if (masterEntity is not null)
            {
                GetInquiryPersonFingerprintListQuery getInquiryPersonFingerprint = new(masterEntity.Id.ToString(), masterEntity.SignRequestPeople.Select(x => x.NationalNo).ToList());

                var foundPerson = masterEntity.SignRequestPeople.FirstOrDefault(x => x.NationalNo == request.PersonNationalNo);
                if (foundPerson != null) 
                {
                    foundPerson.IsFingerprintGotten = "1";
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.NotFound;
                    apiResult.message.Add("شخص مورد نظر یافت نشد .");
                }
                await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);
                ApiResult<SignRequestViewModel> getResponse = await _mediator.Send(new GetSignRequestByIdQuery(masterEntity.Id.ToString()), cancellationToken);
                if (getResponse.IsSuccess)
                {

                    apiResult.Data = getResponse.Data.Adapt<SignRequestViewModel>();
                    _ = apiResult.Data.SignRequestPersons.OrderBy(x => x.RowNo);

                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = getResponse.statusCode;
                    apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");


                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("گواهی امضا مربوطه یافت نشد.");
                apiResult.statusCode = ApiResultStatusCode.NotFound;
            }

            return apiResult;

        }

        protected override bool HasAccess(UpdateRemoteRequestFingerprintStateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
