using Azure;
using MediatR;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.NotaryDocument;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;
using Stimulsoft.Base.Gauge.GaugeGeoms;

namespace Notary.SSAA.BO.QueryHandler.Document
{
    public class GetDSUDealSummariesDataToSignHandler : BaseQueryHandler<GetDSUDealSummariesDataQuery, ApiResult<SignDataViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly EstateInquiryEngine _estateInquiryEngine;



        public GetDSUDealSummariesDataToSignHandler ( IMediator mediator, IUserService userService,
            IDocumentRepository documentRepository,
            EstateInquiryEngine estateInquiryEngine
            )
            : base ( mediator, userService )
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException ( nameof ( documentRepository ) );
            _estateInquiryEngine = estateInquiryEngine;
        }
        protected override bool HasAccess ( GetDSUDealSummariesDataQuery request, IList<string> userRoles )
        {
            return userRoles.Contains ( RoleConstants.Sardaftar ) || userRoles.Contains ( RoleConstants.Daftaryar ) || userRoles.Contains ( RoleConstants.SanadNevis );
        }

        protected override async Task<ApiResult<SignDataViewModel>> RunAsync(GetDSUDealSummariesDataQuery request, CancellationToken cancellationToken)
        {
            SignDataViewModel signDataViewModel = new();
            DSUDealSummariesDataToSignViewModel result = new();
            ApiResult<SignDataViewModel> apiResult = new();
            Domain.Entities.Document document = await _documentRepository.GetDocumentById(request.DocumentId.ToGuid(), new List<string>() { "DocumentType", "DocumentPeople", "DocumentInquiries", "DocumentEstates" }, cancellationToken);
            string dsuDealSummaryMessage = null;



            if (document != null)
            {
                (List<DSUDealSummarySignPacket> SignPacketCollection, dsuDealSummaryMessage) = await _estateInquiryEngine.CollectDSUDealSummariesRawDataB64(document, dsuDealSummaryMessage, cancellationToken);

                if (SignPacketCollection != null && SignPacketCollection.Count > 0)
                {
                    int index = 1;
                    apiResult.Data = new();
                    apiResult.Data.SignDataList = new();
                    foreach (var item in SignPacketCollection)
                    {

                        apiResult.Data.SignDataList.Add(new DataToSign { Data = item.RawDataB64, MainObjectId = item.RegisterServiceReqObjectID + "_" + index });
                        index++;
                    }


                    //result.SignPacketCollection = SignPacketCollection;
                    //apiResult.Data = result;
                    apiResult.IsSuccess = true;


                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.BadRequest;
                    apiResult.message.Add(dsuDealSummaryMessage);
                }

            }
            else
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("سند مربوطه یافت نشد");
            }
            return apiResult;
        }

    }
}
