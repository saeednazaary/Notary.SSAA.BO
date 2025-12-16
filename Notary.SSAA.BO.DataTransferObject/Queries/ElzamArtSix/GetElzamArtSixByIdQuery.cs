using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSix;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ElzamArtSix
{
    public class GetElzamArtSixByIdQuery : BaseQueryRequest<ApiResult<ElzamArtSixViewModel>>
    {
        public GetElzamArtSixByIdQuery(string elzamartsixId)
        {
            ElzamArtSixId = Guid.Parse(elzamartsixId);
        }
        public Guid ElzamArtSixId { get; set; }
    }
}