using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Text;
namespace Notary.SSAA.BO.QueryHandler.SignRequest
{
    internal class GetKatebSignRequestByIdQueryHandler : BaseQueryHandler<GetKatebSignRequestByIdQuery, ApiResult<KatebSignRequestViewModel>>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private ApiResult<KatebSignRequestViewModel> apiResult;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public GetKatebSignRequestByIdQueryHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService)
        {
            _signRequestRepository = signRequestRepository;
            apiResult = new();
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected override bool HasAccess(GetKatebSignRequestByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin);
        }

        protected override async Task<ApiResult<KatebSignRequestViewModel>> RunAsync(GetKatebSignRequestByIdQuery request, CancellationToken cancellationToken)
        {
            SSAA.BO.Domain.Entities.SignRequest signRequest = await _signRequestRepository.GetKatebSignRequesById(request.Id, cancellationToken);
            if (signRequest == null)
            {
                apiResult.message.Add("گواهی امضا یافت نشد");
                apiResult.IsSuccess = false;
                return apiResult;
            }
            
            apiResult.Data = SignRequestMapper.ToKatebSignRequestViewModel(signRequest);
            if (signRequest.IsCostPaid != "1")
            {
                UpdateSignRequestPaymentStateCommand paymentStateCommand = new();
                paymentStateCommand.SignRequestNo = signRequest.ReqNo;
                paymentStateCommand.InquiryMode = true;
                var paymentInquiryResult = await _mediator.Send(paymentStateCommand, cancellationToken);
                if (paymentInquiryResult.IsSuccess)
                {
                    apiResult.Data.PaymentLink = paymentInquiryResult.Data.PaymentLink;
                    apiResult.Data.ReceiptNo = paymentInquiryResult.Data.BillNo;
                    apiResult.Data.SumPrices = paymentInquiryResult.Data.SumPrices;
                    apiResult.Data.IsCostPaid = paymentInquiryResult.Data.IsCostPaid;
                }
                apiResult.message.Add(JsonConvert.SerializeObject(paymentInquiryResult));
            }
          
            apiResult.message.Add(signRequest.IsCostPaid);
            if (!signRequest.SignCertificateDn.IsNullOrWhiteSpace())
            {
                var sardaftarNationalNo = ExtractSerialNumber(signRequest.SignCertificateDn);

                var post = signRequest.SignCertificateDn != null &&
                           signRequest.NationalNo != null &&
                            signRequest.SignCertificateDn.Length >= 5 &&
                           signRequest.NationalNo.Length >= 13 &&
                            signRequest.SignCertificateDn[^5..] == signRequest.NationalNo.Substring(8, 5)
                           ? "ASIL" : "KAFIL";

                apiResult.Data.SardaftarNationaNo = sardaftarNationalNo;
                apiResult.Data.SardaftarPost = post;
            }

            return apiResult;
        }
        public static string ExtractSerialNumber(string dn)
        {
            if (string.IsNullOrEmpty(dn))
                return null;

            var index = dn.IndexOf("SERIALNUMBER=", StringComparison.OrdinalIgnoreCase);
            if (index < 0 || index + 13 >= dn.Length)
                return null;

            var start = index + 13;
            var length = Math.Min(10, dn.Length - start);
            return dn.Substring(start, length);
        }
    }
}
