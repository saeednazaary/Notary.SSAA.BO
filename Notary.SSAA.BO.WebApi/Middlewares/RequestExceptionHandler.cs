using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Exceptions;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Dynamic;

namespace Notary.SSAA.BO.WebApi.Middlewares
{
    public class RequestExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<RequestExceptionHandler> _logger;
        private readonly LoggingOptions _loggingOptions;
        private Dictionary<string, string> _systemMessages;
        private static readonly char DotChar = '.';
        private static readonly char CloseParenChar = ')';
        private readonly object _systemMessagesLock = new();

        public RequestExceptionHandler(
            RequestDelegate next,
            IHostEnvironment env,
            ILogger<RequestExceptionHandler> logger,
            IRepository<SystemMessage> systemMessageRepository,
            IConfiguration configuration)
        {
            _next = next;
            _env = env;
            _logger = logger;
            _loggingOptions = configuration.GetSection("LoggingOptions").Get<LoggingOptions>();
            LoadSystemMessages(systemMessageRepository);
        }

        public async Task Invoke(HttpContext context)
        {
            List<string> messages = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;

            string requestBody = null;
            if (_loggingOptions.LogRequestBody)
            {
                requestBody = await SafeReadBody(context.Request);
            }

            var originalBodyStream = context.Response.Body;
            var responseBody = new MemoryStream();

            try
            {
                context.Response.Body = responseBody;
            context.Response.Headers.Append ( "Content-Security-Policy",
                    "default-src 'self'; " +
                    "script-src 'self' 'unsafe-inline'; " +
                    "style-src 'self' 'unsafe-inline'; " +
                    "img-src 'self' data: https:;" );
                    // Add X-Frame-Options
        context.Response.Headers["X-Frame-Options"] = "DENY";
        
        // Additional security headers
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";
        context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
        context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        context.Response.Headers["Content-Security-Policy"] = "default-src 'self'; frame-ancestors 'none';";
                var requestForLog = _loggingOptions.LogRequestBody ? requestBody : null;

                await _next(context);

                responseBody.Seek(0, SeekOrigin.Begin);
                var responseText = await new StreamReader(responseBody).ReadToEndAsync();

                _logger.LogInformation(
                    "HTTP Request: {RequestMethod} {RequestPath} {RequestQueryString} {RequestBody}, " +
                    "HTTP Response: {StatusCode} {ResponseBody}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.QueryString,
                    requestForLog,
                    context.Response.StatusCode,
                    responseText);

                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (AppException exception)
            {
                LogError(exception, context, requestBody);
                httpStatusCode = exception.HttpStatusCode;
                apiStatusCode = exception.ApiStatusCode;

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace
                    };
                    if (exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }
                    if (exception.AdditionalData != null)
                    {
                        dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));
                    }
                    messages.Add(JsonConvert.SerializeObject(dic));
                }
                else
                {
                    messages.Add(exception.Message);
                }

                await WriteToResponseAsync(exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                LogError(exception, context, requestBody);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync(exception);
            }
            catch (AuthenticationException exception)
            {
                LogError(exception, context, requestBody);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync(exception);
            }
            catch (ValidationException exception)
            {
                LogError(exception, context, requestBody);
                if (_env.IsDevelopment())
                {
                    messages = exception.Message.Split('_').ToList();
                }
                else
                {
                    messages.Clear();
                    messages.Add("درخواست ارسالی مطابق شماتیک api نمیباشد.");
                }
                await WriteToResponseAsync(exception);
            }
            catch (DbUpdateException exception)
            {
                LogError(exception, context, requestBody);
                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace
                    };
                    messages.Add(JsonConvert.SerializeObject(dic));
                }
                else
                {
                    messages.Clear();
                    messages.Add("خطا در ذخیره سازی اطلاعات در سرور.");
                }
                await WriteToResponseAsync(exception);
            }
            catch (OracleException exception)
            {
                await HandleOracleExceptionAsync(exception);
            }
            catch (Exception exception)
            {
                OracleException oracleException = FindOracleException(exception);
                if (oracleException == null)
                {
                    LogError(exception, context, requestBody);
                    if (_env.IsDevelopment())
                    {
                        var dic = new Dictionary<string, string>
                        {
                            ["Exception"] = exception.Message,
                            ["StackTrace"] = exception.StackTrace
                        };
                        messages.Add(JsonConvert.SerializeObject(dic));
                    }
                    else
                    {
                        messages.Clear();
                        messages.Add("خطا در اعتبار سنجی درخواست.");
                    }
                    await WriteToResponseAsync(exception);
                }
                else
                {
                    await HandleOracleExceptionAsync(oracleException);
                }
            }
            finally
            {
                context.Response.Body = originalBodyStream;
                responseBody.Dispose();
            }

            async Task WriteToResponseAsync(Exception exception = null)
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException("The response has already started.");

                context.Response.Body = originalBodyStream;

                dynamic result = new ExpandoObject();
                result.IsSuccess = false;
                result.StatusCode = apiStatusCode;
                result.Messages = "";

                if (_loggingOptions.IncludeExceptionInResponse && exception != null)
                {
                    var baseEx = exception;
                    var trace = new System.Diagnostics.StackTrace(baseEx, true);
                    var frames = trace.GetFrames()?
                        .Select(f =>
                        {
                            var fileName = f.GetFileName();
                            if (!string.IsNullOrEmpty(fileName))
                            {
                                var index = fileName.IndexOf("Notary.SSAA.BO");
                                if (index >= 0)
                                    fileName = fileName.Substring(index);
                                fileName = fileName.Replace("\\", "/");
                            }

                            return new
                            {
                                Method = $"{f.GetMethod()?.DeclaringType?.FullName}.{f.GetMethod()?.Name}".Split("+<")[0],
                                File = fileName,
                                Line = f.GetFileLineNumber()
                            };
                        })
                        .Where(f => f.Line > 0 || !string.IsNullOrEmpty(f.File))
                        .ToList();

                    var logObj = new JObject
                    {
                        ["Error"] = JObject.FromObject(new
                        {
                            Type = baseEx.GetType().Name,
                            Message = baseEx is ValidationException
                        ? "ورودی API معتبر نمی باشد." : $"{baseEx.Message} ",
                            InnerException = $"{baseEx?.InnerException?.Message}",
                            Locations = frames
                        }),
                        ["Timestamp"] = DateTime.UtcNow
                    };

                    result.ExceptionMessage = logObj;
                }

                string json = JsonConvert.SerializeObject(result, Formatting.Indented);

                try
                {
                    context.Response.Clear();
                    context.Response.StatusCode = httpStatusCode == HttpStatusCode.Unauthorized ? 401 : 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                }
                catch (Exception writeEx)
                {
                    _logger.LogError(writeEx, "Failed to write error response");
                }
                finally
                {
                    messages.Clear();
                }
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
                    messages.Add(JsonConvert.SerializeObject(dic));
                }
                else
                {
                    messages.Clear();
                    messages.Add("");
                }
            }

            async Task HandleOracleExceptionAsync(OracleException exception)
            {
                LogError(exception, context, requestBody);
                if (_env.IsDevelopment())
                {
                    messages.Add(exception.Message);
                }
                else
                {
                    messages.Add(GetOracleExceptionMessage(exception));
                }
                await WriteToResponseAsync(exception);
            }
        }

        #region Helpers

        private static async Task<string> SafeReadBody(HttpRequest request)
        {
            if (request.Body == null || !request.Body.CanRead)
                return null;

            request.EnableBuffering();

            using var memoryStream = new MemoryStream();
            await request.Body.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var reader = new StreamReader(memoryStream, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            request.Body.Position = 0;
            return body;
        }

        private static OracleException FindOracleException(Exception ex)
        {
            while (ex != null)
            {
                if (ex is OracleException oex) return oex;
                ex = ex.InnerException;
            }
            return null;
        }

        private static List<string> BuildDevMessage(Exception ex, Exception inner, object additional = null)
        {
            var dic = new Dictionary<string, string>
            {
                ["Exception"] = ex.Message,
                ["StackTrace"] = ex.StackTrace
            };

            if (inner != null)
            {
                dic["InnerException.Exception"] = inner.Message;
                dic["InnerException.StackTrace"] = inner.StackTrace;
            }

            if (additional != null)
                dic["AdditionalData"] = JsonConvert.SerializeObject(additional);

            return new() { JsonConvert.SerializeObject(dic) };
        }

        private string GetOracleExceptionMessage(OracleException ex)
        {
            string code = ex.Message[..9];
            return code switch
            {
                "ORA-02291" or "ORA-02292" or "ORA-00001" => BuildOracleConstraintMessage(ex, code),
                "ORA-12154" => GetMessageByCode("ORA-12154"),
                _ => $"{GetMessageByCode("ORA-00000")} {code}"
            };
        }

        private string BuildOracleConstraintMessage(OracleException ex, string code)
        {
            int dot = ex.Message.IndexOf(DotChar);
            int end = ex.Message.IndexOf(CloseParenChar, dot);
            string messageCode = $"{code}{ex.Message[dot..end]}";
            string msg = GetMessageByCode(messageCode);
            return string.IsNullOrWhiteSpace(msg)
                ? $"{GetMessageByCode("ORA-00000")} {messageCode}"
                : msg;
        }

        private void LoadSystemMessages(IRepository<SystemMessage> repo)
        {
            if (_systemMessages != null) return;

            lock (_systemMessagesLock)
            {
                _systemMessages ??= repo.Table
                    .Select(x => new { x.Code, x.Message })
                    .ToDictionary(x => x.Code, x => x.Message);
            }
        }

        private string GetMessageByCode(string code)
        {
            return _systemMessages.TryGetValue(code, out var msg)
                ? msg
                : $"Message Code: {code}";
        }

        private void LogError(Exception ex, HttpContext context, string requestBody = null)
        {
            var baseEx = ex;

            var trace = new System.Diagnostics.StackTrace(baseEx, true);
            var frames = trace.GetFrames()?
                .Select(f =>
                {
                    var fileName = f.GetFileName();
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        var index = fileName.IndexOf("Notary.SSAA.BO");
                        if (index >= 0)
                            fileName = fileName.Substring(index);
                        fileName = fileName.Replace("\\", "/");
                    }

                    return new
                    {
                        Method = $"{f.GetMethod()?.DeclaringType?.FullName}.{f.GetMethod()?.Name}".Split("+<")[0],
                        File = fileName,
                        Line = f.GetFileLineNumber()
                    };
                })
                .Where(f => f.Line > 0 || !string.IsNullOrEmpty(f.File))
                .ToList();

            object bodyContent = requestBody ?? "null";
            if (!string.IsNullOrEmpty(requestBody) && _loggingOptions.LogRequestBody)
            {
                try { bodyContent = JToken.Parse(requestBody); }
                catch
                {
                    var cleaned = requestBody
                        .Replace("\\\"", "\"")
                        .Replace("\\n", "\n")
                        .Replace("\\r", "")
                        .Replace("\\t", "\t");
                    bodyContent = new JRaw(cleaned);
                }
            }

            var logObj = new JObject
            {
                ["Error"] = JObject.FromObject(new
                {
                    Type = baseEx.GetType().Name,
                    Message = baseEx is ValidationException
                        ? "ورودی API معتبر نمی باشد." : $"Exception: {baseEx.Message}",
                    InnerException = $"{baseEx?.InnerException?.Message}",
                    Locations = frames
                }),
                ["Request"] = new JObject
                {
                    ["Path"] = context.Request.Path.ToString(),
                    ["Method"] = context.Request.Method,
                    ["Query"] = _loggingOptions.LogRequestQuery ? context.Request.QueryString.ToString() : null,
                    ["Body"] = JToken.FromObject(bodyContent)
                },
                ["Timestamp"] = DateTime.UtcNow
            };
            _logger.LogError("Exception details: {Error}", logObj.ToString(Formatting.Indented));
        }

        #endregion
    }
}