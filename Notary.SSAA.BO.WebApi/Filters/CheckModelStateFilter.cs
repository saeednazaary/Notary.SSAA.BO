using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Notary.SSAA.BO.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CheckModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            CheckModelStateValidation(context);
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            CheckModelStateValidation(context);
            base.OnResultExecuting(context);
        }
        private static void CheckModelStateValidation(ActionContext context)
        {
            List<string> errors = context.ModelState.SelectMany(x => x.Value.Errors).Select(x => !string.IsNullOrWhiteSpace(x.ErrorMessage) ?
           x.ErrorMessage : !string.IsNullOrWhiteSpace(x.Exception.Message) ? " Exception: " + x.Exception.Message : string.Empty).ToList();

            var str = "";
            try
            {
                str = GetDetails(context.HttpContext.Request);
            }
            catch
            {

            }
            errors.Insert(0, "درخواست ارسالی مطابق شماتیک api نمیباشد ." + string.Format(" input: {0}", str));
            if (!context.ModelState.IsValid)
                throw new ValidationException(string.Join("_", errors));
        }

        public static string GetDetails(HttpRequest request)
        {
            string baseUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString.Value}";
            StringBuilder sbHeaders = new StringBuilder();
            foreach (var header in request.Headers)
                sbHeaders.Append($"{header.Key}: {header.Value}\n");

            string body = "no-body";
            if (request.Body.CanSeek)
            {
                request.Body.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(request.Body))
                    body = sr.ReadToEnd();
            }

            return $"{request.Protocol} {request.Method} {baseUrl}\n\n{sbHeaders}\n{body}";
        }
    }
}