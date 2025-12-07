using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpExternalServiceCaller;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.SharedModels;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
using System;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;

namespace Notary.SSAA.BO.QueryHandler.Document
{
    public class IsAccessTimeValidQueryHandler : BaseQueryHandler<IsAccessTimeValidًQuery, ApiResult<ONotaryAccessTimeValidationOutputMessage>>
    {
        private readonly IHttpExternalServiceCaller _httpExternalServiceCaller;
        private IRepository<ImportantActionTypeIllegalTime> _importantActionTypeIllegalTime;
        private IRepository<ImportantActionType> _importantActionTypeRepository;
        private ISSARApplicationConfigurationRepository _SSARApplicationConfigurationRepository;
        private IDateTimeService _dateTimeService;
        private readonly IConfiguration _configuration;


        public IsAccessTimeValidQueryHandler ( IMediator mediator, IUserService userService, IDateTimeService dateTimeService, IHttpExternalServiceCaller httpExternalServiceCaller,
            IRepository<ImportantActionTypeIllegalTime> importantActionTypeIllegalTime, IRepository<ImportantActionType> importantActionTypeRepository , ISSARApplicationConfigurationRepository SSARApplicationConfigurationRepository, IConfiguration configuration ) : base(mediator, userService)
        {
            _importantActionTypeIllegalTime = importantActionTypeIllegalTime;
            _importantActionTypeRepository = importantActionTypeRepository;
            _dateTimeService =dateTimeService;
            _httpExternalServiceCaller=httpExternalServiceCaller;
            _configuration = configuration;
            _SSARApplicationConfigurationRepository = SSARApplicationConfigurationRepository;
        }

        protected override bool HasAccess( IsAccessTimeValidًQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ONotaryAccessTimeValidationOutputMessage>> RunAsync( IsAccessTimeValidًQuery request, CancellationToken cancellationToken)
        {
            ONotaryAccessTimeValidationOutputMessage oNotaryAccessTimeValidationOutputMessage=new ONotaryAccessTimeValidationOutputMessage();
            oNotaryAccessTimeValidationOutputMessage.IsAccess = true;
            oNotaryAccessTimeValidationOutputMessage.Message = String.Empty;

                
            ApiResult<ONotaryAccessTimeValidationOutputMessage> apiResult = new();

            if ( System.Diagnostics.Debugger.IsAttached )
            {
                oNotaryAccessTimeValidationOutputMessage.IsAccess = true;
                oNotaryAccessTimeValidationOutputMessage.Message = "محیط تستی می باشد.";
                apiResult.IsSuccess = true;
                apiResult.Data = oNotaryAccessTimeValidationOutputMessage;
                return apiResult;
            }
            bool accessTimeValidationEnabled = Settings.AccessTimeValidationEnabled;
            if ( accessTimeValidationEnabled )
            {
                oNotaryAccessTimeValidationOutputMessage.IsAccess = true;
                oNotaryAccessTimeValidationOutputMessage.Message = "محیط تستی می باشد.";
                apiResult.IsSuccess = true;
                apiResult.Data = oNotaryAccessTimeValidationOutputMessage;
                return apiResult;

            }
            GetOrganizationHierarchyQuery getOrganizationHierarchyQuery = new GetOrganizationHierarchyQuery(_userService.UserApplicationContext.ScriptoriumInformation.Id,_userService.UserApplicationContext.ScriptoriumInformation.Unit.Id);
            var actionType =await _importantActionTypeRepository.TableNoTracking
                .Where(t => t.Id == request.ONotaryActionTypeId ).FirstOrDefaultAsync(cancellationToken);

            if ( actionType == null)
            {
                oNotaryAccessTimeValidationOutputMessage.IsAccess = true;
                oNotaryAccessTimeValidationOutputMessage.Message =string.Empty;
                apiResult.IsSuccess = true;
                apiResult.Data = oNotaryAccessTimeValidationOutputMessage;
                return apiResult;
            }

            if ( actionType.IsIllegalInHolidays==YesNo.Yes.GetString())
            {

                string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl");
                var queryParameters = new Dictionary<string, string>();
                queryParameters.Add ( "inputDate", _dateTimeService.CurrentPersianDate );
                string baseinfo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
                var httpRequest = new HttpExternalServiceRequest(mainUrl + baseinfo_Apis_prefix + "api/v1/Specific/Ssar/Calendar/CheckHoliday", queryParameters);

                var result =
                    await _httpExternalServiceCaller.CallExternalServiceGetApiAsync<ApiResult<CalendarEventViewModel>>(
                        httpRequest
                        , cancellationToken);
                if (result.IsSuccess && result.Data!=null)
                {
                    oNotaryAccessTimeValidationOutputMessage.Message = "چون امروز " + result.Data.Title + " است؛ " + actionType.Title + " در روز تعطیل غیرمجاز است.";
                    oNotaryAccessTimeValidationOutputMessage.IsAccess = false;

                    apiResult.Data = oNotaryAccessTimeValidationOutputMessage;
                    apiResult.IsSuccess = true;
                    return apiResult;
                }
               


               


            }

            string dayOfWeek = await _SSARApplicationConfigurationRepository.GetDayOfWeek(cancellationToken);
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
            var sendMessageResult =
                    await _httpExternalServiceCaller
                        .CallExternalServicePostApiAsync<ApiResult<OrganizationHierarchyViewModel>,
                            GetOrganizationHierarchyQuery>(new HttpExternalServiceRequest<GetOrganizationHierarchyQuery>
                            (_configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix+ "api/v1/Specific/Ssar/Common/GetOrganizationHierarchy",
                                 getOrganizationHierarchyQuery),
                            cancellationToken);


                if ( sendMessageResult.IsSuccess )
                {
                    request.OrganizationHierarchy = sendMessageResult.Data.OrganizationHierarchy.Select ( t => t.OrganizationId ).ToArray ();

                    var isExist=  await _importantActionTypeIllegalTime.TableNoTracking.CountAsync (t =>
                        t.ToDate.CompareTo(_dateTimeService.CurrentPersianDate) > 0 && t.FromDate.CompareTo(_dateTimeService
                            .CurrentPersianDate) < 0
                        && t.ToTime.CompareTo(_dateTimeService
                            .CurrentTime) > 0 &&
                        t.FromTime.CompareTo(_dateTimeService
                            .CurrentTime) < 0 &&
                        t.DayOfWeek==dayOfWeek &&
                        t.ImportantActionTypeId ==
                        request.ONotaryActionTypeId &&
                        (t.OrganizationId == null ||
                         request.OrganizationHierarchy.Contains(

                             t.OrganizationId)),cancellationToken)>0;

                    if ( isExist )
                    {

                        oNotaryAccessTimeValidationOutputMessage.Message = "زمان جاری خارج از ساعات اداری دفترخانه است؛ " + actionType.Title + " ممکن نیست.";
                    }

                    oNotaryAccessTimeValidationOutputMessage.IsAccess = !isExist;

                }

            apiResult.Data = oNotaryAccessTimeValidationOutputMessage;
            apiResult.IsSuccess = true;
            return apiResult;



        }
    }
}
