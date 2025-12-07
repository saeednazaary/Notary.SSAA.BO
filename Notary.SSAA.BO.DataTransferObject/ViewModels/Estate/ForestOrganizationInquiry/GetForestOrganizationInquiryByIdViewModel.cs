using Notary.SSAA.BO.DataTransferObject.Bases;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.ForestOrganizationInquiry
{

    public class ForestOrganizationExtraParam
    {
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public bool CanSend { get; set; }
    }

    public class ForestOrganizationInquiryViewModel : EntityState
    {
        public ForestOrganizationInquiryViewModel()
        {
            FOI_Points = new List<ForestOrganizationInquiryPointViewModel>();
            FOI_Files = new List<ForestOrganizationInquiryFileViewModel>();
        }
        public ForestOrganizationExtraParam ExtraParams { get; set; }        
        public string FOI_Id { get; set; }
        public string FOI_No { get; set; } = null!;
        public string FOI_TrackingCode { get; set; }
        public string FOI_UniqueNo { get; set; }
        public string FOI_SendDate { get; set; }
        public string FOI_SendTime { get; set; }
        public string FOI_SerialNo { get; set; }
        public string FOI_CreateDate { get; set; } = null!;
        public string FOI_CreateTime { get; set; } = null!;
        public string FOI_LetterDate { get; set; }
        public string FOI_LetterNo { get; set; }
        public string FOI_EstatePostCode { get; set; }
        public string FOI_EstateAddress { get; set; }
        public decimal? FOI_EstateArea { get; set; }
        public string FOI_EstateBasic { get; set; }
        public string FOI_EstateSecondary { get; set; }
        public string FOI_EstateSeparate { get; set; }
        public IList<string> FOI_EstateInquiryId { get; set; }
        public int FOI_EstateInquiryTimeStamp { get; set; }
        public IList<string> FOI_CityId { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string StatusTitle { get; set; }
        public IList<string> FOI_ProvinceId { get; set; } = null!;
        public IList<string> FOI_SectionId { get; set; }
        public IList<string> FOI_ScriptoriumId { get; set; } = null!;
        public decimal FOI_Timestamp { get; set; }
        public ForestOrganizationInquiryPersonViewModel FOI_Person { get; set; }
        public List<ForestOrganizationInquiryPointViewModel> FOI_Points { get; set; }
        public List<ForestOrganizationInquiryFileViewModel> FOI_Files { get; set; }
        public ForestOrganizationInquiryResponseViewModel FOI_Response { get; set; }
        public string ResponseHTML { get; set; }

        public string FOI_IncommingLetterNo { get; set; }
        public string FOI_Defect_Text { get; set; }
    }

    public class ForestOrganizationInquiryPersonViewModel : EstateBasePersonViewModel
    {
       
        
    }
    public class ForestOrganizationInquiryPointViewModel : EntityState
    {

        public string FOI_PointId { get; set; }

        public decimal FOI_PointX { get; set; }

        public decimal FOI_PointY { get; set; }

        public short FOI_PointZone { get; set; }

        public decimal Timestamp { get; set; }

    }

    public class ForestOrganizationInquiryFileViewModel : EntityState
    {
        public string FOI_FileId { get; set; }
        public string FOI_FileAttachmentNo { get; set; }
        public string FOI_FileAttachmentTitle { get; set; }
        public byte[] FOI_FileFileContent { get; set; }
        public string FOI_FileExtention { get; set; }
        public string FOI_FileDescription { get; set; }
        public decimal Timestamp { get; set; }

    }

    public class ForestOrganizationInquiryResponsePointViewModel : EntityState
    {
        public string FOI_ResponsePointId { get; set; }

        public string FOI_ResponsePointLat { get; set; }

        public string FOI_ResponsePointLng { get; set; }

        public string FOI_ResponsePointGroupCode { get; set; }
        public decimal Timestamp { get; set; }

    }
    public class ForestOrganizationInquiryResponseViewModel : EntityState
    {
        public ForestOrganizationInquiryResponseViewModel()
        {
            Points = new List<ForestOrganizationInquiryResponsePointViewModel>();
        }
        public string ResponseId { get; set; }
        public string ForestOrganizationResponseDate { get; set; }
        public string ForestOrganizationResponseTime { get; set; }
        public string ForestOrganizationModifyDate { get; set; }
        public string ForestOrganizationModifyTime { get; set; }
        public string ForestOrganizationModifyUser { get; set; }
        public string ResponseShapeType { get; set; }
        public string ResponseShapeArea { get; set; }
        public byte[] ResponsePdfFile { get; set; }
        public decimal Timestamp { get; set; }

        

        public IList<string> ResponseCityId { get; set; }

        public IList<string> ResponseProvinceId { get; set; }

        public IList<string> ResponseSectionId { get; set; }

        public IList<string> ResponseTypeId { get; set; } = null!;

        public string ResponseCreateDate { get; set; }
        public string ResponseCreateTime { get; set; }

        public List<ForestOrganizationInquiryResponsePointViewModel> Points { get; set; }


    }

}
