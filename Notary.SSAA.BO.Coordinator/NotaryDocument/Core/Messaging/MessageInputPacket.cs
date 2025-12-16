using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging
{
    public class MessageInputPacket
    {
        private string _IMSI = string.Empty;

        public Document MainEntity { get; set; }

        public string MainEntityObjectID { get; set; }

        public string SenderScripotiumObjectID { get; set; }

        public string DocObjectID { get; set; }

        public string DocNationalNo { get; set; }

        public string DocumentTypeTitle { get; set; }

        public string ScriptoriumFullName { get; set; }

        public string MessageContext { get; set; }

        public string RecipientPhoneNo
        {
            get { return _IMSI; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (value.StartsWith("09") && value.Length == 11)
                        _IMSI = value;
                    if (value.StartsWith("+989"))
                    {
                        value = value.Replace("+98", "0");
                        if (value.Length == 11)
                            _IMSI = value;
                    }
                    if (value.StartsWith("00989"))
                    {
                        value = value.Replace("0098", "0");
                        if (value.Length == 11)
                            _IMSI = value;
                    }
                }
            }
        }

        public string RecipientFullName { get; set; }

        public UserAction UserCurrentAction { get; set; }

        public string NationalityCode { get; set; }

        public IList<dynamic> scriptorium { get; set; }
        //  public IList<IScriptorium> scriptorium { get; set; }

        public string RecipientDocPersonTypeCode { get; set; }

        public string OriginalRemoteDocumentTypeCode { get; set; }
    }
}

