using Mapster;
using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Stimulsoft.Report.StiRecentConnections;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal class LastSsrConfigAdvancedQueryHandler : BaseQueryHandler<LastSsrConfigAdvancedSearchQuery, ApiResult<SsrConfigAdvancedSearchGridViewModel>>
    {
        private readonly ISsrConfigRepository _advancedSearchGridRepository;
        private IDateTimeService _dateTimeService;
        public LastSsrConfigAdvancedQueryHandler(IMediator mediator, IUserService userService,
            ISsrConfigRepository advancedSearchGridRepository, IDateTimeService dateTimeService)
            : base(mediator, userService)
        {
            _advancedSearchGridRepository = advancedSearchGridRepository ?? throw new ArgumentNullException(nameof(advancedSearchGridRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));

        }

        protected override bool HasAccess(LastSsrConfigAdvancedSearchQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.SSAAAdmin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SsrConfigAdvancedSearchGridViewModel>> RunAsync(LastSsrConfigAdvancedSearchQuery request, CancellationToken cancellationToken)
        {
            var apiresult = new SsrConfigAdvancedSearchGridViewModel();
            bool isOrderBy = false;
            var persianDate = _dateTimeService.CurrentPersianDate;
            var persianTime = _dateTimeService.CurrentTime;
            SortData gridSortInput = new();
            // 
            //  فیلدهایی که لیست می باشند را در متغییر زیر نیز قرار دهید و به صورت دستی باید فیلتر بزنید
            //
            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
                "persons".ToLower(),
                "PersonList".ToLower(),
                "ByteId".ToLower()
            };
            if (request.GridSortInput.Count > 0)
            {
                isOrderBy = true;
            }
            foreach (SortData item in request.GridSortInput)
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            if (request.GridSortInput.Count == 0)
            {
                gridSortInput.Sort = "ConfigStartDate";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }
            SsrConfigAdvancedSearchGrid databseResult = await _advancedSearchGridRepository.GetLastSsrConfigGridItems(request.PageIndex, request.PageSize,
                request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                _userService.UserApplicationContext.BranchAccess.BranchCode, request.ExtraParams, cancellationToken, isOrderBy);

            var result = databseResult.Adapt<SsrConfigAdvancedSearchGridViewModel>();

            var services = databseResult.GridItems
             .Select(c =>
             {
                 // نرمال‌سازی تاریخ ها
                 string startDate = NormalizeDate(c.ConfigStartDate);
                 string endDate = NormalizeDate(c.ConfigEndDate);
                 string currentDate = NormalizeDate(persianDate);

                 // نرمال‌سازی زمان ها
                 string startTime = NormalizeTime(c.ConfigStartTime);
                 string endTime = NormalizeTime(c.ConfigEndTime);
                 string currentTime = NormalizeTime(persianTime);

                 // یک مقدار یکپارچه برای مقایسه
                 string startDT = startDate + " " + startTime;
                 string endDT = endDate + " " + endTime;
                 string currentDT = currentDate + " " + currentTime;

                 return new SsrConfigGridItem
                 {
                     Id = c.Id,
                     ConfigSubjectId = c.ConfigSubjectId,

                     IsDisabled =
                         String.Compare(currentDT, startDT) >= 0 &&
                         String.Compare(currentDT, endDT) <= 0,

                     ConfigEndDate = c.ConfigEndDate,
                     ConfigEndTime = c.ConfigEndTime,
                     ConfigMainSubjectTitle = c.ConfigMainSubjectTitle,
                     ConfigStartDate = c.ConfigStartDate,
                     ConfigState = c.ConfigState,
                     ConfigSubjectTitle = c.ConfigSubjectTitle,
                     Confirmer = c.Confirmer,
                     ConfigStartTime = c.ConfigStartTime,
                     ConditionType = c.ConditionType
                 };
             })
             .ToList();


            var diactive = services.Where(x => x.IsDisabled && x.ConditionType == "3").DistinctBy(x => x.Id).Select(c => new SsrConfigGridItemViewModel
            {
                Id = c.Id,
                ConfigSubjectId = c.ConfigSubjectId,
                ConfigEndDate = c.ConfigEndDate,
                ConfigEndTime = c.ConfigEndTime,
                ConfigMainSubjectTitle = c.ConfigMainSubjectTitle,
                ConfigStartDate = c.ConfigStartDate,
                ConfigState = "غیر فعال",
                ConfigSubjectTitle = c.ConfigSubjectTitle,
                Confirmer = c.Confirmer,
                ConfigStartTime = c.ConfigStartTime,

            }).ToList();
            apiresult.GridItems.AddRange(diactive);

            var activeservice = services.Where(x => !(diactive.Select(x => x.ConfigSubjectId)).Contains(x.ConfigSubjectId)).DistinctBy(x => x.Id).Select(c => new SsrConfigGridItemViewModel
            {
                Id = c.Id,
                ConfigSubjectId = c.ConfigSubjectId,
                ConfigEndDate = c.ConfigEndDate,
                ConfigEndTime = c.ConfigEndTime,
                ConfigMainSubjectTitle = c.ConfigMainSubjectTitle,
                ConfigStartDate = c.ConfigStartDate,
                ConfigState = "فعال",
                ConfigSubjectTitle = c.ConfigSubjectTitle,
                Confirmer = c.Confirmer,

            }).ToList();
            apiresult.GridItems.AddRange(activeservice);

            return new ApiResult<SsrConfigAdvancedSearchGridViewModel>(true, ApiResultStatusCode.Success, apiresult, new List<string> { "..عملیات با موفقیت انجام شد" });
        }

        string NormalizeDate(string d)
        {
            var p = d.Split('/');
            return $"{p[0]}/{p[1].PadLeft(2, '0')}/{p[2].PadLeft(2, '0')}";
        }

        string NormalizeTime(string t)
        {
            t = t.Substring(0, 5); // حذف ثانیه / میلی‌ثانیه
            var p = t.Split(':');
            return $"{p[0].PadLeft(2, '0')}:{p[1].PadLeft(2, '0')}";
        }

    }
}


