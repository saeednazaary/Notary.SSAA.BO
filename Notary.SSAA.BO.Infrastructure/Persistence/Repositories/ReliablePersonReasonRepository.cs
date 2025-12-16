using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class ReliablePersonReasonRepository : Repository<ReliablePersonReason>, IReliablePersonReasonRepository
    {
        public ReliablePersonReasonRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<BaseLookupRepositoryObject> GetExecutiveRequestReliablePersonReasonLookupItems(int pageIndex, int pageSize, string agentTypeCode, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();

            if (agentTypeCode.Trim().Equals(ExecutiveRequestConstant.RelatedAgentType.Motamed))
            {
                List<LinqExpressionInfo> filterList = new()
                {
                    new LinqExpressionInfo() { Connector = LinqExpressionConnector.And, Operator = LinqExpressionOparator.NotEqual, PropertyName = "Code", Value = ExecutiveRequestConstant.ReliablePersonReason.SarDaftaranODaftaryaran, IsMainCriteria = true }
                };

                Expression<Func<ReliablePersonReason, bool>> linqLambdaExpression = LambdaExpressionHelper.CreateLinqLambdaExpression<ReliablePersonReason>(filterList);

                int selectItemCount = 0;
                int gridItemCount = 0;
                BaseLookupItem lookupFilterItem = new();
                string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(lookupFilterItem, GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);


                if (pageIndex == 1 && selectedItemsIds.Count > 0)
                {
                    result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Where(linqLambdaExpression)
                    .Select(y => new BaseLookupItem() { Title = y.Title, Id = y.Id.ToString(), Code = y.Code })
                    .ToListAsync(cancellationToken);
                }
                if (!string.IsNullOrEmpty(filterQueryString) && !string.IsNullOrWhiteSpace(filterQueryString))
                {
                    result.GridItems = isOrderBy
                        ? await TableNoTracking.Where(filterQueryString).Where(linqLambdaExpression).OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ").Skip((pageIndex - 1) * pageSize).Take(pageSize).
                                Select(y => new BaseLookupItem { Title = y.Title, Id = y.Id.ToString(), Code = y.Code }).ToListAsync(cancellationToken)
                        : await TableNoTracking.Where(filterQueryString).Where(linqLambdaExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).
                         Select(y => new BaseLookupItem { Title = y.Title, Id = y.Id.ToString(), Code = y.Code }).ToListAsync(cancellationToken);
                    result.TotalCount = await TableNoTracking.Where(filterQueryString).Where(linqLambdaExpression).CountAsync();

                }
                else
                {
                    result.GridItems = isOrderBy
                        ? await TableNoTracking.Where(linqLambdaExpression).OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ").Skip((pageIndex - 1) * pageSize).Take(pageSize).
                                Select(y => new BaseLookupItem { Title = y.Title, Id = y.Id.ToString(), Code = y.Code }).ToListAsync(cancellationToken)
                        : await TableNoTracking.Where(linqLambdaExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).
                         Select(y => new BaseLookupItem { Title = y.Title, Id = y.Id.ToString(), Code = y.Code }).ToListAsync(cancellationToken);
                    result.TotalCount = await TableNoTracking.Where(linqLambdaExpression).CountAsync();

                }
            }
            else if (agentTypeCode.Trim().Equals(ExecutiveRequestConstant.RelatedAgentType.DadsetanYaRaisDadgahBakhsh))
            {
                List<string> reliablePersonReason = new()
                {
                    ExecutiveRequestConstant.ReliablePersonReason.AshkhasBimar,
                    ExecutiveRequestConstant.ReliablePersonReason.Zendaniyan,
                    ExecutiveRequestConstant.ReliablePersonReason.SarDaftaranODaftaryaran
                };

                List<LinqExpressionInfo> filteList = new()
                {
                    new LinqExpressionInfo() { Connector = LinqExpressionConnector.And, Operator = LinqExpressionOparator.In, PropertyName = "Code", Value = reliablePersonReason, IsMainCriteria = true }
                };

                Expression<Func<ReliablePersonReason, bool>> linqLambdaExpression = LambdaExpressionHelper.CreateLinqLambdaExpression<ReliablePersonReason>(filteList);

                int selectItemCount = 0;
                int gridItemCount = 0;
                BaseLookupItem lookupFilterItem = new();
                string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(lookupFilterItem, GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);



                if (pageIndex == 1 && selectedItemsIds.Count > 0)
                {
                    result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Where(linqLambdaExpression)
                    .Select(y => new BaseLookupItem() { Title = y.Title, Id = y.Id.ToString(), Code = y.Code })
                    .ToListAsync(cancellationToken);
                }
                if (!string.IsNullOrEmpty(filterQueryString) && !string.IsNullOrWhiteSpace(filterQueryString))
                {
                    result.GridItems = isOrderBy
                        ? await TableNoTracking.Where(filterQueryString).Where(linqLambdaExpression).OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ").Skip((pageIndex - 1) * pageSize).Take(pageSize).
                                Select(y => new BaseLookupItem { Title = y.Title, Id = y.Id.ToString(), Code = y.Code }).ToListAsync(cancellationToken)
                        : await TableNoTracking.Where(filterQueryString).Where(linqLambdaExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).
                         Select(y => new BaseLookupItem { Title = y.Title, Id = y.Id.ToString(), Code = y.Code }).ToListAsync(cancellationToken);
                    result.TotalCount = await TableNoTracking.Where(filterQueryString).Where(linqLambdaExpression).CountAsync();

                }
                else
                {
                    result.GridItems = isOrderBy
                        ? await TableNoTracking.Where(linqLambdaExpression).OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ").Skip((pageIndex - 1) * pageSize).Take(pageSize).
                                Select(y => new BaseLookupItem { Title = y.Title, Id = y.Id.ToString(), Code = y.Code }).ToListAsync(cancellationToken)
                        : await TableNoTracking.Where(linqLambdaExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).
                         Select(y => new BaseLookupItem { Title = y.Title, Id = y.Id.ToString(), Code = y.Code }).ToListAsync(cancellationToken);
                    result.TotalCount = await TableNoTracking.Where(linqLambdaExpression).CountAsync();

                }
            }

            return result;


        }

        public async Task<BaseLookupRepositoryObject> GetSignRequestReliablePersonReasonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> ResultFields, List<string> FieldsNotInFilterSearch, SignRequestReliablePersonReasonLookupExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {

            BaseLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(x => x.State == "1");

            if (extraParams is not null)
            {
                if (extraParams.DontShowCodes.Count>0)
                {
                    initialQuery = initialQuery.Where(x => !extraParams.DontShowCodes.Contains(x.Code));
                }
                else if (extraParams.ShowCodes.Count > 0)
                {
                    initialQuery = initialQuery.Where(x => extraParams.ShowCodes.Contains(x.Code));
                }
            }

            var query = initialQuery.Select(y => new BaseLookupItem
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(new BaseLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {



                var selectedItems = await TableNoTracking.Where(x => x.State == "1").ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
                    .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Select(y => new BaseLookupItem
                    {
                        Title = y.Title,
                        Id = y.Id.ToString(),
                        Code = y.Code
                    })
                    .ToList();

                result.SelectedItems = selectedItemQuery;
            }


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
