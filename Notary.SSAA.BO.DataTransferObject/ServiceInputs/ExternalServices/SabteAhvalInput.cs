using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class SabteAhvalInput : BaseExternalRequest<ApiResult<ViewModels.ExternalServices.SabtAhvalServiceViewModel>>
    {
        public string ClientId { get; set; }
        public string NationalityCode { get; set; }
        public string BirthDate { get; set; }
    }
}
