using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Other;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons.EstateInquiryManager;
namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class QuotasValidator
    {
        ClientConfiguration? _clientConfigurations = null;
        public QuotasValidator(ClientConfiguration? clientConfigurations)
        {
            this._clientConfigurations = clientConfigurations;

        }

        public bool VerifyRegCasesInquiriesAndQuotas(Document theONotaryRegisterServiceReq, ref string message)
        {


            try
            {
                if (
                    theONotaryRegisterServiceReq.DocumentType.IsSupportive == YesNo.Yes.GetString() ||
                    theONotaryRegisterServiceReq.DocumentType.DocumentTypeGroup1Id == "3" ||     //سند وكالت فروش اموال غيرمنقول
                                                                                                 //theONotaryRegisterServiceReq.TheONotaryDocumentType.TheONotaryDocumentTypeGroup2.TheONotaryDocumentTypeGroup1.Code == "5" ||     //اجاره
                    theONotaryRegisterServiceReq.DocumentType.DocumentTypeGroup1Id == "6" ||     //تقسیم نامه
                    theONotaryRegisterServiceReq.DocumentTypeId == "901" || //قرارداد پیش فروش ساختمان
                    (theONotaryRegisterServiceReq.DocumentEstates != null && theONotaryRegisterServiceReq.DocumentEstates.Count == 0)
                    )
                {
                    return true;
                }

                if (theONotaryRegisterServiceReq.DocumentType.WealthType == YesNo.No.GetString())
                {
                    bool isValid = false;
                    foreach (DocumentEstate theOneRegCase in theONotaryRegisterServiceReq.DocumentEstates)
                    {
                        isValid = this.VerifyImmovableQuotas(theOneRegCase, ref message);
                        if (!isValid)
                            return false;
                    }

                    string personQuotaMessages = null;
                    isValid = this.VerifyQuotaNeededPersons(theONotaryRegisterServiceReq, ref personQuotaMessages);

                    if (!string.IsNullOrWhiteSpace(personQuotaMessages))
                        message = message + System.Environment.NewLine + personQuotaMessages;

                    if (!isValid)
                        return false;
                }
                else
                {
                    foreach (DocumentEstate theOneRegCase in theONotaryRegisterServiceReq.DocumentEstates)
                    {
                        bool isValid = this.VerifyLinkageQuotas(theOneRegCase, ref message);
                        if (!isValid)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "خطا در بررسی صحت اطلاعات وارد شده در بخش سهم بندی ها. لطفاً اطلاعات ورودی را بررسی نموده و مجدداً تلاش نمایید.";
                return false;
            }

            message = string.Empty;
            return true;
        }
        public bool VerifySeparation(Document theRegisterServiceReq, ref string ResultMessage)
        {

            int ParkingCount = 0;
            int AppartmentCount = 0;
            foreach (DocumentEstate theOneRegCase in theRegisterServiceReq.DocumentEstates)
            {
                foreach (DocumentEstateSeparationPiece theOnePiece in theOneRegCase.DocumentEstateSeparationPieces)
                {
                    if (theOnePiece.DocumentEstateSeparationPieceKindId == "3")
                    {
                        ParkingCount = ParkingCount + 1;
                    }
                    if (theOnePiece.DocumentEstateSeparationPieceKindId == "2")
                    {
                        AppartmentCount = AppartmentCount + 1;
                    }
                }
            }

            foreach (DocumentEstate theOneRegCase in theRegisterServiceReq.DocumentEstates)
            {
                foreach (DocumentEstateSeparationPiece theOnePiece in theOneRegCase.DocumentEstateSeparationPieces)
                {
                    if (theOnePiece.HasOwner == YesNo.No.GetString())
                    {
                        decimal? TotalQuota = 0;
                        bool PieceHasQuota = false;
                        decimal? MaxTotalQuota = 0;
                        if (theOnePiece.DocumentEstateSeparationPieceKindId == "1" || theOnePiece.DocumentEstateSeparationPieceKindId == "2")
                        {

                            foreach (var q in theOnePiece.DocumentEstateSeparationPiecesQuota)
                            {
                                if (q.TotalQuota > MaxTotalQuota)
                                    MaxTotalQuota = q.TotalQuota;
                            }

                            decimal? df = 0;
                            foreach (var q in theOnePiece.DocumentEstateSeparationPiecesQuota)
                            {
                                df += (MaxTotalQuota / q.TotalQuota) * q.DetailQuota;
                            }


                            if ((df == 0) || (df / MaxTotalQuota) < 1)
                            {
                                if (theOnePiece.DocumentEstateSeparationPieceKindId == "1")
                                {
                                    ResultMessage = "مجموع سهم بندی قطعه " + theOnePiece.PieceNo + " از سهم کل این قطعه کمتر است. ";
                                }
                                else
                                {
                                    ResultMessage = "مجموع سهم بندی آپارتمان " + theOnePiece.PieceNo + " از سهم کل این آپارتمان کمتر است. ";
                                }

                                return false;
                            }

                            if ((df / MaxTotalQuota) > 1)
                            {
                                if (theOnePiece.DocumentEstateSeparationPieceKindId == "1")
                                {
                                    ResultMessage = "مجموع سهم بندی قطعه " + theOnePiece.PieceNo + "،از سهم کل این قطعه بیشتر است. ";
                                }
                                else
                                {
                                    ResultMessage = "مجموع سهم بندی آپارتمان " + theOnePiece.PieceNo + "، از سهم کل این آپارتمان بیشتر است. ";
                                }

                                return false;
                            }

                            if (ParkingCount != 0)
                            {
                                if (ParkingCount >= AppartmentCount)
                                {
                                    if (theOnePiece.InverseParking == null || theOnePiece.InverseParking.Count == 0)
                                    {
                                        ResultMessage = "پارکینگی برای آپارتمان شماره " + theOnePiece.PieceNo + " اختصاص نیافته است. یک پارکینگ برای این آپارتمان تعیین کنید.";
                                        return false;
                                    }
                                }
                                else if (ParkingCount < AppartmentCount)
                                {
                                    if (theOnePiece.InverseParking.Count > 1)
                                    {
                                        ResultMessage = "برای آپارتمان شماره " + theOnePiece.PieceNo + " بیش از یک پارکینگ اختصاص یافته است.";
                                        return false;
                                    }
                                }
                            }
                        }
                        else if (theOnePiece.DocumentEstateSeparationPieceKindId == "3" || theOnePiece.DocumentEstateSeparationPieceKindId == "4")
                        {
                            if (theOnePiece.Parking != null)
                            {
                                PieceHasQuota = true;
                            }

                            if (theOnePiece.Anbari != null)
                            {
                                PieceHasQuota = true;
                            }


                            if (PieceHasQuota == false)
                            {
                                if (theOnePiece.DocumentEstateSeparationPieceKindId == "3")
                                {
                                    ResultMessage = "وضعیت مالکیت پارکینگ " + theOnePiece.PieceNo + " مشخص نشده است.";
                                }
                                else
                                {
                                    ResultMessage = "وضعیت مالکیت انباری " + theOnePiece.PieceNo + " مشخص نشده است.";
                                }

                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public bool VerifyDealQuotas(Document theCurrentRegisterServiceReq, ref string messages)
        {
            if (!theCurrentRegisterServiceReq.DocumentEstates.Any())
            {
                messages = "در پرونده جاری مورد معامله ای تعریف نشده است.";
                return false;
            }



            List<DocumentPerson> theBuyersCollection = new List<DocumentPerson>();
            #region Collect-BuyerPerson
            foreach (DocumentPerson theOneDocPerson in theCurrentRegisterServiceReq.DocumentPeople)
            {
                if (theOneDocPerson.IsOriginal != YesNo.Yes.GetString() || theOneDocPerson.DocumentPersonType == null)
                    continue;

                if (theOneDocPerson.DocumentPersonType.IsOwner == YesNo.No.GetString())
                {
                    if (!theBuyersCollection.Any() || !theBuyersCollection.Contains(theOneDocPerson))
                        theBuyersCollection.Add(theOneDocPerson);
                }
            }

            if (!theBuyersCollection.Any())
            {
                messages = "انتقال گیرندگان در پرونده جاری مشخص نشده اند.";
                return false;
            }
            #endregion

            List<DocumentPerson> theSellersCollection = new List<DocumentPerson>();
            #region Collect-SellerPerson
            foreach (DocumentPerson theOneDocPerson in theCurrentRegisterServiceReq.DocumentPeople)
            {
                bool isMovares = this.IsMovarres(theOneDocPerson);

                if (isMovares || (theOneDocPerson.DocumentPersonType != null && theOneDocPerson.DocumentPersonType.IsOwner == YesNo.Yes.GetString()))
                {
                    if (!theSellersCollection.Any() || !theSellersCollection.Contains(theOneDocPerson))
                        theSellersCollection.Add(theOneDocPerson);
                }
            }

            if (!theSellersCollection.Any())
            {
                messages = "انتقال دهندگان در پرونده جاری مشخص نشده اند.";
                return false;
            }
            #endregion

            #region CollectAllDealingPersons
            List<DocumentPerson> theDealPersonsCollection = new List<DocumentPerson>();
            theDealPersonsCollection.AddRange(theSellersCollection);
            theDealPersonsCollection.AddRange(theBuyersCollection);
            #endregion

            foreach (DocumentEstate theOneRegCase in theCurrentRegisterServiceReq.DocumentEstates)
            {
                if (theOneRegCase.IsProportionateQuota == YesNo.No.GetString())
                    continue;

                if (theOneRegCase.IsProportionateQuota == YesNo.None.GetString())
                {
                    messages =
                        "برای " + theOneRegCase.RegCaseText() + "، محاسبه خودکار حسب السهم، مشخص نشده است." +
                        System.Environment.NewLine +
                        "لطفاً در قسمت سهم بندی، محاسبه و یا عدم محاسبه حسب السهم را مشخص نموده و مجدداً تلاش نمایید.";
                    return false;
                }

                if (theOneRegCase.IsProportionateQuota == YesNo.Yes.GetString() && string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId) && !theOneRegCase.DocumentInquiries.Any())
                {
                    messages =
                        "بدلیل عدم انتخاب استعلام هنگام تشکیل پرونده، محاسبه حسب السهم برای " +
                        theOneRegCase.Document.DocumentType.CaseTitle + " " +
                        " زیر امکان پذیر نمی باشد:" +
                        System.Environment.NewLine +
                        theOneRegCase.RegCaseText() + " " +
                        System.Environment.NewLine +
                        "برای محاسبه حسب السهم لطفاً پرونده را با انتخاب استعلام مرتبط مجدداً تشکیل نمایید.";

                    return false;
                }

                if (!theOneRegCase.DocumentEstateQuota.Any())
                {
                    messages = "برای " + theOneRegCase.RegCaseText() + "، سهم های مورد معامله اشخاص تعیین نشده است.";
                    return false;
                }

                #region Invalid-RegServicePersonType
                foreach (DocumentEstateQuotum theOneDealPersonQuota in theOneRegCase.DocumentEstateQuota)
                {
                    if (
                        theOneDealPersonQuota.DocumentPerson.DocumentPersonType == null ||
                        theOneDealPersonQuota.DocumentPerson.DocumentPersonType.IsOwner == YesNo.None.GetString()
                        )
                    {
                        bool isMovarres = this.IsMovarres(theOneDealPersonQuota.DocumentPerson);

                        if (!isMovarres)
                        {
                            messages =
                                      "با توجه به سمت وارد شده برای  " +
                                      theOneDealPersonQuota.DocumentPerson.FullName() +
                                      "، تعریف سهم برای این شخص مجاز نمی باشد." +
                                      System.Environment.NewLine +
                                      "لطفاً از بخش سهم متعاملین، ردیف سهم وارد شده برای این شخص را حذف نموده و مجدداً تلاش نمایید.";

                            return false;
                        }
                    }
                }
                #endregion

                #region VerifyInquiriesForCurrentCase
                bool inquiriesAreValid = this.VerifyInquiriesForCurrentCase(theOneRegCase, ref messages);
                if (!inquiriesAreValid)
                    return false;
                #endregion

                #region (BuyingDealQuotas = SellingDealQuotas)
                bool buyingAndSellingQuotasAreEqual = this.VerifyBuyingAndSellingDealQuotasAreEqual(theOneRegCase.DocumentEstateQuota, ref messages);
                if (!buyingAndSellingQuotasAreEqual)
                    return false;
                #endregion

                #region CollectNonDefinedPersons
                string nonDefinedPersons = string.Empty;
                bool funcEnabled = false;
                if (
                    !Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId) &&
                    funcEnabled
                    )
                    foreach (DocumentPerson theOneDealPerson in theDealPersonsCollection)
                    {
                        bool personDefined = false;

                        foreach (DocumentEstateQuotum theOneDealPersonQuota in theOneRegCase.DocumentEstateQuota)
                        {
                            if (theOneDealPersonQuota.DocumentPerson.DocumentPersonType.IsOwner == YesNo.None.GetString())
                            {
                                messages =
                                    "با توجه به سمت وارد شده برای  " +
                                    theOneDealPersonQuota.DocumentPerson.FullName() +
                                    "، تعریف سهم برای این شخص مجاز نمی باشد." + Environment.NewLine +
                                    "لطفاً از بخش سهم متعاملین، ردیف سهم وارد شده برای این شخص را حذف نموده و مجدداً تلاش نمایید.";

                                return false;
                            }

                            if (
                                theOneDealPersonQuota.DocumentPerson.Id == theOneDealPerson.Id ||
                                (theOneDealPersonQuota.DocumentPerson.NationalNo == theOneDealPerson.NationalNo &&
                                 theOneDealPersonQuota.DocumentPerson.FullName().GetStandardFarsiString() ==
                                 theOneDealPerson.FullName().GetStandardFarsiString())
                               )
                            {
                                personDefined = true;
                                break;
                            }
                        }

                        if (!personDefined)
                        {
                            var personFullName = theOneDealPerson.FullName() ?? string.Empty;

                            if (!nonDefinedPersons.Contains(personFullName))
                            {
                                nonDefinedPersons += "  - " + personFullName + Environment.NewLine;
                            }
                        }
                    }

                if (!string.IsNullOrWhiteSpace(nonDefinedPersons))
                {
                    messages =
                            "در " +
                            theOneRegCase.RegCaseText() +
                            "، سهمی برای اشخاص زیر تعریف نشده است: " +
                            System.Environment.NewLine +
                            nonDefinedPersons +
                            "برای محاسبه خودکار حسب السهم توسط سامانه، لازم است سهم همه اشخاص اصیل در پرونده، تعریف گردد.";

                    return false;
                }
                #endregion

                #region Validate Each DealQuota In Comparison To Original Inquiry And Quotas
                foreach (DocumentEstateQuotum theOneDealQuota in theOneRegCase.DocumentEstateQuota)
                {
                    if (Mathematics.GetDecimalPlacesCount((decimal)theOneDealQuota.DetailQuota) > 2)
                    {
                        messages =
                            "امکان محاسبه حسب السهم بصورت خودکار توسط سامانه امکان پذیر نیست." +
                            System.Environment.NewLine +
                            System.Environment.NewLine +
                            "جزئیات خطا : " +
                            System.Environment.NewLine +
                            " - " + theOneDealQuota.DocumentEstate.RegCaseText() +
                            System.Environment.NewLine +
                            System.Environment.NewLine +
                            "لطفاً گزینه حسب السهم را، (خیر) انتخاب نموده و سهم های هر خریدار از فروشنده را محاسبه و تعیین نمایید.";

                        return false;
                    }

                    decimal[] totalAvailableQuota = null;
                    if (this.IsMovarres(theOneDealQuota.DocumentPerson) || theOneDealQuota.DocumentPerson.DocumentPersonType.IsOwner == YesNo.Yes.GetString())
                    {
                        totalAvailableQuota = this.GetTotalSellerOriginalQuotas(theOneRegCase, theOneDealQuota.DocumentPerson, ref messages);

                        if (totalAvailableQuota == null)
                        {
                            if (string.IsNullOrWhiteSpace(messages))
                                messages = " خطا در دریافت کل سهم مالکیت " + theOneDealQuota.DocumentPerson.FullName() + " در " +
                                    theOneRegCase.RegCaseText();

                            return false;
                        }

                        if ((theOneDealQuota.DetailQuota.TrimDoubleValue() * totalAvailableQuota[1]).TrimDoubleValue() <= (theOneDealQuota.TotalQuota * totalAvailableQuota[0]).TrimDoubleValue())
                            continue;
                        else
                        {
                            messages = "سهم های مورد معامله " + theOneDealQuota.DocumentPerson.FullName() + " در " + theOneRegCase.RegCaseText() + "، از مجموع سهم های پاسخ استعلام بیشتر می باشد.";
                            return false;
                        }
                    }
                }
                #endregion

                #region Generate Prompt Message 4 NotIncluded Persons
                List<DocumentPerson> theNotIncludedBuyers = new List<DocumentPerson>();
                foreach (DocumentPerson theOneBuyer in theBuyersCollection)
                {
                    bool personIsIncluded = false;
                    foreach (DocumentEstateQuotum theOneDealQuota in theOneRegCase.DocumentEstateQuota)
                    {
                        if (
                            theOneBuyer.Id == theOneDealQuota.DocumentPerson.Id ||
                            (theOneBuyer.NationalNo == theOneDealQuota.DocumentPerson.NationalNo && theOneBuyer.FullName().GetStandardFarsiString() == theOneDealQuota.DocumentPerson.FullName().GetStandardFarsiString())
                            )
                        {
                            personIsIncluded = true;
                            break;
                        }
                    }

                    if (!personIsIncluded)
                        if (!theNotIncludedBuyers.Contains(theOneBuyer))
                            theNotIncludedBuyers.Add(theOneBuyer);
                }

                if (theNotIncludedBuyers.Any())
                {
                    string personNames = string.Empty;
                    foreach (DocumentPerson theOneNotIncludedPerson in theNotIncludedBuyers)
                    {
                        personNames += theOneNotIncludedPerson.FullName + System.Environment.NewLine;
                    }

                    messages += "- در" +
                                theOneRegCase.RegCaseText() +
                                System.Environment.NewLine +
                                "انتقال گیرندگان زیر دارای سهم بندی نمی باشند:" +
                                System.Environment.NewLine +
                                personNames;
                }
                #endregion

                #region IsProportionatePermittedBasedOnInquiries
                foreach (DocumentInquiry theOneInquiry in theCurrentRegisterServiceReq.DocumentInquiries)
                {
                    if (string.IsNullOrWhiteSpace(theOneInquiry.EstateInquiriesId))
                        continue;

                    if (theOneInquiry.ReplyDetailQuota != null && (Mathematics.GetDecimalPlacesCount((decimal)theOneInquiry.ReplyTotalQuota)) > 0)
                    {
                        string message =
                            "استعلام مربوط به این " +
                            theCurrentRegisterServiceReq.DocumentType.CaseTitle +
                            " دارای بخش اعشار در کل سهم می باشد و امکان محاسبه حسب السهم با این شرایط وجود ندارد.";

                        return false;
                    }
                }
                #endregion

            }

            return true;
        }
        //===========================================================================================================================
        private bool VerifyQuotaNeededPersons(Document theCurrentRegisterServiceReq, ref string messages)
        {
            bool isRestrictedDoc = Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId);
            bool isDSUPermitted = DsuUtility.IsDSUGeneratingPermitted(ref messages, theCurrentRegisterServiceReq, null, false, _clientConfigurations);

            foreach (DocumentPerson theOneDocPersons in theCurrentRegisterServiceReq.DocumentPeople)
            {
                if (
                    !isRestrictedDoc && isDSUPermitted &&
                    theOneDocPersons.DocumentPersonType != null &&
                    theOneDocPersons.DocumentPersonType.IsOwner == YesNo.No.GetString()
                    )
                {
                    bool buyerHasQuota = this.buyerQuotaHasDefined(theCurrentRegisterServiceReq, theOneDocPersons);

                    if (!buyerHasQuota)
                    {
                        messages =
                            "سهم های تعریف شده کامل نمی باشد" +
                            System.Environment.NewLine +
                            System.Environment.NewLine +
                            " جزئیات خطا : " +
                            System.Environment.NewLine +
                            " - " + theOneDocPersons.DocumentPersonType.SingularTitle + " : " + theOneDocPersons.FullName() +
                            System.Environment.NewLine +
                            " *** این شخص در سند دارای سهم بندی نمی باشد. لطفاً سهم های مورد نیاز را تعریف نمایید *** ";

                        return false;
                    }
                }
            }

            return true;
        }

        private bool VerifyInquiriesForCurrentCase(DocumentEstate theOneRegCase, ref string message)
        {
            if (string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId) && !(theOneRegCase.DocumentInquiries == null || theOneRegCase.DocumentInquiries.Count == 0))
                return true;

            List<DocumentInquiry> theCorrespondingInquiries = new List<DocumentInquiry>();

            foreach (DocumentInquiry theOneInquiry in theOneRegCase.Document.DocumentInquiries)
            {
                if (string.IsNullOrWhiteSpace(theOneInquiry.EstateInquiriesId))
                    continue;

                if (theOneRegCase.EstateInquiryId.Contains(theOneInquiry.EstateInquiriesId) || (theOneRegCase.DocumentInquiries != null && theOneRegCase.DocumentInquiries.Any(d => d.EstateInquiriesId == theOneInquiry.EstateInquiriesId)))
                    if (!theCorrespondingInquiries.Contains(theOneInquiry))
                        theCorrespondingInquiries.Add(theOneInquiry);
            }

            if ( theCorrespondingInquiries.Count == 0)
                return true;

            decimal[] totalInquiriesQuota = null;
            foreach (DocumentInquiry theOneInquiry in theCorrespondingInquiries)
            {
                if (theOneInquiry.ReplyDetailQuota == null || theOneInquiry.ReplyDetailQuota == 0 || theOneInquiry.ReplyTotalQuota == null || theOneInquiry.ReplyTotalQuota == 0)
                    continue;

                if (totalInquiriesQuota == null)
                {
                    totalInquiriesQuota = new decimal[2];
                    totalInquiriesQuota[0] = (decimal)theOneInquiry.ReplyDetailQuota;
                    totalInquiriesQuota[1] = (decimal)theOneInquiry.ReplyTotalQuota;
                }
                else
                {
                    totalInquiriesQuota = Mathematics.MakhrajMoshtarak(totalInquiriesQuota[0], (decimal)theOneInquiry.ReplyDetailQuota, totalInquiriesQuota[1], (decimal)theOneInquiry.ReplyTotalQuota);
                }
            }

            if (totalInquiriesQuota != null)
            {
                if (totalInquiriesQuota[0] > totalInquiriesQuota[1])
                {
                    message = "مجموع سهم مالکیت مندرج در پاسخ استعلام های " + theOneRegCase.RegCaseText() + "، بیشتر از عدد صحیح 1 می باشد. لطفاً از صحت استعلام های انتخاب شده اطمینان حاصل نمایید.";
                    return false;
                }
            }


            return true;
        }

        private bool VerifyBuyingAndSellingDealQuotasAreEqual(ICollection<DocumentEstateQuotum> dealQuotasCollection, ref string messages)
        {
            List<DocumentEstateQuotum> buyersDealQuotas = new List<DocumentEstateQuotum>();
            List<DocumentEstateQuotum> sellersDealQuotas = new List<DocumentEstateQuotum>();

            foreach (DocumentEstateQuotum theOneDealQuota in dealQuotasCollection)
            {
                if (theOneDealQuota.DetailQuota == 0 || theOneDealQuota.DetailQuota == null)
                {
                    messages =
                        "خطا در اطلاعات سهم های وارد شده! جزء سهم معتبر نمی باشد. لطفاً جزء سهم را بطور صحیح وارد نمایید." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا: " +
                        System.Environment.NewLine +
                        " - " + theOneDealQuota.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneDealQuota.DocumentEstate.RegCaseText() +
                        System.Environment.NewLine +
                        " - سهم تعریف شده برای : " + theOneDealQuota.DocumentPerson.FullName();

                    return false;
                }

                if (theOneDealQuota.TotalQuota == 0 || theOneDealQuota.TotalQuota == null)
                {
                    messages =
                        "خطا در اطلاعات سهم های وارد شده! کل سهم معتبر نمی باشد. لطفاً کل سهم را بطور صحیح وارد نمایید." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا: " +
                        System.Environment.NewLine +
                        " - " + theOneDealQuota.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneDealQuota.DocumentEstate.RegCaseText() +
                        System.Environment.NewLine +
                        " - سهم تعریف شده برای : " + theOneDealQuota.DocumentPerson.FullName();

                    return false;
                }

                if (this.IsMovarres(theOneDealQuota.DocumentPerson) || theOneDealQuota.DocumentPerson.DocumentPersonType.IsOwner == YesNo.Yes.GetString())
                {
                    if (!sellersDealQuotas.Contains(theOneDealQuota))
                        sellersDealQuotas.Add(theOneDealQuota);
                }
                else if (theOneDealQuota.DocumentPerson.DocumentPersonType.IsOwner == YesNo.No.GetString())
                {
                    if (!buyersDealQuotas.Contains(theOneDealQuota))
                        buyersDealQuotas.Add(theOneDealQuota);
                }
            }

            if (!sellersDealQuotas.Any())
            {
                messages = "برای انتقال دهندگان سهمی تعیین نشده است.";
                return false;
            }

            if (!buyersDealQuotas.Any())
            {
                messages = "برای انتقال گیرندگان هیچ سهمی تعیین نشده است.";
                return false;
            }

            decimal[] buyersTotalQuota = null;
            decimal[] sellersTotalQuota = null;
            foreach (DocumentEstateQuotum theOneDealQuota in sellersDealQuotas)
            {
                if (sellersTotalQuota == null)
                {
                    sellersTotalQuota = new decimal[2];
                    sellersTotalQuota[0] = theOneDealQuota.DetailQuota.TrimDoubleValue() ?? (decimal)0.0;
                    sellersTotalQuota[1] = theOneDealQuota.TotalQuota ?? (decimal)0.0;
                }
                else
                {
                    sellersTotalQuota = Mathematics.MakhrajMoshtarak(sellersTotalQuota[0], (decimal)theOneDealQuota.DetailQuota, sellersTotalQuota[1], (decimal)theOneDealQuota.TotalQuota);
                }
            }

            foreach (DocumentEstateQuotum theOneDealQuota in buyersDealQuotas)
            {
                if (buyersTotalQuota == null)
                {
                    buyersTotalQuota = new decimal[2];
                    if (sellersTotalQuota == null)
                    {
                        sellersTotalQuota = new decimal[2];
                    }
                    sellersTotalQuota[0] = theOneDealQuota.DetailQuota.TrimDoubleValue() ?? (decimal)0.0;

                    sellersTotalQuota[1] = theOneDealQuota.TotalQuota ?? (decimal)0.0;
                }
                else
                {
                    buyersTotalQuota = Mathematics.MakhrajMoshtarak(buyersTotalQuota[0], (decimal)theOneDealQuota.DetailQuota, buyersTotalQuota[1], (decimal)theOneDealQuota.TotalQuota);
                }
            }

            decimal buyersTotalEnumerator = buyersTotalQuota[0];
            decimal buyersTotalDenumerator = buyersTotalQuota[1];
            decimal sellersTotalEnumerator = sellersTotalQuota[0];
            decimal sellersTotalDenumerator = sellersTotalQuota[1];

            if ((buyersTotalEnumerator * sellersTotalDenumerator) != (buyersTotalDenumerator * sellersTotalEnumerator))
            {
                messages =
                    "مجموع سهم های وارد شده برای انتقال دهندگان با مجموع سهم های وارد شده برای انتقال گیرندگان در سند، برابر نیستند. " +
                    System.Environment.NewLine +
                    "لطفاً در بخش سهم متعاملین، سهم ها را مجدداً بررسی نمایید.";

                return false;
            }

            return true;
        }

        private bool VerifyIndividualQuota(DocumentEstateQuotaDetail theOneQuota, ref string message)
        {
            message = "در بخش سهم بندی ها اشکالات زیر را برطرف نمایید:\n\n";

            if (theOneQuota.DocumentPersonBuyer == null || theOneQuota.DocumentPersonSeller == null)
            {
                message +=
                    " طرفین سهم بندی بطور کامل تعیین نشده است." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا: " +
                    System.Environment.NewLine +
                    " - " + theOneQuota.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneQuota.DocumentEstate.RegCaseText() +
                    System.Environment.NewLine +
                    " - مستند مالکیت با شماره ردیف  " + theOneQuota.DocumentEstateOwnershipDocument.RowNo + " : " + theOneQuota.DocumentEstateOwnershipDocument.OwnershipDocTitle() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "*** سهم های تعریف شده در مستند مذکور را بررسی نموده و از تعیین شدن طرفین سهم مطمئن شوید ***";

                return false;
            }

            string buyerName = theOneQuota.DocumentPersonBuyer.FullName();
            string sellerName = theOneQuota.DocumentPersonSeller.FullName();
            string caseTitle = theOneQuota.DocumentEstate.Document.DocumentType.CaseTitle;
            string caseFullName = caseTitle + " " + theOneQuota.DocumentEstate.RegCaseText();
            string ownershipDocFullName = string.Empty;

            if (theOneQuota.DocumentPersonSellerId != theOneQuota.DocumentEstateOwnershipDocument.DocumentPersonId)
            {
                message +=
                    "سهم تعریف شده اشتباه می باشد." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا: " +
                    System.Environment.NewLine +
                    " - " + theOneQuota.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneQuota.DocumentEstate.RegCaseText() +
                    System.Environment.NewLine +
                    " - مستند مالکیت با شماره ردیف  " + theOneQuota.DocumentEstateOwnershipDocument.RowNo + " : " + theOneQuota.DocumentEstateOwnershipDocument.OwnershipDocTitle() +
                    System.Environment.NewLine +
                    " - سهم تعریف شده بین " + theOneQuota.DocumentPersonSeller.FullName() + " و " + theOneQuota.DocumentPersonBuyer.FullName() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "*** بدلیل عدم تطابق مالک مستند و انتقال دهنده در سهم مذکور، لازم است این سهم از جدول (حذف) شده و مجدداً تعریف گردد. ***";

                return false;
            }

            if (
                //theOneQuota.TheONotaryRegCase.TheONotaryRegisterServiceReq.TheONotaryDocumentType.Code == "221" && //اسناد صلح
                theOneQuota.DocumentPersonBuyer.DocumentPersonType.IsOwner != YesNo.No.GetString()
                )
            {
                message +=
                    "سمت انتقال گیرنده در سهم تعریف شده اشتباه می باشد." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا: " +
                    System.Environment.NewLine +
                    " - " + theOneQuota.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneQuota.DocumentEstate.RegCaseText() +
                    System.Environment.NewLine +
                    " - مستند مالکیت با شماره ردیف  " + theOneQuota.DocumentEstateOwnershipDocument.RowNo + " : " + theOneQuota.DocumentEstateOwnershipDocument.OwnershipDocTitle() +
                    System.Environment.NewLine +
                    " - سهم تعریف شده بین " + theOneQuota.DocumentPersonSeller.FullName() + " و " + theOneQuota.DocumentPersonBuyer.FullName() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "لطفاً سمت شخص انتقال گیرنده را در سهم تعریف شده کنترل نموده و سمتی را انتخاب نمایید که از نوع انتقال گیرنده باشد.";

                return false;
            }



            if (string.IsNullOrWhiteSpace(theOneQuota.DocumentEstate.EstateInquiryId) && ( theOneQuota.DocumentEstate.DocumentInquiries == null || theOneQuota.DocumentEstate.DocumentInquiries.Count == 0))
                return true;

            if (theOneQuota.DocumentPersonBuyer.PersonType == PersonType.NaturalPerson.GetString() && theOneQuota.DocumentPersonBuyer.IdentityNo != null && !theOneQuota.DocumentPersonBuyer.IdentityNo.IsDigit())
            {
                message +=

                    "شماره شناسنامه شخص باید فقط شامل عدد باشد." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا: " +
                    System.Environment.NewLine +
                    " - شخص مورد خطا: " + theOneQuota.DocumentPersonBuyer.FullName() +
                    System.Environment.NewLine +
                    " - شماره شناسنامه وارد شده : " + theOneQuota.DocumentPersonBuyer.IdentityNo +
                    System.Environment.NewLine +
                    "*** لطفاً شماره شناسنامه شخص مذکور را اصلاح نموده و پس از ثبت اطلاعات، مجدداً تلاش نمایید. *** ";

                return false;
            }

            if (string.IsNullOrWhiteSpace(theOneQuota.QuotaText) &&
                (theOneQuota.SellDetailQuota == null || theOneQuota.SellDetailQuota <= 0 ||
                theOneQuota.SellTotalQuota == null || theOneQuota.SellTotalQuota <= 0 ||
                theOneQuota.OwnershipDetailQuota == null || theOneQuota.OwnershipDetailQuota <= 0 ||
                theOneQuota.OwnershipTotalQuota == null || theOneQuota.OwnershipTotalQuota <= 0
                )
                )
            {
                message +=

                    "سهم تعریف شده معتبر نمی باشد. " +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا: " +
                    System.Environment.NewLine +
                    " - " + theOneQuota.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneQuota.DocumentEstate.RegCaseText() +
                    System.Environment.NewLine +
                    " - مستند مالکیت با شماره ردیف  " + theOneQuota.DocumentEstateOwnershipDocument.RowNo + " : " + theOneQuota.DocumentEstateOwnershipDocument.OwnershipDocTitle +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "*** سهم های تعریف شده در مستند مذکور را بررسی نموده و از تعیین شدن جزء و کل سهم و یا متن سهم بطور صحیح، مطمئن شوید ***";

                return false;
            }


            if (
                theOneQuota.DocumentEstate.Document.State != NotaryRegServiceReqState.FinalPrinted.GetString() &&
                theOneQuota.DocumentEstate.Document.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString()
                )
            {
                if (!string.IsNullOrWhiteSpace(theOneQuota.QuotaText))
                {
                    if (
                        theOneQuota.SellTotalQuota != null  ||
                        theOneQuota.SellTotalQuota == 0
                        )
                    {
                        message +=
                        "سهم تعریف شده اشتباه می باشد." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا: " +
                        System.Environment.NewLine +
                        " - " + theOneQuota.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneQuota.DocumentEstate.RegCaseText() +
                        System.Environment.NewLine +
                        " - مستند مالکیت با شماره ردیف  " + theOneQuota.DocumentEstateOwnershipDocument.RowNo + " : " + theOneQuota.DocumentEstateOwnershipDocument.OwnershipDocTitle() +
                        System.Environment.NewLine +
                        " - سهم تعریف شده بین " + theOneQuota.DocumentPersonSeller.FullName() + " و " + theOneQuota.DocumentPersonSeller.FullName() +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "***لطفاً در بخش سهم بندی از متن سهم و یا جزء و کل سهم استفاده نمایید. تعریف همزمان متن سهم و جزء و کل سهم مجاز نمی باشد. ***";

                        return false;
                    }

                    if (!string.IsNullOrWhiteSpace(theOneQuota.DocumentEstate.Document.DocumentInfoText.ConditionsText) && (theOneQuota.DocumentEstate.Document.DocumentInfoText.ConditionsText.Length > 2000))
                    {
                        message +=
                            "متن شرط وارد شده جهت ارسال در خلاصه معامله، قابل ارسال نمی باشد." +
                            System.Environment.NewLine +
                            System.Environment.NewLine +
                            "حداکثر طول مجاز برای متن شرط : 2000 کاراکتر" +
                            System.Environment.NewLine +
                            "*** لطفاً متن شرط را اصلاح نموده و سپس مجدداً تلاش نمایید. ***";

                        return false;
                    }

                    if (theOneQuota.QuotaText.Length > 250)
                    {
                        message += "در سهم مالکیت تعریف شده برای " + theOneQuota.DocumentEstateOwnershipDocument.OwnershipDocTitle() + System.Environment.NewLine +
                                   "برای اشخاص " + buyerName + " و " + sellerName +
                                   " طول متن سهم بیش از حد مجاز می باشد. حداکثر مجاز: 250 کاراکتر ";

                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            if (
                (theOneQuota.SellDetailQuota == null ||
                theOneQuota.SellTotalQuota == null ||
                theOneQuota.SellTotalQuota <= 0
                ) &&
                (string.IsNullOrWhiteSpace(theOneQuota.QuotaText))
                )
            {
                message += "وارد کردن جزء و کل سهم " + caseTitle + " یا متن سهم، اجباری می باشد.";
                return false;
            }

            if (
                (theOneQuota.OwnershipDetailQuota == null ||
                theOneQuota.OwnershipTotalQuota == null ||
                theOneQuota.OwnershipTotalQuota <= 0
                ) &&
                (string.IsNullOrWhiteSpace(theOneQuota.QuotaText))
                )
            {
                message += "وارد کردن جزء و کل سهم " + caseTitle + " یا متن سهم، اجباری می باشد.";
                return false;
            }

            if ((theOneQuota.OwnershipDetailQuota.TrimDoubleValue() * theOneQuota.SellTotalQuota).TrimDoubleValue() < (theOneQuota.OwnershipTotalQuota * theOneQuota.SellDetailQuota.TrimDoubleValue()).TrimDoubleValue())
            {
                string subMessageOwnership = string.Empty;

                message =

                    "سهم تعریف شده معتبر نمی باشد. " +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا: " +
                    System.Environment.NewLine +
                    " - " + theOneQuota.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneQuota.DocumentEstate.RegCaseText() +
                    System.Environment.NewLine +
                    " - مستند مالکیت با شماره ردیف  " + theOneQuota.DocumentEstateOwnershipDocument.RowNo + " : " + theOneQuota.DocumentEstateOwnershipDocument.OwnershipDocTitle() +
                    System.Environment.NewLine +
                    " - سهم بندی تعیین شده بین " + sellerName + " و " + buyerName +
                    System.Environment.NewLine +
                    "سهم " + caseTitle + " " + sellerName + " بیش از سهم مالکیت وی می باشد." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "*** سهم های تعریف شده در مستند مذکور را بررسی نموده و از تعیین شدن جزء و کل سهم و یا متن سهم مطمئن شوید ***";

                return false;
            }

            return true;
        }

        private bool VerifyImmovableQuotas(DocumentEstate theOneRegCase, ref string message)
        {
            bool isDocTypeDSUForced = DsuUtility.IsDocTypeDSUForced(theOneRegCase.Document.DocumentType);
            bool isDSUPermitted = DsuUtility.IsDSUGeneratingPermitted(ref message, theOneRegCase.Document, null, true, _clientConfigurations);
            if (isDocTypeDSUForced && isDSUPermitted)
            {
                if (
                    theOneRegCase.Document.IsRegistered == YesNo.None.GetString() &&
                    string.Compare(theOneRegCase.Document.RequestDate, _clientConfigurations.DSUInitializationDate) >= 0 &&
                    theOneRegCase.Document.State != NotaryRegServiceReqState.FinalPrinted.GetString()
                    )
                {
                    message =
                        "برای پرونده جاری هنگام تشکیل پرونده، وضعیت ثبتی ملک (ملک ثبت شده یا ثبت نشده) مشخص نشده است." +
                        System.Environment.NewLine +
                        "لطفاً پرونده جاری را ابطال نموده و مجدداً نسبت به تشکیل پرونده جدید اقدام نمایید.";

                    return false;
                }
            }

            if (!isDSUPermitted)
                return true;

            if (theOneRegCase.IsProportionateQuota == YesNo.Yes.GetString() && string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId) && (theOneRegCase.DocumentInquiries == null || theOneRegCase.DocumentInquiries.Count == 0))
            {
                message =
                    "بدلیل عدم انتخاب استعلام هنگام تشکیل پرونده، محاسبه حسب السهم برای " +
                    theOneRegCase.Document.DocumentType.CaseTitle + " " +
                    " زیر امکان پذیر نمی باشد:" +
                    System.Environment.NewLine +
                    theOneRegCase.RegCaseText() + " " +
                    System.Environment.NewLine +
                    "برای محاسبه حسب السهم لطفاً پرونده را با انتخاب استعلام مرتبط مجدداً تشکیل نمایید.";

                return false;
            }


            if (string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId) && (theOneRegCase.DocumentInquiries == null || theOneRegCase.DocumentInquiries.Count == 0))
            {
                message =
                    "برای " +
                    theOneRegCase.Document.DocumentType.CaseTitle + " " + theOneRegCase.RegCaseText() +
                    " استعلام مشخص نشده است. لطفاً پرونده را مجدداً تنظیم نمایید.";

                return false;
            }

            if (theOneRegCase.IsAttachment == YesNo.Yes.ToString())
            {
                if (theOneRegCase.ReceiverBasicPlaque == theOneRegCase.BasicPlaque && theOneRegCase.ReceiverSecondaryPlaque == theOneRegCase.SecondaryPlaque)
                {
                    message =
                        "در انتقال منضم، پلاک اصلی و فرعی ملک های مورد انتقال و انتقال گیرنده نباید یکسان باشند. " +
                        System.Environment.NewLine +
                        "جزئیات خطا: " +
                        System.Environment.NewLine +
                        " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText();

                    return false;
                }
            }

            if (
                !DsuUtility.IsDSUGeneratingPermitted(ref message, theOneRegCase.Document, null, false, _clientConfigurations) &&
                theOneRegCase.IsProportionateQuota == YesNo.Yes.GetString() &&
                theOneRegCase.Document.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString() &&
                theOneRegCase.Document.State != NotaryRegServiceReqState.FinalPrinted.GetString()
                )
            {
                message =
                    "در این نوع سند، سهم ها بصورت حسب السهم توسط سیستم محاسبه نمی شوند." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا : " +
                    System.Environment.NewLine +
                    " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "لطفاً سهم ها را بصورت دستی در بخش سهم هر خریدار از فروشنده تعیین نمایید.";

                return false;
            }

            if (!(theOneRegCase.DocumentEstateQuotaDetails == null || theOneRegCase.DocumentEstateQuotaDetails.Count == 0) && (!string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId) || (theOneRegCase.DocumentInquiries != null && theOneRegCase.DocumentInquiries.Count > 0)))
            {
                message =
                    "سهم بندی ها بطور کامل انجام نشده است." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا : " +
                    System.Environment.NewLine +
                    " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "*** لطفاً در بخش سهم بندی، سهم های هر خریدار از فروشنده را تعیین نمایید. ***";

                return false;
            }

            if (!(theOneRegCase.DocumentEstateOwnershipDocuments == null || theOneRegCase.DocumentEstateOwnershipDocuments.Count == 0) && (!string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId) || (theOneRegCase.DocumentInquiries != null && theOneRegCase.DocumentInquiries.Count > 0)))
            {
                message =
                    "مستندات مالکیت تعیین تعریف نشده اند." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "جزئیات خطا : " +
                    System.Environment.NewLine +
                    " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    " *** لطفاً مستندات مالکیت را مشخص نمایید. ***";

                return false;
            }

            #region VerifyInquiriesForCurrentCase
            bool inquiriesAreValid = this.VerifyInquiriesForCurrentCase(theOneRegCase, ref message);
            if (!inquiriesAreValid)
                return false;
            #endregion

            if (!isDocTypeDSUForced && (theOneRegCase.DocumentEstateOwnershipDocuments == null || theOneRegCase.DocumentEstateOwnershipDocuments.Count == 0) && string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId) && (theOneRegCase.DocumentInquiries == null || theOneRegCase.DocumentInquiries.Count == 0))
                return true;


            foreach (DocumentEstateOwnershipDocument theOneOwnershipDocument in theOneRegCase.DocumentEstateOwnershipDocuments)
            {
                if (
                    DsuUtility.IsDSUGeneratingPermitted(ref message, theOneRegCase.Document, null, false, _clientConfigurations) &&
                    theOneOwnershipDocument.OwnershipDocumentType == NotaryOwnershipDocumentType.SabtDocument.GetString() &&
                    string.IsNullOrWhiteSpace(theOneOwnershipDocument.EstateInquiriesId)
                    )
                {
                    message =
                        "مستند مالکیت از نوع سند مالکیت بصورت غیر خودکار و توسط کاربر وارد شده است." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        " جزئیات خطا : " +
                        System.Environment.NewLine +
                        " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText() +
                        System.Environment.NewLine +
                        " - مستند مالکیت با شماره ردیف  : " + theOneOwnershipDocument.RowNo +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "لطفاً این مستند را حذف نموده و در صورت نیاز از دکمه ایجاد رکوردهای مستند مالکیت بر اساس سند قطعی استفاده نمایید.";

                    return false;
                }

                if (theOneOwnershipDocument.DocumentEstateQuotaDetails == null || theOneOwnershipDocument.DocumentEstateQuotaDetails.Count == 0)
                {
                    message =

                        "سهم بندی ها انجام نشده است." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا : " +
                        System.Environment.NewLine +
                        " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText() +
                        System.Environment.NewLine +
                        " - مستند مالکیت : " + theOneOwnershipDocument.OwnershipDocTitle() +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "*** لطفاً سهم های مرتبط به این مستند را تعریف نمایید. ***";

                    return false;
                }

                if (theOneOwnershipDocument.DocumentPerson.DocumentPersonType != null)
                    if (theOneOwnershipDocument.DocumentPerson.DocumentPersonType.IsOwner != YesNo.Yes.GetString())
                    {
                        message =

                        "شخص انتخاب شده به عنوان مالک مستند، سمت مالکیت در سند ندارد." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا : " +
                        System.Environment.NewLine +
                        " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText() +
                        System.Environment.NewLine +
                        " - مستند مالکیت : " + theOneOwnershipDocument.OwnershipDocTitle() +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "*** لطفاً شخص با سمت مالکیت را به عنوان مالک مستند انتخاب نمایید. ***";

                        return false;
                    }

                if (!string.IsNullOrWhiteSpace(theOneOwnershipDocument.DealSummaryText) && theOneOwnershipDocument.DealSummaryText.Length > 1960)
                {
                    message =

                        "موارد لازم به ذکر در خلاصه معامله نباید بیش از 2000 کاراکتر باشند." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا : " +
                        System.Environment.NewLine +
                        " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText() +
                        System.Environment.NewLine +
                        " - مستند مالکیت : " + theOneOwnershipDocument.OwnershipDocTitle() +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "*** لطفاً موارد لازم به ذکر در خلاصه معامله را اصلاح نموده و مجدداً تلاش نمایید. ***";

                    return false;
                }

                decimal[] totalSellingQuoteFraction = null;
                float totalAvailableOwnershipQuote = 0;
                decimal[] totalAvailableOwnershipQuoteFraction = new decimal[2];

                #region totalAvailableOwnershipQuote-Calculations

                #region Find Equivalant Inquiry
                DocumentInquiry theEquivalantInquiry = null;
                if (!string.IsNullOrWhiteSpace(theOneOwnershipDocument.EstateInquiriesId))
                    foreach (DocumentInquiry theOneInquiry in theOneOwnershipDocument.DocumentEstate.Document.DocumentInquiries)
                    {
                        if (string.IsNullOrWhiteSpace(theOneInquiry.EstateInquiriesId))
                            continue;

                        string inquiryMainID = theOneOwnershipDocument.EstateInquiriesId;
                        if (theOneOwnershipDocument.EstateInquiriesId.Contains("@Duplicated"))
                            inquiryMainID = inquiryMainID.Replace("@Duplicated", "");

                        if (theOneInquiry.EstateInquiriesId.Contains(inquiryMainID))
                        {
                            theEquivalantInquiry = theOneInquiry;
                            break;
                        }
                    }
                #endregion

                #region Equivalant Inquiry Not Found
                if (
                    theEquivalantInquiry == null ||
                    (
                    !string.IsNullOrWhiteSpace(theEquivalantInquiry.ReplyQuotaText) &&
                    (
                    theEquivalantInquiry.ReplyDetailQuota == null ||
                    theEquivalantInquiry.ReplyDetailQuota == 0 ||
                    theEquivalantInquiry.ReplyTotalQuota == null ||
                    theEquivalantInquiry.ReplyTotalQuota == 0
                    )
                    )
                    )
                {
                    if (string.IsNullOrWhiteSpace(theOneRegCase.QuotaText))
                    {
                        if (
                            !(theOneRegCase.OwnershipTotalQuota == null ||
                             theOneRegCase.OwnershipTotalQuota == 0 ||
                             theOneRegCase.OwnershipDetailQuota == null)
                            )
                        {
                            totalAvailableOwnershipQuote = (float)theOneRegCase.OwnershipDetailQuota.TrimDoubleValue() / (float)theOneRegCase.OwnershipTotalQuota;
                            totalAvailableOwnershipQuoteFraction[0] = (decimal)theOneRegCase.OwnershipDetailQuota.TrimDoubleValue();
                            totalAvailableOwnershipQuoteFraction[1] = (decimal)theOneRegCase.OwnershipTotalQuota;
                        }
                        else
                        {
                            message =
                                "مقادیر ورودی برای جزء سهم و کل سهم " +
                                theOneRegCase.Document.DocumentType.CaseTitle + " " +
                                theOneRegCase.RegCaseText() + " " +
                                " معتبر نمی باشند.";

                            return false;
                        }
                    }
                }
                #endregion

                #region Equivalant Inquiry Found
                else
                {
                    if (
                        theEquivalantInquiry.ReplyDetailQuota != null &&
                        theEquivalantInquiry.ReplyDetailQuota > 0 &&
                        theEquivalantInquiry.ReplyTotalQuota != null &&
                        theEquivalantInquiry.ReplyTotalQuota > 0
                        )
                    {
                        totalAvailableOwnershipQuote = (float)theEquivalantInquiry.ReplyDetailQuota / (float)theEquivalantInquiry.ReplyTotalQuota;
                        totalAvailableOwnershipQuoteFraction[0] = (decimal)theEquivalantInquiry.ReplyDetailQuota;
                        totalAvailableOwnershipQuoteFraction[1] = (decimal)theEquivalantInquiry.ReplyDetailQuota;
                    }

                }
                #endregion
                #endregion

                foreach (DocumentEstateQuotaDetail theOneQuota in theOneOwnershipDocument.DocumentEstateQuotaDetails)
                {
                    if (theOneQuota.DocumentEstate.Id != theOneOwnershipDocument.DocumentEstate.Id)
                        continue;

                    //if ( theOneQuota.IsMarkForDelete )
                    //    continue;

                    bool individualQuotaValidationStatus = this.VerifyIndividualQuota(theOneQuota, ref message);
                    if (!individualQuotaValidationStatus)
                        return false;

                    if (!string.IsNullOrWhiteSpace(theOneQuota.QuotaText))
                        continue;

                    if (totalSellingQuoteFraction == null)
                    {
                        totalSellingQuoteFraction = new decimal[2];
                        totalSellingQuoteFraction[0] = (decimal)theOneQuota.SellDetailQuota.TrimDoubleValue();
                        totalSellingQuoteFraction[1] = (decimal)theOneQuota.SellTotalQuota;
                    }
                    else
                    {
                        totalSellingQuoteFraction = Mathematics.MakhrajMoshtarak(totalSellingQuoteFraction[0], (decimal)theOneQuota.SellDetailQuota.TrimDoubleValue(), totalSellingQuoteFraction[1], (decimal)theOneQuota.SellTotalQuota);
                    }

                    if ((totalSellingQuoteFraction[0] * totalAvailableOwnershipQuoteFraction[1]) > (totalSellingQuoteFraction[1] * totalAvailableOwnershipQuoteFraction[0]))
                    {
                        message =
                                "سهم های تعیین شده بیش از کل سهم مالکیت می باشد." +
                                System.Environment.NewLine +
                                System.Environment.NewLine +
                                "جزئیات خطا : " +
                                System.Environment.NewLine +
                                " - " + theOneRegCase.Document.DocumentType.CaseTitle + " : " + theOneRegCase.RegCaseText() +
                                System.Environment.NewLine +
                                " - مستند مالکیت : " + theOneOwnershipDocument.OwnershipDocTitle() +
                                System.Environment.NewLine +
                                " - شخص بررسی شده : " + theOneQuota.DocumentPersonSeller.FullName() +
                                System.Environment.NewLine +
                                System.Environment.NewLine +
                                "*** لطفاً سهم های تعیین شده را کنترل نمایید. ***";

                        return false;
                    }
                }

                if (theOneRegCase.IsProportionateQuota == YesNo.Yes.GetString())
                {
                    bool dealQuotasAreEqualToRegCaseQuotas = this.VerifyProportionateCalculatedQuotas(theOneRegCase, ref message);
                    if (!dealQuotasAreEqualToRegCaseQuotas)
                        return false;
                }
            }

            return true;
        }

        private bool VerifyLinkageQuotas(DocumentEstate theOneRegCase, ref string message)
        {
            //if ( theOneRegCase.TheONotaryRegCasePersonQuotaList.CollectionHasElement () )
            //    return true;

            //foreach ( IONotaryRegCasePersonQuota theOneQuota in theOneRegCase.TheONotaryRegCasePersonQuotaList )
            //{
            //    bool isQuotaValid = this.VerifyIndividualQuota(theOneQuota, ref message);
            //    if ( !isQuotaValid )
            //        return false;
            //}

            return true;
        }

        private bool VerifyProportionateCalculatedQuotas(DocumentEstate theOneRegCase, ref string message)
        {
            foreach (DocumentEstateQuotum theOneDealQuota in theOneRegCase.DocumentEstateQuota)
            {
                if (!theOneRegCase.DocumentEstateQuotaDetails.Any())
                {
                    message =
                        "برای " +
                        theOneRegCase.RegCaseText() +
                        "، هیچ سهمی تعریف نشده است.";

                    return false;
                }

                #region Verify Calculated OwnerQuotas
                if (this.IsMovarres(theOneDealQuota.DocumentPerson) || theOneDealQuota.DocumentPerson.DocumentPersonType.IsOwner == YesNo.Yes.GetString())
                {
                    decimal[] ownersCalculatedTotalSellingQuota = null;
                    bool ownersAreValid = this.VerifyCalculatedOwnerSellingQuotas(theOneDealQuota, theOneRegCase, ref ownersCalculatedTotalSellingQuota, ref message);
                    if (!ownersAreValid)
                        return false;

                    if ((ownersCalculatedTotalSellingQuota[0] * theOneDealQuota.TotalQuota).TrimDoubleValue() != (ownersCalculatedTotalSellingQuota[1] * theOneDealQuota.DetailQuota.TrimDoubleValue()).TrimDoubleValue())
                    {
                        message =
                            "در " +
                            theOneRegCase.RegCaseText() + " " +
                            "مجموع سهم های محاسبه شده برای شخص انتقال دهنده با نام " +
                            theOneDealQuota.DocumentPerson.FullName() +
                            " با سهم وارد شده در بخش سهم متعاملین، مغایرت دارد. ";

                        return false;
                    }
                }
                #endregion

                #region Verify Calculated BuyerQuotas
                else if (theOneDealQuota.DocumentPerson.DocumentPersonType.IsOwner == YesNo.No.GetString())
                {
                    decimal[] buyersCalculatedTotalBuyingQuota = null;
                    bool buyersQuotasAreValid = this.VerifyCalculatedBuyerBuyingQuotas(theOneDealQuota, theOneRegCase, ref buyersCalculatedTotalBuyingQuota, ref message);
                    if (!buyersQuotasAreValid)
                        return false;

                    if ((buyersCalculatedTotalBuyingQuota[0] * theOneDealQuota.TotalQuota).TrimDoubleValue() != (buyersCalculatedTotalBuyingQuota[1] * theOneDealQuota.DetailQuota.TrimDoubleValue()).TrimDoubleValue())
                    {
                        message =
                            "در " +
                            theOneRegCase.RegCaseText() + " " +
                            "مجموع سهم های محاسبه شده برای شخص انتقال گیرنده با نام " +
                            theOneDealQuota.DocumentPerson.FullName() +
                            " با سهم وارد شده در بخش سهم متعاملین، مغایرت دارد. ";

                        return false;
                    }

                }
                #endregion

                #region InValid Person RegisterServicePersonType
                else
                {
                    message =
                        "با توجه به سمت وارد شده برای  " +
                        theOneDealQuota.DocumentPerson.FullName() +
                        "، تعریف سهم برای این شخص مجاز نمی باشد." +
                        System.Environment.NewLine +
                        "لطفاً از بخش سهم متعاملین، ردیف سهم وارد شده برای این شخص را حذف نموده و مجدداً تلاش نمایید.";

                    return false;
                }
                #endregion
            }

            return true;
        }

        private bool VerifyCalculatedOwnerSellingQuotas(DocumentEstateQuotum theSellerDealQuota, DocumentEstate theOneRegCase, ref decimal[] commonFraction, ref string message)
        {
            List<DocumentEstateQuotaDetail> theCurrentPersonRegCaseQuotasCollection = new List<DocumentEstateQuotaDetail>();

            foreach (DocumentEstateQuotaDetail theOneRegCasePersonQuota in theOneRegCase.DocumentEstateQuotaDetails)
            {
                //if ( theOneRegCasePersonQuota.IsMarkForDelete )
                //    continue;

                if (
                    theOneRegCasePersonQuota.DocumentPersonSeller.Id == theSellerDealQuota.DocumentPerson.Id ||
                    (theOneRegCasePersonQuota.DocumentPersonSeller.NationalNo == theSellerDealQuota.DocumentPerson.NationalNo && theOneRegCasePersonQuota.DocumentPersonSeller.FullName().GetStandardFarsiString() == theSellerDealQuota.DocumentPerson.FullName().GetStandardFarsiString())
                    )
                {
                    if (!theCurrentPersonRegCaseQuotasCollection.Contains(theOneRegCasePersonQuota))
                        theCurrentPersonRegCaseQuotasCollection.Add(theOneRegCasePersonQuota);
                }
            }

            if (!theCurrentPersonRegCaseQuotasCollection.Any())
            {
                message =
                    "در " +
                    theOneRegCase.RegCaseText() +
                    "، هیچ سهمی برای شخص " + theSellerDealQuota.DocumentPerson.FullName() +
                    " تعریف نشده است.";

                return false;
            }

            foreach (DocumentEstateQuotaDetail theOneRegCasePersonQuota in theCurrentPersonRegCaseQuotasCollection)
            {
                if (commonFraction == null)
                {
                    commonFraction = new decimal[2];
                    commonFraction[0] = theOneRegCasePersonQuota.SellDetailQuota == null ? 0 : (decimal)theOneRegCasePersonQuota?.SellDetailQuota.TrimDoubleValue();
                    commonFraction[1] = theOneRegCasePersonQuota.SellTotalQuota == null ? 0 : (decimal)theOneRegCasePersonQuota?.SellTotalQuota;
                }
                else
                {
                    commonFraction = Mathematics.MakhrajMoshtarak(commonFraction[0], theOneRegCasePersonQuota.SellDetailQuota.TrimDoubleValue() ?? 0m, commonFraction[1], (decimal)theOneRegCasePersonQuota.SellTotalQuota);
                }
            }

            if ((commonFraction[0] * theSellerDealQuota.TotalQuota).TrimDoubleValue() != (commonFraction[1] * theSellerDealQuota.DetailQuota.TrimDoubleValue()).TrimDoubleValue())
            {
                message =
                    "در " +
                    theOneRegCase.RegCaseText() +
                    " مجموع سهم های تعریف شده و سهم تعیین شده برای " +
                    theSellerDealQuota.DocumentPerson.FullName() +
                    " یکسان نیست. ";

                return false;
            }

            return true;
        }

        private bool VerifyCalculatedBuyerBuyingQuotas(DocumentEstateQuotum theBuyerDealQuota, DocumentEstate theOneRegCase, ref decimal[] commonFraction, ref string message)
        {
            List<DocumentEstateQuotaDetail> theCurrentPersonRegCaseQuotasCollection = new List<DocumentEstateQuotaDetail>();

            foreach (DocumentEstateQuotaDetail theOneRegCasePersonQuota in theOneRegCase.DocumentEstateQuotaDetails)
            {
                //if ( theOneRegCasePersonQuota.IsMarkForDelete )
                //    continue;

                if (theOneRegCasePersonQuota.DocumentPersonBuyerId == theBuyerDealQuota.DocumentPersonId)
                {
                    if (!theCurrentPersonRegCaseQuotasCollection.Contains(theOneRegCasePersonQuota))
                        theCurrentPersonRegCaseQuotasCollection.Add(theOneRegCasePersonQuota);
                }
            }

            if (!theCurrentPersonRegCaseQuotasCollection.Any())
            {
                message =
                    "در " +
                    theOneRegCase.RegCaseText() +
                    "، هیچ سهمی برای شخص" + theBuyerDealQuota.DocumentPerson.FullName() +
                    " تعریف نشده است.";

                return false;
            }

            foreach (DocumentEstateQuotaDetail theOneRegCasePersonQuota in theCurrentPersonRegCaseQuotasCollection)
            {
                if (commonFraction == null)
                {
                    commonFraction = new decimal[2];
                    commonFraction[0] = theOneRegCasePersonQuota.SellDetailQuota.TrimDoubleValue() ?? 0m;
                    commonFraction[1] = (decimal)theOneRegCasePersonQuota.SellTotalQuota;
                }
                else
                {
                    commonFraction = Mathematics.MakhrajMoshtarak(commonFraction[0], theOneRegCasePersonQuota.SellDetailQuota.TrimDoubleValue() ?? 0m, commonFraction[1], (decimal)theOneRegCasePersonQuota.SellTotalQuota);
                }
            }

            decimal buyerDealDetailQuota = ((decimal)theBuyerDealQuota.DetailQuota);
            decimal buyerDealTotalQuota = (decimal)theBuyerDealQuota.TotalQuota;

            if ((commonFraction[0] * buyerDealTotalQuota) != (commonFraction[1] * buyerDealDetailQuota))
            {
                message =
                    "در " +
                    theOneRegCase.RegCaseText() +
                    " مجموع سهم های تعریف شده و سهم تعیین شده برای " +
                    theBuyerDealQuota.DocumentPerson.FullName() +
                    " یکسان نیست. ";

                return false;
            }

            return true;
        }

        private void GetOriginalOwnershipValues(DocumentEstate theSelectedRegCase, DocumentEstateOwnershipDocument theSelectedOwnershipDoc, ref decimal? ownershipDetail, ref long? ownershipTotal, ref string ownershipContext)
        {
            if (theSelectedOwnershipDoc == null || theSelectedRegCase == null)
                return;

            if (!theSelectedRegCase.DocumentEstateQuotaDetails.Any())
                return;

            if (
                theSelectedRegCase.Document.DocumentType.WealthType == WealthType.Immovable.GetString() &&
                theSelectedRegCase.Document.IsRegistered != YesNo.No.GetString() &&
                theSelectedRegCase.Document.DocumentInquiries.Any()
                )
            {
                foreach (DocumentInquiry theOneInquiry in theSelectedRegCase.Document.DocumentInquiries)
                {
                    if (theOneInquiry.DocumentInquiryOrganizationId != "1")
                        continue;

                    if (string.IsNullOrWhiteSpace(theOneInquiry.EstateInquiriesId))
                        continue;

                    if (string.IsNullOrWhiteSpace(theSelectedOwnershipDoc.EstateInquiriesId))
                        continue;

                    if (theSelectedOwnershipDoc.EstateInquiriesId.Contains(theOneInquiry.EstateInquiriesId))
                    {
                        ownershipContext = !string.IsNullOrWhiteSpace(theOneInquiry.ReplyQuotaText) ? theOneInquiry.ReplyQuotaText.ToString() : null;
                        if (theOneInquiry.ReplyDetailQuota != null)
                            ownershipDetail = (decimal?)theOneInquiry.ReplyDetailQuota;
                        if (theOneInquiry.ReplyTotalQuota != null)
                            ownershipTotal = (long)theOneInquiry.ReplyTotalQuota;

                        if (ownershipContext != null || ownershipDetail != null || ownershipTotal != null)
                            return;
                    }
                }
            }
        }

        private decimal[] GetTotalSellerOriginalQuotas(DocumentEstate theOneRegCase, DocumentPerson theDealQuotaDocPerson, ref string message)
        {
            if (!theOneRegCase.DocumentEstateOwnershipDocuments.Any())
            {
                message = "هیچ مستندی برای " + theOneRegCase.RegCaseText() + "، تعریف نشده است.";
                return null;
            }

            List<string> inquiryIDsCollection = new List<string>();
            foreach (DocumentEstateOwnershipDocument theOneOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments)
            {
                if (string.IsNullOrWhiteSpace(theOneOwnershipDoc.EstateInquiriesId))
                {
                    message = "استعلامی برای " + theOneOwnershipDoc.OwnershipDocTitle() + " در پرونده یافت نشد.";
                    continue;
                }

                if (
                    theOneOwnershipDoc.DocumentPerson.Id == theDealQuotaDocPerson.Id ||
                    (theOneOwnershipDoc.DocumentPerson.NationalNo == theDealQuotaDocPerson.NationalNo && theOneOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString() == theDealQuotaDocPerson.FullName().GetStandardFarsiString())
                    )
                {
                    string inquiryID = theOneOwnershipDoc.EstateInquiriesId.Replace("@Duplicated", "");
                    if (!inquiryIDsCollection.Contains(inquiryID))
                        inquiryIDsCollection.Add(inquiryID);
                }
            }

            if (!inquiryIDsCollection.Any())
            {
                message =
                    theDealQuotaDocPerson.FullName +
                    " در " + theOneRegCase.Document.DocumentType.CaseTitle + " " +
                    System.Environment.NewLine +
                    theOneRegCase.RegCaseText() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    " دارای مالکیت نبوده و تعریف نمودن سهم در بخش سهم متعاملین برای این شخص صحیح نمی باشد.";

                return null;
            }

            decimal detailQuotasTotal = 0;
            decimal totalQuotasTotal = 0;

            decimal[] totalFraction = null;

            foreach (DocumentInquiry theOneInquiry in theOneRegCase.Document.DocumentInquiries)
            {
                if (!inquiryIDsCollection.Contains(theOneInquiry.EstateInquiriesId))
                    continue;

                if (theOneInquiry.ReplyDetailQuota != null && theOneInquiry.ReplyTotalQuota != null)
                {
                    if (detailQuotasTotal == 0 || totalQuotasTotal == 0 || totalFraction == null)
                    {
                        detailQuotasTotal = (decimal)theOneInquiry.ReplyDetailQuota;
                        totalQuotasTotal = (decimal)theOneInquiry.ReplyTotalQuota;
                        totalFraction = new decimal[2];
                        totalFraction[0] = detailQuotasTotal;
                        totalFraction[1] = totalQuotasTotal;
                        continue;
                    }

                    totalFraction = Mathematics.MakhrajMoshtarak(detailQuotasTotal, (decimal)theOneInquiry.ReplyDetailQuota, totalQuotasTotal, (decimal)theOneInquiry.ReplyTotalQuota);
                    if (totalFraction != null)
                    {
                        detailQuotasTotal = totalFraction[0];
                        totalQuotasTotal = totalFraction[1];
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(theOneInquiry.ReplyQuotaText))
                    {
                        message = "استعلام شماره " + theOneInquiry.RequestNo + " دارای متن سهم بوده و محاسبه حسب السهم بر اساس این استعلام امکان پذیر نمی باشد. ";
                        return null;
                    }
                }
            }

            return totalFraction;
        }

        private bool IsMovarres(DocumentPerson theOneDocPerson)
        {
            if (theOneDocPerson.DocumentPersonTypeId == null)
            {
                if (theOneDocPerson.DocumentPersonRelatedAgentPeople != null && theOneDocPerson.DocumentPersonRelatedAgentPeople.Count > 0)
                {
                    foreach (DocumentPersonRelated theOneDocAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople)
                    {
                        if (theOneDocAgent.AgentTypeId == "9")
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
        }

        private bool buyerQuotaHasDefined(Document theCurrentRegisterServiceReq, DocumentPerson theOneDocPerson)
        {
            if (!theCurrentRegisterServiceReq.DocumentEstates.Any())
                return false;

            bool buyerHasQuota = false;

            foreach (DocumentEstate theOneRegCase in theCurrentRegisterServiceReq.DocumentEstates)
            {
                if (!theOneRegCase.DocumentEstateQuotaDetails.Any())
                    continue;

                foreach (DocumentEstateQuotaDetail theOneQuota in theOneRegCase.DocumentEstateQuotaDetails)
                {
                    if (theOneQuota.DocumentPersonBuyerId == null)
                        continue;

                    if (
                        theOneQuota.DocumentPersonBuyer.Id == theOneDocPerson.Id ||
                        (theOneQuota.DocumentPersonBuyer.NationalNo == theOneDocPerson.NationalNo && theOneQuota.DocumentPersonBuyer.FullName().GetStandardFarsiString() == theOneDocPerson.FullName().GetStandardFarsiString())
                        )
                        return true;
                }
            }

            return buyerHasQuota;
        }
    }

}
