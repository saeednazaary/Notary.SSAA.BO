using Microsoft.AspNetCore.Components.Forms;
using Notary.SSAA.BO.DataTransferObject.Bases;
using Stimulsoft.System.Windows.Forms;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentRelationViewModel : EntityState
    {
        public DocumentRelationViewModel() { }

        public string DocumentId { get; set; }
        public string RelatedDocumentId { get; set; }
        public string RelatedtId { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string RequestSecretCode { get; set; }
        public IList<string> ScriptoriumId { get; set; }
        public DateTime RequestRecordDate { get; set; }
        public bool? IsRequestAbroad { get; set; }
        public bool? IsRequestInSsar { get; set; }
        public IList<string> RelatedDocumentTypeId  { get; set; }
        public string RelatedDocumentScriptorium { get; set; }
        public IList<string> RelatedDocAbroadCountryId { get; set; }
        public string RelatedDocumentScriptoriumTitle { get; set; }
        public string RelatedDocumentTypeTitle  { get; set; }


        public (string,bool) validataionMessages (bool isENoteBookEnabled)
        {
            string validationMessage=null;
            bool isValid = true;
            if ( this.IsRequestAbroad == true || this.IsRequestInSsar == null )
            //validateRelatedDocumentsEnabled = false;
            {
            }
            else if ( this.IsRequestInSsar==true || ( this.IsRequestInSsar == false && isENoteBookEnabled ) )
            {
                if ( string.IsNullOrWhiteSpace ( this.RequestNo ) )
                {
                    validationMessage = "شناسه سند وابسته در بخش اسناد وابسته، مشخص نشده است.";
                    isValid= false;
                }
                if ( this.RequestDate == null )
                {
                    validationMessage = "تاریخ سند وابسته در بخش اسناد وابسته، مشخص نشده است.";
                     isValid = false;
                    
                }
                if (
                       string.Compare ( this.RequestDate.ToString (), "1392/06/26" ) < 0 &&
                       !isENoteBookEnabled
                       )
                {
                    validationMessage = "تاریخ وارد شده برای سند وابسته در بخش اسناد وابسته، قبل از راه اندازی سامانه ثبت الکترونیک اسناد می باشد.";

                    isValid = false;
                }
                if (
                  this.ScriptoriumId == null &&
                  !isENoteBookEnabled
                  )
                {
                    validationMessage = "دفترخانه سند وابسته در بخش اسناد وابسته، مشخص نشده است.";
                    isValid= false;
                }


            }
            return (validationMessage,isValid);


        }

    }
}
