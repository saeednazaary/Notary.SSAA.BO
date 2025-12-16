using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.DocumentPayments
{
    public class DocumentPaymentsConfirmQueryHandler : BaseQueryHandler<DocumentPaymentsConfirmQuery, ApiResult<DocumentPaymentQuestionViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IConfiguration _configuration;
        private readonly IDocumentInquiryRepository _documentInquiryRepository;
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IDocumentElectronicBookBaseInfoRepository _documentElectronicBookBaseInfoRepository;
        private readonly IDocumentElectronicBookRepository _documentElectronicBookRepository;

        public DocumentPaymentsConfirmQueryHandler(IMediator mediator, IUserService userService, IDocumentRepository documentRepository,
            IDocumentInquiryRepository documentInquiryRepository,
            IDocumentElectronicBookBaseInfoRepository documentElectronicBookBaseInfoRepository,
            IDocumentElectronicBookRepository documentElectronicBookRepository,
             ClientConfiguration clientConfiguration,
             IConfiguration configuration
            ) : base(mediator, userService)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _clientConfiguration = clientConfiguration;
            _documentInquiryRepository = documentInquiryRepository;
            _userService = userService;
            _documentElectronicBookBaseInfoRepository = documentElectronicBookBaseInfoRepository;
            _documentElectronicBookRepository = documentElectronicBookRepository;
            _configuration = configuration;
        }
        protected override bool HasAccess(DocumentPaymentsConfirmQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentPaymentQuestionViewModel>> RunAsync(DocumentPaymentsConfirmQuery request, CancellationToken cancellationToken)
        {
            DocumentPaymentsConfirmQueryViewModel result = new();
            ApiResult<DocumentPaymentQuestionViewModel> apiResult = new();
            Domain.Entities.Document document = await _documentRepository.GetDocumentPayments(request.DocumentId.ToGuid(), cancellationToken);
            Domain.Entities.Document documentCosts = await _documentRepository.GetDocumentCostsAndCostUnchange(request.DocumentId.ToGuid(), cancellationToken);

            if (document != null)
            {
                string[] EnableState = new string[] { DocumentState.CreateDocumentId.ToAssignedValue(), DocumentState.BackupReport.ToAssignedValue() };


                if (!(document.State != null && EnableState.Contains(document.State)))
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("تأیید هزینه ها در این مرحله از پرونده ممکن نیست.");
                    return apiResult;
                }


                if (_configuration.GetValue<string>("PublishEnv") == null || (_configuration.GetValue<string>("PublishEnv") != null && _configuration.GetValue<string>("PublishEnv") == PublishEnv.Blue))
                {
                    if (document.DocumentPayments.Count == 0)
                    {

                        apiResult.IsSuccess = false;
                        apiResult.message.Add("هیچ پرداختی برای این سند ثبت نشده است.");
                        return apiResult;
                    }

                    if (document.DocumentPayments.Sum(x => x.Price) == 0 || documentCosts.SumPrices() == 0)
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("هزینه‌های سند ثبت نشده است");
                        return apiResult;
                    }
                }

                apiResult.Data = new DocumentPaymentQuestionViewModel() { IsCostPaymentConfirmed = true };
                apiResult.IsSuccess = true;
                apiResult.message.Add("اطلاعات سند برای تایید معتبر است.");

                return apiResult;
            }
            else
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("سند مربوطه یافت نشد");
                return apiResult;
            }
        }
    }
}