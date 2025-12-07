using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    public static class RichDocumentEstate
    {
        public static string RegCaseText(this Domain.Entities.DocumentEstate documentEstate)
        {
            string casesSpec = "";
            string caseText = string.Empty;
            if (documentEstate != null)
            {

                if (documentEstate.IsAttachment != (YesNo.Yes).GetString())
                {
                    if (documentEstate.DocumentEstateTypeId != null)
                    {
                        if (documentEstate.DocumentEstateType.CountUnitTitle != null)
                            caseText += documentEstate.DocumentEstateType.CountUnitTitle;
                        caseText += documentEstate.DocumentEstateType.Title;
                    }
                    else
                    {
                        caseText += " ملک ";
                    }
                    caseText += !documentEstate.SecondaryPlaque.IsNullOrWhiteSpace() ? " به پلاک فرعی " + documentEstate.SecondaryPlaque : null;
                    caseText += !documentEstate.DivFromSecondaryPlaque.IsNullOrWhiteSpace() ? " مفروز و مجزی از پلاک فرعی " + documentEstate.DivFromSecondaryPlaque : null;
                    caseText += !documentEstate.BasicPlaque.IsNullOrWhiteSpace() ? " پلاک اصلی " + documentEstate.BasicPlaque : null;
                    caseText += !documentEstate.DivFromBasicPlaque.IsNullOrWhiteSpace() ? " مفروز و مجزی از پلاک اصلی " + documentEstate.DivFromBasicPlaque : null;
                }
                else
                {


                    string[] idList = new string[] { documentEstate.AttachmentType };
                    if (documentEstate.AttachmentType != null)
                    {
                        caseText +=
                       "واحد " +
                       ((documentEstate.AttachmentType == ((int)DocumentEstateAttachmentType.Others).ToString()) ? documentEstate.AttachmentTypeOthers : ((DocumentEstateAttachmentType)documentEstate.AttachmentType.ToNullableInt()).GetEnumDescription()) +
                       " از";
                    }

                    if (documentEstate.SecondaryPlaque != null && !string.IsNullOrEmpty(documentEstate.SecondaryPlaque))
                        caseText += " پلاک فرعی " + documentEstate.SecondaryPlaque;

                    if (documentEstate.DivFromSecondaryPlaque != null && !string.IsNullOrEmpty(documentEstate.DivFromSecondaryPlaque))
                        caseText += " مفروز و مجزی از پلاک فرعی " + documentEstate.DivFromSecondaryPlaque;

                    if (documentEstate.BasicPlaque != null && !string.IsNullOrEmpty(documentEstate.BasicPlaque))
                        caseText += " پلاک اصلی " + documentEstate.BasicPlaque;

                    if (documentEstate.DivFromBasicPlaque != null && !string.IsNullOrEmpty(documentEstate.DivFromBasicPlaque))
                        caseText += " مفروز و مجزی از پلاک اصلی " + documentEstate.DivFromBasicPlaque;

                    if (!string.IsNullOrWhiteSpace(caseText))
                        caseText = "یک " + caseText;


                    if (string.IsNullOrWhiteSpace(caseText))
                        caseText = documentEstate.RegisterCaseTitle;
                }


            }
            return caseText;
        }


    }
}
