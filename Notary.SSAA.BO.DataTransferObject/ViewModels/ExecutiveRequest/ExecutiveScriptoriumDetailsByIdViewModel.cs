using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveRequest
{
    public class ExecutiveScriptoriumDetailsByIdViewModel : BaseCommandRequest<ApiResult>
    {
        public ExecutiveScriptoriumDetailsByIdViewModel()
        {

            Items = new();
        }
        public List<ScriptoriumDetailsItem> Items { get; set; }
    }
    public class ScriptoriumDetailsItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
    }
}
