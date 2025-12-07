using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class PersonDocumentCartableQuery: BaseQueryRequest<ApiResult<PersonDocumentCartableViewModel>>
    {
        public string BearerToken { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public CartableDocumentDataExtraParam ExtraParam { get; set; }
    }
    public class PersonDocumentCartableDetailQuery : BaseQueryRequest<ApiResult<PersonDocumentCartableDetailViewModel>>
    {
        public string BearerToken { get; set; }
        public string DocumentNationalNo { get; set; }

    }
    public class CartableDocumentDataExtraParam
    {
        public string NationalNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> StateId { get; set; }
    }
}
