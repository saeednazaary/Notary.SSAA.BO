using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Globalization;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class ValidateEstateInquiryForDealSummaryQueryHandler : BaseQueryHandler<ValidateEstateInquiryForDealSummaryQuery, ApiResult<List<EstateInquiryValidationResultViewModel>>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        ApiResult<List<EstateInquiryValidationResultViewModel>> apiResult = null;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        private readonly IConfiguration _configuration;
        public ValidateEstateInquiryForDealSummaryQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository,IConfiguration configuration)
            : base(mediator, userService)
        {
            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
            _configuration = configuration;
        }
        protected override bool HasAccess(ValidateEstateInquiryForDealSummaryQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<List<EstateInquiryValidationResultViewModel>>> RunAsync(ValidateEstateInquiryForDealSummaryQuery request, CancellationToken cancellationToken)
        {
            apiResult = new();
            apiResult.IsSuccess = false;
            apiResult.statusCode = ApiResultStatusCode.Success;
            apiResult.Data = new List<EstateInquiryValidationResultViewModel>();
            foreach (var inquiryId in request.EstateInquiryId)
            {
                await ValidateEstateInquiry(inquiryId, request.IsRestrictedDealSummary, cancellationToken);
            }
            if (apiResult.message.Count == 0)
                apiResult.IsSuccess = true;
            else
                apiResult.IsSuccess = false;
            return apiResult;
        }

        private async Task ValidateEstateInquiry(string estateInquiryId, bool IsRestrictedDealSummary, CancellationToken cancellationToken)
        {
            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, estateInquiryId.ToGuid());
            ApiResult localApiResult = new ApiResult() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };
            if (estateInquiry != null)
            {
                var inquiryTitle = string.Format("استعلام شماره {0} به تاریخ {1}", estateInquiry.InquiryNo, estateInquiry.InquiryDate);
                await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquiryPeople, cancellationToken);

                var inquiryUnit = await _baseInfoServiceHelper.GetUnitById(new string[] { estateInquiry.UnitId }, cancellationToken);
                var inquiryScriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { estateInquiry.ScriptoriumId }, cancellationToken);
                var inquiryPerson = estateInquiry.EstateInquiryPeople.First();
                string[] states = new string[] { EstateConstant.EstateInquiryStates.ConfirmResponse,
                                             EstateConstant.EstateInquiryStates.Archived,
                                             EstateConstant.EstateInquiryStates.RejectResponse};
                string[] specificStatusList = new string[] {
                    EstateConstant.EstateInquirySpecificStatus.OnlyAllowedForDivision,
                    EstateConstant.EstateInquirySpecificStatus.Arrested,
                    EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision};
                var toDate = DateTime.Now;
                var fromDate1 = toDate.AddDays(-37);
                var fromDate2 = toDate.AddDays(-50);
                if (!IsRestrictedDealSummary)
                {
                    if (inquiryScriptorium.ScriptoriumList[0].GeoLocationId == inquiryUnit.UnitList[0].GeoLocationId)
                    {
                        if (states.Contains(estateInquiry.WorkflowStatesId))
                        {
                            if (string.IsNullOrWhiteSpace(estateInquiry.AttachedToDealsummary))
                            {
                                if (estateInquiry.ResponseResult == "True")
                                {
                                    if (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || !specificStatusList.Contains(estateInquiry.SpecificStatus))
                                    {
                                        var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                                        if (estateInquiryResponseDateTime >= fromDate1 && estateInquiryResponseDateTime <= toDate)
                                        {
                                            localApiResult.IsSuccess = true;
                                        }
                                        else
                                        {
                                            localApiResult.IsSuccess = false;
                                            localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم خلاصه معامله تمام شده است", inquiryTitle));
                                        }
                                    }
                                    else
                                    {
                                        if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.OnlyAllowedForDivision)
                                        {
                                            localApiResult.IsSuccess = false;
                                            localApiResult.message.Add(string.Format("ملک مورد {0} ، مورد تفکیک یا افراز و یا تجمیع قرار گرفته است", inquiryTitle));
                                        }
                                        else if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested)
                                        {
                                            localApiResult.IsSuccess = false;
                                            localApiResult.message.Add(string.Format("مالکیت ملک مورد {0} بازداشت است", inquiryTitle));
                                        }
                                        else if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision)
                                        {
                                            localApiResult.IsSuccess = false;
                                            localApiResult.message.Add(string.Format("ملک مورد {0} ، مورد تفکیک یا افراز و یا تجمیع قرار گرفته است", inquiryTitle));
                                            localApiResult.message.Add(string.Format("مالکیت ملک مورد {0} بازداشت است", inquiryTitle));
                                        }
                                    }
                                }
                                else if ((string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested) && inquiryPerson.ExecutiveTransfer == EstateConstant.BooleanConstant.True)
                                {
                                    var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                                    if (estateInquiryResponseDateTime >= fromDate1 && estateInquiryResponseDateTime <= toDate)
                                    {
                                        localApiResult.IsSuccess = true;
                                    }
                                    else
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم خلاصه معامله تمام شده است", inquiryTitle));
                                    }
                                }
                                else
                                {
                                    localApiResult.IsSuccess = false;
                                    localApiResult.message.Add(string.Format("{0} پاسخ تایید نگرفته است", inquiryTitle));
                                }
                            }
                            else
                            {
                                localApiResult.IsSuccess = false;
                                localApiResult.message.Add(string.Format("خلاصه معامله مالکیت/تقسیم نامه برای  {0} ثبت شده است", inquiryTitle));
                            }
                        }
                        else
                        {
                            localApiResult.IsSuccess = false;
                            localApiResult.message.Add(string.Format("{0} در وضعیت پاسخ داده شده نیست", inquiryTitle));
                        }
                    }
                    else
                    {
                        if (states.Contains(estateInquiry.WorkflowStatesId))
                        {
                            if (string.IsNullOrWhiteSpace(estateInquiry.AttachedToDealsummary))
                            {
                                if (estateInquiry.ResponseResult == "True")
                                {
                                    if (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || !specificStatusList.Contains(estateInquiry.SpecificStatus))
                                    {
                                        var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                                        if (estateInquiryResponseDateTime >= fromDate2 && estateInquiryResponseDateTime <= toDate)
                                        {
                                            localApiResult.IsSuccess = true;
                                        }
                                        else
                                        {
                                            localApiResult.IsSuccess = false;
                                            localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم خلاصه معامله تمام شده است", inquiryTitle));
                                        }
                                    }
                                    else
                                    {
                                        if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.OnlyAllowedForDivision)
                                        {
                                            localApiResult.IsSuccess = false;
                                            localApiResult.message.Add(string.Format("ملک مورد {0} ، مورد تفکیک یا افراز و یا تجمیع قرار گرفته است", inquiryTitle));
                                        }
                                        else if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested)
                                        {
                                            localApiResult.IsSuccess = false;
                                            localApiResult.message.Add(string.Format("مالکیت ملک مورد {0} بازداشت است", inquiryTitle));
                                        }
                                        else if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision)
                                        {
                                            localApiResult.IsSuccess = false;
                                            localApiResult.message.Add(string.Format("ملک مورد {0} ، مورد تفکیک یا افراز و یا تجمیع قرار گرفته است", inquiryTitle));
                                            localApiResult.message.Add(string.Format("مالکیت ملک مورد {0} بازداشت است", inquiryTitle));
                                        }
                                    }
                                }
                                else if ((string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested) && inquiryPerson.ExecutiveTransfer == EstateConstant.BooleanConstant.True)
                                {
                                    var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                                    if (estateInquiryResponseDateTime >= fromDate2 && estateInquiryResponseDateTime <= toDate)
                                    {
                                        localApiResult.IsSuccess = true;
                                    }
                                    else
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم خلاصه معامله تمام شده است", inquiryTitle));
                                    }
                                }
                                else
                                {
                                    localApiResult.IsSuccess = false;
                                    localApiResult.message.Add(string.Format("{0} پاسخ تایید نگرفته است", inquiryTitle));
                                }
                            }
                            else
                            {
                                localApiResult.IsSuccess = false;
                                localApiResult.message.Add(string.Format("خلاصه معامله مالکیت/تقسیم نامه برای این {0} ثبت شده است", inquiryTitle));
                            }
                        }
                        else
                        {
                            localApiResult.IsSuccess = false;
                            localApiResult.message.Add(string.Format("{0} در وضعیت پاسخ داده شده نیست", inquiryTitle));
                        }
                    }
                }
                else
                {
                    if (inquiryScriptorium.ScriptoriumList[0].GeoLocationId == inquiryUnit.UnitList[0].GeoLocationId)
                    {
                        if (states.Contains(estateInquiry.WorkflowStatesId))
                        {

                            if (estateInquiry.ResponseResult == "True")
                            {
                                if (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || !specificStatusList.Contains(estateInquiry.SpecificStatus))
                                {
                                    var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                                    if (estateInquiryResponseDateTime >= fromDate1 && estateInquiryResponseDateTime <= toDate)
                                    {
                                        localApiResult.IsSuccess = true;
                                    }
                                    else
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم خلاصه معامله تمام شده است", inquiryTitle));
                                    }
                                }
                                else
                                {
                                    if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.OnlyAllowedForDivision)
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("ملک مورد {0} ، مورد تفکیک یا افراز و یا تجمیع قرار گرفته است", inquiryTitle));
                                    }
                                    else if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested)
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("مالکیت ملک مورد {0} بازداشت است", inquiryTitle));
                                    }
                                    else if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision)
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("ملک مورد {0} ، مورد تفکیک یا افراز و یا تجمیع قرار گرفته است", inquiryTitle));
                                        localApiResult.message.Add(string.Format("مالکیت ملک مورد {0} بازداشت است", inquiryTitle));
                                    }
                                }
                            }
                            else if ((string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested) && inquiryPerson.ExecutiveTransfer == EstateConstant.BooleanConstant.True)
                            {
                                var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                                if (estateInquiryResponseDateTime >= fromDate1 && estateInquiryResponseDateTime <= toDate)
                                {

                                    localApiResult.IsSuccess = true;
                                }
                                else
                                {
                                    localApiResult.IsSuccess = false;
                                    localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم خلاصه معامله تمام شده است", inquiryTitle));
                                }
                            }
                            else
                            {
                                localApiResult.IsSuccess = false;
                                localApiResult.message.Add(string.Format("{0} پاسخ تایید نگرفته است", inquiryTitle));
                            }

                        }
                        else
                        {
                            localApiResult.IsSuccess = false;
                            localApiResult.message.Add(string.Format("{0} در وضعیت پاسخ داده شده نیست", inquiryTitle));
                        }
                    }
                    else
                    {
                        if (states.Contains(estateInquiry.WorkflowStatesId))
                        {

                            if (estateInquiry.ResponseResult == "True")
                            {
                                if (string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || !specificStatusList.Contains(estateInquiry.SpecificStatus))
                                {
                                    var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                                    if (estateInquiryResponseDateTime >= fromDate2 && estateInquiryResponseDateTime <= toDate)
                                    {
                                        localApiResult.IsSuccess = true;
                                    }
                                    else
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم خلاصه معامله تمام شده است", inquiryTitle));
                                    }
                                }
                                else
                                {
                                    if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.OnlyAllowedForDivision)
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("ملک مورد {0} ، مورد تفکیک یا افراز و یا تجمیع قرار گرفته است", inquiryTitle));
                                    }
                                    else if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested)
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("مالکیت ملک مورد {0} بازداشت است", inquiryTitle));
                                    }
                                    else if (estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision)
                                    {
                                        localApiResult.IsSuccess = false;
                                        localApiResult.message.Add(string.Format("ملک مورد {0} ، مورد تفکیک یا افراز و یا تجمیع قرار گرفته است", inquiryTitle));
                                        localApiResult.message.Add(string.Format("مالکیت ملک مورد {0} بازداشت است", inquiryTitle));
                                    }
                                }
                            }
                            else if ((string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus) || estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested) && inquiryPerson.ExecutiveTransfer == EstateConstant.BooleanConstant.True)
                            {
                                var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                                if (estateInquiryResponseDateTime >= fromDate2 && estateInquiryResponseDateTime <= toDate)
                                {
                                    localApiResult.IsSuccess = true;
                                }
                                else
                                {
                                    localApiResult.IsSuccess = false;
                                    localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم خلاصه معامله تمام شده است", inquiryTitle));
                                }
                            }
                            else
                            {
                                localApiResult.IsSuccess = false;
                                localApiResult.message.Add(string.Format("{0} پاسخ تایید نگرفته است", inquiryTitle));
                            }

                        }
                        else
                        {
                            localApiResult.IsSuccess = false;
                            localApiResult.message.Add(string.Format("{0} در وضعیت پاسخ داده شده نیست", inquiryTitle));
                        }
                    }
                }
            }
            else
            {
                localApiResult.IsSuccess = false;
                localApiResult.statusCode = ApiResultStatusCode.Success;
                localApiResult.message.Add(string.Format("استعلام با شناسه {0} یافت نشد", estateInquiryId));
            }
            
            if (localApiResult.IsSuccess)
            {
                var userInfo = _configuration.GetSection("Validate_Inquiry_For_DealSummary_Service_User").Get<ValidateInquiryForDealSummaryServiceUser>();
                var organization = await _baseInfoServiceHelper.GetOrganizationByUnitId(new string[] { estateInquiry.UnitId }, cancellationToken);
                if (organization == null || organization.OrganizationList.Count == 0)
                {
                    localApiResult.IsSuccess = false;
                    localApiResult.statusCode = ApiResultStatusCode.Success;
                    localApiResult.message.Add("واحد ثبتی مرتبط با استعلام در اطلاعات پایه سامانه یافت نشد");
                }
                else
                {
                    var input = new ValidateInquiryForDealSummaryInput()
                    {
                        ConsumerPassword = userInfo.Password,
                        ConsumerUsername = userInfo.UserName,
                        InquiryId = !string.IsNullOrWhiteSpace(estateInquiry.LegacyId) ? estateInquiry.LegacyId : estateInquiry.Id.ToString().Replace("-", "").Replace("_", "").ToUpper(),
                        ReceiverCmsorganizationId = organization.OrganizationList[0].LegacyId
                    };
                    var output = await _mediator.Send(input, cancellationToken);
                    if (output.IsSuccess)
                    {
                        if (output.Data.IsValid != 1)
                        {
                            localApiResult.IsSuccess = false;
                            localApiResult.message.Add(output.Data.ErrorMessage);
                        }
                    }
                    else
                    {
                        localApiResult.IsSuccess = false;
                        localApiResult.message.AddRange(output.message);
                    }
                }
            }
            
            var eivr = new EstateInquiryValidationResultViewModel();
            eivr.Estate_Inquiry_Id = estateInquiry != null ? estateInquiry.Id : Guid.Empty;
            if (localApiResult.IsSuccess)
            {
                eivr.IsValid = true;
                eivr.ErrorMessage = "";
            }
            else
            {
                eivr.IsValid = false;
                eivr.ErrorMessage = string.Join(Environment.NewLine, localApiResult.message);
                apiResult.message.AddRange(localApiResult.message);
            }
            apiResult.Data.Add(eivr);

        }    
        private static DateTime? ToDateTime(string persianDateTime)
        {
            PersianCalendar pc = new();
            try
            {
                var dateTimeValues = persianDateTime.Split('-');
                var dateValues = dateTimeValues[0].Split('/');
                var timeValues = dateTimeValues[1].Split(':');
                return pc.ToDateTime(Convert.ToInt32(dateValues[0]), Convert.ToInt32(dateValues[1]), Convert.ToInt32(dateValues[2]), Convert.ToInt32(timeValues[0]), Convert.ToInt32(timeValues[1]), timeValues.Length > 2 ? Convert.ToInt32(timeValues[2]) : 0, 0);
            }
            catch
            {
                return null;
            }
        }
    }
}
