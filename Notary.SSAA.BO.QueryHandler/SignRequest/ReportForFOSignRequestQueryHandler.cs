using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.QueryHandler.SignRequest;
public class ReportForFOSignRequestQueryHandler : BaseQueryHandler<ReportForFOSignRequestQuery, ApiResult<ReportSignRequestViewModel>>
{
    private readonly ISignRequestRepository _signRequestRepository;
    private readonly IDateTimeService _dateTimeService;
    private Notary.SSAA.BO.Domain.Entities.SignRequest masterEntity;
    public ReportForFOSignRequestQueryHandler(IMediator mediator, IUserService userService,
        ISignRequestRepository signRequestRepository, IDateTimeService dateTimeService
        )
        : base(mediator, userService)
    {
        _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        masterEntity = new();
    }
    protected override bool HasAccess(ReportForFOSignRequestQuery request, IList<string> userRoles)
    {
        return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
    }

    protected override async Task<ApiResult<ReportSignRequestViewModel>> RunAsync(ReportForFOSignRequestQuery request, CancellationToken cancellationToken)
    {
        SignRequestViewModel result = new();
        Notary.SSAA.BO.Domain.Entities.SignRequest signRequest = null;
        ApiResult<ReportSignRequestViewModel> apiResult = new();
        if (!request.SignRequestNo.IsNullOrWhiteSpace())
        {
            signRequest = await _signRequestRepository.SignRequestTracking(request.SignRequestNo, cancellationToken);
        }
        else
        {
            signRequest = await _signRequestRepository.SignRequestTracking(request.SignRequestId.ToGuid(),_userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);
        }
        if (signRequest != null)
        {
            if (signRequest.State == "2")
            {
                if (signRequest.SignRequestFile is not null)
                {
                    ReportSignRequestViewModel reportViewModel = new();

                    if (signRequest.SignRequestFile.LastFile is null)
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("فایل گواهی امضا ساخته نشده است ، لطفا بعد از چند دقیقه دوباره تلاش کنید");
                    }
                    else
                    {
                        reportViewModel.Data = signRequest.SignRequestFile.LastFile;
                        reportViewModel.ContentType = "application/pdf";
                        reportViewModel.FileName = "Report.pdf";
                        apiResult.Data = reportViewModel;

                    }
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("فایل اسکن گواهی امضا موجود نمیباشد .");
                }

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("وضعیت گواهی امضا ، تایید شده نمیباشد.");
            }
        }
        else
        {
            apiResult.IsSuccess = false;
            apiResult.statusCode = ApiResultStatusCode.NotFound;
            apiResult.message.Add("گواهی امضا مربوطه پیدا نشد");
        }
        return apiResult;
    }

}

