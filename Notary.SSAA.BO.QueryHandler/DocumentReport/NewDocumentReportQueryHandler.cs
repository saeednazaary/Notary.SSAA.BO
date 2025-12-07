using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentReport;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentReport;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport;
using Notary.SSAA.BO.Utilities.Extensions;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using System.Text;
using static Notary.SSAA.BO.SharedKernel.Enumerations.EnumExtensions;
namespace Notary.SSAA.BO.QueryHandler.DocumentReport
{
    internal class NewDocumentReportQueryHandler : BaseQueryHandler<NewDocumentReportQuery, ApiResult<DocumentReportViewModels>>
    {
        private readonly IDocumentReportRepository _documentReportRepository;
        private readonly ApiResult<DocumentReportViewModels> apiResult;
        private readonly IConfiguration _configuration;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private ONotaryRegiserServiceReqEntity oNotaryRegiserServiceReqEntity;
        private readonly AnnotationsController _annotationsController;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public NewDocumentReportQueryHandler(IMediator mediator, IUserService userService, IDocumentReportRepository documentReportRepository, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, AnnotationsController annotationsController, IWebHostEnvironment webHostEnvironment) : base(mediator, userService)
        {
            _documentReportRepository = documentReportRepository;
            apiResult = new();
            _httpEndPointCaller = httpEndPointCaller ?? throw new ArgumentNullException(nameof(httpEndPointCaller));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            oNotaryRegiserServiceReqEntity = new();
            _annotationsController = annotationsController;
            _webHostEnvironment = webHostEnvironment;
        }

        protected override bool HasAccess(NewDocumentReportQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar)
           || userRoles.Contains(RoleConstants.Daftaryar)
           || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentReportViewModels>> RunAsync(NewDocumentReportQuery request, CancellationToken cancellationToken)
        {
            #region DefineFlag
            string PageHeaderTitle = string.Empty;
            bool IsReduplicatePDF = false;
            bool IsSabtServices = false;
            bool IsRooNevesht = false;
            bool IsMain = false;
            #endregion
            DocumentReportViewModel document = await _documentReportRepository.GetDocumentReport(request.Id, cancellationToken);
            if (document.documentReportItems.Count == 0)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("سند یافت نشد.");
                return apiResult;
            }
            if (document.documentReportItems.First().DocumentState != ((int)DocumentState.BeforeCreateIdCanceled).ToString()
               && document.documentReportItems.First().DocumentState != ((int)DocumentState.AfterCreateIdCanceled).ToString()
               && document.documentReportItems.First().DocumentState != ((int)DocumentState.CalculatePay).ToString()
               && document.documentReportItems.First().DocumentState != ((int)DocumentState.CreateDocumentId).ToString()
               && document.documentReportItems.First().DocumentState != ((int)DocumentState.BackupReport).ToString()
               && document.documentReportItems.First().DocumentState != ((int)DocumentState.Confirmed).ToString())
            {
                IsReduplicatePDF = false;
                IsSabtServices = false;
                PageHeaderTitle = null;
            }
            else if (document.documentReportItems.First().DocumentState == ((int)DocumentState.CalculatePay).ToString()
                || document.documentReportItems.First().DocumentState == ((int)DocumentState.CreateDocumentId).ToString()
                || document.documentReportItems.First().DocumentState == ((int)DocumentState.BackupReport).ToString()
                || document.documentReportItems.First().DocumentState == ((int)DocumentState.Confirmed).ToString())
            {
                PageHeaderTitle = "main";
                if (request.CommandName.Contains("cmdArchivePrint"))
                {
                    IsReduplicatePDF = true;
                }

                if (document.documentReportItems.First().DocumentTypeIsSupportive != null && document.documentReportItems.First().DocumentTypeIsSupportive == YesNo.None.GetString())
                {
                    IsSabtServices = true;
                }
            }

            oNotaryRegiserServiceReqEntity = document.documentReportItems.DistinctBy(x => x.DocumentId).Select(x => new ONotaryRegiserServiceReqEntity
            {
                ClassifyNo = x.ClassifyNo?.ToString(),
                DocDate = x.DocumentDate,
                ReqDate = x.RequestDate,
                ReqTime = x.RequestTime,
                ScriptoriumId = x.ScriptoriumId,
                ONotaryDocumentTypeId = x.DocumentTypeId,
                DocumentSecretCode = x.DocumentSecretCode,
                ONotaryDocumentCode = x.DocumentTypeId,
                CurrencyTitle = "ریال",
                GetDocNoDate = x.GetDocumentNoDate,
                NationalNo = x.DocumentNationalNo,
                ONotaryDocumentTypeTitle = x.DocumentTypeTitle,
                RegisterPapersNo = x.BookPaperType,
                RegisterVolumeNo = x.BookVolumeNo,
                Price = x.DocumentPrice.IsNullOrEmpty() ? null : decimal.Parse(x.DocumentPrice),
                SabtPrice = x.DocumentSabtPrice.IsNullOrEmpty() ? null : decimal.Parse(x.DocumentSabtPrice),
                SignDate = x.DocumentSignDate,
                SignTime = x.DocumentSignTime,
                SayerDocTitle = x.DocumentTypeTitle,
                LetterOfPrice = x.DocumentPrice.IsNullOrEmpty() ? null : decimal.Parse(x.DocumentPrice).ToString(),
                SardaftarConfirmDate = x.SardaftarConfirmDate,
                DaftaryarConfirmDate = x.DaftaryarConfirmDate,
                DaftaryarNameFamily = x.DaftaryarConfirmFullName,
                ConfDate = x.SardaftarConfirmDate,
                ConfirmDate = x.ConfirmDate,
                ConfirmerNameFamily = x.SardaftarConfirmFullName,
                CreateDate = x.ConfirmCreateDate,
                CreatorNameFamily = x.CreatorNameFamily,
                DocumentTextFontSize = x.TextFontSize.IsNullOrEmpty() ? null : decimal.Parse(x.TextFontSize),
                DocumentDescription = x.DocumentTextDescription,
                PrintMode = x.TextPrintMode,
                LegalText = x.LegalText,
                HasMultipleAdvocacyPermit = x.HasMultipleAdvocacyPermit,
                IsDocumentBrief = x.IsDocumentBrief,
                RelatedDocumentNo = x.RelatedDocumentNo,
                WealthType = x.WealthType,
                RequestOtherInfo = new ONotaryRequestOtherInfo
                {
                    DividedSectionsCount = x.DividedSectionsCount.IsNullOrEmpty() ? null : decimal.Parse(x.DividedSectionsCount),
                    RegCount = x.RegisterCount.IsNullOrEmpty() ? null : decimal.Parse(x.RegisterCount),
                    HasDismissalPermit = x.HasDismissalPermit.IsNullOrEmpty() ? null : x.HasDismissalPermit.ToInt(),
                    HasDismissalPermitTitle = x.HasDismissalPermit != null && x.HasDismissalPermit == "1" ? "دارد" : x.HasDismissalPermit == "2" ? "موکل ضمن عقد خارج لازم، حق عزل وکیل را از خود سلب و ساقط نموده است." : null,
                    HasAdvocacy2OthersPermit = !x.HasAdvocacyToOthersPermit.IsNullOrEmpty() ? x.HasAdvocacyToOthersPermit.ToInt() : null,
                    HasAdvocacy2OthersPermitTitle = x.HasAdvocacyToOthersPermit != null ? x.HasAdvocacyToOthersPermit == "1" ? "دارد" : x.HasAdvocacyToOthersPermit == "2" ? "ندارد" : null : null,
                    HasTime = !x.HasTime.IsNullOrEmpty() ? x.HasTime.ToInt() : null,
                    HasTimeTitle = !x.HasTime.IsNullOrEmpty() ? ((YesNo)x.HasTime.ToNullableInt()).GetEnumDescription() : null,
                    AdvocacyEndDate = x.AdvocacyEndDate,
                    RentStartDate = x.RentStartDate,
                    RentDuration = x.RentDuration,
                    IsPeace4LifetimeTitle = !x.IsPeaceForLifeTime.IsNullOrEmpty() ? ((YesNo)x.IsPeaceForLifeTime.ToNullableInt()).GetEnumDescription() : null,
                    PeaceDuration = x.PeaceDuration,
                    HagheEntefae = !x.HagheEntefae.IsNullOrEmpty() ? x.HagheEntefae.ToInt() : null,
                    VaghfTypeTitle = x.VaghfType != null && x.VaghfType == "1" ? "خاص" : x.VaghfType == "2" ? "عام" : null,
                    IsRentWithSarGhofli = !x.IsRentWithSarghofli.IsNullOrEmpty() ? x.IsRentWithSarghofli.ToInt() : null,
                    IsRentWithSarGhofliTitle = !x.IsRentWithSarghofli.IsNullOrEmpty() ? ((YesNo)x.IsRentWithSarghofli.ToNullableInt()).GetEnumDescription() : null,
                    MortageDuration = !x.MortageDuration.IsNullOrEmpty() ? (x.MortageDuration.ToString() + "(" + x.MortageDuration + ")") : null,
                    MortageUnit = x.MortageTimeUnitId,//BaseInfo.MEASUREMENT_UNIT_TYPE
                    DadnamehDate = x.JudgmentIssueDate,
                    DadnamehNo = x.JudgmentIssueNo,
                    DadnamehIssuerName = x.JudgmentIssueName,
                    IsDocBasedJudgeHokm = x.IsBaseJudgment.IsNullOrEmpty() ? null : x.IsBaseJudgment.ToInt(),
                    IsDocBasedJudgeHokmTitle = x.IsBaseJudgment.IsNullOrEmpty() ? null : ((YesNo)x.IsBaseJudgment.ToNullableInt()).GetEnumDescription(),
                    HokmType = x.JudmentTypeId.ToInt(),
                    HokmTypeTitle = x.JudmentTypeTitle,
                    CaseClassifyNo = x.JudgmentIssueCaseClassifyNo,
                    HokmNoLebel = !x.IsBaseJudgment.IsNullOrEmpty() ? x.JudmentTypeId == "1" ? "شماره  دادنامه:" : "شماره نامه اجرا:" : null,
                    HokmDateLabel = !x.IsBaseJudgment.IsNullOrEmpty() ? x.JudmentTypeId == "1" ? "تاریخ  دادنامه:" : "تاریخ نامه اجرا:" : null,
                }

            }).FirstOrDefault();
            #region DocumentCost
            if (document.documentReportItems.DistinctBy(x => x.CostTypeId).First().CostTypeId != null)
            {
                oNotaryRegiserServiceReqEntity.RegServiceCosts = document.documentReportItems.DistinctBy(x => x.CostTypeId).Select(x => new ONotaryRegServiceCostEntity { CostSpec = x.CostTypeTitle + " به مبلغ: " + x.CostPrice + " ریال" }).ToList();
                oNotaryRegiserServiceReqEntity.RegServiceCosts.Add(new ONotaryRegServiceCostEntity { CostSpec = "جمع کل: " + document.documentReportItems.DistinctBy(x => x.CostTypeId).Sum(x => long.Parse(x.CostPrice)).ToString() + " ریال ", RegServiceCostCount = document.documentReportItems.DistinctBy(x => x.CostTypeId).Count() });
            }
            #endregion
            oNotaryRegiserServiceReqEntity.RegCases = [];


