using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media
{
    public class MediaRemoveServiceInput: BaseExternalRequest<ApiResult>
    {
        public string MediaId { get; set; }
        public string DocId { get; set; }
        public string ClientId { get; set; }
    }
}
