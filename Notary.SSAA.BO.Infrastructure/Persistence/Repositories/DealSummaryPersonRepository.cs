using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DealSummaryPersonRepository:Repository<DealSummaryPerson>,IDealSummaryPersonRepository
    {
        public DealSummaryPersonRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<DealSummaryPerson>> GetEstateOwnersByDealSummaryInfo(string estateInquiryId, CancellationToken cancellationToken)
        {
            List<DealSummaryPersonSpecialFields> resultList = new();
            var states = new string[] { EstateConstant.DealSummaryStates.Responsed, EstateConstant.DealSummaryStates.Archived, EstateConstant.DealSummaryStates.Sended };
            var dealSummaryPersonList = await TableNoTracking
                .Include(p => p.DealSummary)
                .ThenInclude(d => d.EstateInquiry)
                .ThenInclude(e => e.EstateInquiryPeople)
                .Where(p => p.DealSummary.EstateInquiryId == estateInquiryId.ToGuid() &&
                       states.Contains(p.DealSummary.WorkflowStatesId) &&
                       p.DealSummary.DealSummaryTransferType.Isrestricted == "0" &&
                       (p.DealSummary.WorkflowStatesId == EstateConstant.EstateInquiryStates.Sended  || p.DealSummary.Response != null)
                       )
                .ToListAsync(cancellationToken);

            dealSummaryPersonList = dealSummaryPersonList.Where(p => ((p.DealSummary.WorkflowStatesId == EstateConstant.EstateInquiryStates.Sended && string.IsNullOrWhiteSpace(p.DealSummary.Response)) || (p.DealSummary.Response == "خلاصه معامله مورد تاييد مي باشد" || p.DealSummary.Response == "خلاصه معامله مورد تایید می باشد"))).ToList();


            List<RationalNumber> rationalNumberList = new();
            bool flag = true;
            RationalNumber moamelShare = new();
            DealSummaryPerson moamel = null;

            foreach (var person in dealSummaryPersonList)
            {
                if (person.RelationTypeId == "101")
                {
                    if (person.SharePart.HasValue && person.ShareTotal.HasValue)
                        rationalNumberList.Add(new RationalNumber(person.SharePart.Value, person.ShareTotal.Value));
                    else
                        flag = false;
                }
                else
                    if (person.RelationTypeId == "100")
                {
                    if (person.SharePart.HasValue && person.ShareTotal.HasValue)
                    {
                        moamelShare.S = person.SharePart.Value;
                        moamelShare.M = person.ShareTotal.Value;
                    }
                    else
                    {
                        flag = false;
                    }

                    moamel = person;
                }
            }
            if(dealSummaryPersonList.Count ==0)
            {
                flag = false;
            }
            if (flag)
            {
                RationalNumber totalShareOfBuyers = rationalNumberList[0];
                for (int k = 1; k < rationalNumberList.Count; k++)
                {
                    decimal[] da = Helper.Sum(totalShareOfBuyers.S, rationalNumberList[k].S, totalShareOfBuyers.M, rationalNumberList[k].M);
                    totalShareOfBuyers = new RationalNumber(da[0], da[1]);
                }

                if ((totalShareOfBuyers.M * moamelShare.S) <= (moamelShare.M * totalShareOfBuyers.S))
                {

                    dealSummaryPersonList.Remove(moamel);

                }
                else
                {
                    decimal[] da = Helper.Sum(moamelShare.S, (-1 * totalShareOfBuyers.S), moamelShare.M, totalShareOfBuyers.M);
                    moamel.IsInquiryPerson = EstateConstant.BooleanConstant.True;
                    moamel.SharePart = da[0];
                    moamel.ShareTotal = da[1];
                }
            }


            return dealSummaryPersonList;

        }


        public async Task<DealSummaryPersonGrid> GetDealSummaryPersonGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch,Guid dealSummaryId,bool isOrderBy, CancellationToken cancellationToken)
        {
            DealSummaryPersonGrid result = new();
            var query = TableNoTracking
                .Where(x => x.DealSummaryId == dealSummaryId);

            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(DealSummaryPersonGridItem.PersonName):
                        query = query.Where(x => x.Name.Contains(filter.Value.NormalizeTextChars(true)) || x.Family.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(DealSummaryPersonGridItem.PersonIdentityNo):
                        query = query.Where(x => x.IdentityNo.Contains(filter.Value));
                        break;
                    case nameof(DealSummaryPersonGridItem.PersonNationalityCode):
                        query = query.Where(x => x.NationalityCode.Contains(filter.Value));
                        break;
                    case nameof(DealSummaryPersonGridItem.PersonBirthDate):
                        query = query.Where(x => x.BirthDate.Contains(filter.Value));
                        break;
                    case nameof(DealSummaryPersonGridItem.PersonSharePart):
                        query = query.Where(x => x.SharePart == Convert.ToDecimal(filter.Value));
                        break;
                    case nameof(DealSummaryPersonGridItem.PersonShareTotal):
                        query = query.Where(x => x.ShareTotal == Convert.ToDecimal(filter.Value));                        
                        break;
                    case nameof(DealSummaryPersonGridItem.PersonRelationType):
                        query = query.Where(x => x.RelationType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(DealSummaryPersonGridItem.PersonShareText):
                        query = query.Where(x => x.ShareText.Contains(filter.Value.NormalizeTextChars(true)));
                        break;

                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(x => (x.Name.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.Family.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.IdentityNo.Contains(globalSearch) ||
                                      x.NationalityCode.Contains(globalSearch) ||
                                      x.BirthDate.Contains(globalSearch) ||
                                      x.RelationType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.ShareText.Contains(globalSearch.NormalizeTextChars(true))
                                      ));

            }
            result.TotalCount = await query
               .CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var guidlist = new List<Guid>();
                foreach (var item in selectedItemsIds)
                {
                    guidlist.Add(item.ToGuid());
                }
                result.SelectedItems = await TableNoTracking
                    .Where(p => guidlist.Contains(p.Id)).
                Select(y => new DealSummaryPersonGridItem()
                {
                    PersonBirthDate = y.BirthDate,
                    PersonName = y.Name + " " + (!string.IsNullOrWhiteSpace(y.Family) ? y.Family : ""),
                    PersonNationalityCode = y.NationalityCode,
                    PersonIdentityNo = y.IdentityNo,
                    PersonRelationType = y.RelationType.Title,
                    PersonSharePart = y.SharePart.HasValue ? y.SharePart.Value:null,
                    PersonShareTotal = y.SharePart.HasValue ? y.ShareTotal.Value : null,
                    PersonShareText= y.ShareText
                })

                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {
                    if (gridSortInput.Sort.Equals("personname", StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query                                                  
                          .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                          .OrderBy($"name {gridSortInput.SortType},family  {gridSortInput.SortType}")
                           .Select(y => new DealSummaryPersonGridItem()
                           {
                               PersonBirthDate = y.BirthDate,
                               PersonName = y.Name + " " + (!string.IsNullOrWhiteSpace(y.Family) ? y.Family : ""),
                               PersonNationalityCode = y.NationalityCode,
                               PersonIdentityNo = y.IdentityNo,
                               PersonRelationType = y.RelationType.Title,
                               PersonSharePart = y.SharePart.HasValue ? y.SharePart.Value : null,
                               PersonShareTotal = y.SharePart.HasValue ? y.ShareTotal.Value : null,
                               PersonShareText = y.ShareText
                           })
                              .ToListAsync(cancellationToken);
                    }
                    else
                    {
                        result.GridItems = await query
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")                            
                            .Select(y => new DealSummaryPersonGridItem()
                            {
                                PersonBirthDate = y.BirthDate,
                                PersonName = y.Name + " " + (!string.IsNullOrWhiteSpace(y.Family) ? y.Family : ""),
                                PersonNationalityCode = y.NationalityCode,
                                PersonIdentityNo = y.IdentityNo,
                                PersonRelationType = y.RelationType.Title,
                                PersonSharePart = y.SharePart.HasValue ? y.SharePart.Value : null,
                                PersonShareTotal = y.SharePart.HasValue ? y.ShareTotal.Value : null,
                                PersonShareText = y.ShareText
                            })
                                .ToListAsync(cancellationToken);
                    }

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new DealSummaryPersonGridItem()
                        {
                            PersonBirthDate = y.BirthDate,
                            PersonName = y.Name + " " + (!string.IsNullOrWhiteSpace(y.Family) ? y.Family : ""),
                            PersonNationalityCode = y.NationalityCode,
                            PersonIdentityNo = y.IdentityNo,
                            PersonRelationType = y.RelationType.Title,
                            PersonSharePart = y.SharePart.HasValue ? y.SharePart.Value : null,
                            PersonShareTotal = y.SharePart.HasValue ? y.ShareTotal.Value : null,
                            PersonShareText = y.ShareText
                        })
                        .ToListAsync(cancellationToken);
                }
            }
            Helper.NormalizeStringValuesDeeply(result);
            return result;
            
        }
    }
}
