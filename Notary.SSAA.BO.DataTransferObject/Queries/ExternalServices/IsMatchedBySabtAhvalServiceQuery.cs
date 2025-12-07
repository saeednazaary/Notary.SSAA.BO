using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class IsMatchedBySabtAhvalServiceQuery : BaseQueryRequest<ApiResult<SabtAhvalServiceViewModel>>
    {
        public string BirthDate { get; set; }

        public string NationalNo { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string SexType { get; set; }

        public string FatherName { get; set; }

        public string Seri { get; set; }

        public string Serial { get; set; }

        public string SeriAlpha { get; set; }

        public string IdentityNo { get; set; }

    }
}
