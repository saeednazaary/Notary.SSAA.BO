using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers
{
    public class GetEstateInquiryListQueryHandler : BaseQueryHandler<GetEstateInquiryListQuery, ApiResult<GetEstateInquiryListViewModel>>
    {
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        public GetEstateInquiryListQueryHandler(IMediator mediator, IUserService userService, IRepository<ConfigurationParameter> configurationParameterRepository, IEstateInquiryRepository estateInquiryRepository)
            : base(mediator, userService)
        {
            _configurationParameterRepository = configurationParameterRepository;
            _estateInquiryRepository = estateInquiryRepository;
        }
        protected override bool HasAccess(GetEstateInquiryListQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetEstateInquiryListViewModel>> RunAsync(GetEstateInquiryListQuery request, CancellationToken cancellationToken)
        {
            ApiResult<GetEstateInquiryListViewModel> result = new ApiResult<GetEstateInquiryListViewModel>() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };
            if (request.EstateInquiryId == null || request.EstateInquiryId.Length == 0)
            {
                result.IsSuccess = false;
                result.message.Add("لیست استعلام های ورودی خالی می باشد");
                return result;
            }
            
            try
            {
                var guidList = request.EstateInquiryId.Select(x => x.ToGuid()).ToList();
                var estateInquiryList = await _estateInquiryRepository
                                              .TableNoTracking
                                              .Include(x=>x.EstateInquiryPeople)
                                              .Include(x=>x.EstateInquirySendreceiveLogs)
                                              .Where(x => guidList.Contains(x.Id)).ToListAsync(cancellationToken);
                
                if (estateInquiryList.Count == 0)
                {
                    result.IsSuccess = false;
                    result.message.Add("استعلام ها در سرویس دهنده سازمان ثبت یافت نشد");
                    return result;
                }
                var sa = new string[] { "DealSummaries"
                ,"DocumentEstateInquiries"
                ,"EstateInquiryNavigation"
                ,"EstateInquiryPeople"
                ,"EstateInquirySendreceiveLogs"
                ,"EstateInquiryType"
                ,"EstateSection"
                ,"EstateSeridaftar"
                ,"EstateSubsection"
                ,"EstateTaxInquiries"
                ,"ForestorgInquiries"
                ,"InverseEstateInquiryNavigation"
                ,"WorkflowStates"
                ,"EstateInquirySendedSms"
                ,"EstateInquiryId"
                };
                System.Reflection.PropertyInfo[] propList = null;
                System.Reflection.PropertyInfo[] propList1 = null;
                System.Reflection.PropertyInfo[] propList2 = null;
                int orderNo = 1;
                var command = new GetEstateInquiryListViewModel();
                foreach (var estateInquiry in estateInquiryList)
                {
                    sa = new string[] { "DealSummaries"
                ,"DocumentEstateInquiries"
                ,"EstateInquiryNavigation"
                ,"EstateInquiryPeople"
                ,"EstateInquirySendreceiveLogs"
                ,"EstateInquiryType"
                ,"EstateSection"
                ,"EstateSeridaftar"
                ,"EstateSubsection"
                ,"EstateTaxInquiries"
                ,"ForestorgInquiries"
                ,"InverseEstateInquiryNavigation"
                ,"WorkflowStates"
                ,"EstateInquirySendedSms"
                ,"EstateInquiryId"
                };
                    if (propList == null)
                        propList = estateInquiry.GetType().GetProperties();
                    
                    var Data = new EntityData();
                    Data.CommandType = 2;
                    Data.EntityName = "ESTATEINQUIRY";
                    Data.OrderNo = orderNo++;
                    foreach (var prop in propList)
                    {
                        if (!sa.Contains(prop.Name))
                        {
                            var fv = new FieldValue();
                            fv.FieldName = prop.Name;
                            fv.Value = prop.GetValue(estateInquiry);
                            Data.FieldValues.Add(fv);
                        }
                    }
                    command.EstateInquiryList.Add(Data);

                    
                    sa = new string[] { "EstateInquiry" };
                    var inquiryPerson = estateInquiry.EstateInquiryPeople.FirstOrDefault();
                    if (inquiryPerson != null)
                    {
                        if (propList1 == null)
                            propList1 = inquiryPerson.GetType().GetProperties();
                        Data = new EntityData();
                        Data.CommandType = 2;
                        Data.EntityName = "ESTATEINQUIRYPERSON";
                        Data.OrderNo = orderNo++;
                        foreach (var prop in propList1)
                        {
                            if (!sa.Contains(prop.Name))
                            {
                                var fv = new FieldValue();
                                fv.FieldName = prop.Name;
                                fv.Value = prop.GetValue(inquiryPerson);
                                Data.FieldValues.Add(fv);
                            }
                        }
                        command.EstateInquiryList.Add(Data);
                    }
                    sa = new string[] { "EstateInquiry", "EstateInquiryActionType", "WorkflowStates" };
                    
                    
                    foreach (var sendReceveLog in estateInquiry.EstateInquirySendreceiveLogs)
                    {
                        if (propList2 == null)
                            propList2 = sendReceveLog.GetType().GetProperties();
                        Data = new EntityData();
                        Data.CommandType = 2;
                        Data.EntityName = "ESTATEINQUIRYSENDRECEIVELOG";
                        Data.OrderNo = orderNo++;
                        foreach (var prop in propList2)
                        {
                            if (!sa.Contains(prop.Name))
                            {
                                var fv = new FieldValue();
                                fv.FieldName = prop.Name;
                                fv.Value = prop.GetValue(sendReceveLog);
                                Data.FieldValues.Add(fv);
                            }
                        }
                        command.EstateInquiryList.Add(Data);
                    }

                }
                result.Data = command;
                return result;
            }
            catch(Exception ex) 
            {

                result.IsSuccess = false;
                result.message.Add(SystemMessagesConstant.OrganizationRegistry_Service_Execution_Failed);

                return result;
            }
          
        }

    }
}
