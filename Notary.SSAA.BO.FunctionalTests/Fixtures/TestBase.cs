using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.FunctionalTests.Fixtures
{
    public class TestBase : IDisposable
    {
        protected readonly DbContextOptions<SsarContext> _dbContextOptions;

        public TestBase ( )
        {
            _dbContextOptions = new DbContextOptionsBuilder<SsarContext> ()
                .UseInMemoryDatabase ( databaseName: Guid.NewGuid ().ToString () )
                .Options;
        }

        protected ApplicationContext CreateDbContext ( )
        {
            return new ApplicationContext ( _dbContextOptions );
        }


        protected UserApplicationContext CreateUnAuthorizedUser()
        {
            ApplicationUserRole userRole = new ApplicationUserRole("UnAuthorized","UnAuthorized");
            UserApplicationContext userApplicationContext =
                new UserApplicationContext(null, userRole, null, null, null);
            return userApplicationContext;
        }
        protected UserApplicationContext CreateSaradaftarUser ( )
        {
            ApplicationUserRole userRole = new ApplicationUserRole(RoleConstants.Sardaftar,"sardaftar");
            ApplicationUser user = new ApplicationUser("1", "testUserName", "testName", "testFamily");
            ApplicationBranchAccess branchAccess =
                new ApplicationBranchAccess("57999", "57999", "57999", "testScriptorium", "testDisplayName","true","true");
            ScriptoriumInformation scriptoriumInformation = new ScriptoriumInformation();
            scriptoriumInformation.Code = "57999";
            scriptoriumInformation.Id = "57999";
            scriptoriumInformation.GeoLocationId = "1";
            scriptoriumInformation.ScriptoriumNo = "57999";
            scriptoriumInformation.Unit = new();
            scriptoriumInformation.Unit.Code = "57999";
            scriptoriumInformation.Unit.GeoLocationId = "1";
            UserApplicationContext userApplicationContext =
                new UserApplicationContext(user, userRole, branchAccess, null, scriptoriumInformation);
            return userApplicationContext;
        }

        
        public void Dispose ( )
        {
            // Clean up if needed
        }
    }
}
