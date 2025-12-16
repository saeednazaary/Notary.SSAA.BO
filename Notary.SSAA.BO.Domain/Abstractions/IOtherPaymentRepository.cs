using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IOtherPaymentRepository : IRepository<OtherPayment>
    {
        Task<string> GetMaxNationalNo(string beginNationalNo, CancellationToken cancellationToken);
        Task<OtherPaymentGrid> GetOtherPaymentGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, List<SortData> gridSortInputDefaults, SortData gridSortInput, string GlobalSearch, IList<Guid> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
