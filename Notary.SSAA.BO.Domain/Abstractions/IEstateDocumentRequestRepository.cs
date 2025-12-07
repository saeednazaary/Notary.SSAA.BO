using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IEstateDocumentRequestRepository : IRepository<EstateDocumentRequest>
    {
        Task<string> GetMaxNo(string scriptorumId,string no, CancellationToken cancellationToken);
        Task<List<EstateDocumentRequestSpecialFields>> GetSimilarRequestList(string scriptorumId, string personNationalNo, string estateUnitId, string estateBasic, bool estateBasicIsRemaining, string estateSecondary, bool estateSecondaryIsRemaining, string estateSectionId, string estateSubSectionId, string currentRequestId, string rejectedRequestId, CancellationToken cancellationToken);
        Task<EstateDocumentRequestGrid> GetEstateDocumentRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, string[] statusId, Dictionary<string, object> extraParam, CancellationToken cancellationToken);
        Task<EstateDocumentRequestGrid> GetRejectedEstateDocumentRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, CancellationToken cancellationToken);
        Task<EstateDocumentRequestGrid> GetCanceldEstateDocumentRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, CancellationToken cancellationToken);
        Task<EstateDocumentRequest> GetEstateDocumentRequestById(string estateDocumentRequestId, CancellationToken cancellationToken);
    }
}