            if (oNotaryRegiserServiceReqEntity.WealthType == WealthType.Immovable.GetString())
            {
                oNotaryRegiserServiceReqEntity.RegCases = document.documentReportItems.DistinctBy(x => x.EstateId).Where(x => !x.EstateId.IsNullOrEmpty()).Select(x => new ONotaryRegCaseEntity
                {
                    Description = x.EstateDescription,
                    Address = x.EstateAddress,
                    Area = x.EstateArea.IsNullOrEmpty() ? null : decimal.Parse(x.EstateArea),
                    AreaDescription = x.EstateAreaDescription,
                    ONotaryAssetTypeId = x.EstateTypeId,
                    ONotaryAssetTypeTitle = x.EstateTypeTitle.IsNullOrEmpty() ? x.VehicleTipTitle : x.EstateTypeTitle,
                    Price = x.EstatePrice.IsNullOrEmpty() ? null : decimal.Parse(x.EstatePrice),
                    BakhshSSAAId = x.EstateSectionId,
                    BakhshSSAATitle = x.EstateSectionTitle,
                    BasicPlaque = x.EstateBasicPlaque,
                    BasicPlaqueHasRemain = x.EstateBasicPlaqueHasRemain.IsNullOrEmpty() ? null : x.EstateBasicPlaqueHasRemain.ToInt(),
                    BasicPlaqueHasRemainDesc = x.EstateBasicPlaqueHasRemain.IsNullOrEmpty() ? null : ((YesNo)x.EstateBasicPlaqueHasRemain.ToNullableInt()).GetEnumDescription(),
                    BlockNo = x.EstateBlock,
                    Commons = x.EstateCommons,
                    Direction = x.EstateDirection,
                    DivFromBasicPlaque = x.EstateDivFromBasicPlaque,
                    DivFromSecondaryPlaque = x.EstateDivFromSecondaryPlaque,
                    FloorNo = x.EstateFloor,
                    GeoLocationId = x.EstateGeoLocationId?.ToString(),//
                    NahyehSSAAId = x.EstateSubSectionId,
                    NahyehSSAATitle = x.EstateSubSectionTitle,
                    EvacuationDescription = x.EstateEvacuationDescription,
                    ImmovaleType = x.EstateImmovaleType.IsNullOrEmpty() ? null : x.EstateImmovaleType.ToInt(),
                    ImmovaleTypeDesc = x.EstateImmovaleType.IsNullOrEmpty() ? null : ((NotaryImmovaleType)x.EstateImmovaleType.ToNullableInt()).GetEnumDescription(),
                    Limits = x.EstateLimits,
                    LocationType = x.EstateLocationType.IsNullOrEmpty() ? null : x.EstateLocationType.ToInt(),
                    LocationTypeDesc = x.EstateLocationType.IsNullOrEmpty() ? null : ((LocationType)x.EstateLocationType.ToNullableInt()).GetEnumDescription(),
                    HozehSSAAId = x.EstateUnitId,//BASEINFO
                    OldSaleDescription = x.EstateOldSaleDescription,
                    PieceNo = x.EstatePiece,
                    PlaqueText = x.EstatePlaqueText,
                    SecondaryPlaqueHasRemain = x.EstateSecondaryPlaqueHasRemain.IsNullOrEmpty() ? null : x.EstateSecondaryPlaqueHasRemain.ToInt(),
                    SecondaryPlaqueHasRemainDesc = x.EstateSecondaryPlaqueHasRemain.IsNullOrEmpty() ? null : ((YesNo)x.EstateSecondaryPlaqueHasRemain.ToNullableInt()).GetEnumDescription(),
                    SecondaryPlaqueNo = x.EstateSecondaryPlaque,
                    MunicipalityDate = x.EstateMunicipalityDate,
                    MunicipalityNo = x.EstateMunicipalityNo,
                    MunicipalityDescription = "شماره " + x.EstateMunicipalityNo + " مورخ " + x.EstateMunicipalityDate + " " + x.EstateMunicipalityIssuer,
                    SeparationDate = x.EstateSeprationDate,
                    SeparationNo = x.EstateSeprationNo,
                    SeparationDescription = "شماره " + x.EstateSeprationNo + " مورخ " + x.EstateSeprationDate + " " + x.EstateSeprationIssuer,
                    CaseQuotaDescription = x.EstateQuotaText,
                    CaseTitle = x.DocumentTypeCaseTitle,
                    ReceiverBasicPlaque = x.EstateReceiverBasicPlaque,
                    ReceiverBasicPlqHasRemain = x.EstateReceiverBasicPlaqueHasRemain.IsNullOrEmpty() ? null : x.EstateReceiverBasicPlaqueHasRemain.ToInt(),
                    ReceiverDivFromBasicPlaque = x.EstateReceiverDivFromBasicPlaque,
                    ReceiverDivFromScndryPlaque = x.EstateReceiverDivFromSecondaryPlaque,
                    ReceiverPlaqueText = x.EstateReceiverPlaqueText,
                    ReceiverScndryPlqHasRemain = x.EstateReceiverSecondaryPlaqueHasRemain.IsNullOrEmpty() ? null : x.EstateReceiverSecondaryPlaqueHasRemain.ToInt(),
                    RemortageText = x.EstateIsRemortage == "1" ? "توضیح: رهن مجدد با حفظ حقوق سند رهنی قبلی است." : "",
                    AttachmentSpecifications = x.EstateAttachmentSpecifications,
                    AttachmentDescriptionRegCase = x.EstateAttachmentDescription,
                    AttachmentType = x.EstateAttachmentType.IsNullOrEmpty() ? null : x.EstateAttachmentType.ToInt(),
                    AttachmentTypeOthers = x.EstateAttachmentTypeOther,
                    IsAttachment = x.EstateIsAttachment.IsNullOrEmpty() ? null : x.EstateIsAttachment.ToInt(),
                    SellDetailQuota = x.EstateSellDetailQuota.IsNullOrEmpty() ? null : decimal.Parse(x.EstateSellDetailQuota),
                    SellTotalQuota = x.EstateSellTotalQuota.IsNullOrEmpty() ? null : decimal.Parse(x.EstateSellTotalQuota),
                    WealthType = x.WealthType.ToInt(),
                    HasAsset = x.HasAsset,
                    RegCaseCount = document.documentReportItems.DistinctBy(x => x.EstateId).Where(x => !x.EstateId.IsNullOrEmpty()).Count(),
                    RegCaseTitle = x.DocumentTypeCaseTitle,
                    Id = x.MainPersonId,
                    ONotaryRegCaseId = x.EstateId
                }).ToList();

                List<ONotaryPersonOwnershipDocEntity> ownershipDocs = document.documentReportItems.DistinctBy(x => new { x.EstateId, x.MainPersonId }).Where(x => x.EstateId != null && x.EstateOwnershipDocumentType != null).Select(x => new ONotaryPersonOwnershipDocEntity
                {
                    OwnershipDocTitle = GetOwnershipDoc(x),
                    ONotaryRegCaseId = x.EstateId,
                    ONotaryDocPersonId = x.MainPersonId,

                }).ToList();
                if (oNotaryRegiserServiceReqEntity.RegCases != null)
                {
                    foreach (ONotaryRegCaseEntity regCase in oNotaryRegiserServiceReqEntity.RegCases)
                    {
                        regCase.OwnershipDocs = ownershipDocs.Where(x => x.ONotaryRegCaseId == regCase.ONotaryRegCaseId).ToList();
                    }
                }
                oNotaryRegiserServiceReqEntity.Inquiries = document.documentReportItems.DistinctBy(x => new { x.DocumentInquiryPersonId }).Where(x => x.DocumentInquiryPersonId != null).Select(x => new ONotaryRegServiceInquiryEntity
                {
                    Description = x.InquiryDescription,
                    ReplyNo = x.InquiryReplyNo,
                    ReplyDate = x.InquiryReplyDate,
                    ONotaryInquiryOrganizationTitle = x.InquiryOrganizationTitle,
                }).ToList();
            }
            else
            {
                oNotaryRegiserServiceReqEntity.RegCases = document.documentReportItems.Where(x => !x.DocumentVehicleId.IsNullOrEmpty()).DistinctBy(x => x.DocumentVehicleId).Select(x => new ONotaryRegCaseEntity
                {
                    VehicleCardNo = x.VehicleCardNo,
                    VehicleChassisNo = x.VehicleChassisNo,
                    VehicleColor = x.VehicleColor,
                    VehicleCylinderCount = x.VehicleCylinderCount,
                    VehicleDutyFicheNo = x.VehicleDutyFicheNo,
                    VehicleEngineCapacity = x.VehicleEngineCapacity,
                    VehicleEngineNo = x.VehicleEngineNo,
                    VehicleNumberingLocation = x.VehicleNumberingLocation,
                    VehicleOtherInfo = x.VehicleOtherInfo,
                    VehicleInssuranceCo = x.VehicleInssuranceNo,
                    VehicleInssuranceNo = x.VehicleInssuranceNo,
                    VehicleOldDocumentDate = x.VehicleOldDocumentDate,
                    VehicleOldDocumentIssuer = x.VehicleOldDocumentIssuer,
                    VehicleOldDocumentNo = x.VehicleOldDocumentNo,
                    VehicleOwnershipPrintedDocNo = x.VehicleOwnershipPrintedDocumentNo,
                    VehiclePlaqueSeri = x.VehiclePlaqueSeriSeller != null ? "ایران -" + x.VehiclePlaqueSeriSeller : null,
                    VehiclePlaqueNo1Buyer = x.VehiclePlaqueNo1Buyer.IsNullOrEmpty() ? null : decimal.Parse(x.VehiclePlaqueNoAlphaBuyer),
                    VehiclePlaqueNo1Seller = x.VehiclePlaqueNo1Seller.IsNullOrEmpty() ? null : decimal.Parse(x.VehiclePlaqueNo1Seller),
                    VehiclePlaqueNo2Buyer = x.VehiclePlaqueNo2Buyer.IsNullOrEmpty() ? null : decimal.Parse(x.VehiclePlaqueNo2Buyer),
                    VehiclePlaqueNo2Seller = x.VehiclePlaqueNo2Seller.IsNullOrEmpty() ? null : decimal.Parse(x.VehiclePlaqueNo2Seller),
                    VehiclePlaqueNoAlphaBuyer = x.VehiclePlaqueNoAlphaBuyer,
                    VehiclePlaqueNoAlphaSeller = x.VehiclePlaqueNoAlphaSeller,
                    VehiclePlaqueSeriBuyer = x.VehiclePlaqueSeriBuyer.IsNullOrEmpty() ? null : decimal.Parse(x.VehiclePlaqueSeriBuyer),
                    VehiclePlaqueSeriSeller = x.VehiclePlaqueSeriSeller.IsNullOrEmpty() ? null : decimal.Parse(x.VehiclePlaqueSeriSeller),
                    VehiclePlaqueNo = x.VehiclePlaqueSeller,
                    WealthType = x.WealthType.ToInt(),
                    CaseTitle = x.DocumentTypeCaseTitle,
                }).ToList();
            }

            List<string> removingAgentsCodes = ["10", "11", "12", "14", "15"];

