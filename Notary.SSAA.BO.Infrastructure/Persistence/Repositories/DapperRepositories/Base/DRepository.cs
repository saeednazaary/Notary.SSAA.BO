using Oracle.ManagedDataAccess.Client;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using System.Data;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories.DapperRepositories.Base
{
    public class DRepository : IDRepository
    {
        private readonly IDbConnection _connection;

        public DRepository ( IDbConnection connection )
        {
            _connection = connection;
        }
        public void EnsureConnectionOpen ( )
        {
            if ( _connection.State != ConnectionState.Open )
            {
                _connection.Open ();
            }
        }
    }
    }
