using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Circular;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.Validators.Services;
using Notary.SSAA.BO.DataTransferObject.Validators.Services.Circular;
using Notary.SSAA.BO.DataTransferObject.Validators.Services.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]

    public class ExternalController : BaseController
    {
        public ExternalController(/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
        }


      
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> LegalForiegnerService([FromQuery] LegalForiegnerServiceQuery command)
        {
            var validator = new LegalForeignerServiceValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> SabtAhvalPhotoListService([FromBody] SabtAhvalPhotoListServiceQuery command)
        {
            var validator = new SabtAhvalPhotoListServiceValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> IsMatchedBySabtAhval(IsMatchedBySabtAhvalServiceQuery command)
        {
            var validator = new IsMatchedBySabtAhvalServiceValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> IsMatchedByILENC(IsMatchedByILENCServiceQuery command)
        {
            var validator = new IsMatchedByILENCServiceValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> EstateSeparationElements(GetEstateSeparationElementsQuery command)
        {
            var validator = new GetEstateSeparationElementsValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> EstateSeparationElementsOwnership(SetEstateSeparationElementsOwnershipQuery command)
        {
            var validator = new SetEstateSeparationElementsOwnershipValidator();

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

           [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> PostService([FromQuery] PostServiceQuery command)
        {
            var validator = new PostServiceValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> ShahkarService([FromQuery] ShahkarServiceQuery command)
        {
            var validator = new ShahkarServiceValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> SabtAhvalService([FromQuery] SabtAhvalServiceQuery command)
        {
            var validator = new SabtAhvalServiceValidator();

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

      

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> SanaService([FromQuery] SanaServiceQuery command)
        {
            var validator = new SanaServiceValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> ILENCService([FromQuery] ILENCServiceQuery command)
        {
            var validator = new ILENCServiceValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> RealForiegnerService([FromQuery] RealForeignerServiceQuery command)
        {
            var validator = new RealForeignerServiceValidator();

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


        #region Circular

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> CheckCircularService([FromBody] CheckCircularGridQuery command)
        {
            var validator = new CheckCircularGridValidator();

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


        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> CircularTypeService ([FromBody] CircularTypeLookupQuery command)
        {
            var validator = new CircularTypeLookupValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> CircularItemTypeService([FromBody] CircularItemTypeLookupQuery command)
        {
            var validator = new CircularItemTypeLookupValidator();

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


        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> ReferenceCircularService([FromBody] ReferenceCircularLookupQuery command)
        {
            var validator = new ReferenceCircularLookupValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> CircularAdvancedSearchService([FromBody] CircularAdvancedSearchQuery command)
        {
            var validator = new CircularAdvancedSearchValidator();

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

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> CircularService([FromBody] CircularQuery command)
        {
            var validator = new CircularValidator();

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
        #endregion

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> EstateInfo(ConfirmDocumentByElectronicEstateNoteNoQuery command)
        {
            var validator = new ConfirmDocumentByElectronicEstateNoteNoValidator();

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
