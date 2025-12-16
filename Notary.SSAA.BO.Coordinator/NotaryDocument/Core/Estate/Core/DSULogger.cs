//using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Text;


namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class DSULogger
    {
        private readonly IRepository<DocumentEstateDealSummaryGenerated> _documentEstateDealSummaryGeneratedRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IUserService _userService;

        public DSULogger(IRepository<DocumentEstateDealSummaryGenerated> documentEstateDealSummaryGeneratedRepository,
            IDateTimeService dateTimeService, IUserService userService)
        {
            _documentEstateDealSummaryGeneratedRepository = documentEstateDealSummaryGeneratedRepository;
            _dateTimeService = dateTimeService;
            _userService = userService;
        }





        public async Task<DocumentEstateDealSummaryGenerated> LogGeneratedDSUCollection(List<DSUDealSummaryObject> generatedDSUsCollection, string registerServiceReqID
            , CancellationToken cancellationToken
            , string dsuMessages = null)
        {
            Boolean isNewLog = false;
            DocumentEstateDealSummaryGenerated? dsuLog = null;

            if (!generatedDSUsCollection.Any() || string.IsNullOrWhiteSpace(registerServiceReqID))
                return null;


            dsuLog = await _documentEstateDealSummaryGeneratedRepository.TableNoTracking.FirstOrDefaultAsync(d => d.DocumentId == Guid.Parse(registerServiceReqID), cancellationToken);
            if (dsuLog == null)
            {
                dsuLog = new DocumentEstateDealSummaryGenerated();
                dsuLog.Id = Guid.NewGuid();
                dsuLog.CreateTime = _dateTimeService.CurrentTime;
                dsuLog.ScriptoriumId = _userService.UserApplicationContext.ScriptoriumInformation.Id;
                dsuLog.RecordDate = _dateTimeService.CurrentDateTime;

                dsuLog.Ilm = "1";
                isNewLog = true;
            }


            string? tempError = (!string.IsNullOrWhiteSpace(dsuMessages)) ? dsuMessages.ToString() : null;
            if (!string.IsNullOrWhiteSpace(tempError))
            {
                tempError = tempError.GetStandardFarsiString();
            }

            ExportManager exportController = new ExportManager();

            string exportedXML = exportController.GenerateXML(generatedDSUsCollection);
            string modifiedXML = exportedXML.GetStandardFarsiString(false);

            dsuLog.Xml = modifiedXML;

            dsuLog.GeneratedDealSummaryCount = short.Parse(generatedDSUsCollection.Count.ToString());
            dsuLog.DocumentId = Guid.Parse(registerServiceReqID);
            dsuLog.CreateDate = _dateTimeService.CurrentPersianDate;
            dsuLog.CreateTime = _dateTimeService.CurrentTime;
            dsuLog.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchId;

            dsuLog.IsSent = NotaryGeneratedDealSummary.NotSent.GetString();

            if (!string.IsNullOrWhiteSpace(dsuMessages))
                dsuLog.LastErrorDescription = dsuMessages;

            if (dsuLog.SendAttemptCount == null || dsuLog.SendAttemptCount == 0)
                dsuLog.SendAttemptCount = 0;
            if (isNewLog)
                await _documentEstateDealSummaryGeneratedRepository.AddAsync(dsuLog, cancellationToken, false);
            else
                await _documentEstateDealSummaryGeneratedRepository.UpdateAsync(dsuLog, cancellationToken, false);



            return dsuLog;


        }

        public async Task LogVerificationMessages(string registerServiceReqID, string dsuMessages, CancellationToken cancellationToken)
        {
            bool isNewLog = false;
            DocumentEstateDealSummaryGenerated? dsuLog = null;
            if (string.IsNullOrWhiteSpace(registerServiceReqID) || string.IsNullOrWhiteSpace(dsuMessages))
                return;



            dsuLog = await _documentEstateDealSummaryGeneratedRepository.GetAsync(t => t.DocumentId == Guid.Parse(registerServiceReqID), cancellationToken);


            if (dsuLog == null)
            {
                isNewLog = true;
                dsuLog = new DocumentEstateDealSummaryGenerated();
                dsuLog.Id = Guid.NewGuid();
                dsuLog.Ilm = "1";
                dsuLog.CreateDate = _dateTimeService.CurrentPersianDate;
                dsuLog.CreateTime = _dateTimeService.CurrentTime;
                dsuLog.ScriptoriumId = _userService.UserApplicationContext.ScriptoriumInformation.Id;
                dsuLog.RecordDate = _dateTimeService.CurrentDateTime;


                dsuLog.DocumentId = Guid.Parse(registerServiceReqID);
            }

            if (string.IsNullOrWhiteSpace(dsuLog.Xml))
                dsuLog.Xml = "Verification Is In Progress . . .";

            if (!string.IsNullOrWhiteSpace(dsuLog.LastErrorDescription))
                dsuLog.LastErrorDescription += System.Environment.NewLine + "==================================" + System.Environment.NewLine;

            dsuLog.LastErrorDescription += dsuMessages;

            dsuLog.CreateDate = _dateTimeService.CurrentPersianDate;
            dsuLog.CreateTime = _dateTimeService.CurrentTime;
            dsuLog.ScriptoriumId = _userService.UserApplicationContext.ScriptoriumInformation.Id;

            dsuLog.IsSent = NotaryGeneratedDealSummary.NotSent.GetString();
            if (isNewLog)
                await _documentEstateDealSummaryGeneratedRepository.AddAsync(dsuLog, cancellationToken, false);
            else
                await _documentEstateDealSummaryGeneratedRepository.UpdateAsync(dsuLog, cancellationToken, false);




        }

        public async Task<List<DSUDealSummaryObject>> ExportGeneratedDSULog(string registerServiceReqID, CancellationToken cancellationToken)
        {

            List<DSUDealSummaryObject> result = new List<DSUDealSummaryObject>();
            DocumentEstateDealSummaryGenerated oldDSULog = await _documentEstateDealSummaryGeneratedRepository.GetAsync
                (t => t.DocumentId == Guid.Parse(registerServiceReqID), cancellationToken);


            if (oldDSULog != null)
            {
                if (oldDSULog.Xml == null)
                    return null;

                ExportManager exportManager = new ExportManager();
                string originalDBXML = oldDSULog.Xml;
                string modifiedXML = originalDBXML.GetStandardFarsiString(false);
                result = exportManager.ExportDSUObjectFromXML(modifiedXML);
                //return dsuObject;
            }

            return result;
        }

        public async Task UpdateDSULogStatus(string registerServiceReqID, NotaryGeneratedDealSummary status, string sendingMessage, CancellationToken cancellationToken, List<DSUDealSummaryObject> signedDSUDealSummariesCollection = null)
        {
            Boolean isNewLog = false;
            DocumentEstateDealSummaryGenerated dsuLog = await _documentEstateDealSummaryGeneratedRepository.GetAsync
                (t => t.DocumentId == Guid.Parse(registerServiceReqID), cancellationToken);

            if (dsuLog == null)
            {
                isNewLog = true;
                if (!signedDSUDealSummariesCollection.Any())
                    return;
                dsuLog = new DocumentEstateDealSummaryGenerated();
                ExportManager exportController = new ExportManager();
                string exportedXML = exportController.GenerateXML(signedDSUDealSummariesCollection);

                dsuLog.GeneratedDealSummaryCount = short.Parse(signedDSUDealSummariesCollection.Count.ToString());
                dsuLog.DocumentId = Guid.Parse(registerServiceReqID);
                dsuLog.Xml = exportedXML;

                dsuLog.IsSent = status.GetString();
                dsuLog.SendAttemptCount = 0;
                dsuLog.Ilm = "1";
                dsuLog.CreateDate = _dateTimeService.CurrentPersianDate;
                dsuLog.CreateTime = _dateTimeService.CurrentTime;
                dsuLog.ScriptoriumId = _userService.UserApplicationContext.ScriptoriumInformation.Id;
                dsuLog.RecordDate = _dateTimeService.CurrentDateTime;

            }

            dsuLog.IsSent = status.GetString();

            if (status == NotaryGeneratedDealSummary.Sent)
                dsuLog.LastErrorDescription = null;
            else
                dsuLog.LastErrorDescription = sendingMessage;

            short? sendAttemptCount = dsuLog.SendAttemptCount;
            dsuLog.SendAttemptCount = ++sendAttemptCount;

            dsuLog.SendDate = _dateTimeService.CurrentPersianDate;
            dsuLog.SendTime = _dateTimeService.CurrentTime;
            if (isNewLog)
                await _documentEstateDealSummaryGeneratedRepository.AddAsync(dsuLog, cancellationToken, false);
            else
                await _documentEstateDealSummaryGeneratedRepository.UpdateAsync(dsuLog, cancellationToken, false);
        }
    }

}
