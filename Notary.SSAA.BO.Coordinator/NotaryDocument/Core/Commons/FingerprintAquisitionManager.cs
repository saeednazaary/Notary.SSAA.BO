using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RichDomain.Document;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons
{
    public static class FingerprintAquisitionManager
    {
        /// <summary>
        /// تصميم مي گيرد که آيا اثر انگشت از شخص نبايد گرفته شود، بايد گرفته شود يا به اختيار کاربر است
        /// پارامتر آخر برای این است که در هنگام بررسی رابطه های پیچیده و بازگشتی، نباید اجازه دهیم تابع رابطه قبل را بررسی نماید.
        /// در واقع جهت حرکت گراف برگشتی همیشه رو به جلو می باشد
        /// </summary>
        /// <param name="desiredDocPerson"></param>
        /// <returns>
        /// Forbidden : نبايد اثر انگشت اخذ شود
        /// Mandatory : بايد اثر انگشت اخذ شود
        /// Optional : اخذ اثر انگشت به اختيار کاربر است و بايد در اين مورد از کاربر پرسش شود
        /// </returns>
        public static FingerprintAquisitionPermission IsFingerprintAquisitionPermitted(Document desiredRegisterServiceReq, DocumentPerson desiredDocPerson, bool isFirstNode = true)
        {
            string personFullName = desiredDocPerson.FullName() + " - " + desiredDocPerson.NationalNo;


            if (isFirstNode && desiredDocPerson.PersonType == PersonType.Legal.GetString())
                return FingerprintAquisitionPermission.Forbidden;

            if (
                desiredDocPerson.PersonType == PersonType.NaturalPerson.GetString() &&
                (desiredDocPerson.IsIranian == YesNo.Yes.GetString() && desiredDocPerson.IsAlive == YesNo.No.GetString())
                )
            {
                return FingerprintAquisitionPermission.Forbidden;
            }

            // در تاریخ 14000618 و به اصرار امور اسناد و سرکار خانم صحرائیان، از اخذ اثر انگشت اولین موکل در اسناد ممانعت شد
            if (desiredDocPerson.DocumentPersonTypeId != null && desiredDocPerson.DocumentPersonTypeId == "59")   // اولين موکل
                return FingerprintAquisitionPermission.Forbidden;

            // آيا شخص داراي وکيل، ولي، نماينده و ... است؟
            if (isFirstNode)
                foreach (DocumentPerson docPerson in desiredRegisterServiceReq.DocumentPeople)
                    if (docPerson.IsRelated == YesNo.Yes.GetString())
                        foreach (DocumentPersonRelated docAgent in docPerson.DocumentPersonRelatedAgentPeople)
                            if (
                                (
                                ((!string.IsNullOrWhiteSpace(docAgent.MainPerson.NationalNo)) && (!string.IsNullOrWhiteSpace(desiredDocPerson.NationalNo)) && docAgent.MainPerson.NationalNo == desiredDocPerson.NationalNo && docAgent.MainPerson.PersonType == desiredDocPerson.PersonType) ||
                                docAgent.MainPersonId == desiredDocPerson.Id) &&
                                docAgent.AgentTypeId !=  Notary.SSAA.BO.SharedKernel.Constants.DocumentAgentType.Movares && // مورث نباشد
                                docAgent.AgentTypeId !=  Notary.SSAA.BO.SharedKernel.Constants.DocumentAgentType.Motamed && // معتمد نباشد
                                docAgent.AgentTypeId !=  Notary.SSAA.BO.SharedKernel.Constants.DocumentAgentType.Moaref && // معرف نباشد
                                docAgent.AgentTypeId !=  Notary.SSAA.BO.SharedKernel.Constants.DocumentAgentType.Motarjem && // مترجم نباشد
                                docAgent.AgentTypeId !=  Notary.SSAA.BO.SharedKernel.Constants.DocumentAgentType.DadsetanYaRaisDadgahBakhsh && // دادستان نباشد 
                                docAgent.AgentTypeId !=  Notary.SSAA.BO.SharedKernel.Constants.DocumentAgentType.Shahed    // شاهد                                
                                )
                            {
                                if (desiredRegisterServiceReq.DocumentTypeId == "007" || desiredRegisterServiceReq.DocumentTypeId == "0034")
                                    return FingerprintAquisitionPermission.Optional;
                                else
                                    return FingerprintAquisitionPermission.Forbidden;
                            }

            // ایا سند انتقال اجرایی یا براساس حکم دادگاه است؟
            if (desiredRegisterServiceReq.IsBasedJudgment == YesNo.Yes.GetString())
                return FingerprintAquisitionPermission.Optional;

            //971116-  آقای کریمی-  فرد صغیر دارای وکیل 
            // آیا شخص دارای ولی، وکیل نیز دارد؟
            //if (desiredDocPerson.IsAgentPerson == YesNo.Yes)
            //    foreach (IONotaryDocAgent docAgent in desiredDocPerson.TheAgentPrsList)
            //    {
            //        if (docAgent.TheONotaryAgentType.Code == "3")
            //        {
            //            foreach (IONotaryDocPerson docPerson in desiredRegisterServiceReq.TheONotaryDocPersonList)
            //                if (docPerson.IsAgentPerson == YesNo.Yes)
            //                    foreach (IONotaryDocAgent docAgents in docPerson.TheAgentPrsList)
            //                        if (docAgent.ONotaryAgentTypeId != "E74C3AAE62204ABF875146C9D82A954A")
            //                            return FingerprintAquisitionPermission.Forbidden;

            //        }
            //    }


            //1397/12/19   آقای کریمی
            // آیا شخص دارای ولی، وکیل نیز دارد؟
            //if (desiredDocPerson.IsAgentPerson == YesNo.Yes)
            //    foreach (IONotaryDocAgent docAgent in desiredDocPerson.TheAgentPrsList)
            //    {
            //        if (docAgent.TheONotaryAgentType.Code == "3" && desiredRegisterServiceReq.TheONotaryDocumentType.Code != "321")          // اگر ولی داشته باشد و سند وکالت نامه نباشد
            //        {
            //            foreach (IONotaryDocPerson docPerson in desiredRegisterServiceReq.TheONotaryDocPersonList)
            //                if (docPerson.IsAgentPerson == YesNo.Yes)
            //                    foreach (IONotaryDocAgent docAgents in docPerson.TheAgentPrsList)
            //                        if (docAgent.ONotaryAgentTypeId != "E74C3AAE62204ABF875146C9D82A954A")          // اگر وکیل نباشد
            //                            return FingerprintAquisitionPermission.Forbidden;
            //                        else
            //                            return FingerprintAquisitionPermission.Mandatory;
            //            ////  اقای کریمی

            //        }
            //    }

            //// آیا شخص دارای ولی، وکیل نیز دارد؟
            //if (desiredDocPerson.IsAgentPerson == YesNo.Yes)
            //    foreach (IONotaryDocAgent docAgent in desiredDocPerson.TheAgentPrsList)
            //    {
            //        if (docAgent.TheONotaryAgentType.Code == "3" &&
            //              desiredRegisterServiceReq.TheONotaryDocumentType.Code != "311" &&  //	سند وكالت فروش اموال غيرمنقول
            //              desiredRegisterServiceReq.TheONotaryDocumentType.Code != "312" &&    //سند وكالت فروش اموال منقول - وسايل نقليه
            //              desiredRegisterServiceReq.TheONotaryDocumentType.Code != "313" &&   //	سند وكالت فروش اموال منقول - ساير اموال منقول
            //              desiredRegisterServiceReq.TheONotaryDocumentType.Code != "321" &&        //سند وكالت‌نامه
            //              desiredRegisterServiceReq.TheONotaryDocumentType.Code != "322" &&  //	سند وكالت کاري اموال غيرمنقول
            //              desiredRegisterServiceReq.TheONotaryDocumentType.Code != "323" && //	سند وكالت کاري وسايل نقليه
            //              desiredRegisterServiceReq.TheONotaryDocumentType.Code != "331" &&    //	تفويض وکالت فروش وسايل نقليه
            //              desiredRegisterServiceReq.TheONotaryDocumentType.Code != "332"	///     //تفويض وكالت
            //               )///           // اگر ولی داشته باشد و سند وکالت نامه نباشد
            //        {
            //            foreach (IONotaryDocPerson docPerson in desiredRegisterServiceReq.TheONotaryDocPersonList)
            //                if (docPerson.IsAgentPerson == YesNo.Yes)
            //                    foreach (IONotaryDocAgent docAgentinmainlist in docPerson.TheAgentPrsList)
            //                        if (docAgentinmainlist.ONotaryAgentTypeId == "E74C3AAE62204ABF875146C9D82A954A")          // اگر وکیل داشته باشد
            //                            return FingerprintAquisitionPermission.Forbidden;
            //                        else
            //                            return FingerprintAquisitionPermission.Mandatory;

            //        }
            //    }

            //  این بخش مربوط به صغیر دارای وکیل و ولی می باشد که بعد از نسخه گذاری استثناهایی مشخص شد و برای بررسی بیشتر موضوع نسخه برگردانده شد

            // آیا این شخص وکيل، ولي، نماينده و ... شخصی است که سمت آن شخص دیگر ایجاب می کند حتماً وکیل وی سند را امضاء کند؟
            bool JudicialAuthority = false;
            if (desiredDocPerson.IsRelated == YesNo.Yes.GetString())
                foreach (DocumentPersonRelated docAgent in desiredDocPerson.DocumentPersonRelatedAgentPeople)
                {
                    if (docAgent.MainPerson == null)
                        continue;

                    if (docAgent.MainPerson.DocumentPersonType != null &&
                         docAgent.MainPerson.DocumentPersonType.IsRequired == YesNo.Yes.GetString() &&
                        docAgent.MainPerson.DocumentPersonTypeId != "16" &&     // وکیل
                        docAgent.MainPerson.DocumentPersonTypeId != "59" &&     // اولين موکل
                        docAgent.MainPerson.DocumentPersonTypeId != "39")       // متهب                          
                        if (
                            docAgent.AgentTypeId ==  Notary.SSAA.BO.SharedKernel.Constants.DocumentAgentType.NemayandeMaghamGhazayi &&
                            desiredRegisterServiceReq.DocumentTypeId == "009") //مقام قضایی و رونوشت از سند --- زهری
                            JudicialAuthority = true;
                        else
                            return FingerprintAquisitionPermission.Mandatory;


                    if (
                        docAgent.MainPerson.DocumentPersonTypeId == null &&
                        docAgent.MainPerson.IsRelated == YesNo.Yes.GetString() &&
                        docAgent.MainPerson.DocumentPersonRelatedAgentPeople != null &&
                        docAgent.MainPerson.DocumentPersonRelatedAgentPeople.Count > 0
                        )
                    {
                        FingerprintAquisitionPermission currentAgentPemission = IsFingerprintAquisitionPermitted(desiredRegisterServiceReq, docAgent.MainPerson, false);
                        if (currentAgentPemission == FingerprintAquisitionPermission.Forbidden && (desiredRegisterServiceReq.DocumentTypeId == "007" || desiredRegisterServiceReq.DocumentTypeId == "0034"))
                            return FingerprintAquisitionPermission.Optional;
                        else
                            return currentAgentPemission;
                    }
                }
            if (JudicialAuthority) return FingerprintAquisitionPermission.Optional; //زهری

            //سمت هایی هستند که هرچند ورودشان به سند اجباری نیست ولی اگر انتخاب شوند، باید حتماً اثرانگشت داشته باشند و سند را امضا نمایند.
            //سمت هایی نیز با توجه به نوع سند اجباری می گردند.
            if (
                desiredDocPerson.DocumentPersonTypeId != null && desiredRegisterServiceReq != null && (
                (desiredDocPerson.DocumentPersonTypeId == "16" && desiredRegisterServiceReq.DocumentTypeId == "0022") || //وکیل در استعفای وکیل
                (desiredDocPerson.DocumentPersonTypeId == "63" && desiredRegisterServiceReq.DocumentTypeId == "005") || // مفاسخ در سند فسخ سند
                (desiredDocPerson.DocumentPersonTypeId == "17" && desiredRegisterServiceReq.DocumentTypeId == "006") //موکل در عزل وکیل
                ))
                return FingerprintAquisitionPermission.Mandatory;

            // سمت های زیر هرچند اجباراً باید در سند وارد شوند ولی ممکن است سند را امضاء نکنند
            if (desiredDocPerson.DocumentPersonTypeId != null && desiredRegisterServiceReq != null &&
                (desiredDocPerson.DocumentPersonTypeId == "16" ||      // وکیل
                desiredDocPerson.DocumentPersonTypeId == "39" ||       // متهب
                (desiredDocPerson.DocumentPersonTypeId == "56" && desiredRegisterServiceReq.DocumentTypeId == "007")       // متقاضی در اسناد اجراییه
                ))
                return FingerprintAquisitionPermission.Optional;

            // در سند صداق، امضای هیچ یک از اصحاب سند اجباری نیست
            if ( desiredRegisterServiceReq != null && desiredRegisterServiceReq.DocumentTypeId == "979")      // سند صداق
                return FingerprintAquisitionPermission.Optional;

            // سمت هایی که اجباراً باید سند را امضاء کنند
            if (
                desiredDocPerson.DocumentPersonType != null &&
                desiredDocPerson.DocumentPersonType.IsRequired == YesNo.Yes.GetString()
                )
                return FingerprintAquisitionPermission.Mandatory;

            // در اسناد رهنی، سمت های متنوعی داریم که بسته به تشخیص کاربر انتخاب می شوند و باید حتماً سند را امضاء کنند
            if ( desiredRegisterServiceReq != null &&desiredRegisterServiceReq.DocumentType.DocumentTypeGroup1Id == "4" ||     // اسناد رهني
                (desiredRegisterServiceReq != null && desiredRegisterServiceReq.DocumentTypeId == "961") )    // سند ذمه
                return FingerprintAquisitionPermission.Mandatory;

            return FingerprintAquisitionPermission.Optional;

        }
    }

}
