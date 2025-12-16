using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using DocumentEstate = Notary.SSAA.BO.Domain.Entities.DocumentEstate;
using DocumentEstateOwnershipDocument = Notary.SSAA.BO.Domain.Entities.DocumentEstateOwnershipDocument;
using DocumentEstateQuotaDetails = Notary.SSAA.BO.Domain.Entities.DocumentEstateQuotaDetail;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Other;
using DocumentInquiry = Notary.SSAA.BO.Domain.Entities.DocumentInquiry;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons.EstateInquiryManager;
using DocumentPerson = Notary.SSAA.BO.Domain.Entities.DocumentPerson;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class QuotasGeneratorEngine
    {
        private readonly IDocumentRepository _documentRepository;

        public QuotasGeneratorEngine( IDocumentRepository documentRepository )
        {
            _documentRepository = documentRepository;
        }

        private DSUConditionTextBackupEngine _dsuConditionTextBackupEngine = null;
        internal bool GeneratePersonQuotas ( ref Notary.SSAA.BO.Domain.Entities.Document theCurrentRegisterServiceReq, ref string messages )
        {
            bool isGenerated = false;

            _dsuConditionTextBackupEngine = new DSUConditionTextBackupEngine ();
            var currentQuotaListForDeleteList = new List<Notary.SSAA.BO.Domain.Entities.DocumentEstateQuotaDetail>();
            foreach ( Notary.SSAA.BO.Domain.Entities.DocumentEstate theOneRegCase in theCurrentRegisterServiceReq.DocumentEstates )
            {
                if ( theOneRegCase.IsProportionateQuota != YesNo.Yes.GetString() || string.IsNullOrWhiteSpace ( theOneRegCase.EstateInquiryId ) )
                    continue;

                //Delete All Recent Quotas
                //theOneRegCase.TheONotaryRegCasePersonQuotaList.DeleteCompletely();
                int quotasCount = 0;

                if ( theOneRegCase.DocumentEstateQuotaDetails!=null && theOneRegCase.DocumentEstateQuotaDetails.Count>0)
                {
                    quotasCount = theOneRegCase.DocumentEstateQuotaDetails.Count;

                    for ( int i = quotasCount - 1; i >= 0; i-- )
                    {
                        //if (!(theOneRegCase.DocumentEstateQuotaDetails != null && theOneRegCase.DocumentEstateQuotaDetails.Count > 0))
                        //    break;

                        Notary.SSAA.BO.Domain.Entities.DocumentEstateQuotaDetail currentQuota = theOneRegCase.DocumentEstateQuotaDetails.ElementAt(i) ;
                        if ( currentQuota == null )
                            continue;

                        _dsuConditionTextBackupEngine.Backup ( currentQuota.DealSummaryPersonConditions, currentQuota.DocumentEstateId.ToString(), currentQuota.DocumentEstateOwnershipDocumentId?.ToString(), currentQuota.DocumentPersonBuyerId?.ToString() );
                        if ( currentQuota.DocumentPersonSeller != null && currentQuota.DocumentPersonSeller.DocumentEstateQuotaDetailDocumentPersonSellers != null )
                            currentQuota.DocumentPersonSeller.DocumentEstateQuotaDetailDocumentPersonSellers.Remove ( currentQuota );
                        if ( currentQuota.DocumentPersonSeller != null && currentQuota.DocumentPersonSeller.DocumentEstateQuotaDetailDocumentPersonSellers != null )
                            currentQuota.DocumentPersonSeller.DocumentEstateQuotaDetailDocumentPersonSellers.Remove ( currentQuota );
                        if ( currentQuota.DocumentPersonBuyer != null && currentQuota.DocumentPersonBuyer.DocumentEstateQuotaDetailDocumentPersonBuyers != null )
                            currentQuota.DocumentPersonBuyer.DocumentEstateQuotaDetailDocumentPersonBuyers.Remove ( currentQuota );
                        if ( currentQuota.DocumentPersonBuyer != null && currentQuota.DocumentPersonBuyer.DocumentEstateQuotaDetailDocumentPersonBuyers != null )
                            currentQuota.DocumentPersonBuyer.DocumentEstateQuotaDetailDocumentPersonBuyers.Remove ( currentQuota );
                        currentQuotaListForDeleteList.Add(currentQuota);
                       // currentQuota.MarkForDelete ();
                        if ( theOneRegCase != null && theOneRegCase.DocumentEstateQuotaDetails != null )
                            theOneRegCase.DocumentEstateQuotaDetails.Remove ( currentQuota );

                        foreach ( var item in theOneRegCase.DocumentEstateOwnershipDocuments )
                        {
                            item.DocumentEstateQuotaDetails.Remove ( currentQuota );
                        }
                    }
                }
                List<DocumentEstateQuotaDetail> documentEstateQuotaDetails = new List<DocumentEstateQuotaDetail>();

                //generate quotas for each case
                (isGenerated,documentEstateQuotaDetails) = this.GeneratePersonQuotasForEachCase (  theOneRegCase, ref messages );
               
                


                if ( !isGenerated )
                    return false;
                else
                {
                    theOneRegCase.DocumentEstateQuotaDetails = documentEstateQuotaDetails;
                }

            }

            return isGenerated;
        }

        //private Document GetONotaryRegisterServiceReq ( string objectID )

        //{
        //    Document=_documentRepository.GetDocumentById(Guid.Parse( objectID ),new List<string>(){},null )
        //    IONotaryRegisterServiceReq theCurrentRegisterServiceReq = Rad.CMS.InstanceBuilder.GetEntityById<IONotaryRegisterServiceReq>(objectID);

        //    return theCurrentRegisterServiceReq;
        //}

        private (bool, List<DocumentEstateQuotaDetail>) GeneratePersonQuotasForEachCase (  DocumentEstate theOneRegCase, ref string messages )
        {
            bool isGenerated = false;
            List<DocumentEstateQuotaDetail> documentEstateQuotaDetails = new List<DocumentEstateQuotaDetail>();

            foreach (var theOneOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments )
            {
                //generate person quotas for OwnershipDocs
                (isGenerated, documentEstateQuotaDetails) = this.GeneratePersonQuotasForEachOwnershipDoc ( theOneOwnershipDoc, theOneRegCase.DocumentEstateQuota, ref messages );
                if ( !isGenerated )
                    return (false,null);
            }

            return (isGenerated, documentEstateQuotaDetails);
        }

        private (bool, List<DocumentEstateQuotaDetail>) GeneratePersonQuotasForEachOwnershipDoc ( DocumentEstateOwnershipDocument theOneOwnershipDoc, ICollection<DocumentEstateQuotum> theDealPersonQuotasCollection, ref string messages )
        {
            //for each ownershipdoc a collection of PersonQuotas should be generated based on DealQuotas
            if ( !theDealPersonQuotasCollection.Any())
            {
                messages = "برای مورد معامله " + theOneOwnershipDoc.DocumentEstate.RegCaseText() + "، سهم های مورد معامله اشخاص تعیین نشده است.";
                return (false,null);
            }

            List<DocumentEstateQuotum> theBuyersDealQuotasCollection = new List<DocumentEstateQuotum>();
            List<DocumentEstateQuotum> theSellersDealQuotasCollection = new List<DocumentEstateQuotum>();
            DocumentEstateQuotum theCurentSellerDealQuota = null;
            bool isObjectChanged = false;

            #region Collect Buyers DealQuotas, And The Equivalant SellerDealQuota
            foreach ( DocumentEstateQuotum theOneDealPersonQuota in theDealPersonQuotasCollection )
            {
                if ( theOneDealPersonQuota.DocumentPerson.DocumentPersonType != null )
                {
                    if ( theOneDealPersonQuota.DocumentPerson.DocumentPersonType.IsOwner ==YesNo.No.GetString() )
                    {
                        if ( !theBuyersDealQuotasCollection.Contains ( theOneDealPersonQuota ) )
                            theBuyersDealQuotasCollection.Add ( theOneDealPersonQuota );
                    }
                    else
                    {
                        if ( !theSellersDealQuotasCollection.Contains ( theOneDealPersonQuota ) )
                            theSellersDealQuotasCollection.Add ( theOneDealPersonQuota );

                        if (
                            theOneDealPersonQuota.DocumentPerson.Id == theOneOwnershipDoc.DocumentPerson.Id ||
                            ( theOneDealPersonQuota.DocumentPerson.NationalNo == theOneOwnershipDoc.DocumentPerson.NationalNo && theOneDealPersonQuota.DocumentPerson.FullName().GetStandardFarsiString() == theOneOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString () )
                            )
                            theCurentSellerDealQuota = theOneDealPersonQuota;
                    }
                }
                else
                {
                    if ( !theSellersDealQuotasCollection.Contains ( theOneDealPersonQuota ) )
                        theSellersDealQuotasCollection.Add ( theOneDealPersonQuota );

                    if (
                        theOneDealPersonQuota.DocumentPerson.Id == theOneOwnershipDoc.DocumentPerson.Id ||
                        ( theOneDealPersonQuota.DocumentPerson.NationalNo == theOneOwnershipDoc.DocumentPerson.NationalNo && theOneDealPersonQuota.DocumentPerson.FullName().GetStandardFarsiString () == theOneOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString () )
                        )
                        theCurentSellerDealQuota = theOneDealPersonQuota;
                }

            }

            if ( theCurentSellerDealQuota == null )
            {
                messages =
                    "خطا در محاسبه سهم ها بصورت حسب السهم!" +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "- جزئیات خطا : " +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    " * " + theOneOwnershipDoc.DocumentEstate.Document.DocumentType.CaseTitle + " : " +
                    System.Environment.NewLine +
                    theOneOwnershipDoc.DocumentEstate.RegCaseText() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    " * مستند مالکیت :" +
                    System.Environment.NewLine +
                    theOneOwnershipDoc.OwnershipDocTitle() +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    "برای شخص " + theOneOwnershipDoc.DocumentPerson.FullName() + " هیچ سهم معادلی در بخش سهم متعاملین یافت نشد." +
                    System.Environment.NewLine +
                    "**لطفاً سهم شخص مذکور را در بخش سهم متعاملین بررسی نموده و مجدداً تلاش نمایید.**";

                return (false,null);
            }
            #endregion

            #region CommonDenumerator
            decimal[] multiplierFraction = null;
            decimal[] commonInverseMultiplierFraction = null;
            decimal? commonDenumerator = null;
            bool commonDenumeratorNeeded = true;
            //If All Sellers' Ownership fractions differ from each other(Denumerators Are Not Equal),
            //it's needed to make "CommonDenumerator" and making a mulriplier for all ownership fractions
            //****** We Call this multiplier as "commonInverseMultiplierFraction"
            //the Inverse Keyword is caused by which it's needed to Inverse the MultiplierFraction before multiplying in other fractions
            //foreach (IONotaryDealPersonQuota theOneSellerDealQuota in theSellersDealQuotasCollection)
            //{
            //    if (commonDenumerator == null)
            //        commonDenumerator = theOneSellerDealQuota.TotalQuota;

            //    if (commonDenumerator != theOneSellerDealQuota.TotalQuota)
            //    {
            //        commonDenumeratorNeeded = true;
            //        break;
            //    }
            //}

            //if ( commonDenumeratorNeeded )
            //{
                foreach ( DocumentEstateQuotum theOneSellerDealQuota in theSellersDealQuotasCollection )
                {
                    if ( multiplierFraction == null )
                    {
                        multiplierFraction = new decimal [ 2 ];
                        multiplierFraction [ 0 ] = ( decimal ) theOneSellerDealQuota.DetailQuota;
                        multiplierFraction [ 1 ] = ( decimal ) theOneSellerDealQuota.TotalQuota;
                    }
                    else
                    {
                        multiplierFraction =Mathematics.MakhrajMoshtarak ( multiplierFraction [ 0 ], ( decimal ) theOneSellerDealQuota.DetailQuota, multiplierFraction [ 1 ], ( decimal ) theOneSellerDealQuota.TotalQuota );
                    }
                }

                commonInverseMultiplierFraction = new decimal [ 2 ];
                commonInverseMultiplierFraction =Mathematics.InverseFraction ( multiplierFraction );
                commonInverseMultiplierFraction =Mathematics.SimplifyFraction ( commonInverseMultiplierFraction );
            //}
            #endregion

            #region Generate Quotas
            List< DocumentEstateQuotaDetail> documentEstateQuotaDetailList=new List< DocumentEstateQuotaDetail >();

            foreach ( DocumentEstateQuotum theOneBuyerDealQuota in theBuyersDealQuotasCollection )
            {

                if ( !string.IsNullOrEmpty ( theOneBuyerDealQuota.QuotaText ) )
                    continue;

                DocumentEstateQuotaDetail theNewPersonQuota = null;

                try
                {
                    theNewPersonQuota = new DocumentEstateQuotaDetail ();
                    theNewPersonQuota.Id = Guid.NewGuid();
                    theNewPersonQuota.ScriptoriumId = theOneOwnershipDoc.ScriptoriumId;
                    theNewPersonQuota.Ilm = "1";
                    theNewPersonQuota.DocumentEstateOwnershipDocument = theOneOwnershipDoc;
                    theNewPersonQuota.DocumentEstateOwnershipDocumentId = theOneOwnershipDoc.Id;
                    theNewPersonQuota.DocumentEstateId = theOneOwnershipDoc.DocumentEstateId;
                    theNewPersonQuota.DocumentPersonSellerId = theOneOwnershipDoc.DocumentPersonId;
                    theNewPersonQuota.DocumentPersonSeller = theOneOwnershipDoc.DocumentPerson;
                    theNewPersonQuota.DocumentPersonBuyer = theOneBuyerDealQuota.DocumentPerson;
                    theNewPersonQuota.DocumentPersonBuyerId = theOneBuyerDealQuota.DocumentPersonId;
                    theNewPersonQuota.DealSummaryPersonConditions = _dsuConditionTextBackupEngine.Restore ( theOneOwnershipDoc.DocumentEstateId.ToString(), theOneOwnershipDoc.Id.ToString(), theOneBuyerDealQuota.DocumentPersonId.ToString() );

                    DocumentInquiry theCurrentInquiry = this.GetCorrespondingInquiryOfCurrentOwnershipDoc(theOneOwnershipDoc, ref messages);
                    if ( theCurrentInquiry == null )
                    {
                        messages = "هیچ استعلام معادلی برای ایجاد سهم شخص جاری، یافت نشد.";
                        return (false,null);
                    }

                    decimal? currentInquiryDetail = null;
                    decimal? currentInquiryTotal = null; // theCurrentInquiry.ReplyShareTotal.Value;

                    if ( theCurrentInquiry.ReplyDetailQuota.HasValue )
                        currentInquiryDetail = theCurrentInquiry.ReplyDetailQuota.Value;
                    //currentInquiryDetail = NotaryOfficeCommons.Mathematics.Mathematics.FormatDoubleDecimalPlaces(theCurrentInquiry.ReplySharePart.Value);

                    if ( theCurrentInquiry.ReplyTotalQuota.HasValue )
                        currentInquiryTotal = theCurrentInquiry.ReplyTotalQuota.Value;

                    //در اسناد قطعی همه محاسبات بر مبنای استعلام است. اما در اسناد رهنی مبنا میزان فروخته شده سهام می باشد.
                    //بنابراین در اسناد قطعی اگر استعلام دارای متن باشد، حسب السهم بر اساس استعلام معنایی ندارد.
                    if (
                        !string.IsNullOrWhiteSpace ( theCurrentInquiry.ReplyQuotaText ) &&
                        !Mapper.IsONotaryDocumentRestrictedType( theOneOwnershipDoc.DocumentEstate.Document.DocumentTypeId )
                        )
                    {
                        if ( currentInquiryDetail == null || currentInquiryDetail == 0 || currentInquiryTotal == null || currentInquiryTotal == 0 )
                        {
                            messages = "استعلام شماره " + theCurrentInquiry.RequestNo + " دارای جزء و کل سهم معتبر نمی باشد و محاسبه حسب السهم بر اساس این استعلام امکان پذیر نیست.";
                            return (false,null);
                        }
                    }

                    decimal[] currentInquiryFraction = new decimal[2];
                    currentInquiryFraction [ 0 ] = currentInquiryDetail !=null? ( decimal ) currentInquiryDetail:(decimal)0.0;
                    currentInquiryFraction [ 1 ] = currentInquiryTotal != null ? (decimal)currentInquiryTotal : (decimal)0.0; 

                    decimal[] summarizedInquiries = this.GetSummarizedInquiryQuotasOfPerson(theOneOwnershipDoc.DocumentPerson, theOneOwnershipDoc.DocumentEstate);
                    decimal[] inversedSummarizedInquiries = Mathematics.InverseFraction(summarizedInquiries);

                    //decimal? calculatedSellDetail = null;
                    //decimal? calculatedSellTotal = null;
                    decimal[] calculatedSellFraction = new decimal[2];

                    #region Quota Values Calculation
                    //Based On The Formula specified in Designed-Algorithms
                    //if ( !commonDenumeratorNeeded )
                    //{
                    //    theNewPersonQuota.OwnershipDetailQuota = currentInquiryDetail;
                    //    theNewPersonQuota.OwnershipTotalQuota = ( long ) currentInquiryTotal;

                    //    calculatedSellFraction [ 0 ] = ( decimal ) ( theOneBuyerDealQuota.DetailQuota * theCurentSellerDealQuota.DetailQuota * currentInquiryDetail * inversedSummarizedInquiries [ 0 ] );
                    //    calculatedSellFraction [ 1 ] = ( decimal ) ( theOneBuyerDealQuota.TotalQuota * theCurentSellerDealQuota.TotalQuota * currentInquiryTotal * inversedSummarizedInquiries [ 1 ] );
                    //}
                    //else
                    //{
                        string dealerName = theOneBuyerDealQuota.DocumentPerson.FullName();
                        string sellerName = theCurentSellerDealQuota.DocumentPerson.FullName();

                        calculatedSellFraction [ 0 ] = theOneBuyerDealQuota!=null?(theOneBuyerDealQuota.DetailQuota!=null? ( decimal ) theOneBuyerDealQuota.DetailQuota:(decimal)0.0) : (decimal)0.0;
                        calculatedSellFraction [ 1 ] = theOneBuyerDealQuota != null ? (theOneBuyerDealQuota.TotalQuota != null ? (decimal)theOneBuyerDealQuota.TotalQuota : (decimal)0.0) : (decimal)0.0;
                        calculatedSellFraction =Mathematics.SimplifyFraction ( calculatedSellFraction );

                        decimal[] theCurentSellerDealQuotaFraction = new decimal[2];
                        theCurentSellerDealQuotaFraction [ 0 ] = theCurentSellerDealQuota != null ? (theCurentSellerDealQuota.DetailQuota != null ? (decimal)theCurentSellerDealQuota.DetailQuota : (decimal)0.0) : (decimal)0.0; 
                        theCurentSellerDealQuotaFraction [ 1 ] = theCurentSellerDealQuota != null ? (theCurentSellerDealQuota.TotalQuota != null ? (decimal)theCurentSellerDealQuota.TotalQuota : (decimal)0.0) : (decimal)0.0; 
                        theCurentSellerDealQuotaFraction = Mathematics.SimplifyFraction ( theCurentSellerDealQuotaFraction );

                        if (
                            theCurentSellerDealQuotaFraction [ 0 ] != commonInverseMultiplierFraction [ 1 ] ||
                            theCurentSellerDealQuotaFraction [ 1 ] != commonInverseMultiplierFraction [ 0 ]
                            )
                        {
                            calculatedSellFraction [ 0 ] = ( decimal ) ( calculatedSellFraction [ 0 ] * theCurentSellerDealQuotaFraction [ 0 ] * commonInverseMultiplierFraction [ 0 ] );
                            calculatedSellFraction [ 1 ] = ( decimal ) ( calculatedSellFraction [ 1 ] * theCurentSellerDealQuotaFraction [ 1 ] * commonInverseMultiplierFraction [ 1 ] );
                            calculatedSellFraction = Mathematics.SimplifyFraction ( calculatedSellFraction );
                        }

                        //calculatedSellFraction[0] = (decimal)(calculatedSellFraction[0] * currentInquiryDetail).TrimDoubleValue();
                        //calculatedSellFraction[1] = (decimal)(calculatedSellFraction[1] * currentInquiryTotal).TrimDoubleValue();

                        if (
                            inversedSummarizedInquiries [ 0 ] != currentInquiryTotal ||
                            inversedSummarizedInquiries [ 1 ] != currentInquiryDetail
                            )
                        {
                        if (currentInquiryDetail != null && currentInquiryTotal.HasValue)
                        {
                            calculatedSellFraction [ 0 ] = ( decimal ) ( calculatedSellFraction [ 0 ] * inversedSummarizedInquiries [ 0 ] * currentInquiryDetail ).TrimDoubleValue ();

                        }
                        else
                        {
                            calculatedSellFraction[0] = (decimal)0.0;
                        }
                        if (currentInquiryTotal!=null && currentInquiryTotal.HasValue && inversedSummarizedInquiries.Length>1 && calculatedSellFraction.Length>1)
                        {
                            calculatedSellFraction [ 1 ] = ( decimal ) ( calculatedSellFraction [ 1 ] * inversedSummarizedInquiries [ 1 ] * currentInquiryTotal ).TrimDoubleValue ();

                        }
                        else
                        {
                            calculatedSellFraction[1]=(decimal)0.0;
                        }
                        }
                    //}
                    #endregion

                    calculatedSellFraction = Mathematics.SimplifyFraction ( calculatedSellFraction, currentInquiryFraction );
                    calculatedSellFraction = Mathematics.RemoveDecimalType ( calculatedSellFraction );

                    theNewPersonQuota.SellDetailQuota = calculatedSellFraction [ 0 ];
                    theNewPersonQuota.SellTotalQuota = ( long ) calculatedSellFraction [ 1 ];


                    if (Mapper.IsONotaryDocumentRestrictedType ( theOneOwnershipDoc.DocumentEstate.Document.DocumentTypeId ) )
                    {
                        theNewPersonQuota.OwnershipDetailQuota = theCurentSellerDealQuota!=null ? theCurentSellerDealQuota.DetailQuota : (decimal)0.0; 
                        theNewPersonQuota.OwnershipTotalQuota = theCurentSellerDealQuota != null ? theCurentSellerDealQuota.TotalQuota : (decimal)0.0; 
                    }
                    else
                    {
                        theNewPersonQuota.OwnershipDetailQuota = currentInquiryDetail;
                        theNewPersonQuota.OwnershipTotalQuota =  currentInquiryTotal;
                    }

                    if (
                        !theNewPersonQuota.SellDetailQuota.IsQuotaDetailValueAllowed () ||
                        !theNewPersonQuota.SellTotalQuota.IsQuotaTotalValueAllowed () ||
                        !theNewPersonQuota.OwnershipDetailQuota.IsQuotaDetailValueAllowed () ||
                        !theNewPersonQuota.OwnershipTotalQuota.IsQuotaTotalValueAllowed () )
                    {
                        messages =
                            "با توجه به اعداد سهم های وارد شده، امکان محاسبه حسب السهم بصورت خودکار وجود ندارد." +
                            System.Environment.NewLine +
                            "لطفاً سهم بندی را بدون استفاده از گزینه حسب السهم و بصورت دستی محاسبه نموده و در بخش سهم هر خریدار از فروشنده درج نمایید.";

                       // theNewPersonQuota.MarkForDelete();
                        return (false,null);
                    }
                    documentEstateQuotaDetailList.Add( theNewPersonQuota );
                    isObjectChanged = true;

                }
                catch ( Exception ex )
                {
                    if ( theNewPersonQuota != null )
                       // theNewPersonQuota.MarkForDelete ();

                    isObjectChanged = false;
                }
            }
            #endregion

            return (isObjectChanged, documentEstateQuotaDetailList);
        }

        private List<DocumentEstateOwnershipDocument> CollectOwnerPersonOwnershipDocs ( DocumentPerson theOwnerPerson, DocumentEstate theCurrentRegCase )
        {
            List<DocumentEstateOwnershipDocument> ownershipDocsCollection = null;

            foreach ( DocumentEstateOwnershipDocument theOneOwnershipDoc in theCurrentRegCase.DocumentEstateOwnershipDocuments )
            {
                if ( string.IsNullOrWhiteSpace ( theOneOwnershipDoc.EstateInquiriesId ) )
                    continue;

                if ( theOneOwnershipDoc.DocumentPersonId != theOwnerPerson.Id )
                    continue;

                if ( ownershipDocsCollection == null )
                    ownershipDocsCollection = new List<DocumentEstateOwnershipDocument> ();


                if ( !ownershipDocsCollection.Contains ( theOneOwnershipDoc ) )
                    ownershipDocsCollection.Add ( theOneOwnershipDoc );
            }

            //foreach (IONotaryRegCasePersonQuota theOneQuota in theOwnerPerson.TheONotaryRegCasePersonQuotaList)
            //{
            //    if (theOneQuota.TheONotaryPersonOwnershipDoc == null)
            //        continue;

            //    if (theOneQuota.ONotaryRegCaseId != theCurrentRegCase.ObjectId)
            //        continue;

            //    if (ownershipDocsCollection == null)
            //        ownershipDocsCollection = new List<IONotaryPersonOwnershipDoc>();

            //    if (!ownershipDocsCollection.Contains(theOneQuota.TheONotaryPersonOwnershipDoc))
            //        ownershipDocsCollection.Add(theOneQuota.TheONotaryPersonOwnershipDoc);
            //}

            return ownershipDocsCollection;
        }

        private List<DocumentInquiry> CollectCorrespondingInquiries ( DocumentPerson theOneDocPerson, DocumentEstate theCurrentRegCase )
        {
            List<DocumentEstateOwnershipDocument> ownershipDocsCollection = this.CollectOwnerPersonOwnershipDocs(theOneDocPerson, theCurrentRegCase);

            List<DocumentInquiry> inquiriesCollection = null;
            List<string> inquiriesIDsCollection = null;

            if (
                theOneDocPerson.Document.DocumentInquiries == null ||
                theOneDocPerson.Document.DocumentInquiries.Count < 1 ||
                ownershipDocsCollection == null || ownershipDocsCollection.Count == 0
                )
                return null;

            foreach ( DocumentEstateOwnershipDocument theOnePersonOwnershipDoc in ownershipDocsCollection )
            {
                if ( theOnePersonOwnershipDoc == null )
                    continue;

                if ( string.IsNullOrEmpty ( theOnePersonOwnershipDoc.EstateInquiriesId ) )
                    continue;

                string inquiryID = theOnePersonOwnershipDoc.EstateInquiriesId;
                if ( inquiryID.Contains ( "@Duplicated" ) )
                    inquiryID = inquiryID.Replace ( "@Duplicated", "" );

                if ( inquiriesIDsCollection == null )
                    inquiriesIDsCollection = new List<string> ();

                if ( !inquiriesIDsCollection.Contains ( inquiryID ) )
                    inquiriesIDsCollection.Add ( inquiryID );
            }

            foreach ( DocumentInquiry theOneInquiry in theOneDocPerson.Document.DocumentInquiries )
            {
                if ( string.IsNullOrEmpty ( theOneInquiry.EstateInquiriesId ) )
                    continue;

                if ( inquiriesIDsCollection != null )
                    if ( inquiriesIDsCollection.Count > 0 )
                        if ( !inquiriesIDsCollection.Contains ( theOneInquiry.EstateInquiriesId ) )
                            continue;

                if ( inquiriesCollection == null )
                    inquiriesCollection = new List<DocumentInquiry> ();

                if ( !inquiriesCollection.Contains ( theOneInquiry ) )
                    inquiriesCollection.Add ( theOneInquiry );
            }

            return inquiriesCollection;
        }

        private DocumentInquiry GetCorrespondingInquiryOfCurrentOwnershipDoc ( DocumentEstateOwnershipDocument theOneOwnershipDoc, ref string messages )
        {
            if ( !theOneOwnershipDoc.DocumentEstate.Document.DocumentInquiries.Any() )
            {
                messages = "هیچ استعلامی در سند یافت نشد.";
                return null;
            }


            string inquiryID = theOneOwnershipDoc.EstateInquiriesId;
            if ( inquiryID.Contains ( "@Duplicated" ) )
                inquiryID = inquiryID.Replace ( "@Duplicated", "" );

            foreach ( DocumentInquiry theOneInquiry in theOneOwnershipDoc.DocumentEstate.Document.DocumentInquiries )
            {
                if ( string.IsNullOrWhiteSpace ( theOneInquiry.EstateInquiriesId ) )
                    continue;

                if ( theOneInquiry.EstateInquiriesId.Contains ( inquiryID ) )
                    return theOneInquiry;
            }

            return null;
        }

        private decimal [ ] GetSummarizedInquiryQuotasOfPerson ( DocumentPerson? theOneDocPerson, DocumentEstate theCurrentRegCase )
        {
            List<DocumentInquiry> correspondingInquiriesOfCurrentPerson = this.CollectCorrespondingInquiries(theOneDocPerson, theCurrentRegCase);
            if (correspondingInquiriesOfCurrentPerson==null|| !correspondingInquiriesOfCurrentPerson.Any() )
                return null;

            decimal[] multiplierFraction = null;
            foreach ( DocumentInquiry theOneInquiry in correspondingInquiriesOfCurrentPerson )
            {
                if ( multiplierFraction == null )
                {
                    multiplierFraction = new decimal [ 2 ];
                    multiplierFraction [ 0 ] = theOneInquiry.ReplyDetailQuota != null ? (decimal)theOneInquiry.ReplyDetailQuota : (decimal)0.0;
                    multiplierFraction [ 1 ] = theOneInquiry.ReplyTotalQuota != null ? (decimal)theOneInquiry.ReplyTotalQuota : (decimal)0.0; 
                }
                else
                {
                   decimal? quota = theOneInquiry.ReplyDetailQuota != null ? (decimal?)theOneInquiry.ReplyDetailQuota : (decimal?)0.0;
                    var quotaTrim = quota.TrimDoubleValue();
                   var ReplyDetailQuotaValue = quotaTrim == null?(decimal)0.0:(decimal)quotaTrim;
                    var ReplyTotalQuotaValue =   theOneInquiry.ReplyTotalQuota != null ? (decimal)theOneInquiry.ReplyTotalQuota : (decimal)0.0;
                    multiplierFraction =Mathematics. MakhrajMoshtarak ( multiplierFraction [ 0 ], ReplyDetailQuotaValue, multiplierFraction [ 1 ], ReplyTotalQuotaValue);
                }
            }

            return multiplierFraction;
        }

    }


}
