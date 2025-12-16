using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.Mappers.InquiryFromUnit;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal sealed class InquiryFromUnitGridQueryHandler : BaseQueryHandler<InquiryFromUnitGridQuery, ApiResult<InquiryFromUnitGridViewModel>>
    {
        private readonly IInquiryFromUnitRepository _inquiryFromUnitRepository;
        private IConfiguration _configuration;
        private IHttpEndPointCaller _httpEndPointCaller;

        public InquiryFromUnitGridQueryHandler(IMediator mediator, IUserService userService, IInquiryFromUnitRepository inquiryFromUnitRepository,
           IConfiguration configuration, IHttpEndPointCaller httpEndPointCaller) : base(mediator, userService)
        {
            _inquiryFromUnitRepository = inquiryFromUnitRepository;
            _configuration = configuration;
            _httpEndPointCaller = httpEndPointCaller;
        }

        protected override bool HasAccess(InquiryFromUnitGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<InquiryFromUnitGridViewModel>> RunAsync(InquiryFromUnitGridQuery request, CancellationToken cancellationToken)
        {
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
            string baseInfoUrl = _configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "api/v1/Specific/Ssar/";

            ApiResult<GetUnitByIdViewModel> apiResultUnit = new ();

            bool isOrderBy = false;

            SortData gridSortInput = new();

            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
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
                gridSortInput.Sort = "inquiryno";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }



            var databaseResult = await _inquiryFromUnitRepository.GetInquiryFromUnitGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, 
                gridSortInput,request.SelectedItems, FieldsNotInFilterSearch, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken,isOrderBy);

            var unitIds = databaseResult.GridItems.Select(x => x.UnitId).ToList();
            unitIds.AddRange(databaseResult.SelectedItems.Select(x => x.UnitId).ToList());

            if (unitIds.Count>0)
                apiResultUnit = await _httpEndPointCaller.CallPostApiAsync<GetUnitByIdViewModel, GetUnitByIdQuery>(new HttpEndpointRequest<GetUnitByIdQuery>(baseInfoUrl + "Common/GetUnitById",
                    _userService.UserApplicationContext.Token, new GetUnitByIdQuery(unitIds.Distinct().ToArray())), cancellationToken);
            
            InquiryFromUnitGridViewModel result = new();

            result = InquiryFromUnitMapper.ToViewModel(databaseResult);

            foreach (var item in databaseResult.GridItems)
            {
                var addingItem = InquiryFromUnitMapper.ToItem(item);

                if (request.SelectedItems.Contains(item.Id))
                    addingItem.IsSelected = true;

                addingItem.UnitTitle = apiResultUnit.Data.UnitList
                    .Where(x => x.Id == item.UnitId)
                    .Select(x => x.Name)
                    .FirstOrDefault();

                addingItem.StateTitle = item.State switch
                {
                    "1" => "پرونده ایجاد شده است",
                    "2" => "تأیید نهایی شده است",
                    "3" => "پرونده بسته شده است",
                    _ => "وضعیت نامشخص است",
                };

                result.GridItems.Add(addingItem);
            }

            foreach (var item in databaseResult.SelectedItems)
            {
                var addingItem = InquiryFromUnitMapper.ToItem(item);

                addingItem.UnitTitle = apiResultUnit.Data.UnitList
                    .Where(x => x.Id == item.UnitId)
                    .Select(x => x.Name)
                    .FirstOrDefault();

                addingItem.StateTitle = item.State switch
                {
                    "1" => "پرونده ایجاد شده است",
                    "2" => "تأیید نهایی شده است",
                    "3" => "پرونده بسته شده است",
                    _ => "وضعیت نامشخص است",
                };

                result.SelectedItems.Add(addingItem);
            }

            return result;
        }
    }
}
