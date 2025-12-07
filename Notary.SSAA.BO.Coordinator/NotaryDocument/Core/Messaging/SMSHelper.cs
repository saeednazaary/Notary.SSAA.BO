namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging
{
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="SMSHelper" />
    /// </summary>
    internal class SMSHelper
    {
        /// <summary>
        /// The CollectSMSRecipients
        /// </summary>
        /// <param name="theFoundRegisterServiceReq">The theFoundRegisterServiceReq<see cref="Notary.SSAA.BO.Domain.Entities.Document"/></param>
        /// <returns>The <see cref="List{SMSRecipientPacket}?"/></returns>
        internal List<SMSRecipientPacket>? CollectSMSRecipients ( Notary.SSAA.BO.Domain.Entities.Document theFoundRegisterServiceReq )
        {
            if ( theFoundRegisterServiceReq.DocumentPeople != null && theFoundRegisterServiceReq.DocumentPeople.Count > 0 )
            {
                List<SMSRecipientPacket> smsRecipientPacketCollection = new List<SMSRecipientPacket>();
                foreach ( DocumentPerson theOnePerson in theFoundRegisterServiceReq.DocumentPeople )
                {
                    if ( theOnePerson.IsOriginal == YesNo.Yes.GetString () )
                    {
                        if ( theOnePerson.DocumentPersonTypeId != null )
                        {
                            if (
                                   theOnePerson.DocumentPersonTypeId == "17"    //موکل
                                || theOnePerson.DocumentPersonTypeId == "59" ) //اولین موکل
                            {
                                if ( string.IsNullOrWhiteSpace ( theOnePerson.MobileNo ) || theOnePerson.MobileNo.Length < 11 )
                                    continue;
                                else
                                {
                                    SMSRecipientPacket theOneSMSRecipient = new SMSRecipientPacket();

                                    theOneSMSRecipient.RecipientFullName = theOnePerson.FullName ();
                                    theOneSMSRecipient.RecipientMobileNo = theOnePerson.MobileNo;

                                    smsRecipientPacketCollection.Add ( theOneSMSRecipient );
                                }
                            }
                        }
                    }
                }

                return smsRecipientPacketCollection;
            }
            else
            {
                return null;
            }
        }
    }
}
