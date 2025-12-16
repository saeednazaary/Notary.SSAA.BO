namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
    using Notary.SSAA.BO.DataTransferObject.Validators.StandardContract;
    using Notary.SSAA.BO.DataTransferObject.Validators.DocumentStandardContract;
    using Notary.SSAA.BO.SharedKernel.Attributes;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Notary.SSAA.BO.DataTransferObject.Validators.Document;
    using Notary.SSAA.BO.DataTransferObject.Commands.Document;
    using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;

    /// <summary>
    /// Defines the <see cref="DocumentStandardContractController" />
    /// </summary>
    [ApiVersion("1.0")]
    [GetScriptoriumInformation]
    public class DocumentStandardContractController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentStandardContractController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator<see cref="IMediator"/></param>
        public DocumentStandardContractController(IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> Get([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractByIdQuery(documentId);
            var validator = new GetDocumentStandardContractByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractRelatedPeople
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractRelatedPeople([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractRelatedPeopleByIdQuery(documentId);
            var validator = new GetDocumentStandardContractRelatedPeopleByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractPeople([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractPeopleByIdQuery(documentId);
            var validator = new GetDocumentStandardContractPeopleByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
        /// <summary>
        /// The DocumentStandardContractRelation
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractRelation([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractRelationsByIdQuery(documentId);
            var validator = new GetDocumentStandardContractRelationsByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractEstates
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractEstates([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractEstatesByIdQuery(documentId);
            var validator = new GetDocumentStandardContractEstatesByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractCases
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractCases([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractCasesByIdQuery(documentId);
            var validator = new GetDocumentStandardContractCasesByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="command">The command<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> Save(SaveDocumentStandardContractCommand command)
        {
            var validator = new SaveDocumentStandardContractValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractVehicles
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractVehicles([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractVehiclesByIdQuery(documentId);
            var validator = new GetDocumentStandardContractVehiclesByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractInfoText
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> DocumentStandardContractInfoText(string documentId)
        {
            var command = new GetDocumentStandardContractInfoTextByIdQuery(documentId);
            var validator = new GetDocumentStandardContractInfoTextByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractInfoJudgment
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> DocumentStandardContractInfoJudgment(string documentId)
        {
            var command = new GetDocumentStandardContractInfoJudgmentByIdQuery(documentId);
            var validator = new GetDocumentStandardContractInfoJudgmentByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractInfoOther
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> DocumentStandardContractInfoOther(string documentId)
        {
            var command = new GetDocumentStandardContractInfoOtherByIdQuery(documentId);
            var validator = new GetDocumentStandardContractInfoOtherByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractCosts
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractCosts([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractCostsByIdQuery(documentId);
            var validator = new GetDocumentStandardContractCostsByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractPayments
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractPayments([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractPaymentsByIdQuery(documentId);
            var validator = new GetDocumentStandardContractPaymentsByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractInquiries
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractInquiries([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractInquiriesByIdQuery(documentId);
            var validator = new GetDocumentStandardContractInquiriesByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }



        /// <summary>
        /// The FingerprintState
        /// </summary>
        /// <param name="query">The query<see cref="UpdateDocumentStandardContractFingerprintStateCommand"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> FingerprintState(UpdateDocumentStandardContractFingerprintStateCommand query)
        {
            var validator = new UpdateDocumentStandardContractFingerprintStateValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(query);
            }
        }

        /// <summary>
        /// The PersonFingerprint
        /// </summary>
        /// <param name="DocumentStandardContractId">The DocumentStandardContractId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet]
        [Route("[action]"), Authorize]

        public async Task<IActionResult> PersonFingerprint([FromQuery] string DocumentStandardContractId)
        {
            var command = new GetDocumentStandardContractPersonFingerprintQuery(DocumentStandardContractId);
            var validator = new GetDocumentStandardContractPersonFingerprintValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractInfoConfirm
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> DocumentStandardContractInfoConfirm(string documentId)
        {
            var command = new GetDocumentStandardContractInfoConfirmByIdQuery(documentId);
            var validator = new GetDocumentStandardContractInfoConfirmByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractCostQuestion
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> DocumentStandardContractCostQuestion(string documentId)
        {
            var command = new GetDocumentStandardContractCostQuestionByIdQuery(documentId);
            var validator = new GetDocumentStandardContractCostQuestionByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The DocumentStandardContractDetail
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <param name="detail">The detail<see cref="string"/></param>
        /// <param name="caseType">The caseType<see cref="CaseType"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> DocumentStandardContractDetail(string documentId, string detail, CaseType caseType)
        {
            var command = new GetDetailsOfDocumentStandardContractQuery(documentId, detail, caseType);
            var validator = new GetDetailsOfDocumentStandardContractValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPut, Authorize, Route("[action]")]
        public async Task<IActionResult> PaymentState(UpdateDocumentStandardContractPaymentStateCommand command)
        {
            var validator = new UpdateDocumentStandardContractPaymentStateValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPut, Authorize, Route("[action]")]
        public async Task<IActionResult> InquiryAfterPaid(UpdateDocumentStandardContractAfterPaidCommand command)
        {
            var validator = new UpdateDocumentStandardContractAfterPaidValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpGet, Route("[action]"), Authorize]
        public async Task<IActionResult> ValidatDocumentStandardContract(string documentdate, string documentnationalno, string documentscriptoriumid, string documenttypeid, string secretcode, bool isrelateddocumentinssar)
        {
            var command = new ValidatDocumentStandardContractQuery(documentdate, documentnationalno, documentscriptoriumid, documenttypeid, secretcode, isrelateddocumentinssar);
            var validator = new ValidatDocumentStandardContractValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        /// <summary>
        /// The AutoGenerateOwnershipDocs
        /// </summary>
        /// <param name="query">The query<see cref="AutoGenerateOwnershipDocsCommand"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost]
        [Route("[action]"), Authorize]
        public async Task<IActionResult> AutoGenerateOwnershipDocs(AutoGenerateOwnershipDocsCommand query)
        {
            var validator = new AutoGenerateOwnershipDocsValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await ExecuteCommandAsync(query);
            }
        }
        /// <summary>
        /// The DocumentStandardContractSms
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractSms([FromQuery] string documentId)
        {
            var command = new GetDocumentStandardContractSmsByIdQuery(documentId);
            var validator = new GetDocumentStandardContractSmsByIdValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }
        /// <summary>
        /// The DocumentStandardContractPaymentsConfirm
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentStandardContractPaymentsConfirm([FromQuery] string documentId)
        {
            var command = new DocumentStandardContractPaymentsConfirmQuery(documentId);
            var validator = new DocumentStandardContractPaymentsConfirmValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

    }
}
