using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Net.Http.Headers;
using System.Text;


namespace Notary.SSAA.BO.ServiceHandler.Base
{
    public class CallBOApiHelper
    {
        public static async Task<ApiResult<TOutput>> PostAsync<TInput, TOutput>(TInput request, string url, string token, CancellationToken cancellationToken) where TInput : class where TOutput : class
        {
            ApiResult<TOutput> apiResult = null;
            var tokenWithoutbearer = token.Replace("Bearer", "").Trim();
            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            using (var http = new HttpClient(handler1))
            {
                try
                {
                    http.Timeout = TimeSpan.FromSeconds(20);
                    http.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenWithoutbearer);

                    var data = JsonConvert.SerializeObject(request);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await http.PostAsync(url, content, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var re = await response.Content.ReadAsStringAsync(cancellationToken);
                        apiResult = JsonConvert.DeserializeObject<ApiResult<TOutput>>(re);
                        if (apiResult == null)
                        {
                            apiResult= new ApiResult<TOutput>();
                            apiResult.statusCode = ApiResultStatusCode.ServerError;
                            apiResult.IsSuccess = false;
                            apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت رخ داد");
                            Console.WriteLine(JsonConvert.SerializeObject(response));
                        }
                        return apiResult;
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            http.DefaultRequestHeaders.Remove("Authorization");
                            http.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
                            response = await http.PostAsync(url, content, cancellationToken);
                            if (response.IsSuccessStatusCode)
                            {
                                var re = await response.Content.ReadAsStringAsync(cancellationToken);
                                apiResult = JsonConvert.DeserializeObject<ApiResult<TOutput>>(re);
                                if (apiResult == null)
                                {
                                    apiResult = new ApiResult<TOutput>();
                                    apiResult.statusCode = ApiResultStatusCode.UnAuthorized;
                                    apiResult.IsSuccess = false;
                                    apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت رخ داد");
                                    Console.WriteLine(JsonConvert.SerializeObject(response));

                                }
                                return apiResult;
                            }
                            else                                
                            {
                                apiResult = new ApiResult<TOutput>();
                                apiResult.statusCode = ApiResultStatusCode.ServerError;
                                apiResult.IsSuccess = false;
                                apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت_کد خطا :" + ((int)response.StatusCode).ToString());
                                Console.WriteLine(JsonConvert.SerializeObject(response));

                            }
                        }
                        else
                        {
                            apiResult = new ApiResult<TOutput>();
                            apiResult.statusCode = ApiResultStatusCode.ServerError;
                            apiResult.IsSuccess = false;
                            apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت_کد خطا :" + ((int)response.StatusCode).ToString());
                            Console.WriteLine(JsonConvert.SerializeObject(response));

                        }

                    }
                }
                catch (Exception ex)
                {
                    Exception exp = ex;
                    while (exp.InnerException != null)
                        exp = exp.InnerException;
                    apiResult = new ApiResult<TOutput>();
                    apiResult.statusCode = ApiResultStatusCode.ServerError;
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت_متن خطا :" +JsonConvert.SerializeObject(exp));
                    Console.WriteLine("خطا در فراخوانی سرویس دهنده سازمان ثبت_متن خطا :" +JsonConvert.SerializeObject(exp));
                }
            }
            return apiResult;
        }

        public static async Task<ApiResult<TOutput>> GetAsync<TOutput>(string url, Dictionary<string, string> queryStrings, string token, CancellationToken cancellationToken) where TOutput : class
        {
            ApiResult<TOutput> apiResult = null;
            var tokenWithoutbearer = token.Replace("Bearer", "").Trim();
            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            using (var http = new HttpClient(handler1))
            {
                try
                {
                    http.Timeout = TimeSpan.FromSeconds(20);
                    http.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenWithoutbearer);
                    if (queryStrings != null && queryStrings.Count > 0)
                    {
                        url = QueryHelpers.AddQueryString(url, queryStrings);
                    }
                    var response = await http.GetAsync(url, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var re = await response.Content.ReadAsStringAsync(cancellationToken);

                        apiResult = JsonConvert.DeserializeObject<ApiResult<TOutput>>(re);
                        if (apiResult == null)
                        {
                            apiResult = new ApiResult<TOutput>();
                            apiResult.statusCode = ApiResultStatusCode.Success;
                            apiResult.IsSuccess = false;
                            apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت رخ داد");
                        }
                        return apiResult;
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            http.DefaultRequestHeaders.Remove("Authorization");
                            http.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
                            response = await http.GetAsync(url, cancellationToken);
                            if (response.IsSuccessStatusCode)
                            {
                                var re = await response.Content.ReadAsStringAsync(cancellationToken);
                                apiResult = JsonConvert.DeserializeObject<ApiResult<TOutput>>(re);
                                if (apiResult == null)
                                {
                                    apiResult = new ApiResult<TOutput>();
                                    apiResult.statusCode = ApiResultStatusCode.Success;
                                    apiResult.IsSuccess = false;
                                    apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت رخ داد");
                                }
                                return apiResult;
                            }
                            else
                            {
                                apiResult = new ApiResult<TOutput>();
                                apiResult.statusCode = ApiResultStatusCode.Success;
                                apiResult.IsSuccess = false;
                                apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت_کد خطا :" + ((int)response.StatusCode).ToString());
                            }
                        }
                        else
                        {
                            apiResult = new ApiResult<TOutput>();
                            apiResult.statusCode = ApiResultStatusCode.Success;
                            apiResult.IsSuccess = false;
                            apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت_کد خطا :" + ((int)response.StatusCode).ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Exception exp = ex;
                    while (exp.InnerException != null)
                        exp = exp.InnerException;
                    apiResult = new ApiResult<TOutput>();
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("خطا در فراخوانی سرویس دهنده سازمان ثبت_متن خطا :" + JsonConvert.SerializeObject(exp));
                    Console.WriteLine("خطا در فراخوانی سرویس دهنده سازمان ثبت_متن خطا :" + JsonConvert.SerializeObject(exp));

                }
            }
            return apiResult;
        }

        public static async Task<TOutput> PostAsyncWithoutApiResultReturnType<TInput, TOutput>(TInput request, string url, string token, CancellationToken cancellationToken) where TInput : class where TOutput:class
        {
            TOutput apiResult = null;
            var tokenWithoutbearer = token.Replace("Bearer", "").Trim();
            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            using (var http = new HttpClient(handler1))
            {
                try
                {
                    http.Timeout = TimeSpan.FromSeconds(20);
                    http.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenWithoutbearer);

                    var data = JsonConvert.SerializeObject(request);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await http.PostAsync(url, content, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var re = await response.Content.ReadAsStringAsync(cancellationToken);
                        apiResult = JsonConvert.DeserializeObject<TOutput>(re);                       
                        return apiResult;
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            http.DefaultRequestHeaders.Remove("Authorization");
                            http.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
                            response = await http.PostAsync(url,content, cancellationToken);
                            if (response.IsSuccessStatusCode)
                            {
                                var re = await response.Content.ReadAsStringAsync(cancellationToken);
                                apiResult = JsonConvert.DeserializeObject<TOutput>(re);                                
                                return apiResult;
                            }
                           
                        }
                        
                    }
                    
                }
                catch (Exception ex)
                {
                    Exception exp = ex;
                    while (exp.InnerException != null)
                        exp = exp.InnerException;

                }
            }
            return apiResult;
        }

        public static async Task<TOutput> GetAsyncWithoutApiResultReturnType<TOutput>(string url, Dictionary<string, string> queryStrings, string token, CancellationToken cancellationToken) where TOutput:class
        {
            TOutput apiResult = null;
            var tokenWithoutbearer = token.Replace("Bearer", "").Trim();
            var handler1 = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true // Disable SSL validation (only for development)
            };
            using (var http = new HttpClient(handler1))
            {
                try
                {
                    http.Timeout = TimeSpan.FromSeconds(20);
                    http.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenWithoutbearer);
                    if (queryStrings != null && queryStrings.Count > 0)
                    {
                        url = QueryHelpers.AddQueryString(url, queryStrings);
                    }
                    var response = await http.GetAsync(url, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var re = await response.Content.ReadAsStringAsync(cancellationToken);
                        apiResult = JsonConvert.DeserializeObject<TOutput>(re);                                            
                        return apiResult;
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            http.DefaultRequestHeaders.Remove("Authorization");
                            http.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
                            response = await http.GetAsync(url, cancellationToken);
                            if (response.IsSuccessStatusCode)
                            {
                                var re = await response.Content.ReadAsStringAsync(cancellationToken);
                                apiResult = JsonConvert.DeserializeObject<TOutput>(re);                               
                                return apiResult;
                            }
                            
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Exception exp = ex;
                    while (exp.InnerException != null)
                        exp = exp.InnerException;
                }
            }
            return apiResult;
        }
    }
}
