
using SixLabors.ImageSharp.ColorSpaces;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentEstateOwnerShipViewModel : EntityState
    {

        public string EstateOwnerShipId { get; set; }
        public string DocumentEstateId { get; set; }
        public string RowNo { get; set; }

        //مالک مستند نام و نام خانوادگی
        public string OwnerShipDocumentPersonFullName { get; set; }
        //مشخصات مستند مالکیت
        public string OwnerShipSpecificationsText { get; set; }
        //نام واحد ثبتی
        public string OwnerShipSabtStateReportUnitName { get; set; }
        //موارد لازم به ذکر خلاصه معامله
        public string OwnerShipDealSummaryText { get; set; }
        //موارد لازم به ذکر
        public string OwnerShipDescription { get; set; }
        //نوع مستند
        public string OwnershipDocumentType { get; set; }
        public string OwnershipDocTitle
        {
            get
            {
                string title = string.Empty;

                if (this.OwnershipDocumentType == NotaryOwnershipDocumentType.SabtDocument.GetString())
                {
                    if (!string.IsNullOrWhiteSpace(this.EstateElectronicPageNo))
                    {
                        title =
                            ((NotaryOwnershipDocumentType)OwnershipDocumentType.ToRequiredInt()).GetDescription() + " "+
                            " با شماره صفحه دفتر الکترونیک املاک : " + this.EstateElectronicPageNo;
                        if (this.EstateIsReplacementDocument == YesNo.Yes.GetString())
                            title += " - سند المثنی است.";
                    }
                    else
                    {
                        title =
                            ((NotaryOwnershipDocumentType)OwnershipDocumentType.ToRequiredInt()).GetDescription() + " " +
                            " به شماره ثبت: " + this.EstateSabtNo +
                            " - شماره چاپی سند: " + this.EstateDocumentNo +
                            " - شماره دفتر: " + this.EstateBookNo +
                            " - شماره صفحه دفتر: " + this.EstateBookPageNo;
                        if (this.EstateIsReplacementDocument == YesNo.Yes.GetString())
                            title += " - سند المثنی است";
                    }

                  
                }
                else if (this.OwnershipDocumentType == NotaryOwnershipDocumentType.SabtStateReport.GetString())
                {
                    title =
                        "شماره گزارش وضعیت ثبتی: " + this.SabtStateReportNo +
                        " - تاریخ گزارش وضعیت ثبتی: " + this.SabtStateReportDate;
                    if (!string.IsNullOrEmpty(this.OwnerShipSabtStateReportUnitName))
                        title += " - صادرکننده گزارش وضعیت ثبتی: " + this.OwnerShipSabtStateReportUnitName;
                }
                else if (this.OwnershipDocumentType == NotaryOwnershipDocumentType.NotaryDocument.GetString())
                {
                    title =
                        "دفترخانه: " + this.NotaryNo +
                        " - " + this.NotaryLocation +
                        " - شماره: " + this.NotaryDocumentNo +
                        " - تاریخ: " + this.NotaryDocumentDate;
                }

                if (this.Description != null && !string.IsNullOrEmpty(this.Description))
                    title += " - توضیحات: " + this.Description;

                return title;
            }
        }
        public string OwnershipDocumentTypeTitle => ((NotaryOwnershipDocumentType)OwnershipDocumentType.ToRequiredInt()).GetDescription();
        //مالک مستند
        public IList<string> OwnerShipDocumentPersonId { get; set; }
        /// شناسه استعلام هاي املاک مربوط به سند
        public string EstateInquiriesId { get; set; }
        /// نوع سند ملک
        public string EstateDocumentType { get; set; }
        // شماره ثبت
        public string EstateSabtNo { get; set; }
        // شماره چاپي سند
        public string EstateDocumentNo { get; set; }
        /// شماره دفتر املاک
        public string EstateBookNo { get; set; }

        /// شماره صفحه دفتر املاک
        public string EstateBookPageNo { get; set; }

        /// نوع دفتر املاک
        public string EstateBookType { get; set; }

        /// شماره صفحه دفتر الکترونيک املاک
        public string EstateElectronicPageNo { get; set; }

        /// شناسه سري دفتر املاک
        public IList<string> EstateSeridaftarId { get; set; }

        /// آيا سند المثني است؟
        public string EstateIsReplacementDocument { get; set; }

        /// شماره دفترخانه صادرکننده سند بيع
        public string NotaryNo { get; set; }

        /// محل دفترخانه صادرکننده سند بيع
        public string NotaryLocation { get; set; }

        /// شماره سند بيع دفترخانه
        public string NotaryDocumentNo { get; set; }

        /// تاريخ صدور سند بيع دفترخانه
        public string NotaryDocumentDate { get; set; }

        /// شماره گزارش وضعيت ثبتي
        public string SabtStateReportNo { get; set; }

        /// تاريخ گزارش وضعيت ثبتي
        public string SabtStateReportDate { get; set; }

        /// متن رهن
        public string MortgageText { get; set; }


        /// توضيحات
        public string Description { get; set; }

        /// شناسه دفترخانه صادرکننده
        public string ScriptoriumId { get; set; }


    }
}
