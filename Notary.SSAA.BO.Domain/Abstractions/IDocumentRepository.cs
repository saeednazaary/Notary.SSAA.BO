using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
using Stimulsoft.Report;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<DocumentGrid> GetDocumentGridItems(
            int pageIndex,
            int pageSize,
            ICollection<SearchData> GridSearchInput,
            string GlobalSearch,
            SortData gridSortInput,
            IList<Guid> selectedItemsIds,
            List<string> FieldsNotInFilterSearch,
            string scriptoriumId,
            DocumentExtraParams extraParams,
            CancellationToken cancellationToken,
            bool isOrderBy = false
        );

        public void ClearTracking();

        Task<List<DocumentSelselehAyadiRepositoryObject>> GetSelselehAyadi(string DocumentEstateSubSectionId, string DocumentEstateSectionId, string DocumentUnitId, string SecondaryPlaque, string BasicPlaque, IList<string> ExcludedScriptoriumIds, CancellationToken cancellationToken);
        Task<Document> GetDocument(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentModify(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocument(string documentRequestNo, CancellationToken cancellationToken);
        Task<Document> GetDocumentRelatedPeople(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentPeople(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentRelations(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentEstates(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentSms(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentVehicles(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentInfoOther(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentInfoTextById(string documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentInfoConfirmById(string documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentCostQuestionById(string documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentInfoJudgmentById(string documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentCases(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentCostsAndCostUnchange(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentRegisterReqPrices(Guid documentId, CancellationToken cancellationToken);
        Task<string> GetMaxDocNo(string beginReqNo, CancellationToken cancellationToken);
        Task<Document> GetDocumentForConfirmationOfAuthenticity(string NationalCode, int SecretCode, CancellationToken cancellationToken);
        Task<DocumentLookupRepositoryObject> GetDocumentLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
SortData gridSortInput, IList<string> selectedItemsIds, string ScriptoriumId, string ScriptoriumTitle, List<string> FieldsNotInFilterSearch, DocumentSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<OldExecutiveDocumentLookupRepositoryObject> GetOldExecutiveDocumentLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
SortData gridSortInput, IList<string> selectedItemsIds, string ScriptoriumId, List<string> FieldsNotInFilterSearch, OldExecutiveDocumentSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<Document> GetDocumentInformation(Guid documentId, CancellationToken cancellationToken);
        Task<(Document, int)> GetDocumentBook(string scriptorumId, int PageNumber, string NationalNo, int? PersonSignClassifyNo, CancellationToken cancellationToken);
        Task<int> GetDocumentElectronicBookTotalCount(string scriptorumId, CancellationToken cancellationToken);
        Task<Document> GetDocumentInquiries(Guid documentId, CancellationToken cancellationToken);
        Task<Document> GetDocumentPayments(Guid documentId, CancellationToken cancellationToken);
        Task<IEnumerable<Document>> GetDocumentByNoPrint(string classifyNo, string relatedDocumentNo, string scriptoriumId, CancellationToken cancellationToken);
        Task<Document> GetDocumentPrintRelatedPeople(Guid guid, CancellationToken cancellationToken);
        Task<Document> GetDocumentById(Guid id, List<string> details, CancellationToken cancellationToken);
        Task<int?> GetClassifyNoDocument(string nationalNo, string scriptoriumId, CancellationToken cancellationToken);
        Task<int> GetLastClassifyNoFromDocs( string scriptoriumId, CancellationToken cancellationToken); 
        Task<List<Document>> GetDocuments(List<string> details, CancellationToken cancellationToken
             , string id = null, string state = null,
             string relatedScriptoriumId = null, string[] relatedDocumentNo = null,
             string[] documentTypes = null, string relatedDocumentId = null
             , List<string> nationalNoes = null, List<AgentParam> agentParams = null, string scriptoriumId = null);

        Task RemoveVehicleQuotaDetails(List<Guid> ids);
        Task RemoveVehicleQuota(List<Guid> ids);
        Task RemoveEstateQuotaDetails(List<Guid> ids);
        Task RemoveEstateQuota(List<Guid> ids);
        Task RemoveEstateAttchments(List<Guid> ids);
        Task<RequsetType> GetRequestType(CancellationToken cancellationToken, string documentId = null, string nationalNo = null);
        Task<List<RequsetType>> GetRequestTypes(string state, string scriptoriumID, List<string> relatedDocumentNoes, CancellationToken cancellationToken);
        Task<Document> GetDocumentByNationalNo(List<string> details, string nationalNo, CancellationToken cancellationToken);


        Task<List<DocumentPerson>> GetAnnotationsForOnePerson(string natonalno, string personId, string documentId);
        Task<DocumentType> GetDocumentTypeById(string documentTypeID, CancellationToken cancellationToken);
        Task<DocumentLimit> GetDocumentLimitation(string nationalNo, CancellationToken cancellationToken);
        Task<string> GetMaxDocumentNationalNo(string beginNationalNo, CancellationToken cancellationToken);
        Task RemoveEstateOwnerShips(List<Guid> ids);
        Task RemoveEstateSeparations(List<Guid> ids);

        Task<List<Document>> FindRelatedDeterministicRegServices(string scriptoriumId,
            List<string> eSTEStateIdCollection, List<string> deterministicDocumentTypeCodes, Guid documentId, CancellationToken cancellationToken);

        Task<int?> GetMaxClassifyNo(string scriptoriumId, string documentTypeId, CancellationToken cancellationToken);
        Task<bool> ValidateCurrentClassifyNo(int classifyNo, string scriptoriumId, CancellationToken cancellationToken);

        //Task<int?> MaxClassifyNo ( string scriptoriumId, string documentTypeId, CancellationToken cancellationToken );
        Task<Document> GetDocumentForDigitalSign(Guid documentId, CancellationToken cancellationToken);

    }
}
