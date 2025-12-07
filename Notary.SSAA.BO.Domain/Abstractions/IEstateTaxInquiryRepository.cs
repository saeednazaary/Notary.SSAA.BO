using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IEstateTaxInquiryRepository : IRepository<EstateTaxInquiry>
    {
        Task<string> GetMaxInquiryNo(string scriptorumId, string no, CancellationToken cancellationToken);
        Task<string> GetMaxInquiryNo2(string scriptorumId, string no, CancellationToken cancellationToken);
        Task<EstateTaxInquiryGrid> GetEstateTaxInquiryGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, Dictionary<string, object> extraParam, CancellationToken cancellationToken);
        Task<EstateTaxInquiry> GetEstateTaxInquiryById(string id, CancellationToken cancellationToken);
    }
}