            List<ONotaryDocPersonEntity> Persons = document.documentReportItems.DistinctBy(x => x.MainPersonId).Select(x => new ONotaryDocPersonEntity
            {
                Address = x.MainPersonAddress,
                BirthDate = x.MainPersonBirthDate,
                CompanyRegisterDate = x.MainPersonCompanyRegisterDate,
                CompanyRegisterNo = x.MainPersonCompanyRegisterNo,
               // CompanyRegisterLocation = x.MainPersonCompanyRegisterLocation,
                //CompanyTypeTile = x.MainPersonCompanyTypeId.IsNullOrEmpty() ? null : ((NotaryCompanyType)x.MainPersonCompanyTypeId.ToNullableInt()).GetEnumDescription(),
                Description = x.MainPersonDescription,
                Family = x.MainPersonFamily,
                FatherName = x.MainPersonFatherName,
                FullName = x.MainPersonName + " " + x.MainPersonFamily,
                IdentityIssueLocation = x.MainPersonIdentityIssueLocation,
                IdentityNo = x.MainPersonIdentityNo,
                IsIranian = x.MainPersonIsIranian.IsNullOrEmpty() ? null : x.MainPersonIsIranian.ToInt(),
                IsOriginalPerson = x.MainPersonIsOrginal.IsNullOrEmpty() ? null : x.MainPersonIsOrginal.ToInt(),
                IsOriginalPersonDesc = x.MainPersonIsOrginal != null ? ((YesNo)x.MainPersonIsOrginal.ToNullableInt()).GetEnumDescription() : null,
                //LegalPersonBaseTypeTitle = x.MainLegalPersonNatureId != null ? ((LegalpersonNature)x.MainLegalPersonNatureId.ToNullableInt()).GetEnumDescription() : null,
                //LegalPersonTypeDesc = x.MainLegalPersonTypeId != null ? ((LegalPersonType)x.MainLegalPersonTypeId.ToNullableInt()).GetEnumDescription() : null,
                Name = x.MainPersonName,
                Nationality = x.MainPersonNationality,
                NationalNo = x.MainPersonNationalNo,
                ONotaryRegServicePersonTypeId = x.MainPersonTypeId,
                PersonType = x.MainPersonType.IsNullOrEmpty() ? null : x.MainPersonType.ToInt(),
                PersonTypeDesc = x.MainPersonType.IsNullOrEmpty() ? null : ((PersonType)x.MainPersonType.ToNullableInt()).GetEnumDescription(),
                PersonTypeId = x.MainPersonTypeId,
                PostalCode = x.MainPersonPostalCode,
                RowNo = x.MainPersonRowNo.IsNullOrEmpty() ? null : x.MainPersonRowNo.ToInt(),
                SexTypeDesc = x.MainPersonSexType,
                TelNo = x.MainPersonTel,
                Id = x.MainPersonId,
                ONotaryRegServicePersonTypeTitle = document.documentReportItems.DistinctBy(x => x.MainPersonId).GroupBy(x => x.MainPersonTypeId).Count() > 1 ? x.PersonTypePrintPluralTitle : x.PersonTypePrintSingularTitle,
                ONotaryRegisterServiceReqId = x.PersonTypeSingularTitle,
                IsOwner = x.IsOwner,
                PrintSingularTitle = x.PersonTypePrintSingularTitle,
                LastLegalPaperDate = x.MainPersonLastLegalPaperDate,
                LastLegalPaperNo = x.MainPersonLastLegalPaperNo,
                FingerprintImage = x.PersonFingerPrintImageFile != null ? Convert.FromBase64String(x.PersonFingerPrintImageFile) : null,
            }).OrderBy(x => x.RowNo).ToList();

            List<ONotaryDocPersonEntity> Agents = document.documentReportItems.DistinctBy(x => new { x.MainAgentPersonId, x.AgentPersonId, x.AgentPersonName }).Where(x => x.MainAgentPersonId != null).Select(x => new ONotaryDocPersonEntity
            {
                Address = x.MainPersonAddress,
                BirthDate = x.MainPersonBirthDate,
                CompanyRegisterDate = x.MainPersonCompanyRegisterDate,
                CompanyRegisterNo = x.MainPersonCompanyRegisterNo,
                CompanyRegisterLocation = x.MainPersonCompanyRegisterLocation,
                CompanyTypeTile = x.MainPersonCompanyTypeId.IsNullOrEmpty() ? null : ((NotaryCompanyType)x.MainPersonCompanyTypeId.ToNullableInt()).GetEnumDescription(),
                Description = x.MainPersonDescription,
                Family = x.AgentPersonFamily,
                FatherName = x.MainPersonFatherName,
                FullName = x.AgentPersonName + " " + x.AgentPersonFamily,
                IdentityIssueLocation = x.MainPersonIdentityIssueLocation,
                IdentityNo = x.MainPersonIdentityNo,
                IsIranian = x.MainPersonIsIranian.IsNullOrEmpty() ? null : x.MainPersonIsIranian.ToInt(),
                IsOriginalPerson = x.MainPersonIsOrginal.IsNullOrEmpty() ? null : x.MainPersonIsOrginal.ToInt(),
                IsOriginalPersonDesc = x.MainPersonIsOrginal != null ? ((YesNo)x.MainPersonIsOrginal.ToNullableInt()).GetEnumDescription() : null,
                LegalPersonBaseTypeTitle = x.MainLegalPersonNatureId != null ? ((LegalpersonNature)x.MainLegalPersonNatureId.ToNullableInt()).GetEnumDescription() : null,
                LegalPersonTypeDesc = x.MainLegalPersonTypeId != null ? ((LegalPersonType)x.MainLegalPersonTypeId.ToNullableInt()).GetEnumDescription() : null,
                Name = x.AgentPersonName,
                Nationality = x.MainPersonNationality,
                NationalNo = x.MainPersonNationalNo,
                ONotaryRegServicePersonTypeId = x.MainPersonTypeId,
                PersonType = x.MainPersonType.IsNullOrEmpty() ? null : x.MainPersonType.ToInt(),
                PersonTypeDesc = x.MainPersonType.IsNullOrEmpty() ? null : ((PersonType)x.MainPersonType.ToNullableInt()).GetEnumDescription(),
                PersonTypeId = x.MainPersonTypeId,
                PostalCode = x.MainPersonPostalCode,
                RowNo = x.MainPersonRowNo.IsNullOrEmpty() ? null : x.MainPersonRowNo.ToInt(),
                SexTypeDesc = x.MainPersonSexType,
                TelNo = x.MainPersonTel,
                ParentPersonId = x.MainAgentPersonId,
                AgentTypeCode = x.AgentTypeCode,
                AgentTypeRoot = x.AgentTypeRoot,
                ReliablePersonReasonCode = x.ReliablePersonReasonCode,
                RelatedAgentDocumentDate = x.RelatedAgentDocumentDate,
                RelatedAgentDocumentIssuer = x.RelatedAgentDocumentIssuer,
                RelatedAgentDocumentNo = x.RelatedAgentDocumentNo,

            }).ToList();

