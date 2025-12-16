using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Application.UnitTests.Fixtures;
using Notary.SSAA.BO.Application.UnitTests.Seeds.SignRequestSeedData;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Application.UnitTests.Repositories.SignRequest
{
    public class SignRequestRepositoryTests: TestBase
    {
        private readonly ApplicationContext _applicationContext;
        private readonly SignRequestRepository _repository;

        public SignRequestRepositoryTests ( )
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _applicationContext = CreateDbContext ();
            _repository = new SignRequestRepository ( _applicationContext );
            
            _applicationContext.AddRange ( SignRequestSeedData.GetSignRequestSubjectGroups () );
            _applicationContext.AddRange ( SignRequestSeedData.GetSignRequestSubjects () );
            _applicationContext.AddRange ( SignRequestSeedData.GetSignRequestGetters () );
            _applicationContext.AddRange ( SignRequestSeedData.GetSsrSignEbookBaseinfoes () );
            _applicationContext.AddRange ( SignRequestSeedData.GetSignRequests () );
            _applicationContext.AddRange ( SignRequestSeedData.GetAgentTypes () );

            _applicationContext.SsrConfigMainSubjects.AddRange(SignRequestSeedData.GetMainSubjects());

            _applicationContext.SsrConfigSubjects.AddRange(SignRequestSeedData.GetSubjects());

            _applicationContext.SsrConfigs.AddRange(SignRequestSeedData.GetConfigs());

            _applicationContext.SsrConfigConditionScrptrms.AddRange(SignRequestSeedData.GetConfigConditionScrptrm());

            _applicationContext.SsrConfigConditionTimes.AddRange(SignRequestSeedData.GetConfigConditionTime());
            _applicationContext.SaveChanges ();

        }
        [Fact]
        public async Task GetMaxReqNo_ShouldReturnMaxReqNo_WhenMatchingRecordsExist ( )
        {

            // Arrange


            // Act
            var result = await _repository.GetMaxReqNo("14044445799", CancellationToken.None);

            // Assert
            result.Should ().Be ( "140444457999000005" );
        }
        [Fact]
        public async Task GetMaxNationalNo_ShouldReturnMaxNationalNo_WhenMatchingRecordsExist ( )
        {
          

            // Act
            var result = await _repository.GetMaxNationalNo("14040215799", CancellationToken.None);

            // Assert
            result.Should ().Be ( "140402157999000005" );
        }


        [Fact]
        public async Task GetSignRequest_ShouldReturnSignRequestWithRelatedData_WhenExists ( )
        {

            var signRequestId=Guid.Parse( "62f720b6-c03b-4dfd-908e-cff35e8c6bed");
            // Act
            var result = await _repository.GetSignRequest(signRequestId,"57999", CancellationToken.None);

            // Assert
            result.Should ().NotBeNull ();
            result.Id.Should ().Be ( signRequestId );
            result.SignRequestSubject.Should ().NotBeNull ();
            result.SignRequestPersonRelateds.Should ().HaveCount ( 1 );
            result.SignRequestPeople.Should ().HaveCount ( 3);
        }
        [Fact]
        public async Task GetSignRequestPersons_ShouldReturnSignRequestWithPeople_WhenExists ( )
        {
            // Arrange
            var signRequestId=Guid.Parse( "62f720b6-c03b-4dfd-908e-cff35e8c6bed");


            // Act
            var result = await _repository.GetSignRequestPersons(signRequestId, CancellationToken.None);

            // Assert
            result.Should ().NotBeNull ();
            result.Id.Should ().Be ( signRequestId );
            result.SignRequestPeople.Should ().HaveCount ( 3);
        }
        [Fact]
        public async Task SignRequestTracking_ById_ShouldReturnFullSignRequestData_WhenExists ( )
        {
            // Arrange
              var signRequestId = Guid.Parse ( "62f720b6-c03b-4dfd-908e-cff35e8c6bed" );



            // Act
            var result = await _repository.SignRequestTracking(signRequestId,"57999", CancellationToken.None);

            // Assert
            result.Should ().NotBeNull ();
            result.Id.Should ().Be ( signRequestId );
            result.SignRequestGetter.Should ().NotBeNull ();
            result.SignRequestSubject.Should ().NotBeNull ();
            result.SignRequestPeople.Should ().HaveCount ( 3);
            result.SignRequestFile.Should ().BeNull (  );
        }

        [Fact]
        public async Task SignRequestTracking_ByRequestNo_ShouldReturnFullSignRequestData_WhenExists ( )
        {
            // Arrange
            var requestNo = "140444457999000001";
           

            // Act
            var result = await _repository.SignRequestTracking(requestNo, CancellationToken.None);

            // Assert
            result.Should ().NotBeNull ();
            result.ReqNo.Should ().Be ( requestNo );
        }
        [Fact]
        public async Task GetSignRequestElectronicBookTotalCount_ShouldReturnCorrectCount_ForSpecificScriptorium ( )
        {
            // Arrange
            var scriptoriumId = "57999";
           

            // Act
            var result = await _repository.GetSignRequestElectronicBookTotalCount(scriptoriumId, CancellationToken.None);

            // Assert
            result.Should ().Be ( 5 );
        }

        [Theory]
        [InlineData ( 1, 10 )]
        [InlineData ( 2, 10 )]
        public async Task GetSignRequestGridItems_ShouldReturnPaginatedResults ( int pageIndex, int pageSize )
        {
            // Arrange
            var scriptoriumId = "57999";

           

            var gridSearchInput = new List<SearchData>();
            var selectedItemsIds = new List<string>();
            var fieldsNotInFilterSearch = new List<string>();
            var sortInput = new SortData { Sort = "ReqNo", SortType = "asc" };

            // Act
            var result = await _repository.GetSignRequestGridItems(
                pageIndex, pageSize, gridSearchInput, "", sortInput,
                selectedItemsIds, fieldsNotInFilterSearch, scriptoriumId, null,
                CancellationToken.None, true);

            // Assert
            result.Should ().NotBeNull ();
            //result.GridItems.Should ().HaveCount ( Math.Min ( pageSize, 15 - ( pageIndex - 1 ) * pageSize ) );
            result.TotalCount.Should ().Be (5);
        }


        [Fact]
        public async Task GetSignRequestBookPage_ShouldReturnCorrectPageNumber_WhenSearchingByNationalNo ( )
        {
            // Arrange
            var scriptoriumId = "57999";
            var targetNationalNo = "140402157999000001";
        

            // Act
            var (signRequest, pageNumber) = await _repository.GetSignRequestBookPage (
                scriptoriumId, 1, targetNationalNo, 72876, CancellationToken.None );

            // Assert
            signRequest.Should ().NotBeNull ();
            signRequest.NationalNo.Should ().Be ( targetNationalNo );
            pageNumber.Should ().Be ( 5 ); // Should be the second item when ordered by NationalNo descending
        }
    }
}
