using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Estate
{
    public class DSUConditionTextPacket
    {
        public string _dsuConditionText;
        public string _documentObjectID;
        public string _buyerObjectID;
        public string _regCaseObjectID;

        public string RegCaseObjectID
        {
            get { return _regCaseObjectID; }
            set { _regCaseObjectID = value; }
        }
        public string BuyerObjectID
        {
            get { return _buyerObjectID; }
            set { _buyerObjectID = value; }
        }
        public string DocumentObjectID
        {
            get { return _documentObjectID; }
            set { _documentObjectID = value; }
        }
        public string DsuConditionText
        {
            get { return _dsuConditionText; }
            set { _dsuConditionText = value; }
        }
    }
}
