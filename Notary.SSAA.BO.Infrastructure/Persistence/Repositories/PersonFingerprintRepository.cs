using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO    .Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using   Notary.SSAA.BO.Utilities.Extensions;
using static Notary.SSAA.BO.Infrastructure.Contexts.GuidConverter;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class PersonFingerprintRepository : Repository<PersonFingerprint>, IPersonFingerprintRepository
    {
        public PersonFingerprintRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<GetInquiryPersonFingerprintRepositoryObject>> GetInquiryPersonFingerprint(List<string> nationalNos, string mainObjectId, CancellationToken cancellationToken)
        {
            var tempRes = await TableNoTracking.Include(x => x.PersonFingerType).Where(x => nationalNos.Contains(x.PersonNationalNo) && x.UseCaseMainObjectId == mainObjectId)
                .ToListAsync(cancellationToken);
            return tempRes.Select(x => new GetInquiryPersonFingerprintRepositoryObject
            {
                FingerprintId = x.Id.ToString(),
                PersonObjectId = x.UseCasePersonObjectId,
                MainObjectId = x.UseCaseMainObjectId,
                PersonNationalNo = x.PersonNationalNo,
                PersonFingerTypeId = x.PersonFingerTypeId,
                TFAState = x.TfaState,
                IsFingerprintGotten = (x.FingerprintImageFile != null && x.IsDeleted == "2" && (x.State == "1" || string.IsNullOrWhiteSpace(x.State))),
                MOCState = x.MocState,
                PersonFingerTypeTitle = x.PersonFingerType?.Title,
                IsDeleted = x.IsDeleted.ToBoolean(),
                FingerprintDateTime = x.FingerprintGetDate + "-" + x.FingerprintGetTime

            }).ToList();

        }

        public async Task<GetInquiryPersonFingerprintRepositoryObject> GetInquiryPersonFingerprint(string nationalNo, string mainObjectId, CancellationToken cancellationToken)
        {
            var tempRes = await TableNoTracking.Include(x => x.PersonFingerType).Where(x => x.UseCaseMainObjectId == mainObjectId && x.PersonNationalNo == nationalNo).ToListAsync(cancellationToken);
            return tempRes.OrderBy(x => x.FingerprintGetDate)
                .OrderBy(x => x.FingerprintGetTime)
                 .Select(x => new GetInquiryPersonFingerprintRepositoryObject
                 {
                     FingerprintId = x.Id.ToString(),
                     PersonObjectId = x.UseCasePersonObjectId,
                     MainObjectId = x.UseCaseMainObjectId,
                     PersonNationalNo = x.PersonNationalNo,
                     PersonFingerTypeId = x.PersonFingerTypeId,
                     TFAState = x.TfaState,
                     IsFingerprintGotten = (x.FingerprintImageFile != null && x.IsDeleted == "2" && (x.State == "1" || string.IsNullOrWhiteSpace(x.State))),
                     PersonFingerTypeTitle = x.PersonFingerType?.Title,
                     MOCState = x.MocState,
                     IsDeleted = x.IsDeleted.ToBoolean(),
                     FingerprintDateTime = x.FingerprintGetDate + "-" + x.FingerprintGetTime,
                     SabtAhvalFingerCode = x.PersonFingerType != null ? x.PersonFingerType.SabtahvalCode : ""

                 }).FirstOrDefault();

        }

        public async Task<GetInquiryPersonFingerprintRepositoryObject> GetInquiryPersonFingerprint(string fingerprintId, CancellationToken cancellationToken)
        {
            var tempRes = await TableNoTracking.Include(x => x.PersonFingerType).Where(x => x.Id == fingerprintId.ToGuid()).ToListAsync(cancellationToken);
            return tempRes.OrderBy(x => x.FingerprintGetDate)
                 .OrderBy(x => x.FingerprintGetTime)
                  .Select(x => new GetInquiryPersonFingerprintRepositoryObject
                  {
                      FingerprintId = x.Id.ToString(),
                      PersonObjectId = x.UseCasePersonObjectId,
                      MainObjectId = x.UseCaseMainObjectId,
                      PersonNationalNo = x.PersonNationalNo,
                      PersonFingerTypeId = x.PersonFingerTypeId,
                      PersonFingerTypeTitle = x.PersonFingerType?.Title,
                      TFAState = x.TfaState,
                      IsFingerprintGotten = (x.FingerprintImageFile != null && x.IsDeleted == "2" && (x.State == "1" || string.IsNullOrWhiteSpace(x.State))),
                      MOCState = x.MocState,
                      IsDeleted = x.IsDeleted.ToBoolean(),
                      FingerprintDateTime = x.FingerprintGetDate + "-" + x.FingerprintGetTime,
                      SabtAhvalFingerCode = x.PersonFingerType != null ? x.PersonFingerType.SabtahvalCode : ""

                  }).FirstOrDefault();
        }

        public async Task<PersonFingerprint> GetLastFingerprint(List<string> mainObjectIds, string nationalNo,string sabteAhvalCodeFinger,List<string> notInscriptoriumIds, CancellationToken cancellationToken)
        {

            return await TableNoTracking.Include(x=>x.PersonFingerType).Where(x =>mainObjectIds.Contains(x.UseCaseMainObjectId) && x.PersonNationalNo == nationalNo && x.PersonFingerType.SabtahvalCode==sabteAhvalCodeFinger &&
            !notInscriptoriumIds.Contains(x.OrganizationId) && x.FingerprintGetDate.CompareTo("1394/12/01")>0 && x.State=="1" && x.IsDeleted=="2"&&x.FingerprintRawImage!=null )
                .OrderByDescending(x => x.RecordDate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<GetPersonFingerprintImageRepositoryObject>> GetPersonFingerprintImage(List<string> nationalNos, string mainObjectId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => nationalNos.Contains(x.PersonNationalNo) && x.UseCaseMainObjectId == mainObjectId && x.IsDeleted == "2" && (x.State == "1" || string.IsNullOrWhiteSpace(x.State)))
                 .Select(x => new GetPersonFingerprintImageRepositoryObject
                 {
                     PersonObjectId = x.UseCasePersonObjectId,
                     PersonNationalNo = x.PersonNationalNo,
                     MainObjectId = x.UseCaseMainObjectId,
                     FingerPrintImage = x.FingerprintImageFile
                 }).ToListAsync(cancellationToken);
        }

        public async Task<List<GetPersonFingerprintImageRepositoryObject>> GetPersonFingerprintImage(string mainObjectId, CancellationToken cancellationToken)
        {
            return (await TableNoTracking.Where(x => x.UseCaseMainObjectId == mainObjectId && x.IsDeleted == "2" && (x.State == "1" || string.IsNullOrWhiteSpace(x.State)))
                .OrderBy(x => x.FingerprintGetDate)
                .OrderBy(x => x.FingerprintGetTime)
                 .Select(x => new GetPersonFingerprintImageRepositoryObject
                 {
                     FingerprintId = x.Id.ToString(),
                     PersonFingerTypeId = x.PersonFingerTypeId,
                     PersonObjectId = x.UseCasePersonObjectId,
                     PersonNationalNo = x.PersonNationalNo,
                     MainObjectId = x.UseCaseMainObjectId,
                     FingerPrintImage = x.FingerprintImageFile

                 }).ToListAsync(cancellationToken))
                 .DistinctBy(x => x.PersonObjectId).ToList();
        }

        public async Task<List<SignablePersonRepositoryObject>> GetPersonFingerprint(string mainObjectId, CancellationToken cancellationToken)
        {
            return (await TableNoTracking.Where(x => x.UseCaseMainObjectId == mainObjectId && (x.State == "1" || string.IsNullOrWhiteSpace(x.State)))
               .OrderBy(x => x.FingerprintGetDate)
               .OrderBy(x => x.FingerprintGetTime)
                .Select(x => new SignablePersonRepositoryObject
                {
                    FinterPrintImage = x.FingerprintImageFile != null ? x.FingerprintImageFile : null,
                    NationalNo = x.PersonNationalNo,

                }).ToListAsync(cancellationToken))
                .DistinctBy(x => x.NationalNo).ToList();
        }
        public async Task<string> GetLastFingerprintDateTime(string mainObjectId,
            CancellationToken cancellationToken)
        {

            return await TableNoTracking.Where(t => t.UseCaseMainObjectId == mainObjectId && (t.State == "1" || string.IsNullOrWhiteSpace(t.State))
                                                      && t.FingerprintImageFile != null).MaxAsync(t => t.FingerprintGetDate + "-" + t.FingerprintGetTime, cancellationToken);
        }
    }
}
