using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.ENoteBook;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;
using Stimulsoft.Base.Gauge.GaugeGeoms;

namespace Notary.SSAA.BO.QueryHandler.Document
{
    public class GetElectronicBookDataToSignQueryHandler : BaseQueryHandler<GetElectronicBookDataToSignQuery, ApiResult<SignDataViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly EstateInquiryEngine _estateInquiryEngine;
        private readonly IDocumentElectronicBookRepository _documentElectronicBookRepository;
        private readonly ENoteBookServerController _eNoteBookServerController;
        private readonly SignProvider _signProvider;

        public GetElectronicBookDataToSignQueryHandler(IMediator mediator, IUserService userService,
            IDocumentRepository documentRepository,
            IDocumentElectronicBookRepository documentElectronicBookRepository,
            ENoteBookServerController eNoteBookServerController,
            EstateInquiryEngine estateInquiryEngine,
            SignProvider signProvider
            )
            : base(mediator, userService)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _documentElectronicBookRepository = documentElectronicBookRepository;
            _estateInquiryEngine = estateInquiryEngine;
            _eNoteBookServerController = eNoteBookServerController;
            _signProvider = signProvider;
        }
        protected override bool HasAccess(GetElectronicBookDataToSignQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SignDataViewModel>> RunAsync(
    GetElectronicBookDataToSignQuery request,
    CancellationToken cancellationToken)
        {
            ApiResult<SignDataViewModel> apiResult = new();

            var document = await _documentRepository.TableNoTracking
                .Include(t => t.DocumentType)
                .Where(t => t.Id == request.DocumentId.ToGuid())
                .Select(t => new
                {
                    t.DocumentType.IsSupportive,
                    t.RelatedDocumentIsInSsar,
                    t.NationalNo,
                    t.DocumentTypeId,
                    t.ScriptoriumId,
                    t.RelatedDocumentNo,
                    t.RelatedScriptoriumId,
                    t.DocumentDate,
                    t.RelatedDocumentTypeId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (document == null)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("سند مورد نظر یافت نشد.");
                return apiResult;
            }

            List<DocumentElectronicBook> documentElectronicBooks = new();

            var baseQuery = _documentElectronicBookRepository.TableNoTracking
                .Where(t => t.ScriptoriumId == document.ScriptoriumId);

            if (document.IsSupportive == YesNo.No.GetString())
            {
                documentElectronicBooks = await baseQuery
                    .Where(t => t.NationalNo == document.NationalNo &&
                                t.DocumentTypeId == document.DocumentTypeId)
                    .ToListAsync(cancellationToken);
            }
            else
            {
                if (document.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
                {
                    string messages = string.Empty;
                    (Domain.Entities.Document relatedDocument, messages) =
                        await _eNoteBookServerController.GetRelatedDocFromView(
                            document.RelatedDocumentNo,
                            document.RelatedScriptoriumId,
                            cancellationToken,
                            messages
                        );

                    if (relatedDocument != null)
                    {
                        var relatedQuery = _documentElectronicBookRepository.TableNoTracking
                            .Where(t => t.ScriptoriumId == relatedDocument.ScriptoriumId)
                            .Where(t =>
                                t.NationalNo == relatedDocument.NationalNo &&
                                t.ClassifyNo == relatedDocument.ClassifyNo &&
                                t.DocumentTypeId == relatedDocument.DocumentTypeId
                            );

                        documentElectronicBooks = await relatedQuery.ToListAsync(cancellationToken);
                    }
                }
                else
                {
                    // حالت سند غیر SSAR
                    documentElectronicBooks = await baseQuery
                        .Where(t =>
                            t.NationalNo == "-" &&
                            t.ClassifyNo.ToString() == document.RelatedDocumentNo &&
                            t.DocumentTypeId == document.RelatedDocumentTypeId &&
                            t.DocumentDate == document.DocumentDate
                        )
                        .ToListAsync(cancellationToken);
                }
            }

            var eNoteBookHashedData = _signProvider.ProvideENoteBookHashedDataWithReservedClassifyNo(documentElectronicBooks);

            apiResult.Data = new SignDataViewModel
            {
                SignDataList = new List<DataToSign>()
            };

            for (int i = 0; i < eNoteBookHashedData.Count; i++)
            {
                apiResult.Data.SignDataList.Add(new DataToSign
                {
                    MainObjectId = documentElectronicBooks.ElementAt(i).Id.ToString(),
                    Data = eNoteBookHashedData.ElementAt(i)
                });
            }

            apiResult.IsSuccess = true;
            return apiResult;
        }

    }
}
