using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;
using Notary.SSAA.BO.SharedKernel.Enumerations;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DocumentInquiryRepository : Repository<DocumentInquiry>, IDocumentInquiryRepository
    {
        public DocumentInquiryRepository ( ApplicationContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<DocumentInquiryInformation>> GetDocumentInquiriesInformation ( Guid documentId )
        {
            List<DocumentInquiryInformation> documentInquiriesInformation = new();

            documentInquiriesInformation = await TableNoTracking.Where(t => t.DocumentId == documentId)
                .Select(t => new DocumentInquiryInformation
                    { EstateInquiryId = t.EstateInquiriesId, InquiryOrganizationId = t.DocumentInquiryOrganizationId })
                .ToListAsync();

            return documentInquiriesInformation;


        }
        public async Task<List<DocumentInquiry>> CollectSignedDSUDealSummaries ( Guid documentId, string scriptoriumID, List<string> estestateInquiryIDsCollection, bool IsUnRegisteredEstateInquiryForced, string DSUInitializationDate )
        {
            List<string> documrntCodes = new List<string>() { "111", "115" };


            if ( !IsUnRegisteredEstateInquiryForced )
            {
                return await TableNoTracking.Include ( t => t.Document )
                    .Where (
                        t => t.ScriptoriumId == scriptoriumID

                             && t.EstateInquiriesId != null
                             && estestateInquiryIDsCollection.Contains ( t.EstateInquiriesId )
                             && t.DocumentInquiryOrganizationId == "1"
                             && documrntCodes.Contains ( t.Document.DocumentTypeId )
                             && t.Document.State == "6"
                             && t.Document.IsRegistered == YesNo.Yes.GetString ()
                             && t.Document.Id != documentId
                             && string.Compare ( t.Document.RequestDate, DSUInitializationDate ) >= 0 ).ToListAsync ();
            }
            else
            {
                return await TableNoTracking.Include ( t => t.Document )
                    .Where (
                        t => t.ScriptoriumId == scriptoriumID

                             && t.EstateInquiriesId != null
                             && estestateInquiryIDsCollection.Contains ( t.EstateInquiriesId )
                             && t.DocumentInquiryOrganizationId == "1"
                             && documrntCodes.Contains ( t.Document.DocumentTypeId )
                             && t.Document.State == "6"
                             && t.Document.Id != documentId
                             && string.Compare ( t.Document.RequestDate, DSUInitializationDate ) >= 0 ).ToListAsync ();


            }


        }


        public async Task<List<DocumentInquiry>> GetRecentDeterministicRegisterServiceReqs(string scriptoriumId,
            string estateInquiryId, CancellationToken cancellationToken)
        {
            List<string> codes = new List<string>() { "111", "115" };
            List<string> states = new List<string>() { "5", "6", "7" };
            return await TableNoTracking.Include(t => t.Document)
                .Where(t => t.ScriptoriumId == t.Document.ScriptoriumId
                            && t.EstateInquiriesId == estateInquiryId
                            && t.DocumentInquiryOrganizationId == "1" && codes.Contains(t.Document.DocumentTypeId) &&
                            states.Contains(t.Document.State)).ToListAsync(cancellationToken);


        }

    }
    }
