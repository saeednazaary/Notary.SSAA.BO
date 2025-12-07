namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    using global:: Notary.SSAA.BO.DataTransferObject.Queries.Document;
    using global:: Notary.SSAA.BO.DataTransferObject.Validators.DocumentPayments;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Notary.SSAA.BO.DataTransferObject.Commands.Document;
    using Notary.SSAA.BO.DataTransferObject.Queries.Document;
    using Notary.SSAA.BO.DataTransferObject.Validators.Document;
    using Notary.SSAA.BO.DataTransferObject.Validators.DocumentPayments;
    using Notary.SSAA.BO.SharedKernel.Attributes;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Result;

    /// <summary>
    /// Defines the <see cref="DocumentController" />
    /// </summary>
    [ApiVersion ( "1.0" )]
    [GetScriptoriumInformation]
    public class DocumentController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator<see cref="IMediator"/></param>
        public DocumentController ( IMediator mediator )
            : base ( mediator )
        {
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> Get ( [FromQuery] string documentId )       
        {
            var command = new GetDocumentByIdQuery(documentId);
            var validator = new GetDocumentByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentRelatedPeople
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route ( "[action]" )]
        public async Task<IActionResult> DocumentRelatedPeople ( [FromQuery] string documentId )
        {
            var command = new GetDocumentRelatedPeopleByIdQuery(documentId);
            var validator = new GetDocumentRelatedPeopleByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }
        [HttpGet, Authorize, Route ( "[action]" )]
        public async Task<IActionResult> DocumentPeople ( [FromQuery] string documentId )
        {
            var command = new GetDocumentPeopleByIdQuery(documentId);
            var validator = new GetDocumentPeopleByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }
        /// <summary>
        /// The DocumentRelation
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route ( "[action]" )]
        public async Task<IActionResult> DocumentRelation ( [FromQuery] string documentId )
        {
            var command = new GetDocumentRelationsByIdQuery(documentId);
            var validator = new GetDocumentRelationsByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentEstates
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route ( "[action]" )]
        public async Task<IActionResult> DocumentEstates ( [FromQuery] string documentId )
        {
            var command = new GetDocumentEstatesByIdQuery(documentId);
            var validator = new GetDocumentEstatesByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentCases
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route ( "[action]" )]
        public async Task<IActionResult> DocumentCases ( [FromQuery] string documentId )
        {
            var command = new GetDocumentCasesByIdQuery(documentId);
            var validator = new GetDocumentCasesByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }
        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="command">The command<see cref="SaveDocumentCommand"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> Save ( SaveDocumentCommand command )
        {
            var validator = new SaveDocumentValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await ExecuteCommandAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentVehicles
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route ( "[action]" )]
        public async Task<IActionResult> DocumentVehicles ( [FromQuery] string documentId )
        {
            var command = new GetDocumentVehiclesByIdQuery(documentId);
            var validator = new GetDocumentVehiclesByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentInfoText
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType ( typeof ( ApiResult ), 200 )]
        [HttpGet, Route ( "[action]" ), Authorize]
        public async Task<IActionResult> DocumentInfoText ( string documentId )
        {
            var command = new GetDocumentInfoTextByIdQuery(documentId);
            var validator = new GetDocumentInfoTextByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentInfoJudgment
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType ( typeof ( ApiResult ), 200 )]
        [HttpGet, Route ( "[action]" ), Authorize]
        public async Task<IActionResult> DocumentInfoJudgment ( string documentId )
        {
            var command = new GetDocumentInfoJudgmentByIdQuery(documentId);
            var validator = new GetDocumentInfoJudgmentByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentInfoOther
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType ( typeof ( ApiResult ), 200 )]
        [HttpGet, Route ( "[action]" ), Authorize]
        public async Task<IActionResult> DocumentInfoOther ( string documentId )
        {
            var command = new GetDocumentInfoOtherByIdQuery(documentId);
            var validator = new GetDocumentInfoOtherByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentCosts
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route ( "[action]" )]
        public async Task<IActionResult> DocumentCosts ( [FromQuery] string documentId )
        {
            var command = new GetDocumentCostsByIdQuery(documentId);
            var validator = new GetDocumentCostsByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentPayments
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentPayments([FromQuery] string documentId)
        {
            var command = new GetDocumentPaymentsByIdQuery(documentId);
            var validator = new GetDocumentPaymentsByIdValidator();

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
        /// The DocumentInquiries
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route ( "[action]" )]
        public async Task<IActionResult> DocumentInquiries ( [FromQuery] string documentId )
        {
            var command = new GetDocumentInquiriesByIdQuery(documentId);
            var validator = new GetDocumentInquiriesByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }



        /// <summary>
        /// The FingerprintState
        /// </summary>
        /// <param name="query">The query<see cref="UpdateDocumentFingerprintStateCommand"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost]
        [Route ( "[action]" ), Authorize]
        public async Task<IActionResult> FingerprintState ( UpdateDocumentFingerprintStateCommand query )
        {
            var validator = new UpdateDocumentFingerprintStateValidator();

            var result = await validator.ValidateAsync(query);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await ExecuteCommandAsync ( query );
            }
        }

        /// <summary>
        /// The PersonFingerprint
        /// </summary>
        /// <param name="DocumentId">The DocumentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet]
        [Route ( "[action]" ), Authorize]

        public async Task<IActionResult> PersonFingerprint ( [FromQuery] string DocumentId )
        {
            var command = new GetDocumentPersonFingerprintQuery(DocumentId);
            var validator = new GetDocumentPersonFingerprintValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentInfoConfirm
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType ( typeof ( ApiResult ), 200 )]
        [HttpGet, Route ( "[action]" ), Authorize]
        public async Task<IActionResult> DocumentInfoConfirm ( string documentId )
        {
            var command = new GetDocumentInfoConfirmByIdQuery(documentId);
            var validator = new GetDocumentInfoConfirmByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentCostQuestion
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType ( typeof ( ApiResult ), 200 )]
        [HttpGet, Route ( "[action]" ), Authorize]
        public async Task<IActionResult> DocumentCostQuestion ( string documentId )
        {
            var command = new GetDocumentCostQuestionByIdQuery(documentId);
            var validator = new GetDocumentCostQuestionByIdValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        /// <summary>
        /// The DocumentDetail
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <param name="detail">The detail<see cref="string"/></param>
        /// <param name="caseType">The caseType<see cref="CaseType"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [ProducesResponseType ( typeof ( ApiResult ), 200 )]
        [HttpGet, Route ( "[action]" ), Authorize]
        public async Task<IActionResult> DocumentDetail ( string documentId, string detail, CaseType caseType )
        {
            var command = new GetDetailsOfDocumentQuery(documentId,detail,caseType);
            var validator = new GetDetailsOfDocumentValidator();

            var result = await validator.ValidateAsync(command);
            if ( !result.IsValid )
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok ( new ApiResult<List<string>> ( false, ApiResultStatusCode.Success, null, errorMessages ) );
            }
            else
            {
                return await RunQueryAsync ( command );
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [HttpPut, Authorize, Route("[action]")]
        public async Task<IActionResult> PaymentState(UpdateDocumentPaymentStateCommand command)
        {
            var validator = new UpdateDocumentPaymentStateValidator();

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
        public async Task<IActionResult> InquiryAfterPaid (UpdateDocumentAfterPaidCommand command)
        {
            var validator = new UpdateDocumentAfterPaidValidator();

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
        public async Task<IActionResult> ValidatDocument(string documentdate, string documentnationalno, string documentscriptoriumid, string documenttypeid, string secretcode,  bool isrelateddocumentinssar)
        {
            var command = new ValidatDocumentQuery(documentdate, documentnationalno, documentscriptoriumid, documenttypeid, secretcode, isrelateddocumentinssar);
            var validator = new ValidatDocumentValidator();

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
        public async Task<IActionResult> AutoGenerateOwnershipDocs( AutoGenerateOwnershipDocsCommand query)
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
        /// The SaveFromKateb
        /// </summary>
        /// <param name="command">The command<see cref="SaveDocumentCommand"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPut, Authorize, Route("[action]")]
        public async Task<IActionResult> SaveFromKateb(SaveDocumentCommand command)
        {
            var documentValidator = new SaveDocumentValidator();
            var KatebValidator = new SaveFromKatebDocumentValidator();

            var result = await documentValidator.ValidateAsync(command);
            var resultKated = await KatebValidator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else if (!resultKated.IsValid)
            {
                var errorMessages = resultKated.Errors.Select(x => x.ErrorMessage).ToList();
                return Ok(new ApiResult<List<string>>(false, ApiResultStatusCode.Success, null, errorMessages));
            }
            else
            {
                command = IgnoreSomeFields(command);
                return await ExecuteCommandAsync(command);
            }
        }
        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> GetFromKateb([FromQuery] string documentId)
        {
            var command = new GetDocumentByIdQuery(documentId);
            var validator = new GetDocumentByIdValidator();

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
        private SaveDocumentCommand IgnoreSomeFields(SaveDocumentCommand command)
        {
            command.UnSignedPersonsListIds = null;
            command.PrepareDocumentConfirmation = false;
            command.SardaftarConfirm = false;
            command.DaftaryarConfirm = false;
            command.DigitalBookSignatureCollection = null;
            command.SignedDSUDealSummaryCollection = null;
            command.SignedDocumentId = null;
            return command;
        }
        /// <summary>
        /// The DocumentSms
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentSms([FromQuery] string documentId)
        {
            var command = new GetDocumentSmsByIdQuery(documentId);
            var validator = new GetDocumentSmsByIdValidator();

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
        /// The DocumentPaymentsConfirm
        /// </summary>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet, Authorize, Route("[action]")]
        public async Task<IActionResult> DocumentPaymentsConfirm([FromQuery] string documentId)
        {
            var command = new DocumentPaymentsConfirmQuery(documentId);
            var validator = new DocumentPaymentsConfirmValidator();

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
