using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Notary.SSAA.BO.CommandHandler.SignRequest.Handlers;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.FunctionalTests.Fixtures;
using Notary.SSAA.BO.FunctionalTests.Seeds.SignRequestSeedData;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories;
using Notary.SSAA.BO.QueryHandler.SignRequest;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.WebApi.Controllers.v1;
using Serilog;


namespace Notary.SSAA.BO.FunctionalTests.SignRequest
{
    public class SignRequestUpdateApiTests: TestBase
    {
        /// <summary>
        /// Defines the _mediatorMock
        /// </summary>
        private readonly Mock<IMediator> _mediatorMock;

        /// <summary>
        /// Defines the _dateTimeServiceMock
        /// </summary>
        private readonly Mock<IDateTimeService> _dateTimeServiceMock;
        private readonly Mock<IApplicationIdGeneratorService> _applicationIdGeneratorService;

        /// <summary>
        /// Defines the _userServiceMock
        /// </summary>
        private readonly Mock<IUserService> _userServiceMock;

        /// <summary>
        /// Defines the _signRequestRepository
        /// </summary>


        private readonly ISignRequestPersonRepository _signRequestPersonRepository;
        private readonly ISsrConfigRepository _ssrConfigRepository;

        /// <summary>
        /// Defines the _loggerMock
        /// </summary>
        private readonly Mock<ILogger> _loggerMock;

        /// <summary>
        /// Defines the _handler
        /// </summary>
        private readonly UpdateSignRequestCommandHandler _commandhandler;

        /// <summary>
        /// Defines the _queryHandler
        /// </summary>
        private readonly GetSignRequestByIdQueryHandler _queryHandler;

        /// <summary>
        /// Defines the _applicationContext
        /// </summary>
        private readonly ApplicationContext _applicationContext;


        private readonly SignRequestController _controller;
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly ISignRequestPersonRelatedRepository _signRequestPersonRelatedRepository;

        public SignRequestUpdateApiTests ( )
        {
            _applicationContext = CreateDbContext ();
            _mediatorMock = new Mock<IMediator> ();
            _dateTimeServiceMock = new Mock<IDateTimeService> ();
            _applicationIdGeneratorService = new Mock<IApplicationIdGeneratorService> ();
            _userServiceMock = new Mock<IUserService> ();
            _signRequestRepository = new SignRequestRepository ( _applicationContext );
            _signRequestPersonRelatedRepository = new SignRequestPersonRelatedRepository ( _applicationContext );
            _signRequestPersonRepository = new SignRequestPersonRepository ( _applicationContext );
            _ssrConfigRepository = new SsrConfigRepository(_applicationContext);

            _loggerMock = new Mock<ILogger> ();

            _commandhandler = new UpdateSignRequestCommandHandler (
                _mediatorMock.Object,
                _dateTimeServiceMock.Object,
                _userServiceMock.Object,
                _signRequestRepository,
                _loggerMock.Object,
                _applicationIdGeneratorService.Object,
                _ssrConfigRepository
                
            );
            _queryHandler = new GetSignRequestByIdQueryHandler (
                _mediatorMock.Object,
                _userServiceMock.Object,
                _signRequestRepository,
                _applicationIdGeneratorService.Object);


            _mediatorMock.Setup ( x => x.Send ( It.IsAny<UpdateSignRequestCommand> (), It.IsAny<CancellationToken> () ) )
                .Returns<UpdateSignRequestCommand, CancellationToken> ( ( query, token ) =>
                    _commandhandler.Handle ( query, token ) );

       

            _dateTimeServiceMock.Setup ( x => x.CurrentTime )
                .Returns ( "12:00" );
            _dateTimeServiceMock.Setup ( x => x.CurrentPersianDate )
                .Returns ( "1404/04/04" );
            _dateTimeServiceMock.Setup ( x => x.CurrentPersianDateTime )
                .Returns ( "1404/04/04-12:00" );



            _httpContextMock = new Mock<HttpContext> ();

            _controller = new SignRequestController ( _mediatorMock.Object )
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        [Fact]
        public async Task Update_WithValidCommand_ReturnsOkResult ( )
        {
            _applicationContext.SignRequests.RemoveRange ( _applicationContext.SignRequests );
            // Arrange
            _userServiceMock.Setup ( x => x.UserApplicationContext )
                  .Returns ( CreateSaradaftarUser () );
            _applicationContext.SignRequestSubjectGroups.AddRange ( SignRequestSeedData.GetSignRequestSubjectGroups () );

            _applicationContext.SignRequestSubjects.AddRange ( SignRequestSeedData.GetSignRequestSubjects () );
            _applicationContext.SignRequestGetters.AddRange ( SignRequestSeedData.GetSignRequestGetters () );
            _applicationContext.AgentTypes.AddRange ( SignRequestSeedData.GetAgentTypes () );
            _applicationContext.SsrConfigMainSubjects.AddRange(SignRequestSeedData.GetMainSubjects());

            _applicationContext.SsrConfigSubjects.AddRange(SignRequestSeedData.GetSubjects());

            _applicationContext.SsrConfigs.AddRange(SignRequestSeedData.GetConfigs());

            _applicationContext.SsrConfigConditionScrptrms.AddRange(SignRequestSeedData.GetConfigConditionScrptrm());

            _applicationContext.SsrConfigConditionTimes.AddRange(SignRequestSeedData.GetConfigConditionTime());
            await _applicationContext.SaveChangesAsync ( CancellationToken.None );
            var command =SignRequestSeedData.CreateSignRequestCommand;
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
            var validCommand =SignRequestSeedData.UpdateSignRequestCommand;


            _mediatorMock
         .Setup ( x => x.Send ( It.IsAny<GetSignRequestByIdQuery> (), It.IsAny<CancellationToken> () ) )
         .Returns<GetSignRequestByIdQuery, CancellationToken> ( ( query, token ) =>
             _queryHandler.Handle ( query, token ) );

            // Act
            var result = await _controller.Update(validCommand);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
         
        }

    }
}
