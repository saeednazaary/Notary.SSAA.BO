using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class RealForeignerServiceQuery : BaseQueryRequest<ApiResult<RealForeignerServiceViewModel>>
    {
        public string ForeignerCode { get; set; }
        public string ClientId { get; set; }
        public RealForeignerServiceQuery()
        {
            ClientId = "SSAR";
        }

    }
}
