using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class PersonDocumentCartableInput: BaseExternalRequest<ApiResult<PersonDocumentCartableViewModel>>
    {
        public string BearerToken { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public CartableDocumentDataExtraParam ExtraParam { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

   
    public class PersonDocumentCartableDetailInput: BaseExternalRequest<ApiResult<PersonDocumentCartableDetailViewModel>>
    {
        public string BearerToken { get; set; }
        public required string DocumentNationalNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
  
}
