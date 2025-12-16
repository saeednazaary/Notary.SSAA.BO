using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Data;


namespace Notary.SSAA.BO.QueryHandler.ExternalUserAuthenticate
{
    public class ExternalUserAuthenticateQueryHandler : BaseExternalQueryHandler<ExternalUserAuthenticateQuery, ExternalApiResult>
    {
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private ExternalApiResult apiResult;

        public ExternalUserAuthenticateQueryHandler(IMediator mediator, IUserService userService,IRepository<SsrApiExternalUser> ssrApiExternalUser) 
            : base(mediator, userService)
        {
            _ssrApiExternalUser = ssrApiExternalUser ?? throw new ArgumentNullException(nameof(ssrApiExternalUser));
            apiResult = new();
        }

        protected override async Task<ExternalApiResult> RunAsync(ExternalUserAuthenticateQuery request, CancellationToken cancellationToken)
        {
            LastPersonFingerprintViewModel inquiryFingerprintResult = new();
            apiResult.ResCode = "1";
            if (string.IsNullOrWhiteSpace(request.UserName) )
            {
                apiResult.ResMessage = "نام کاربری وارد نشده است.";
                apiResult.ResCode = "110";
                return apiResult;
            }
            if ( string.IsNullOrWhiteSpace(request.Password))
            {
                apiResult.ResMessage = "رمز عبور وارد نشده است.";
                apiResult.ResCode = "111";
                return apiResult;
            }
            var user = await _ssrApiExternalUser.TableNoTracking.Where(x => x.UserName == request.UserName && x.UserPassword == request.Password && x.State == "1")
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                apiResult.ResMessage = "نام کاربری یا رمز عبور وارد شده معتبر نمیباشد";
                apiResult.ResCode = "103";
                return apiResult;
            }  
            return apiResult;
        }

        protected override bool HasAccess(ExternalUserAuthenticateQuery request, IList<string> userRoles)
        {
            return true;
        }


    }
}
