using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDealSummaryRepository:IRepository<DealSummary>
    {
        Task<bool> IsAttachedToDealSummary(string inquiryId,CancellationToken cancellationToken);
        Task<DealSummaryGrid> GetDealSummaryGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, string[] statusId, Dictionary<string, object> extraParam, CancellationToken cancellationToken);
        Task<DealSummary> GetDealSummaryById(string dealSummaryId, CancellationToken cancellationToken);
        Task<DealSummary> GetDealSummaryByLegacyId(string legacyId, CancellationToken cancellationToken);
        Task<RestrictionDealSummaryListViewModel> GetRestrictedDealSummaryList(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, string[] statusId, List<string> estateInquiryIdList, List<string> dealSummaryIdList, string documentClassifyNo, string documentSignDate, CancellationToken cancellationToken);
        Task<string> GetMaxInquiryNo(string scriptorumId, string no, CancellationToken cancellationToken);
    }
}
