using Dapper;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.DapperRepositories.Base;
using System.Data;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories.DapperRepositories
{
    public class DateTimeRepository : DRepository, IDateTimeRepository
    {
        private IDbConnection _connection;
        public DateTimeRepository(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<string> GetCurrentDateTime(string query)
        {
            this.EnsureConnectionOpen();
            return (await _connection.QueryAsync<string>(query)).FirstOrDefault();
        }
    }


}
