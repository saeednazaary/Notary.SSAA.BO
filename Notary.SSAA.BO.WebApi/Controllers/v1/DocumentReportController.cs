using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentReport;
using Notary.SSAA.BO.DataTransferObject.Validators.DocumentReport;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    public class DocumentReportController : BaseController
    {
        public DocumentReportController(IMediator mediator) : base(mediator)
        {
        }

       
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> Report([FromBody] NewDocumentReportQuery command)
        {
            var validator = new NewDocumentReportValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(command);
            }


        }
    }
}
