using MediatR;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.DataTransferObject.ViewModels.RelatedDocument;
using Notary.SSAA.BO.DataTransferObject.Mappers.RelatedDocument;
using Notary.SSAA.BO.DataTransferObject.Queries.RelatedDocument;



namespace Notary.SSAA.BO.QueryHandler.RelatedDocument
{
    public class GetDocumentDetailByIdQueryHandler : BaseQueryHandler<GetDocumentDetailByIdQuery, ApiResult<DocumentDetailViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IRepository<Domain.Entities.DocumentType> _documenttypeRepository;

        public GetDocumentDetailByIdQueryHandler(IMediator mediator, IUserService userService, IDocumentRepository documentRepository, IRepository<Domain.Entities.DocumentType> documenttypeRepository) : base(mediator, userService)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _documenttypeRepository = documenttypeRepository ?? throw new ArgumentNullException(nameof(documenttypeRepository));
        }

        protected override bool HasAccess(GetDocumentDetailByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DocumentDetailViewModel>> RunAsync(GetDocumentDetailByIdQuery request, CancellationToken cancellationToken)
        {
            DocumentDetailViewModel result = new();
            ApiResult<DocumentDetailViewModel> apiResult = new();
            var document = await _documentRepository.GetDocumentInformation(request.DocumentId.ToGuid(), cancellationToken);
            if (document != null)
            {
                result = RelatedDocumentMapper.ToDocumentDetailViewModel(document);

                string[] idList = new string[] { document.ScriptoriumId };
                ScriptoriumInput scriptoriumInput = new ScriptoriumInput(idList);
                ApiResult<ScriptoriumViewModel> scriptoriumResponse = await _mediator.Send(scriptoriumInput, cancellationToken);

                if (scriptoriumResponse != null)
                {
                    result.RelatedScriptoriumName = scriptoriumResponse.Data.ScriptoriumList.First()?.Code + "-" + scriptoriumResponse.Data.ScriptoriumList.First()?.Name + "" + scriptoriumResponse.Data.ScriptoriumList.First()?.GeoLocationName.Replace("ء", "ی");
                }
                if (result.RelatedDocumentTypeId != null)
                {
                    if (result.RelatedDocumentTypeId.Count > 0)
                    {

                        result.RelatedDocumentTypeTitle = _documenttypeRepository.GetById(result.RelatedDocumentTypeId.First())?.Title;
                    }
                }
                apiResult.Data = result;
                apiResult.IsSuccess = true;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("سند مربوطه پیدا نشد");
            }
            return apiResult;
        }
    }
}
