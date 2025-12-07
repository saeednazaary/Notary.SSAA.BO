using MediatR;
using Newtonsoft.Json;
using Notary.ExternalServices.WebApi.Models.RequestsModel.Person;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.SsrSignEbookBaseInfo
{
    public class GetSsrSignEbookBaseInfoByIdQueryHandler : BaseQueryHandler<GetSsrSignEbookBaseInfoByIdQuery, ApiResult<SsrSignEbookBaseInfoViewModel>>
    {
        private IRepository<SsrSignEbookBaseinfo> _ssrSignEbookBaseinfoRepository;


        public GetSsrSignEbookBaseInfoByIdQueryHandler(IMediator mediator, IUserService userService, IRepository<SsrSignEbookBaseinfo> ssrSignEbookBaseinfoRepository)
            : base(mediator, userService)
        {
            _ssrSignEbookBaseinfoRepository = ssrSignEbookBaseinfoRepository;

        }
        protected override bool HasAccess(GetSsrSignEbookBaseInfoByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<SsrSignEbookBaseInfoViewModel>> RunAsync(GetSsrSignEbookBaseInfoByIdQuery request, CancellationToken cancellationToken)
        {

            SsrSignEbookBaseInfoViewModel result = new();
            ApiResult<SsrSignEbookBaseInfoViewModel> apiResult = new();
            Domain.Entities.SsrSignEbookBaseinfo ssrSignEbookBaseinfo = await _ssrSignEbookBaseinfoRepository.GetAsync(s => s.ScriptoriumId == _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);


            if (ssrSignEbookBaseinfo != null)
            {


                result = SsrSignEbookBaseinfoMapper.ToViewModel(ssrSignEbookBaseinfo);
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("اطلاعات پایه دفتر الكترونیك گواهی امضاء مربوطه یافت نشد");
            }
            return apiResult; ;
        }

    }
}
