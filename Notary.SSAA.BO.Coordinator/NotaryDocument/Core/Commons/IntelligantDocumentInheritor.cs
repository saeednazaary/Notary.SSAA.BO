using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons
{
    public static class IntelligantDocumentInheritor
    {
        public static bool IsAutoQuotaGeneratingPermitted ( Document theOnotaryRegisterServiceReq, ClientConfiguration clientConfiguration, ref string messageText )
        {
            if ( !DsuUtility.IsDSUGeneratingPermitted ( ref messageText, theOnotaryRegisterServiceReq,null,false, clientConfiguration ) )
                return false;

            if ( theOnotaryRegisterServiceReq.DocumentType.WealthType != WealthType.Immovable.GetString() )
                return false;

            if ( theOnotaryRegisterServiceReq.DocumentType.IsSupportive != YesNo.No.GetString() )
                return false;

            if ( !theOnotaryRegisterServiceReq.DocumentEstates.Any () )
                return false;

            //if (
            //    theOnotaryRegisterServiceReq.State == Rad.CMS.Enumerations.NotaryRegServiceReqState.SetNationalDocumentNo ||
            //    theOnotaryRegisterServiceReq.State == Rad.CMS.Enumerations.NotaryRegServiceReqState.FinalPrinted
            //    )
            //    return false;

            foreach ( DocumentEstate theOneRegCase in theOnotaryRegisterServiceReq.DocumentEstates )
            {
                if ( theOneRegCase.IsProportionateQuota != YesNo.No.GetString() )
                    return true;
            }

            return false;
        }

    }
}
