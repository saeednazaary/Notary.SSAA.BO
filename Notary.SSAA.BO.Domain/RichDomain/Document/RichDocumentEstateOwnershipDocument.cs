namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System.Text;
    using YesNo = Notary.SSAA.BO.SharedKernel.Enumerations.YesNo;

    /// <summary>
    /// Defines the <see cref="RichDocumentEstateOwnershipDocument" />
    /// </summary>
    public static class RichDocumentEstateOwnershipDocument
    {


        /// <summary>
        /// The HasVehicles
        /// </summary>
        /// <param name="documentEstateOwnershipDocument">The document<see cref="Domain.Entities.Document"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string OwnershipDocTitle(this Domain.Entities.DocumentEstateOwnershipDocument documentEstateOwnershipDocument)
        {
            if (documentEstateOwnershipDocument != null
                && documentEstateOwnershipDocument.OwnershipDocumentType != null
            )
            {
                string title = string.Empty;
                switch ((NotaryOwnershipDocumentType)documentEstateOwnershipDocument.OwnershipDocumentType.ToRequiredInt())
                {
                    case NotaryOwnershipDocumentType.SabtDocument:

                        if (!string.IsNullOrWhiteSpace(documentEstateOwnershipDocument.EstateElectronicPageNo))
                        {
                            title =
                            ((NotaryOwnershipDocumentType)documentEstateOwnershipDocument.OwnershipDocumentType.ToRequiredInt()).GetDescription() + " " +
                             ((EstateDocType)documentEstateOwnershipDocument.EstateDocumentType.ToRequiredInt()).GetDescription() +
                            " با شماره صفحه دفتر الکترونیک املاک : " + documentEstateOwnershipDocument.EstateElectronicPageNo;
                            if (documentEstateOwnershipDocument.EstateIsReplacementDocument == YesNo.Yes.GetString())
                                title += " - سند المثنی است.";
                        }
                        else
                        {
                            title =
                             ((NotaryOwnershipDocumentType)documentEstateOwnershipDocument.OwnershipDocumentType.ToRequiredInt()).GetDescription() + " " +
                            ((EstateDocType)documentEstateOwnershipDocument.EstateDocumentType.ToRequiredInt()).GetDescription() +
                            " به شماره ثبت: " + documentEstateOwnershipDocument.EstateSabtNo +
                            " - شماره چاپی سند: " + documentEstateOwnershipDocument.EstateDocumentNo +
                            " - شماره دفتر: " + documentEstateOwnershipDocument.EstateBookNo +
                            " - شماره صفحه دفتر: " + documentEstateOwnershipDocument.EstateBookPageNo;
                            if (documentEstateOwnershipDocument.EstateIsReplacementDocument == YesNo.Yes.GetString())
                                title += " - سند المثنی است";
                            if (documentEstateOwnershipDocument.EstateSeridaftar != null)
                                title +=
                                    " -  سری دفتر: " + documentEstateOwnershipDocument.EstateSeridaftar.Title;
                        }

                        if (string.IsNullOrEmpty(documentEstateOwnershipDocument.EstateBookType) && documentEstateOwnershipDocument.EstateBookType != (NotaryBookType.None.GetString()))
                            title += " - نوع دفتر: " + ((NotaryBookType)documentEstateOwnershipDocument.EstateBookType.ToRequiredInt()).GetDescription();
                        break;
                    case NotaryOwnershipDocumentType.SabtStateReport:
                        title =
                            "شماره گزارش وضعیت ثبتی: " + documentEstateOwnershipDocument.SabtStateReportNo +
                            " - تاریخ گزارش وضعیت ثبتی: " + documentEstateOwnershipDocument.SabtStateReportDate;
                        if (!string.IsNullOrEmpty(documentEstateOwnershipDocument.SabtStateReportUnitName))
                            title += " - صادرکننده گزارش وضعیت ثبتی: " + documentEstateOwnershipDocument.SabtStateReportUnitName;
                        break;
                    case NotaryOwnershipDocumentType.NotaryDocument:
                        title =
                            "دفترخانه: " + documentEstateOwnershipDocument.NotaryNo +
                            " - " + documentEstateOwnershipDocument.NotaryLocation +
                            " - شماره: " + documentEstateOwnershipDocument.NotaryDocumentNo +
                            " - تاریخ: " + documentEstateOwnershipDocument.NotaryDocumentDate;
                        break;
                }
                if (documentEstateOwnershipDocument.Description != null && !string.IsNullOrEmpty(documentEstateOwnershipDocument.Description))
                    title += " - توضیحات: " + documentEstateOwnershipDocument.Description;
                return title;
            }
            else
            {
                return null;
            }
        }

    }

    /// <summary>
    /// Defines the <see cref="AnnotationPack" />
    /// </summary>


}