            foreach (ONotaryDocPersonEntity mainPerson in Persons.Where(x => x.IsOriginalPerson == 1).ToList())
            {
                mainPerson.Agents = [];

                if (mainPerson.PersonType == (int)PersonType.NaturalPerson)
                {
                    mainPerson.NationalNoLabel = "شماره ملی : " + mainPerson.NationalNo;
                }
                if (mainPerson.PersonType == (int)PersonType.Legal)
                {
                    mainPerson.NationalNoLabel = !mainPerson.Nationality.IsNullOrEmpty()
                        ? "تابعیت : " + " غیر ایرانی - " + mainPerson.Nationality
                        : "تابعیت : " + " غیر ایرانی - ";
                }

                string singularTitle = string.Empty;
                if (Agents.Any(x => x.ParentPersonId == mainPerson.Id))
                {
                    foreach (ONotaryDocPersonEntity agent in Agents)
                    {
                        if (agent.ParentPersonId == mainPerson.Id && !removingAgentsCodes.Contains(agent.AgentTypeCode))
                        {
                            if (!mainPerson.RelatedPersonInfo.IsNullOrEmpty())
                            {
                                mainPerson.RelatedPersonInfo += "و";
                            }
                            else
                            {
                                mainPerson.RelatedPersonInfo = mainPerson.FullName + " ";
                            }

                            mainPerson.RelatedPersonInfo +=
                            ((agent.AgentTypeCode == "9") ?
                            " وارث " :
                            (agent.AgentTypeCode == "13") ?
                            (" به " + agent.AgentTypeRoot) :
                            (" با " + agent.AgentTypeRoot)) +
                            " " + agent.SexTypeDesc + " " + agent.FullName + " ";



                            if (!agent.ReliablePersonReasonCode.IsNullOrEmpty())
                            {
                                switch (agent.AgentTypeCode)
                                {
                                    case "10":
                                        if (agent.ReliablePersonReasonCode == "1")
                                        {
                                            agent.MokamelAgentStatement = " چون " + mainPerson.FullName + " سواد نداشت، " + agent.FullName + " معتمداً سند را برایش خواند، رضایت داشت.";
                                        }
                                        else if (agent.ReliablePersonReasonCode == "2")
                                        {
                                            agent.MokamelAgentStatement = " چون " + mainPerson.FullName + " از اشخاص مشمول ماده 64 قانون ثبت اسناد واملاک بود، " + agent.FullName + " که توانایی تفهیم مطالب را به وی داشت، معتمداً ثبت و سند را به وی تفهیم نمود.";
                                        }
                                        else if (agent.ReliablePersonReasonCode == "3")
                                        {
                                            agent.MokamelAgentStatement = " چون " + mainPerson.FullName + " بیمار بود، سند با حضور " + agent.FullName + " به عنوان معتمد تنظیم شد.";
                                        }
                                        else if (agent.ReliablePersonReasonCode == "4")
                                        {
                                            agent.MokamelAgentStatement = " چون " + mainPerson.FullName + " دست و پا نداشت، سند با حضور " + agent.FullName + " به عنوان معتمد در محل دفترخانه تنظیم شد.";
                                        }
                                        else if (agent.ReliablePersonReasonCode == "7")
                                        {
                                            agent.MokamelAgentStatement = " چون امکان اخذ اثرانگشت " + mainPerson.FullName + " وجود نداشت، سند با حضور " + agent.FullName + " به عنوان معتمد در محل دفترخانه تنظیم شد.";
                                        }

                                        break;
                                    case "11":
                                        agent.MokamelAgentStatement = "  " + agent.FullName + " اعلام نمود " + mainPerson.FullName + " را می شناسد و هویت وی را به سردفتر معرفی و گواهی می‌نماید.";
                                        break;
                                    case "12":
                                        agent.MokamelAgentStatement = " چون " + mainPerson.FullName + "  فارسی نمی دانست، " + agent.FullName + " به عنوان مترجم رسمی، سند را برای وی کاملاً ترجمه نمود، رضایت داشت.";
                                        break;
                                    case "14":
                                        if (agent.ReliablePersonReasonCode == "3")
                                        {
                                            agent.MokamelAgentStatement = " سند با حضور " + agent.FullName + " به عنوان  نماینده دادستان تنظیم و ثبت شد.";
                                        }
                                        else if (agent.ReliablePersonReasonCode == "5")
                                        {
                                            agent.MokamelAgentStatement = " سند با حضور " + agent.FullName + " به عنوان  نماینده دادستان تنظیم و ثبت شد.";
                                        }
                                        else if (agent.ReliablePersonReasonCode == "6")
                                        {
                                            agent.MokamelAgentStatement = " سند با حضور " + agent.FullName + " به عنوان دادستان/نماینده دادستان/رئیس دادگاه بخش/نماینده رئیس دادگاه بخش تنظیم و ثبت شد.";
                                        }
                                        break;
                                }
                            }
                            string post = string.Empty;
                            if (!string.IsNullOrEmpty(agent.RelatedAgentDocumentNo))
                            {
                                switch (agent.AgentTypeCode)
                                {
                                    case "01": post = "طبق وکالت شماره"; break;
                                    case "02":
                                    case "03":
                                    case "04":
                                    case "05":
                                    case "06":
                                    case "07":
                                        post = "طبق مدرک شماره"; break;
                                    case "09":
                                        singularTitle = "مورث";
                                        break;
                                }
                                agent.DocAgentInfo += post + " " + agent.RelatedAgentDocumentNo;
                            }
                            if (!string.IsNullOrEmpty(agent.RelatedAgentDocumentDate))
                            {
                                agent.DocAgentInfo += " مورخ " + agent.RelatedAgentDocumentDate;
                            }

                            if (!string.IsNullOrEmpty(agent.RelatedAgentDocumentIssuer))
                            {
                                agent.DocAgentInfo += " " + agent.RelatedAgentDocumentIssuer;
                            }

                            if (!string.IsNullOrEmpty(agent.Description))
                            {
                                agent.DocAgentInfo += " - " + agent.Description;
                            }

                            mainPerson.Agents.Add(agent);

                            post = string.Empty;

                            switch (agent.AgentTypeCode)
                            {
                                case "01": post += "وکیل"; break;
                                case "02": post += "نماینده"; break;
                                case "03": post += "ولی"; break;
                                case "04": post += "مدیر"; break;
                                case "05": post += "قائم مقام"; break;
                                case "06": post += "متولی"; break;
                                case "07": post += "قیم"; break;
                                case "08": post += "وارث"; break;
                                case "09": post += "مورث"; break;
                                case "10": post += "معتمد"; break;
                                case "11": post += "معرف"; break;
                                case "12": post += "مترجم"; break;
                                case "15": post += "شاهد"; break;
                                case "14": post += "نماینده دادستان"; break;
                            }
                            agent.NameAndPost = post + ": " + agent.SexTypeDesc + " " + agent.FullName;

                        }
                    }
                }
                else
                {
                    ONotaryDocPersonEntity agent = new();
                    mainPerson.RelatedPersonInfo = mainPerson.FullName;
                    mainPerson.Agents.Add(agent);
                }

                if (mainPerson.ONotaryRegServicePersonTypeId != null)
                {
                    singularTitle = mainPerson.ONotaryRegServicePersonTypeId == "40" ||     // بانك
                        mainPerson.ONotaryRegServicePersonTypeId == "47"
                        ? "مرتهن"
                        : mainPerson.PrintSingularTitle;
                }

                foreach (DocumentReportItem item in document.documentReportItems.DistinctBy(x => new { x.EstateId, x.MainPersonId }).Where(x => x.EstateId != null).ToList())
                {
                    if (mainPerson.IsOwner == YesNo.Yes.GetString())
                    {
                        //سهم مالک
                        if (item.EstateIsRegistered == YesNo.Yes.GetString() && item.DocumentInquiryPersonId == mainPerson.Id)
                        {
                            mainPerson.CaseQouta += "مالک " + item.DocumentInquiryReplyDetailQuota + " سهم از " + item.DocumentInquiryReplyTotalQuota + " سهم و ";
                        }
                        else
                        {
                            if (item.EstateIsProportionateQuota == YesNo.Yes.GetString() && item.EstateQuotaPersonId == mainPerson.Id)
                            {
                                if (!item.EstateQuotaDetailQuota.IsNullOrEmpty() && !item.EstateQuotaTotalQuota.IsNullOrEmpty())
                                {
                                    mainPerson.CaseQouta += "مالک " + item.EstateQuotaDetailQuota + " سهم از " + item.EstateQuotaTotalQuota + " سهم و ";
                                }
                                else
                                {
                                    mainPerson.CaseQouta += item.EstateQuotaText;
                                }
                            }
                            else if (item.EstateIsProportionateQuota == YesNo.No.GetString() && item.EstateQuotaDetailSellerPersonId == mainPerson.Id)
                            {
                                if (!item.EstateQuotaDetailOwnerShipDetailQuota.IsNullOrEmpty() && !item.EstateQuotaDetailOwnerShipTotalQuota.IsNullOrEmpty())
                                {
                                    mainPerson.CaseQouta += "مالک " + item.EstateQuotaDetailOwnerShipDetailQuota + " سهم از " + item.EstateQuotaDetailOwnerShipTotalQuota + " سهم و ";
                                }
                            }

                        }
                        //سهم فروشنده
                        if (item.EstateIsProportionateQuota == YesNo.Yes.GetString() && item.EstateQuotaPersonId == mainPerson.Id)
                        {
                            if (!item.EstateQuotaDetailQuota.IsNullOrEmpty() && !item.EstateQuotaTotalQuota.IsNullOrEmpty())
                            {
                                mainPerson.CaseQouta += singularTitle + " " + item.EstateQuotaDetailQuota + " سهم از " + item.EstateQuotaTotalQuota + " سهم ";
                            }
                            else
                            {
                                mainPerson.CaseQouta += item.EstateQuotaText;
                            }
                        }
                        else if (item.EstateIsProportionateQuota == YesNo.No.GetString() && item.EstateQuotaDetailSellerPersonId == mainPerson.Id)
                        {
                            if (!item.EstateQuotaDetailSellerDetailQuota.IsNullOrEmpty() && !item.EstateQuotaDetailSellerTotalQuota.IsNullOrEmpty())
                            {
                                mainPerson.CaseQouta += singularTitle + " " + item.EstateQuotaDetailSellerDetailQuota + " سهم از " + item.EstateQuotaDetailSellerTotalQuota + " سهم ";
                            }
                            else
                            {
                                mainPerson.CaseQouta += item.EstateQuotaDetailQuotaText;
                            }
                        }
                    }
                    else
                    {
                        //سهم خریدار
                        if (item.EstateIsProportionateQuota == YesNo.Yes.GetString() && item.EstateQuotaPersonId == mainPerson.Id)
                        {
                            if (!item.EstateQuotaDetailQuota.IsNullOrEmpty() && !item.EstateQuotaTotalQuota.IsNullOrEmpty())
                            {
                                mainPerson.CaseQouta += singularTitle + " " + item.EstateQuotaDetailQuota + " سهم از " + item.EstateQuotaTotalQuota + " سهم ";
                            }
                            else
                            {
                                mainPerson.CaseQouta += item.EstateQuotaText;
                            }
                        }
                        else if (item.EstateIsProportionateQuota == YesNo.No.GetString() && item.EstateQuotaDetailSellerPersonId == mainPerson.Id)
                        {
                            if (!item.EstateQuotaDetailSellerDetailQuota.IsNullOrEmpty() && !item.EstateQuotaDetailSellerTotalQuota.IsNullOrEmpty())
                            {
                                mainPerson.CaseQouta += singularTitle + " " + item.EstateQuotaDetailSellerDetailQuota + " سهم از " + item.EstateQuotaDetailSellerTotalQuota + " سهم ";
                            }
                            else
                            {
                                mainPerson.CaseQouta += item.EstateQuotaDetailQuotaText;
                            }
                        }

                    }
                }

                foreach (DocumentReportItem itemVehicle in document.documentReportItems.DistinctBy(x => new { x.DocumentVehicleId, x.MainPersonId }).Where(x => x.DocumentVehicleId != null).ToList())
                {
                    if (mainPerson.IsOwner == YesNo.Yes.GetString())
                    {
                        // مالک

                        if (itemVehicle.VehicleQuotaPersonId == mainPerson.Id)
                        {
                            if (!itemVehicle.VehicleQuotaDetailQuota.IsNullOrEmpty() && !itemVehicle.VehicleQuotaTotalQuota.IsNullOrEmpty())
                            {
                                mainPerson.CaseQouta += "مالک " + itemVehicle.VehicleQuotaDetailQuota + " سهم از " + itemVehicle.VehicleQuotaTotalQuota + " سهم و ";
                            }
                            else
                            {
                                mainPerson.CaseQouta += itemVehicle.VehicleQuotaQuotaText;
                            }
                        }


                        //سهم فروشنده
                        if (itemVehicle.VehicleQuotaDetailSellerPersonId == mainPerson.Id)
                        {
                            if (!itemVehicle.VehicleQuotaDetailSellerDetailQuota.IsNullOrEmpty() && !itemVehicle.VehicleQuotaDetailSellerTotalQuota.IsNullOrEmpty())
                            {
                                mainPerson.CaseQouta += singularTitle + " " + itemVehicle.VehicleQuotaDetailSellerDetailQuota + " سهم از " + itemVehicle.VehicleQuotaDetailSellerTotalQuota + " سهم ";
                            }
                            else
                            {
                                mainPerson.CaseQouta += itemVehicle.VehicleQuotaDetailQuotaText;
                            }
                        }
                    }
                    else
                    {
                        //سهم خریدار
                        if (itemVehicle.VehicleQuotaDetailBuyerPersonId == mainPerson.Id)
                        {
                            if (!itemVehicle.VehicleQuotaDetailBuyerDetailQuota.IsNullOrEmpty() && !itemVehicle.VehicleQuotaDetailBuyerTotalQuota.IsNullOrEmpty())
                            {
                                mainPerson.CaseQouta += singularTitle + " " + itemVehicle.VehicleQuotaDetailBuyerDetailQuota + " سهم از " + itemVehicle.VehicleQuotaDetailBuyerTotalQuota + " سهم ";
                            }
                            else
                            {
                                mainPerson.CaseQouta += itemVehicle.VehicleQuotaDetailQuotaText;
                            }
                        }
                    }
                }

                mainPerson.PrintableSpec = mainPerson.NameAndPost + " - " + mainPerson.CaseQouta + " - ";
                if (mainPerson.PersonType == (int)YesNo.Yes)
                {
                    mainPerson.PrintableSpec += "شماره ملی: " + mainPerson.NationalNo + " - " +
                            "نام پدر: " + mainPerson.FatherName + " - " +
                            "تاریخ تولد: " + mainPerson.BirthDate + " - " +
                            "شماره شناسنامه: " + mainPerson.IdentityNo + " - " +
                            "محل صدور: " + mainPerson.IdentityIssueLocation + " - ";
                    if (mainPerson.IdentityNo != null)
                    {
                        mainPerson.PrintableSpec +=
                            "سریال شناسنامه: " + mainPerson.IdentityNo + " - ";
                    }

                    mainPerson.PrintableSpec +=
                        "کد پستی: " + mainPerson.PostalCode + " - " +
                        "نشانی: " + mainPerson.Address + "\n";
                }
                else
                {
                    mainPerson.PrintableSpec +=
                        "شناسه ملی: " + mainPerson.NationalNo + " - ";

                    if (mainPerson.CompanyRegisterNo != null && !string.IsNullOrEmpty(mainPerson.CompanyRegisterNo))
                    {
                        mainPerson.PrintableSpec +=
                            "شماره ثبت: " + mainPerson.CompanyRegisterNo + " - ";
                    }

                    if (mainPerson.CompanyRegisterDate != null && !string.IsNullOrEmpty(mainPerson.CompanyRegisterDate))
                    {
                        mainPerson.PrintableSpec +=
                            "تاریخ ثبت: " + mainPerson.CompanyRegisterDate + " - ";
                    }

                    if (mainPerson.CompanyRegisterLocation != null && !string.IsNullOrEmpty(mainPerson.CompanyRegisterLocation))
                    {
                        mainPerson.PrintableSpec +=
                            "محل ثبت: " + mainPerson.CompanyRegisterLocation + " - ";
                    }

                    if (mainPerson.LastLegalPaperNo != null && !string.IsNullOrEmpty(mainPerson.LastLegalPaperNo))
                    {
                        mainPerson.PrintableSpec +=
                            "شماره آخرین روزنامه رسمی: " + mainPerson.LastLegalPaperNo + " - ";
                    }

                    if (mainPerson.LastLegalPaperDate != null && !string.IsNullOrEmpty(mainPerson.LastLegalPaperDate))
                    {
                        mainPerson.PrintableSpec +=
                            "تاریخ آخرین روزنامه رسمی: " + mainPerson.LastLegalPaperDate + " - ";
                    }

                    mainPerson.PrintableSpec +=
                        "کد پستی: " + mainPerson.PostalCode + " - " +
                        "نشانی: " + mainPerson.Address + "\n";
                }
                if (mainPerson.Description != null && !string.IsNullOrEmpty(mainPerson.Description))
                {
                    mainPerson.PrintableSpec += "\n" + mainPerson.Description;
                }

                SignablePerson signablePerson = new()
                {
                    FinterPrintImage = mainPerson.FingerprintImage,
                    FullName = mainPerson.FullName,
                    PersonTypeTitle = mainPerson.ONotaryRegisterServiceReqId
                };
                oNotaryRegiserServiceReqEntity.SignablePersons.Add(signablePerson);

                ONotarySeparationEntity seprationPiece = document.documentReportItems.DistinctBy(x => new { x.DocumentId, x.EstateId, x.EstateSeprationPersonId }).Where(x => x.EstateSeprationPersonId == mainPerson.Id)
                    .Select(x => new ONotarySeparationEntity
                    {
                        Area = x.EstateSeprationPiecesArea,
                        Floor = x.EstateSeprationPiecesFloor,
                        Direction = x.EstateSeprationPiecesDirection,
                        PieceNo = x.EstateSeprationPiecesNo,
                        PieceType = x.EstatePieceType,
                        Limit = "حد شمالی : " + x.EstateSeprationPiecesNorthLimit + Environment.NewLine
                               + " حد شرقی : " + x.EstateSeprationPiecesEasternLimit + Environment.NewLine
                               + " حد جنوبی :" + x.EstateSeprationPiecesSouthLimit + Environment.NewLine
                               + " حد غربی :" + x.EstateSeprationPiecesWesternLimit + Environment.NewLine,
                        Moshaat = x.EstateSeprationPiecesCommons.IsNullOrEmpty() ? "ندارد" : x.EstateSeprationPiecesCommons,
                        HoghogheErtefaei = x.EstateSeprationPiecesRights.IsNullOrEmpty() ? "ندارد" : x.EstateSeprationPiecesRights.IsNullOrEmpty() ? "ندارد" : x.EstateSeprationPiecesRights,
                        PieceDescription = x.EstateSeprationPiecesDescription.IsNullOrEmpty() ? "ندارد" : x.EstateSeprationPiecesDescription,
                        Plaque = (x.EstateSeprationPieceSecondaryPlaque.IsNullOrEmpty() ? null : "  فرعی " + x.EstateSeprationPieceSecondaryPlaque + " از ") + " اصلی " + x.EstateSeprationPieceBasicPlaque + " مفروز و مجزا از فرعی " + x.EstateSeprationPieceDividedFromSecondaryPlaque
                             + " - " + (x.EstateSeprationPieceSectionTitle == null || x.EstateSeprationPieceSectionTitle == "ندارد" ? " بخش ثبتی: ندارد " : " بخش ثبتی: " + x.EstateSeprationPieceSectionTitle) + "،"
                             + (x.EstateSeprationPieceSubSectionTitle == null || x.EstateSeprationPieceSubSectionTitle == "ندارد" ? " ناحیه ثبتی: ندارد " : " ناحیه ثبتی " + x.EstateSeprationPieceSubSectionTitle)
                             + (x.EstateSeprationPiecePlaqueText == null ? null : " - " + "پلاک متنی : " + x.EstateSeprationPiecePlaqueText),
                        Sahm = mainPerson.SexTypeDesc + " " + mainPerson.FullName + " مالک " + x.EstateSeprationPieceDetailQuota + " سهم از " + x.EstateSeprationPieceTotalQuota + " سهم  ",
                        Anbar = x.EstateSeprationPieceKindId == "4" ? x.EstatePieceType + " شماره " + x.EstateSeprationPiecesNo : null,
                        Parking = x.EstateSeprationPieceKindId == "3" ? x.EstatePieceType + " شماره " + x.EstateSeprationPiecesNo : null,
                    }).FirstOrDefault();
                oNotaryRegiserServiceReqEntity.SeparationPieces.Add(seprationPiece);


                oNotaryRegiserServiceReqEntity.DocPersons.Add(mainPerson);
            }


