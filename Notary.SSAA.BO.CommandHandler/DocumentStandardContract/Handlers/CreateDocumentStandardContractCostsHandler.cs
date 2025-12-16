using Mapster;
using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Shell;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.FinalVerificationManager;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;

namespace Notary.SSAA.BO.CommandHandler.DocumentStandardContract.Handlers
{
    public class CreateDocumentStandardContractCostsHandler : BaseCommandHandler<CreateDocumentStandardContractCostsCommand, ApiResult>
    {
        private readonly IDocumentRepository _DocumentRepository;
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IDateTimeService _dateTimeService;
        private Domain.Entities.Document masterEntity;
        private readonly ApiResult<DocumentStandardContractViewModel> apiResult;
        private readonly SmartQuotaGeneratorEngine _smartQuotaGeneratorEngine;
        private readonly RegisterServiceRequestVerifier _registerServiceRequestVerifier;
        private readonly QuotasValidator _quotasValidator;

        public CreateDocumentStandardContractCostsHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, IDocumentRepository documentRepository,
            ILogger logger, ClientConfiguration clientConfiguration, SmartQuotaGeneratorEngine smartQuotaGeneratorEngine, RegisterServiceRequestVerifier registerServiceRequestVerifier, QuotasValidator quotasValidator)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _DocumentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _clientConfiguration = clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));
            _smartQuotaGeneratorEngine = smartQuotaGeneratorEngine ?? throw new ArgumentNullException(nameof(smartQuotaGeneratorEngine));
            _registerServiceRequestVerifier = registerServiceRequestVerifier ?? throw new ArgumentNullException(nameof(registerServiceRequestVerifier));
            _quotasValidator = quotasValidator ?? throw new ArgumentNullException(nameof(quotasValidator));
        }

        protected override bool HasAccess(CreateDocumentStandardContractCostsCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateDocumentStandardContractCostsCommand request, CancellationToken cancellationToken)
        {
            string messageText = string.Empty;
            masterEntity = await _DocumentRepository
                          .GetDocumentById(request.DocumentId.ToGuid(), ["DocumentCosts", "DocumentEstates", "DocumentInquiries", "DocumentPeople"], cancellationToken);

            ONotaryRegisterServiceReqOutputMessage output = new();

            #region DocPersonsValidation
            (bool resultValidateDocPersonsCollectionResult, messageText) = await _registerServiceRequestVerifier.ValidateDocPersonsCollection([.. masterEntity.DocumentPeople], masterEntity, messageText);
            if (!resultValidateDocPersonsCollectionResult)
            {
                ApiResult<DocumentStandardContractCostsViewModel> getRegisterReqPrices = new()
                {
                    IsSuccess = false
                };
                getRegisterReqPrices.message.Add(messageText);
                return getRegisterReqPrices;


            }
            #endregion

            #region DSUPermission
            if (!DsuUtility.IsDSUGeneratingPermitted(ref messageText, masterEntity, [.. masterEntity.DocumentInquiries.Select(t => new DocumentInquiryInformation
            { EstateInquiryId = t.EstateInquiriesId, InquiryOrganizationId = t.DocumentInquiryOrganizationId })], true, _clientConfiguration))
            {
                if (!string.IsNullOrWhiteSpace(messageText))
                {
                    ApiResult<DocumentStandardContractCostsViewModel> getRegisterReqPrices = new()
                    {
                        IsSuccess = false
                    };
                    getRegisterReqPrices.message.Add(messageText);
                    return getRegisterReqPrices;
                }
            }
            #endregion
            #region QuotaGenerating
            if (IntelligantDocumentInheritor.IsAutoQuotaGeneratingPermitted(masterEntity, _clientConfiguration, ref messageText))
            {
                (bool result, masterEntity, messageText) = await _smartQuotaGeneratorEngine.GenerateQuotas(masterEntity, cancellationToken, messageText, false);

                if (!result)
                {
                    ApiResult<DocumentStandardContractCostsViewModel> getRegisterReqPrices = new()
                    {
                        IsSuccess = false
                    };
                    getRegisterReqPrices.message.Add(messageText); ;
                    return getRegisterReqPrices;
                }
            }
            #endregion

            #region QuotaValidating
            bool verifyRegCasesInquiriesAndQuotas = _quotasValidator.VerifyRegCasesInquiriesAndQuotas(masterEntity, ref messageText);
            if (!verifyRegCasesInquiriesAndQuotas)
            {
                ApiResult<DocumentStandardContractCostsViewModel> getRegisterReqPrices = new()
                {
                    IsSuccess = false
                };
                getRegisterReqPrices.message.Add(messageText); ;
                return getRegisterReqPrices;
            }
            if (masterEntity.DocumentType.Code == "611")
            {
                bool verified = _quotasValidator.VerifySeparation(masterEntity, ref messageText);
                if (!verified)
                {
                    ApiResult<DocumentStandardContractCostsViewModel> getRegisterReqPrices = new()
                    {
                        IsSuccess = false
                    };
                    getRegisterReqPrices.message.Add(messageText); ;
                    return getRegisterReqPrices;
                }
            }
            #endregion


            if (masterEntity != null)
            {
                #region DocumentCost_RegisterReqPricesBySystem محاسبه ی هزینه توسط سامانه و ایجاد هزینه ها توسط سامانه

                ApiResult<DocumentStandardContractCostsViewModel> getRegisterReqPrices = await GetEpaymentDocumentCosts(masterEntity, cancellationToken);
                if (getRegisterReqPrices.IsSuccess)
                {
                    UpdateDocumentCost(getRegisterReqPrices.Data);


                    await _DocumentRepository.UpdateAsync(masterEntity, cancellationToken, true);
                    ApiResult<DocumentStandardContractViewModel> getResponse = await _mediator.Send(new GetDocumentStandardContractRegisterReqPricesByIdQuery(masterEntity.Id.ToString()), cancellationToken);
                    if (getResponse.IsSuccess)
                    {
                        if (getResponse.Data != null)
                        {
                            apiResult.Data = getResponse.Data.Adapt<DocumentStandardContractViewModel>();

                        }
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.statusCode = getResponse.statusCode;
                        apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");
                        apiResult.message = getResponse.message;
                    }
                }

                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = getRegisterReqPrices.statusCode;
                    apiResult.message.Add("مشکلی در محاسبه هزینه وجود دارد ");
                    apiResult.message = getRegisterReqPrices.message;
                }

                #endregion
            }
            else
            {
                apiResult.message.Add("سند مربوطه یافت نشد ");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }

            return apiResult;
        }
        private void UpdateDocumentCost(DocumentStandardContractCostsViewModel request)
        {
            #region DocumentCost

            masterEntity.DocumentCosts.Clear();

            masterEntity.DocumentCostUnchangeds.Clear();


            if (request != null)
            {
                if (request.DocumentCosts != null)
                {
                    if (request.DocumentCosts.Count > 0)
                    {

                        foreach (DocumentStandardContractCostViewModel documentCostViewModel in request.DocumentCosts)
                        {
                            documentCostViewModel.IsNew = true;
                            documentCostViewModel.IsValid = true;
                            DocumentCost newDocumentCost = new();
                            DocuemntStandardContractCostMapper.MapToDocumentStandardContractCost(ref newDocumentCost, documentCostViewModel);
                            newDocumentCost.ScriptoriumId = masterEntity.ScriptoriumId;
                            newDocumentCost.Ilm = "1";
                            newDocumentCost.RecordDate = _dateTimeService.CurrentDateTime;
                            masterEntity.DocumentCosts.Add(newDocumentCost);
                            if (!string.IsNullOrEmpty(documentCostViewModel.RequestPriceUnchanged))
                            {
                                DocumentCostUnchanged documentCostUnchanged = new();
                                DocuemntStandardContractCostMapper.MapToDocumentStandardContractCostUnchanged(ref documentCostUnchanged, documentCostViewModel);
                                documentCostUnchanged.DocumentId = masterEntity.Id;
                                documentCostUnchanged.ScriptoriumId = masterEntity.ScriptoriumId;
                                documentCostUnchanged.Ilm = DocumentConstants.CreateIlm;
                                documentCostUnchanged.RecordDate = _dateTimeService.CurrentDateTime;
                                masterEntity.DocumentCostUnchangeds.Add(documentCostUnchanged);
                            }

                        }
                    }
                }
            }



            #endregion


        }

        private async Task<ApiResult<DocumentStandardContractCostsViewModel>> GetEpaymentDocumentCosts(Domain.Entities.Document document, CancellationToken cancellationToken)
        {

            EpaymentCostCalculatorServiceInput CalculatorInput = new()
            {
                DocumentTypeId = new string[] { document.DocumentTypeId },
                EpaymentUseCaseId = new string[] { "07" },
                Price = (long?)document.SabtPrice,
                Elzam = false
            };

            if (document.DocumentInfoOther != null)
            {
                int? DocumentPeopleAdditionalCount = document.DocumentPeople.Where(x => x.IsOriginal == "1").Count();
                CalculatorInput.Quantity1 = DocumentPeopleAdditionalCount - 2 > 0 ? DocumentPeopleAdditionalCount - 2 : null;/*تعداد نفرات اضافه*/
                CalculatorInput.Quantity2 = document.DocumentInfoOther.RegisterCount;/*تعداد قبوض اقساطی*/
                CalculatorInput.Quantity = document.DocumentInfoOther.RegisterCount;/*تعداد*/
                CalculatorInput.Cadastre = document.DocumentInfoOther.IsKadastr.ToNullabbleBoolean();/*آیا کاداستری است؟*/
            }

            if (document.DocumentType != null)
            {
                string[] FinancialCode = ["441", "911", "922", "931"];
                string[] EghaleCode = ["710", "711", "718", "719", "721", "731"];

                if (FinancialCode.Contains(document.DocumentType.Code))
                {
                    CalculatorInput.FinancialDocument = true;
                }

                if (EghaleCode.Contains(document.DocumentType.Code))
                {
                    CalculatorInput.Eghale = true;
                }
            }

            DocumentStandardContractCostsViewModel result = new();
            ApiResult<EpaymentCostCalculatorViewModel> EpaymentCosts = await _mediator.Send(CalculatorInput, cancellationToken);
            EpaymentCostCalculatorDetail haghTahrir = EpaymentCosts.Data.EpaymentCostCalculatorDetailList.Where(x => x.CostTypeId == "2").FirstOrDefault();
            if (haghTahrir != null)
            {
                EpaymentCosts.Data.EpaymentCostCalculatorDetailList.Remove(haghTahrir);
                EpaymentCostCalculatorDetail TenPercent = EpaymentCosts.Data.EpaymentCostCalculatorDetailList.Where(x => x.CostTypeId == "25").FirstOrDefault();
                haghTahrir.Price = haghTahrir.Price.GetValueOrDefault() - (TenPercent != null ? TenPercent.Price.GetValueOrDefault() : 0);
                EpaymentCosts.Data.EpaymentCostCalculatorDetailList.Add(haghTahrir);
            }
            List<DocumentStandardContractCostViewModel> DocumentCosts = [.. EpaymentCosts.Data.EpaymentCostCalculatorDetailList.Select(x => new DocumentStandardContractCostViewModel
                {
                    DocumentId = document.Id.ToString(),
                    RequestPrice = x.Price.ToString(),
                    RequestPriceUnchanged = x.Price.ToString(),
                    RequestCostTypeId = [x.CostTypeId]
                })];

            result.DocumentCosts = DocumentCosts;
            return result;
        }

    }
}