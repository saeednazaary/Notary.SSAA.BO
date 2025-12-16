namespace Notary.SSAA.BO.Application.UnitTests.Commands.SignRequest
{
    using FluentAssertions;
    using FluentValidation.TestHelper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Notary.SSAA.BO.Application.UnitTests.Fixtures;
    using Notary.SSAA.BO.Application.UnitTests.Seeds.SignRequestSeedData;
    using Notary.SSAA.BO.CommandHandler.SignRequest.Handlers;
    using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
    using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
    using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Infrastructure.Contexts;
    using Notary.SSAA.BO.Infrastructure.Persistence.Repositories;
    using Notary.SSAA.BO.Infrastructure.Services.Implementations.ApplicationIdGenerator;
    using Notary.SSAA.BO.QueryHandler.SignRequest;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Serilog;
    using System.Collections.Generic;
    using System.Security.Authentication;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="GetSignRequestByIdQueryHandlerTests" />
    /// </summary>
    public class GetSignRequestByIdQueryHandlerTests : TestBase
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
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        /// <summary>
        /// Defines the _signRequestRepository
        /// </summary>
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly ISignRequestPersonRelatedRepository _signRequestPersonRelatedRepository;

        private readonly ISignRequestPersonRepository _signRequestPersonRepository;

        /// <summary>
        /// Defines the _loggerMock
        /// </summary>
        private readonly Mock<ILogger> _loggerMock;

        /// <summary>
        /// Defines the _handler
        /// </summary>
        private readonly CreateSignRequestCommandHandler _handler;

        /// <summary>
        /// Defines the _queryHandler
        /// </summary>
        private readonly GetSignRequestByIdQueryHandler _queryHandler;

        /// <summary>
        /// Defines the _applicationContext
        /// </summary>
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Defines the _validator
        /// </summary>
        private readonly CreateSignRequestValidator _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSignRequestByIdQueryHandlerTests"/> class.
        /// </summary>
        public GetSignRequestByIdQueryHandlerTests ( )
        {
            var configData = new Dictionary<string, string?>
{
    { "aesEncryptionKey", Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)) } // 256‑bit key
};
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();

            _validator = new CreateSignRequestValidator ();
            _applicationContext = CreateDbContext ();
            _mediatorMock = new Mock<IMediator> ();
            _dateTimeServiceMock = new Mock<IDateTimeService> ();
            _userServiceMock = new Mock<IUserService> ();
            _signRequestRepository = new SignRequestRepository ( _applicationContext );
            _signRequestPersonRelatedRepository = new SignRequestPersonRelatedRepository ( _applicationContext );
            _signRequestPersonRepository = new SignRequestPersonRepository ( _applicationContext );
            _applicationIdGeneratorService = new ApplicationIdGeneratorService(configuration);
            _loggerMock = new Mock<ILogger> ();

           
            _queryHandler = new GetSignRequestByIdQueryHandler (
                _mediatorMock.Object,
                _userServiceMock.Object,
                _signRequestRepository,
                _applicationIdGeneratorService);

            _mediatorMock
                .Setup ( x => x.Send ( It.IsAny<GetSignRequestByIdQuery> (), It.IsAny<CancellationToken> () ) )
                .Returns<GetSignRequestByIdQuery, CancellationToken> ( ( query, token ) =>
                    _queryHandler.Handle ( query, token ) );

            _dateTimeServiceMock.Setup ( x => x.CurrentTime )
                .Returns ( "12:00" );
            _dateTimeServiceMock.Setup ( x => x.CurrentPersianDate )
                .Returns ( "1404/04/04" );
            _dateTimeServiceMock.Setup ( x => x.CurrentPersianDateTime )
                .Returns ( "1404/04/04-12:00" );
            _applicationContext.AddRange ( SignRequestSeedData.GetSignRequestSubjectGroups () );
            _applicationContext.AddRange ( SignRequestSeedData.GetSignRequestSubjects () );
            _applicationContext.AddRange ( SignRequestSeedData.GetSignRequestGetters () );
            _applicationContext.AddRange ( SignRequestSeedData.GetSsrSignEbookBaseinfoes () );
            _applicationContext.AddRange ( SignRequestSeedData.GetSignRequests () );
            _applicationContext.AddRange ( SignRequestSeedData.GetAgentTypes () );
            _applicationContext.SaveChanges ();
        }



        [Fact]          
        public async Task Handle_UserWithoutRequiredRole_ShouldReturnAuthenticationException ( )
        {
            _userServiceMock.Setup ( x => x.UserApplicationContext )
               .Returns ( CreateUnAuthorizedUser () );
            // Arrange
            var signRequestId= "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
            var query = new GetSignRequestByIdQuery(signRequestId);
            _userServiceMock.Setup ( x => x.UserApplicationContext )
                .Returns ( CreateUnAuthorizedUser () );

            // Act
            ApiResult result=new ApiResult{IsSuccess = true};
            try
            {
                result = await _queryHandler.Handle ( query, CancellationToken.None );

            }
            catch ( AuthenticationException exception )
            {
                result.IsSuccess = false;
            }

            // Assert
            result.IsSuccess.Should ().BeFalse ();
        }

        /// <summary>
        /// The Handle_ValidCommand_ShouldCreateSignRequest
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateSignRequest ( )
        {
            _userServiceMock.Setup ( x => x.UserApplicationContext )
               .Returns ( CreateSaradaftarUser () );

            var signRequestId= "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
            var query = new GetSignRequestByIdQuery(signRequestId);
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

            var result = await _queryHandler.Handle(query, CancellationToken.None);
            Assert.True ( result.IsSuccess );
        }

        /// <summary>
        /// The Handle_ValidCommand_Empty_SignRequest
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task Handle_ValidCommand_Empty_SignRequest ( )
        {
            _userServiceMock.Setup ( x => x.UserApplicationContext )
               .Returns ( CreateSaradaftarUser () );
            var signRequestId= "49f720b6-c03b-4dfd-908e-cff35e8c6bed";
            var query = new GetSignRequestByIdQuery(signRequestId);
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

            var result = await _queryHandler.Handle(query, CancellationToken.None);
            Assert.False ( result.IsSuccess );
        }
    }
}
