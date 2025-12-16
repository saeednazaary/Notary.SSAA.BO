using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DocumentPaymentRepository : Repository<DocumentPayment>, IReusedDocumentPaymentRepository
    {
        public DocumentPaymentRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<ReusedDocumentPaymentLookupRepositoryObject> GetReusedDocumentPaymentLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<Guid> selectedItemsIds, List<string> FieldsNotInFilterSearch, ReusedDocumentPaymentLookupExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            ReusedDocumentPaymentLookupRepositoryObject result = new();
            IQueryable<DocumentPayment> initialQuery = TableNoTracking.Include(x=>x.Document);

            if (extraParams is not null)
            {

                if (!extraParams.DocumentId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.DocumentId.ToString() == extraParams.DocumentId);

                if (!extraParams.CostTypeId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.CostTypeId == extraParams.CostTypeId);

                if (!extraParams.PaymentDate.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => string.Compare(x.PaymentDate, extraParams.PaymentDate) >= 0);

                if (!extraParams.PaymentTime.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => string.Compare(x.PaymentTime, extraParams.PaymentTime) >= 0);


            }

            var query = initialQuery.Select(y => new ReusedDocumentPaymentLookupItem()
            {
                Id = y.Id,
                DocumentId = y.DocumentId.ToString(),
                DocumentNo = y.Document.RequestNo,
                CostTypeId = y.CostTypeId,
                CostTypeTitle = y.CostType.Title,
                No = y.No,
                Price = y.Price,
                HowToQuotation = y.HowToQuotation,
                HowToPay = y.HowToPay,
                PaymentNo = y.PaymentNo,
                PaymentDate = y.PaymentDate,
                PaymentTime = y.PaymentTime,
                PaymentType = y.PaymentType,
                BankCounterInfo = y.BankCounterInfo,
                CardNo = y.CardNo,
                RecordDate = y.RecordDate
            });

            string filterQueryString = LambdaString<ReusedDocumentPaymentLookupItem, SearchData>.CreateWhereLambdaString(new ReusedDocumentPaymentLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await query.Where(x => selectedItemsIds.Contains(x.Id)).ToListAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }
    }
}