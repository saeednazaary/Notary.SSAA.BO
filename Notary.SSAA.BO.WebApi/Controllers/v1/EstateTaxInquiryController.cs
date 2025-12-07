using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EstateTaxInquiryController : BaseController
    {
        public EstateTaxInquiryController(IMediator mediator)
    : base(mediator)
        {
        }
       
        [HttpPost, Route("[action]"), Authorize]
        public async Task<IActionResult> Update(UpdateEstateTaxInquiryCommand command)
        {

            var validator = new UpdateEstateTaxInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                command.IsValid = true;
                command.TheInquiryOwner.IsValid = true;
                return await ExecuteCommandAsync(command);
            }
        }


        [HttpGet, Authorize]

        public async Task<IActionResult> Get(string estateTaxInquiryId)
        {

            var command = new GetEstateTaxInquiryByIdQuery() { EstateTaxInquiryId = estateTaxInquiryId };

            var validator = new GetEstateTaxInquiryByIdValidator();

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

        [HttpGet, Route("[action]/{estateTaxInquiryId}"), Authorize]

        public async Task<IActionResult> NewInquiryByCopy(string estateTaxInquiryId)
        {
            var command = new GetNewEstateTaxInquiryByCopyQuery(estateTaxInquiryId);
            var validator = new GetNewEstateTaxInquiryByCopyValidator();

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

        [HttpGet, Route("[action]/{legacyId}"), Authorize]

        public async Task<IActionResult> GetByLegacyId(string legacyId)
        {
            var estateTaxInquiryQuery = new GetEstateTaxInquiryByIdQuery() { LegacyId = legacyId };
            var validator = new GetEstateTaxInquiryByIdValidator();

            var result = await validator.ValidateAsync(estateTaxInquiryQuery);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                return await RunQueryAsync(estateTaxInquiryQuery);
            }

        }

        [HttpGet, Route("[action]/{estateTaxInquiryId}"), Authorize]

        public async Task<IActionResult> Send(string estateTaxInquiryId)
        {
            var command = new SendEstateTaxInquiryCommand(estateTaxInquiryId);
            var validator = new SendEstateTaxInquiryValidator();

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

        [HttpGet, Route("[action]/{estateTaxInquiryId}"), Authorize]

        public async Task<IActionResult> CertificateRenewal(string estateTaxInquiryId)
        {
            var command = new SendCertificateRenewalCommand(estateTaxInquiryId);
            var validator = new SendCertificateRenewalValidator();

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

        [HttpGet, Route("[action]/{estateTaxInquiryId}"), Authorize]

        public async Task<IActionResult> TransferStoped(string estateTaxInquiryId)
        {
            var command = new SendTransferStopedCommand(estateTaxInquiryId);
            var validator = new SendTransferStopedValidator();

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

        [HttpGet, Route("[action]/{estateTaxInquiryId}"), Authorize]

        public async Task<IActionResult> Cancel(string estateTaxInquiryId)
        {
            var command = new CancelEstateTaxInquiryCommand(estateTaxInquiryId);
            var validator = new CancelEstateTaxInquiryValidator();

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

        [HttpGet, Route("[action]/{estateTaxInquiryId}"), Authorize]

        public async Task<IActionResult> Status(string estateTaxInquiryId)
        {
            var command = new GetEstateTaxInquiryStatusCommand(estateTaxInquiryId);
            var validator = new GetEstateTaxInquiryStatusValidator();

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

        public async Task<IActionResult> GetTehranTaxBlockRadif(GetTehranTaxBlockRadifQuery command)
        {

            var validator = new GetTehranTaxBlockRadifValidator();

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

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> ExistsActiveEstateTaxInquiry(ExistsActiveEstateTaxInquiryQuery command)
        {
            var validator = new ExistsActiveEstateTaxInquiryValidator();

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


        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> LastNo(GetEstateTaxInquiryLastNoQuery input)
        {
            return await RunQueryAsync(input);
        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> LastNo2(GetEstateTaxInquiryLastNo2Query input)
        {
            return await RunQueryAsync(input);
        }

        [HttpPost, Route("[action]"), Authorize]

        public async Task<IActionResult> Create(CreateEstateTaxInquiryCommand command)
        {

            var validator = new CreateEstateTaxInquiryValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, Helper.RemoveDuplicateMessages(errorMessages)));
            }
            else
            {
                command.IsValid = true;
                command.TheInquiryOwner.IsValid = true;
                return await ExecuteCommandAsync(command);
            }
        }
    }
}
