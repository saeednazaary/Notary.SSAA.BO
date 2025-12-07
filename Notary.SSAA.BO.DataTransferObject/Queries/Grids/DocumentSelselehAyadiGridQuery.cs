using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Grids
{
    public class DocumentSelselehAyadiGridQuery : BaseQueryRequest<ApiResult<DocumentSelselehAyadiGridViewModel>>
    {
        public string BasicPlaque { get; set; }
        public string SecondaryPlaque { get; set; }
        //حوزه ثبتی
        public string DocumentUnitId { get; set; }
        //بخش ثبتی
        public string DocumentEstateSectionId { get; set; }
        //ناحیه ثبتی
        public string DocumentEstateSubSectionId { get; set; }
    }
}
