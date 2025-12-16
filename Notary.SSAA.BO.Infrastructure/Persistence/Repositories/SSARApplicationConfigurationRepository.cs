using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.DapperRepositories.Base;
using Notary.SSAA.BO.SharedKernel.SharedModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class SSARApplicationConfigurationRepository : DRepository, ISSARApplicationConfigurationRepository
    {
        private readonly IDbConnection _connection;
        public SSARApplicationConfigurationRepository ( IDbConnection connection ) : base ( connection )
        {
            _connection = connection;
        }

        public async Task<string> GetDayOfWeek ( CancellationToken cancellationToken )
        {

            this.EnsureConnectionOpen ();
            var mainQuery = string.Format(@"select to_char(sysdate,'d') DayOfWeek from dual");
            var queryResult = await _connection.QuerySingleAsync<string>(mainQuery);
            return queryResult;
        }

    }
}
