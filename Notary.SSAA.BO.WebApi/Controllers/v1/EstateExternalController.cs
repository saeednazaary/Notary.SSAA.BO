using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem;
using Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Validators.Estate.LegacySystem;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    
    public class EstateExternalController : ExternalBaseController
    {
        public EstateExternalController(IMediator mediator)
    : base(mediator)
        {
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EstateResponse(LegacySystemCommand command)
        {
            var validator = new LegacySystemCommandValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                ExternalApiResult externalApiResult = new ExternalApiResult();
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                if (errorMessages[0] == "1")
                {
                    externalApiResult.ResCode = "101";
                    externalApiResult.ResMessage = "نام کاربری و کلمه عبور استفاده کننده اجباری  می باشد";
                }

                if (errorMessages[0] == "2")
                {
                    externalApiResult.ResCode = "102";
                    externalApiResult.ResMessage = "نوع موجودیت اجباری  می باشد";
                }
                if (errorMessages[0] == "3")
                {
                    externalApiResult.ResCode = "103";
                    externalApiResult.ResMessage = "داده ورودی جهت ثبت در سیستم اجباری  می باشد";
                }

                return BadRequest(externalApiResult);
            }
            else
            {
                ExternalApiResult apiResult = null;
                if (command.EntityType == 1)
                    apiResult = await ExecuteCommandAsync(new EstateInquiryLegacySystemCommand() { EntityType = command.EntityType, Data = command.Data, Password = command.Password, UserName = command.UserName });
                else if (command.EntityType == 2)
                    apiResult = await ExecuteCommandAsync(new DealSummaryLegacySystemCommand() { EntityType = command.EntityType, Data = command.Data, Password = command.Password, UserName = command.UserName });
                else
                {
                    apiResult = new ExternalApiResult();
                    apiResult.ResCode = "104";
                    apiResult.ResMessage = "مقدار نوع موجودیت معتبر نمی باشد";
                }
                if (apiResult.ResCode == "104" || apiResult.ResCode == "105" || apiResult.ResCode == "106")
                    return Ok(apiResult);                
                if (apiResult.ResCode == "901")
                    return StatusCode(500, apiResult);
                return Ok(apiResult);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EstateTaxInquiryResponse(EstateTaxInquiryResponseReceiveCommand command)
        {

            var validator = new ReceiveTaxInquiryResponseCommandValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                errorMessages.Sort();
                var errorCode = errorMessages.First().Substring(0, 1);
                switch (errorCode)
                {
                    case "1":
                        return BadRequest(new ExternalApiResult() { ResCode = "101", ResMessage = errorMessages.First().Substring(2) });
                    case "2":
                        return BadRequest(new ExternalApiResult() { ResCode = "102", ResMessage = errorMessages.First().Substring(2) });
                    case "3":
                        return BadRequest(new ExternalApiResult() { ResCode = "103", ResMessage = errorMessages.First().Substring(2) });
                    case "4":
                        return BadRequest(new ExternalApiResult() { ResCode = "104", ResMessage = errorMessages.First().Substring(2) });
                    case "5":
                        return BadRequest(new ExternalApiResult() { ResCode = "105", ResMessage = errorMessages.First().Substring(2) });
                    case "6":
                        return BadRequest(new ExternalApiResult() { ResCode = "106", ResMessage = errorMessages.First().Substring(2) });
                    case "7":
                        return BadRequest(new ExternalApiResult() { ResCode = "107", ResMessage = errorMessages.First().Substring(2) });
                    default:
                        return BadRequest(new ExternalApiResult() { ResCode = "108", ResMessage = "ورودی معتبر نمی باشد" });
                }
            }
            var apiResult = await ExecuteCommandAsync(command);
            if (apiResult.ResCode == "109")
                return NotFound(apiResult);
            else if (apiResult.ResCode == "901")
                return StatusCode(500, apiResult);
            else
                return Ok(apiResult);

        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EstateTaxInquiryData(EstateTaxInquiryLegacySystemCommand command)
        {
            
            command.EntityType = 3;
            var validator = new LegacySystemCommandValidator();
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                ExternalApiResult externalApiResult = new ExternalApiResult();
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                if (errorMessages[0] == "1")
                {
                    externalApiResult.ResCode = "101";
                    externalApiResult.ResMessage = "نام کاربری و کلمه عبور استفاده کننده اجباری  می باشد";
                }

                if (errorMessages[0] == "2")
                {
                    externalApiResult.ResCode = "102";
                    externalApiResult.ResMessage = "نوع موجودیت اجباری  می باشد";
                }
                if (errorMessages[0] == "3")
                {
                    externalApiResult.ResCode = "103";
                    externalApiResult.ResMessage = "داده ورودی جهت ثبت در سیستم اجباری  می باشد";
                }

                return BadRequest(externalApiResult);
            }
            else
            {

                var apiResult = await ExecuteCommandAsync(command);
                if (apiResult.ResCode == "104")
                    return Ok(apiResult);
                if (apiResult.ResCode == "105")
                    return Ok(apiResult);
                if (apiResult.ResCode == "106")
                    return Ok(apiResult);
                if (apiResult.ResCode == "901")
                    return StatusCode(500, apiResult);
                return Ok(apiResult);
            }
        }
    }
}
