using Mapster;
using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentModify;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentModify;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;
namespace Notary.SSAA.BO.CommandHandler.DocumentModify.Handlers
{

    public class CreateDocumentModifyCommandHandler : BaseCommandHandler<CreateDocumentModifyCommand, ApiResult>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDateTimeService _dateTimeService;
        private Domain.Entities.Document masterEntity;
        private readonly ApiResult<DocumentModifyViewModel> apiResult;
        private readonly ClientConfiguration _clientConfiguration;



        public CreateDocumentModifyCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, IDocumentRepository documentRepository, ClientConfiguration clientConfiguration,
            ILogger logger)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            masterEntity = new();
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _clientConfiguration = clientConfiguration;

        }
        protected override bool HasAccess(CreateDocumentModifyCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateDocumentModifyCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _documentRepository.GetDocumentModify(request.DocumentId.ToGuid(), cancellationToken);
            await BusinessValidation(request, cancellationToken);

            if (apiResult.IsSuccess)
            {
                Guid DocumentModifyId = UpdateDatabase(request);

                await _documentRepository.UpdateAsync(masterEntity, cancellationToken);
                ApiResult<DocumentModifyViewModel> getResponse = await _mediator.Send(new GetDocumentModifyByIdQuery(DocumentModifyId.ToString()), cancellationToken);
                if (getResponse.IsSuccess)
                {

                    apiResult.Data = getResponse.Data.Adapt<DocumentModifyViewModel>();
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = getResponse.statusCode;
                    apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");
                    apiResult.message = getResponse.message;
                }
            }

            return apiResult;

        }
        private Guid UpdateDatabase(CreateDocumentModifyCommand request)
        {

            masterEntity.ClassifyNo = request.ClassifyNoNew.ToInt();
            masterEntity.BookPapersNo = request.RegisterPagesNoNew;
            masterEntity.BookVolumeNo = request.RegisterVolumeNoNew;
            if (!string.IsNullOrEmpty(request.WriteInBookDateNew))
            {
                masterEntity.WriteInBookDate = request.WriteInBookDateNew;
            }
            // مقدار اولیه توضیحات
            if (string.IsNullOrEmpty(masterEntity.DocumentInfoText.DocumentModifyDescription))
                masterEntity.DocumentInfoText.DocumentModifyDescription = string.Empty;

            // تغییر جلد دفتر
            if (request.RegisterVolumeNoNew != request.RegisterVolumeNoOld)
            {
                if (!string.IsNullOrEmpty(masterEntity.DocumentInfoText.DocumentModifyDescription))
                    masterEntity.DocumentInfoText.DocumentModifyDescription += "\n";

                masterEntity.DocumentInfoText.DocumentModifyDescription +=
                    "با توجه به اظهار سردفتر در تاریخ " + _dateTimeService.CurrentPersianDate +
                    " شماره جلد دفتر " + request.RegisterVolumeNoNew + " صحیح می باشد.";
            }

            // تغییر صفحات دفتر
            if (request.RegisterPagesNoNew != request.RegisterPagesNoOld)
            {
                if (!string.IsNullOrEmpty(masterEntity.DocumentInfoText.DocumentModifyDescription))
                    masterEntity.DocumentInfoText.DocumentModifyDescription += "\n";

                masterEntity.DocumentInfoText.DocumentModifyDescription +=
                    "با توجه به اظهار سردفتر در تاریخ " + _dateTimeService.CurrentPersianDate +
                    " شماره صفحات دفتر " + request.RegisterPagesNoNew + " صحیح می باشد.";
            }

            // تغییر شماره ترتیب سند
            if (request.ClassifyNoNew != request.ClassifyNoOld)
            {
                if (!string.IsNullOrEmpty(masterEntity.DocumentInfoText.DocumentModifyDescription))
                    masterEntity.DocumentInfoText.DocumentModifyDescription += "\n";

                masterEntity.DocumentInfoText.DocumentModifyDescription +=
                    "با توجه به اظهار سردفتر در تاریخ " + _dateTimeService.CurrentPersianDate +
                    " شماره ترتیب سند " + request.ClassifyNoNew + " صحیح می باشد.";
            }

            // تغییر تاریخ ثبت در دفتر
            if (request.WriteInBookDateNew != request.WriteInBookDateOld)
            {
                if (!string.IsNullOrEmpty(masterEntity.DocumentInfoText.DocumentModifyDescription))
                    masterEntity.DocumentInfoText.DocumentModifyDescription += "\n";

                masterEntity.DocumentInfoText.DocumentModifyDescription +=
                    "با توجه به اظهار سردفتر در تاریخ " + _dateTimeService.CurrentPersianDate +
                    " تاریخ ثبت در دفتر " + request.WriteInBookDateNew + " صحیح می باشد.";
            }


            SsrDocModifyClassifyNo newDocumentModify = new();
            newDocumentModify.Id = Guid.NewGuid();
            newDocumentModify.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
            newDocumentModify.Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;
            newDocumentModify.ModifyDate = _dateTimeService.CurrentPersianDate;
            newDocumentModify.ModifyTime = _dateTimeService.CurrentTime;
            DocumentModifyMapper.ToEntity(ref newDocumentModify, request);
            if (!string.IsNullOrEmpty(request.WriteInBookDateNew))
            {
                newDocumentModify.WriteInBookDateNew = request.WriteInBookDateNew;
            }
            else
            {
                newDocumentModify.WriteInBookDateNew = request.WriteInBookDateOld;
            }
            masterEntity.SsrDocModifyClassifyNos.Add(newDocumentModify);

            return newDocumentModify.Id;

        }
        private async Task BusinessValidation(CreateDocumentModifyCommand request, CancellationToken cancellationToken)
        {
            if (masterEntity != null)
            {
                await _documentRepository.LoadCollectionAsync(masterEntity, q => q.SsrDocModifyClassifyNos, cancellationToken);
                await _documentRepository.LoadReferenceAsync(masterEntity, q => q.DocumentInfoText, cancellationToken);
                //if (
                //    string.Compare(masterEntity.DocumentDate, _clientConfiguration.ENoteBookEnabledDate) > 0
                //    )
                //{
                //    apiResult.message.Add("این سند پس از راه اندازی دفترالکترونیک تنظیم شده است. امکان اصلاح اطلاعات ثبت در دفتر برای این سند مقدور نمی باشد.");


                //}
                if (
                 masterEntity.State != NotaryRegServiceReqState.Finalized.GetString() &&
                 masterEntity.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString() &&
                 masterEntity.State != NotaryRegServiceReqState.FinalPrinted.GetString()
                 )
                {
                    apiResult.message.Add("امکان اصلاح اطلاعات ثبت در دفتر برای این سند در وضعیت فعلی مقدور نمی باشد. ");
                }
            }
            else
            {
                apiResult.message.Add("جدول مربوطه یافت نشد ");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }

    }
}


