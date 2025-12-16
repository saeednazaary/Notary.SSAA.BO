using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;
using static Notary.SSAA.BO.SharedKernel.Constants.EstateConstant;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class ElzamArtSixRepository : Repository<Domain.Entities.SsrArticle6Inq>, IElzamArtSixRepository
    {
        public ElzamArtSixRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Domain.Entities.SsrArticle6Inq> GetElzamArtSixGetById(Guid ElzamArtSixId, CancellationToken cancellationToken)
        {
            return await Table.Include(x => x.SsrArticle6InqPeople)
                .Include(x => x.SsrArticle6InqReceivers)
                .Include(x => x.WorkflowStates)
                .Include(x => x.SsrArticle6InqResponses).ThenInclude(x => x.SenderOrg)
                .Include(x => x.SsrArticle6InqResponses).ThenInclude(x => x.Opposition)
                .Where(x => x.Id == ElzamArtSixId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ElzamArtSixGrid> GetElzamArtSixGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<Guid> selectedItemsIds, List<string> FieldsNotInFilterSearch, string branchCode, ElzamArtSixGridExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            ElzamArtSixGrid result = new();
            var initialQuery = TableNoTracking.Include(x => x.WorkflowStates).Include(x => x.SsrArticle6InqPeople).Include(x => x.SsrArticle6InqResponses).Where(x => x.ScriptoriumId == branchCode);

            if (extraParams is not null)
            {

                if (!extraParams.CreateDate.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => string.Compare(x.CreateDate, extraParams.CreateDate) >= 0);

                if (!extraParams.CreateTime.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => string.Compare(x.CreateTime, extraParams.CreateTime) >= 0);

                if (!extraParams.ProvinceId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.ProvinceId == extraParams.ProvinceId);

                if (!extraParams.CountyId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.CountyId == extraParams.CountyId);

                if (!extraParams.EstateUnitId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.EstateUnitId == extraParams.EstateUnitId);

                if (!extraParams.EstateSectionId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.EstateSectionId == extraParams.EstateSectionId);

                if (!extraParams.EstateSubsectionId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.EstateSubsectionId == extraParams.EstateSubsectionId);

                if (!extraParams.SendDate.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => string.Compare(x.SendDate, extraParams.SendDate) >= 0);

                if (!extraParams.SendTime.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => string.Compare(x.SendTime, extraParams.SendTime) >= 0);

                if (!extraParams.ScriptoriumId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.ScriptoriumId == extraParams.ScriptoriumId);

                if (!extraParams.WorkflowStatesId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.WorkflowStatesId == extraParams.WorkflowStatesId);

                if (!extraParams.EstateUsingId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.EstateUsingId == extraParams.EstateUsingId);

                if (!extraParams.EstateTypeId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.EstateTypeId == extraParams.EstateTypeId);

                if (!extraParams.SellerNationalNO.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SsrArticle6InqPeople.Where(y => y.RelationType == EstateElzamSixRelationType.Seller).FirstOrDefault().NationalityCode == extraParams.SellerNationalNO);

                if (!extraParams.SellerName.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SsrArticle6InqPeople.Where(y => y.RelationType == EstateElzamSixRelationType.Seller).FirstOrDefault().Name == extraParams.SellerName.ArabicTopersian());

                if (!extraParams.SellerFamily.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SsrArticle6InqPeople.Where(y => y.RelationType == EstateElzamSixRelationType.Seller).FirstOrDefault().Name == extraParams.SellerFamily.ArabicTopersian());
            }

            var query = initialQuery.Select(y => new ElzamArtSixGridItem()
            {
                Id = y.Id,
                No = y.No,
                CreateDate = y.CreateDate,
                CreateTime = y.CreateTime,
                Type = y.Type,
                EstateMap = y.EstateMap,
                EstateMainPlaque = y.EstateMainPlaque,
                EstateSecondaryPlaque = y.EstateSecondaryPlaque,
                EstateArea = y.EstateArea,
                EstatePostCode = y.EstatePostCode,
                SendDate = y.SendDate,
                SendTime = y.SendTime,
                ResponseDate = !y.SsrArticle6InqResponses.IsNullOrEmpty() ? y.SsrArticle6InqResponses.Min(x => x.ResponseDate) : null,
                ResponseTime = !y.SsrArticle6InqResponses.IsNullOrEmpty() ? y.SsrArticle6InqResponses.OrderBy(x => x.ResponseDate).FirstOrDefault().ResponseTime : null,
                WorkflowStatesTitle = y.WorkflowStates.Title,
                TrackingCode = y.TrackingCode,
                SellerNationalNO = y.SsrArticle6InqPeople.Where(x => x.RelationType == EstateElzamSixRelationType.Seller).FirstOrDefault().NationalityCode,
                SellerName = y.SsrArticle6InqPeople.Where(x => x.RelationType == EstateElzamSixRelationType.Seller).FirstOrDefault().Name,
                SellerFamily = y.SsrArticle6InqPeople.Where(x => x.RelationType == EstateElzamSixRelationType.Seller).FirstOrDefault().Family
            });

            string filterQueryString = LambdaString<ElzamArtSixGridItem, SearchData>.CreateWhereLambdaString(new ElzamArtSixGridItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await query.Where(x => selectedItemsIds.Contains(x.Id)).ToListAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString.ArabicTopersian());

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }
    }
}
