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
    public interface IWorkfolwStateRepository:IRepository<WorkflowState>
    {
        Task<BaseLookupRepositoryObject> GetEstateInquiryStatusLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, CancellationToken cancellationToken);
        Task<string> GetEstateInquiryWorkflowStateId(string state, CancellationToken cancellationToken);
        Task<string> GetForestOrganizationInquiryWorkflowStateId(string state, CancellationToken cancellationToken);

        Task<BaseLookupRepositoryObject> GetDealSummaryStatusLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, bool isOrderBy, CancellationToken cancellationToken);
        Task<BaseLookupRepositoryObject> GetEstateDocumentRequestStatusLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, bool isOrderBy, CancellationToken cancellationToken);
        Task<BaseLookupRepositoryObject> GetEstateTaxInquiryStatusLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, CancellationToken cancellationToken);
    }
}
