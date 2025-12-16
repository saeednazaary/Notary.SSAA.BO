using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentEstateQuotaDetailRepository:IRepository<DocumentEstateQuotaDetail>
    {
        Task<DocumentEstateQuotaDetail> GetDocumentEstateQuotaDetailForEstateDocumentRequest(Guid id, bool personIsSeller, bool isAttchmentTransfer, CancellationToken cancellationToken);
        Task<List<DocumentEstateQuotaDetail>> CollectValidPersonQuotas ( Guid documentId, string EstateInquiry, CancellationToken cancellationToken );

    }
}
