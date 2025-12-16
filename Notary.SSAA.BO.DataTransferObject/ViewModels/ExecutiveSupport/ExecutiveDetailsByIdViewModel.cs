using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveSupport
{
    public class ExecutiveDetailsByIdViewModel : BaseCommandRequest<ApiResult>
    {
        public ExecutiveDetailsByIdViewModel()
        {

            Items = new();
        }
        public List<DetailsItem> Items { get; set; }
    }
    public class DetailsItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
    }
}