            #region DocumentScriptorium

            GetScriptoriumByIdServiceInput scriptoriumQuery = new(new string[] { oNotaryRegiserServiceReqEntity.ScriptoriumId });
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
            string baseInfoUrl = _configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "api/v1/Specific/Ssar/";
            ApiResult<ScriptoriumViewModel> scriptoriumResult = new();
            if (scriptoriumQuery != null)
            {
                scriptoriumResult = await _httpEndPointCaller.CallPostApiAsync<ScriptoriumViewModel, GetScriptoriumByIdServiceInput>(
               new HttpEndpointRequest<GetScriptoriumByIdServiceInput>(baseInfoUrl + ScriptoriumRequestConstant.Scriptorium,
               _userService.UserApplicationContext.Token, scriptoriumQuery), cancellationToken);
            }
            if (scriptoriumResult.IsSuccess)
            {
                oNotaryRegiserServiceReqEntity.ScriptoriumCode = scriptoriumResult.Data.ScriptoriumList.First().Code;
                oNotaryRegiserServiceReqEntity.ScriptoriumAddress = scriptoriumResult.Data.ScriptoriumList.First().Address.Replace("ء", "ی");
                oNotaryRegiserServiceReqEntity.ScriptoriumTitle = scriptoriumResult.Data.ScriptoriumList.First().Name.Replace("شماره", "");

                string input = oNotaryRegiserServiceReqEntity.ScriptoriumTitle;
                string keyword = "استان";
                int index = input.IndexOf(keyword);
                oNotaryRegiserServiceReqEntity.ScriptoriumTitle = index >= 0 ? input[..index] : input;


            }
            #endregion

            #region DocumentRelated-AnnotationPack
            if (oNotaryRegiserServiceReqEntity.ONotaryRegisterServiceTypeId == "009" && oNotaryRegiserServiceReqEntity.RelatedDocumentNo != null)
            {
                List<SharedKernel.Contracts.Coordinator.Document.AnnotationPack> AnnPack = await _annotationsController.CollectAnnotations(oNotaryRegiserServiceReqEntity.ClassifyNo.ToString(), oNotaryRegiserServiceReqEntity.ScriptoriumId, oNotaryRegiserServiceReqEntity.RelatedDocumentNo);
                oNotaryRegiserServiceReqEntity.AnnotationList = AnnPack;
            }
            #endregion

            #region BarCode
            if (!PageHeaderTitle.IsNullOrEmpty())
            {
                string EncryptedText = (oNotaryRegiserServiceReqEntity.NationalNo + "-" + oNotaryRegiserServiceReqEntity.DocumentSecretCode).Encrypt(oNotaryRegiserServiceReqEntity.ScriptoriumCode);
                string EncryptedPass = oNotaryRegiserServiceReqEntity.ScriptoriumCode.Encrypt(EncryptedText[..5]);

                oNotaryRegiserServiceReqEntity.MatrixBarcode = "برای تصدیق اصالت سند از برنامه موبایل سازمان ثبت یا درگاه ssaa.ir استفاده کنید." + "#" + EncryptedText + "#1#" + EncryptedPass;
            }
            #endregion

            #region Stimul

            oNotaryRegiserServiceReqEntity.PageHeaderTitle = PageHeaderTitle;
            oNotaryRegiserServiceReqEntity.IsMain = IsMain;
            oNotaryRegiserServiceReqEntity.IsReduplicatePDF = IsReduplicatePDF;
            oNotaryRegiserServiceReqEntity.IsRooNevesht = IsRooNevesht;
            oNotaryRegiserServiceReqEntity.IsSabtServices = IsSabtServices;

            StiNetCoreActionResult response = GenerateDocumentReport(oNotaryRegiserServiceReqEntity);
            if (response is null)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("مشکلی در ایجاد فایل چاپ به وجود امده است.");
                return apiResult;
            }

            #endregion

            DocumentReportViewModels reportViewModel = new()
            {
                Data = response.Data,
                ContentType = response.ContentType,
                FileName = response.FileName
            };

            apiResult.Data = reportViewModel;

