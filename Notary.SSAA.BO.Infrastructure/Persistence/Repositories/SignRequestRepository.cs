using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequestReports;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Diagnostics;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class SignRequestRepository : Repository<SignRequest>, ISignRequestRepository
    {
        private readonly ApplicationContext DbContext;

        public SignRequestRepository(ApplicationContext context) : base(context)
        {
            DbContext = context;
        }

        public async Task<string> GetMaxReqNo(string beginNationalNo, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.ReqNo.StartsWith(beginNationalNo)).Select(x => x.ReqNo).MaxAsync(cancellationToken);
        }
        public async Task<string> GetMaxNationalNo(string beginNationalNo, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.NationalNo.StartsWith(beginNationalNo)).Select(x => x.NationalNo).MaxAsync(cancellationToken);
        }

        public async Task<SignRequest> GetSignRequest(Guid signRequestId, string ScriptoriumId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
                .AsNoTracking().Include(x => x.SignRequestSubject).Include(x => x.SignRequestPeople).Include(x => x.SignRequestPersonRelateds)
                .ThenInclude(x => x.AgentType)
               .Where(x => x.Id == signRequestId && x.ScriptoriumId == ScriptoriumId)
               .FirstOrDefaultAsync(cancellationToken);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="GridSearchInput"> فیلترهای مخصوص هر فیلد </param>
        /// <param name="GlobalSearch"> فیلتر کلی بر روی تمام فیلدها</param>
        /// <param name="gridSortInput"></param>
        /// <param name="selectedItemsIds">شناسه هایی که قبلا انتخاب شده</param>
        /// <param name="FieldsNotInFilterSearch">لیستی از فیلدهایی که از انتیتی مدنظر که نمیخواهیم بر روی آنها فیلتر بزنیم </param>
        /// <param name="cancellationToken"></param>
        /// <param name="isOrderBy"></param>
        /// <returns></returns>
        public async Task<SignRequestGrid> GetSignRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
               IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SignRequestSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SignRequestGrid result = new();
            var initialQuery = TableNoTracking
            .Include(c => c.SignRequestGetter)
            .Include(s => s.SignRequestSubject)
            .Include(f => f.SignRequestPeople)
            .Where(x => x.ScriptoriumId == scriptoriumId);

            var selectedItemQuery = initialQuery
                .Where(p => selectedItemsIds.Contains(p.Id.ToString())).
                Select(y => new SignRequestGridItem()
                {
                    Id = y.Id.ToString(),
                    ReqNo = y.ReqNo,
                    NationalNo = y.NationalNo,
                    ReqDate = y.ReqDate,
                    SignDate = y.SignDate,
                    StateId = y.State,
                    SignRequestGetterTitle = y.SignRequestGetter.Title,
                    SignRequestSubjectTitle = y.SignRequestSubject.Title,
                    Persons = string.Join(",", y.SignRequestPeople.Select(p => p.Name + " " + p.Family).ToList())
                });

            if (extraParams is not null)
            {
                if (!extraParams.SignRequestReqNo.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.ReqNo == extraParams.SignRequestReqNo);

                if (!extraParams.SignRequestNationalNo.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.NationalNo == extraParams.SignRequestNationalNo);

                if (!extraParams.SignRequestStateId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.State == extraParams.SignRequestStateId);

                if (!extraParams.SignRequestReqDateFrom.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.ReqDate.CompareTo(extraParams.SignRequestReqDateFrom) >= 0);

                if (!extraParams.SignRequestReqDateTo.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.ReqDate.CompareTo(extraParams.SignRequestReqDateTo) <= 0);

                if (!extraParams.SignRequestSignDateFrom.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignDate.CompareTo(extraParams.SignRequestSignDateFrom) >= 0);

                if (!extraParams.SignRequestSignDateTo.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignDate.CompareTo(extraParams.SignRequestSignDateTo) <= 0);

                if (!extraParams.PersonNationalNo.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.NationalNo == extraParams.PersonNationalNo));

                if (!extraParams.PersonName.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.Name.Contains(extraParams.PersonName.PersianToArabic())));

                if (!extraParams.PersonFamily.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.Family.Contains(extraParams.PersonFamily.PersianToArabic())));

                if (!extraParams.PersonFatherName.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.FatherName.Contains(extraParams.PersonFatherName.PersianToArabic())));

                if (!extraParams.PersonIdentityNo.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.IdentityNo == extraParams.PersonIdentityNo));

                if (!extraParams.PersonSignClassifyNo.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.SignClassifyNo == int.Parse(extraParams.PersonSignClassifyNo)));

                if (!extraParams.PersonSeri.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.Seri == extraParams.PersonSeri));

                if (!extraParams.PersonSerial.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.Serial == extraParams.PersonSerial));

                if (!extraParams.PersonPostalCode.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.PostalCode == extraParams.PersonPostalCode));

                if (!extraParams.PersonMobileNo.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.MobileNo == extraParams.PersonMobileNo));

                if (!extraParams.PersonAddress.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.Address.Contains(extraParams.PersonAddress.PersianToArabic())));

                if (!extraParams.PersonTel.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.Tel == extraParams.PersonTel));

                if (!extraParams.PersonBirthDate.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.BirthDate == extraParams.PersonBirthDate));

                if (!extraParams.PersonSexType.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.SexType == extraParams.PersonSexType));

                if (extraParams.IsPersonOriginal.HasValue)
                    if (extraParams.IsPersonOriginal.Value)
                        initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.IsOriginal == "1"));
                    else
                        initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.IsOriginal == "2"));

                if (extraParams.IsPersonRelated.HasValue)
                    if (extraParams.IsPersonRelated.Value)
                        initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.IsRelated == "1"));
                    else
                        initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.IsRelated == "2"));

                if (extraParams.IsPersonIranian.HasValue)
                    if (extraParams.IsPersonIranian.Value)
                        initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.IsIranian == "1"));
                    else
                        initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.IsIranian == "2"));

                if (!extraParams.PersonAlphabetSeri.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.SignRequestPeople.Any(x => x.SeriAlpha == extraParams.PersonAlphabetSeri));

            }

            var query = initialQuery.Select(y => new SignRequestGridItem()
            {
                Id = y.Id.ToString(),
                ReqNo = y.ReqNo,
                NationalNo = y.NationalNo,
                ReqDate = y.ReqDate,
                SignDate = y.SignDate,
                StateId = y.State,
                SignRequestGetterTitle = y.SignRequestGetter.Title,
                SignRequestSubjectTitle = y.SignRequestSubject.Title,
                PersonList = y.SignRequestPeople.Where(x => x.ScriptoriumId == scriptoriumId).Select(p => p.Name + " " + p.Family).ToList(),
                //Persons = string.Join(",", y.SignRequestPeople.Where(x => x.ScriptoriumId == scriptoriumId).Select(p => p.Name + " " + p.Family).ToList())
            });
            SearchData personsFilter = GridSearchInput.Where(s => s.Filter.ToLower() == "Persons".ToLower()).FirstOrDefault();

            string filterQueryString = LambdaString<SignRequestGridItem, SearchData>.CreateWhereLambdaString(new SignRequestGridItem(), GridSearchInput, GlobalSearch.PersianToArabic(), FieldsNotInFilterSearch);

            if (personsFilter is not null)
                filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch.PersianToArabic(), "PersonList", personsFilter.Value);




            if (!string.IsNullOrWhiteSpace(GlobalSearch))
            {
                if (personsFilter is null)
                    filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch.PersianToArabic(), "PersonList");
            }

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
            foreach (var item in result.GridItems)
            {
                item.Persons = string.Join(",", item.PersonList);
            }
            return result;
        }

        public async Task<SignRequestGrid> GetSignRequestAdminGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SignRequestAdminSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SignRequestGrid result = new();
            var initialQuery = TableNoTracking
            .Include(c => c.SignRequestGetter)
            .Include(s => s.SignRequestSubject)
            .Include(f => f.SignRequestPeople).Where(x => x.State == "2").AsQueryable();
            if (selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = initialQuery
                .Where(p => selectedItemsIds.Contains(p.Id.ToString())).
                 Select(y => new SignRequestGridItem()
                 {
                     Id = y.Id.ToString(),
                     ReqNo = y.ReqNo,
                     NationalNo = y.NationalNo,
                     ReqDate = y.ReqDate,
                     SignDate = y.SignDate,
                     StateId = y.State,
                     SignRequestGetterTitle = y.SignRequestGetter.Title,
                     SignRequestSubjectTitle = y.SignRequestSubject.Title,
                     Persons = string.Join(",", y.SignRequestPeople.Select(p => p.Name + " " + p.Family).ToList())
                 });
                if (pageIndex == 1 && selectedItemsIds.Count > 0)
                    result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }
            var query = initialQuery.Select(y => new SignRequestGridItem()
            {
                Id = y.Id.ToString(),
                ReqNo = y.ReqNo,
                NationalNo = y.NationalNo,
                ReqDate = y.ReqDate,
                SignDate = y.SignDate,
                StateId = y.State,
                SignRequestGetterTitle = y.SignRequestGetter.Title,
                SignRequestSubjectTitle = y.SignRequestSubject.Title,
                PersonList = y.SignRequestPeople.Select(p => p.Name + " " + p.Family).ToList(),

            });
            if (extraParams is not null)
            {
                if (!extraParams.SignRequestReqNo.IsNullOrWhiteSpace())
                    query = query.Where(x => x.ReqNo == extraParams.SignRequestReqNo);
                if (!extraParams.SignRequestNationalNo.IsNullOrWhiteSpace())
                    query = query.Where(x => x.NationalNo == extraParams.SignRequestNationalNo);

            }

            string filterQueryString = LambdaString<SignRequestGridItem, SearchData>.CreateWhereLambdaString(new SignRequestGridItem(), GridSearchInput, GlobalSearch.PersianToArabic(), FieldsNotInFilterSearch);



            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);
            foreach (var item in result.GridItems)
            {
                item.Persons = string.Join(",", item.PersonList);
            }
            return result;
        }
        public async Task<ICollection<SignRequestPerson>> GetSignRequestPersonFingerprint(Guid signRequestId, CancellationToken cancellationToken)
        {
            return (await TableNoTracking.Include(x => x.SignRequestPeople).AsSplitQuery().
                Where(x => x.Id == signRequestId).
                FirstOrDefaultAsync(cancellationToken)).SignRequestPeople;
        }
        public async Task<SignRequest> GetSignRequestPersons(Guid signRequestId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.SignRequestPeople).AsSplitQuery().
                Where(x => x.Id == signRequestId).
                FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<SignRequest> SignRequestTracking(Guid signRequestId, string scriptoriumId, CancellationToken cancellationToken)
        {
            return await Table
                .Include(x => x.SignRequestPersonRelateds)
                .ThenInclude(x => x.AgentType)
                .Include(x => x.SignRequestGetter)
                .Include(x => x.SignRequestSubject)
                .Include(x => x.SignRequestPeople)
                .ThenInclude(y => y.SignRequestPersonRelatedMainPeople)
                .Include(x => x.SignRequestFile)
                .Where(x => x.Id == signRequestId && x.ScriptoriumId == scriptoriumId)
                .FirstOrDefaultAsync(cancellationToken);

        }
        public async Task<SignRequest> SignRequestTracking(string requestNo, CancellationToken cancellationToken)
        {
            return await Table.Include(x => x.SignRequestPersonRelateds).ThenInclude(x => x.AgentType).Include(x => x.SignRequestGetter).Include(x => x.SignRequestSubject).Include(x => x.SignRequestPeople).Include(x => x.SignRequestFile)
                .Where(x => x.ReqNo == requestNo).FirstOrDefaultAsync(cancellationToken);

        }
        public async Task<(SignRequest, decimal)> GetSignRequestBookPage(string scriptorumId, int currentPageNumber, string NationalNo, int? PersonSignClassifyNo, CancellationToken cancellationToken)
        {
            SignRequest signRequest;
            var initialQuery = TableNoTracking.Include(x => x.SignRequestSubject).Include(x => x.SignRequestGetter).Include(x => x.SignRequestPeople).OrderByDescending(x => x.NationalNo).Where(x => x.State == "2" && x.ScriptoriumId == scriptorumId);
            if ((PersonSignClassifyNo != null && PersonSignClassifyNo > 0) || !string.IsNullOrEmpty(NationalNo))
            {
                decimal FindLastNumber = decimal.Parse(await TableNoTracking.Where(x=>x.ScriptoriumId==scriptorumId).Select(x => x.NationalNo).MaxAsync(cancellationToken));
                signRequest = await initialQuery.Where(x => (x.NationalNo == NationalNo || string.IsNullOrEmpty(NationalNo)) && (x.SignRequestPeople.Any(y => y.ScriptoriumId == x.ScriptoriumId && (y.SignClassifyNo == PersonSignClassifyNo) || PersonSignClassifyNo == null || PersonSignClassifyNo == 0))).FirstOrDefaultAsync(cancellationToken);
                if (signRequest is null)
                {
                    return (null, currentPageNumber);
                }
                decimal FindSignRequestNumber = decimal.Parse(signRequest.NationalNo);

                return (signRequest, (FindLastNumber- FindSignRequestNumber)+1);

            }
            return (await initialQuery.Where(x => (x.NationalNo == NationalNo || string.IsNullOrEmpty(NationalNo)) && (x.SignRequestPeople.Any(y => y.ScriptoriumId == x.ScriptoriumId && (y.SignClassifyNo == PersonSignClassifyNo) || PersonSignClassifyNo == null || PersonSignClassifyNo == 0))).Skip(currentPageNumber-1).Take(1).FirstOrDefaultAsync(cancellationToken), currentPageNumber);
        }
        public async Task<int> GetSignRequestElectronicBookTotalCount(string scriptorumId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
        .Where(x => x.ScriptoriumId == scriptorumId && x.State == "2").CountAsync(cancellationToken);

        }
        public async Task<SignRequest> ConfirmSignRequest(SignRequest source, List<Domain.Entities.SignElectronicBook> signElectronicBooks, CancellationToken cancellationToken)
        {
            DbContext.Database.BeginTransaction();
            DbContext.Attach(source);
            DbContext.Update(source);
            DbContext.UpdateRange(source.SignRequestPeople);
            DbContext.UpdateRange(source.SignRequestPersonRelateds);
            DbContext.AddRange(signElectronicBooks);
            await DbContext.SignRequestSemaphores.Where(x => x.SignRequestId == source.Id).ExecuteDeleteAsync(cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            //DbContext.SignRequests.ExecuteUpdateAsync(b => b.SetProperty(u => u.ReqNo, source.ReqNo) ,cancellationToken);
            await DbContext.Database.CommitTransactionAsync(cancellationToken);
            return source;
        }

        public async Task<KatebSignRequestGrid> GetKatebSignRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridFilterInput, string globalSearch, SortData gridSortInput, IList<string> selectedItems, List<string> fieldsNotInFilterSearch, string branchCode, KatebSignRequestSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy)
        {
            KatebSignRequestGrid result = new();
            var initialQuery = TableNoTracking
                .Include(x => x.SignRequestSubject)
                .Include(f => f.SignRequestPeople)
                .Where(x => x.SignRequestPeople.Any(x => x.NationalNo == extraParams.NationalNo));

            var selectedItemQuery = initialQuery
                .Where(p => selectedItems.Contains(p.Id.ToString())).
                Select(y => new KatebSignRequestGridItem()
                {
                    Id = y.Id.ToString(),
                    ReqNo = y.ReqNo,
                    SsarNo = y.NationalNo,
                    ReqDate = y.ReqDate,
                    StateId = y.State,
                    IsCostPaid = y.IsCostPaid,
                    ReqTime = y.ReqTime,
                    ScriptoriumId = y.ScriptoriumId,
                    IsRemote = y.IsRemoteRequest,
                    SignRequestSubjectTitle = y.SignRequestSubject.Title,
                    IsReadyToPay = y.IsReadyToPay,
                });

            if (extraParams is not null)
            {
                if (!extraParams.StateId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.State == extraParams.StateId);

                if (!extraParams.FromDate.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => string.Compare(x.ReqDate, extraParams.FromDate) >= 0);

                if (!extraParams.ToDate.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => string.Compare(x.ReqDate, extraParams.ToDate) <= 0);

                if (extraParams.IsCostPaid != null && extraParams.IsCostPaid.Count > 0)
                    initialQuery = initialQuery.Where(x => extraParams.IsCostPaid.Contains(x.IsCostPaid));
                if (!extraParams.IsReadyToPay.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.IsReadyToPay == extraParams.IsReadyToPay);
            }

            var query = initialQuery.Select(y => new KatebSignRequestGridItem()
            {
                Id = y.Id.ToString(),
                ReqNo = y.ReqNo,
                SsarNo = y.NationalNo,
                ReqDate = y.ReqDate,
                StateId = y.State,
                IsCostPaid = y.IsCostPaid,
                ReqTime = y.ReqTime,
                ScriptoriumId = y.ScriptoriumId,
                IsRemote = y.IsRemoteRequest,
                SignRequestSubjectTitle = y.SignRequestSubject.Title,
                IsReadyToPay = y.IsReadyToPay,
            });

            string filterQueryString = LambdaString<KatebSignRequestGridItem, SearchData>.CreateWhereLambdaString(new KatebSignRequestGridItem(), gridFilterInput, globalSearch, fieldsNotInFilterSearch);


            if (pageIndex == 1 && selectedItems.Count > 0)
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }
        public async Task<SignRequest> GetKatebSignRequesById(string id, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.SignRequestGetter).Include(x => x.SignRequestSubject).Include(x => x.SignRequestPeople).Include(x => x.SignRequestFile).
                 FirstOrDefaultAsync(x => x.Id == id.ToGuid() || x.ReqNo == id, cancellationToken);
        }


        public async Task<List<SignRequestStatisticRepositoryObject>> generateSignRequsetStatisticReport(string fromDate, string toDate, IList<string> getter, IList<string> subjects, string branchCode, CancellationToken cancellationToken)
        {
            var quiry = TableNoTracking.Include(x => x.SignRequestGetter).Include(x => x.SignRequestSubject).Include(x => x.SignRequestPeople).Where(x => x.ScriptoriumId == branchCode && x.State == "2");

            if (getter != null || getter?.Count > 0)
            {
                quiry = quiry.Where(x => x.SignRequestGetterId == getter.First());
            }
            if (subjects != null || subjects?.Count > 0)
            {
                quiry = quiry.Where(x => x.SignRequestSubjectId == subjects.First());
            }

            return await quiry.SelectMany(x => x.SignRequestPeople.Where(p => p.SignClassifyNo != null).Select(p => new SignRequestStatisticRepositoryObject
            {
                ClassifyNo = p.SignClassifyNo.ToString(),
                Family = p.Family,
                Name = p.Name,
                Getters = x.SignRequestGetter != null ? x.SignRequestGetter.Title : null,
                NationalNo = x.NationalNo,
                SignDate = x.SignDate,
                Subjects = x.SignRequestSubject != null ? x.SignRequestSubject.Title : null,

            })).ToListAsync(cancellationToken);

        }

        public async Task<List<SignRequestAffidavitRepositoryObject>> SignRequestAffidavit(string signRequestNationalNo, string signRequestSecretCode, string scriptoriumId,List<string> NotInScriptoriumId, CancellationToken cancellationToken)
        {
            var query =
                from req in DbContext.SignRequests
                join person in DbContext.SignRequestPeople
                on req.Id equals person.SignRequestId
                join agent in DbContext.SignRequestPersonRelateds
                on person.Id equals agent.AgentPersonId into agentGroup
                from agent in agentGroup.DefaultIfEmpty()
                join agentType in DbContext.AgentTypes
                on agent.AgentTypeId equals agentType.Id into agentTypeGroup
                from agentType in agentTypeGroup.DefaultIfEmpty()
                join subjType in DbContext.SignRequestSubjects
                on req.SignRequestSubjectId equals subjType.Id into subjTypeGroup
                from subjType in subjTypeGroup.DefaultIfEmpty()
                join getter in DbContext.SignRequestGetters
                on req.SignRequestGetterId equals getter.Id into getterGroup
                from getter in getterGroup.DefaultIfEmpty()
                    // Left join to Movakel (ONOTARYSIGNPERSON)
                join movakel in DbContext.SignRequestPeople
                on agent.MainPersonId equals movakel.Id into movakelGroup
                from movakel in movakelGroup.DefaultIfEmpty()
                where
                req.NationalNo == signRequestNationalNo &&
                req.LegacyId==null &&
                req.ScriptoriumId == scriptoriumId &&
                !NotInScriptoriumId.Contains(req.ScriptoriumId) &&
                 req.State == "2" &&
                 req.SecretCode == signRequestSecretCode
                select new SignRequestAffidavitRepositoryObject
                {
                    NationalRegisterNo = req.NationalNo,
                    DocDate = req.SignDate,
                    SignGetterTitle = getter.Title,
                    SubjectTypeTitle = subjType.Title,
                    NationalNo = person.NationalNo,
                    Name = person.Name,
                    Family = person.Family,
                    Title = agentType.Title,
                    MovakelName = movakel.Name,
                    MovakelFamily = movakel.Family,
                    MovakelNationalNo = movakel.NationalNo,
                    MobileNo = person.MobileNo,
                    Tel = person.Tel,
                    PostalCode = person.PostalCode,
                    Address = person.Address,
                    MovakelMobileNo = movakel.MobileNo,
                    MovakelTelNo = movakel.Tel,
                    MovakelPostalCode = movakel.PostalCode,
                    MovakelAddress = movakel.Address,
                    PersonId = person.Id.ToString(),
                    SignRequestId = req.Id.ToString(),
                    SignRequestNo = req.ReqNo,
                    // Removed join with Scriptorium and FingerPrint
                    ScriptoriumId = req.ScriptoriumId,
                    // FingerPrintImage removed completely
                };

            return await query.ToListAsync(cancellationToken);



        }

        public async Task<List<SignRequestVerificationWithImportantAnnexRepositoryObject>> SignRequestVerificationWithImportantAnnex(string signRequestNationalNo, string signRequestSecretCode, string ScriptoriumId,List<string> NotInScriptoriumId, CancellationToken cancellationToken)
        {
            var query =
                from signReq in DbContext.SignRequests
    where signReq.NationalNo == signRequestNationalNo
          && signReq.ScriptoriumId == ScriptoriumId
          && !NotInScriptoriumId.Contains(signReq.ScriptoriumId)
          && signReq.SecretCode == signRequestSecretCode
          && signReq.LegacyId == null 
          && signReq.State == "2"

    join signPerson in DbContext.SignRequestPeople
        on signReq.Id equals signPerson.SignRequestId
    join signAgent in DbContext.SignRequestPersonRelateds
        on signPerson.Id equals signAgent.AgentPersonId into signAgentJoin
    from signAgent in signAgentJoin.DefaultIfEmpty()
    join agentType in DbContext.AgentTypes
        on signAgent.AgentTypeId equals agentType.Id into agentTypeJoin
    from agentType in agentTypeJoin.DefaultIfEmpty()
    join subjectType in DbContext.SignRequestSubjects
        on signReq.SignRequestSubjectId equals subjectType.Id into subjectTypeJoin
    from subjectType in subjectTypeJoin.DefaultIfEmpty()
    join getter in DbContext.SignRequestGetters
        on signReq.SignRequestGetterId equals getter.Id into getterJoin
    from getter in getterJoin.DefaultIfEmpty()
    join movakel in DbContext.SignRequestPeople
        on signAgent.MainPersonId equals movakel.Id into movakelJoin
    from movakel in movakelJoin.DefaultIfEmpty()

    select new SignRequestVerificationWithImportantAnnexRepositoryObject
    {
        NationalRegisterNo = signReq.NationalNo,
        DocDate = signReq.SignDate,
        SignGetterTitle = getter.Title ?? "",
        SubjectTypeTitle = subjectType.Title ?? "",
        NationalNo = signPerson.NationalNo ?? "",
        Name = signPerson.Name ?? "",
        Family = signPerson.Family ?? "",
        Title = agentType.Title ?? "",
        MovakelName = movakel.Name ?? "",
        MovakelFamily = movakel.Family ?? "",
        MovakelNationalNo = movakel.NationalNo ?? "",
        MobileNo = signPerson.MobileNo ?? "",
        Tel = signPerson.Tel ?? "",
        PostalCode = signPerson.PostalCode ?? "",
        Address = signPerson.Address ?? "",
        MovakelMobileNo = movakel.MobileNo ?? "",
        MovakelTelNo = movakel.Tel ?? "",
        MovakelPostalCode = movakel.PostalCode ?? "",
        MovakelAddress = movakel.Address ?? "",
        ScriptoriumId = signReq.ScriptoriumId ?? "",
        ClassifyNo = signPerson.SignClassifyNo,
        SignDate=signReq.SignDate,
        PersonType= signPerson.PersonType,
        PersonId= signPerson.Id.ToString(),
        SignRequestId= signReq.Id.ToString(),
        SignRequestNo = signReq.ReqNo,
    };

            return await query.ToListAsync(cancellationToken);

        }
        public async Task<List<SignRequestFingerPrintRepositoryObject>> getFingerPrintReport(Guid signRequestId, CancellationToken cancellationToken)
        {

            var query = from signReq in DbContext.SignRequests
                        where signReq.Id == signRequestId
                        join signPerson in DbContext.SignRequestPeople
                        on signReq.Id equals signPerson.SignRequestId
                        join personFingerprint in DbContext.PersonFingerprints
                        on signPerson.NationalNo equals personFingerprint.PersonNationalNo
                        join personFingerType in DbContext.PersonFingerTypes
                        on personFingerprint.PersonFingerTypeId equals personFingerType.Id
                        where personFingerprint.UseCaseMainObjectId == signRequestId.ToString()

                        select (new SignRequestFingerPrintRepositoryObject
                        {
                            DocDate = signReq.ConfirmDate,
                            DocNo = signReq.ReqNo,
                            FingerDate = personFingerprint.FingerprintGetDate,
                            FingerTime = personFingerprint.FingerprintGetTime,
                            FingerDeviceName = personFingerprint.FingerprintScannerDeviceType,
                            FingerImage = personFingerprint.FingerprintImageFile,
                            FingerType = personFingerType.Title,
                            IsFingerMatch = personFingerprint.MocState,
                            PersonFamilyFinger = signPerson.Family,
                            PersonMobileNoFinger = signPerson.MobileNo,
                            PersonNameFinger = signPerson.Name,
                            PersonNatinalNoFinger = signPerson.NationalNo,
                            PersonPostalCodeFinger = signPerson.PostalCode,
                            TFASendDate = personFingerprint.TfaSendDate,
                            TFASendTime = personFingerprint.TfaSendTime,
                            SignNationalNo = signReq.NationalNo,
                            scriptoriumId = signReq.ScriptoriumId,
                        });

            return await query.ToListAsync(cancellationToken);
        }
    }
}
