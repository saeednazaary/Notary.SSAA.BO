using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentElectronicBook;
using Notary.SSAA.BO.DataTransferObject.Validators.DocumentElectronic;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DocumentElectronicBookController : BaseController
    {
        public DocumentElectronicBookController(IMediator mediator) : base(mediator)
        {

        }


        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> GetData(DocumentElectronicBookQuery query)
        {
            var validator = new DocumentElectronicValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [HttpGet]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> Count([FromQuery] DocumentElectronicBookCountQuery query)
        {
            var validator = new DocumentElectronicCountValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

    }
}
