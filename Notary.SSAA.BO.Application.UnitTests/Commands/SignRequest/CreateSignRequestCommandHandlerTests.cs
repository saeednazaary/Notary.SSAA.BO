namespace Notary.SSAA.BO.Application.UnitTests.Commands.SignRequest
{
    using FluentAssertions;
    using FluentValidation.TestHelper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Notary.SSAA.BO.Application.UnitTests.Fixtures;
    using Notary.SSAA.BO.Application.UnitTests.Seeds.SignRequestSeedData;
    using Notary.SSAA.BO.CommandHandler.SignRequest.Handlers;
    using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
    using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
    using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Infrastructure.Contexts;
    using Notary.SSAA.BO.Infrastructure.Persistence.Repositories;
    using Notary.SSAA.BO.QueryHandler.SignRequest;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Serilog;
    using System.Collections.Generic;
    using System.Security.Authentication;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="GetSignRequestByIdQueryHandlerTests" />
    /// </summary>
    public class CreateSignRequestCommandHandlerTests : TestBase
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
        /// Defines the _signRequestRepository
        /// </summary>
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly ISignRequestPersonRelatedRepository _signRequestPersonRelatedRepository;

        private readonly ISignRequestPersonRepository _signRequestPersonRepository;
        private readonly ISsrConfigRepository _ssrConfigRepository;
        private readonly Mock<IApplicationIdGeneratorService> _applicationIdGeneratorService;
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
        private readonly GetSignRequestByIdQueryHandler _realQueryHandler;

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
        public CreateSignRequestCommandHandlerTests ( )
        {
            _validator = new CreateSignRequestValidator ();
            _applicationContext = CreateDbContext ();
            _mediatorMock = new Mock<IMediator> ();
            _dateTimeServiceMock = new Mock<IDateTimeService> ();
            _userServiceMock = new Mock<IUserService> ();
            _applicationIdGeneratorService = new Mock<IApplicationIdGeneratorService> ();
            _signRequestRepository = new SignRequestRepository ( _applicationContext );
            _signRequestPersonRelatedRepository = new SignRequestPersonRelatedRepository ( _applicationContext );
            _signRequestPersonRepository = new SignRequestPersonRepository ( _applicationContext );
            _ssrConfigRepository = new SsrConfigRepository( _applicationContext );

            _loggerMock = new Mock<ILogger> ();

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

            _mediatorMock
                .Setup ( x => x.Send ( It.IsAny<GetSignRequestByIdQuery> (), It.IsAny<CancellationToken> () ) )
                .Returns<GetSignRequestByIdQuery, CancellationToken> ( ( query, token ) =>
                    _realQueryHandler.Handle ( query, token ) );

            _dateTimeServiceMock.Setup ( x => x.CurrentTime )
                .Returns ( "12:00" );
            _dateTimeServiceMock.Setup ( x => x.CurrentPersianDate )
                .Returns ( "1404/04/04" );
            _dateTimeServiceMock.Setup ( x => x.CurrentPersianDateTime )
                .Returns ( "1404/04/04-12:00" );
        }

        /// <summary>
        /// The InvalidPersonNames
        /// </summary>
        /// <returns>The <see cref="IEnumerable{object [ ]}"/></returns>
        public static IEnumerable<object [ ]> InvalidPersonNames ( )
        {
            yield return new object [ ] { "" };
            yield return new object [ ] { null };
            yield return new object [ ] { new string ( 'a', 151 ) }; // Dynamically create 151-char string
        }

        /// <summary>
        /// The InvalidPersonFamilies
        /// </summary>
        /// <returns>The <see cref="IEnumerable{object [ ]}"/></returns>
        public static IEnumerable<object [ ]> InvalidPersonFamilies ( )
        {
            yield return new object [ ] { "" };
            yield return new object [ ] { null };
            yield return new object [ ] { new string ( 'a', 101 ) }; // Dynamically create 151-char string
        }

        /// <summary>
        /// The InvalidPersonFatherNames
        /// </summary>
        /// <returns>The <see cref="IEnumerable{object [ ]}"/></returns>
        public static IEnumerable<object [ ]> InvalidPersonFatherNames ( )
        {
            yield return new object [ ] { "" };
            yield return new object [ ] { null };
            yield return new object [ ] { new string ( 'a', 101 ) }; // Dynamically create 151-char string
        }

        /// <summary>
        /// The InvalidIssueLocations
        /// </summary>
        /// <returns>The <see cref="IEnumerable{object [ ]}"/></returns>
        public static IEnumerable<object [ ]> InvalidIssueLocations ( )
        {
            yield return new object [ ] { "" };
            yield return new object [ ] { null };
            yield return new object [ ] { new string ( 'a', 101 ) }; // Dynamically create 151-char string
        }

        /// <summary>
        /// The InvalidEmails
        /// </summary>
        /// <returns>The <see cref="IEnumerable{object [ ]}"/></returns>
        public static IEnumerable<object [ ]> InvalidEmails ( )
        {
            yield return new object [ ] { "invalid-email" };
            yield return new object [ ] { "abc@" };
            yield return new object [ ] { "abc.com" };
            yield return new object [ ] { "a".PadLeft ( 151, 'a' ) + "@test.com" }; // Dynamically create 151-char string
        }

        /// <summary>
        /// The InvalidPersonDescriptions
        /// </summary>
        /// <returns>The <see cref="IEnumerable{object [ ]}"/></returns>
        public static IEnumerable<object [ ]> InvalidPersonDescriptions ( )
        {
            yield return new object [ ] { "a".PadLeft ( 2001, 'a' ) };
        }

        /// <summary>
        /// The InvalidPersonAddresses
        /// </summary>
        /// <returns>The <see cref="IEnumerable{object [ ]}"/></returns>
        public static IEnumerable<object [ ]> InvalidPersonAddresses ( )
        {
            yield return new object [ ] { "a".PadLeft ( 4001, 'a' ) };
        }

        /// <summary>
        /// The GetValidCommand
        /// </summary>
        /// <returns>The <see cref="CreateSignRequestCommand"/></returns>
        private CreateSignRequestCommand GetValidCommand ( )
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(SignRequestSeedData.CreateSignRequestCommand);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CreateSignRequestCommand> ( json );
        }

        /// <summary>
        /// The Should_Pass_When_Command_Is_Valid
        /// </summary>
        [Fact]
        public void Should_Pass_When_Command_Is_Valid ( )
        {
            var command = GetValidCommand();
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors ();
        }

        // ❌ TOP-LEVEL COMMAND RULES

        /// <summary>
        /// The Should_Fail_When_IsNew_IsFalse
        /// </summary>
        [Fact]
        public void Should_Fail_When_IsNew_IsFalse ( )
        {
            var command = GetValidCommand();
            command.IsNew = false;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor ( x => x.IsNew );
        }

        /// <summary>
        /// The Should_Fail_When_IsDelete_IsTrue
        /// </summary>
        [Fact]
        public void Should_Fail_When_IsDelete_IsTrue ( )
        {
            var command = GetValidCommand();
            command.IsDelete = true;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor ( x => x.IsDelete );
        }

        /// <summary>
        /// The Should_Fail_When_SignRequestGetterId_IsEmpty
        /// </summary>
     

        /// <summary>
        /// The Should_Fail_When_SignRequestSubjectId_IsEmpty
        /// </summary>
        [Fact]
        public void Should_Fail_When_SignRequestSubjectId_IsEmpty ( )
        {
            var command = GetValidCommand();
            command.SignRequestSubjectId = new List<string> ();
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor ( x => x.SignRequestSubjectId );
        }

        // ❌ PERSON VALIDATIONS

        /// <summary>
        /// The Should_Fail_When_PersonSexType_Invalid
        /// </summary>
        /// <param name="sexType">The sexType<see cref="string"/></param>
        [Theory]
        [InlineData ( null )]
        [InlineData ( "" )]
        [InlineData ( "3" )] // out of allowed range (1-2)
        public void Should_Fail_When_PersonSexType_Invalid ( string sexType )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonSexType = sexType;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonSexType" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonBirthDate_Invalid
        /// </summary>
        /// <param name="birthDate">The birthDate<see cref="string"/></param>
        [Theory]
        [InlineData ( null )]
        [InlineData ( "" )]
        [InlineData ( "1400/13/01343434343433" )] // invalid Persian date
        public void Should_Fail_When_PersonBirthDate_Invalid ( string birthDate )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonBirthDate = birthDate;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonBirthDate" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonNationalNo_Invalid
        /// </summary>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        [Theory]
        [InlineData ( null )]
        [InlineData ( "" )]
        [InlineData ( "123" )] // too short
        [InlineData ( "123456789012" )] // too long
        public void Should_Fail_When_PersonNationalNo_Invalid ( string nationalNo )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonNationalNo = nationalNo;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonNationalNo" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonName_Invalid
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        [Theory]
        [MemberData ( nameof ( InvalidPersonNames ) )]

        public void Should_Fail_When_PersonName_Invalid ( string name )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonName = name;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonName" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonFamily_Invalid
        /// </summary>
        /// <param name="family">The family<see cref="string"/></param>
        [Theory]
        [MemberData ( nameof ( InvalidPersonFamilies ) )]
        public void Should_Fail_When_PersonFamily_Invalid ( string family )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonFamily = family;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonFamily" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonFatherName_Invalid
        /// </summary>
        /// <param name="fatherName">The fatherName<see cref="string"/></param>
        [Theory]
        [MemberData ( nameof ( InvalidPersonFatherNames ) )]

        public void Should_Fail_When_PersonFatherName_Invalid ( string fatherName )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonFatherName = fatherName;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonFatherName" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonIdentityNo_Invalid
        /// </summary>
        /// <param name="identityNo">The identityNo<see cref="string"/></param>
        [Theory]
        [InlineData ( "" )]
        [InlineData ( null )]
        [InlineData ( "ABC123" )] // not numeric
        [InlineData ( "12345678901" )] // too long
        public void Should_Fail_When_PersonIdentityNo_Invalid ( string identityNo )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonIdentityNo = identityNo;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonIdentityNo" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonIdentityIssueLocation_Invalid
        /// </summary>
        /// <param name="issueLocation">The issueLocation<see cref="string"/></param>
        [Theory]
        [MemberData ( nameof ( InvalidIssueLocations ) )]

        public void Should_Fail_When_PersonIdentityIssueLocation_Invalid ( string issueLocation )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonIdentityIssueLocation = issueLocation;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonIdentityIssueLocation" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonPostalCode_Invalid
        /// </summary>
        /// <param name="postalCode">The postalCode<see cref="string"/></param>
        [Theory]
        [InlineData ( "" )]
        [InlineData ( null )]
        [InlineData ( "12345" )] // too short
        [InlineData ( "abcdefghij" )] // not numeric
        public void Should_Fail_When_PersonPostalCode_Invalid ( string postalCode )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonPostalCode = postalCode;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonPostalCode" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonMobileNo_Invalid
        /// </summary>
        /// <param name="mobile">The mobile<see cref="string"/></param>
        [Theory]
        [InlineData ( "" )]
        [InlineData ( null )]
        [InlineData ( "12345" )] // too short
        [InlineData ( "abcdefghijk" )] // invalid
        public void Should_Fail_When_PersonMobileNo_Invalid ( string mobile )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonMobileNo = mobile;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonMobileNo" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonTel_Invalid
        /// </summary>
        /// <param name="tel">The tel<see cref="string"/></param>
        [Theory]
        [InlineData ( "invalid-phone" )] // not numeric
        public void Should_Fail_When_PersonTel_Invalid ( string tel )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonTel = tel;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonTel" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonEmail_Invalid
        /// </summary>
        /// <param name="email">The email<see cref="string"/></param>
        [Theory]
        [MemberData ( nameof ( InvalidEmails ) )]

        public void Should_Fail_When_PersonEmail_Invalid ( string email )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonEmail = email;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonEmail" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonDescription_TooLong
        /// </summary>
        /// <param name="description">The description<see cref="string"/></param>
        [Theory]
        [MemberData ( nameof ( InvalidPersonDescriptions ) )]
        public void Should_Fail_When_PersonDescription_TooLong ( string description )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonDescription = description;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonDescription" ) );
        }

        /// <summary>
        /// The Should_Fail_When_PersonAddress_Invalid
        /// </summary>
        /// <param name="address">The address<see cref="string"/></param>
        [Theory]
        [MemberData ( nameof ( InvalidPersonAddresses ) )]

        public void Should_Fail_When_PersonAddress_Invalid ( string address )
        {
            var command = GetValidCommand();
            command.SignRequestPersons [ 0 ].PersonAddress = address;
            var result = _validator.TestValidate(command);
            result.Errors.Should ().Contain ( e =>
                e.PropertyName.Contains ( "PersonAddress" ) );
        }

        /// <summary>
        /// The Handle_UserWithoutRequiredRole_ShouldReturnAuthenticationException
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task Handle_UserWithoutRequiredRole_ShouldReturnAuthenticationException ( )
        {
            // Arrange

            var command = new CreateSignRequestCommand();
            _userServiceMock.Setup ( x => x.UserApplicationContext )
                .Returns ( CreateUnAuthorizedUser () );

            // Act
            ApiResult result=new ApiResult{IsSuccess = true};
            try
            {
                result = await _handler.Handle ( command, CancellationToken.None );

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
            // Arrange
            _userServiceMock.Setup ( x => x.UserApplicationContext )
                .Returns ( CreateSaradaftarUser () );
            _applicationContext.SignRequestSubjects.AddRange ( SignRequestSeedData.GetSignRequestSubjects() );
            _applicationContext.SignRequestGetters.AddRange ( SignRequestSeedData.GetSignRequestGetters() );
            _applicationContext.SignRequestSubjectGroups.AddRange ( SignRequestSeedData.GetSignRequestSubjectGroups() );
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

            var result = await _handler.Handle(command, CancellationToken.None);
        }

        /// <summary>
        /// The Handle_ValidCommand_Empty_SignRequest
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task Handle_ValidCommand_Empty_SignRequest ( )
        {
            // Arrange
            using var context = CreateDbContext();
            var item= await context.SignRequests.FirstOrDefaultAsync();

            Assert.Null ( item );
        }
    }
}
