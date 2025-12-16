using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.DataTransferObject.Validators.Grids;
using Notary.SSAA.BO.DataTransferObject.Validators.Lookups;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DocumentStandardContractSearchController : BaseController
    {
        public DocumentStandardContractSearchController(IMediator mediator) : base(mediator)
        {

        }

        #region Lookup
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractType([FromBody] DocumentTypeLookupQuery query)
        {
            var validator = new DocumentTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> RelatedDocumentStandardContractType([FromBody] RelatedDocumentTypeLookupQuery query)
        {
            var validator = new RelatedDocumentTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractPersonType([FromBody] DocumentPersonTypeLookupQuery query)
        {
            var validator = new DocumentPersonTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractVehicleTip([FromBody] DocumentVehicleTipLookupQuery query)
        {
            var validator = new DocumentVehicleTipLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractTypeGroupOne([FromBody] DocumentTypeGroupOneLookupQuery query)
        {
            var validator = new DocumentTypeGroupOneLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractTypeGroupTwo([FromBody] DocumentTypeGroupTwoLookupQuery query)
        {
            var validator = new DocumentTypeGroupTwoLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractAgentType(DocumentAgentTypeLookupQuery command)
        {
            var validator = new DocumentAgentTypeLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractRelatedPerson(DocumentRelatedPersonLookupQuery command)
        {
            var validator = new DocumentRelatedPersonLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractOriginalPerson(DocumentOriginalPersonLookupQuery command)
        {
            var validator = new DocumentOriginalPersonLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractReliablePersonReason(DocumentReliablePersonReasonLookupQuery command)
        {
            var validator = new DocumentReliablePersonReasonLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractProsecutorPersonReason(DocumentProsecutorPersonReasonLookupQuery command)
        {
            var validator = new DocumentProsecutorPersonReasonLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractVehicleType([FromBody] DocumentVehicleTypeLookupQuery query)
        {
            var validator = new DocumentVehicleTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractVehicleSystem([FromBody] DocumentVehicleSystemLookupQuery query)
        {
            var validator = new DocumentVehicleSystemLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractEstateType([FromBody] DocumentEstateTypeLookupQuery query)
        {
            var validator = new DocumentEstateTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractEstateTypeGroup([FromBody] DocumentEstateTypeGroupLookupQuery query)
        {
            var validator = new DocumentEstateTypeGroupLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractSearch([FromBody] DocumentLookupQuery query)
        {
            var validator = new DocumentLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> OldExecutiveDocumentStandardContract([FromBody] OldExecutiveDocumentLookupQuery query)
        {
            var validator = new OldExecutiveDocumentLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractAssetType([FromBody] DocumentAssetTypeLookupQuery query)
        {
            var validator = new DocumentAssetTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractTypeSubject([FromBody] DocumentTypeSubjectLookupQuery query)
        {
            var validator = new DocumentTypeSubjectLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> ExecutiveType(ExecutiveTypeLookupQuery query)
        {
            var validator = new ExecutiveTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractJudgmentType(DocumentJudgmentTypeLookupQuery query)
        {
            var validator = new DocumentJudgmentTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> CostType(CostTypeLookupQuery query)
        {
            var validator = new CostTypeLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractEstateSection(DocumentEstateSectionLookupQuery query)
        {
            var validator = new DocumentEstateSectionLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractEstateSubsection(DocumentEstateSubsectionLookupQuery query)
        {
            var validator = new DocumentEstateSubsectionLookupValidator();
            
            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }


        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractEstateSeridaftar(DocumentEstateSeridaftarLookupQuery query)
        {
            var validator = new DocumentEstateSeridaftarLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractInquiryOrganization(DocumentInquiryOrganizationLookupQuery query)
        {
            var validator = new DocumentInquiryOrganizationLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }
        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractPerson(DocumentPersonLookupQuery command)
        {
            var validator = new DocumentPersonLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractEstste(DocumentEstateLookupQuery command)
        {
            var validator = new DocumentEstateLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractPersonOwnerShip(DocumentPersonOwnerShipLookupQuery command)
        {
            var validator = new DocumentPersonOwnerShipLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractOwnerShip(DocumentOwnerShipLookupQuery command)
        {
            var validator = new DocumentOwnerShipLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractEststePieceType(DocumentEstatePieceTypeLookupQuery command)
        {
            var validator = new DocumentEstatePieceTypeLookupValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> ReusedDocumentStandardContractPayment(ReusedDocumentPaymentLookupQuery query)
        {
            var validator = new ReusedDocumentPaymentLookupValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, errorMessages));

            }
            else
            {
                return await RunQueryAsync(query);
            }
        }
        #endregion

        #region Grid

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContract([FromBody] DocumentGridQuery command)
        {
            var validator = new DocumentGridValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ApiResult<List<string>>(false, ApiResultStatusCode.BadRequest, null, errorMessages));

            }
            else
            {
                return await RunQueryAsync(command);
            }
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> DocumentStandardContractCase([FromBody] DocumentCaseGridQuery command)
        {
            var validator = new DocumentCaseGridValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> SelselehAyadi([FromBody] DocumentSelselehAyadiGridQuery command)
        {
            var validator = new DocumentSelselehAyadiGridValidator();

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
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> PersonDocumentStandardContractCartable([FromBody] PersonDocumentCartableQuery request)
        {
            return await RunQueryAsync(request);
        }

        [ProducesResponseType(typeof(ApiResult), 200)]
        [Route("[action]"), Authorize]
        [HttpPost]
        public async Task<IActionResult> PersonDocumentStandardContractCartableDetail([FromBody] PersonDocumentCartableDetailQuery request)
        {
            return await RunQueryAsync(request);
        }
    }
}
