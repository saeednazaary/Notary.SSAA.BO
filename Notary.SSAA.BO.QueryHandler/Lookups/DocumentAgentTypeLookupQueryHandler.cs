using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Security.Cryptography.X509Certificates;

namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    public sealed class DocumentAgentTypeLookupQueryHandler : BaseQueryHandler<DocumentAgentTypeLookupQuery, ApiResult<DocumentAgentTypeLookupRepositoryObject>>
    {
        private readonly IDocumentAgentTypeRepository _documentAgentTypeRepository;
        private readonly IDocumentPersonRepository _documentPersonRepository;
        private readonly IDocumentRepository _documentRepository;
        public DocumentAgentTypeLookupQueryHandler(IMediator mediator, IUserService userService,
            IDocumentAgentTypeRepository documentTypeRepository, IDocumentPersonRepository documentPersonRepository, IDocumentRepository documentRepository)
            : base(mediator, userService)
        {
            _documentAgentTypeRepository = documentTypeRepository ?? throw new ArgumentNullException(nameof(documentTypeRepository));
            _documentPersonRepository = documentPersonRepository ?? throw new ArgumentNullException(nameof(documentPersonRepository));
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));


        }

        protected override bool HasAccess(DocumentAgentTypeLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<DocumentAgentTypeLookupRepositoryObject>> RunAsync(DocumentAgentTypeLookupQuery request, CancellationToken cancellationToken)
        {
            DocumentAgentTypeLookupRepositoryObject result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new SortData();
            List<string> FieldsNotInFilterSearch = new List<string>()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
            };
            isOrderBy = true;
            if (request.GridSortInput.Count > 0)
            {
                foreach (var item in request.GridSortInput)
                {
                    gridSortInput.Sort = item.Sort;
                    gridSortInput.SortType = item.SortType;
                }
            }
            else
            {
                gridSortInput.Sort = "code";
                gridSortInput.SortType = "asc";
            }
            List<string> CodesNotInAgetType = new List<string>();
            bool extraParamsValidationFlag = true;
            if (request.ExtraParams is not null)
            {
                if (!string.IsNullOrEmpty(request.ExtraParams.MainPersonId) && !string.IsNullOrEmpty(request.ExtraParams.DocumentId) && !string.IsNullOrEmpty(request.ExtraParams.RelatedAgentPersonId))
                {

                    var mainperson = _documentPersonRepository.GetById(request.ExtraParams.MainPersonId.ToGuid());
                    var relatedAgentPerson = _documentPersonRepository.GetById(request.ExtraParams.RelatedAgentPersonId.ToGuid());
                    var document = _documentRepository.GetById(request.ExtraParams.DocumentId.ToGuid());
                    if (mainperson != null && relatedAgentPerson != null && document!=null)
                    {
                        await _documentRepository.LoadReferenceAsync(document, e => e.DocumentType, cancellationToken);
                        await _documentRepository.LoadCollectionAsync(document, e => e.DocumentPeople, cancellationToken);
                        extraParamsValidationFlag = false;
                        if (!(relatedAgentPerson.IsOriginal == "2" && relatedAgentPerson.IsRelated == "1" && relatedAgentPerson.IsAlive=="2"/* && relatedAgentPerson.IsIranian=="1" && mainperson.IsIranian=="1"*/ && mainperson.IsOriginal== "1" && mainperson.IsRelated == "2"))
                        {
                            CodesNotInAgetType.Add("09");
                            CodesNotInAgetType.Add("13");
                        }
                        if (!(document.DocumentType.Code == "009" && relatedAgentPerson.PersonType == "1" && document.DocumentPeople.Any(p=>p.PersonType=="2")))
                        {
                            CodesNotInAgetType.Add("18");
                        }

                    }
                }

            }
            if (extraParamsValidationFlag)
            {
                CodesNotInAgetType.Add("9");
                CodesNotInAgetType.Add("13");
                CodesNotInAgetType.Add("18");
            }
            result = await _documentAgentTypeRepository.GetDocumentAgentTypeLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, CodesNotInAgetType, cancellationToken, isOrderBy);

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }

            return new ApiResult<DocumentAgentTypeLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
