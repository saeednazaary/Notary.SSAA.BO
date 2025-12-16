using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices.Estate
{
    public class ConfirmDocumentByElectronicEstateNoteNoQueryHandler : BaseQueryHandler<ConfirmDocumentByElectronicEstateNoteNoQuery, ApiResult<ConfirmDocumentByElectronicEstateNoteNoViewModel>>
    {
        
        private readonly IEstateSectionRepository _sectionRepository;
        private readonly IEstateSubSectionRepository _subSectionRepository;
        private readonly IRepository<SsrArticle6EstateType> _article6EstateTypeRepository;
        private readonly IRepository<SsrArticle6EstateUsing> _article6EstateUsingRepository;
        private readonly IRepository<SsrArticle6County> _article6Countyrepository;
        private readonly IRepository<SsrArticle6Province> _article6ProvinceRepository;
        private readonly IDateTimeService _dateTimeService;

        public ConfirmDocumentByElectronicEstateNoteNoQueryHandler(
            IMediator mediator, 
            IUserService userService,
            IDateTimeService dateTimeService,
            IEstateSectionRepository estateSectionRepository,
            IEstateSubSectionRepository estateSubSectionRepository,
            IRepository<SsrArticle6EstateType> article6EstateTypeRepository,
            IRepository<SsrArticle6EstateUsing> article6EstateUsingRepository,
            IRepository<SsrArticle6County> article6Countyrepository,
            IRepository<SsrArticle6Province> article6ProvinceRepository)
            : base(mediator, userService)
        {

            _sectionRepository = estateSectionRepository;
            _subSectionRepository = estateSubSectionRepository;
            _dateTimeService = dateTimeService;
            _article6EstateUsingRepository = article6EstateUsingRepository;
            _article6Countyrepository = article6Countyrepository;
            _article6ProvinceRepository = article6ProvinceRepository;
            _article6EstateTypeRepository = article6EstateTypeRepository;
        }
        protected override bool HasAccess(ConfirmDocumentByElectronicEstateNoteNoQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<ConfirmDocumentByElectronicEstateNoteNoViewModel>> RunAsync(ConfirmDocumentByElectronicEstateNoteNoQuery request, CancellationToken cancellationToken)
        {
            var input = new ConfirmDocumentByElectronicEstateNoteNoServiceInput()
            {                
                ElectronicEstateNoteNo = request.ElectronicEstateNoteNo,
                NationalityCode = request.NationalityCode,
                RequestDateTime = _dateTimeService.CurrentPersianDateTime
            };
            var result = await _mediator.Send(input, cancellationToken);
            return await CorrectResult(request, result, cancellationToken);
        }

        private async Task<ApiResult<ConfirmDocumentByElectronicEstateNoteNoViewModel>> CorrectResult(ConfirmDocumentByElectronicEstateNoteNoQuery request,ApiResult<ConfirmDocumentByElectronicEstateNoteNoViewModel> result,CancellationToken cancellationToken)
        {
            if (result.IsSuccess)
            {
                if (result.Data != null)
                {
                    if (result.Data.Successful && request.UIForm != ConfirmDocumentByElectronicEstateNoteNoUIForm.None)
                    {
                        
                        var section = await _sectionRepository.TableNoTracking.Where(x => x.LegacyId == result.Data.SectionID ).FirstOrDefaultAsync(cancellationToken);
                        var subSection = await _subSectionRepository.TableNoTracking.Where(x => x.LegacyId == result.Data.SubSectionID).FirstOrDefaultAsync(cancellationToken);
                        result.Data.SectionID = (section != null) ? section.Id : "";
                        result.Data.SubSectionID = (subSection != null) ? subSection.Id : "";
                        result.Data.UnitId = (section != null) ? section.UnitId : "";
                        result.Data.CountyId = "";
                        if (!string.IsNullOrWhiteSpace(result.Data.ProvinceName))
                        {
                            var arabicProviceName = result.Data.ProvinceName.NormalizeTextChars();
                            var farsiProvinceName = result.Data.ProvinceName.NormalizeTextChars(false);
                            var province = await _article6ProvinceRepository.TableNoTracking.Where(x => x.Title == arabicProviceName || x.Title == farsiProvinceName).FirstOrDefaultAsync(cancellationToken);
                            if (province != null)
                            {
                                result.Data.ProvinceId = province.Id;
                            }
                            else
                                result.Data.ProvinceId = "";
                        }
                        else
                            result.Data.ProvinceId = "";
                        if (!string.IsNullOrWhiteSpace(result.Data.EPieceTypeTitle))
                        {
                            var arabicEstateType = result.Data.EPieceTypeTitle.NormalizeTextChars();
                            var farsiEstateType = result.Data.EPieceTypeTitle.NormalizeTextChars(false);
                            var estateType = await _article6EstateTypeRepository.TableNoTracking.Where(x => x.Title == arabicEstateType || x.Title == farsiEstateType).FirstOrDefaultAsync(cancellationToken);
                            if (estateType != null)
                            {
                                result.Data.EstateTypeId = estateType.Id;
                            }
                            else
                                result.Data.EstateTypeId = "";
                        }
                        else
                            result.Data.EstateTypeId = "";
                        if (!string.IsNullOrWhiteSpace(result.Data.EstateUsingType))
                        {
                            var arabicEstateUsingType = result.Data.EstateUsingType.NormalizeTextChars();
                            var farsiEstateUsingType = result.Data.EstateUsingType.NormalizeTextChars(false);
                            var estateUsingType = await _article6EstateUsingRepository.TableNoTracking.Where(x => x.Title == arabicEstateUsingType || x.Title == farsiEstateUsingType).FirstOrDefaultAsync(cancellationToken);
                            if (estateUsingType != null)
                            {
                                result.Data.EstateUsingTypeId = estateUsingType.Id;
                            }
                            else
                                result.Data.EstateUsingTypeId = "";
                        }
                        else
                            result.Data.EstateUsingTypeId = "";
                    }
                }
            }
            return result;
        }


    }
}
