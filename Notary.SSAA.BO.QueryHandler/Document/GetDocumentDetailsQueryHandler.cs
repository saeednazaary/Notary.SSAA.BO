using Notary.SSAA.BO.Domain.RepositoryObjects.Document;

namespace Notary.SSAA.BO.QueryHandler.Document
{
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
    using Notary.SSAA.BO.DataTransferObject.Queries.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.QueryHandler.Base;
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Notary.SSAA.BO.Utilities.Extensions;

    using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ConfirmationAuthenticity;
    using Notary.SSAA.BO.SharedKernel.AppSettingModel;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;


    /// <summary>
    /// Defines the <see cref="GetDocumentDetailsQueryHandler" />
    /// </summary>
    public class GetDocumentDetailsQueryHandler
        : BaseQueryHandler<GetDetailsOfDocumentQuery, ApiResult<DocumentViewModel>>
    {
        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private readonly IDocumentRepository documentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDocumentInfoOtherRepository _documentInfoOtherRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDocumentDetailsQueryHandler"/> class.
        /// </summary>
        /// <param name="mediator">The mediator<see cref="IMediator"/></param>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        public GetDocumentDetailsQueryHandler (
            IMediator mediator,
            IUserService userService,
            IDocumentRepository _documentRepository,
            IDocumentTypeRepository documentTypeRepository,
            IDocumentInfoOtherRepository documentInfoOtherRepository,

            IConfiguration configuration,
            IHttpEndPointCaller httpEndPointCaller
        )
            : base ( mediator, userService )
        {
            documentRepository =
                _documentRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _documentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException ( nameof ( documentTypeRepository ) );
            _documentInfoOtherRepository = documentInfoOtherRepository;
        }

