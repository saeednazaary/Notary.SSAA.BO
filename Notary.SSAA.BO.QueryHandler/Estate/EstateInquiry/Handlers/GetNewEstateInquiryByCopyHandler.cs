using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetNewEstateInquiryByCopyHandler : BaseQueryHandler<GetNewEstateInquiryByCopyQuery, ApiResult<EstateInquiryViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;



        public GetNewEstateInquiryByCopyHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository)
            : base(mediator, userService)
        {

            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
        }
        protected override bool HasAccess(GetNewEstateInquiryByCopyQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<EstateInquiryViewModel>> RunAsync(GetNewEstateInquiryByCopyQuery request, CancellationToken cancellationToken)
        {
            EstateInquiryViewModel result = new();
            ApiResult<EstateInquiryViewModel> apiResult = new();

            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.EstateInquiryId.ToGuid());
            if (estateInquiry != null)
            {
                await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquiryPeople, cancellationToken);



                result = EstateInquiryMapper.EntityToViewModel(estateInquiry);
                result.InqId = string.Empty;
                result.InqResponse = string.Empty;
                result.InqInquiryNo = string.Empty;
                result.InqInquiryDate = string.Empty;
                result.InqFirstSendDate = string.Empty;
                result.InqLastSendTime = string.Empty;
                result.InqFirstSendTime = string.Empty;
                result.InqLastSendDate = string.Empty;
                result.InqResponseDate = string.Empty;
                result.InqResponseDigitalsignature = null;
                result.InqEstateInquiryId = new List<string>();
                result.InqResponseNumber = string.Empty;
                result.InqSpecificStatus = string.Empty;
                result.InqResponseResult = string.Empty;
                result.InqResponseSubjectdn = string.Empty;
                result.InqResponseTime = string.Empty;
                result.InqCreateDate = string.Empty;
                result.InqCreateTime = string.Empty;
                result.InqTimestamp = 0;
                result.InqNo= string.Empty;                
                var inquiryPersonViewModel = EstateInquiryMapper.EntityToViewModel(estateInquiry.EstateInquiryPeople.First());
                inquiryPersonViewModel.PersonId = string.Empty;
                inquiryPersonViewModel.PersonTimestamp = 0;
                result.InqInquiryPerson = inquiryPersonViewModel;
                result.IsValid = true;
                result.IsNew = true;
                result.InqInquiryPerson.IsValid = true;
                result.InqInquiryPerson.IsNew = true;
                result.ExtraParams = new EstateInquiryExtraParam
                {
                    CanDelete = false,
                    CanEdit = true,
                    CanSend = false,
                    CanArchive = false,
                    CanDeArchive = false
                };
                apiResult.Data = result;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام ملک مربوطه یافت نشد");
            }
            return apiResult;
        }

    }
}