            return apiResult;
        }

        private StiNetCoreActionResult GenerateDocumentReport(ONotaryRegiserServiceReqEntity reportObject)
        {
            StiReport stimulTools = new();

            string reportName = _webHostEnvironment.ContentRootPath + "/Content/Reports/Document/RptReportDocument.mrt";



            stimulTools.Pages[0].Watermark.Text = reportObject.ScriptoriumTitle;
            StiReport report = stimulTools.Load(reportName);
            _ = stimulTools.RegBusinessObject("Request", reportObject);

            StiPageHeaderBand headerand = stimulTools.Pages[0].Components["PageHeaderBand1"] as StiPageHeaderBand;
            StiPageFooterBand footerband = stimulTools.Pages[0].Components["PageFooterBand1"] as StiPageFooterBand;
           
            string documentTypeTitle = reportObject.ONotaryDocumentTypeTitle;
            switch (reportObject.ONotaryDocumentTypeId)
            {
                case "910":
                    documentTypeTitle = reportObject.SayerDocTitle;
                    break;
                case "114":
                    documentTypeTitle = "سند قطعی منقول";
                    break;
                case "117":
                    documentTypeTitle = "سند قطعی مشتمل بر رهن منقول";
                    break;
                case "214":
                    documentTypeTitle = "سند صلح منقول";
                    break;
                case "234":
                    documentTypeTitle = "سند صلح";
                    break;
                case "313":
                    documentTypeTitle = "سند وکالت فروش اموال منقول";
                    break;
                case "417":
                    documentTypeTitle = "سند تسهیلات بانکی";
                    break;
                case "425":
                    documentTypeTitle = "سند رهنی غیر بانکی";
                    break;
                case "731":
                    documentTypeTitle = "سند اقاله";
                    break;
                case "982":
                    documentTypeTitle = "سند وقف";
                    break;
            }
            if (reportObject.RequestOtherInfo != null && reportObject.RequestOtherInfo.IsDocBasedJudgeHokm == 1)
            {
                documentTypeTitle = documentTypeTitle + " - " + "انتقال اجرایی";
            }
            if (string.IsNullOrEmpty(reportObject.PageHeaderTitle))
            {
                stimulTools.Pages[0].Watermark.Text = "";
                StiDataBand dataBand = stimulTools.Pages[0].Components["dbDocument"] as StiDataBand;
               
                if (headerand != null)
                {
                    foreach (StiComponent component in headerand.Components)
                    {
                        if (component is StiText)
                        {
                            if (component is StiText st)
                            {
                                st.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(Stimulsoft.Base.Drawing.StiColor.Get("White"));
                            }
                        }
                        else
                        {
                            component.Enabled = false;
                        }
                    }
                    if (headerand.Components["txtDocTypeTitle"] is StiText txtDocTypeTitle)
                    {
                        txtDocTypeTitle.Enabled = true;
                        txtDocTypeTitle.Text = documentTypeTitle;
                        txtDocTypeTitle.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(Stimulsoft.Base.Drawing.StiColor.Get("Black"));
                        txtDocTypeTitle.CanGrow = true;
                        txtDocTypeTitle.WordWrap = true;
                    }


                }

                if (footerband != null)
                {
                    foreach (StiComponent component in footerband.Components)
                    {
                        if (component is StiText)
                        {
                            if (component is StiText st)
                            {
                                st.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(Stimulsoft.Base.Drawing.StiColor.Get("White"));
                            }
                        }
                    }
                    if (footerband.Components["SubReport4"] is StiSubReport footerSubReport)
                    {
                        footerSubReport.Enabled = true;
                        footerSubReport.Border.Color = Stimulsoft.Base.Drawing.StiColor.Get("White");
                    }
                    if (stimulTools.Pages["subReport_1"].Components["DataBand8"] is StiDataBand dbSign)
                    {
                        dbSign.Border.Color = Stimulsoft.Base.Drawing.StiColor.Get("White");
                        if (dbSign.Components["Panel64"] is StiPanel panelSign)
                        {
                            panelSign.Border.Color = Stimulsoft.Base.Drawing.StiColor.Get("White");
                            if (panelSign.Components["Text128"] is StiText txtSign)
                            {
                                txtSign.Border.Color = Stimulsoft.Base.Drawing.StiColor.Get("White");
                                txtSign.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(Stimulsoft.Base.Drawing.StiColor.Get("White"));
                            }

                        }


                    }

                }

            }
            else
            {
                stimulTools.Pages[0].Watermark.Text = reportObject.IsReduplicatePDF == true ? "نسخه پشتیبان مخصوص بایگانی دفترخانه" : reportObject.ScriptoriumTitle;
                SetPageHeader(reportObject, documentTypeTitle, headerand, footerband);
            }
            SetReport(reportObject, stimulTools);
            if (stimulTools.Pages["subReport_1"].Components["DataBand8"] is StiDataBand DBand8 && stimulTools.Pages["subReport_2"].Components["DataBand12"] is StiDataBand DBand12)
            {
                if (DBand8.Components["Panel64"] is StiPanel panel64 && DBand12.Components["Panel66"] is StiPanel panel66)
                {
                    if (panel64.Components["Image4"] is StiImage Image4)
                    {
                        Image4.File = _webHostEnvironment.ContentRootPath + "\\SignPic.png";
                    }
                    if (panel64.Components["Image8"] is StiImage Image8)
                    {
                        Image8.File = _webHostEnvironment.ContentRootPath + "\\EmtenaPic.png";
                    }
                    if (panel66.Components["Image3"] is StiImage Image3)
                    {
                        Image3.File = _webHostEnvironment.ContentRootPath + "\\SignPic.png";
                    }
                    if (panel66.Components["Image9"] is StiImage Image9)
                    {
                        Image9.File = _webHostEnvironment.ContentRootPath + "\\EmtenaPic.png";
                    }

                    if (string.IsNullOrEmpty(reportObject.PageHeaderTitle))
                    {
                        DBand12.Enabled = false;
                    }
                    if (reportObject.IsReduplicatePDF || string.IsNullOrEmpty(reportObject.PageHeaderTitle))
                    {
                        panel64.Components["Image1"].Enabled = false;
                        panel64.Components["Image4"].Enabled = false;
                        panel64.Components["Image8"].Enabled = false;
                        panel66.Components["Image3"].Enabled = false;
                        panel66.Components["Image5"].Enabled = false;
                        panel66.Components["Image9"].Enabled = false;
                    }
                }


            }

            if (stimulTools.Pages[0].Components["ReportSummaryBand1"] is StiReportSummaryBand stirsb
                && stimulTools.Pages[0].Components["ReportSummaryBand2"] is StiReportSummaryBand stirsb2
                && stimulTools.Pages[0].Components["DataBand21"] is StiDataBand DBand21
                && stimulTools.Pages[0].Components["PageFooterBand1"] is StiPageFooterBand stifoot)
            {
                if (reportObject.IsRooNevesht)
                {
                    stirsb2.Components["SubReport7"].Enabled = true;
                    stirsb2.Enabled = true;
                    stirsb.Components["SubReport1"].Enabled = false;
                    stifoot.Components["SubReport4"].Enabled = false;
                    DBand21.Enabled = true;
                    DBand21.Components["SubReport8"].Enabled = true;
                    stirsb.Enabled = false;
                    headerand.Components["Text215"].Enabled = false;
                    headerand.Components["txtKafilSardaftar"].Enabled = false;
                    headerand.Components["txtReportType"].Enabled = false;
                    headerand.Components["txtExordiumNameFamily"].Enabled = false;
                    if (headerand.Components["txtDocTypeTitle"] is StiText txtreport)
                    {
                        txtreport.Text = "رونوشت دفتر";
                    }
                    if (headerand.Components["txtDaftaryarInfo"] is StiText txtDaftaryarInfo)
                    {
                        txtDaftaryarInfo.Text = "رونوشت دفتر" + reportObject.ONotaryDocumentTypeTitle + " به شماره " + reportObject.ClassifyNo + " مورخ " + reportObject.ConfDate + " که به ......ارائه می شود.";
                    }
                }
                else
                {
                    if (reportObject.SignablePersonCount < 30)
                    {
                        stirsb.Enabled = false;
                        stirsb.Components["SubReport1"].Enabled = false;
                        stirsb2.Components["SubReport7"].Enabled = false;
                        DBand21.Components["SubReport8"].Enabled = false;
                        DBand21.Enabled = false;
                        stirsb2.Enabled = false;
                        stifoot.Components["SubReport4"].Enabled = true;
                    }
                    else if (reportObject.SignablePersonCount >= 30)
                    {
                        stirsb.Enabled = false;
                        stirsb.Components["SubReport1"].Enabled = false;
                        stirsb2.Components["SubReport7"].Enabled = true;
                        stirsb2.Enabled = true;
                        DBand21.Components["SubReport8"].Enabled = false;
                        DBand21.Enabled = false;
                        stifoot.Components["SubReport4"].Enabled = false;
                    }
                }
            }
            StiPdfExportSettings PdfSettings = new()
            {

                EmbeddedFonts = false,
                StandardPdfFonts = true,

            };
            StiNetCoreActionResult generatedReport = StiNetCoreReportResponse.ResponseAsPdf(report, PdfSettings);
            return generatedReport;
        }
        private static void SetReport(ONotaryRegiserServiceReqEntity reportObject, StiReport stimulTools)
        {
            if (reportObject == null || stimulTools == null)
            {
                return;
            }

            StiPage srRegCasePage = stimulTools.Pages["srRegCase"];
            StiPage subReport3Page = stimulTools.Pages["subReport_3"];
            StiPage firstPage = stimulTools.Pages.Count > 0 ? stimulTools.Pages[0] : null;

            // === Case title conditions ===
            if (srRegCasePage?.Components["dbRegCaseTitle"] is StiDataBand dbRegCaseTitle)
            {
                StiCondition sc = new(
                    "Request.RegCases != null && Request.RegCases.WealthType==2 || Request.RegCases != null && Request.RegCases.WealthType==1",
                    Stimulsoft.Base.Drawing.StiColor.Get("Red"),
                    Stimulsoft.Base.Drawing.StiColor.Get("Blue"),
                    null,
                    false
                );
                dbRegCaseTitle.Conditions.Add(sc);
            }

            // === Separation panels ===
            if (srRegCasePage?.Components["dbImmovableAssetTypeAttributes"] is StiDataBand separationBand)
            {
                if (separationBand.Components["Panel2"] is StiPanel panel2)
                {
                    if (panel2.Components["Panel29"] is StiPanel panel29 &&
                        panel29.Components["Text138"] is StiText txt138)
                    {
                        if (reportObject.ONotaryDocumentCode == "611")
                        {
                            txt138.Enabled = false;
                        }
                    }

                    if (panel2.Components["Panel103"] is StiPanel panel103 &&
                        reportObject.ONotaryDocumentCode == "611")
                    {
                        foreach (string key in new[] { "Text45", "Text46", "Text47", "Text48", "Text30", "Text29" })
                        {
                            if (panel103.Components[key] is StiText t)
                            {
                                t.Enabled = false;
                            }
                        }
                    }
                }

                if (separationBand.Components["Panel3"] is StiPanel panel3 &&
                    panel3.Components["Panel25"] is StiPanel panel25 &&
                    reportObject.ONotaryDocumentCode == "611")
                {
                    foreach (string key in new[] { "Text9", "Text64" })
                    {
                        if (panel25.Components[key] is StiText t)
                        {
                            t.Enabled = false;
                        }
                    }
                }

                if (separationBand.Components["Panel4"] is StiPanel panel4 &&
                    panel4.Components["Panel22"] is StiPanel panel22 &&
                    reportObject.ONotaryDocumentCode == "611")
                {
                    foreach (string key in new[] { "Text72", "Text71", "Text74", "Text73" })
                    {
                        if (panel22.Components[key] is StiText t)
                        {
                            t.Enabled = false;
                        }
                    }
                }

                // SubReport toggle
                if (separationBand.Components["SubReportSeparationPiece"] is StiSubReport subReport)
                {
                    subReport.Enabled = reportObject.ONotaryDocumentCode == "611";
                }
            }

            // === SubReport_3 panels ===
            if (subReport3Page?.Components["DataBand3"] is StiDataBand db3 &&
                db3?.Components["Panel18"] is StiPanel panel18 &&
                panel18?.Components["Panel39"] is StiPanel panel39 &&
                reportObject.ONotaryDocumentCode == "611")
            {
                if (panel39.Components["Text35"] is StiText txt35)
                {
                    txt35.Enabled = false;
                }
            }

            // === WealthType conditions ===
            if (srRegCasePage?.Components["dbImmovableAssetTypeAttributes"] is StiDataBand dbRegCase)
            {
                StiCondition scon = new(
                    "Request.RegCases.WealthType!=2",
                    Stimulsoft.Base.Drawing.StiColor.Get("Red"),
                    Stimulsoft.Base.Drawing.StiColor.Get("Blue"),
                    null,
                    false
                );
                dbRegCase.Conditions.Add(scon);
            }

            if (srRegCasePage?.Components["DataBandManghoolKhodro"] is StiDataBand dbRegCaseManghool)
            {
                StiCondition sconManghool = new(
                    "Request.RegCases.WealthType!=1",
                    Stimulsoft.Base.Drawing.StiColor.Get("Red"),
                    Stimulsoft.Base.Drawing.StiColor.Get("Blue"),
                    null,
                    false
                );
                dbRegCaseManghool.Conditions.Add(sconManghool);
            }

            // === Font size ===
            decimal fontSize = reportObject.DocumentTextFontSize is >= 8 and <= 14
                ? reportObject.DocumentTextFontSize.Value
                : 14;

            if (firstPage?.Components["dbLegalText"] is StiDataBand dbLegalText &&
                dbLegalText.Components["txtLegalText"] is StiText txtLegalText)
            {
                txtLegalText.Font = new Stimulsoft.Drawing.Font("B Nazanin", (float)fontSize);
            }

            // === Print mode ===
            if (reportObject.PrintMode == "WYSIWYG" &&
                firstPage?.Components["dbLegalText"] is StiDataBand dbLegalTextPrintMode &&
                dbLegalTextPrintMode.Components["txtLegalText"] is StiText txtLegalTextPrintMode)
            {
                txtLegalTextPrintMode.TextQuality = StiTextQuality.Wysiwyg;
            }

            // === Document code handling ===
            if (short.TryParse(reportObject.ONotaryDocumentCode, out short docCode))
            {
                SetReportByDocumentTypeCode(docCode, reportObject, stimulTools);
            }

            // === Hide price ===
            HidePrice(stimulTools, reportObject);
        }



        private static void HidePrice(StiReport stimulTools, ONotaryRegiserServiceReqEntity reportObject)
        {
            if (stimulTools.Pages["mainPage"].Components["dbPrice"].Enabled == true)
            {
                bool isPriceEmpty = false;
                if (reportObject.Price == null || reportObject.Price == 0)
                {
                    isPriceEmpty = true;
                }

                bool isHowToPayEmpty = false;
                if (string.IsNullOrEmpty(reportObject.HowToPay) || reportObject.HowToPay == "ــــ")
                {
                    isHowToPayEmpty = true;
                }

                if (isPriceEmpty && isHowToPayEmpty)
                {
                    stimulTools.Pages["mainPage"].Components["dbPrice"].Enabled = false;
                }
            }
        }

        private static void SetReportByDocumentTypeCode(int code, ONotaryRegiserServiceReqEntity reportObject, StiReport stimulTools)
        {
            if (stimulTools == null)
            {
                return;
            }

            switch (code)
            {
                // --- بیع ---
                case 111: break;  /* سند قطعي غيرمنقول */
                case 112: break;  /* سند قطعي غيرمنقول با حق استرداد */
                case 115: break;  /* سند قطعي مشتمل بر رهن - غيرمنقول */
                case 113:
                case 114:
                case 116:
                case 117:
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;

                // --- صلح ---
                case 211: break;
                case 212: break;
                case 213:
                case 214:
                case 223:
                case 224:
                case 233:
                case 234:
                    HideOwnerShipDoc(stimulTools);
                    break;
                case 221: break;
                case 222: break;
                case 231:
                case 232:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    break;

                // --- وکالت ---
                case 311:
                case 333:
                case 313:
                case 321:
                case 332:
                case 331:
                case 323:
                    HidePrice(stimulTools);
                    SetVacalatKhodroFields(stimulTools);
                    SetMelkGhyerManghool(stimulTools);
                    break;
                case 312:
                    HidePrice(stimulTools);
                    SetVacalatKhodroFields(stimulTools);
                    SetMelkGhyerManghool(stimulTools);
                    HideInquiries(stimulTools);
                    break;
                case 322:
                    HidePrice(stimulTools);
                    SetVecalatKariGheyrManghool(stimulTools);
                    break;

                // --- اسناد رهنی بانکی ---
                case 411:
                case 412:
                case 413:
                case 414:
                case 415:
                case 416:
                case 417:
                    HideVazaiteTakhlieAndHowToPay(stimulTools);
                    break;

                // --- اسناد رهنی غیر بانکی ---
                case 421:
                case 422:
                case 423:
                case 424:
                case 425:
                    HideInquiries(stimulTools);
                    HideVazaiteTakhlieAndHowToPay(stimulTools);
                    break;

                // --- اسناد تعویض یا ضم وثیقه ---
                case 431:
                    HideVazaiteTakhlieAndHowToPay(stimulTools);
                    break;

                // --- اجاره ---
                case 511: break;
                case 521:
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;

                // --- تقسیم نامه ---
                case 611:
                case 612:
                    HidePrice(stimulTools);
                    break;
                case 613:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;

                // --- اقاله ---
                case 711:
                    HidePrice(stimulTools);
                    break;
                case 721:
                case 731:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;

                // --- حق انتفاع ---
                case 811: break;

                // --- سایر اسناد ---
                case 901:
                    HidePrice(stimulTools);
                    break;
                case 910:
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    if (reportObject?.Price == null || reportObject.Price == 0)
                    {
                        HidePrice(stimulTools);
                    }

                    break;
                case 911:
                case 922:
                case 931:
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;
                case 912:
                case 913:
                case 923:
                case 932:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;
                case 921:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;
                case 941:
                    HidePrice(stimulTools);
                    break;
                case 942:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;
                case 951:
                    HidePrice(stimulTools);
                    break;
                case 961: break;
                case 971:
                    HidePrice(stimulTools);
                    break;
                case 972:
                case 982:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    break;
                case 981:
                    HidePrice(stimulTools);
                    break;
                case 991: break;

                // --- خاص ---
                case 8:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    HideRegCaseTitle(stimulTools);
                    SetLegalText(stimulTools, "متن ");
                    break;
                case 5:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideRegCaseTitle(stimulTools);
                    SetLegalText(stimulTools, "متن فسخ");
                    break;
                case 4:
                    HidePrice(stimulTools);
                    SetLegalText(stimulTools, "متن فک رهن");
                    break;
                case 6:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    HideRegCaseTitle(stimulTools);
                    SetLegalText(stimulTools, "متن حقوقی");
                    break;
                case 12:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    HideRegCaseTitle(stimulTools);
                    SetLegalText(stimulTools, "متن گواهی عدم حضور");
                    break;
                case 22:
                    HidePrice(stimulTools);
                    HideOwnerShipDoc(stimulTools);
                    HideInquiries(stimulTools);
                    HideRegCaseTitle(stimulTools);
                    SetLegalText(stimulTools, "توضیحات");
                    break;
                case 7:
                    HideInquiries(stimulTools);
                    HideRegCaseTitle(stimulTools);
                    SetLegalText(stimulTools, "متن اجرائیه");
                    break;
                case 2:
                    HideInquiries(stimulTools);
                    HideRegCaseTitle(stimulTools);
                    //SetLegalText(stimulTools, "توضیحات");
                    break;
            }
        }

        // helper for lblLegalText
        private static void SetLegalText(StiReport stimulTools, string text)
        {
            if (stimulTools.Pages["mainPage"]?.Components["dbLegalText"] is StiDataBand dbLegalText &&
                dbLegalText.Components["lblLegalText"] is StiText lblLegalText)
            {
                lblLegalText.Text = text;
            }
        }

        private static void HideRegCaseTitle(StiReport stimulTools)
        {
            stimulTools.Pages["srRegCase"].Components["pageHeaderBandRegCase"].Enabled = false;
        }

        private static void HideVazaiteTakhlieAndHowToPay(StiReport stimulTools)
        {
            if (stimulTools == null)
            {
                return;
            }

            if (stimulTools.Pages["srRegCase"]?.Components["dbImmovableAssetTypeAttributes"] is StiDataBand dataBand &&
                dataBand.Components["Panel5"] is StiPanel panel5 &&
                panel5.Components["PanelTakhliye"] is StiPanel panelTakhliye)
            {
                panelTakhliye.Enabled = false;
            }

            if (stimulTools.Pages["mainPage"]?.Components["dbPrice"] is StiDataBand dbPrice &&
                dbPrice.Components["Panel53"] is StiPanel panel53)
            {
                panel53.Enabled = false;
            }
        }


        private static void SetVecalatKariGheyrManghool(StiReport stimulTools)
        {
            StiDataBand databand = (StiDataBand)stimulTools.Pages["srRegCase"].Components["dbImmovableAssetTypeAttributes"];
            StiDataBand databand1 = (StiDataBand)stimulTools.Pages["srRegCase"].Components["DataBandVecalatKariGhyerManghool"];
            databand.Enabled = false;
            databand1.Enabled = true;

        }

        private static void SetMelkGhyerManghool(StiReport stimulTools)
        {
            if (stimulTools == null)
            {
                return;
            }

            if (stimulTools.Pages["srRegCase"]?.Components["dbImmovableAssetTypeAttributes"] is not StiDataBand databand)
            {
                return;
            }

            if (databand.Components["Panel3"] is StiPanel panel3 &&
                panel3.Components["PanelMelkInfo"] is StiPanel panelMelkInfo)
            {
                panelMelkInfo.Enabled = false;
            }

            if (databand.Components["Panel4"] is StiPanel panel4 &&
                panel4.Components["PanelPayanKarShahrdari"] is StiPanel panelPayanKarShahrdari)
            {
                panelPayanKarShahrdari.Enabled = false;
            }

            if (databand.Components["Panel5"] is StiPanel panel5 &&
                panel5.Components["PanelTakhliye"] is StiPanel panelTakhliye)
            {
                panelTakhliye.Enabled = false;
            }

            if (databand.Components["Panel2"] is StiPanel panel2 &&
                panel2.Components["Panel29"] is StiPanel panel29 &&
                panel29.Components["Text138"] is StiText txt138)
            {
                txt138.Enabled = false;
            }
        }


        private static void SetVacalatKhodroFields(StiReport stimulTools)
        {
            if (stimulTools == null)
            {
                return;
            }

            // Navigate safely: srRegCase -> DataBandManghoolKhodro -> Panel55 -> pnlEntezami
            if (stimulTools.Pages["srRegCase"]?.Components["DataBandManghoolKhodro"] is StiDataBand dbManghool &&
                dbManghool.Components["Panel55"] is StiPanel panel55 &&
                panel55.Components["pnlEntezami"] is StiPanel panel &&
                panel.Components["lblSeller"] is StiText lblSeller &&
                panel.Components["txtSeller"] is StiText txtSeller &&
                panel.Components["lblBuyer"] is StiText lblBuyer &&
                panel.Components["txtBuyer"] is StiText txtBuyer)
            {
                // Disable buyer fields
                lblBuyer.Enabled = false;
                txtBuyer.Enabled = false;

                // Adjust seller fields
                lblSeller.Text = "شماره انتظامی:";
                lblSeller.Width = 0.91;
                lblSeller.Left = 6.49;

                txtSeller.Width = 2.8;
                txtSeller.Left = 3.69;
            }
        }


        private static void HidePrice(StiReport stimulTools)
        {
            stimulTools.Pages["mainPage"].Components["dbPrice"].Enabled = false;
        }

        private static void HideInquiries(StiReport stimulTools)
        {
            stimulTools.Pages["mainPage"].Components["dbInquiries"].Enabled = false;
        }

        private static void HideOwnerShipDoc(StiReport stimulTools)
        {
            // Safe access to pages and components with null checking
            if (stimulTools.Pages["srRegCase"]?.Components["dbImmovableAssetTypeAttributes"] is StiDataBand dbImmovableAssetTypeAttributes)
            {
                if (dbImmovableAssetTypeAttributes.Components["SubReportAsset"] is StiSubReport subReportAsset)
                {
                    subReportAsset.Enabled = false;
                }
            }

            if (stimulTools.Pages["srRegCase"]?.Components["DataBandManghoolKhodro"] is StiDataBand dataBandManghoolKhodro)
            {
                if (dataBandManghoolKhodro.Components["SubReportDataBandManghoolKhodro"] is StiSubReport subReportDataBandManghoolKhodro)
                {
                    subReportDataBandManghoolKhodro.Enabled = false;
                }
            }

            if (stimulTools.Pages["srRegCase"]?.Components["DataBandVecalatKariGhyerManghool"] is StiDataBand dataBandVecalatKariGhyerManghool)
            {
                if (dataBandVecalatKariGhyerManghool.Components["SubReportDataBandVecalatKariGhyerManghool"] is StiSubReport subReportDataBandVecalatKariGhyerManghool)
                {
                    subReportDataBandVecalatKariGhyerManghool.Enabled = false;
                }
            }

            if (stimulTools.Pages["srRegCase"]?.Components["dbRegCaseTitle"] is StiDataBand dbRegCaseTitle)
            {
                if (dbRegCaseTitle.Components["SubReportRegCaseTitle"] is StiSubReport subReportRegCaseTitle)
                {
                    subReportRegCaseTitle.Enabled = false;
                }
            }
        }
        private static void SetPageHeader(ONotaryRegiserServiceReqEntity reportObject, string documentTypeTitle, StiPageHeaderBand headerand, StiPageFooterBand footerband)
        {
            if (headerand == null || footerband == null)
            {
                return;
            }

            string daftarYarInfo = @"این سند در دفتر الکترونیک {0} تحت شماره {1} مورخ {2} ثبت شده است.";
            daftarYarInfo = string.Format(daftarYarInfo, reportObject.ScriptoriumTitle, reportObject.ClassifyNo, reportObject.ConfDate);

            // Safe component access with pattern matching
            if (headerand.Components["txtDocTypeTitle"] is StiText txtDocTypeTitle)
            {
                txtDocTypeTitle.Text = documentTypeTitle;
            }

            if (headerand.Components["txtExordiumNameFamily"] is StiText txtExordiumNameFamily)
            {
                txtExordiumNameFamily.Text = reportObject.ExordiumNameFamily;
            }

            if (headerand.Components["txtKafilSardaftar"] is StiText txtkafiltitle)
            {
                txtkafiltitle.Text = reportObject.BailsmanNameFamily;
            }

            switch (reportObject.ONotaryDocumentCode)
            {
                case "0012": // گواهی عدم حضور
                case "002":  // قبض سپرده
                case "0022": // استعفای وکیل
                case "004":  // فک رهن
                case "005":  // فسخ سند غیر منقول
                case "006":  // عزل وکیل
                case "007":  // اجرائیه
                case "0034": // رفع نقص اجرائیه
                case "008":  // اخطاریه
                    if (headerand.Components["txtNationalNo"] is StiText txtNationalNo)
                    {
                        txtNationalNo.Text = "شناسه تصدیق: " + reportObject.NationalNo;
                    }

                    if (reportObject.IsReduplicatePDF)
                    {
                        daftarYarInfo = Environment.NewLine + "تاریخ اخذ شناسه یکتا : " + reportObject.GetDocNoDate;

                        if (headerand.Components["Text146"] is StiText text146)
                        {
                            text146.Text = "نسخه پشتیبان مخصوص بایگانی دفترخانه";
                        }
                    }
                    else
                    {
                        if (!reportObject.IsSabtServices)
                        {
                            if (reportObject.ONotaryDocumentCode == "0012" || reportObject.ONotaryDocumentCode == "008")
                            {
                                daftarYarInfo = @"این تصدیق با شماره {3} در صفحه {0} دفتر اندیکاتور جلد {1} {2} در تاریخ {4} درج شده است.";
                                daftarYarInfo = string.Format(daftarYarInfo, reportObject.RegisterPapersNo, reportObject.RegisterVolumeNo, reportObject.ScriptoriumTitle, reportObject.ClassifyNo, reportObject.ConfDate);
                            }
                            else if (reportObject.ONotaryDocumentCode == "002")
                            {
                                daftarYarInfo = @"این تصدیق با شماره {3} در صفحه {0} دفتر امانات جلد {1} {2} در تاریخ {4} درج شده است.";
                                daftarYarInfo = string.Format(daftarYarInfo, reportObject.RegisterPapersNo, reportObject.RegisterVolumeNo, reportObject.ScriptoriumTitle, reportObject.ClassifyNo, reportObject.ConfDate);
                            }
                            else
                            {
                                daftarYarInfo = @"این تصدیق در ستون ملاحظات سند شماره {0} دفتر الکترونیک {1} در تاریخ {2} درج شده است.";
                                daftarYarInfo = string.Format(daftarYarInfo, reportObject.ClassifyNo, reportObject.ScriptoriumTitle, reportObject.ConfDate);
                            }
                        }
                        else if (reportObject.IsSabtServices)
                        {
                            daftarYarInfo = @"این تصدیق در ستون ملاحظات سند شماره {0} دفتر الکترونیک {1} در تاریخ {2} درج شده است.";
                            daftarYarInfo = string.Format(daftarYarInfo, reportObject.ClassifyNo, reportObject.ScriptoriumTitle, reportObject.ConfDate);
                        }
                    }

                    if (headerand.Components["txtReportType"] is StiText txtReportType)
                    {
                        txtReportType.Enabled = false;
                    }

                    if (footerband.Components["Text122"] is StiText text122)
                    {
                        text122.Text = text122.Text.Value?.Replace("شناسه سند و ", "") ?? string.Empty;
                    }

                    if (headerand.Components["Text215"] is StiText text215)
                    {
                        text215.Text = daftarYarInfo;
                        text215.Font = new Stimulsoft.Drawing.Font("B Nazanin", 8f);
                    }

                    if (headerand.Components["txtDaftaryarInfo"] is StiText txtDaftaryarInfo)
                    {
                        txtDaftaryarInfo.Text = "";
                    }
                    break;

                default:
                    if (headerand.Components["txtNationalNo"] is StiText txtNationalNoDefault)
                    {
                        txtNationalNoDefault.Text = "شناسه سند: " + reportObject.NationalNo;
                    }

                    if (!reportObject.IsReduplicatePDF && reportObject.IsMoaref)
                    {
                        if (headerand.Components["Text215"] is StiText text215Default)
                        {
                            text215Default.Text = @"با احراز هویت امضاء کننده/امضاءکنندگان ذیل سند توسط معرف/ معرفین، تمام مراتب مسطور در این سند نزد اینجانب واقع شد.";
                        }
                    }

                    if (reportObject.IsReduplicatePDF)
                    {
                        if (headerand.Components["txtDaftaryarInfo"] is StiText txtDaftaryarInfoRedup)
                        {
                            txtDaftaryarInfoRedup.Text = "      تاریخ اخذ شناسه یکتا : " + reportObject.DocDate;
                        }

                        if (headerand.Components["Text146"] is StiText text146Redup)
                        {
                            text146Redup.Text = "نسخه پشتیبان مخصوص بایگانی دفترخانه";
                        }
                    }
                    else
                    {
                        if (headerand.Components["txtDaftaryarInfo"] is StiText txtDaftaryarInfoNormal)
                        {
                            txtDaftaryarInfoNormal.Text = daftarYarInfo;
                        }
                    }
                    break;
            }

            if (headerand.Components["txtDocumentSecretCode"] is StiText txtDocumentSecretCode)
            {
                txtDocumentSecretCode.Text = "رمز تصدیق: " + reportObject.DocumentSecretCode;
            }

            //string EncryptedText = StringExtensions.Encrypt(reportObject.NationalNo + "-" + reportObject.DocumentSecretCode, reportObject.ScriptoriumCode);
            //string EncryptedPass = StringExtensions.Encrypt(reportObject.ScriptoriumCode, EncryptedText.Substring(0, 5));

            //if (headerand.Components["imgMatrixBarcode"] is StiImage imgBarcodeMatrix)
            //{
            //    imgBarcodeMatrix.Image = StringExtensions.GetQRBarcode("برای تصدیق اصالت سند از برنامه موبایل سازمان ثبت یا درگاه ssaa.ir استفاده کنید." + "#" + EncryptedText + "#1#" + EncryptedPass);
            //}

            string scriptoriumTitle = @"{0}  
نشانی دفترخانه : {1} ";
            scriptoriumTitle = string.Format(scriptoriumTitle, reportObject.ScriptoriumTitle, reportObject.ScriptoriumAddress);

            if (headerand.Components["txtScriptoriumTitle"] is StiText txtScriptoriumTitle)
            {
                txtScriptoriumTitle.Text = scriptoriumTitle;
            }

            if (footerband.Components["txtFooterSignTitle"] is StiText)
            {
                // Handle footer sign title if needed
            }
        }
        private static string GetOwnershipDoc(DocumentReportItem x)
        {
            string title = string.Empty;
            switch ((NotaryOwnershipDocumentType)x.EstateOwnershipDocumentType.ToRequiredInt())
            {
                case NotaryOwnershipDocumentType.SabtDocument:

                    if (!string.IsNullOrWhiteSpace(x.EstateOwnershipDocumentEloctronicPageNo))
                    {
                        title =
                        ((NotaryOwnershipDocumentType)x.EstateOwnershipDocumentType.ToRequiredInt()).GetDescription() + " " +
                         ((EstateDocType)x.EstateDocumentType.ToRequiredInt()).GetDescription() +
                        " با شماره صفحه دفتر الکترونیک املاک : " + x.EstateOwnershipDocumentEloctronicPageNo;
                        if (x.EstateOwnershipDocumentIsReplacment == YesNo.Yes.GetString())
                        {
                            title += " - سند المثنی است.";
                        }
                    }
                    else
                    {
                        title =
                         ((NotaryOwnershipDocumentType)x.EstateOwnershipDocumentType.ToRequiredInt()).GetDescription() + " " +
                        ((EstateDocType)x.EstateDocumentType.ToRequiredInt()).GetDescription() +
                        " به شماره ثبت: " + x.EstateOwnershipSabtNo +
                        " - شماره چاپی سند: " + x.EstateOwnershipDocNo +
                        " - شماره دفتر: " + x.EstateOwnershipBookNo +
                        " - شماره صفحه دفتر: " + x.EstateOwnershipBookPageNo;
                        if (x.EstateOwnershipDocumentIsReplacment == YesNo.Yes.GetString())
                        {
                            title += " - سند المثنی است";
                        }

                        if (x.EstateOwnershipSardaftar != null)
                        {
                            title +=
                                " -  سری دفتر: " + x.EstateOwnershipSardaftar;
                        }
                    }

                    if (string.IsNullOrEmpty(x.EstateOwnershipBooktype) && x.EstateOwnershipBooktype != NotaryBookType.None.GetString())
                    {
                        title += " - نوع دفتر: " + ((NotaryBookType)x.EstateOwnershipBooktype.ToRequiredInt()).GetDescription();
                    }

                    break;
                case NotaryOwnershipDocumentType.SabtStateReport:
                    title =
                        "شماره گزارش وضعیت ثبتی: " + x.EstateOwnershipSabtStateReportNo +
                        " - تاریخ گزارش وضعیت ثبتی: " + x.EstateOwnershipSabtStateReportDate;
                    if (!string.IsNullOrEmpty(x.EstateOwnershipSabtStateReportUnitName))
                    {
                        title += " - صادرکننده گزارش وضعیت ثبتی: " + x.EstateOwnershipSabtStateReportUnitName;
                    }

                    break;
                case NotaryOwnershipDocumentType.NotaryDocument:
                    title =
                        "دفترخانه: " + x.EstateOwnershipNotaryNo +
                        " - " + x.EstateOwnershipNotaryLocation +
                        " - شماره: " + x.EstateOwnershipNotaryDocumentNo +
                        " - تاریخ: " + x.EstateOwnershipNotaryDocumentDate;
                    break;
            }
            if (x.EstateOwnershipDescription != null && !string.IsNullOrEmpty(x.EstateOwnershipDescription))
            {
                title += " - توضیحات: " + x.EstateOwnershipDescription;
            }

            return title;
        }
    }
}
