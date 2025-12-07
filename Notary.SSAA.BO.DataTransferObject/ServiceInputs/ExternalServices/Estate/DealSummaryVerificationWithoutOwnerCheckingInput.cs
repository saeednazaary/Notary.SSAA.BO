using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class DealSummaryVerificationWithoutOwnerCheckingInput<T> : SendDealSummaryInput<T> where T : class
    {
        public DealSummaryVerificationWithoutOwnerCheckingInput()
        {
            this.IsRemoveRestrictionDealSummary = null;
        }
    }    
}
