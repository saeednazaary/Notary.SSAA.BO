using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.Utilities.Security;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons
{
    public class SignProvider
    {
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly IDocumentFileRepository _documentFileRepository;
        public SignProvider(IPersonFingerprintRepository personFingerprintRepository, IDocumentFileRepository documentFileRepository)
        {
            _personFingerprintRepository = personFingerprintRepository;
            _documentFileRepository = documentFileRepository;

        }

        public List<string> ProvideENoteBookHashedData(List<DocumentElectronicBook> theCurrentDigitalBookCollection)
        {
            List<string> hashedDigitalBooks = new List<string>();

            foreach (DocumentElectronicBook theCurrentDigitalBook in theCurrentDigitalBookCollection)
            {
                List<string> hashedCollection = new List<string>();

                if (theCurrentDigitalBook.ClassifyNo != 0)
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.ClassifyNo.ToString()));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.Description))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.Description));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.DocumentDate))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.DocumentDate));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.EnterBookDateTime))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.EnterBookDateTime));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.ExordiumConfirmDateTime))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.ExordiumConfirmDateTime));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.NationalNo))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.NationalNo));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.DocumentTypeId))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.DocumentTypeId));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.ScriptoriumId))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.ScriptoriumId));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.SignDate))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.SignDate));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.HashOfFingerprints))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.HashOfFingerprints));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.HashOfPdf))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.HashOfPdf));

                byte[] overalByteArray = hashedCollection.SelectMany(s => System.Text.Encoding.UTF8.GetBytes(s)).ToArray();
                string finalHashedData = MD5Core.GetHashString(overalByteArray);

                hashedDigitalBooks.Add(finalHashedData);
            }

            return hashedDigitalBooks;
        }
        public List<string> ProvideENoteBookHashedDataWithReservedClassifyNo(List<DocumentElectronicBook> theCurrentDigitalBookCollection)
        {
            List<string> hashedDigitalBooks = new List<string>();

            foreach (DocumentElectronicBook theCurrentDigitalBook in theCurrentDigitalBookCollection)
            {
                List<string> hashedCollection = new List<string>();

                if (theCurrentDigitalBook.ClassifyNoReserved != 0)
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.ClassifyNoReserved.ToString()));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.Description))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.Description));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.DocumentDate))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.DocumentDate));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.EnterBookDateTime))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.EnterBookDateTime));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.ExordiumConfirmDateTime))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.ExordiumConfirmDateTime));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.NationalNo))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.NationalNo));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.DocumentTypeId))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.DocumentTypeId));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.ScriptoriumId))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.ScriptoriumId));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.SignDate))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.SignDate));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.HashOfFingerprints))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.HashOfFingerprints));
                if (!string.IsNullOrWhiteSpace(theCurrentDigitalBook.HashOfPdf))
                    hashedCollection.Add(MD5Core.GetHashString(theCurrentDigitalBook.HashOfPdf));

                byte[] overalByteArray = hashedCollection.SelectMany(s => System.Text.Encoding.UTF8.GetBytes(s)).ToArray();
                string finalHashedData = MD5Core.GetHashString(overalByteArray);

                hashedDigitalBooks.Add(finalHashedData);
            }

            return hashedDigitalBooks;
        }

        public async Task<string> ProvideFingerPrintsHash(string onotaryRegisterServiceReqObjectID, CancellationToken cancellationToken)
        {
            List<PersonFingerprintHash> fingerPrintsCollection = await _personFingerprintRepository.TableNoTracking
                .Where(x => x.UseCaseMainObjectId == onotaryRegisterServiceReqObjectID && x.IsDeleted == "2")
                .OrderBy(x => x.FingerprintGetDate)
                .ThenBy(x => x.FingerprintGetTime)
                .Select(x => new PersonFingerprintHash
                {

                    PersonObjectId = x.UseCasePersonObjectId,
                    Description = x.Description,
                    FingerPrintImage = x.FingerprintImageFile

                }).ToListAsync(cancellationToken);




            string hashedData = this.ProvideFingerPrintsHash(fingerPrintsCollection);
            return hashedData;
        }
        public async Task<string> ProvideDocumentImageHash(string onotaryRegisterServiceReqObjectID, CancellationToken cancellationToken)
        {
            var docImage = await _documentFileRepository.TableNoTracking
                .Where(t => t.DocumentId == Guid.Parse(onotaryRegisterServiceReqObjectID))
                .Select(t => new { DocImage2 = t.LastFile }).FirstOrDefaultAsync(cancellationToken);

            if (docImage == null || docImage.DocImage2 == null)
                return null;

            return MD5Core.GetHashString(docImage.DocImage2);

        }
        private string ProvideFingerPrintsHash(IList<PersonFingerprintHash> fingerPrintsCollection)
        {
            if (fingerPrintsCollection.Count == 0)
                return null;

            List<string> hashedCollection = new List<string>();

            for (int i = 0; i < fingerPrintsCollection.Count; i++)
            {
                string hashedData = "";

                if (fingerPrintsCollection.ElementAt(i).FingerPrintImage != null)
                    hashedData = MD5Core.GetHashString(((byte[])fingerPrintsCollection.ElementAt(i).FingerPrintImage));

                if (fingerPrintsCollection.ElementAt(i).Description != null)
                    hashedData += MD5Core.GetHashString(((string)fingerPrintsCollection.ElementAt(i).Description));

                if (!string.IsNullOrWhiteSpace(hashedData))
                    hashedCollection.Add(hashedData);
            }


            if (!hashedCollection.Any())
                return null;

            byte[] overalByteArray = hashedCollection.SelectMany(s => System.Text.Encoding.UTF8.GetBytes(s)).ToArray();
            string finalHashedData = MD5Core.GetHashString(overalByteArray);

            return finalHashedData;
        }
        private string ProvideDocumentImageHash(byte[] documentImage)
        {
            return MD5Core.GetHashString(documentImage);
        }
    }

}
