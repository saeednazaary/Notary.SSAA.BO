using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Diagnostics;

namespace Notary.SSAA.BO.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SignRequestExternalController : ExternalBaseController
    {
        private readonly IRepository<SsrDocVerifCallLog> _logInDatabase;
        private readonly IDateTimeService _dateTimeService;
        public SignRequestExternalController(IRepository<SsrDocVerifCallLog> repository, IDateTimeService dateTimeService,/*IAccountService accountService,*/ IMediator mediator)
            : base(mediator)
        {
            _logInDatabase = repository;
            _dateTimeService = dateTimeService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignRequestAffidavit([FromBody] SignRequestAffidavitQuery query, CancellationToken cancellationToken)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var validator = new SignRequestAffidavitValidator();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var firstErrorCode = result.Errors[0].ErrorCode;
                var affidavit = new SignRequestAffidavitViewModel();
                affidavit.ExistDoc = false;
                affidavit.HasPermission = true;
                affidavit.Desc = result.Errors[0].ErrorMessage;
                affidavit.succseed = true;
                var resobj =new ExternalApiResult<SignRequestAffidavitViewModel>
                {
                    ResCode = firstErrorCode,
                    ResMessage = result.Errors[0].ErrorMessage,
                    Data = affidavit
                };

                var entity = new SsrDocVerifCallLog
                {
                    Id = Guid.NewGuid(), 
                    DocumentNo = query?.SignRequestNationalNo?.Length==18 ? query?.SignRequestNationalNo : "000000000000000000",               
                    ScriptoriumId = query?.SignRequestScriptoriumNo?.Length == 5 ? query?.SignRequestScriptoriumNo : "00000",         
                    DocumentSecretCode = query?.SignRequestSecretCode?.Length == 6 ? query?.SignRequestSecretCode : "000000", 
                    SsrDocVerifExternalUserId = "", 
                    CallDateTime = _dateTimeService.CurrentPersianDateTime,           
                    Input = JsonConvert.SerializeObject(query),                    
                    Output = JsonConvert.SerializeObject(resobj),                   
                    UserName = query.UserName,                 
                    Description = string.Join("--", result.Errors),             
                    ExecutionTimeInMillisecond = sw.ElapsedMilliseconds.ToString(),
                    State = "2"

                };
                await _logInDatabase.AddAsync(entity, cancellationToken);
                return Ok(resobj);

            }
            else
            {
                var resobj = await RunQueryAsync(query);
                var entity = new SsrDocVerifCallLog
                {
                    Id = Guid.NewGuid(), 
                    DocumentNo = query?.SignRequestNationalNo?.Length == 18 ? query?.SignRequestNationalNo : "000000000000000000",
                    ScriptoriumId = query?.SignRequestScriptoriumNo?.Length == 5 ? query?.SignRequestScriptoriumNo : "00000",
                    DocumentSecretCode = query?.SignRequestSecretCode?.Length == 6 ? query?.SignRequestSecretCode : "000000",
                    SsrDocVerifExternalUserId = "", 
                    CallDateTime = _dateTimeService.CurrentPersianDateTime,          
                    Input = JsonConvert.SerializeObject(query),                   
                    Output = JsonConvert.SerializeObject(resobj),                  
                    UserName = query.UserName,               
                    Description = string.Join("--", result.Errors),             
                    ExecutionTimeInMillisecond = sw.ElapsedMilliseconds.ToString(), 
                };
                entity.State = "1";

                if (resobj.ResCode == "101" || resobj.ResCode == "104")
                {
                    entity.State = "2";
                    await _logInDatabase.AddAsync(entity, cancellationToken);
                    return NotFound(resobj);
                }
                if (resobj.ResCode == "106" || resobj.ResCode == "105" || resobj.ResCode == "103")
                {
                    entity.State = "2";
                }
                await _logInDatabase.AddAsync(entity, cancellationToken);

                return Ok(resobj);

            }


        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignRequestVerificationWithImportantAnnex([FromBody] SignRequestVerificationWithImportantAnnexTextQuery query,CancellationToken cancellationToken)
        {

            var validator = new SignRequestVerificationWithImportantAnnexTextValidator();
            Stopwatch sw = Stopwatch.StartNew();

            var result = await validator.ValidateAsync(query);
            if (!result.IsValid)
            {
                var firstErrorCode = result.Errors[0].ErrorCode;
                var affidavit = new SignRequestVerificationWithImportantAnnexTextViewModel();
                affidavit.ExistDoc = false;
                affidavit.HasPermission = true;
                affidavit.Desc = result.Errors[0].ErrorMessage;
                affidavit.succseed = true;
                var resobj = new ExternalApiResult<SignRequestVerificationWithImportantAnnexTextViewModel>
                {
                    ResCode = firstErrorCode,
                    ResMessage = result.Errors[0].ErrorMessage,
                    Data = affidavit
                };

                var entity = new SsrDocVerifCallLog
                {
                    Id = Guid.NewGuid(),
                    DocumentNo = query?.SignRequestNationalNo?.Length == 18 ? query?.SignRequestNationalNo : "000000000000000000",
                    ScriptoriumId = query?.SignRequestScriptoriumNo?.Length == 5 ? query?.SignRequestScriptoriumNo : "00000",
                    DocumentSecretCode = query?.SignRequestSecretCode?.Length == 6 ? query?.SignRequestSecretCode : "000000",
                    SsrDocVerifExternalUserId = "",
                    CallDateTime = _dateTimeService.CurrentPersianDateTime,
                    Input = JsonConvert.SerializeObject(query),
                    Output = JsonConvert.SerializeObject(resobj),
                    UserName = query.UserName,
                    Description = string.Join("--", result.Errors),
                    ExecutionTimeInMillisecond = sw.ElapsedMilliseconds.ToString(),
                    State = "2"

                };
                await _logInDatabase.AddAsync(entity, cancellationToken);
                return Ok(resobj);
            }
            else
            {
                var resobj = await RunQueryAsync(query);
                var entity = new SsrDocVerifCallLog
                {
                    Id = Guid.NewGuid(),
                    DocumentNo = query?.SignRequestNationalNo?.Length == 18 ? query?.SignRequestNationalNo : "000000000000000000",
                    ScriptoriumId = query?.SignRequestScriptoriumNo?.Length == 5 ? query?.SignRequestScriptoriumNo : "00000",
                    DocumentSecretCode = query?.SignRequestSecretCode?.Length == 6 ? query?.SignRequestSecretCode : "000000",
                    SsrDocVerifExternalUserId = "",
                    CallDateTime = _dateTimeService.CurrentPersianDateTime,
                    Input = JsonConvert.SerializeObject(query),
                    Output = JsonConvert.SerializeObject(resobj),
                    UserName = query.UserName,
                    Description = string.Join("--", result.Errors),
                    ExecutionTimeInMillisecond = sw.ElapsedMilliseconds.ToString(),
                };
                entity.State = "1";
                if (resobj.ResCode == "101" || resobj.ResCode == "104")
                {
                    entity.State = "2";
                    await _logInDatabase.AddAsync(entity, cancellationToken);
                    return NotFound(resobj);
                }
                if (resobj.ResCode == "106" || resobj.ResCode == "105" || resobj.ResCode == "103")
                {
                    entity.State = "2";
                }
                await _logInDatabase.AddAsync(entity, cancellationToken);
                return Ok(resobj);
            }
        }
    }
}
