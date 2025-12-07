using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.SharedModels;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Document
{
    public class IsAccessTimeValidًQuery : BaseQueryRequest<ApiResult<ONotaryAccessTimeValidationOutputMessage>>
    {
        public IsAccessTimeValidًQuery ( string oNotaryActionTypeId, string [ ] organizationHierarchy )
        {
            ONotaryActionTypeId = oNotaryActionTypeId;
            OrganizationHierarchy=organizationHierarchy;
            ;
        }

        public string ONotaryActionTypeId { get; set; }
        public string[] OrganizationHierarchy { get; set; }

    }
}
