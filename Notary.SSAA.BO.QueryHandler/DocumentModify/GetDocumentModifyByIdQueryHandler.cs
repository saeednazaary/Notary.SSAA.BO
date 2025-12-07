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
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.DocumentModify
{
    public class GetDocumentModifyByIdQueryHandler : BaseQueryHandler<GetDocumentModifyByIdQuery, ApiResult<DocumentModifyViewModel>>
    {
        private readonly ISsrDocModifyClassifyNoRepository _DocumentModifyRepository;
        private readonly ClientConfiguration _clientConfiguration;


        public GetDocumentModifyByIdQueryHandler(IMediator mediator, IUserService userService, ISsrDocModifyClassifyNoRepository DocumentModifyRepository, ClientConfiguration clientConfiguration)
            : base(mediator, userService)
        {
            _DocumentModifyRepository = DocumentModifyRepository ?? throw new ArgumentNullException(nameof(DocumentModifyRepository));
            _clientConfiguration = clientConfiguration;
        }
        protected override bool HasAccess(GetDocumentModifyByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentModifyViewModel>> RunAsync(GetDocumentModifyByIdQuery request, CancellationToken cancellationToken)
        {

            DocumentModifyViewModel result = new();
            ApiResult<DocumentModifyViewModel> apiResult = new();
            Domain.Entities.SsrDocModifyClassifyNo DocumentModify = await _DocumentModifyRepository.GetDocumentModify(request.DocumentModifyId.ToGuid(), _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
            if (DocumentModify != null && DocumentModify.Document!=null)
            {
//                if (
//string.Compare(DocumentModify.Document.DocumentDate, _clientConfiguration.ENoteBookEnabledDate) > 0
//)
//                {
//                    apiResult.message.Add("این سند پس از راه اندازی دفترالکترونیک تنظیم شده است. امکان اصلاح اطلاعات ثبت در دفتر برای این سند مقدور نمی باشد.");


//                }
                if (
                 DocumentModify.Document.State != NotaryRegServiceReqState.Finalized.GetString() &&
                 DocumentModify.Document.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString() &&
                 DocumentModify.Document.State != NotaryRegServiceReqState.FinalPrinted.GetString()
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
                result = DocumentModifyMapper.ToViewModel(DocumentModify);
                result.Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;

                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("سابقه اصلاح اطلاعات ثبت سند در دفتر پیدا نشد");
            }
            return apiResult; ;
        }

    }
}
