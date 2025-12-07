using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using DocumentType = Notary.SSAA.BO.Domain.Entities.DocumentType;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons
{
    public static class DsuUtility
    {
        private static string[] _ExceptionReqs = new string[]
        {
            "575a3d91359b49e89c8815a387f5fda7", //139341252939000019 // پرونده رهنی که با سند قطعس تطابق ندارد و شناسه یکتا هم گرفته است.
            "6bc674ba9c344bcbbb0c9317b99f4d55", //139341552539000005 //عدم تطابق اشخاص با سند قطعی مربوطه 
            "f864ec15b4094c7bbcc6df138f3a3213", //139341751994000096 //عدم تطابق نام بعد از شناسه یکتا
            "91b5b500c44246408b6b34a0db3a6735",
            "1bbf49a5526b4cc5ad2610cf058db3c6",
            "7b9cfbf947cb4e56ae6ed8814a532916",
            "89bbc90a6b854aa59e1853e404a315d7",
            "9b0fcc5c8a5b481aba71b796676583c1",
            "dda94bade68a42ddbfeca6471929937c",
            "90b60925e2634a1692b02e8893d2e6a9",
            "8af777f69f974b1aa972b57d9407e897",
            "ec7b0270b31349e49af462447e25dbb7",
            "3af44fcd125a4c9c865891fffde1040b",
            "423d1e917a34499a91e41d44ca715373",
            "fb0d6f0dd1894bf6b15ad6738d6550a1",
            "0968fd562088427eb5c57019580b3ae9",
            "e2970bc0d8dc43f2a5bb9e8c0caacf94",
            "a1dac5735cb342e0816e4c98fc9e64c5",
            "412fe838f72b46799b3274bcaa6dc5f2",
            "7dc6c5c6dcb14ff9bf7961bbcc687e15",
            "6a0ddfaa4a47472e95afef268d80e028",
            "255add0e8786429c88b3805cd6deab4e",
            "1a0afbbe98524888af854493cb952be9",
            "26f992840e4647cabc2482c803517f9c",
            "7f2ff702dc2f42d5b6fa910d955bee67",
            "86635f863ecc4222a7cb628b414f3163","82919ed7975247949a0e7a9f704a7cb0" , "c8b63f566da74c15920acf14de88cdf0" ,"b9f0985b84694e4fa64657b877066ade" , "adf084da59784bb3954dd81d7142e759" //شخص خارجی در سند رهنی هیچ مشخصات هویتی خاصی ندارد . 1393/08/04 اعلام توسط مهندس فراهانی
        };

        public static bool IsDSUGeneratingPermitted ( ref string messages, Document theCurrentNotaryRegisterServiceReqEntity,List<DocumentInquiryInformation>? documentInquiriesInformation,bool isReadFromDocument=false, ClientConfiguration clientConfiguration = null )
        {
         

            if (
                ( theCurrentNotaryRegisterServiceReqEntity.DocumentType.IsSupportive == YesNo.Yes.GetString () && theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId!= "004" ) || // خدمات ثبتی به استثناء فک رهن
                theCurrentNotaryRegisterServiceReqEntity.DocumentType.WealthType != YesNo.No.GetString() || // سند اموال غیر منقول                
                theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId == "311" || // سند وكالت فروش اموال غيرمنقول نباشد
                theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId == "333" || // تفويض وكالت فروش اموال غيرمنقول
                theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId == "322" || // سند وكالت کاری اموال غيرمنقول نباشد
                theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId == "222" || // سند حق انتفاع
                theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId == "811" || // سند صلح حق انتفاع 
                                                                                                 //theCurrentNotaryRegisterServiceReqEntity.TheONotaryDocumentType.Code == "941" || // سند وصیت نامه تملیکی
                                                                                                 //theCurrentNotaryRegisterServiceReqEntity.TheONotaryDocumentType.Code == "901" || // قرارداد پیش فروش ساختمان
                theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId == "221" || // سند صلح سرقفلي و حق كسب و پيشه و تجارت
                theCurrentNotaryRegisterServiceReqEntity.DocumentType.DocumentTypeGroup1Id == "6" || //سند تقسیم نامه
                ( !clientConfiguration.IsUnRegisteredEstateInquiryForced && theCurrentNotaryRegisterServiceReqEntity.IsRegistered ==YesNo.No.GetString() && theCurrentNotaryRegisterServiceReqEntity.DocumentType.IsSupportive == YesNo.No.ToString() ) || //ملک ثبت شده باشد 
                clientConfiguration.IsDSUDealSummaryCreationEnabled !=DSUActionLevel.FullFeature ||
                IsCurrentReqIncludedInExceptions ( theCurrentNotaryRegisterServiceReqEntity.Id.ToString() ) //بعضی اسنادی که دچار اشکال شده اند و شناسه یکتا گرفتند لازم نیست خلاصه از اینجا ارسال کنند و در سامانه جامع املاک ارسال می گردد. 
                )
            {
                return false;
            }
            else
            {
                if (
                    string.Compare ( theCurrentNotaryRegisterServiceReqEntity.RequestDate, clientConfiguration.DSUInitializationDate ) <= 0 && //بعد از راه اندازی خلاصه معامله در ثبت الکترونیک اسناد
                    theCurrentNotaryRegisterServiceReqEntity.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString() &&
                    theCurrentNotaryRegisterServiceReqEntity.State != NotaryRegServiceReqState.FinalPrinted.GetString()
                    )
                {
                    messages =
                        "با توجه به بند 6 بخشنامه تنظیم اسناد رسمی با قابلیت صدور الکترونیک خلاصه معاملات،" +
                        System.Environment.NewLine +
                        " لازم است پرونده جاری ابطال شده و مجدداً نسبت به تشکیل پرونده جدید اقدام شود.";

                    return false;
                }

                if ( !string.IsNullOrWhiteSpace ( theCurrentNotaryRegisterServiceReqEntity.DocumentDate ) &&
                    string.Compare ( theCurrentNotaryRegisterServiceReqEntity.DocumentDate, clientConfiguration.DSUInitializationDate ) < 0 &&
                    theCurrentNotaryRegisterServiceReqEntity.State ==NotaryRegServiceReqState.FinalPrinted.GetString()
                    )
                {
                    return false;
                }

                if (
                    theCurrentNotaryRegisterServiceReqEntity.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString() &&
                    theCurrentNotaryRegisterServiceReqEntity.State != NotaryRegServiceReqState.FinalPrinted.GetString ()
                    )
                {
                    if ( IsDocTypeDSUForced ( theCurrentNotaryRegisterServiceReqEntity.DocumentType ) )
                    {
                        if (
                            theCurrentNotaryRegisterServiceReqEntity.IsRegistered ==YesNo.None.GetString() &&
                            string.Compare ( theCurrentNotaryRegisterServiceReqEntity.RequestDate, clientConfiguration.DSUInitializationDate ) >= 0
                            )
                        {
                            messages =
                                "برای پرونده جاری هنگام تشکیل پرونده، وضعیت ثبتی ملک (ملک ثبت شده یا ثبت نشده) مشخص نشده است." +
                                System.Environment.NewLine +
                                "لطفاً پرونده جاری را ابطال نموده و مجدداً نسبت به تشکیل پرونده جدید اقدام نمایید.";

                            return false;
                        }
                    }
                }

                if (isReadFromDocument==true)
                {
                    if ( documentInquiriesInformation != null && documentInquiriesInformation.Count > 0 )
                    {
                        bool result=false;
                        documentInquiriesInformation.ForEach ( ( theOneInquiry ) =>
                        {

                            if ( theOneInquiry.InquiryOrganizationId == "1" ||
                                 !string.IsNullOrWhiteSpace ( theOneInquiry.EstateInquiryId ) )
                            {
                                result = true;

                            }



                        } );

                        return result;
                    }
                    else
                    {
                        return false;

                    }

                }
                else
                {

                    if ( theCurrentNotaryRegisterServiceReqEntity.DocumentInquiries!=null && theCurrentNotaryRegisterServiceReqEntity.DocumentInquiries.Count>0 )
                    {
                        foreach ( DocumentInquiry theOneInquiry in theCurrentNotaryRegisterServiceReqEntity.DocumentInquiries )
                        {
                            if ( theOneInquiry.DocumentInquiryOrganizationId == "1" || !string.IsNullOrWhiteSpace ( theOneInquiry.EstateInquiriesId ) )
                                return true;
                        }

                        return false;
                    }
                    else
                    {
                        return false;
                    }

                }



            }

            return false;
        }

        public static bool IsDocTypeDSUForced ( DocumentType theSelectedDocumentType )
        {
            if ( theSelectedDocumentType == null )
                return true;

            if ( theSelectedDocumentType.WealthType != YesNo.No.GetString() )
                return false;

            switch ( theSelectedDocumentType.Code )
            {
                case "311":    // سند وكالت فروش اموال غيرمنقول
                case "333":    // تفويض وكالت فروش اموال غيرمنقول
                case "322":    // سند وكالت کاری اموال غيرمنقول
                case "221":    // سند صلح سرقفلي و حق كسب و پيشه و تجارت
                //case "941":    //سند وصیت نامه تملیکی
                case "222":    // سند حق انتفاع
                case "811":    // سند صلح حق انتفاع 
                case "004":    // فک رهن
                               //case "901":    //قرارداد پیش فروش ساختمان
                    return false;
            }

            if (
                ( theSelectedDocumentType.DocumentTypeGroup2Id == "42" && theSelectedDocumentType.Code != "426" ) ||//رهنی غیر بانکی بجز رهنی غیر بانکی ملکی
                theSelectedDocumentType.DocumentTypeGroup2Id == "43" ||                               //رهنی ضم یا تعویض
                theSelectedDocumentType.DocumentTypeGroup1Id == "5" ||   //اجاره
                theSelectedDocumentType.Code == "225" ||    // حق انتفاع - غیرمنقول
                theSelectedDocumentType.DocumentTypeGroup1Id == "6"     //تقسیم نامه
                )
                return false;

            return true;
        }

        public static bool IsCurrentReqIncludedInExceptions ( string reqID )
        {
            if ( string.IsNullOrWhiteSpace ( reqID ) )
                return false;

            if ( _ExceptionReqs == null || _ExceptionReqs.Count<string> () == 0 )
                return false;

            if ( _ExceptionReqs.Contains<string> ( reqID ) )
                return true;

            return false;
        }



        public static bool isLoadDocumentInquiriesList(DocumentType documentType,string isRegistered,string documentId  ,ClientConfiguration clientConfiguration = null )
        {
            if (
                ( documentType.IsSupportive == YesNo.Yes.GetString () && documentType.Id != "004" ) || // خدمات ثبتی به استثناء فک رهن
                 documentType.WealthType != YesNo.No.GetString () || // سند اموال غیر منقول                
                 documentType.Id == "311" || // سند وكالت فروش اموال غيرمنقول نباشد
                 documentType.Id == "333" || // تفويض وكالت فروش اموال غيرمنقول
                 documentType.Id == "322" || // سند وكالت کاری اموال غيرمنقول نباشد
                 documentType.Id == "222" || // سند حق انتفاع
                 documentType.Id == "811" || // سند صلح حق انتفاع 
                                             //theCurrentNotaryRegisterServiceReqEntity.TheONotaryDocumentType.Code == "941" || // سند وصیت نامه تملیکی
                                             //theCurrentNotaryRegisterServiceReqEntity.TheONotaryDocumentType.Code == "901" || // قرارداد پیش فروش ساختمان
                                             documentType.Id == "221" || // سند صلح سرقفلي و حق كسب و پيشه و تجارت
                                             documentType.DocumentTypeGroup1Id == "6" || //سند تقسیم نامه
                ( !clientConfiguration.IsUnRegisteredEstateInquiryForced && isRegistered== YesNo.No.GetString () && documentType.IsSupportive == YesNo.No.ToString () ) || //ملک ثبت شده باشد 
                clientConfiguration.IsDSUDealSummaryCreationEnabled != DSUActionLevel.FullFeature ||
                IsCurrentReqIncludedInExceptions ( documentId ) //بعضی اسنادی که دچار اشکال شده اند و شناسه یکتا گرفتند لازم نیست خلاصه از اینجا ارسال کنند و در سامانه جامع املاک ارسال می گردد. 
                )
            {
                return false;
            }

            return true;


        }
    }

}
