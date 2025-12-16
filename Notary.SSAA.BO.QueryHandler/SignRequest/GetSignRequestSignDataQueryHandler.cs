using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.QueryHandler.DigitalSign;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Text;


namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    public class GetSignRequestSignDataQueryHandler : BaseQueryHandler<GetSignRequestSignDataQuery, ApiResult<SignDataViewModel>>
    {
        readonly IDigitalSignatureConfigurationRepository _digitalSignatureConfigurationRepository;
        readonly ISignRequestSemaphoreRepository _signRequestSemaphoreRepository;
        public GetSignRequestSignDataQueryHandler(IMediator mediator, IUserService userService, IDigitalSignatureConfigurationRepository digitalSignatureConfigurationRepository, ISignRequestSemaphoreRepository signRequestSemaphoreRepository)
            : base(mediator, userService)
        {
            _digitalSignatureConfigurationRepository = digitalSignatureConfigurationRepository;
            _signRequestSemaphoreRepository = signRequestSemaphoreRepository;
        }
        protected override bool HasAccess(GetSignRequestSignDataQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SignDataViewModel>> RunAsync(GetSignRequestSignDataQuery request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult<SignDataViewModel>() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };
            apiResult.Data = new SignDataViewModel();
            apiResult.Data.SignDataList = new List<DataToSign>();

            var dataTosign = new DataToSign();
            dataTosign.MainObjectId = request.SignRequestId;

            var input = new GetDataToSignQuery() { ConfigName = "SIGN_REQUEST_DIGITAL_SIGNATURE_CONFIG", EntityId = request.SignRequestId, FormName = "SIGN_REQUEST_FORM" };
            var output = await _mediator.Send(input, cancellationToken);
            if (output.IsSuccess)
            {
                dataTosign.Data = output.Data.Data;
                apiResult.Data.SignDataList.Add(dataTosign);

                var dataGraphSignHelper = new DataGraphSignHelper(this._mediator);
                dataGraphSignHelper.DigitalSignatureConfiguration = await _digitalSignatureConfigurationRepository.GetSignRequestElectronicBookConfiguration(cancellationToken);
                var provider = new Utilities.DigitalSign.FormSignInfoDataGraphProvider();
                var dataGrophInfo = provider.GetFormSignInfoDataGraph(dataGraphSignHelper.DigitalSignatureConfiguration.Descriptor);
                var bookList = await _signRequestSemaphoreRepository.GetSignElectronicBookForDigitalSign(request.SignRequestId.ToGuid(), cancellationToken);
                foreach (var book in bookList)
                {
                    var signData = await dataGraphSignHelper.GetDataSignText(book, dataGrophInfo, cancellationToken);                   
                    dataTosign = new();
                    dataTosign.MainObjectId = book.Id.ToString();

                    var byteList = new List<byte>();
                    byteList.AddRange(Encoding.UTF8.GetBytes(signData.StringData));
                    byte[] rawDataByte = byteList.ToArray();
                    dataTosign.Data = Convert.ToBase64String(rawDataByte);
                    
                    apiResult.Data.SignDataList.Add(dataTosign);
                }
            }
            else
            {
                apiResult.IsSuccess = false;
            }

            return apiResult;
        }

    }
}
