using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpExternalServiceCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Security.Policy;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons
{
    public class PersonInquiryManager
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IHttpExternalServiceCaller _httpExternalServiceCaller;
        private readonly IMediator _mediator;

        public PersonInquiryManager ( IConfiguration configuration,IUserService userService, IMediator mediator, IHttpExternalServiceCaller httpExternalServiceCaller )
        {
            _configuration=configuration;
            _userService=userService;
            _httpExternalServiceCaller=httpExternalServiceCaller;
            _mediator=mediator;
        }

        #region GetPersonInfo
        public async Task<(List<ApiResult<DataTransferObject.ViewModels.ExternalServices.SabtAhvalServiceViewModel>>, List<ApiResult<GetGeolocationByNationalityCodeViewModel>>,bool ,string)>  GetPersonInfo ( List<InputPerson> inputPersons,CancellationToken cancellationToken, string extraParam = null )
        {
            var peopleInquiry = new List<ApiResult<DataTransferObject.ViewModels.ExternalServices.SabtAhvalServiceViewModel>>();
            var esPersonsList = new List<ApiResult<GetGeolocationByNationalityCodeViewModel>>();
            bool isValid=false;
            string errorMessage = string.Empty;
            foreach (var person in inputPersons )
            {
                var result = await GetPersonData(person.NationalCode, person.BirthDate, cancellationToken);
                if ( result.IsSuccess == false )
                {
                    isValid = false;
                    errorMessage = "\n خطا در تصدیق مشخصات " + person.Name + " " + person.Family;
                    //return (null, null, isValid, errorMessage);

                }

                peopleInquiry.Add ( result );

            }

            foreach ( var person in inputPersons )
            {
               var result= await _mediator.Send ( new GetGeolocationByNationalityCodeQuery ( person.NationalCode ),
                    cancellationToken );

               if (result.IsSuccess == false)
               {
                   isValid = false;
                   errorMessage += "\n خطا در تصدیق مشخصات " + person.Name + " " + person.Family;
                   //return (null, null, isValid, errorMessage);
                }
               esPersonsList.Add(result);
            }

            return (peopleInquiry, esPersonsList,isValid,errorMessage);
        }
        #endregion

        public async Task<ApiResult<DataTransferObject.ViewModels.ExternalServices.SabtAhvalServiceViewModel>> GetPersonData (string nationalCode,string birthDate,CancellationToken cancellationToken )
        {
            string url = _configuration.GetValue(typeof(string), "InternalGatewayUrl").ToString() +
                         "ExternalServices/v1/SabtAhvalService";
            SabteAhvalInput sabteAhvalInput = new();
            sabteAhvalInput.NationalityCode = nationalCode;
            sabteAhvalInput.BirthDate = birthDate;
            sabteAhvalInput.ClientId = "SSAR_Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons_PersonInquiryManagerShell_GetPersonData";
            var sabtAhvalServiceResult = await _httpExternalServiceCaller.CallExternalServicePostApiAsync<ApiResult<DataTransferObject.ViewModels.ExternalServices.SabtAhvalServiceViewModel>, SabteAhvalInput>(new HttpExternalServiceRequest<SabteAhvalInput>
                (url, sabteAhvalInput), _userService.UserApplicationContext.Token, cancellationToken);
            return sabtAhvalServiceResult;

        }

    }
}
