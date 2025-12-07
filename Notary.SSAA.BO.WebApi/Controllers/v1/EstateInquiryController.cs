using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EstateInquiryController : BaseController
    {
        public EstateInquiryController(IMediator mediator)
    : base(mediator)
        {
        }

        [HttpPost,Route("[action]"), Authorize]

        public async Task<IActionResult> Create(CreateEstateInquiryCommand command)
        {

            var validator = new CreateEstateInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                command.IsValid = true;
                command.InqInquiryPerson.IsValid = true;
                return await ExecuteCommandAsync(command);
            }
        }


        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> Update(UpdateEstateInquiryCommand command)
        {

            var validator = new UpdateEstateInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                command.IsValid = true;
                command.InqInquiryPerson.IsValid = true;
                return await ExecuteCommandAsync(command);
            }
        }

        
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> UpdateFromFO(UpdateEstateInquiryCommand command)
        {

            var validator = new UpdateEstateInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                command.IsValid = true;
                command.InqInquiryPerson.IsValid = true;
                return await ExecuteCommandAsync(command);
            }
        }

        [HttpGet, Authorize]        
        public async Task<IActionResult> Get(string estateInquiryId)
        {

            var command = new GetEstateInquiryByIdQuery() { EstateInquiryId = estateInquiryId };
            
            var validator = new GetEstateInquiryByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }
            
        }

       
       
        
        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]

        public async Task<IActionResult> NewInquiryByCopy(string estateInquiryId)
        {
            var command = new GetNewEstateInquiryByCopyQuery(estateInquiryId);
            var validator = new GetNewEstateInquiryByCopyValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }

        }

        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]

        public async Task<IActionResult> NewFollowingInquiry(string estateInquiryId)
        {
            var command = new GetNewFollowingEstateInquiryQuery(estateInquiryId);
            var validator = new GetNewFollowingEstateInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(command);
            }

        }


        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpGet]
        public async Task<IActionResult> CurrentScriptorium()
        {
            var command = new GetCurrentScriptoriumQuery();
            return await RunQueryAsync(command);
        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> Delete(DeleteEstateInquiryCommand command)
        {
            //var command = new DeleteEstateInquiryCommand(estateInquiryId);
            var validator = new DeleteEstateInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }

        }


        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]

        public async Task<IActionResult> Archive(string estateInquiryId)
        {
            var command = new ArchiveEstateInquiryCommand(estateInquiryId);
            var validator = new ArchiveEstateInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }

        }
        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]

        public async Task<IActionResult> DeArchive(string estateInquiryId)
        {
            var command = new DeArchiveEstateInquiryCommand(estateInquiryId);
            var validator = new DeArchiveEstateInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }

        }

        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]

        public async Task<IActionResult> Send(string estateInquiryId)
        {
            var command = new SendEstateInquiryCommand(estateInquiryId);
            var validator = new SendEstateInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }

        }

        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]

        public async Task<IActionResult> Print(string estateInquiryId)
        {
            var estateInquiryPrintQuery = new EstateInquiryPrintQuery(estateInquiryId);
            var validator = new EstateInquiryPrintValidator();

            var result = await validator.ValidateAsync(estateInquiryPrintQuery);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(estateInquiryPrintQuery);
            }

        }
        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]
        public async Task<IActionResult> ResponsePrint(string estateInquiryId)
        {
            var estateInquiryResponsePrintQuery = new EstateInquiryResponsePrintQuery(estateInquiryId);
            var validator = new EstateInquiryResponsePrintValidator();

            var result = await validator.ValidateAsync(estateInquiryResponsePrintQuery);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(estateInquiryResponsePrintQuery);
            }

        }

        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]

        public async Task<IActionResult> Payment(string estateInquiryId)
        {
            var command = new UpdateEstateInquiryPaymentStateCommand(estateInquiryId) { InquiryMode = false };
            var validator = new UpdateEstateInquiryPaymentStateValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }

        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> UIConfigParam(GetEstateInquiryUIConfigParamsQuery request)
        {
            return await RunQueryAsync(request);
        }

        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> ValidateEstateInquiryForDealSummary(ValidateEstateInquiryForDealSummaryQuery request)
        {
            
            var validator = new ValidateEstateInquiryForDealSummaryValidator();

            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(request);
            }

        }

        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> ValidateEstateInquiryForDivisionRequest(ValidateEstateInquiryForDivisionRequestQuery request)
        {
            
            var validator = new ValidateEstateInquiryForDivisionRequestValidator();

            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(request);
            }

        }

        [HttpGet, Route("[action]/{estateInquiryId}"), Authorize]
        public async Task<IActionResult> EstateOwnerList(string estateInquiryId)
        {
            var request = new GetEstateOwnersByInquiryIdQuery(estateInquiryId);
            var validator = new GetEstateOwnersByInquiryIdValidator();

            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(request);
            }

        }

        [HttpGet, Route("[action]/{legacyId}"), Authorize]

        public async Task<IActionResult> GetByLegacyId(string legacyId)
        {
            var estateInquiryQuery = new GetEstateInquiryByIdQuery() { LegacyId = legacyId };
            var validator = new GetEstateInquiryByIdValidator();

            var result = await validator.ValidateAsync(estateInquiryQuery);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(estateInquiryQuery);
            }

        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> LastNo(GetEstateInquiryLastNoQuery input)
        {
            
            var validator = new GetEstateInquiryLastNoValidator();

            var result = await validator.ValidateAsync(input);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(input);
            }

        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> LastNo2(GetEstateInquiryLastNo2Query input)
        {

            var validator = new GetEstateInquiryLastNo2Validator();

            var result = await validator.ValidateAsync(input);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(input);
            }

        }

        
        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> ValidateEstateInquiryItems(ValidateEstateInquiryItemsQuery input)
        {

            var validator = new ValidateEstateInquiryItemsValidator();

            var result = await validator.ValidateAsync(input);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(input);
            }

        }

        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> DocumentRelatedData(GetDocumentRelatedDataQuery request)
        {

            var validator = new GetDocumentRelatedDataValidator();

            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(request);
            }

        }

        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> TestDigitalSignData(Notary.SSAA.BO.QueryHandler.Estate.EstateInquiry.Handlers.TestQuery request)
        {

            return await RunQueryAsync(request);


        }

    }
}
