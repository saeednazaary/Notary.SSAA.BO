using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateArticle6Inquiry;
using Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateArticle6Inquiry;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Net;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EstateArticleInquiryController:ExternalBaseController
    {
        public EstateArticleInquiryController(IMediator mediator)
  : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveResponse(EstateArticle6InquiryResponseCommand command)
        {

            var validator = new EstateArticle6InquiryResponseValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                errorMessages = Helper.RemoveDuplicateMessages(errorMessages);
                if (errorMessages[0]=="1")
                    return Ok(new ExternalApiResult<object>() { ResCode = "01", ResMessage = "نام کاربری و کلمه عبور استفاده کننده نمی تواند خالی باشد", Data = null });
                if (errorMessages[0]=="2")
                    return Ok(new ExternalApiResult<object>() { ResCode = "02", ResMessage = "نام کاربری یا کلمه عبور استفاده کننده اشتباه می باشد", Data = null });
                if (errorMessages[0]=="3")
                    return Ok(new ExternalApiResult<object>() { ResCode = "03", ResMessage = "شماره استعلام مربوطه نمی تواند خالی باشد", Data = null });
                if (errorMessages[0]=="4")
                    return Ok(new ExternalApiResult<object>() { ResCode = "04", ResMessage = "نوع پاسخ (موافقت/عدم موافقت) مشخص نشده است", Data = null });
                if (errorMessages[0]=="5")
                    return Ok(new ExternalApiResult<object>() { ResCode = "05", ResMessage = "کد ارگان پاسخ دهنده ی استعلام خالی می باشد", Data = null });
                if (errorMessages[0]=="6")
                    return Ok(new ExternalApiResult<object>() { ResCode = "06", ResMessage = "کد علت مخالفت خالی می باشد", Data = null });
                if (errorMessages[0]=="7")
                    return Ok(new ExternalApiResult<object>() { ResCode = "07", ResMessage = "عنوان علت مخالفت خالی می باشد", Data = null });
                return BadRequest();
                
            }
            else
            {

                if (!string.IsNullOrWhiteSpace(command.ResponseDate) && !command.ResponseDate.IsValidDate())
                    return Ok(new ExternalApiResult<object>() { ResCode = "12", ResMessage = "فرمت تاریخ پاسخ اشتباه می باشد", Data = null });
                
                
                var apiResult = await ExecuteCommandAsync(command);
                if(apiResult.ResCode=="00")
                    return Ok(apiResult);
                if (apiResult.ResCode == "08" || apiResult.ResCode == "09" || apiResult.ResCode == "10")
                    return Ok(apiResult);
                if(apiResult.ResCode =="11")
                    return Ok(apiResult);
                if (apiResult.ResCode == "12")
                    return Ok(apiResult);
                if (apiResult.ResCode == "13")
                    return Ok(apiResult);
                return Ok("خطا در انجام عملیات رخ داد");
            }
        }

        
        [HttpPost,Route("[action]")]
        public async Task<IActionResult> RelatedOrgans(EstateArticle6InquiryRelatedOrgansCommand command)
        {

            var validator = new EstateArticle6InquiryRelatedOrgansValidator();

            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                errorMessages = Helper.RemoveDuplicateMessages(errorMessages);
                if (errorMessages[0] == "1")
                    return BadRequest(new ExternalApiResult<object>() { ResCode = "101", ResMessage = "نام کاربری و کلمه عبور استفاده کننده نمی تواند خالی باشد", Data = null });               
                if (errorMessages[0] == "2")
                    return BadRequest(new ExternalApiResult<object>() { ResCode = "102", ResMessage = "شماره استعلام مربوطه نمی تواند خالی باشد", Data = null });
                if (errorMessages[0] == "3")
                    return BadRequest(new ExternalApiResult<object>() { ResCode = "103", ResMessage = "لیست دستگاه های استعلام شده نمی تواند خالی باشد", Data = null });
                foreach(var relatedOrgan in command.RelatedOrganList)
                {
                    if (string.IsNullOrWhiteSpace(relatedOrgan.OrganizationTrackingCode) ||
                       string.IsNullOrWhiteSpace(relatedOrgan.OrganizationCode) ||
                       string.IsNullOrWhiteSpace(relatedOrgan.SendDate) ||
                       string.IsNullOrWhiteSpace(relatedOrgan.MinistryCode)
                        )
                        return BadRequest(new ExternalApiResult<object>() { ResCode = "104", ResMessage = "در ورودی و درلیست دستگاه های استعلام شده، رکوردی وجود دارد که یکی یا چند تا از اقلام اجباری آن ( کد وزارتخانه ، کد دستگاه ، کد رهگیری دستگاه و تاریخ ارسال به دستگاه) خالی می باشد ", Data = null });
                }
                return BadRequest();

            }
            else
            {
                var apiResult = await ExecuteCommandAsync(command);
                if (apiResult.ResCode == "1")
                    return Ok(apiResult);
                else if (apiResult.ResCode == "105" || apiResult.ResCode == "106" || apiResult.ResCode == "107" || apiResult.ResCode == "108")  
                    return StatusCode(299, apiResult);
                else
                    return StatusCode(500, apiResult);           
            }
        }
    }
}
