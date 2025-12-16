using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons
{
    public static class DigitalBookUtility
    {
        public static DigitalBookGeneratingPermissionStatus IsDigitalBookGeneratingPermitted ( Document theCurrentReq,string ENoteBookEnabledDate, bool IsENoteBookAutoClassifyNoEnabled, ref string message )
        {
            //ConfigurationManager.TypeDefinitions.ClientConfiguration clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration(theCurrentReq.TheScriptorium.TheCMSOrganization);

            if (
                !IsENoteBookAutoClassifyNoEnabled ||
                string.Compare ( theCurrentReq.DocumentDate, ENoteBookEnabledDate ) < 0
            )
            {
                message = "سند جاری شرایط لازم برای درج در صفحه دفتر الکترونیک را دارا نمی باشد.";
                return DigitalBookGeneratingPermissionStatus.NotNeeded;
            }

            if ( theCurrentReq.DocumentType.IsSupportive ==YesNo.No.GetString() )
                return DigitalBookGeneratingPermissionStatus.Needed;

            if ( theCurrentReq.DocumentType.IsSupportive == YesNo.Yes.GetString() )
            {
                if ( theCurrentReq.RelatedDocumentIsInSsar !=YesNo.None.GetString() )
                    return DigitalBookGeneratingPermissionStatus.Needed;
                else
                    return DigitalBookGeneratingPermissionStatus.NotNeeded;
            }

            return DigitalBookGeneratingPermissionStatus.Needed;
        }
    }
}
