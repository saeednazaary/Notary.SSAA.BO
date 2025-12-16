using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using System.Threading;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IForestOrganizationInquiryRepository : IRepository<ForestorgInquiry>
    {
        Task<string> GetMaxForestOrganizationInquiryNo(string scriptorumId,string no, CancellationToken cancellationToken);
        Task<string> GetMaxForestOrganizationInquiryUniqueNo(string scriptorumId, string no, CancellationToken cancellationToken);
        Task<ForestOrganizationInquiryGrid> GetForestOrganizationInquiryGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy , CancellationToken cancellationToken);
        Task<ForestorgInquiry> GetForestOrganizationInquiryById(string forestOrganizationInquiryId,CancellationToken cancellationToken);
    }
}
