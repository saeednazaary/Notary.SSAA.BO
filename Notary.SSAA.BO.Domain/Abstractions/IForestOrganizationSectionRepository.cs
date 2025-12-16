using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IForestOrganizationSectionRepository:IRepository<ForestorgSection>
    {
        Task<BaseLookupRepositoryObject> GetForestOrganizationSectionItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch,  string cityId, bool isOrderBy, CancellationToken cancellationToken);
        
    }
}
