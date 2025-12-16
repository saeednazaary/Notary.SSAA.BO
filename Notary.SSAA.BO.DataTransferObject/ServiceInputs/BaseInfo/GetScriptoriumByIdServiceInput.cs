

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo
{

    public class GetScriptoriumByIdServiceInput
    {
        public GetScriptoriumByIdServiceInput(string[] idList)
        {
            IdList = idList;
        }
        public string[] IdList { get; set; }
    }

}

