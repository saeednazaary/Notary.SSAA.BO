using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDealSummaryPersonRepository:IRepository<DealSummaryPerson>
    {
        Task<List<DealSummaryPerson>> GetEstateOwnersByDealSummaryInfo(string estateInquiryId, CancellationToken cancellationToken);
        Task<DealSummaryPersonGrid> GetDealSummaryPersonGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, Guid dealSummaryId, bool isOrderBy, CancellationToken cancellationToken);
    }
}
