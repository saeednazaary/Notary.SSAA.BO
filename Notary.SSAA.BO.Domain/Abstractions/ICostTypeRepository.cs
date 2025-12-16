using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ICostTypeRepository : IRepository<CostType>
    {
        Task<BaseLookupRepositoryObject> GetCostTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch,DocumentCostTypeLookupExtraParams documentCostTypeLookupExtraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<CostType> GetIdByCode(string code);
    }
}
