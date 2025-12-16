using MediatR;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentModify;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.QueryHandler.DocumentModify
{

    public class GetDocumentByNationalNoQueryHandler : BaseQueryHandler<GetDocumentByNationalNoQuery, ApiResult<DocumentModifyViewModel>>
    {
        private readonly IDocumentRepository _DocumentModifyRepository;
        private readonly ClientConfiguration _clientConfiguration;


        public GetDocumentByNationalNoQueryHandler(IMediator mediator, IUserService userService, IDocumentRepository DocumentModifyRepository, ClientConfiguration clientConfiguration)
            : base(mediator, userService)
        {
            _DocumentModifyRepository = DocumentModifyRepository ?? throw new ArgumentNullException(nameof(DocumentModifyRepository));
            _clientConfiguration = clientConfiguration;
        }
        protected override bool HasAccess(GetDocumentByNationalNoQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentModifyViewModel>> RunAsync(GetDocumentByNationalNoQuery request, CancellationToken cancellationToken)
        {

            DocumentModifyViewModel result = new();
            ApiResult<DocumentModifyViewModel> apiResult = new();
            Domain.Entities.Document Document = await _DocumentModifyRepository.GetAsync(x => request.NationalNo == x.NationalNo && x.ScriptoriumId == _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (Document != null)
            {
    //            if (
    //string.Compare(Document.DocumentDate, _clientConfiguration.ENoteBookEnabledDate) > 0
    //)
    //            {
    //                apiResult.message.Add("این سند پس از راه اندازی دفترالکترونیک تنظیم شده است. امکان اصلاح اطلاعات ثبت در دفتر برای این سند مقدور نمی باشد.");


    //            }
                if (
                 Document.State != NotaryRegServiceReqState.Finalized.GetString() &&
                 Document.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString() &&
                 Document.State != NotaryRegServiceReqState.FinalPrinted.GetString()
                 )
                {
                    apiResult.message.Add("امکان اصلاح اطلاعات ثبت در دفتر برای این سند در وضعیت فعلی مقدور نمی باشد. ");
                }
                if (apiResult.message.Count > 0)
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    return apiResult;
                }
                result = DocumentModifyMapper.ToViewModel(Document);
                result.Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("سابقه اصلاح اطلاعات ثبت سند در دفتر پیدا نشد");
            }
            return apiResult;
        }

    }
}
