using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentEbookBaseInfo;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.DocumentEbookBaseInfo
{
    public class GetDocumentEbookBaseInfoByIdQueryHandler : BaseQueryHandler<GetDocumentEbookBaseInfoByIdQuery, ApiResult<DocumentEbookBaseInfoViewModel>>
    {
        private IRepository<DocumentElectronicBookBaseinfo> _DocumentEbookBaseInfoRepository;
        private IDocumentRepository _document;


        public GetDocumentEbookBaseInfoByIdQueryHandler(IMediator mediator, IUserService userService, IRepository<DocumentElectronicBookBaseinfo> DocumentEbookBaseInfoRepository, IDocumentRepository Document)
            : base(mediator, userService)
        {
            _DocumentEbookBaseInfoRepository = DocumentEbookBaseInfoRepository;
            _document = Document;

        }
        protected override bool HasAccess(GetDocumentEbookBaseInfoByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentEbookBaseInfoViewModel>> RunAsync(GetDocumentEbookBaseInfoByIdQuery request, CancellationToken cancellationToken)
        {

            DocumentEbookBaseInfoViewModel result = new();
            ApiResult<DocumentEbookBaseInfoViewModel> apiResult = new();
            Domain.Entities.DocumentElectronicBookBaseinfo DocumentEbookBaseInfo = await _DocumentEbookBaseInfoRepository.GetAsync(s => s.ScriptoriumId == _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);


            if (DocumentEbookBaseInfo != null)
            {   
                result = DocumentEbookBaseInfoMapper.ToViewModel(DocumentEbookBaseInfo);
                apiResult.Data = result;
            }
            else
            {
                var branchCode = _userService.UserApplicationContext.BranchAccess.BranchCode;
                int MaxClassifyNo = await _document.GetLastClassifyNoFromDocs(branchCode, cancellationToken);
                result.LastClassifyNo = MaxClassifyNo.ToString();
                apiResult.Data = result;
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("اطلاعات پایه دفتر الكترونیك سند  مربوطه یافت نشد");
            }
            return apiResult; ;
        }

    }
}
