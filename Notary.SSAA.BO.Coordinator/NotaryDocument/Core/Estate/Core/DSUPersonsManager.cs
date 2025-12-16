using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Text;
using MediatR;
using Notary.SSAA.BO.Domain.Abstractions;
using EstateOwnershipType = Notary.SSAA.BO.SharedKernel.Enumerations.EstateOwnershipType;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using System.Threading;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Estate.EstateInquiry;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class DSUPersonsManager
    {
        #region Local Variables
        public bool? _IsRestricted = false;

        public ProcessActionType _DsuCurrentActionType = ProcessActionType.DSUSimulation;
        #endregion

        #region Ctor

        private readonly IDocumentEstateQuotaDetailRepository _documentEstateQuotaDetailRepository;
        private readonly IDocumentOwnerShipRepository _documentOwnerShipRepository;
        private readonly IDocumentInquiryRepository _documentInquiryRepository;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public DSUPersonsManager(
            IDocumentEstateQuotaDetailRepository documentEstateQuotaDetailRepository
            , IDocumentOwnerShipRepository documentOwnerShipRepository, IDocumentInquiryRepository documentInquiryRepository, IUserService userService, IMediator mediator)
        {


            _documentEstateQuotaDetailRepository = documentEstateQuotaDetailRepository;
            this._documentOwnerShipRepository = documentOwnerShipRepository;
            _documentInquiryRepository = documentInquiryRepository;
            _userService = userService;
            _mediator = mediator;

        }



        #endregion

        #region Internal Operations

        /// <summary>
        /// This method creates all dsuPersonsCollection
        /// </summary>
        /// <param name="equivalantPersonQuotas">For Restricted: Quotas Count = 1, For Non-Restricted: Quotas Count = n</param>
        /// <param name="theMainESTEstateInquiry"></param>
        /// <param name="theNewDSUDealSummary"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        internal async Task<(List<DSURealLegalPersonObject>, DSUDealSummaryObject, string)> CreateAllDSUPersonsCollection(List<DocumentEstateQuotaDetail> equivalantPersonQuotas, EstateInquiry theMainESTEstateInquiry, CancellationToken cancellationToken, DSUDealSummaryObject theNewDSUDealSummary, string messages)
        {
            DSUDealSummaryObject theNewDSUDealSummaryRef = theNewDSUDealSummary;
            string messagesRef = messages;

            Document theCurrentRegisterServiceReq = equivalantPersonQuotas[0].DocumentEstate.Document;

            List<DSURealLegalPersonObject> dsuPersonsCollection = null;

            (dsuPersonsCollection, theNewDSUDealSummaryRef, messagesRef) = await this.CreateAllDSUPersonsCollection(equivalantPersonQuotas, theMainESTEstateInquiry, theCurrentRegisterServiceReq, cancellationToken, theNewDSUDealSummaryRef, messagesRef);

            return (dsuPersonsCollection, theNewDSUDealSummaryRef, messagesRef);
        }
        internal async Task<(List<DSURealLegalPersonObject>, DSUDealSummaryObject, string)> CreateAllDSUPersons4Presell(DocumentPerson theOneBuyerPerson, EstateInquiry theMainESTEstateInquiry, Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, DSUDealSummaryObject theNewDSUDealSummary, string messages)
        {
            DSUDealSummaryObject theNewDSUDealSummaryRef = theNewDSUDealSummary;
            string messagesRef = messages;

            List<DSURealLegalPersonObject> mainDSUPersonsCollection = new List<DSURealLegalPersonObject>();

            if (theNewDSUDealSummaryRef.TheDSURealLegalPersonList.Any())
            {
                foreach (DSURealLegalPersonObject theOneDSUPerson in theNewDSUDealSummaryRef.TheDSURealLegalPersonList)
                {
                    if (this.IsDSUPersonPermittedToAdd(theOneDSUPerson, mainDSUPersonsCollection, theCurrentRegisterServiceReq))
                        mainDSUPersonsCollection.Add(theOneDSUPerson);
                }
            }

            #region DSUSeller
            DSURealLegalPersonObject newDSUPerson = this.CreateDSUPersonBasedOnESTEstateInquiry(theMainESTEstateInquiry, theCurrentRegisterServiceReq, theNewDSUDealSummaryRef.DSUTransferTypeId);
            if (newDSUPerson != null)
                if (this.IsDSUPersonPermittedToAdd(newDSUPerson, mainDSUPersonsCollection, theCurrentRegisterServiceReq))
                    mainDSUPersonsCollection.Add(newDSUPerson);
            #endregion

            #region DSUBuyers
            (newDSUPerson, theNewDSUDealSummaryRef, messagesRef) = await this.CreateDSUPersonBasedOnDocPerson(theOneBuyerPerson, cancellationToken, theNewDSUDealSummaryRef, messagesRef);

            if (newDSUPerson != null)
            {
                if (this.IsDSUPersonPermittedToAdd(newDSUPerson, mainDSUPersonsCollection, theCurrentRegisterServiceReq))
                    mainDSUPersonsCollection.Add(newDSUPerson);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(messagesRef))
                    messagesRef = "خطا در درج اشخاص در بسته های خلاصه معامله.";

                return (null, theNewDSUDealSummaryRef, messagesRef);
            }
            #endregion

            return (mainDSUPersonsCollection, theNewDSUDealSummaryRef, messagesRef);
        }
        #endregion

        #region Private Operations
        private DSURealLegalPersonObject CreateDSUPersonBasedOnESTEstateInquiry(EstateInquiry theMainESTEstateInquiry, Document theCurrentRegisterServiceReq, string DSUTransferTypeId)
        {
            DSURealLegalPersonObject theOneDSURealLegalPerson = null;

            if (theMainESTEstateInquiry.EstateInquiryPeople.Any())
            {
                EstateInquiryPerson theMainBSTPerson = theMainESTEstateInquiry.EstateInquiryPeople.ElementAt(0);
                if (theMainBSTPerson != null)
                {
                        theOneDSURealLegalPerson = new DSURealLegalPersonObject();

                    //This field is set to "100", because the data of this person is being copied from ESTEstateInquiry which involves the seller
                    if (theCurrentRegisterServiceReq.DocumentTypeId == "115")    // قطعی مشتمل بر رهن
                    {
                        if (DSUTransferTypeId == "1")
                        {
                            theOneDSURealLegalPerson.DSURelationKindId = "102";   // ذينفع
                        }
                        else
                        {
                            theOneDSURealLegalPerson.DSURelationKindId = "100";   // معامل
                        }
                    }
                    else
                    {
                        theOneDSURealLegalPerson.DSURelationKindId = "100";   // معامل

                        theOneDSURealLegalPerson.RelatedPersonId = theMainBSTPerson.Id.ToString();
                        theOneDSURealLegalPerson.IsInquiryPerson = 1;
                    }

                    DocumentPerson theOriginalDocPerson = null;
                    if (theMainBSTPerson.PersonType == PersonType.Legal.GetString())
                    {
                        var theOneLegalPerson = theMainBSTPerson;
                        theOriginalDocPerson = this.FindEquivalantOriginalDocPerson(theCurrentRegisterServiceReq, theOneLegalPerson.NationalityCode);

                        theOneDSURealLegalPerson.Name = theOneLegalPerson.Name;
                        theOneDSURealLegalPerson.NationalCode = theOneLegalPerson.NationalityCode;
                        theOneDSURealLegalPerson.BirthDate = theOneLegalPerson.BirthDate;
                        theOneDSURealLegalPerson.Address = theOneLegalPerson.Address;
                        theOneDSURealLegalPerson.NationalityId = theOneLegalPerson.NationalityId?.ToString();
                        theOneDSURealLegalPerson.CityId = theOneLegalPerson.CityId.ToString();

                        theOneDSURealLegalPerson.personType = 0; //0; // Legal 

                    }
                    else
                    {
                        var theOneRealPerson = theMainBSTPerson;

                        theOriginalDocPerson = this.FindEquivalantOriginalDocPerson(theCurrentRegisterServiceReq, theOneRealPerson.NationalityCode);

                        theOneDSURealLegalPerson.Name = theOneRealPerson.Name;
                        theOneDSURealLegalPerson.Family = theOneRealPerson.Family;
                        theOneDSURealLegalPerson.FatherName = theOneRealPerson.FatherName;
                        theOneDSURealLegalPerson.NationalCode = (theOneRealPerson.NationalityCode != null) ? theOneRealPerson.NationalityCode.ToString() : "0";
                        theOneDSURealLegalPerson.NationalityId = theOneRealPerson.NationalityId?.ToString();
                        theOneDSURealLegalPerson.BirthDate = theOneRealPerson.BirthDate;
                        theOneDSURealLegalPerson.CityId = theOneRealPerson.CityId.ToString();
                        theOneDSURealLegalPerson.IssuePlaceId = theOneRealPerson.IssuePlaceId.ToString();
                        theOneDSURealLegalPerson.BirthdateId = theOneRealPerson.BirthPlaceId?.ToString();
                        //change 1397/11/10
                        theOneDSURealLegalPerson.IdentificationNo = theOneRealPerson.IdentityNo;
                        switch (theOneRealPerson.SexType)
                        {
                            case "1":
                                theOneDSURealLegalPerson.sex = 2;
                                break;
                            case "2":
                                theOneDSURealLegalPerson.sex = 1;
                                break;
                            case "0":
                                theOneDSURealLegalPerson.sex = 0;
                                break;
                        }
                        //theOneDSURealLegalPerson.sex = ( theOneRealPerson.SexType != null ) ?  int.Parse ( theOneRealPerson.SexType )  : 0;

                        if (theOriginalDocPerson != null)
                            theOneDSURealLegalPerson.PostalCode = theOriginalDocPerson.PostalCode;
                        else
                            theOneDSURealLegalPerson.PostalCode = theOneRealPerson.PostalCode;

                        theOneDSURealLegalPerson.personType = 1;//1; //Real
                        theOneDSURealLegalPerson.Seri = theOneRealPerson.Seri;
                        if (!string.IsNullOrWhiteSpace(theOneRealPerson.SerialNo))
                        {
                            try
                            {
                                theOneDSURealLegalPerson.Serial = Convert.ToInt64(theOneRealPerson.SerialNo);
                            }
                            catch
                            {

                            }
                        }

                    }



                    theOneDSURealLegalPerson.SharePart = (theMainESTEstateInquiry.EstateInquiryPeople.ElementAt(0)).SharePart;
                    theOneDSURealLegalPerson.ShareTotal = ((theMainESTEstateInquiry.EstateInquiryPeople.ElementAt(0))).ShareTotal;
                    theOneDSURealLegalPerson.ShareContext = (theMainESTEstateInquiry.EstateInquiryPeople.ElementAt(0)).ShareText;

                }
            }

            return theOneDSURealLegalPerson;
        }

        private async Task<(DSURealLegalPersonObject, DSUDealSummaryObject, string)> CreateDSUPersonBasedOnQuota(DocumentEstateQuotaDetail theOnePersonQuota, CancellationToken cancellationToken, DSUDealSummaryObject theNewDSUDealSummary, string messages)
        {
            DSUDealSummaryObject theNewDSUDealSummaryRef = theNewDSUDealSummary;
            string messagesRef = messages;

            if (theOnePersonQuota.DocumentPersonBuyer == null)
                return (null, theNewDSUDealSummaryRef, messagesRef);

            DSURealLegalPersonObject theOneDSURealLegalPerson = new DSURealLegalPersonObject();

            if (theOnePersonQuota.DocumentPersonSeller.Document.DocumentTypeId != "115")    // قطعی مشتمل بر رهن غیرمنقول نباشد
            {
                if (this._IsRestricted == true)
                {
                    theOneDSURealLegalPerson.DSURelationKindId = "102"; //ذینفع
                }
                else
                {
                    theOneDSURealLegalPerson.DSURelationKindId = "101"; //خریدار            
                }
            }
            else    // قطعی مشتمل بر رهن غیرمنقول
            {
                if (this._IsRestricted == true)
                {
                    theOneDSURealLegalPerson.DSURelationKindId = "100"; //معامل 
                }
                else
                {
                    theOneDSURealLegalPerson.DSURelationKindId = "101"; //خریدار   
                }
            }

            theOneDSURealLegalPerson.Name = theOnePersonQuota.DocumentPersonBuyer.Name;
            if (theOnePersonQuota.DocumentPersonBuyer.PersonType == PersonType.NaturalPerson.GetString())
            {
                theOneDSURealLegalPerson.Family = theOnePersonQuota.DocumentPersonBuyer.Family;
                theOneDSURealLegalPerson.FatherName = theOnePersonQuota.DocumentPersonBuyer.FatherName;
                theOneDSURealLegalPerson.Seri = theOnePersonQuota.DocumentPersonBuyer.Seri;
                if (!string.IsNullOrWhiteSpace(theOnePersonQuota.DocumentPersonBuyer.Serial))
                {
                    try
                    {
                        theOneDSURealLegalPerson.Serial = Convert.ToInt64(theOnePersonQuota.DocumentPersonBuyer.Serial);
                    }
                    catch
                    {

                    }
                }
            }

            theOneDSURealLegalPerson.NationalCode = theOnePersonQuota.DocumentPersonBuyer.NationalNo;
            //change 1397/11/10
            theOneDSURealLegalPerson.IdentificationNo = (!string.IsNullOrWhiteSpace(theOnePersonQuota.DocumentPersonBuyer.IdentityNo)) ? theOnePersonQuota.DocumentPersonBuyer.IdentityNo : "0";
            theOneDSURealLegalPerson.PostalCode = theOnePersonQuota.DocumentPersonBuyer.PostalCode;
            theOneDSURealLegalPerson.BirthDate = theOnePersonQuota.DocumentPersonBuyer.BirthDate;
            theOneDSURealLegalPerson.personType = (theOnePersonQuota.DocumentPersonBuyer.PersonType == PersonType.Legal.GetString()) ? 0 : 1;


            //switch (theOnePersonQuota.TheBuyer.IsInformationCorrect)
            //{
            //    case YesNo.No:
            //        theOneDSURealLegalPerson.IsVerified = "false";
            //        break;
            //    case YesNo.Yes:
            //        theOneDSURealLegalPerson.IsVerified = "true";
            //        break;
            //    case YesNo.None:
            //        break;
            //}

            if (theOnePersonQuota.DocumentPersonSeller.Document.IsBasedJudgment == YesNo.Yes.GetString())
                theOneDSURealLegalPerson.ExecutiveTransfer = true;

            if (theOnePersonQuota.DocumentPersonBuyer.IsIranian == YesNo.Yes.GetString())
            {

                var geoInput = new GetGeolocationByIdQuery(Array.Empty<int>()) { FetchGeolocationOfIran = true };
                var geoOutput = await _mediator.Send(geoInput, cancellationToken);
                theOneDSURealLegalPerson.NationalityId = geoOutput.Data.GeolocationList[0].Id; //ایران
            }
            else if (theOnePersonQuota.DocumentPersonBuyer.IsIranian == YesNo.No.GetString())
                theOneDSURealLegalPerson.NationalityId = theOnePersonQuota.DocumentPersonBuyer.NationalityId?.ToString();

            switch (theOnePersonQuota.DocumentPersonBuyer.SexType)
            {
                case "1":
                    theOneDSURealLegalPerson.sex = 2;
                    break;
                case "2":
                    theOneDSURealLegalPerson.sex = 1;
                    break;
                case "0":
                    theOneDSURealLegalPerson.sex = 0;
                    break;
            }

            string conditionText = theOnePersonQuota.DealSummaryPersonConditions;
            if (!string.IsNullOrWhiteSpace(conditionText) && !string.IsNullOrWhiteSpace(theOnePersonQuota.DocumentPersonBuyer.Document.DocumentInfoText.ConditionsText))
            {
                conditionText +=
                    System.Environment.NewLine +
                    theOnePersonQuota.DocumentPersonBuyer.Document.DocumentInfoText.ConditionsText;
            }

            if (!string.IsNullOrWhiteSpace(conditionText) && conditionText.Length >= 400)
            {
                messagesRef =
                    "بروز خطا در ساخت بسته های خلاصه معامله!" +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "متن شرط شخص " + theOnePersonQuota.DocumentPersonBuyer.FullName() + " را کنترل نمایید." +
                    System.Environment.NewLine +
                    "طول متن شرط نباید بیش از 400 کاراکتر باشد.";

                return (null, theNewDSUDealSummaryRef, messagesRef);
            }

            theOneDSURealLegalPerson.Description = conditionText;

            int? issuePlaceID = theOnePersonQuota.DocumentPersonBuyer.IdentityIssueGeoLocationId;
            string issuePlaceName = string.Empty;
            GetGeolocationByNationalityCodeViewModel geoLocation = null;
            if ((issuePlaceID == null) && theOnePersonQuota.DocumentPersonBuyer.PersonType == PersonType.NaturalPerson.GetString() &&
                !string.IsNullOrWhiteSpace(theOnePersonQuota.DocumentPersonBuyer.NationalNo))
            {
                var response = await _mediator.Send(new GetGeolocationByNationalityCodeQuery(theOnePersonQuota.DocumentPersonBuyer.NationalNo), cancellationToken);
                if (response.IsSuccess)
                {
                    geoLocation = response.Data;
                }
                else
                {
                    geoLocation = null;
                }

                issuePlaceID = (geoLocation != null) ? int.Parse(geoLocation.Geolocation.Id) : null;
            }

            theOneDSURealLegalPerson.CityId = issuePlaceID?.ToString();
            theOneDSURealLegalPerson.IssuePlaceId = issuePlaceID?.ToString();
            //======================================================================
            //theNewDSUDealSummary.Address = theOnePersonQuota.TheONotaryRegCase.Address;
            theNewDSUDealSummaryRef.Amount = (long?)theOnePersonQuota.DocumentEstate.Price;
            //theNewDSUDealSummary.AmountUnitId = "10"; //ریال
            if (_IsRestricted == true)
            {
                theNewDSUDealSummaryRef.Duration = (long?)theOnePersonQuota.DocumentEstate.Document.DocumentInfoOther.MortageDuration;
                theNewDSUDealSummaryRef.TimeUnitId = theOnePersonQuota.DocumentEstate.Document.DocumentInfoOther.MortageTimeUnitId;
            }

            //=========================مازاد=======================================
            if (theOnePersonQuota.DocumentEstate.IsRemortage == YesNo.Yes.GetString())
                theNewDSUDealSummaryRef.Surplus = "True";

            //=========================متن منضم====================================
            if (theOnePersonQuota.DocumentEstate.IsAttachment == YesNo.Yes.GetString())
                theNewDSUDealSummaryRef.Attached = theOnePersonQuota.DocumentEstate.AttachmentDescription;//AttachmentText

            //========================جزء سهم و کل سهم مورد معامله==============
            theOneDSURealLegalPerson.SharePart = theOnePersonQuota.SellDetailQuota.TrimDoubleValue();
            theOneDSURealLegalPerson.ShareTotal = theOnePersonQuota.SellTotalQuota;
            theOneDSURealLegalPerson.ShareContext = theOnePersonQuota.QuotaText;
            //=====================================================================

            //عرصه و اعیان
            //=====================================================================
            switch (theOnePersonQuota.DocumentEstate.FieldOrGrandee)
            {
                case /*EstateOwnershipType.Feild_Grandee*/"3":
                    theNewDSUDealSummaryRef.DSUTransitionCaseId = "102";
                    break;
                case /*EstateOwnershipType.Field*/"1":
                    theNewDSUDealSummaryRef.DSUTransitionCaseId = "100";
                    break;
                case /*EstateOwnershipType.Grandee*/"2":
                    theNewDSUDealSummaryRef.DSUTransitionCaseId = "101";
                    break;
                case /*EstateOwnershipType.None*/"0":
                    break;
                default:
                    break;
            }
            //===========================توضیحات مندرج در خلاصه معامله=============================
            theNewDSUDealSummaryRef.Description =
                (theOnePersonQuota.DocumentEstate.Document.IsBasedJudgment == YesNo.Yes.GetString()) ?
                "سند از نوع انتقال اجرایی می باشد." + System.Environment.NewLine + theOnePersonQuota.DocumentEstateOwnershipDocument.DealSummaryText :
                theOnePersonQuota.DocumentEstateOwnershipDocument.DealSummaryText;
            // 99/11/6  -  بنا به درخواست سامانه املاک، حداکثر طول متن، 400 کاراکتر می تواند باشد
            if (!string.IsNullOrWhiteSpace(theNewDSUDealSummaryRef.Description) && theNewDSUDealSummaryRef.Description.Length > 400)
                theNewDSUDealSummaryRef.Description = theNewDSUDealSummaryRef.Description.Substring(0, 400);

            //===========================مفروز و مشاع===============================================
            switch (theOnePersonQuota.DocumentEstate.OwnershipType)
            {
                case /*NotaryOwnershipType.Mafroz*/"1":
                    theNewDSUDealSummaryRef.DSUOwnerShipTypeId = "1";
                    break;
                case /*NotaryOwnershipType.Moshae*/"2":
                    theNewDSUDealSummaryRef.DSUOwnerShipTypeId = "2";
                    break;
                default:
                    break;
            }

            //================================================ثمنیه و ربعیه============================================================

            string octantQuarterBuffer =
               ((NotarySomnyehRobeyehActionType)theOnePersonQuota.DocumentEstate.SomnyehRobeyehActionType.ToRequiredInt()).GetDescription() + " " +
                ((NotaryGrandeeExceptionType)theOnePersonQuota.DocumentEstate.GrandeeExceptionType.ToRequiredInt()).GetDescription() + " " +
                ((EstateOwnershipType)theOnePersonQuota.DocumentEstate.SomnyehRobeyehFieldGrandee.ToRequiredInt()).GetDescription();

            if (!string.IsNullOrWhiteSpace(octantQuarterBuffer))
                theOneDSURealLegalPerson.OctantQuarter = octantQuarterBuffer;

            if (theOnePersonQuota.DocumentEstate.GrandeeExceptionDetailQuota != null && !string.IsNullOrWhiteSpace(theOnePersonQuota.DocumentEstate.GrandeeExceptionDetailQuota.ToString()))
                theOneDSURealLegalPerson.OctantQuarterPart = theOnePersonQuota.DocumentEstate.GrandeeExceptionDetailQuota.ToString();

            if (theOnePersonQuota.DocumentEstate.GrandeeExceptionTotalQuota != null && !string.IsNullOrWhiteSpace(theOnePersonQuota.DocumentEstate.GrandeeExceptionTotalQuota.ToString()))
                theOneDSURealLegalPerson.OctantQuarterTotal = theOnePersonQuota.DocumentEstate.GrandeeExceptionTotalQuota.ToString();
            //===========================================================================================================================

            return (theOneDSURealLegalPerson, theNewDSUDealSummaryRef, messagesRef);
        }

        private async Task<(DSURealLegalPersonObject, DSUDealSummaryObject, string)> CreateDSUPersonBasedOnDocPerson(DocumentPerson theOneDocPerson, CancellationToken cancellationToken, DSUDealSummaryObject theNewDSUDealSummary, string messages)
        {
            DSUDealSummaryObject theNewDSUDealSummaryRef = theNewDSUDealSummary;
            string messagesRef = messages;
            DSURealLegalPersonObject theOneDSURealLegalPerson = new DSURealLegalPersonObject();

            if (this._IsRestricted == true)
            {
                if (theOneDocPerson.Document.DocumentTypeId != "115")    // قطعی مشتمل بر رهن غیرمنقول نباشد
                    theOneDSURealLegalPerson.DSURelationKindId = "102"; //ذینفع
                else
                    theOneDSURealLegalPerson.DSURelationKindId = "102"; //معامل
            }
            else
            {
                theOneDSURealLegalPerson.DSURelationKindId = "101"; //خریدار            
            }

            theOneDSURealLegalPerson.Name = theOneDocPerson.Name;
            if (theOneDocPerson.PersonType == PersonType.NaturalPerson.GetString())
            {
                theOneDSURealLegalPerson.Family = theOneDocPerson.Family;
                theOneDSURealLegalPerson.FatherName = theOneDocPerson.FatherName;
                theOneDSURealLegalPerson.Seri = theOneDocPerson.Seri;
                if (!string.IsNullOrWhiteSpace(theOneDocPerson.Serial))
                {
                    try
                    {
                        theOneDSURealLegalPerson.Serial = Convert.ToInt64(theOneDocPerson.Serial);
                    }
                    catch
                    {

                    }
                }

            }

            theOneDSURealLegalPerson.NationalCode = theOneDocPerson.NationalNo;
            //change 1397/11/10
            theOneDSURealLegalPerson.IdentificationNo = (!string.IsNullOrWhiteSpace(theOneDocPerson.IdentityNo)) ? theOneDocPerson.IdentityNo : "0";
            theOneDSURealLegalPerson.PostalCode = theOneDocPerson.PostalCode;
            theOneDSURealLegalPerson.BirthDate = theOneDocPerson.BirthDate;
            theOneDSURealLegalPerson.personType = (theOneDocPerson.PersonType == PersonType.Legal.GetString()) ? 0 : 1;

            if (theOneDocPerson.Document.IsBasedJudgment == YesNo.Yes.GetString())
                theOneDSURealLegalPerson.ExecutiveTransfer = true;

            if (theOneDocPerson.IsIranian == YesNo.Yes.GetString())
            {
                var geoInput = new GetGeolocationByIdQuery(Array.Empty<int>()) { FetchGeolocationOfIran = true };
                var geoOutput = await _mediator.Send(geoInput, cancellationToken);
                theOneDSURealLegalPerson.NationalityId = geoOutput.Data.GeolocationList[0].Id; //ایران
            }
            else if (theOneDocPerson.IsIranian == YesNo.No.GetString())
                theOneDSURealLegalPerson.NationalityId = theOneDocPerson.NationalityId?.ToString();

            switch (theOneDocPerson.SexType)
            {
                case "1"://SexType.Female:
                    theOneDSURealLegalPerson.sex = 2;
                    break;
                case "2"://SexType.Male:
                    theOneDSURealLegalPerson.sex = 1;
                    break;
                case "0"://SexType.None:
                    theOneDSURealLegalPerson.sex = 0;
                    break;
            }

            int? issuePlaceID = theOneDocPerson.IdentityIssueGeoLocationId;
            string issuePlaceName = string.Empty;
            GetGeolocationByNationalityCodeViewModel geoLocation = null;
            if ((issuePlaceID == null) && theOneDocPerson.PersonType == PersonType.NaturalPerson.GetString() && !string.IsNullOrWhiteSpace(theOneDocPerson.NationalNo))
            {
                var response = await _mediator.Send(new GetGeolocationByNationalityCodeQuery(theOneDocPerson.NationalNo), cancellationToken);
                if (response.IsSuccess)
                {
                    geoLocation = response.Data;
                }
                else
                {
                    geoLocation = null;
                }

                issuePlaceID = (geoLocation != null) ? int.Parse(geoLocation.Geolocation.Id) : null;
            }

            theOneDSURealLegalPerson.CityId = issuePlaceID.ToString();
            theOneDSURealLegalPerson.IssuePlaceId = issuePlaceID.ToString();

            //عرصه و اعیان
            //=====================================================================
            //switch (theOnePersonQuota.TheONotaryRegCase.FieldOrGrandee)
            //{
            //    case EstateOwnershipType.Feild_Grandee:
            //        theNewDSUDealSummary.DSUTransitionCaseId = "102";
            //        break;
            //    case EstateOwnershipType.Field:
            //        theNewDSUDealSummary.DSUTransitionCaseId = "100";
            //        break;
            //    case EstateOwnershipType.Grandee:
            //        theNewDSUDealSummary.DSUTransitionCaseId = "101";
            //        break;
            //    case EstateOwnershipType.None:
            //        break;
            //    default:
            //        break;
            //}
            //===========================توضیحات مندرج در خلاصه معامله=============================
            //theNewDSUDealSummary.Description =
            //    (theOnePersonQuota.TheONotaryRegCase.TheONotaryRegisterServiceReq.IsDocBasedJudgeHokm == YesNo.Yes) ?
            //    "سند از نوع انتقال اجرایی می باشد." + System.Environment.NewLine + theOnePersonQuota.TheONotaryPersonOwnershipDoc.DsuDescription :
            //    theOnePersonQuota.TheONotaryPersonOwnershipDoc.DsuDescription;

            ////===========================مفروز و مشاع===============================================
            //switch (theOnePersonQuota.TheONotaryRegCase.OwnershipType)
            //{
            //    case NotaryOwnershipType.Mafroz:
            //        theNewDSUDealSummary.DSUOwnerShipTypeId = "100";
            //        break;
            //    case NotaryOwnershipType.Moshae:
            //        theNewDSUDealSummary.DSUOwnerShipTypeId = "101";
            //        break;
            //    default:
            //        break;
            //}

            ////================================================ثمنیه و ربعیه============================================================
            //string octantQuarterBuffer =
            //    theOnePersonQuota.TheONotaryRegCase.SomnyehRobeyehActionType.GetDescription() + " " +
            //    theOnePersonQuota.TheONotaryRegCase.GrandeeExceptionType.GetDescription() + " " +
            //    theOnePersonQuota.TheONotaryRegCase.SomnyehRobeyehFieldGrandee.GetDescription();

            //if (!string.IsNullOrWhiteSpace(octantQuarterBuffer))
            //    theOneDSURealLegalPerson.OctantQuarter = octantQuarterBuffer;

            //if (theOnePersonQuota.TheONotaryRegCase.GrandeeExceptionDetailQuota != null && !string.IsNullOrWhiteSpace(theOnePersonQuota.TheONotaryRegCase.GrandeeExceptionDetailQuota.ToString()))
            //    theOneDSURealLegalPerson.OctantQuarterPart = theOnePersonQuota.TheONotaryRegCase.GrandeeExceptionDetailQuota.ToString();

            //if (theOnePersonQuota.TheONotaryRegCase.GrandeeExceptionTotalQuota != null && !string.IsNullOrWhiteSpace(theOnePersonQuota.TheONotaryRegCase.GrandeeExceptionTotalQuota.ToString()))
            //    theOneDSURealLegalPerson.OctantQuarterTotal = theOnePersonQuota.TheONotaryRegCase.GrandeeExceptionTotalQuota.ToString();
            ////===========================================================================================================================

            return (theOneDSURealLegalPerson, theNewDSUDealSummaryRef, messagesRef);
        }

        private DocumentPerson FindEquivalantOriginalDocPerson(Document theCurrentRegisterServiceRequest, string inputNationalNo)
        {
            if (!theCurrentRegisterServiceRequest.DocumentPeople.Any())
                return null;

            foreach (DocumentPerson theOneDocPerson in theCurrentRegisterServiceRequest.DocumentPeople)
            {
                if (theOneDocPerson.NationalNo == inputNationalNo)
                    return theOneDocPerson;
            }

            return null;
        }

        private async Task<ICollection<DocumentEstateQuotaDetail>> CollectValidPersonQuotas(string theMainNotaryServiceReqObjectID, EstateInquiry theMainESTEstateInquiry, CancellationToken cancellationToken, DocumentEstateOwnershipDocument theCurrentOwnershipDoc4Restricted = null)
        {
            ICollection<DocumentEstateQuotaDetail> equivalantPersonQuotasCollection = null;

            if (theCurrentOwnershipDoc4Restricted != null)
            {
                equivalantPersonQuotasCollection = theCurrentOwnershipDoc4Restricted.DocumentEstateQuotaDetails;
                return equivalantPersonQuotasCollection;
            }

            equivalantPersonQuotasCollection = await
                _documentEstateQuotaDetailRepository.CollectValidPersonQuotas(Guid.Parse(theMainNotaryServiceReqObjectID),
                    theMainESTEstateInquiry.Id.ToString(), cancellationToken);


            return equivalantPersonQuotasCollection;
        }

        private async Task<(List<DSURealLegalPersonObject>, DSUDealSummaryObject, string)> CreateDSUPersonsCollectionBasedOnQuotas(List<DocumentEstateQuotaDetail> thePersonQuotaCollection, CancellationToken cancellationToken, DSUDealSummaryObject theNewDSUDealSummary, string messages)
        {
            DSUDealSummaryObject theNewDSUDealSummaryRef = theNewDSUDealSummary;
            string messagesRef = messages;
            List<DSURealLegalPersonObject> dsuPersonsCollection = null;

            foreach (DocumentEstateQuotaDetail theOnePersonQuota in thePersonQuotaCollection)
            {
                if (dsuPersonsCollection == null)
                    dsuPersonsCollection = new List<DSURealLegalPersonObject>();
                DSURealLegalPersonObject newDSUPerson;


                (newDSUPerson, theNewDSUDealSummaryRef, messagesRef) = await this.CreateDSUPersonBasedOnQuota(theOnePersonQuota, cancellationToken, theNewDSUDealSummaryRef, messagesRef);

                if (newDSUPerson == null && !string.IsNullOrWhiteSpace(messagesRef))
                    return (null, theNewDSUDealSummaryRef, messagesRef);

                if (newDSUPerson != null)
                    dsuPersonsCollection.Add(newDSUPerson);
            }

            return (dsuPersonsCollection, theNewDSUDealSummaryRef, messagesRef);
        }

        private async Task<(List<DSURealLegalPersonObject>, DSUDealSummaryObject, string)> CreateAllDSUPersonsCollection(List<DocumentEstateQuotaDetail> thePersonQuotaCollection, EstateInquiry theMainESTEstateInquiry, Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, DSUDealSummaryObject theNewDSUDealSummary, string messages)
        {
            DSUDealSummaryObject theNewDSUDealSummaryRef = theNewDSUDealSummary;
            string messagesRef = messages;

            List<DSURealLegalPersonObject> mainDSUPersonsCollection = new List<DSURealLegalPersonObject>();

            if (theNewDSUDealSummaryRef.TheDSURealLegalPersonList.Any())
            {
                foreach (DSURealLegalPersonObject theOneDSUPerson in theNewDSUDealSummaryRef.TheDSURealLegalPersonList)
                {
                    if (this.IsDSUPersonPermittedToAdd(theOneDSUPerson, mainDSUPersonsCollection, theCurrentRegisterServiceReq))
                        mainDSUPersonsCollection.Add(theOneDSUPerson);
                }
            }

            //Creating Owner Person Based On Equivalant "ESTEstateInquiry" For Deterministic Doc-Types
            if (_IsRestricted == false
                //NotaryOfficeCommons.EstateInquiryManager.Mapper.SemiRestrictedDocTypes.Contains(theCurrentRegisterServiceReq.TheONotaryDocumentType.Code) //اسناد از نوع وصیت نامه تملیکی خلاصه از نوع محدودیت است ولی سهم مالک باید از استعلام اخذ گردد.
                )
            {
                DSURealLegalPersonObject newDSUPerson = this.CreateDSUPersonBasedOnESTEstateInquiry(theMainESTEstateInquiry, theCurrentRegisterServiceReq, theNewDSUDealSummaryRef.DSUTransferTypeId);
                if (newDSUPerson != null)
                    if (this.IsDSUPersonPermittedToAdd(newDSUPerson, mainDSUPersonsCollection, theCurrentRegisterServiceReq))
                        mainDSUPersonsCollection.Add(newDSUPerson);
            }

            #region Restricted-Document
            //***If IsRestricted == true, add persons being fetched from RecentDSUs***
            //While DSU Is Being simulated, owner is not valid yet. So the Deterministic DSU should be sent in order to verify the owner
            //Sequence-Check will handle this task Before any Restricted DSU Generation 
            if (_IsRestricted == true && _DsuCurrentActionType == ProcessActionType.DSUSending)
            {
                DocumentEstateQuotaDetail theOnePersonQuota = (DocumentEstateQuotaDetail)thePersonQuotaCollection[0];
                DSURealLegalPersonObject theRecentOwnerDSUPersonToAdd;
                (theRecentOwnerDSUPersonToAdd, messagesRef) = await this.GetOwnerFromRecentDSUsByProxy(theOnePersonQuota, theMainESTEstateInquiry, cancellationToken, messagesRef);
                if (theRecentOwnerDSUPersonToAdd != null)
                    mainDSUPersonsCollection.Add(theRecentOwnerDSUPersonToAdd);
                else
                {
                    
                        if (mainDSUPersonsCollection.Count == 2)
                            return (mainDSUPersonsCollection, theNewDSUDealSummaryRef, messagesRef);

                    List<Document> deterministicRegServices = await this.GetRecentDeterministicRegisterServiceReqs(theMainESTEstateInquiry, theOnePersonQuota.DocumentPersonSeller.Document, cancellationToken);
                    if (deterministicRegServices == null || !deterministicRegServices.Any())
                    {
                        messagesRef += System.Environment.NewLine +
                                   theOnePersonQuota.DocumentPersonSeller.FullName() +
                                   " با مالک مستند تعریف شده در استعلام شماره " +
                                   theMainESTEstateInquiry.InquiryNo +
                                   " تطابق ندارد.";

                        return (null, theNewDSUDealSummaryRef, messagesRef);
                    }

                    string personsNotIncluded = string.Empty;
                    bool isPersonIncludedAsBuyer = this.IsPersonIncludedAsBuyer(theOnePersonQuota, deterministicRegServices);
                    if (!isPersonIncludedAsBuyer)
                    {
                        messagesRef += System.Environment.NewLine +
                                    theOnePersonQuota.DocumentPersonSeller.FullName() +
                                    " با خریدار سند قطعی تطابق ندارد. ";

                        return (null, theNewDSUDealSummaryRef, messagesRef);
                    }
                    else
                    {
                        messagesRef += System.Environment.NewLine +
                                    " خلاصه معامله سند قطعی مربوط به " +
                                    theOnePersonQuota.DocumentPersonSeller.FullName() +
                                    " ارسال نشده است.";

                        return (null, theNewDSUDealSummaryRef, messagesRef);
                    }
                }
            }
            #endregion

            if (thePersonQuotaCollection.Any())
            {
                List<DSURealLegalPersonObject> newDSUPersonCollection;
                (newDSUPersonCollection, theNewDSUDealSummaryRef, messagesRef) = await this.CreateDSUPersonsCollectionBasedOnQuotas(thePersonQuotaCollection, cancellationToken, theNewDSUDealSummaryRef, messagesRef);
                if (newDSUPersonCollection.Any())
                {
                    //if (theCurrentRegisterServiceReq.TheONotaryDocumentType.Code == "115")    //قطعی مشتمل بر رهن - غیرمنقول
                    //{
                    //    DSURealLegalPersonObject newDSUPerson = this.CreateDSUPersonBasedOnESTEstateInquiry(theMainESTEstateInquiry, theCurrentRegisterServiceReq);
                    //    if (newDSUPerson != null)
                    //        if (this.IsDSUPersonPermittedToAdd(newDSUPerson, mainDSUPersonsCollection, theCurrentRegisterServiceReq))
                    //            mainDSUPersonsCollection.Add(newDSUPerson);
                    //}

                    foreach (DSURealLegalPersonObject theOneDSUPerson in newDSUPersonCollection)
                    {
                        if (this.IsDSUPersonPermittedToAdd(theOneDSUPerson, mainDSUPersonsCollection, theCurrentRegisterServiceReq))
                        {
                            mainDSUPersonsCollection.Add(theOneDSUPerson);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(messagesRef))
                        messagesRef = "خطا در درج اشخاص در بسته های خلاصه معامله.";

                    return (null, theNewDSUDealSummaryRef, messagesRef);
                }
            }

            this.EnsureAndAlterOwnerPersonDataBasedOnQuotas(thePersonQuotaCollection, ref mainDSUPersonsCollection);

            return (mainDSUPersonsCollection, theNewDSUDealSummaryRef, messagesRef);
        }

        private void EnsureAndAlterOwnerPersonDataBasedOnQuotas(List<DocumentEstateQuotaDetail> thePersonQuotaCollection, ref List<DSURealLegalPersonObject> mainDSUPersonsCollection)
        {
            foreach (DSURealLegalPersonObject theOneRealLegalPerson in mainDSUPersonsCollection)
            {
                if (theOneRealLegalPerson.DSURelationKindId != "100")
                    continue;

                if (theOneRealLegalPerson.SharePart != null || theOneRealLegalPerson.ShareTotal != null || !string.IsNullOrEmpty(theOneRealLegalPerson.ShareContext))
                    continue;

                foreach (DocumentEstateQuotaDetail theOneQuota in thePersonQuotaCollection)
                {
                    string dsuPersonFullName = (theOneRealLegalPerson.Name + theOneRealLegalPerson.Family).GetStandardFarsiString();
                    string quotaPersonName = theOneQuota.DocumentPersonSeller.FullName().GetStandardFarsiString();

                    if (theOneRealLegalPerson.NationalCode != theOneQuota.DocumentPersonSeller.NationalNo)
                        if (dsuPersonFullName != quotaPersonName)
                            continue;

                    theOneRealLegalPerson.SharePart = theOneQuota.SellDetailQuota;
                    theOneRealLegalPerson.ShareTotal = theOneQuota.SellTotalQuota;

                    if (theOneRealLegalPerson.SharePart == null && theOneRealLegalPerson.ShareTotal == null)
                        theOneRealLegalPerson.ShareContext = theOneQuota.QuotaText;

                    break;
                }
            }
        }

        private async Task<(DSURealLegalPersonObject, string)> GetOwnerFromRecentDSUsByProxy(DocumentEstateQuotaDetail theOnePersonQuota, EstateInquiry theMainESTEstateInquiry, CancellationToken cancellationToken, string personMessages)
        {
            string personMessagesRef = personMessages;

            List<DSURealLegalPersonObject> ownersProxyCollection;

            var response = await _mediator.Send(new GetEstateOwnersByInquiryIdInput(theMainESTEstateInquiry.Id.ToString()), cancellationToken);
            if (response.IsSuccess)
            {
                personMessagesRef = "مالک مستند از سرور املاک دریافت نشد.\n";
                return (null, personMessagesRef);
            }
            else
            {
                ownersProxyCollection = response.Data;
            }


            //OwnersListMessage ownersProxyCollection = EstateInquiryManager.ServicesGateway.EstateServiceInvokerGateway.GetOwnersList(theMainESTEstateInquiry.Id, theOnePersonQuota.DocumentEstate.Document.ScriptoriumId);

            if (!ownersProxyCollection.Any())
            {
                personMessagesRef = "مالک مستند از سرور املاک دریافت نشد.\n";
                return (null, personMessagesRef);
            }

            if (theOnePersonQuota == null)
            {
                personMessagesRef = "برای ایجاد اشخاص خلاصه معامله رهنی سهم های مربوط به سند یافت نشد.\n";
                return (null, personMessagesRef);
            }

            DSURealLegalPersonObject theTargetDSURealLegalPerson = null;
            foreach (DSURealLegalPersonObject theOneDSUPerson in ownersProxyCollection)
            {
                if (theOnePersonQuota.DocumentPersonSeller.DocumentPersonType == null)
                {
                    bool isMovares = this.IsMovaresSpecified(theOnePersonQuota.DocumentPersonSeller);
                    if (!isMovares)
                        continue;
                }
                else if (theOnePersonQuota.DocumentPersonSeller.DocumentPersonType.IsOwner != YesNo.Yes.GetString())
                    continue;

                if (theOnePersonQuota.DocumentPersonSeller.NationalNo == theOneDSUPerson.NationalCode)
                {
                    theTargetDSURealLegalPerson = theOneDSUPerson;
                    break;
                }

                if (theOnePersonQuota.DocumentPersonSeller.FullName().GetStandardFarsiString() == (theOneDSUPerson.Name + theOneDSUPerson.Family).GetStandardFarsiString())
                {
                    theTargetDSURealLegalPerson = theOneDSUPerson;
                    break;
                }

                if (theOneDSUPerson.personType == 0) //For LegalPersons if the FullNames Contain each other, will meet our requirements
                {
                    string docPersonFullName = theOnePersonQuota.DocumentPersonSeller.FullName();
                    string dealSummaryPersonFullName = theOneDSUPerson.Name + theOneDSUPerson.Family;

                    if (
                        docPersonFullName.GetStandardFarsiString().Contains((dealSummaryPersonFullName).GetStandardFarsiString()) ||
                        dealSummaryPersonFullName.GetStandardFarsiString().Contains((docPersonFullName).GetStandardFarsiString())
                        )
                    {
                        theTargetDSURealLegalPerson = theOneDSUPerson;
                        break;
                    }
                }
            }

            if (theTargetDSURealLegalPerson != null)
            {
                theTargetDSURealLegalPerson.SharePart = theOnePersonQuota.SellDetailQuota;
                theTargetDSURealLegalPerson.ShareTotal = theOnePersonQuota.SellTotalQuota;
                theTargetDSURealLegalPerson.ShareContext = theOnePersonQuota.QuotaText;

                return (theTargetDSURealLegalPerson, personMessagesRef);
            }


            personMessagesRef = "طبق اطلاعات دریافتی از سامانه املاک، " + theOnePersonQuota.DocumentPersonSeller.FullName() + " مالک " + theOnePersonQuota.DocumentEstate.RegCaseText() + " نمی باشد. ";

            return (null, personMessagesRef);
        }

        private async Task<List<DocumentPerson>> GetOwnersFromRecentDeterministicRegServiceReq(Document theCurrentRegisterServiceReq, EstateInquiry theMainESTEstateInquiry, CancellationToken cancellationToken)
        {


            List<DocumentEstateOwnershipDocument> ownershipDocsCollection = await _documentOwnerShipRepository.GetOwnersFromRecentDeterministicRegServiceReq(
                _userService.UserApplicationContext.ScriptoriumInformation.Id, theMainESTEstateInquiry.Id.ToString(),
                cancellationToken);



            if (!ownershipDocsCollection.Any())
                return null;
            List<DocumentPerson> deterministicDocPersonCollection = new List<DocumentPerson>();
            foreach (DocumentEstateOwnershipDocument theOneOwnershipDoc in ownershipDocsCollection)
            {
                foreach (DocumentPerson theOneDocPerson in theOneOwnershipDoc.DocumentEstate.Document.DocumentPeople)
                {
                    if (theOneDocPerson.DocumentPersonType == null)
                        continue;


                    if (theOneDocPerson.DocumentPersonTypeId == "2")
                    {
                        string sFullName = theOneDocPerson.FullName();
                        if (this.FindEquivalantPerson(deterministicDocPersonCollection, theOneDocPerson.NationalNo) == null)
                            deterministicDocPersonCollection.Add(theOneDocPerson);
                    }
                }
            }

            //=================Seek 4 Equivalant Owner In Current Request==============================
            List<DocumentPerson> desiredDocPersons = null;
            foreach (DocumentPerson theOneDeterministicDocPerson in deterministicDocPersonCollection)
            {
                foreach (DocumentPerson theOneDocPerson in theCurrentRegisterServiceReq.DocumentPeople)
                {
                    if (theOneDocPerson.DocumentPersonType == null)
                        continue;

                    if (theOneDocPerson.DocumentPersonType.IsOwner != YesNo.Yes.GetString())
                        continue;

                    if (theOneDocPerson.NationalNo == theOneDeterministicDocPerson.NationalNo)
                    {
                        if (desiredDocPersons == null)
                            desiredDocPersons = new List<DocumentPerson>();

                        if (this.FindEquivalantPerson(desiredDocPersons, theOneDocPerson.NationalNo) == null)
                            desiredDocPersons.Add(theOneDocPerson);
                    }
                }
            }

            return desiredDocPersons;
        }

        private async Task<List<Document>> GetRecentDeterministicRegisterServiceReqs(EstateInquiry theMainESTEstateInquiry, Document theCurrentRegisterServiceReq, CancellationToken cancellationToken)
        {


            var deterministicInquiries = await _documentInquiryRepository.GetRecentDeterministicRegisterServiceReqs(
                _userService.UserApplicationContext.ScriptoriumInformation.Id, theMainESTEstateInquiry.Id.ToString(),
                cancellationToken);
            if (!deterministicInquiries.Any())
                return null;

            List<Document> deterministicRegisterServiceReqsCollection = null;

            foreach (DocumentInquiry theOneDeterministicInquiry in deterministicInquiries)
            {
                if (deterministicRegisterServiceReqsCollection == null)
                    deterministicRegisterServiceReqsCollection = new List<Document>();

                if (!deterministicRegisterServiceReqsCollection.Contains(theOneDeterministicInquiry.Document))
                    deterministicRegisterServiceReqsCollection.Add(theOneDeterministicInquiry.Document);
            }

            return deterministicRegisterServiceReqsCollection;
        }

        private bool IsPersonIncludedAsBuyer(DocumentEstateQuotaDetail theOnePersonQuota, List<Document> deterministicRegServicesCollection)
        {
            foreach (Document theOneRegisterServiceReq in deterministicRegServicesCollection)
            {
                foreach (DocumentPerson theOneDeterministicPerson in theOneRegisterServiceReq.DocumentPeople)
                {
                    if (theOneDeterministicPerson.DocumentPersonType == null)
                        continue;

                    if (theOneDeterministicPerson.DocumentPersonType.IsOwner != YesNo.No.GetString())
                        continue;

                    if (theOneDeterministicPerson.NationalNo == theOnePersonQuota.DocumentPersonSeller.NationalNo)
                        return true;

                    if (theOneDeterministicPerson.FullName() == theOnePersonQuota.DocumentPersonSeller.FullName())
                        return true;
                }
            }

            return false;
        }

        private DocumentPerson FindEquivalantPerson(List<DocumentPerson> personsCollection, string nationalNo)
        {
            if (!personsCollection.Any() || string.IsNullOrWhiteSpace(nationalNo))
                return null;

            foreach (DocumentPerson theOnePerson in personsCollection)
            {
                if (theOnePerson.NationalNo == nationalNo)
                    return theOnePerson;
            }

            return null;
        }

        private bool IsDSUPersonPermittedToAdd(DSURealLegalPersonObject theNewDSUPersonToAdd, List<DSURealLegalPersonObject> theDSUPersonCollection, Document theCurrentRegisterServiceReq)
        {
            if (theNewDSUPersonToAdd == null)
                return false;

            if (!theDSUPersonCollection.Any())
                return true;

            foreach (DSURealLegalPersonObject theOneDSUPerson in theDSUPersonCollection)
            {
                if (
                    theOneDSUPerson.NationalCode == theNewDSUPersonToAdd.NationalCode &&
                    theOneDSUPerson.personType == theNewDSUPersonToAdd.personType
                    )
                {
                    string newDSUPersonFullName = theNewDSUPersonToAdd.Name + " " + theNewDSUPersonToAdd.Family;
                    string recentDSUPersonFullName = theOneDSUPerson.Name + " " + theOneDSUPerson.Family;

                    if (
                        newDSUPersonFullName.GetStandardFarsiString() == recentDSUPersonFullName.GetStandardFarsiString() &&
                        theOneDSUPerson.personType == 1 && theNewDSUPersonToAdd.personType == 1 //Real Person
                        )
                    {
                        bool isMultiRoled = this.IsCurrentPersonMultiRoled(theCurrentRegisterServiceReq, theOneDSUPerson);
                        if (isMultiRoled)
                            return true;
                    }

                    if (
                        newDSUPersonFullName.GetStandardFarsiString() != recentDSUPersonFullName.GetStandardFarsiString() &&
                        theOneDSUPerson.personType == 0 && theNewDSUPersonToAdd.personType == 0 //Legal Person
                        )
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsCurrentPersonMultiRoled(Document theCurrentRegisterServiceReq, DSURealLegalPersonObject theOneDSUPerson)
        {
            bool isMultiRoled = false;

            List<DocumentPerson> equivalantPersonsCollection = this.CollectEquivalantDocPersons(theCurrentRegisterServiceReq, theOneDSUPerson);

            if (equivalantPersonsCollection==null ||  !equivalantPersonsCollection.Any())
                return false;


            DocumentPersonType? personRoleType = null;
            for (int i = 0; i < equivalantPersonsCollection.Count; i++)
            {
                if (i == 0)
                    personRoleType = equivalantPersonsCollection[i].DocumentPersonType;

                else if (personRoleType != equivalantPersonsCollection[i].DocumentPersonType)
                {
                    isMultiRoled = true;
                    break;
                }
            }

            return isMultiRoled;
        }

        private List<DocumentPerson> CollectEquivalantDocPersons(Document theCurrentRegisterServiceReq, DSURealLegalPersonObject theOneDSUPerson)
        {
            if (!theCurrentRegisterServiceReq.DocumentPeople.Any())
                return null;

            List<DocumentPerson> equivalantPersonsCollection = null;

            foreach (DocumentPerson theOneDocPerson in theCurrentRegisterServiceReq.DocumentPeople)
            {
                if (
                    theOneDocPerson.NationalNo == theOneDSUPerson.NationalCode &&
                    theOneDocPerson.FullName().GetStandardFarsiString() == (theOneDSUPerson.Name + theOneDSUPerson.Family).GetStandardFarsiString()
                    )
                {
                    if (equivalantPersonsCollection == null)
                        equivalantPersonsCollection = new List<DocumentPerson>();

                    equivalantPersonsCollection.Add(theOneDocPerson);
                }
            }

            return equivalantPersonsCollection;
        }

        private bool IsDSUOwnerPersonPermittedToAdd(DSURealLegalPersonObject theNewDSUPersonToAdd, List<DSURealLegalPersonObject> theDSUPersonCollection)
        {
            if (theNewDSUPersonToAdd == null)
                return false;

            if (!theDSUPersonCollection.Any())
                return true;

            foreach (DSURealLegalPersonObject theOneDSUPerson in theDSUPersonCollection)
            {
                //theOneRecentDSUPerson.RelationTypeId == "100"  //برای جلوگیری از افزوده شدن مالک اضافی به خلاصه               

                if (theOneDSUPerson.DSURelationKindId == "100")
                    return false;

                if (theOneDSUPerson.NationalCode == theNewDSUPersonToAdd.NationalCode)
                {
                    string newDSUPersonFullName = theNewDSUPersonToAdd.Name + " " + theNewDSUPersonToAdd.Family;
                    string recentDSUPersonFullName = theOneDSUPerson.Name + " " + theOneDSUPerson.Family;

                    if (
                        newDSUPersonFullName.GetStandardFarsiString() != recentDSUPersonFullName.GetStandardFarsiString() &&
                        theOneDSUPerson.personType == 0 && theNewDSUPersonToAdd.personType == 0//Legal Person
                        )
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsMovaresSpecified(DocumentPerson theOneDocPerson)
        {
            if (!theOneDocPerson.DocumentPersonRelatedAgentPeople.Any())
                return false;

            foreach (DocumentPersonRelated theOneDocAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople)
            {
                if (theOneDocAgent.AgentTypeId == "9") //مورث
                    return true;
            }


            return false;
        }
        #endregion
    }

}
