using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
   

    

    public class PersonDocumentCartableViewModel
    {
        public PersonDocumentCartableViewModel()
        {
            this.Documents = new List<DocumentCartableData>();
        }
        public List<DocumentCartableData> Documents { get; set; }
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }
    }

    public class DocumentCartableData
    {
        public string? IsRemoteRequest { get; set; }
        public string? RemoteRequestId { get; set; }
        public string? NationalNo { get; set; }
        public string? RequestNo { get; set; }
        public string? RequestDate { get; set; }
        public string? RequestTime { get; set; }
        public CartableDocumentType? DocumentType { get; set; }
        public string? ScriptoriumId { get; set; }
        public string? StateTitle { get; set; }
        public string? StateId { get; set; }
        public string? DocumentTypeTitle { get; set; }
        public string? DocumentDate { get; set; }
        public bool IsInLegacySystem { get; set; }
        public CartableDocumentInfoConfirm? DocumentInfoConfirm { get; set; }
    }

    public class CartableDocumentType
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string IsSupportive { get; set; }
    }

    public class CartableDocumentInfoConfirm
    {
        public string? ConfirmerRole { get; set; }
        public string? ConfirmerNationalNo { get; set; }
        public string? ConfirmerNameFamily { get; set; }
        public string? ConfirmDate { get; set; }
        public string? ConfirmTime { get; set; }
    }
    public class CartableDocumentPersonType
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
    }
    public class CartableDocumentPerson
    {
        //public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? NationalNo { get; set; }
        public string? BirthDate { get; set; }
        public string? PersonType { get; set; }
        public string? IsOriginal { get; set; }
        public string? LegalPersonTypeTitle { get; set; }
        public string? MobileNo { get; set; }
        public CartableDocumentPersonType? DocumentPersonType { get; set; }
    }
    public class CartableDocumentText
    {
        public string? LegalText { get; set; }
    }
    public class PersonDocumentCartableDetailViewModel : DocumentCartableData
    {
        public List<CartableDocumentPerson>? DocumentPersons { get; set; } = new List<CartableDocumentPerson>();
        public CartableDocumentText? DocumentInfoText { get; set; }
    }

}
