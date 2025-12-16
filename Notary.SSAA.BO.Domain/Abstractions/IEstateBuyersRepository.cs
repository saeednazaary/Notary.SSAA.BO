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
    public interface IEstateBuyersRepository:IDapperRepository
    {
        Task<EstateBuyerRepositoryObject> GetEstateBuyersList(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, CancellationToken cancellationToken);
    }
}
