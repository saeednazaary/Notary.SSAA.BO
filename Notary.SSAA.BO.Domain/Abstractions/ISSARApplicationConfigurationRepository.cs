using   Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISSARApplicationConfigurationRepository : IDRepository
    {
        Task<string> GetDayOfWeek (CancellationToken cancellationToken );
    }
}

