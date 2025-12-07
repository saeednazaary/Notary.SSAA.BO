using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Notary.SSAA.BO.CommandHandler.SignRequest.Handlers;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.FunctionalTests.Seeds.SignRequestSeedData;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories;
using Notary.SSAA.BO.QueryHandler.Fingerprint;
using Notary.SSAA.BO.QueryHandler.SignRequest;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.WebApi.Controllers.v1;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.IntegrationTests.SignRequest
{
    public class SignRequestCreateApiTests: TestBase
    {
        /// <summary>
        /// Defines the _mediatorMock
        /// </summary>
        private readonly Mock<IMediator> _mediatorMock;

        /// <summary>
        /// Defines the _dateTimeServiceMock
        /// </summary>
        private readonly Mock<IDateTimeService> _dateTimeServiceMock;

        /// <summary>
        /// Defines the _userServiceMock
        /// </summary>
        private readonly Mock<IUserService> _userServiceMock;
        /// <summary>
        /// Defines the _applicationIdGeneratorService
        /// </summary>
        private readonly Mock<IApplicationIdGeneratorService> _applicationIdGeneratorService;
        /// <summary>
        /// Defines the _signRequestRepository
        /// </summary>
        private readonly ISignRequestPersonRepository _signRequestPersonRepository;


        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly ISsrConfigRepository _ssrConfigRepository;

        /// <summary>
        /// Defines the _loggerMock
        /// </summary>
        private readonly Mock<ILogger> _loggerMock;

        private readonly GetInquiryPersonFingerprintListQueryHandler _getInquiryPersonFingerprintListQueryHandler;
        private readonly UpdateSignRequestFingerprintStateCommandHandler _updateSignRequestFingerprintStateCommandHandler;

        /// <summary>
        /// Defines the _handler
        /// </summary>
        private readonly CreateSignRequestCommandHandler _handler;

        /// <summary>
        /// Defines the _queryHandler
        /// </summary>
        private readonly GetSignRequestByIdQueryHandler _realQueryHandler;

        /// <summary>
        /// Defines the _applicationContext
        /// </summary>
        private readonly ApplicationContext _applicationContext;


        private readonly SignRequestController _controller;
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly Mock<ClaimsPrincipal> _userMock;
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly ISignRequestPersonRelatedRepository _signRequestPersonRelatedRepository;

        public SignRequestCreateApiTests ( )
        {
            _applicationContext = CreateDbContext ();
            _mediatorMock = new Mock<IMediator> ();
            _dateTimeServiceMock = new Mock<IDateTimeService> ();
            _userServiceMock = new Mock<IUserService> ();
            _signRequestRepository = new SignRequestRepository ( _applicationContext );
            _signRequestPersonRelatedRepository = new SignRequestPersonRelatedRepository ( _applicationContext );
            _signRequestPersonRepository = new SignRequestPersonRepository ( _applicationContext );
            _personFingerprintRepository=new PersonFingerprintRepository ( _applicationContext );
            _ssrConfigRepository = new SsrConfigRepository( _applicationContext );
            _loggerMock = new Mock<ILogger> ();
            _applicationIdGeneratorService = new Mock<IApplicationIdGeneratorService> ();

            _handler = new CreateSignRequestCommandHandler (
                _mediatorMock.Object,
                _dateTimeServiceMock.Object,
                _userServiceMock.Object,
                _signRequestRepository,
                _loggerMock.Object,
                _applicationIdGeneratorService.Object,
                _ssrConfigRepository
            );
            _realQueryHandler = new GetSignRequestByIdQueryHandler (
                _mediatorMock.Object,
                _userServiceMock.Object,
                _signRequestRepository,
                _applicationIdGeneratorService.Object);
            _getInquiryPersonFingerprintListQueryHandler = new GetInquiryPersonFingerprintListQueryHandler ( _mediatorMock.Object,
                _userServiceMock.Object,
                 _personFingerprintRepository
              );

            _updateSignRequestFingerprintStateCommandHandler = new UpdateSignRequestFingerprintStateCommandHandler ( _mediatorMock.Object,
              _userServiceMock.Object,
               _loggerMock.Object, _signRequestRepository,
               _applicationIdGeneratorService.Object,
               _ssrConfigRepository,
               _dateTimeServiceMock.Object
            );
            

            _mediatorMock.Setup ( x => x.Send ( It.IsAny<CreateSignRequestCommand> (), It.IsAny<CancellationToken> () ) )
                .Returns<CreateSignRequestCommand, CancellationToken> ( ( query, token ) =>
                    _handler.Handle ( query, token ) );

            _mediatorMock.Setup ( x => x.Send ( It.IsAny<GetInquiryPersonFingerprintListQuery> (), It.IsAny<CancellationToken> () ) )
                   .Returns<GetInquiryPersonFingerprintListQuery, CancellationToken> ( ( query, token ) =>
                       _getInquiryPersonFingerprintListQueryHandler.Handle ( query, token ) );

            _mediatorMock.Setup ( x => x.Send ( It.IsAny<UpdateSignRequestFingerprintStateCommand> (), It.IsAny<CancellationToken> () ) )
                 .Returns<UpdateSignRequestFingerprintStateCommand, CancellationToken> ( ( query, token ) =>
                     _updateSignRequestFingerprintStateCommandHandler.Handle ( query, token ) );

            _dateTimeServiceMock.Setup ( x => x.CurrentTime )
                .Returns ( "12:00" );
            _dateTimeServiceMock.Setup ( x => x.CurrentPersianDate )
                .Returns ( "1404/04/04" );
            _dateTimeServiceMock.Setup ( x => x.CurrentPersianDateTime )
                .Returns ( "1404/04/04-12:00" );



            _httpContextMock = new Mock<HttpContext> ();
            _userMock = new Mock<ClaimsPrincipal> ();

            _controller = new SignRequestController ( _mediatorMock.Object )
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        [Fact]
        public async Task UpdatePersons_SetFingerPrints_ReturnsIsFingerPrintGotten ( )
        {
            _applicationContext.SignRequests.RemoveRange ( _applicationContext.SignRequests );
            // Arrange
            _userServiceMock.Setup ( x => x.UserApplicationContext )
                  .Returns ( CreateSaradaftarUser () );
            _applicationContext.SignRequestSubjectGroups.AddRange ( SignRequestSeedData.GetSignRequestSubjectGroups () );
            _applicationContext.SignRequestSubjects.AddRange ( SignRequestSeedData.GetSignRequestSubjects () );
            _applicationContext.SignRequestGetters.AddRange ( SignRequestSeedData.GetSignRequestGetters () );
            _applicationContext.AgentTypes.AddRange ( SignRequestSeedData.GetAgentTypes () );
            _applicationContext.SignRequests.AddRange ( SignRequestSeedData.GetSignRequests () );
            _applicationContext.PersonFingerTypes.AddRange ( SignRequestSeedData.GetPersonFingerTypes () );
            _applicationContext.PersonFingerprintUseCases.AddRange ( SignRequestSeedData.GetPersonFingerprintUseCases () );
            _applicationContext.PersonFingerprints.AddRange ( SignRequestSeedData.GetPersonFingerPrints () );
            _applicationContext.SsrConfigMainSubjects.AddRange(SignRequestSeedData.GetMainSubjects());
            _applicationContext.SsrConfigSubjects.AddRange(SignRequestSeedData.GetSubjects());
            _applicationContext.SsrConfigs.AddRange(SignRequestSeedData.GetConfigs());
            _applicationContext.SsrConfigConditionScrptrms.AddRange(SignRequestSeedData.GetConfigConditionScrptrm());
            _applicationContext.SsrConfigConditionTimes.AddRange(SignRequestSeedData.GetConfigConditionTime());
            await _applicationContext.SaveChangesAsync ( CancellationToken.None );
            var command =SignRequestSeedData.UpdatePeopleOfSignRequestCommand;
            var updateRemoteRequestFingerprintStateCommand=new UpdateSignRequestFingerprintStateCommand();
            updateRemoteRequestFingerprintStateCommand.SignRequestId = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
            var mediaResult = new ApiResult { IsSuccess = true };
            _mediatorMock.Setup ( x => x.Send ( It.IsAny<CreateMediaServiceInput> (), It.IsAny<CancellationToken> () ) )
                .ReturnsAsync ( mediaResult );
            var personalImagesResult = new ApiResult<GetPersonPhotoListViewModel>
            {
                IsSuccess = true,
                Data = new GetPersonPhotoListViewModel
                {
                    PersonsData = new List<PersonPhotoServiceViewModel>
                    {
                        new PersonPhotoServiceViewModel()
                        {
                            NationalNo = "",
                            PersonalImage = null
                        }
                    }
                }
            };

            _mediatorMock
                .Setup ( x => x.Send ( It.IsAny<GetPersonPhotoListServiceInput> (), It.IsAny<CancellationToken> () ) )
                .ReturnsAsync ( personalImagesResult );
            var validCommand =SignRequestSeedData.CreateSignRequestCommand;


            _mediatorMock
         .Setup ( x => x.Send ( It.IsAny<GetSignRequestByIdQuery> (), It.IsAny<CancellationToken> () ) )
         .Returns<GetSignRequestByIdQuery, CancellationToken> ( ( query, token ) =>
             _realQueryHandler.Handle ( query, token ) );

            // Act
            var result = await _controller.Update(command);
            var addFingerPrintResult=await _controller.FingerprintState(updateRemoteRequestFingerprintStateCommand);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(addFingerPrintResult);


        }
    }
}
