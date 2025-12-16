using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal sealed class DocumentSelselehAyadiGridQueryHandler : BaseQueryHandler<DocumentSelselehAyadiGridQuery, ApiResult<DocumentSelselehAyadiGridViewModel>>
    {
        private readonly IDocumentRepository _documentRepository;


        private ApiResult<DocumentSelselehAyadiGridViewModel> apiResult;

        public DocumentSelselehAyadiGridQueryHandler(IMediator mediator, IUserService userService,
            IDocumentRepository documentRepository)
            : base(mediator, userService)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            apiResult = new ApiResult<DocumentSelselehAyadiGridViewModel>();
        }

        protected override bool HasAccess(DocumentSelselehAyadiGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentSelselehAyadiGridViewModel>> RunAsync(DocumentSelselehAyadiGridQuery request, CancellationToken cancellationToken)
        {
            DocumentSelselehAyadiGridViewModel result = new();

            var excludedScriptoriumIds = new List<string>
{
    "0112b846e35c4f4781ee50f44a180df6",
    "71f804e50de34193b44d528cb132ad08",
    "f172c6a6d79542d586d7cacda5fcb2d1",
    "907420174D8742F2B507C90971DAE4B9",
    "D778A64A6B04464AA88B4DED3DAC39D5",
    "D4C6650EA90C4F7BAB8A223F40F4A9F3",
    "C77A794C3C8F433FB2F5F676D8C6E4BA",
    "F4C02E98FD6D4F2A81098E4FE27FC5E6",
    "4833080a942a4d7cbbc720ce79f41e51",
    "e6e09dab510c4589b2c19dc9a732b062",
    "cf2f7bd46a3448a0a87ed55f2dd81fe5"
};

            var databaseresult = await _documentRepository.GetSelselehAyadi(request.DocumentEstateSubSectionId, request.DocumentEstateSectionId, request.DocumentUnitId, request.SecondaryPlaque, request.BasicPlaque, excludedScriptoriumIds, cancellationToken);
            if (databaseresult != null && databaseresult.Count > 0)
            {
                List<string> scriptoriumIdList = new();
                foreach (var document in databaseresult)
                {
                    scriptoriumIdList.Add(document.ScriptoriumId);

                }
                var scritoriumList = await GetScriptoriumById(scriptoriumIdList.ToArray(), cancellationToken);
                for (int i = 0; i < databaseresult.Count; i++)
                {
                    var scriptorium = scritoriumList.ScriptoriumList.FirstOrDefault(s => s.Id == databaseresult[i].ScriptoriumId);
                    databaseresult[i].ScriptoriumName = scriptorium!=null ?("دفتر " + scriptorium.ScriptoriumNo + " " + scriptorium.GeoLocationName):null;
                }
            }
            result.GridItems = databaseresult;
            apiResult.Data = result;
            return apiResult;
        }

        public async Task<GetScriptoriumByIdViewModel> GetScriptoriumById(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetScriptoriumByIdQuery(scriptoriumId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }
    }
}


