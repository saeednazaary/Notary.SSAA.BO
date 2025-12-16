using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Utilities.Estate;



namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetDocumentRelatedDataQueryHandler : BaseQueryHandler<GetDocumentRelatedDataQuery, ApiResult<GetDocumentRelatedDataViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IRepository<Domain.Entities.DocumentEstateInquiry> _documentEstateInquiryRepository;


        public GetDocumentRelatedDataQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository, IRepository<Domain.Entities.DocumentEstateInquiry> documentEstateInquiryRepository)
            : base(mediator, userService)
        {

            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            _documentEstateInquiryRepository = documentEstateInquiryRepository;
        }
        protected override bool HasAccess(GetDocumentRelatedDataQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
      
        protected async override Task<ApiResult<GetDocumentRelatedDataViewModel>> RunAsync(GetDocumentRelatedDataQuery request, CancellationToken cancellationToken)
        {
            ApiResult<GetDocumentRelatedDataViewModel> apiResult = new() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };
            GetDocumentRelatedDataViewModel result = new() { DocumentRelatedData = new DocumentRelatedData() };
            apiResult.Data = result;

            var restricted = IsRestricted(request.DocumentTypeCode);
            if (request.CheckRepeatedRequest == true && (!restricted.HasValue || !restricted.Value))
            {
                var idList = new List<Guid?>();
                foreach (var item in request.EstateInquiryId)
                {
                    idList.Add(item.ToGuid());
                }
                var stateList = new string[] { "8", "9" };
                var currentRequest = await _documentEstateInquiryRepository.
                    TableNoTracking
                    .Where(x => idList.Contains(x.EstateInquiryId) && !stateList.Contains(x.DocumentEstate.Document.State) && x.DocumentEstate.Document.ScriptoriumId == _userService.UserApplicationContext.BranchAccess.BranchCode)
                    .Select(x => x.DocumentEstate.Document.RequestNo)
                    .ToListAsync(cancellationToken);
                if (currentRequest != null && currentRequest.Count > 0)
                {
                    var msg = "استعلام های انتخاب شده شما، در پرونده های زیر در حال استفاده می باشند:\n\n";
                    foreach (var no in currentRequest)
                    {
                        msg += "-  " + no + System.Environment.NewLine;
                    }

                    msg += "\n" + "لطفاً در صورت اطمینان از انتخاب صحیح استعلام ها، فرایند ایجاد پرونده را ادامه دهید.";
                    result.HasAlarmMessage = true;
                    result.AlarmMessage = msg;                    
                }
            }
            if (result.HasAlarmMessage)
            {
                return apiResult;
            }

            foreach (var inquiryId in request.EstateInquiryId)
            {
                var theEstateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, inquiryId.ToGuid());
                if (theEstateInquiry != null)
                {
                    await _estateInquiryRepository.LoadCollectionAsync(theEstateInquiry, x => x.EstateInquiryPeople, cancellationToken);
                    var estateInquiryPerson = theEstateInquiry.EstateInquiryPeople.First();
                    
                    var theDocumentEstate = result.DocumentRelatedData.DocumentEstateList.Where(x => x.BasicPlaque == theEstateInquiry.Basic && x.SecondaryPlaque == theEstateInquiry.Secondary && x.EstateSectionId == theEstateInquiry.EstateSectionId && x.EstateSubsectionId == theEstateInquiry.EstateSubsectionId).FirstOrDefault();
                    if (theDocumentEstate == null)
                    {
                        theDocumentEstate = new DocumentEstate()
                        {
                            Address = theEstateInquiry.EstateAddress,
                            Area = theEstateInquiry.Area,
                            BasicPlaque = theEstateInquiry.Basic,
                            BasicPlaqueHasRemain = theEstateInquiry.BasicRemaining,
                            EstateSectionId = theEstateInquiry.EstateSectionId,
                            EstateSubsectionId = theEstateInquiry.EstateSubsectionId,
                            GeoLocationId = theEstateInquiry.GeoLocationId,
                            Id = Guid.NewGuid(),
                            IsAttachment = (request.IsAttachment != null) ? request.IsAttachment.ToYesNo() : null,
                            Ilm = "1",
                            IsProportionateQuota = (request.DocumentTypeCode != "901") ? "1" : "2",
                            IsRegistered = (request.IsRegistered != null) ? request.IsRegistered.ToYesNo() : null,
                            PostalCode = theEstateInquiry.EstatePostalCode,
                            ScriptoriumId = theEstateInquiry.ScriptoriumId,
                            SecondaryPlaque = theEstateInquiry.Secondary,
                            SecondaryPlaqueHasRemain = theEstateInquiry.SecondaryRemaining,
                            EstateInquiryId = theEstateInquiry.Id.ToString(),
                            UnitId = theEstateInquiry.UnitId

                        };                        
                        result.DocumentRelatedData.DocumentEstateList.Add(theDocumentEstate);
                    }
                    else
                    {
                        theDocumentEstate.EstateInquiryId += "," + theEstateInquiry.Id.ToString();                        
                    }

                    DocumentPerson documentPerson = null;
                    if (!string.IsNullOrWhiteSpace(estateInquiryPerson.NationalityCode))
                        documentPerson = result.DocumentRelatedData.DocumentPersonList.Where(p => p.NationalNo == estateInquiryPerson.NationalityCode).FirstOrDefault();
                    else
                        documentPerson = result.DocumentRelatedData.DocumentPersonList
                            .Where(p =>
                                   string.Concat(p.Name,p.Family).NormalizeTextChars(false).Replace(" ", "") == string.Concat(estateInquiryPerson.Name,estateInquiryPerson.Family).NormalizeTextChars(false).Replace(" ", "")
                                   )
                            .FirstOrDefault();
                    if (documentPerson == null)
                    {
                        documentPerson = new DocumentPerson()
                        {
                            Address = estateInquiryPerson.Address,
                            BirthDate = (estateInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.RealPerson) ? estateInquiryPerson.BirthDate : "",
                            CompanyRegisterDate = (estateInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.LegalPerson) ? estateInquiryPerson.BirthDate : "",
                            CompanyRegisterNo = (estateInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.LegalPerson) ? estateInquiryPerson.IdentityNo : "",
                            EstateInquiryId = theEstateInquiry.Id.ToString(),
                            Family = estateInquiryPerson.Family,
                            FatherName = estateInquiryPerson.Family,
                            Id = Guid.NewGuid(),
                            IdentityNo = (estateInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.RealPerson) ? estateInquiryPerson.IdentityNo : "",
                            IdentityIssueGeoLocationId = estateInquiryPerson.IssuePlaceId,
                            IdentityIssueLocation = estateInquiryPerson.IssuePlaceText,
                            Ilm = "1",
                            IsIranian = estateInquiryPerson.IsIranian,
                            IsOriginal = "1",
                            IsRelated = "2",
                            IsSabtahvalChecked = estateInquiryPerson.IsSabtahvalChecked,
                            IsSabtahvalCorrect = estateInquiryPerson.IsSabtahvalCorrect,
                            MobileNo = estateInquiryPerson.MobileNo,
                            MobileNoState = estateInquiryPerson.MobileNoState,
                            Name = estateInquiryPerson.Name,
                            NationalityId = estateInquiryPerson.NationalityId,
                            NationalNo = estateInquiryPerson.NationalityCode,
                            PersonType = estateInquiryPerson.PersonType,
                            PostalCode = estateInquiryPerson.PostalCode,
                            SanaState = estateInquiryPerson.SanaState,
                            ScriptoriumId = estateInquiryPerson.ScriptoriumId,
                            Seri = estateInquiryPerson.Seri,
                            Serial = estateInquiryPerson.SerialNo,
                            SeriAlpha = estateInquiryPerson.SeriAlpha,
                            SexType = estateInquiryPerson.SexType

                        };

                        result.DocumentRelatedData.DocumentPersonList.Add(documentPerson);
                    }
                    else
                        documentPerson.EstateInquiryId += "," + theEstateInquiry.Id.ToString();

                    var documentEstateOwnershipDocument = new DocumentEstateOwnershipDocument()
                    {
                        DocumentEstateId = theDocumentEstate.Id,
                        EstateIsReplacementDocument = theEstateInquiry.DocumentIsReplica,
                        EstateInquiriesId = theEstateInquiry.Id.ToString(),
                        EstateElectronicPageNo = theEstateInquiry.ElectronicEstateNoteNo,
                        EstateDocumentType = theEstateInquiry.DocumentIsNote == "1" ? "2" : "1",
                        EstateDocumentNo = theEstateInquiry.DocPrintNo,
                        EstateSabtNo = theEstateInquiry.RegisterNo,
                        DocumentPersonId = documentPerson.Id,
                        EstateBookNo = theEstateInquiry.EstateNoteNo,
                        EstateBookPageNo = theEstateInquiry.PageNo,
                        EstateSeridaftarId = theEstateInquiry.EstateSeridaftarId,
                        Id = Guid.NewGuid(),
                        Ilm = "1",
                        MortgageText = theEstateInquiry.MortgageText,
                        OwnershipDocumentType = "1",
                        ScriptoriumId = theEstateInquiry.ScriptoriumId
                    };
                    result.DocumentRelatedData.DocumentEstateOwnershipDocumentList.Add(documentEstateOwnershipDocument);

                    if (restricted.HasValue && !restricted.Value)
                    {
                        var documentEstateQuota = result.DocumentRelatedData.DocumentEstateQuotaList.Where(x => x.DocumentEstateId == theDocumentEstate.Id && x.DocumentPersonId == documentPerson.Id).FirstOrDefault();
                        if (documentEstateQuota == null)
                        {
                            documentEstateQuota = new DocumentEstateQuota()
                            {

                                DocumentEstateId = theDocumentEstate.Id,
                                DocumentPersonId = documentPerson.Id,
                                Id = Guid.NewGuid(),
                                Ilm = "1",
                                QuotaText = estateInquiryPerson.ShareText,
                                ScriptoriumId = estateInquiryPerson.ScriptoriumId,

                            };
                            if (estateInquiryPerson.SharePart != null && estateInquiryPerson.ShareTotal != null && string.IsNullOrWhiteSpace(estateInquiryPerson.ShareText))
                            {
                                documentEstateQuota.DetailQuota = estateInquiryPerson.SharePart;
                                documentEstateQuota.TotalQuota = estateInquiryPerson.ShareTotal;
                                theDocumentEstate.IsProportionateQuota = "1";
                            }
                            else
                            {
                                documentEstateQuota.DetailQuota = -1;
                                documentEstateQuota.TotalQuota = -1;
                                documentEstateQuota.QuotaText = estateInquiryPerson.ShareText;                                
                                theDocumentEstate.IsProportionateQuota = "2";
                            }
                            result.DocumentRelatedData.DocumentEstateQuotaList.Add(documentEstateQuota);

                        }
                        else
                        {
                            if (estateInquiryPerson.SharePart != null && estateInquiryPerson.ShareTotal != null && string.IsNullOrWhiteSpace(estateInquiryPerson.ShareText))
                            {
                                if (documentEstateQuota.DetailQuota.HasValue && documentEstateQuota.DetailQuota != -1)
                                {
                                    try
                                    {
                                        theDocumentEstate.IsProportionateQuota = "1";
                                        var sumResult = Helper.Sum(documentEstateQuota.DetailQuota.Value, estateInquiryPerson.SharePart.Value, documentEstateQuota.TotalQuota.Value, estateInquiryPerson.ShareTotal.Value);
                                        documentEstateQuota.DetailQuota = sumResult[0];
                                        documentEstateQuota.TotalQuota = sumResult[1];
                                    }
                                    catch
                                    {
                                        documentEstateQuota.DetailQuota = -1;
                                        documentEstateQuota.TotalQuota = -1;
                                        documentEstateQuota.QuotaText = null;
                                        theDocumentEstate.IsProportionateQuota = "2";
                                    }
                                }

                            }
                            else
                            {
                                documentEstateQuota.DetailQuota = -1;
                                documentEstateQuota.TotalQuota = -1;
                                documentEstateQuota.QuotaText = null;
                                theDocumentEstate.IsProportionateQuota = "2";
                            }
                        }
                    }
                    if (request.DocumentTypeCode == "901")
                    {
                        result.DocumentRelatedData.DocumentEstateQuotaDetailsList.Add(new DocumentEstateQuotaDetails()
                        {
                            DocumentPersonSellerId = documentPerson.Id,
                            DocumentEstateOwnershipDocumentId = documentEstateOwnershipDocument.Id,
                            DocumentEstateId = theDocumentEstate.Id,
                            EstateInquiriesId = theEstateInquiry.Id.ToString(),
                            Id = Guid.NewGuid(),
                            Ilm = "1",
                            OwnershipDetailQuota = estateInquiryPerson.SharePart,
                            OwnershipTotalQuota = estateInquiryPerson.ShareTotal,
                            SellTotalQuota = estateInquiryPerson.ShareTotal,
                            QuotaText = estateInquiryPerson.ShareText,
                            ScriptoriumId = estateInquiryPerson.ScriptoriumId
                        });
                    }
                    result.DocumentRelatedData.DocumentInquiryList.Add(new DocumentInquiry()
                    {
                        DocumentInquiryOrganizationId = "1",
                        EstateInquiriesId = theEstateInquiry.Id.ToString(),
                        Id = Guid.NewGuid(),
                        Ilm = "1",
                        Price = theEstateInquiry.SumPrices,
                        ReplyDate = theEstateInquiry.ResponseDate,
                        ReplyDetailQuota = estateInquiryPerson.SharePart,
                        ReplyNo = theEstateInquiry.ResponseNumber,
                        ReplyQuotaText = estateInquiryPerson.ShareText,
                        ReplyText = theEstateInquiry.Response,
                        ReplyTotalQuota = estateInquiryPerson.ShareTotal,
                        RequestDate = theEstateInquiry.InquiryDate,
                        RequestNo = theEstateInquiry.InquiryNo,
                        ScriptoriumId = theEstateInquiry.ScriptoriumId,
                        State = "3",
                        DocumentEstateId = theDocumentEstate.Id,
                        DocumentEstateOwnershipDocumentId = documentEstateOwnershipDocument.Id,
                        DocumentPersonId = documentPerson.Id
                    });
                    result.DocumentRelatedData.DocumentEstateInquiryList.Add(new DocumentEstateInquiry()
                    {
                        DocumentEstateId = theDocumentEstate.Id,
                        EstateInquiryId = theEstateInquiry.Id,
                        Id = Guid.NewGuid(),
                        Ilm = "1",
                        ScriptoriumId = theEstateInquiry.ScriptoriumId
                    });
                }
            }
            result.DocumentRelatedData.DocumentEstateQuotaList.ForEach(x =>
            {
                if (x.DetailQuota == -1)
                {
                    x.DetailQuota = null;
                    x.TotalQuota = null;
                }
            });
            //result.DocumentRelatedData.DocumentEstateList.ForEach(x =>
            //{
            //    if (x.OwnershipDetailQuota == -1)
            //    {
            //        x.OwnershipDetailQuota = null;
            //        x.OwnershipTotalQuota = null;
            //    }
            //});
            apiResult.Data = result;
            return apiResult;
        }

        public static bool? IsRestricted(string documentTypeCode)
        {
            //string dSUTransferTypeID = string.Empty;
            bool? isRestricted = null;

            switch (documentTypeCode)
            {
                case "111": //سند قطعي غيرمنقول
                case "115": //سند قطعي مشتمل بر رهن - غيرمنقول
                case "979": //سند صداق
                    //dSUTransferTypeID = "26B149EBEEAB4FD8A00A52815E69095B"; // انتقال قطعی
                    isRestricted = false;
                    break;

                case "112": //سند قطعي غيرمنقول با حق استرداد
                    //dSUTransferTypeID = "89830F2E9C6B4E959FEAC12672951A1E"; //بیع شرطی
                    isRestricted = false;
                    break;

                case "211": //سند صلح اموال غيرمنقول
                    //dSUTransferTypeID = "07D8B7F98D624C73872869C45B521FCB"; //صلح
                    isRestricted = false;
                    break;

                case "212": //سند صلح اموال غيرمنقول با حق استرداد
                    //dSUTransferTypeID = "6BD32003B9B44B6F8187DCF20D9E59ED"; //صلح مشروط
                    isRestricted = false;
                    break;

                case "221": //سند صلح سرقفلي و حق كسب و پيشه و تجارت
                    //dSUTransferTypeID = "A48F344E2DF648E09A784C6EFA5D3721"; //واگذاري سرقفلي
                    isRestricted = true;
                    break;

                case "411": //سند جعاله
                case "412": //سند فروش اقساطي
                case "413": //سند مساقات
                case "414": //سند رهني مسكن
                case "415": //سند مشاركت مدني
                case "416": //سند مضاربه
                case "417": //ساير تسهيلات بانكي
                case "431": //سند تعويض يا ضم وثيقه
                    //dSUTransferTypeID = "1"; //رهني
                    isRestricted = true;
                    break;

                case "511": //سند اجاره اموال غيرمنقول
                case "225": //سند صلح حق انتفاع - غيرمنقول
                    //dSUTransferTypeID = "58376D73759844609A201D8165E1AF0A"; //اجاره
                    isRestricted = true;
                    break;

                case "611": //سند تقسيم نامه با صورتمجلس تفكيكي
                case "612": // سند تقسيم نامه بدون صورتمجلس تفكيكي
                    //dSUTransferTypeID = "3E12783909DA4F9681480FE9B8AD50E0"; //سایر
                    isRestricted = false;
                    break;

                case "711": //سند اقاله اموال غيرمنقول
                            // dSUTransferTypeID = "A30DC26C019C4F3482FF972D44665486"; //اقاله
                    isRestricted = false;
                    break;

                case "971": //سند هبه غيرمنقول
                    //dSUTransferTypeID = "3E12783909DA4F9681480FE9B8AD50E0"; //سایر
                    isRestricted = false;
                    break;
                case "941": //سند وصيت تمليكي
                    //dSUTransferTypeID = "3E12783909DA4F9681480FE9B8AD50E0"; //سایر
                    isRestricted = true;
                    break;

                case "981": //اسناد وقف اموال غيرمنقول
                    //dSUTransferTypeID = "6106563D59CD478A8D5EC4A9B7FD32C6"; //وقفنامه
                    isRestricted = false;
                    break;

                default:
                    break;
            }

            return isRestricted;
        }

    }
}
