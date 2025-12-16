using SixLabors.ImageSharp.ColorSpaces;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    public static class RichDocumentEstateAttachment
    {
        #region Commons

        public static string AttachmentDescription(this Domain.Entities.DocumentEstateAttachment documentEstateAttachment)
        {
                string attachmentText;

                if (documentEstateAttachment.AttachmentType != NotaryRegCaseAttachmentType.Others.GetString())
                    attachmentText = ((NotaryRegCaseAttachmentType)documentEstateAttachment.AttachmentType.ToNullableInt()).GetDescription();
                else
                    attachmentText = "";
                if (
                    (documentEstateAttachment.AttachmentType == NotaryRegCaseAttachmentType.AbCommon.GetString() ||
                    documentEstateAttachment.AttachmentType == NotaryRegCaseAttachmentType.AbPrivate.GetString() ||
                    documentEstateAttachment.AttachmentType == NotaryRegCaseAttachmentType.BarghCommon.GetString() ||
                    documentEstateAttachment.AttachmentType == NotaryRegCaseAttachmentType.BarghPrivate.GetString() ||
                    documentEstateAttachment.AttachmentType == NotaryRegCaseAttachmentType.GazCommon.GetString() ||
                    documentEstateAttachment.AttachmentType == NotaryRegCaseAttachmentType.GazPrivate.GetString() ||
                    documentEstateAttachment.AttachmentType == NotaryRegCaseAttachmentType.Tel.GetString()) && string.IsNullOrEmpty(documentEstateAttachment.No))
                {
                    attachmentText += " با مشخصات منصوبه در محل";
                    if (documentEstateAttachment.Description != null && !string.IsNullOrEmpty(documentEstateAttachment.Description))
                        attachmentText += " - توضیحات: " + documentEstateAttachment.Description;
                    return attachmentText;
                }

                if (documentEstateAttachment.No != null && !string.IsNullOrEmpty(documentEstateAttachment.No))
                    attachmentText += " با شماره " + documentEstateAttachment.No;
                if (documentEstateAttachment.Area != null)
                    attachmentText += " به مساحت " + documentEstateAttachment.Area.ToString() + " مترمربع ";
                if (documentEstateAttachment.Location != null && !string.IsNullOrEmpty(documentEstateAttachment.Location))
                    attachmentText += " واقع در " + documentEstateAttachment.Location;
                if (documentEstateAttachment.Description != null && !string.IsNullOrEmpty(documentEstateAttachment.Description))
                    attachmentText += " - توضیحات: " + documentEstateAttachment.Description;

                return attachmentText;
    }
        #endregion
 
    }
}
