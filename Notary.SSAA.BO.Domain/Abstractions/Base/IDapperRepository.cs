using System.Data;

namespace Notary.SSAA.BO.Domain.Abstractions.Base
{

    public interface IDapperRepository
    {
        public IDbConnection DBConnection();


    }

}
