

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo
{

    public class GetExordiumByNationalNoServiceInput
    {
        public GetExordiumByNationalNoServiceInput(string nationalNo)
        {
            NationalNo = nationalNo;
        }
        public string NationalNo { get; set; }
    }

}

