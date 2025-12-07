using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.Validators.Estate.DealSummary;
using Notary.SSAA.BO.SharedKernel.Result;
using SSAA.Notary.DataTransferObject.ViewModels.Estate.DealSummary;


namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DealSummaryController : BaseController
    {
        public DealSummaryController(IMediator mediator)
    : base(mediator)
        {
        }
        
       
        [HttpGet, Authorize]
        
        public async Task<IActionResult> Get(string dealSummaryId)
        {

            var command = new GetDealSummaryByIdQuery() { DealSummaryId = dealSummaryId };
            
            var validator = new GetDealSummaryByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
               
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
            
        }

        [HttpGet, Route("[action]/{dealSummaryId}"), Authorize]

        public async Task<IActionResult> Print(string dealSummaryId)
        {
            var dealSummaryPrintQuery = new DealSummaryPrintQuery(dealSummaryId);
            var validator = new DealSummaryPrintValidator();

            var result = await validator.ValidateAsync(dealSummaryPrintQuery);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(dealSummaryPrintQuery);
            }

        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> UnRestrict(UnRestrictDealSummaryCommand command)
        {

            var validator = new UnRestrictDealSummaryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [HttpGet, Route("[action]/{legacyId}"), Authorize]

        public async Task<IActionResult> GetByLegacyId(string legacyId)
        {
            var dealSummaryQuery = new GetDealSummaryByIdQuery() { LegacyId = legacyId };
            var validator = new GetDealSummaryByIdValidator();

            var result = await validator.ValidateAsync(dealSummaryQuery);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(dealSummaryQuery);
            }

        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> Save(SendDealSummaryQuery<DealSummaryServiceOutputViewModel> command)
        {
            return await RunQueryAsync(command);
        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> DealSummaryVerification(DealSummaryVerificationQuery<DealSummaryVerificationResultViewModel> command)
        {
            return await RunQueryAsync(command);
        }


        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> DealSummaryVerificationWithoutOwnerChecking(DealSummaryVerificationWithoutOwnerCheckingQuery<DealSummaryVerificationResultViewModel> command)
        {
            return await RunQueryAsync(command);
        }
    }
}
