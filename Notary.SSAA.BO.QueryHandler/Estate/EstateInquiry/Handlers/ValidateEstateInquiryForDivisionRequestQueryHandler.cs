using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Globalization;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;

namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class ValidateEstateInquiryForDivisionRequestQueryHandler : BaseQueryHandler<ValidateEstateInquiryForDivisionRequestQuery, ApiResult<List<EstateInquiryValidationResultViewModel>>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        ApiResult<List<EstateInquiryValidationResultViewModel>> apiResult = new();
        List<string> estateInquiryIdList = null;

        public ValidateEstateInquiryForDivisionRequestQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository, IRepository<ConfigurationParameter> configurationParameterRepository)
            : base(mediator, userService)
        {
            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            _configurationParameterRepository = configurationParameterRepository;
        }
        protected override bool HasAccess(ValidateEstateInquiryForDivisionRequestQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        private async Task ValidateEstateInquiry(string estateInquiryId, CancellationToken cancellationToken)
        {
            var estateInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, estateInquiryId.ToGuid());
            var localApiResult = new ApiResult() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };
            string estateInquiryIdInOldSystem = "";
            string inquiryTitle = "";
            if (estateInquiry != null)
            {
                if (!string.IsNullOrWhiteSpace(estateInquiry.LegacyId))
                    estateInquiryIdInOldSystem = estateInquiry.LegacyId;
                else
                    estateInquiryIdInOldSystem = estateInquiry.Id.ToString().Replace("_", "").Replace("-", "").ToUpper();

                await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquiryPeople, cancellationToken);
                inquiryTitle = string.Format("استعلام شماره {0} به تاریخ {1}", estateInquiry.InquiryNo, estateInquiry.InquiryDate);
                var inquiryUnit = await GetUnitById(new string[] { estateInquiry.UnitId }, cancellationToken);
                var inquiryScriptorium = await GetScriptoriumById(new string[] { estateInquiry.ScriptoriumId }, cancellationToken);
                var inquiryPerson = estateInquiry.EstateInquiryPeople.First();
                string[] states = new string[] { EstateConstant.EstateInquiryStates.ConfirmResponse,
                                             EstateConstant.EstateInquiryStates.Archived,
                                             EstateConstant.EstateInquiryStates.RejectResponse};
                string[] specificStatusList = new string[] {
                    EstateConstant.EstateInquirySpecificStatus.OnlyAllowedForDivision,
                    EstateConstant.EstateInquirySpecificStatus.AllowedForDivision};
                var toDate = DateTime.Now;
                var fromDate1 = toDate.AddDays(-37);
                var fromDate2 = toDate.AddDays(-50);

                //if (inquiryScriptorium.ScriptoriumList[0].GeoLocationId == inquiryUnit.UnitList[0].GeoLocationId)
                //{
                if (states.Contains(estateInquiry.WorkflowStatesId))
                {
                    if (string.IsNullOrWhiteSpace(estateInquiry.AttachedToDealsummary))
                    {
                        if (estateInquiry.ResponseResult == "True")
                        {
                            localApiResult.IsSuccess = true;
                            //if (specificStatusList.Contains(estateInquiry.SpecificStatus))
                            //{
                            //    var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                            //    if (estateInquiryResponseDateTime >= fromDate1 && estateInquiryResponseDateTime <= toDate)
                            //    {
                            //        localApiResult.IsSuccess = true;
                            //    }
                            //    else
                            //    {
                            //        localApiResult.IsSuccess = false;
                            //        localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم تقسیم نامه تمام شده است", inquiryTitle));
                            //    }
                            //}
                            //else
                            //{
                            //    localApiResult.IsSuccess = false;
                            //    localApiResult.message.Add(string.Format("ملک مرتبط با {0} ، مجاز برای ثبت تقسیم نامه نمی باشد", inquiryTitle));
                            //}
                        }
                        else if ((estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested || estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision || string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus)) && inquiryPerson.ExecutiveTransfer == EstateConstant.BooleanConstant.True)
                        {
                            localApiResult.IsSuccess = true;
                            //var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                            //if (estateInquiryResponseDateTime >= fromDate1 && estateInquiryResponseDateTime <= toDate)
                            //{
                            //    localApiResult.IsSuccess = true;
                            //}
                            //else
                            //{
                            //    localApiResult.IsSuccess = false;
                            //    localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم تقسیم نامه تمام شده است", inquiryTitle));
                            //}
                        }
                        else
                        {
                            localApiResult.IsSuccess = false;
                            localApiResult.message.Add(string.Format("{0} پاسخ تایید ندارد", inquiryTitle));
                        }
                    }
                    else
                    {
                        localApiResult.IsSuccess = false;
                        localApiResult.message.Add(string.Format("خلاصه معامله مالکیت/تقسیم نامه دیگری برای {0} ثبت شده است", inquiryTitle));
                    }
                }
                else
                {
                    localApiResult.IsSuccess = false;
                    localApiResult.message.Add(string.Format("{0} در وضعیت پاسخ داده شده نیست", inquiryTitle));
                }
                //}
                //else
                //{
                //    if (states.Contains(estateInquiry.WorkflowStatesId))
                //    {
                //        if (string.IsNullOrWhiteSpace(estateInquiry.AttachedToDealsummary))
                //        {
                //            if (estateInquiry.ResponseResult == "True")
                //            {
                //                if (specificStatusList.Contains(estateInquiry.SpecificStatus))
                //                {
                //                    var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                //                    if (estateInquiryResponseDateTime >= fromDate2 && estateInquiryResponseDateTime <= toDate)
                //                    {
                //                        localApiResult.IsSuccess = true;
                //                    }
                //                    else
                //                    {
                //                        localApiResult.IsSuccess = false;
                //                        localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم تقسیم نامه تمام شده است", inquiryTitle));
                //                    }
                //                }
                //                else
                //                {
                //                    localApiResult.IsSuccess = false;
                //                    localApiResult.message.Add(string.Format("ملک مرتبط با {0} ، مجاز برای ثبت تقسیم نامه نمی باشد", inquiryTitle));
                //                }
                //            }
                //            else if ((estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested || estateInquiry.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision || string.IsNullOrWhiteSpace(estateInquiry.SpecificStatus)) && inquiryPerson.ExecutiveTransfer == EstateConstant.BooleanConstant.True)
                //            {
                //                var estateInquiryResponseDateTime = ToDateTime(estateInquiry.ResponseDate + "-" + estateInquiry.ResponseTime);
                //                if (estateInquiryResponseDateTime >= fromDate2 && estateInquiryResponseDateTime <= toDate)
                //                {
                //                    localApiResult.IsSuccess = true;
                //                }
                //                else
                //                {
                //                    localApiResult.IsSuccess = false;
                //                    localApiResult.message.Add(string.Format("اعتبار {0} برای تنظیم تقسیم نامه تمام شده است", inquiryTitle));
                //                }
                //            }
                //            else
                //            {
                //                localApiResult.IsSuccess = false;
                //                localApiResult.message.Add(string.Format("{0} پاسخ تایید نگرفته است", inquiryTitle));
                //            }
                //        }
                //        else
                //        {
                //            localApiResult.IsSuccess = false;
                //            localApiResult.message.Add(string.Format("خلاصه معامله مالکیت/تقسیم نامه برای {0} ثبت شده است", inquiryTitle));
                //        }
                //    }
                //    else
                //    {
                //        localApiResult.IsSuccess = false;
                //        localApiResult.message.Add(string.Format("{0} در وضعیت پاسخ داده شده نیست", inquiryTitle));
                //    }
                //}


            }
            else
            {
                localApiResult.IsSuccess = false;
                localApiResult.statusCode = ApiResultStatusCode.Success;
                localApiResult.message.Add(string.Format("استعلام با شناسه {0} یافت نشد", estateInquiryId));
            }

            /*
            if (localApiResult.IsSuccess)
            {
                var organization = await GetOrganizationByUnitId(new string[] { estateInquiry.UnitId }, cancellationToken);
                var input = new ValidateInquiryForDealSummaryInput()
                {
                    ConsumerPassword = "2662839991",
                    ConsumerUsername = "test",
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
           */
            var eivr = new EstateInquiryValidationResultViewModel();
            eivr.Estate_Inquiry_Id = estateInquiry != null ? estateInquiry.Id : Guid.Empty;
            if (localApiResult.IsSuccess)
            {
                eivr.IsValid = true;
                eivr.ErrorMessage = "";

                var input = new ValidateEstateSeparationInquiriesInput()
                {
                    EstateInquiryId = new string[] { estateInquiryIdInOldSystem }
                };
                var output = await _mediator.Send(input, cancellationToken);
                if (output != null)
                {
                    if (output.IsSuccess)
                    {
                        if (!output.Data.Result)
                        {
                            localApiResult.IsSuccess = false;
                            localApiResult.message.Add(output.Data.ErrorMessage);
                        }
                    }
                    else
                    {
                        localApiResult.IsSuccess = false;
                        localApiResult.message = output.message;
                    }
                }
                else
                {
                    localApiResult.IsSuccess = false;
                    localApiResult.message.Add(string.Format("عدم امکان و یا بروز خطا در فراخوانی سرویس دهنده سازمان ثبت به منظور اعتبار سنجی استعلام {0}", inquiryTitle));
                }
                if (!localApiResult.IsSuccess)
                {
                    eivr.IsValid = false;
                    eivr.ErrorMessage = string.Join(Environment.NewLine, localApiResult.message);
                    apiResult.message.AddRange(localApiResult.message);
                }
            }
            else
            {
                eivr.IsValid = false;
                eivr.ErrorMessage = string.Join(Environment.NewLine, localApiResult.message);
                apiResult.message.AddRange(localApiResult.message);
            }
            apiResult.Data.Add(eivr);
        }
        protected async override Task<ApiResult<List<EstateInquiryValidationResultViewModel>>> RunAsync(ValidateEstateInquiryForDivisionRequestQuery request, CancellationToken cancellationToken)
        {
           
            estateInquiryIdList = new List<string>();
            apiResult = new();
            apiResult.IsSuccess = false;
            apiResult.statusCode = ApiResultStatusCode.Success;
            apiResult.Data = new List<EstateInquiryValidationResultViewModel>();
            foreach (var inquiryId in request.EstateInquiryId)
            {
                await ValidateEstateInquiry(inquiryId, cancellationToken);
            }
            if (apiResult.message.Count == 0)
            {
                apiResult.IsSuccess = true;
            }
            else apiResult.IsSuccess = false;
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
        public async Task<OrganizationViewModel> GetOrganizationByUnitId(string[] unitId, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(new GetOrganizationByUnitIdQuery(unitId), cancellationToken);
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
