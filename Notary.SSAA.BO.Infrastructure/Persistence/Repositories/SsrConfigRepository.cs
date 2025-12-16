using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.SsrConfig;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class SsrConfigRepository : Repository<SsrConfig>, ISsrConfigRepository
    {
        private readonly ApplicationContext DbContext;

        public SsrConfigRepository(ApplicationContext context) : base(context)
        {
            DbContext = context;
        }

        public async Task<List<SsrConfig>> GetBusinessConfig(SsrConfigRepositoryInput businessInput, CancellationToken cancellationToken)
        {
            var configs = TableNoTracking
                .Include(x => x.SsrConfigSubject)
                .Where(c => c.IsConfirmed == "1" 
                && (c.ConfigStartDate+"-"+c.ConfigStartTime).CompareTo(businessInput.CurrentDateTime) < 0  
                && (c.ConfigEndDate + "-" + c.ConfigEndTime).CompareTo(businessInput.CurrentDateTime) > 0)
                .AsQueryable();

            if (!businessInput.CurrentCostTypeId.IsNullOrWhiteSpace())
            {
                configs = configs
                    .Include(x => x.SsrConfigConditionCostTypes)
                    .Where(x =>
                        (x.CostTypeCondition == "1" && x.SsrConfigConditionCostTypes.Any(c => c.CostTypeId == businessInput.CurrentCostTypeId)) ||
                         x.CostTypeCondition == "0");
            }

            if (!businessInput.CurrentAgentTypeId.IsNullOrWhiteSpace())
            {
                configs = configs
                    .Include(x => x.SsrConfigConditionAgntTypes)
                    .Where(x =>
                        (x.AgentTypeCondition == "1" && x.SsrConfigConditionAgntTypes.Any(a => a.AgentTypeId == businessInput.CurrentAgentTypeId)) ||
                         x.AgentTypeCondition == "0");
            }

            if (!businessInput.CurrentDocTypeId.IsNullOrWhiteSpace())
            {
                configs = configs
                    .Include(x => x.SsrConfigConditionDoctypes)
                    .Where(x =>
                        (x.DocTypeCondition == "1" && x.SsrConfigConditionDoctypes.Any(d => d.DocumentTypeId == businessInput.CurrentDocTypeId)) ||
                         x.DocTypeCondition == "0");
            }

            if (!businessInput.CurrentGeoLocationId.IsNullOrWhiteSpace())
            {
                configs = configs
                    .Include(x => x.SsrConfigConditionGeolocs)
                    .Where(x =>
                        (x.GeoCondition == "1" && x.SsrConfigConditionGeolocs.Any(g => g.GeoLocationId == businessInput.CurrentGeoLocationId.ToInt())) ||
                         x.GeoCondition == "0");
            }

            if (!businessInput.CurrentDocumentPersonTypeId.IsNullOrWhiteSpace())
            {
                configs = configs
                    .Include(x => x.SsrConfigConditionDcprstps)
                    .Where(x =>
                        (x.PersonTypeCondition == "1" && x.SsrConfigConditionDcprstps.Any(p => p.DocumentPersonTypeId == businessInput.CurrentDocumentPersonTypeId)) ||
                         x.PersonTypeCondition == "0");
            }

            if (!businessInput.CurrentScriptoriumId.IsNullOrWhiteSpace())
            {
                configs = configs
                    .Include(x => x.SsrConfigConditionScrptrms)
                    .Where(x =>
                        (x.ScriptoriumCondition == "1" && x.SsrConfigConditionScrptrms.Any(s => s.ScriptoriumId == businessInput.CurrentScriptoriumId)) ||
                         x.ScriptoriumCondition == "0");
            }

            if (!businessInput.CurrentDayOfWeek.IsNullOrWhiteSpace())
            {
                configs = configs
                    .Include(x => x.SsrConfigConditionTimes)
                    .Where(x =>
                        (x.TimeCondition == "1" && x.SsrConfigConditionTimes.Any(t => t.DayOfWeek == businessInput.CurrentDayOfWeek)) ||
                         x.TimeCondition == "0");
            }

            if (!businessInput.UnitLevelCode.IsNullOrWhiteSpace())
            {
                configs = configs
                    .Include(x => x.SsrConfigConditionUnits)
                    .Where(x =>
                        (x.UnitCondition == "1" && x.SsrConfigConditionUnits.Any(u => u.UnitLevelCode.StartsWith(businessInput.UnitLevelCode))) ||
                         x.UnitCondition == "0");
            }



            return await configs.GroupBy(c => c.SsrConfigSubjectId)
                .Select(g => g.OrderByDescending(c => c.ConfirmDate)
                .ThenByDescending(c => c.ConfirmTime).First())
                .ToListAsync(cancellationToken);
        }

        public async Task<SsrConfigAdvancedSearchGrid> GetLastSsrConfigGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SsrConfigGridExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SsrConfigAdvancedSearchGrid result = new();


            var initialQuery = TableNoTracking
                        .Include(x => x.SsrConfigSubject)
                        .Include(x => x.SsrConfigMainSubject).AsQueryable();

            var selectedItemQuery = initialQuery
                .Where(p => selectedItemsIds.Contains(p.Id.ToString())).
                Select(y => new SsrConfigGridItem()
                {
                    Id = y.Id.ToString(),
                    ConfigMainSubjectTitle = y.SsrConfigMainSubject.Title,
                    ConfigSubjectTitle = y.SsrConfigSubject.Title,
                    ConfigStartDate = y.ConfigStartDate,
                    ConfigEndDate = y.ConfigEndDate,
                    ConfigState = y.IsConfirmed,
                    ConfigEndTime = y.ConfigEndTime,
                    Confirmer = y.Confirmer,
                    ConfigStartTime = y.ConfigStartTime,
                    ConditionType = y.ConditionType
                });

            if (extraParams is not null)
            {


            }
            var query = initialQuery.
                Select(y => new SsrConfigGridItem()
                {
                    Id = y.Id.ToString(),
                    ConfigMainSubjectTitle = y.SsrConfigMainSubject.Title,
                    ConfigSubjectTitle = y.SsrConfigSubject.Title,
                    ConfigStartDate = y.ConfigStartDate,
                    ConfigEndDate = y.ConfigEndDate,
                    ConfigSubjectId = y.SsrConfigSubjectId,
                    ConfigState = y.IsConfirmed,
                    ConfigEndTime = y.ConfigEndTime,
                    Confirmer = y.Confirmer,
                    ConfigStartTime = y.ConfigStartTime,
                    ConditionType = y.ConditionType
                });

            string filterQueryString = LambdaString<SsrConfigGridItem, SearchData>.CreateWhereLambdaString(new SsrConfigGridItem(), GridSearchInput, GlobalSearch.PersianToArabic(), FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.OrderByDescending(c => c.ConfigStartDate).Skip((pageIndex - 1) * pageSize)
                              .Take(pageSize).ToListAsync(cancellationToken);


            result.TotalCount = await query.GroupBy(c => c.ConfigSubjectId)
                .Select(g => g.OrderByDescending(c => c.ConfigStartDate)
                              .First()).CountAsync(cancellationToken);

            return result;
        }

        public async Task<SsrConfigAdvancedSearchGrid> GetSsrConfigGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
            IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SsrConfigGridExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SsrConfigAdvancedSearchGrid result = new();
            var initialQuery = TableNoTracking;

            var selectedItemQuery = initialQuery
                .Where(p => selectedItemsIds.Contains(p.Id.ToString())).
                Select(y => new SsrConfigGridItem()
                {
                    Id = y.Id.ToString(),
                    ConfigMainSubjectTitle = y.SsrConfigMainSubject.Title,
                    ConfigSubjectTitle = y.SsrConfigSubject.Title,
                    ConfigStartDate = y.ConfigStartDate,
                    ConfigEndDate = y.ConfigEndDate,

                });

            if (extraParams is not null)
            {


            }

            var query = initialQuery.
                Select(y => new SsrConfigGridItem()
                {
                    Id = y.Id.ToString(),
                    ConfigMainSubjectTitle = y.SsrConfigMainSubject.Title,
                    ConfigSubjectTitle = y.SsrConfigSubject.Title,
                    ConfigStartDate = y.ConfigStartDate,
                    ConfigEndDate = y.ConfigEndDate,

                });

            string filterQueryString = LambdaString<SsrConfigGridItem, SearchData>.CreateWhereLambdaString(new SsrConfigGridItem(), GridSearchInput, GlobalSearch.PersianToArabic(), FieldsNotInFilterSearch);

            //زمانی که فیلد مرتبسازی از نوع لیست باشد به صورت زیر عمل کنید
            if (gridSortInput.Sort == "persons")
                gridSortInput.Sort = "PersonList.FirstOrDefault() ";

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);

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
