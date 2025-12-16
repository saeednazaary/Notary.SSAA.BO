using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequestReports;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISignRequestRepository : IRepository<SignRequest>
    {
        Task<string> GetMaxReqNo(string beginReqNo, CancellationToken cancellationToken);
        Task<SignRequestGrid> GetSignRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
            IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SignRequestSearchExtraParams extraParams,
            CancellationToken cancellationToken, bool isOrderBy = false);
        Task<SignRequestGrid> GetSignRequestAdminGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
            IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SignRequestAdminSearchExtraParams extraParams,
            CancellationToken cancellationToken, bool isOrderBy = false);
        Task<ICollection<SignRequestPerson>> GetSignRequestPersonFingerprint(Guid signRequestId, CancellationToken cancellationToken);
        Task<SignRequest> GetSignRequest(Guid signRequestId,string ScriptoriumId, CancellationToken cancellationToken);
        Task<List<SignRequestAffidavitRepositoryObject>> SignRequestAffidavit(string signRequestNationalNo,string signRequestSecretCode,string ScriptoriumId, List<string> NotInScriptoriumId, CancellationToken cancellationToken);
        Task<List<SignRequestVerificationWithImportantAnnexRepositoryObject>> SignRequestVerificationWithImportantAnnex(string signRequestNationalNo,string signRequestSecretCode,string ScriptoriumId,List<string> NotInScriptoriumId, CancellationToken cancellationToken);
        Task<(SignRequest, decimal)> GetSignRequestBookPage(string scriptoriumId, int PageNumber, string NationalNo, int? PersonSignClassifyNo, CancellationToken cancellationToken);
        Task<int> GetSignRequestElectronicBookTotalCount(string scriptoriumId, CancellationToken cancellationToken);
        Task<SignRequest> GetSignRequestPersons(Guid signRequestId, CancellationToken cancellationToken);
        Task<SignRequest> SignRequestTracking(Guid signRequestId,string scriptoriumId, CancellationToken cancellationToken);
        Task<string> GetMaxNationalNo(string beginNationalNo, CancellationToken cancellationToken);        
        Task<SignRequest> SignRequestTracking(string requestNo, CancellationToken cancellationToken);
        Task<SignRequest> ConfirmSignRequest(SignRequest source, List<Domain.Entities.SignElectronicBook> signElectronicBooks, CancellationToken cancellationToken);
        Task<KatebSignRequestGrid> GetKatebSignRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridFilterInput, string globalSearch, SortData gridSortInput, IList<string> selectedItems, List<string> fieldsNotInFilterSearch, string branchCode, KatebSignRequestSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy);
        Task<SignRequest> GetKatebSignRequesById(string id, CancellationToken cancellationToken);
        Task<List<SignRequestStatisticRepositoryObject>> generateSignRequsetStatisticReport(string fromDate, string toDate, IList<string> getter, IList<string> subjects, string branchCode, CancellationToken cancellationToken);
        Task<List<SignRequestFingerPrintRepositoryObject>> getFingerPrintReport(Guid signRequestId, CancellationToken cancellationToken);
    }
}
