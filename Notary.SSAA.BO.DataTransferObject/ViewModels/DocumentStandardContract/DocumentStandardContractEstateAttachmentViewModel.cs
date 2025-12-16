
using SixLabors.ImageSharp.ColorSpaces;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractEstateAttachmentViewModel : EntityState
    {

        public string EstateAttachmentId { get; set; }
        public string DocumentEstateId { get; set; }
        public string Description { get; set; }
        public string AttachmentArea { get; set; }
        public string AttachmentNo { get; set; }
        public string RowNo { get; set; }
        public string AttachmentLocation { get; set; }
        public string AttachmentType { get; set; }
        public string AttachmentDescription
        {
            get
            {
                string attachmentText;

                if (this.AttachmentType != NotaryRegCaseAttachmentType.Others.GetString())
                    attachmentText =  ((NotaryRegCaseAttachmentType)this.AttachmentType.ToNullableInt()).GetDescription();
                else
                    attachmentText = "";
                if (
                    (this.AttachmentType == NotaryRegCaseAttachmentType.AbCommon.GetString() ||
                    this.AttachmentType == NotaryRegCaseAttachmentType.AbPrivate.GetString() ||
                    this.AttachmentType == NotaryRegCaseAttachmentType.BarghCommon.GetString() ||
                    this.AttachmentType == NotaryRegCaseAttachmentType.BarghPrivate.GetString() ||
                    this.AttachmentType == NotaryRegCaseAttachmentType.GazCommon.GetString() ||
                    this.AttachmentType == NotaryRegCaseAttachmentType.GazPrivate.GetString() ||
                    this.AttachmentType == NotaryRegCaseAttachmentType.Tel.GetString()) && string.IsNullOrEmpty( this.AttachmentNo))
                {
                    attachmentText += " با مشخصات منصوبه در محل";
                    if (this.Description != null && !string.IsNullOrEmpty(this.Description))
                        attachmentText += " - توضیحات: " + this.Description;
                    return attachmentText;
                }

                if (this.AttachmentNo != null && !string.IsNullOrEmpty(this.AttachmentNo))
                    attachmentText += " با شماره " + this.AttachmentNo;
                if (this.AttachmentArea != null)
                    attachmentText += " به مساحت " + this.AttachmentArea.ToString() + " مترمربع ";
                if (this.AttachmentLocation != null && !string.IsNullOrEmpty(this.AttachmentLocation))
                    attachmentText += " واقع در " + this.AttachmentLocation;
                if (this.Description != null && !string.IsNullOrEmpty(this.Description))
                    attachmentText += " - توضیحات: " + this.Description;

                return attachmentText;


            }
        }

    }

}
