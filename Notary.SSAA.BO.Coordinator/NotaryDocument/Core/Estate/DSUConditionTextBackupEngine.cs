using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Estate;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate
{
    public class DSUConditionTextBackupEngine
    {
        List<DSUConditionTextPacket> _dsuConditionTextPacketsCollection = null;
        public DSUConditionTextBackupEngine ( )
        {
            _dsuConditionTextPacketsCollection = new List<DSUConditionTextPacket> ();
        }
        public void Backup ( string conditionText, string regCaseID, string documentID, string buyerID )
        {
            if ( string.IsNullOrWhiteSpace ( conditionText ) || string.IsNullOrWhiteSpace ( buyerID ) )
                return;

            _dsuConditionTextPacketsCollection.Add ( new DSUConditionTextPacket () { DsuConditionText = conditionText, DocumentObjectID = documentID, BuyerObjectID = buyerID, RegCaseObjectID = regCaseID } );
        }
        public string Restore ( string regCaseID, string documentID, string buyerID )
        {
            DSUConditionTextPacket backupPacket = _dsuConditionTextPacketsCollection.Find(q => q.BuyerObjectID == buyerID && q.DocumentObjectID == documentID && q.RegCaseObjectID == regCaseID);

            if ( backupPacket != null )
                return backupPacket.DsuConditionText;

            return null;
        }
    }
}
