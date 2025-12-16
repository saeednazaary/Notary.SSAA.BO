using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.DocumentStandardContract
{
    public class GetDocumentStandardContractSignDataQueryHandler : BaseQueryHandler<GetDocumentStandardContractSignDataQuery, ApiResult<SignDataViewModel>>
    {
        

        public GetDocumentStandardContractSignDataQueryHandler(IMediator mediator, IUserService userService)
            : base(mediator, userService)
        {
            
        }
        protected override bool HasAccess(GetDocumentStandardContractSignDataQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SignDataViewModel>> RunAsync(GetDocumentStandardContractSignDataQuery request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult<SignDataViewModel>() { IsSuccess=true, statusCode= ApiResultStatusCode.Success};
            apiResult.Data = new SignDataViewModel();
            apiResult.Data.SignDataList = new List<DataToSign>();

            var dataTosign = new DataToSign();
            dataTosign.MainObjectId = request.DocumentId;

            var input = new GetDataToSignQuery() { ConfigName = "DOCUMENT_DIGITAL_SIGNATURE_CONFIG", EntityId = request.DocumentId, FormName = "DOCUMENT_FORM" };
            var output = await _mediator.Send(input, cancellationToken);
            if (output.IsSuccess)
            {
                dataTosign.Data = output.Data.Data;                
                apiResult.Data.SignDataList.Add(dataTosign);
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.AddRange(output.message);
            }    
            
            return apiResult;
        }

    }
}
