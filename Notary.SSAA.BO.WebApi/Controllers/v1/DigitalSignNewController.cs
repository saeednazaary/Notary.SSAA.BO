using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;


namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DigitalSignNewController : BaseController
    {        
        public DigitalSignNewController(IMediator mediator)
    : base(mediator)
        {                       
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> GetData(GetDataToNewSignQuery command)
        {
            return await RunQueryAsync(command);
        }



        [HttpPost, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> VerifySign(VerifyNewSignQuery command)
        {
            return await RunQueryAsync(command);
        }


        [HttpGet, Authorize]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetDigitalSignValue(string id)
        {
            var command = new GetSignValueQuery() { Id = id };
            return await RunQueryAsync(command);
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> GetDataList(GetSignDataListQuery command)
        {            
            return await RunQueryAsync(command);                       
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> VerifySignList(VerifySignListQuery command)
        {
            return await RunQueryAsync(command);
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> GetDigitalSignValueList(GetSignValueListQuery command)
        {
            return await RunQueryAsync(command);
        }

    }
}
