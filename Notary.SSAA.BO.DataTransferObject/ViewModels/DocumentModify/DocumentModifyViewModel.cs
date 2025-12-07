namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentModify
{
    public class DocumentModifyViewModel
    {
        public DocumentModifyViewModel()
        {

        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        /// <summary>
        /// شناسه  
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// شناسه  سند
        /// </summary>
        public string DocumentId { get; set; }
        /// <summary>
        /// شناسه یکتا سند
        /// </summary>
        public string NationalNo { get; set; }
        /// <summary>
        /// نوع سند
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// تاریخ سند
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// شماره ترتیب قبلی
        /// </summary>
        public string ClassifyNoOld { get; set; }

        /// <summary>
        /// شماره ترتیب جدید
        /// </summary>
        public string ClassifyNoNew { get; set; }

        /// <summary>
        /// تاریخ قبلی ثبت در دفتر
        /// </summary>
        public string WriteInBookDateOld { get; set; }

        /// <summary>
        /// تاریخ جدید ثبت در دفتر
        /// </summary>
        public string WriteInBookDateNew { get; set; }

        /// <summary>
        /// شماره جلد قبلی دفتر
        /// </summary>
        public string RegisterVolumeNoOld { get; set; }

        /// <summary>
        /// شماره جلد جدید دفتر
        /// </summary>
        public string RegisterVolumeNoNew { get; set; }

        /// <summary>
        /// شماره صفحات قبلی دفتر
        /// </summary>
        public string RegisterPagesNoOld { get; set; }

        /// <summary>
        /// شماره صفحات جدید دفتر
        /// </summary>
        public string RegisterPagesNoNew { get; set; }

        /// <summary>
        /// تاریخ اصلاح
        /// </summary>
        public string ModifyDate { get; set; }
        public string Modifier { get; set; }

    }
}
