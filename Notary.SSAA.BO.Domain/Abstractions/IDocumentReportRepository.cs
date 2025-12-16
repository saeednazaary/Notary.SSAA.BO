using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.SharedModels;
using Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentReportRepository : IDRepository
    {
        Task <DocumentReportViewModel> GetDocumentReport(string id, CancellationToken cancellationToken);
    }
}
