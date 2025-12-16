using MediatR;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Grids.Estate
{
    public class EstateTaxInquirySelectableGridQueryHandler : BaseQueryHandler<EstateTaxInquirySelectableGridQuery, ApiResult<EstateTaxInquiryGrid>>
    {
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;

        public EstateTaxInquirySelectableGridQueryHandler(IMediator mediator, IUserService userService,
            IEstateTaxInquiryRepository selectableGridRepository)
            : base(mediator, userService)
        {
            _estateTaxInquiryRepository = selectableGridRepository ?? throw new ArgumentNullException(nameof(selectableGridRepository));
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }

        protected override bool HasAccess(EstateTaxInquirySelectableGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<EstateTaxInquiryGrid>> RunAsync(EstateTaxInquirySelectableGridQuery request, CancellationToken cancellationToken)
        {
            EstateTaxInquiryGrid result = new();
            IDictionary<string, string> searchMap = new Dictionary<string, string>();
            bool isOrderBy = false;
            SortData gridSortInput = new();

            List<string> FieldsNotInFilterSearch = new();

            if (request.GridSortInput.Count > 0)
            {
                isOrderBy = true;
            }
            foreach (var item in request.GridSortInput)
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            if (request.GridSortInput.Count == 0)
            {
                gridSortInput.Sort = "InquiryDate";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }
            var dic = ToDictionary(request.ExtraParams);
            result = await _estateTaxInquiryRepository.GetEstateTaxInquiryGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                _userService.UserApplicationContext.BranchAccess.BranchCode, isOrderBy, dic, cancellationToken);

            if (result.GridItems != null && result.GridItems.Count > 0)
            {
                var unitInfo = await _baseInfoServiceHelper.GetUnitById(result.GridItems.Select(x => x.EstateUnitTitle).ToArray(), cancellationToken);
                foreach (var obj in result.GridItems)
                {
                    obj.RelatedServer = "BO";
                    var unit = unitInfo.UnitList.Where(u => u.Id == obj.EstateUnitTitle).FirstOrDefault();
                    if (unit != null)
                        obj.EstateUnitTitle = unit.Name;
                    else
                        obj.EstateUnitTitle = "";
                }
            }
            if (result.SelectedItems != null && result.SelectedItems.Count > 0)
            {
                var unitInfo = await _baseInfoServiceHelper.GetUnitById(result.SelectedItems.Select(x => x.EstateUnitTitle).ToArray(), cancellationToken);
                foreach (var obj in result.SelectedItems)
                {
                    obj.RelatedServer = "BO";
                    var unit = unitInfo.UnitList.Where(u => u.Id == obj.EstateUnitTitle).FirstOrDefault();
                    if (unit != null)
                        obj.EstateUnitTitle = unit.Name;
                    else
                        obj.EstateUnitTitle = "";
                }
            }
            return new ApiResult<EstateTaxInquiryGrid>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }

        private static Dictionary<string, object> ToDictionary(object obj)
        {
            Dictionary<string, object> r = new();
            var type = obj.GetType();
            var piLst = type.GetProperties();
            foreach (var pi in piLst)
            {
                var val = pi.GetValue(obj, null);
                if (val != null)
                {
                    r.Add(pi.Name, val);
                }
            }
            return r;
        }
        
    }
}
