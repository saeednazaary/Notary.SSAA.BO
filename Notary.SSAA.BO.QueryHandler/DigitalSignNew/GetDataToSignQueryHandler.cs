using MediatR;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Newtonsoft.Json;
using System.Collections;
using Notary.SSAA.BO.Utilities.Extensions;


namespace Notary.SSAA.BO.QueryHandler.DigitalSignNew
{
    public class GetDataToSignQueryHandler : BaseQueryHandler<GetDataToNewSignQuery, ApiResult<GetDataToSignViewModel2>>
    {

       
        public GetDataToSignQueryHandler(IMediator mediator, IUserService userService)
            : base(mediator, userService)
        {

        }
        protected override bool HasAccess(GetDataToNewSignQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetDataToSignViewModel2>> RunAsync(GetDataToNewSignQuery request, CancellationToken cancellationToken)
        {
            
            ApiResult<GetDataToSignViewModel2> apiResult = new()
            {
                statusCode = ApiResultStatusCode.Success,
                IsSuccess = true
            };
            try
            {
                var handlerType = Type.GetType(request.HandlerName);
                if (handlerType != null)
                {
                    var handlerBaseType = handlerType.BaseType;
                    if (handlerBaseType.IsGenericType)
                    {
                        Type[] genericArguments = handlerBaseType.GetGenericArguments();
                        var inputType = genericArguments[0];
                        var input = JsonConvert.DeserializeObject(request.HandlerInput, inputType);
                        var output = await _mediator.Send(input, cancellationToken);
                        if (output != null)
                        {
                            var outputType = output.GetType();
                            if (outputType.IsGenericType)
                            {
                                if (output is ApiResult)
                                {
                                    var outputApiResult = (ApiResult)output;
                                    if (outputApiResult.IsSuccess)
                                    {                                        
                                        var dataObject = (SignDataViewModel)output.GetType().GetProperty("Data").GetValue(output, null);
                                        GetDataToSignViewModel2 r = new GetDataToSignViewModel2();                                        
                                        r.HandlerName = request.HandlerName;
                                        r.HandlerInput = request.HandlerInput;
                                        r.SignDataQueryHandler = request.SignDataQueryHandler;
                                        r.SignDataList = dataObject.SignDataList;                                       
                                        apiResult.Data = r;
                                    }
                                    else
                                    {
                                        apiResult.IsSuccess = false;
                                        apiResult.message.Add("خطا در دریافت داده جهت امضا ");
                                        apiResult.message.AddRange(outputApiResult.message);
                                    }
                                }
                                else
                                {
                                    apiResult.IsSuccess = false;
                                    apiResult.message.Add("خطا در دریافت داده جهت امضا ");
                                }
                            }
                            else
                            {
                                apiResult.IsSuccess = false;
                                apiResult.message.Add("خطا در دریافت داده جهت امضا ");
                            }
                        }
                        else
                        {
                            apiResult.IsSuccess = false;
                            apiResult.message.Add("خطا در دریافت داده جهت امضا ");
                        }
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("خطا در دریافت داده جهت امضا ");
                    }
                }
            }
            catch (Exception ex)
            {
                apiResult.IsSuccess = false;
                Exception exp = ex;
                while(exp.InnerException != null) 
                    exp = exp.InnerException;
                apiResult.message.Add(ex.ToCompleteString());
            }
            
            return apiResult;
        }

    }
}
