using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    /// <summary>
    /// Defines the <see cref="RichDocumentCost" />
    /// </summary>
    public static class RichDocumentCost
    {
        // یافتن نوع هزینه تکراری 
        // در سند نباید نوع هزینه تکراری را وارد کرد
        public static bool IsCosttypeRepetitious(this IEnumerable<DocumentCost> costs)
        {

            string costtypeId = string.Empty;
            IList<DocumentCost> costList = costs.ToList();
            foreach (DocumentCost cost in costList)
            {
                if (costtypeId == cost.CostTypeId)
                {
                    return true;
                }
                costtypeId = cost.CostTypeId;
            }
            return false;


        }
    }
}
