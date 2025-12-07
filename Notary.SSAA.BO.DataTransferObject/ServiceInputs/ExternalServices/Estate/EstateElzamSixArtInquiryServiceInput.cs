using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    
    
    public class EstateElzamSixArtInquiryFile
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class EstateElzamSixArtInquiryGeometry
    {
        public string Type { get; set; }
        public List<List<List<double>>> Coordinates { get; set; }
    }

    

    public class EstateElzamSixArtInquiryServiceInput : BaseExternalRequest<ApiResult<EstateElzamSixArtInquiryViewModel>>
    {
        public EstateElzamSixArtInquiryServiceInput()
        {
            ClientId = "SSAR";
        }
        public string ClientId { get; set; }               
        public EstateElzamSixArtInquiryServiceInputData SendData { get; set; }
    }
    public class EstateElzamSixArtInquiryServiceInputData
    {
        public EstateElzamSixArtInquiryServiceInputData()
        {
            InqOrgan = new List<string>();
            SellerNationalCode = string.Empty;
            BuyerNationalCode = string.Empty;
            UnitNumberSdd = string.Empty;
            OriginalNumberSdd = string.Empty;
            SubNumberSdd = string.Empty;
            TotalShare = string.Empty;
            FracShare = string.Empty;
            SabtTrackingCode = string.Empty;
            RequestDate = string.Empty;
            PostalCode = string.Empty;
            PartIdSdd = string.Empty;
            CountyIdSdd = string.Empty;
        }
        public EstateElzamSixArtInquiryGeometry Geometry { get; set; }
        public int ProvinceCode { get; set; }
        public int CountyCode { get; set; }
        public string SellerNationalCode { get; set; }
        public string BuyerNationalCode { get; set; }
        public string UnitNumberSdd { get; set; }
        public string OriginalNumberSdd { get; set; }
        public string SubNumberSdd { get; set; }
        public string TotalShare { get; set; }
        public string FracShare { get; set; }
        public string SabtTrackingCode { get; set; }
        public string RequestDate { get; set; }
        public int OwnerType { get; set; }
        public int UserCode { get; set; }
        public decimal AreaT { get; set; }
        public int RequestType { get; set; }
        //public int TypeDocumentSdd { get; set; }
        public EstateElzamSixArtInquiryFile File { get; set; }        
        public string PostalCode { get; set; }
        public string CountyIdSdd { get; set; }
        public string PartIdSdd { get; set; }
        public List<string> InqOrgan { get; set; }        
    }

   

}
