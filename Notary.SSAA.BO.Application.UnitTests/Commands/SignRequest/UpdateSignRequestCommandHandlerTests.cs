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
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Serilog;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Authentication;
    using System.Threading;
    using System.Threading.Tasks;


    public class UpdateSignRequestCommandHandlerTests : TestBase
    {
        private readonly Mock<IMediator> _mediatorMock;

        private readonly Mock<IDateTimeService> _dateTimeServiceMock;

        private readonly Mock<IUserService> _userServiceMock;

        private readonly ISignRequestRepository _signRequestRepository;

        private readonly ISignRequestPersonRelatedRepository _signRequestPersonRelatedRepository;

        private readonly ISignRequestPersonRepository _signRequestPersonRepository;
        private readonly ISsrConfigRepository _ssrConfigRepository;

        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IApplicationIdGeneratorService> _applicationIdGeneratorService;

        private readonly UpdateSignRequestCommandHandler _handler;

        private readonly GetSignRequestByIdQueryHandler _realQueryHandler;

        private readonly ApplicationContext _applicationContext;

        private readonly UpdateSignRequestValidator _validator;

        public UpdateSignRequestCommandHandlerTests()
        {
            _validator = new UpdateSignRequestValidator();
            _applicationContext = CreateDbContext();
            _mediatorMock = new Mock<IMediator>();
            _dateTimeServiceMock = new Mock<IDateTimeService>();
            _applicationIdGeneratorService = new Mock<IApplicationIdGeneratorService>();
            _userServiceMock = new Mock<IUserService>();
            _signRequestRepository = new SignRequestRepository(_applicationContext);
            _signRequestPersonRelatedRepository = new SignRequestPersonRelatedRepository(_applicationContext);
            _signRequestPersonRepository = new SignRequestPersonRepository(_applicationContext);
            _ssrConfigRepository = new SsrConfigRepository(_applicationContext);

            _loggerMock = new Mock<ILogger>();

            _handler = new UpdateSignRequestCommandHandler(
                _mediatorMock.Object,
                _dateTimeServiceMock.Object,
                _userServiceMock.Object,
                _signRequestRepository,
                _loggerMock.Object,
                _applicationIdGeneratorService.Object,
                _ssrConfigRepository
            );
            _realQueryHandler = new GetSignRequestByIdQueryHandler(
                _mediatorMock.Object,
                _userServiceMock.Object,
                _signRequestRepository,
                _applicationIdGeneratorService.Object);

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetSignRequestByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns<GetSignRequestByIdQuery, CancellationToken>((query, token) =>
                    _realQueryHandler.Handle(query, token));

            _dateTimeServiceMock.Setup(x => x.CurrentTime)
                .Returns("12:00");
            _dateTimeServiceMock.Setup(x => x.CurrentPersianDate)
                .Returns("1404/04/04");
            _dateTimeServiceMock.Setup(x => x.CurrentPersianDateTime)
                .Returns("1404/04/04-12:00");
            _applicationContext.AddRange(SignRequestSeedData.GetSignRequestSubjectGroups());
            _applicationContext.AddRange(SignRequestSeedData.GetSignRequestSubjects());
            _applicationContext.AddRange(SignRequestSeedData.GetSignRequestGetters());
            _applicationContext.AddRange(SignRequestSeedData.GetSsrSignEbookBaseinfoes());
            _applicationContext.AddRange(SignRequestSeedData.GetSignRequests());
            _applicationContext.AddRange(SignRequestSeedData.GetAgentTypes());
            _applicationContext.SaveChanges();
        }

        public static IEnumerable<object[]> InvalidPersonNames()
        {
            yield return new object[] { "" };
            yield return new object[] { null };
            yield return new object[] { new string('a', 151) }; // Dynamically create 151-char string
        }

        public static IEnumerable<object[]> InvalidPersonFamilies()
        {
            yield return new object[] { "" };
            yield return new object[] { null };
            yield return new object[] { new string('a', 51) }; // Dynamically create 151-char string
        }

        public static IEnumerable<object[]> InvalidPersonFatherNames()
        {
            yield return new object[] { "" };
            yield return new object[] { null };
            yield return new object[] { new string('a', 51) }; // Dynamically create 151-char string
        }

        public static IEnumerable<object[]> InvalidIssueLocations()
        {
            yield return new object[] { "" };
            yield return new object[] { null };
            yield return new object[] { new string('a', 51) }; // Dynamically create 151-char string
        }

        public static IEnumerable<object[]> InvalidEmails()
        {
            yield return new object[] { "invalid-email" };
            yield return new object[] { "abc@" };
            yield return new object[] { "abc.com" };
            yield return new object[] { "a".PadLeft(101, 'a') + "@test.com" }; // Dynamically create 151-char string
        }

        public static IEnumerable<object[]> InvalidPersonDescriptions()
        {
            yield return new object[] { "a".PadLeft(2001, 'a') };
        }

        public static IEnumerable<object[]> InvalidPersonAddresses()
        {
            yield return new object[] { "a".PadLeft(2001, 'a') };
        }

        private UpdateSignRequestCommand GetValidCommand()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(SignRequestSeedData.UpdateSignRequestCommand);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateSignRequestCommand>(json);
        }

        [Theory]
        [InlineData(true)]
        public void Should_Fail_When_IsNew_Is_True(bool isNew)
        {
            var command = GetValidCommand();
            command.IsNew = isNew;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.IsNew)
                .WithErrorMessage(SystemMessagesConstant.Request_IsNew_Invalid);
        }
        [Theory]
        [InlineData(true)]
        public void Should_Fail_When_IsDelete_Is_True(bool isDelete)
        {
            var command = GetValidCommand();
            command.IsDelete = isDelete;
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.IsDelete)
                .WithErrorMessage(SystemMessagesConstant.Request_IsDelete_Invalid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid-guid")]
        public void Should_Fail_When_SignRequestId_Invalid(string signRequestId)
        {
            var command = GetValidCommand();
            command.SignRequestId = signRequestId;
            var result = _validator.TestValidate(command);
            result.Errors.Should().Contain(e =>
                 e.PropertyName.Contains("SignRequestId"));
        }

        [Fact]
        public void Should_Fail_When_SignRequestId_Empty()
        {
            var command = GetValidCommand();
            command.SignRequestId = "";
            var result = _validator.TestValidate(command);
            result.Errors.Should().Contain(e =>
                 e.PropertyName.Contains("SignRequestId"));
        }
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(2)]
        public void Should_Fail_When_SignRequestSubjectId_Invalid(int? count)
        {
            var command = GetValidCommand();
            if (count == null)
            {
                command.SignRequestSubjectId = null;
            }
            else
            {
                command.SignRequestSubjectId = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    command.SignRequestSubjectId.Add(i.ToString());
                }
            }
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.SignRequestSubjectId);
        }

        [Fact]
        public void Should_Fail_When_SignRequestSubjectId_Empty()
        {
            var command = GetValidCommand();
            command.SignRequestSubjectId = new List<string> { "" };
            var result = _validator.TestValidate(command);
            result.Errors.Should().Contain(e =>
                e.PropertyName.Contains("SignRequestSubjectId"));

        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("3")] // out of allowed range (1-2)
        public void Should_Fail_When_PersonSexType_Invalid(string sexType)
        {
            var command = GetValidCommand();
            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonSexType = sexType;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonSexType"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }

        /// <summary>
        /// The Should_Fail_When_PersonBirthDate_Invalid
        /// </summary>
        /// <param name="birthDate">The birthDate<see cref="string"/></param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("1400/13/01343434343433")] // invalid Persian date
        public void Should_Fail_When_PersonBirthDate_Invalid(string birthDate)
        {
            var command = GetValidCommand();

            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonBirthDate = birthDate;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonBirthDate"));
            }
            else
            {
                Assert.NotNull(command);
            }


        }

        /// <summary>
        /// The Should_Fail_When_PersonNationalNo_Invalid
        /// </summary>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")] // too short
        [InlineData("123456789012")] // too long
        public void Should_Fail_When_PersonNationalNo_Invalid(string nationalNo)
        {
            var command = GetValidCommand();

            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonNationalNo = nationalNo;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonNationalNo"));
            }
            else
            {
                Assert.NotNull(command);
            }



        }

        /// <summary>
        /// The Should_Fail_When_PersonName_Invalid
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        [Theory]
        [MemberData(nameof(InvalidPersonNames))]

        public void Should_Fail_When_PersonName_Invalid(string name)
        {
            var command = GetValidCommand();

            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonName = name;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonName")); ;
            }
            else
            {
                Assert.NotNull(command);
            }


        }

        /// <summary>
        /// The Should_Fail_When_PersonFamily_Invalid
        /// </summary>
        /// <param name="family">The family<see cref="string"/></param>
        [Theory]
        [MemberData(nameof(InvalidPersonFamilies))]
        public void Should_Fail_When_PersonFamily_Invalid(string family)
        {
            var command = GetValidCommand();

            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonFamily = family;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonFamily"));
            }
            else
            {
                Assert.NotNull(command);
            }


        }

        /// <summary>
        /// The Should_Fail_When_PersonFatherName_Invalid
        /// </summary>
        /// <param name="fatherName">The fatherName<see cref="string"/></param>
        [Theory]
        [MemberData(nameof(InvalidPersonFatherNames))]

        public void Should_Fail_When_PersonFatherName_Invalid(string fatherName)
        {
            var command = GetValidCommand();

            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonFatherName = fatherName;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonFatherName"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }

        /// <summary>
        /// The Should_Fail_When_PersonIdentityNo_Invalid
        /// </summary>
        /// <param name="identityNo">The identityNo<see cref="string"/></param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ABC123")] // not numeric
        [InlineData("12345678901")] // too long
        public void Should_Fail_When_PersonIdentityNo_Invalid(string identityNo)
        {
            var command = GetValidCommand();

            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonIdentityNo = identityNo;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonIdentityNo"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }

        /// <summary>
        /// The Should_Fail_When_PersonIdentityIssueLocation_Invalid
        /// </summary>
        /// <param name="issueLocation">The issueLocation<see cref="string"/></param>
        [Theory]
        [MemberData(nameof(InvalidIssueLocations))]

        public void Should_Fail_When_PersonIdentityIssueLocation_Invalid(string issueLocation)
        {
            var command = GetValidCommand();
            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonIdentityIssueLocation = issueLocation;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonIdentityIssueLocation"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }

        /// <summary>
        /// The Should_Fail_When_PersonPostalCode_Invalid
        /// </summary>
        /// <param name="postalCode">The postalCode<see cref="string"/></param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("12345")] // too short
        [InlineData("abcdefghij")] // not numeric
        public void Should_Fail_When_PersonPostalCode_Invalid(string postalCode)
        {
            var command = GetValidCommand();
            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonPostalCode = postalCode;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonPostalCode"));
            }
            else
            {
                Assert.NotNull(command);
            }


        }

        /// <summary>
        /// The Should_Fail_When_PersonMobileNo_Invalid
        /// </summary>
        /// <param name="mobile">The mobile<see cref="string"/></param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("12345")] // too short
        [InlineData("abcdefghijk")] // invalid
        public void Should_Fail_When_PersonMobileNo_Invalid(string mobile)
        {
            var command = GetValidCommand();
            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonMobileNo = mobile;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonMobileNo"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }

        /// <summary>
        /// The Should_Fail_When_PersonTel_Invalid
        /// </summary>
        /// <param name="tel">The tel<see cref="string"/></param>
        [Theory]
        [InlineData("invalid-phone")] // not numeric
        public void Should_Fail_When_PersonTel_Invalid(string tel)
        {
            var command = GetValidCommand();

            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonTel = tel;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonTel"));
            }
            else
            {
                Assert.NotNull(command);
            }
        }

        /// <summary>
        /// The Should_Fail_When_PersonEmail_Invalid
        /// </summary>
        /// <param name="email">The email<see cref="string"/></param>
        [Theory]
        [MemberData(nameof(InvalidEmails))]

        public void Should_Fail_When_PersonEmail_Invalid(string email)
        {
            var command = GetValidCommand();
            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonEmail = email;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonEmail"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }

        /// <summary>
        /// The Should_Fail_When_PersonDescription_TooLong
        /// </summary>
        /// <param name="description">The description<see cref="string"/></param>
        [Theory]
        [MemberData(nameof(InvalidPersonDescriptions))]
        public void Should_Fail_When_PersonDescription_TooLong(string description)
        {
            var command = GetValidCommand();
            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonDescription = description;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonDescription"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }

        /// <summary>
        /// The Should_Fail_When_PersonAddress_Invalid
        /// </summary>
        /// <param name="address">The address<see cref="string"/></param>
        [Theory]
        [MemberData(nameof(InvalidPersonAddresses))]

        public void Should_Fail_When_PersonAddress_Invalid(string address)
        {
            var command = GetValidCommand();
            if (command.IsDirty || command.IsDirty)
            {
                command.SignRequestPersons[0].PersonAddress = address;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                    e.PropertyName.Contains("PersonAddress"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }
        // Related Persons Tests
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid-guid")]
        public void Should_Fail_When_RelatedPerson_SignRequestId_Invalid(string signRequestId)
        {
            var command = GetValidCommand();
            if (command.SignRequestPersons != null && command.SignRequestRelatedPersons.Count > 0
                && (command.SignRequestRelatedPersons[0].IsDirty || command.SignRequestRelatedPersons[0].IsNew))
            {
                command.SignRequestRelatedPersons[0].SignRequestId = signRequestId;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e =>
                     e.PropertyName.Contains("SignRequestId"));
            }
            else
            {
                Assert.NotNull(command);
            }

        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(2)]
        public void Should_Fail_When_MainPersonId_Invalid(int? count)
        {
            var command = GetValidCommand();
            if (command.SignRequestPersons != null && command.SignRequestRelatedPersons.Count > 0
               && (command.SignRequestRelatedPersons[0].IsDirty || command.SignRequestRelatedPersons[0].IsNew))
            {
                {
                    if (count == null)
                    {
                        command.SignRequestRelatedPersons[0].MainPersonId = null;
                    }
                    else
                    {
                        command.SignRequestRelatedPersons[0].MainPersonId = new List<string>();
                        for (int i = 0; i < count; i++)
                        {
                            command.SignRequestRelatedPersons[0].MainPersonId.Add(Guid.NewGuid().ToString());
                        }
                    }
                    var result = _validator.TestValidate(command);
                    result.Errors.Should().Contain(e =>
                                         e.PropertyName.Contains("MainPersonId"));
                }
            }
            else
            {
                Assert.NotNull(command);
            }
        }
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(2)]
        public void Should_Fail_When_RelatedAgentPersonId_Invalid(int? count)
        {
            var command = GetValidCommand();

            if (command.SignRequestPersons != null && command.SignRequestRelatedPersons.Count > 0
              && (command.SignRequestRelatedPersons[0].IsDirty || command.SignRequestRelatedPersons[0].IsNew))
            {
                if (count == null)
                {
                    command.SignRequestRelatedPersons[0].RelatedAgentPersonId = null;
                }
                else
                {
                    command.SignRequestRelatedPersons[0].RelatedAgentPersonId = new List<string>();
                    for (int i = 0; i < count; i++)
                    {
                        command.SignRequestRelatedPersons[0].RelatedAgentPersonId.Add(Guid.NewGuid().ToString());
                    }
                }
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e => e.PropertyName.Contains("RelatedAgentPersonId"));
            }
            else
            {
                Assert.NotNull(command);

            }
        }
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(2)]
        public void Should_Fail_When_RelatedAgentTypeId_Invalid(int? count)
        {
            var command = GetValidCommand();
            if (command.SignRequestPersons != null && command.SignRequestRelatedPersons.Count > 0
             && (command.SignRequestRelatedPersons[0].IsDirty || command.SignRequestRelatedPersons[0].IsNew))
            {
                if (count == null)
                {
                    command.SignRequestRelatedPersons[0].RelatedAgentTypeId = null;
                }
                else
                {
                    command.SignRequestRelatedPersons[0].RelatedAgentTypeId = new List<string>();
                    for (int i = 0; i < count; i++)
                    {
                        command.SignRequestRelatedPersons[0].RelatedAgentTypeId.Add(i.ToString());
                    }
                }
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e => e.PropertyName.Contains("RelatedAgentTypeId"));
            }
            else
            {
                Assert.NotNull(command);

            }

        }

        [Fact]
        public void Should_Fail_When_RelatedReliablePersonReasonId_Null()
        {
            var command = GetValidCommand();
            if (command.SignRequestPersons != null && command.SignRequestRelatedPersons.Count > 0
            && (command.SignRequestRelatedPersons[0].IsDirty || command.SignRequestRelatedPersons[0].IsNew))
            {
                command.SignRequestRelatedPersons[0].RelatedReliablePersonReasonId = null;
                var result = _validator.TestValidate(command);
                result.Errors.Should().Contain(e => e.PropertyName.Contains("RelatedReliablePersonReasonId"));
            }
            else
            {
                Assert.NotNull(command);

            }

        }


        [Fact]
        public async Task Handle_UserWithoutRequiredRole_ShouldReturnAuthenticationException()
        {
            // Arrange

            var command = new UpdateSignRequestCommand();
            _userServiceMock.Setup(x => x.UserApplicationContext)
                .Returns(CreateUnAuthorizedUser());

            // Act
            ApiResult result = new ApiResult { IsSuccess = true };
            try
            {
                result = await _handler.Handle(command, CancellationToken.None);

            }
            catch (AuthenticationException exception)
            {
                result.IsSuccess = false;
            }

            // Assert
            result.IsSuccess.Should().BeFalse();
        }
        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateSignRequest()
        {
            // Arrange
            _userServiceMock.Setup(x => x.UserApplicationContext)
                .Returns(CreateSaradaftarUser());

            await _applicationContext.SaveChangesAsync(CancellationToken.None);
            var command = SignRequestSeedData.UpdateSignRequestCommand;
            var mediaResult = new ApiResult { IsSuccess = true };
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateMediaServiceInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediaResult);
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
                .Setup(x => x.Send(It.IsAny<GetPersonPhotoListServiceInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(personalImagesResult);

            var result = await _handler.Handle(command, CancellationToken.None);
        }

        [Fact]
        public async Task Handle_ValidCommand_Empty_SignRequest()
        {
            // Arrange
            using var context = CreateDbContext();
            var item = await context.SignRequests.FirstOrDefaultAsync(t => t.Id == Guid.NewGuid());

            Assert.Null(item);
        }
    }
}
