using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISignRequestSubjectRepository : IRepository<SignRequestSubject>
    {
        Task<SignRequestSubjectLookupRepositoryObject> GetSignRequestSubjectLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);


    }
}
