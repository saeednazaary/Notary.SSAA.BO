using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;
using System.ComponentModel.DataAnnotations;
using Notary.SSAA.BO.SharedKernel.Exceptions;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Notary.SSAA.BO.WebApi.Middlewares
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next,
            IHostEnvironment env,
            ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            List<string> messages = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;

            try
            {
                await _next(context);
            }
            catch (AppException exception)
            {
                LogError(exception);
                httpStatusCode = exception.HttpStatusCode;
                apiStatusCode = exception.ApiStatusCode;

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    if (exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }
                    if (exception.AdditionalData != null)
                        dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));

                    messages.Add(JsonConvert.SerializeObject(dic));
                }
                else
                {
                    messages.Add(exception.Message);
                }
                await WriteToResponseAsync();
            }

            catch (UnauthorizedAccessException exception)
            {
                LogError(exception);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync();
            }
            catch (AuthenticationException exception)
            {
                LogError(exception);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync();
            }
            catch (ValidationException exception)
            {
                LogError(exception);
                if (_env.IsDevelopment())
                {
                    messages = exception.Message.Split('_').ToList();
                }
                else
                {
                    messages.Clear();
                    messages.Add("خطا در اعتبار سنجی درخواست.");
                }
                await WriteToResponseAsync();

            }
            catch (DbUpdateException ex) 
            {
                LogError(ex);

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = ex.Message,
                        ["StackTrace"] = ex.StackTrace,
                    };
                    messages.Add(JsonConvert.SerializeObject(dic));
                }
                else
                {
                    messages.Clear();
                    messages.Add("خطا در ذخیره سازی اطلاعات در سرور.");
                    //messages.Add(exception.ToString());
                }
                await WriteToResponseAsync();
            }
            catch (Exception exception)
            {
                LogError(exception);

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    messages.Add(JsonConvert.SerializeObject(dic));
                }
                else
                {
                    messages.Clear();
                    messages.Add("خطا در اعتبار سنجی درخواست.");
                    //messages.Add(exception.ToString());
                }
                await WriteToResponseAsync();
            }

            async Task WriteToResponseAsync()
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

                var result = new ApiResult(false, apiStatusCode, messages);
                var json = JsonConvert.SerializeObject(result);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
                messages.Clear();
            }

            void SetUnAuthorizeResponse(Exception exception)
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                apiStatusCode = ApiResultStatusCode.UnAuthorized;

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace
                    };
                    //if (exception is SecurityTokenExpiredException tokenException)
                    //    dic.Add("Expires", tokenException.Expires.ToString());

                    messages.Add(JsonConvert.SerializeObject(dic));
                }
                else
                {
                    messages.Clear();
                    messages.Add("");
                }
            }
        }

        private void LogError(System.Exception exception)
        {
            var msg = exception.ToCompleteString();
            _logger.LogError(exception, "Exception complete message: {msg} ", msg);
        }
    }
}
