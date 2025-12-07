using Oracle.ManagedDataAccess.Client;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using System.Data;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories.DapperRepositories.Base
{
    public class DapperRepository : IDapperRepository
    {
        private readonly string _connectionString = "";

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection DBConnection()
        {
            return new OracleConnection(_connectionString);
        }

    }
}
