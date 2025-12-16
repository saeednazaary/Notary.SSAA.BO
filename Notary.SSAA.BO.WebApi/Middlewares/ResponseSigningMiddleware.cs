using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Notary.SSAA.BO.WebApi.Middlewares
{
    public class ResponseSigningMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ECDsa _privateKey;

        public ResponseSigningMiddleware(RequestDelegate next, X509Certificate2 cert)
        {
            _next = next;

            // بررسی اینکه کلید خصوصی موجود باشد
            _privateKey = cert.GetECDsaPrivateKey()
                ?? throw new Exception("Private key not found in certificate");
        }

        public async Task Invoke(HttpContext context)
        {
            var cancellationToken = context.RequestAborted;

            var originalBodyStream = context.Response.Body;

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            responseBody.Seek(0, SeekOrigin.Begin);

            var responseBytes = responseBody.ToArray();
            var signature = _privateKey.SignData(responseBytes, HashAlgorithmName.SHA256);
            var signatureBase64 = Convert.ToBase64String(signature);

            context.Response.Headers["X-Signature"] = signatureBase64;
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream, cancellationToken);

        }
    }
}