using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentRelatedPersonViewModel : EntityState
    {
        public DocumentRelatedPersonViewModel()
        {

        }
        //public List<string> validateRelatedAgentDocumentDate
        //{ 


        //}

        public List<string> validateRelatedAgentDocumentIssuer()
        {
            List<string> messages=new List<string>();
            bool isRequired = false;

            if (isVakil())
            {
                if (this.IsRelatedAgentDocumentAbroad == true)
                {
                    if (string.IsNullOrEmpty(RelatedAgentDocumentIssuer))
                    {
                        messages.Add("فیلد مرجع صدور اجباری میباشد . ");
                        return messages;
                    }

                }
            }
            else if (!isVakil() && !isVali())
            {
                if (IsRelatedAgentDocumentAbroad == true)
                {
                    if (string.IsNullOrEmpty(RelatedAgentDocumentIssuer))
                    {
                        messages.Add("فیلد مرجع صدور اجباری میباشد . ");
                        return messages;
                    }
                }


            }
            return null;
        }
        public bool isVakil()
        {
            return 
                   RelatedAgentTypeId != null &&
                   RelatedAgentTypeId.Count > 0 &&
                  RelatedAgentTypeId[0] == "1";
        }

        public  bool isVali()
        {
            return 
                   RelatedAgentTypeId != null &&
                   RelatedAgentTypeId.Count > 0 &&
                   RelatedAgentTypeId[0] == "3";
        }

        public  bool isMotamed(DocumentRelatedPersonViewModel documentRelatedPerson)
        {
            return 
                  RelatedAgentTypeId != null &&
                  RelatedAgentTypeId.Count > 0 &&
                  RelatedAgentTypeId[0] == "10";
        }

        public string RelatedPersonId { get; set; }
        public string DocumentId { get; set; }
        public IList<string> MainPersonId { get; set; }
        public IList<string> RelatedAgentPersonId { get; set; }
        public IList<string> RelatedAgentTypeId { get; set; }
        public IList<string> RelatedAgentDocumentCountryId { get; set; }
        public bool IsRelatedAgentDocumentAbroad { get; set; }
        public bool IsRelatedDocumentInSSAR { get; set; }
        public string RelatedAgentDocumentNo { get; set; }
        public string RelatedAgentDocumentDate { get; set; }
        public string RelatedAgentDocumentIssuer { get; set; }
        public string RelatedAgentDocumentSecretCode { get; set; }
        public IList<string> RelatedAgentDocumentScriptoriumId { get; set; }
        public bool IsRelatedPersonLawyer { get; set; }
        public IList<string> RelatedReliablePersonReasonId { get; set; }
        public string RelatedPersonDescription { get; set; }
        public string RelatedAgentTypeTitle { get; set; }
        public string  RowNo { get; set; }
    }


}
