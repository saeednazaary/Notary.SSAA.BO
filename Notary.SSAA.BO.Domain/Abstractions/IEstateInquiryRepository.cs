using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IEstateInquiryRepository : IRepository<EstateInquiry>
    {
        Task<string> GetMaxInquiryNo(string scriptorumId,string no, CancellationToken cancellationToken);
        Task<string> GetMaxInquiryNo2(string scriptorumId,string no, CancellationToken cancellationToken);
        Task<EstateInquiryGrid> GetEstateInquiryGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch,  string scriptoriumId, bool isOrderBy , string[] statusId, Dictionary<string, object> extraParam, CancellationToken cancellationToken);
        Task<bool> IsExistsInquiry(string inquiryDate, string inquiryNo, string inquiryId, string scriptoriumId, CancellationToken cancellationToken);
        Task<List<EstateInquirySpecialFields>> GetSimilarInquiryList(string scriptoriumId, string unitId, string pageNumber, string noteBookNo, string seriDaftariId, string melliCode, string basic, string secondary, string inquiryId, bool isExecuteTransfer, string name, string family, string docPrintNo, CancellationToken cancellationToken);
        
    }
}