        /// <summary>
        /// The HasAccess
        /// </summary>
        /// <param name="request">The request<see cref="GetDetailsOfDocumentQuery"/></param>
        /// <param name="userRoles">The userRoles<see cref="IList{string}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool HasAccess (
            GetDetailsOfDocumentQuery request,
            IList<string> userRoles
        )
        {
            return userRoles.Contains ( RoleConstants.Sardaftar )
                || userRoles.Contains ( RoleConstants.Daftaryar )
                || userRoles.Contains ( RoleConstants.SanadNevis );
        }

        /// <summary>
        /// The RunAsync
        /// </summary>
        /// <param name="request">The request<see cref="GetDetailsOfDocumentQuery"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ApiResult{DocumentViewModel}}"/></returns>
        protected override async Task<ApiResult<DocumentViewModel>> RunAsync (
            GetDetailsOfDocumentQuery request,
            CancellationToken cancellationToken
        )
        {

            DocumentViewModel result = new();
            Notary.SSAA.BO.Domain.Entities.Document document=null;
            ApiResult<DocumentViewModel> apiResult = new();
            if ( !string.IsNullOrEmpty ( request.DetailName ) )
            {

                if ( request.DetailName == DocumentDatailName.DocumentPeople.GetString () )
                {
                    document = await documentRepository.GetDocumentById ( request.DocumentId.ToGuid (), new List<string> { "DocumentPeople" }, cancellationToken );
                    if ( document != null )
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel ( document );
                        DocumentInfoOtherObject documentInfoOtherObject = await _documentInfoOtherRepository
                            .GetDocumentInfoOtherInformation((Guid.Parse(result.RequestId)));
                        result.RequestType.AssetTypeId = documentInfoOtherObject.AssetTypeId;
                        result.RequestType.DocumentTypeSubjectId = documentInfoOtherObject.DocumentTypeSubjectId;
                        result.DocumentPeople = result.DocumentPeople.OrderBy ( x => x.RowNo ).ToList ();
                        apiResult.Data = result;
                    }
                }
                else
                if ( request.DetailName == DocumentDatailName.DocumentRelatedPeople.GetString () )

                {
                    document = await documentRepository.GetDocumentById ( request.DocumentId.ToGuid (), new List<string> { "DocumentRelatedPeople" }, cancellationToken );
                    if ( document != null )
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel ( document );

                        result.DocumentRelatedPeople = result.DocumentRelatedPeople.OrderBy ( x => x.RowNo ).ToList ();
                        DocumentInfoOtherObject documentInfoOtherObject = await _documentInfoOtherRepository
                            .GetDocumentInfoOtherInformation((Guid.Parse(result.RequestId)));
                        result.RequestType.AssetTypeId = documentInfoOtherObject.AssetTypeId;
                        result.RequestType.DocumentTypeSubjectId = documentInfoOtherObject.DocumentTypeSubjectId;
                        apiResult.Data = result;
                    }
                }
                else
                if ( request.DetailName == DocumentDatailName.DocumentCase.GetString () )
                {
                    document = await documentRepository.GetDocumentById ( request.DocumentId.ToGuid (), new List<string> { "DocumentCases" }, cancellationToken );
                    if ( document != null )
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel ( document );

                        result.DocumentCases = result.DocumentCases.OrderBy ( x => x.RowNo ).ToList ();
                        DocumentInfoOtherObject documentInfoOtherObject = await _documentInfoOtherRepository
                            .GetDocumentInfoOtherInformation((Guid.Parse(result.RequestId)));
                        result.RequestType.AssetTypeId = documentInfoOtherObject.AssetTypeId;
                        result.RequestType.DocumentTypeSubjectId = documentInfoOtherObject.DocumentTypeSubjectId;
                        apiResult.Data = result;
                    }
                }
                else
                if ( request.DetailName == DocumentDatailName.DocumentInfoOther.GetString () )
                {
                    document = await documentRepository.GetDocumentById ( request.DocumentId.ToGuid (), new List<string> { "DocumentInfoOther" }, cancellationToken );
                    if ( document != null )
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel ( document );
                        result.DocumentInfoOther.DocumentTypeTitle = document.DocumentTypeTitle;
                        if ( result.DocumentInfoText != null )
                        {
                            if ( result.DocumentInfoOther != null && !string.IsNullOrEmpty ( result.DocumentInfoText.DocumentDescription ) )
                            {
                                result.DocumentInfoOther.DocumentDescription = result.DocumentInfoText.DocumentDescription;

                            }
                        }
                        result.RequestType.AssetTypeId = (result.DocumentInfoOther.DocumentAssetTypeId!=null && result.DocumentInfoOther.DocumentAssetTypeId.Count>0)? result.DocumentInfoOther.DocumentAssetTypeId[0]:null;
                        result.RequestType.DocumentTypeSubjectId = (result.DocumentInfoOther.DocumentTypeSubjectId != null && result.DocumentInfoOther.DocumentTypeSubjectId.Count>0)? result.DocumentInfoOther.DocumentTypeSubjectId [ 0]:null;
                        result.DocumentInfoText = null;
                        apiResult.Data = result;
                    }
                }
                else
                if ( request.DetailName == DocumentDatailName.DocumentInfoText.GetString () )
                {
                    document = await documentRepository.GetDocumentById ( request.DocumentId.ToGuid (), new List<string> { "DocumentInfoText" }, cancellationToken );
                    if ( document != null )
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel ( document );
                        DocumentInfoOtherObject documentInfoOtherObject = await _documentInfoOtherRepository
                            .GetDocumentInfoOtherInformation((Guid.Parse(result.RequestId)));
                        result.RequestType.AssetTypeId = documentInfoOtherObject.AssetTypeId;
                        result.RequestType.DocumentTypeSubjectId = documentInfoOtherObject.DocumentTypeSubjectId;
                        apiResult.Data = result;
                    }
                }
                else
                if ( request.DetailName == DocumentDatailName.RelationDocument.GetString () )
                {
                    document = await documentRepository.GetDocumentById ( request.DocumentId.ToGuid (), new List<string> { "DocumentRelationDocuments" }, cancellationToken );
                    if ( document != null )
                    {
                        result = DocumentMapper.MapDocumentToDocumentViewModel ( document );

                        if ( result.DocumentRelations != null && result.DocumentRelations.Count> 0 )
                        {
                            IList<string> documentTypeIds = result.DocumentRelations.Select(p => p.RelatedDocumentTypeId.FirstOrDefault()).ToList();
                            string[] documentscriptoriumIds = result.DocumentRelations.Select(p => p.ScriptoriumId.FirstOrDefault()).ToArray();
                            GetScriptoriumByIdServiceInput scriptoriumQuery = new(documentscriptoriumIds);
                            IList<Domain.Entities.DocumentType> documentTypes = await _documentTypeRepository.GetDocumentTypes(documentTypeIds, cancellationToken);
                            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
                            var baseInfoUrl = _configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "api/v1/Specific/Ssar/";
                            IList<ScriptoriumData> scriptoriumData = null;
                            ApiResult<ScriptoriumViewModel> scriptoriumResult = new();
                            if ( scriptoriumQuery.IdList != null && scriptoriumQuery.IdList [ 0 ] != null )
                            {
                                scriptoriumResult = await _httpEndPointCaller.CallPostApiAsync<ScriptoriumViewModel, GetScriptoriumByIdServiceInput> (
                               new HttpEndpointRequest<GetScriptoriumByIdServiceInput> ( baseInfoUrl + ScriptoriumRequestConstant.Scriptorium,
                               _userService.UserApplicationContext.Token, scriptoriumQuery ), cancellationToken );

                                scriptoriumData = scriptoriumResult.Data.ScriptoriumList;

                            }

                            for (int i = 0; i < result.DocumentRelations.Count; i++)
                            {
                                var relation = result.DocumentRelations[i];

                                string sId = relation?.ScriptoriumId?.FirstOrDefault();
                                string dtypeId = relation?.RelatedDocumentTypeId?.FirstOrDefault();

                                if (scriptoriumData != null && !string.IsNullOrEmpty(sId))
                                {
                                    var scriptorium = scriptoriumData.FirstOrDefault(p => p.Id == sId);
                                    if (scriptorium != null)
                                    {
                                        relation.RelatedDocumentScriptoriumTitle = scriptorium.Name;
                                    }
                                }

                                if (documentTypes != null && !string.IsNullOrEmpty(dtypeId))
                                {
                                    var docType = documentTypes.FirstOrDefault(p => p.Id == dtypeId);
                                    if (docType != null)
                                    {
                                        relation.RelatedDocumentTypeTitle = docType.Title;
                                    }
                                }
                            }

                            result.DocumentRelations = result.DocumentRelations.OrderByDescending ( x => x.RequestDate ).OrderBy ( x => x.RequestRecordDate ).ToList ();
                        }
                        DocumentRelationViewModel documentRelationViewModel=new DocumentRelationViewModel();
                        documentRelationViewModel.IsDelete = false;
                        documentRelationViewModel.IsDirty = false;
                        documentRelationViewModel.IsValid = true;
                        documentRelationViewModel.IsNew = false;
                        documentRelationViewModel.RequestNo = document.RelatedDocumentNo;
                        documentRelationViewModel.RequestDate = document.RelatedDocumentDate;
                        documentRelationViewModel.RequestSecretCode = document.RelatedDocumentSecretCode;
                        documentRelationViewModel.IsRequestAbroad = document.IsRelatedDocAbroad.ToYesNo ();
                        documentRelationViewModel.IsRequestInSsar = document.RelatedDocumentIsInSsar.ToYesNo ();
                        documentRelationViewModel.RelatedDocumentScriptorium = document.RelatedDocumentScriptorium;
                        documentRelationViewModel.RelatedDocAbroadCountryId = document.RelatedDocAbroadCountryId == null ? new List<string> { } : new List<string> { document.RelatedDocAbroadCountryId.ToString () };
                        documentRelationViewModel.ScriptoriumId = document.RelatedScriptoriumId == null ? new List<string> { } : new List<string> { document.RelatedScriptoriumId };
                        documentRelationViewModel.RelatedDocumentTypeId = document.RelatedDocumentTypeId == null ? new List<string> { } : new List<string> { document.RelatedDocumentTypeId };
                        result.DocumentMainRelation = documentRelationViewModel;
                        DocumentInfoOtherObject documentInfoOtherObject = await _documentInfoOtherRepository
                            .GetDocumentInfoOtherInformation((Guid.Parse(result.RequestId)));
                        result.RequestType.AssetTypeId = documentInfoOtherObject.AssetTypeId;
                        result.RequestType.DocumentTypeSubjectId = documentInfoOtherObject.DocumentTypeSubjectId;
                        apiResult.Data = result;
                    }
                }
                else
                if ( request.DetailName == DocumentDatailName.DocumentInquirie.GetString () )
                {
                    document = await documentRepository.GetDocumentById ( request.DocumentId.ToGuid (), new List<string> { "DocumentInquiries" }, cancellationToken );
                   
                        if ( document != null )
                        {
                            result = DocumentMapper.MapDocumentToDocumentViewModel ( document );
                            result.DocumentInquiries = result.DocumentInquiries.ToList ();
                            DocumentInfoOtherObject documentInfoOtherObject = await _documentInfoOtherRepository
                                .GetDocumentInfoOtherInformation((Guid.Parse(result.RequestId)));
                            result.RequestType.AssetTypeId = documentInfoOtherObject.AssetTypeId;
                            result.RequestType.DocumentTypeSubjectId = documentInfoOtherObject.DocumentTypeSubjectId;
                        apiResult.Data = result;
                        }
                    
                }
              

            }

         
            if ( document == null )
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add ( "سند مربوطه یافت نشد" );
            }
          
            return apiResult;
        }
    }
}
