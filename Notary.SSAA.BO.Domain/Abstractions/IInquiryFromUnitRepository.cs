using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IInquiryFromUnitRepository : IRepository<InquiryFromUnit>
    {
        Task<InquiryFromUnitGrid> GetInquiryFromUnitGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
            IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, CancellationToken cancellationToken, bool isOrderBy = false);

        Task<string> GetMaxInquiryNo(  string beginNationalNo, CancellationToken cancellationToken);
    }
}
