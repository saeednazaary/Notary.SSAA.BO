using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    /// <summary>
    /// Defines the <see cref="RichDocumentInquiry" />
    /// </summary>
    public static class RichDocumentInquiry
    {
        public static bool IsSabtDocumentInquiryOrganization(this Domain.Entities.DocumentInquiry inquiry)
        {
            if (inquiry.DocumentInquiryOrganizationId == "C9A08BB604CE406BB5E17827CB1E88A8")
                return true;
            else return false;
        }
    }
}
