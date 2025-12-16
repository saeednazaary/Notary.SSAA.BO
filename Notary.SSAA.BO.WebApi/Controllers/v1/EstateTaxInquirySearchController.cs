using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.DataTransferObject.Validators.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.Validators.Lookups.Estate;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EstateTaxInquirySearchController : BaseController
    {
        public EstateTaxInquirySearchController(IMediator mediator) : base(mediator)
        {

        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> EstateTaxInquiry([FromBody] EstateTaxInquirySelectableGridQuery command)
        {

            var validator = new EstateTaxInquirySelectableGridValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> TransferType(EstateTaxTransferTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxTransferTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentRequestStatus(EstateTaxDocumentRequestTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxDocumentRequestTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> BuildingType(EstateTaxBuildingTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxBuildingTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> BuildingUsingType(EstateTaxBuildingUsingTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxBuildingUsingTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> BuildingStatus(EstateTaxBuildingStatusLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxBuildingStatusLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> BuildingConstructionStep(EstateTaxBuildingConstructionStepLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxBuildingConstructionStepLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> FieldType(EstateTaxFieldTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxFieldTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> FieldUsingType(EstateTaxFieldUsingTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxFieldUsingTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> Province(EstateTaxProvinceLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxProvinceLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> County(EstateTaxCountyLookupQuery command)
        {
            var validator = new EstateTaxCountyLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> City(EstateTaxCityLookupQuery command)
        {
            var validator = new EstateTaxCityLookupValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> LocationAssignRigthOwnershipType(EstateTaxLocationAssignRigthOwnershipTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxLocationAssignRigthOwnershipTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> LocationAssignRigthUsingType(EstateTaxLocationAssignRigthUsingTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxLocationAssignRigthUsingTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> PieceType(EstatePeiceTypeForEstateTaxInquiryLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstatePeiceTypeForEstateTaxInquiryLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> SideType(EstateSideTypeLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateSideTypeLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> PersonRelationType(DealSummaryPersonRelationTypeForTaxInquiryLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<DealSummaryPersonRelationTypeForTaxInquiryLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        //EstateTaxInquiryStatus
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> Status(EstateTaxInquiryStatusLookupQuery command)
        {
            var validator = new EstateBaseLookupValidator<EstateTaxInquiryStatusLookupQuery>();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

    }
}
