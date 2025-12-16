using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Notary.SSAA.BO.SharedKernel.Result
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public bool HasAllarmMessage { get; set; }
        public ApiResultStatusCode statusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> message { get; set; } = new List<string>();

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, List<string> messages = null, bool HasAllarmMessage = false)
        {
            IsSuccess = isSuccess;
            this.statusCode = statusCode;
            message = messages;
            this.HasAllarmMessage = HasAllarmMessage;
        }
        public ApiResult()
        {
            IsSuccess = true;
            statusCode = ApiResultStatusCode.Success;
        }

        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            List<string> messages = new()
            {
                result.Value.ToString()
            };
            if (result.Value is SerializableError errors)
            {
                messages = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
            }
            return new ApiResult(false, ApiResultStatusCode.BadRequest, messages);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success, new List<string> { result.Content });
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.NotFound);
        }
        #endregion
    }

    public class ApiResult<TData> : ApiResult
        where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, List<string> messages = null, bool hasAlarmMessage = false)
            : base(isSuccess, statusCode, messages)
        {
            Data = data;
            HasAllarmMessage = hasAlarmMessage;
        }

        public ApiResult()
        {
            IsSuccess = true;
            HasAllarmMessage = false;
            statusCode = ApiResultStatusCode.Success;
        }

        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, data);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, (TData)result.Value);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            List<string> messages = new List<string>()
            {
                result.Value.ToString()
            };
            if (result.Value is SerializableError errors)
            {
                messages = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
            }
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, messages);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null, new List<string> { result.Content });
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, (TData)result.Value);
        }
        #endregion
    }
    public class ExternalApiResult
    {
        public string ResCode { get; set; }
        public string ResMessage { get; set; }
    }

    public class ExternalApiResult<T> : ExternalApiResult
    {
        public T Data { get; set; }
    }
}
