using Notary.SSAA.BO.Domain.Abstractions.Base;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDateTimeRepository: IDRepository
    {
        Task<string> GetCurrentDateTime(string query);

    }

  }
