using MediatR;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.DocumentStandardContract
{
    public class ValidatDocumentStandardContractQueryHandler : BaseQueryHandler<ValidatDocumentStandardContractQuery, ApiResult<ValidatDocumentStandardContractViewModel>>
    {
        private ValidatorGateway _validatorGateway;
        public ValidatDocumentStandardContractQueryHandler(IMediator mediator, IUserService userService, ValidatorGateway validatorGateway) : base(mediator, userService)
        {
            _validatorGateway = validatorGateway;
        }

        protected override bool HasAccess(ValidatDocumentStandardContractQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ValidatDocumentStandardContractViewModel>> RunAsync(ValidatDocumentStandardContractQuery request, CancellationToken cancellationToken)
        {
            string response = "";
            ApiResult<ValidatDocumentStandardContractViewModel> apiResult = new();
            ValidatDocumentStandardContractViewModel Result = new ValidatDocumentStandardContractViewModel();
            RelatedDocumentValidationRequest relatedDocumentValidationRequest = new RelatedDocumentValidationRequest();

            relatedDocumentValidationRequest.DocumentDate = request.DocumentDate;
            relatedDocumentValidationRequest.DocumentNationalNo = request.DocumentNationalNo;
            relatedDocumentValidationRequest.DocumentScriptoriumId = request.DocumentScriptoriumId != null ? request.DocumentScriptoriumId : null;
            relatedDocumentValidationRequest.DocumentTypeID = request.DocumentTypeId;
            relatedDocumentValidationRequest.IsRelatedDocumentInSSAR = request.IsRelatedDocumentInSSAR == true ? YesNo.Yes : YesNo.No;
            RelatedDocumentValidationResponse relatedDocumentValidationResponse = new RelatedDocumentValidationResponse();

            (relatedDocumentValidationResponse, response) = await _validatorGateway.ValidateRelatedDocument(relatedDocumentValidationRequest, response);
            if (relatedDocumentValidationResponse.ValidationResult)
            {
                Result.IsValidat = true;
                Result.DocumentId = relatedDocumentValidationResponse.registerServiceReqObjectID;
            }
            else
            {
                Result.IsValidat = false;
                if (relatedDocumentValidationResponse.ValidationResponseMessage.Contains("-"))
                {
                    relatedDocumentValidationResponse.ValidationResponseMessage.Split("-").ToList().ForEach(x =>
                    Result.ErrorMessages.Add(x)
                    );
                }
                else
                {
                    Result.ErrorMessages.Add(relatedDocumentValidationResponse.ValidationResponseMessage);
                }
            }
            apiResult.Data = Result;
            return apiResult;
        }
    }
}
