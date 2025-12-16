using Azure;
using Mapster;
using MediatR;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateTaxInquiry
{
    public class ExistsActiveEstateTaxInquiryQueryHandler : BaseQueryHandler<ExistsActiveEstateTaxInquiryQuery, ApiResult<ExistsActiveEstateTaxInquiryViewModel>>
    {
        private readonly IEstateTaxInquiryRepository _estatetaxInquiryRepository;



        public ExistsActiveEstateTaxInquiryQueryHandler(IMediator mediator, IUserService userService,
            IEstateTaxInquiryRepository estateTaxInquiryRepository)
            : base(mediator, userService)
        {

            _estatetaxInquiryRepository = estateTaxInquiryRepository;

        }
        protected override bool HasAccess(ExistsActiveEstateTaxInquiryQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<ExistsActiveEstateTaxInquiryViewModel>> RunAsync(ExistsActiveEstateTaxInquiryQuery request, CancellationToken cancellationToken)
        {

            ApiResult<ExistsActiveEstateTaxInquiryViewModel> apiResult = new() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };

            Notary.SSAA.BO.Domain.Entities.EstateTaxInquiry estateTaxInquiry =  await _estatetaxInquiryRepository.Entities
                       .Include(x => x.WorkflowStates)
                       .Where(x =>  x.EstateInquiryId == request.EstateInquiryId.ToGuid() && x.IsActive == EstateConstant.BooleanConstant.True)
                       .FirstOrDefaultAsync(cancellationToken);
            if (estateTaxInquiry != null)
            {
                if (estateTaxInquiry.WorkflowStates.State == "0" || estateTaxInquiry.WorkflowStates.State == "8")
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add(string.Format("امکان ثبت وجود ندارد.استعلام مشابه  به شماره {0} و تاریخ {1} قبلا ثبت شده است که قابل ویرایش و ارسال  می باشد .لطفا از همان استعلام جهت ویرایش و ارسال به سازمان امور مالیاتی استفاده کنید ", estateTaxInquiry.No, estateTaxInquiry.CreateDate));
                    
                }
                else
                {
                    apiResult.Data = new ExistsActiveEstateTaxInquiryViewModel() { EstateTaxInquiryId = estateTaxInquiry.Id.ToString() };
                }
                
            }

            return apiResult;
        }

        public async Task<GetUnitByIdViewModel> GetUnitById(string[] unitId, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(new GetUnitByIdQuery(unitId), cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }

        }

        public async Task<GetGeolocationByIdViewModel> GetGeoLocationById(int[] geoLocationId, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(new GetGeolocationByIdQuery(geoLocationId), cancellationToken);
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
