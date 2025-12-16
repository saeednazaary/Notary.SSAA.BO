using Mapster;
using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;

namespace Notary.SSAA.BO.CommandHandler.DocumentStandardContract.Handlers
{
    internal class UpdateDocumentStandardContractFingerprintStateCommandHandler : BaseCommandHandler<UpdateDocumentStandardContractFingerprintStateCommand, ApiResult<DocumentStandardContractViewModel>>
    {
        private Domain.Entities.Document masterEntity;
        private readonly IDocumentRepository _DocumentRepository;
        private ApiResult<DocumentStandardContractViewModel> apiResult;

        public UpdateDocumentStandardContractFingerprintStateCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IDocumentRepository DocumentRepository)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _DocumentRepository = DocumentRepository ?? throw new ArgumentNullException(nameof(DocumentRepository));
        }

        protected override async Task<ApiResult<DocumentStandardContractViewModel>> ExecuteAsync(UpdateDocumentStandardContractFingerprintStateCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _DocumentRepository.GetByIdAsync(cancellationToken, request.DocumentId.ToGuid());
            await _DocumentRepository.LoadCollectionAsync(masterEntity, x => x.DocumentPeople, cancellationToken);
            if (masterEntity is not null)
            {
                GetInquiryPersonFingerprintListQuery getInquiryPersonFingerprint = new(masterEntity.Id.ToString(), masterEntity.DocumentPeople.Select(x => x.NationalNo).ToList());

                var inquiryPersonFingerprintResult = await _mediator.Send(getInquiryPersonFingerprint, cancellationToken);
                if (inquiryPersonFingerprintResult.IsSuccess)
                {
                    foreach (DocumentPerson item in masterEntity.DocumentPeople)
                    {

                        var foundPerson = inquiryPersonFingerprintResult.Data.FirstOrDefault(x => x.PersonObjectId == item.Id.ToString());
                        if (foundPerson is not null)
                        {
                            item.TfaState = foundPerson.TFAState;
                            item.MocState = foundPerson.MOCState;
                            item.IsFingerprintGotten = foundPerson.IsFingerprintGotten.ToYesNo();
                        }

                    }
                }

                await _DocumentRepository.UpdateAsync(masterEntity, cancellationToken);
                ApiResult<DocumentStandardContractViewModel> getResponse = await _mediator.Send(new GetDocumentStandardContractByIdQuery(masterEntity.Id.ToString()), cancellationToken);
                if (getResponse.IsSuccess)
                {

                    apiResult.Data = getResponse.Data.Adapt<DocumentStandardContractViewModel>();
                    _ = apiResult.Data.DocumentPeople.OrderBy(x => x.RowNo);

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
                apiResult.message.Add("سند مربوطه یافت نشد.");
                apiResult.statusCode = ApiResultStatusCode.NotFound;
            }

            return apiResult;

        }

        protected override bool HasAccess(UpdateDocumentStandardContractFingerprintStateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
