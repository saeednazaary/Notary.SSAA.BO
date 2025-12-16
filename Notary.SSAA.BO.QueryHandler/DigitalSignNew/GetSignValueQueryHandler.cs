using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;


namespace Notary.SSAA.BO.QueryHandler.DigitalSignNew
{
    public class GetSignValueQueryHandler : BaseQueryHandler<GetSignValueQuery, ApiResult<GetSignValueViewModel>>
    {
        private readonly IRepository<DigitalSignatureValue> _digitalSignatureValueRepository;
        private readonly IDateTimeService _dateTimeService;
        public GetSignValueQueryHandler(IMediator mediator, IUserService userService, IRepository<DigitalSignatureValue> digitalSignatureValueRepository, IDateTimeService dateTimeService)
            : base(mediator, userService)
        {
            _digitalSignatureValueRepository = digitalSignatureValueRepository;
            _dateTimeService = dateTimeService;
        }
        protected override bool HasAccess(GetSignValueQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetSignValueViewModel>> RunAsync(GetSignValueQuery request, CancellationToken cancellationToken)
        {
            GetSignValueViewModel result = new();
            ApiResult<GetSignValueViewModel> apiResult = new()
            {
                statusCode = ApiResultStatusCode.Success,
                IsSuccess = true
            };
            var digitalSignatureValue = await _digitalSignatureValueRepository
                                              .TableNoTracking
                                              .Where(x => x.Id == request.Id.ToGuid())
                                              .FirstOrDefaultAsync(cancellationToken);
            if (digitalSignatureValue != null)
            {
                var expireDatetime = (digitalSignatureValue.ExpireDate + "-" + digitalSignatureValue.ExpireTime).ToDateTime();
                var currentDatetime = _dateTimeService.CurrentDateTime;
                if (expireDatetime < currentDatetime)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("امضای دیجیتال مربوطه منقضی شده است .");
                }
                else
                {
                    var verifyInput = new VerifyNewSignQuery() { Certificate = digitalSignatureValue.Certificate, HandlerInput = digitalSignatureValue.DataHandlerInput, HandlerName = digitalSignatureValue.DataHandlerName, Sign = digitalSignatureValue.SignValue, SaveSignValue = false, MainObjectId = digitalSignatureValue.MainObjectId };
                    var verifiyResult = await _mediator.Send(verifyInput, cancellationToken);
                    if (verifiyResult.IsSuccess && verifiyResult.Data != null && verifiyResult.Data.Result)
                    {
                        result.Certificate = digitalSignatureValue.Certificate;
                        result.SignValue = digitalSignatureValue.SignValue;
                        result.MainObjectId = digitalSignatureValue.MainObjectId;
                        result.Id = request.Id;
                        result.RawDataBase64 = verifiyResult.Data.RawDataBase64;
                        await _digitalSignatureValueRepository.DeleteAsync(digitalSignatureValue, cancellationToken,false);
                        apiResult.Data = result;
                    }
                    else
                    {
                        result.Certificate = digitalSignatureValue.Certificate;
                        result.SignValue = digitalSignatureValue.SignValue;
                        result.MainObjectId = digitalSignatureValue.MainObjectId;
                        result.Id = request.Id;
                        //result.RawDataBase64 = verifiyResult.Data.RawDataBase64;
                        apiResult.Data = result;
                        apiResult.IsSuccess = true;
                        apiResult.message.Add("تصدیق امضای دیجیتال مربوطه با موفقیت انجام نشد");
                    }
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("با شناسه ورودی امضای دیجیتال یافت نشد");
            }
            return apiResult;
        }

    }
}
