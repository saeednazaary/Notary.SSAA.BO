using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Shell
{
    public class SmartQuotaGeneratorEngine
    {
        private QuotasGeneratorEngine  _quotasGeneratorEngine=null;
        private IDocumentRepository _documentRepository;
        private ClientConfiguration? _clientConfigurations;
        public SmartQuotaGeneratorEngine ( QuotasGeneratorEngine quotasGeneratorEngine, IDocumentRepository documentRepository, ClientConfiguration? clientConfiguration )
        { 
            _quotasGeneratorEngine = quotasGeneratorEngine;
            _clientConfigurations= clientConfiguration;
            _documentRepository = documentRepository;
        }

        public async Task<(bool,Document,string)> GenerateQuotas (  Document theCurrentRegisterServiceReq,CancellationToken cancellationToken,  string messages, bool immediateCommit = true )
        {
            bool isGenerated = true;
            Document theCurrentRegisterServiceReqRef=  theCurrentRegisterServiceReq;
             string messagesRef=messages;
            try
            {
                #region ImmediateCommit
                if ( immediateCommit )
                {
                    //using ( NotaryOfficeCommons.TransactedOperation newContext = new NotaryOfficeCommons.TransactedOperation ( true ) )
                    {
                        theCurrentRegisterServiceReqRef = await _documentRepository.GetAsync ( t => t.Id == theCurrentRegisterServiceReqRef.Id,cancellationToken );


                        QuotasValidator quotasValidator = new QuotasValidator(_clientConfigurations);
                        bool dealQuotasAreValid = quotasValidator.VerifyDealQuotas(theCurrentRegisterServiceReqRef, ref messagesRef);
                        if ( !dealQuotasAreValid )
                            return (false, theCurrentRegisterServiceReqRef,messagesRef);



                        isGenerated = _quotasGeneratorEngine.GeneratePersonQuotas ( ref theCurrentRegisterServiceReqRef, ref messagesRef );


                        try
                        {
                            if ( isGenerated && this.AreGeneratedQuotasCountValid ( theCurrentRegisterServiceReq ) )
                            {

                                _documentRepository.UpdateAsync(theCurrentRegisterServiceReq, cancellationToken, true);
                                // _documentRepository.SaveChanges(cancellationToken);
                                // Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();
                            }
                            else
                            {
                                //Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();

                            }
                        }
                        catch (Exception ex )
                        {
                            //Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                            messagesRef = ex.ToString ();
                            isGenerated = false;
                        }
                    }

                }
                #endregion

                #region Non-Immediate
                else
                {
                    QuotasValidator quotasValidator = new QuotasValidator(_clientConfigurations);
                    bool dealQuotasAreValid = quotasValidator.VerifyDealQuotas(theCurrentRegisterServiceReqRef, ref messagesRef);
                    if ( !dealQuotasAreValid )
                        return (false, theCurrentRegisterServiceReqRef, messagesRef);

                    isGenerated = _quotasGeneratorEngine.GeneratePersonQuotas ( ref theCurrentRegisterServiceReqRef, ref messagesRef );
                }
                #endregion

            }
            catch (Exception ex )
            {
               // ZCommonUtility.LoggerHelper.LogError ( "IntelligantDocumentInheritor.Shell.SmartQuotaGeneratorEngine.GenerateQuotas", ex, theCurrentRegisterServiceReq.ObjectId );
                if ( string.IsNullOrWhiteSpace ( messagesRef ) )
                    messagesRef = "خطا در انجام محاسبات حسب السهم!";

                isGenerated = false;
            }

            isGenerated = isGenerated && this.AreGeneratedQuotasCountValid ( theCurrentRegisterServiceReqRef );
            return (isGenerated, theCurrentRegisterServiceReqRef, messagesRef);
        }

        private bool AreGeneratedQuotasCountValid ( Document theCurrentRegisterServiceReq )
        {
            foreach (DocumentEstate theOneRegCase in theCurrentRegisterServiceReq.DocumentEstates )
            {
                if ( theOneRegCase.IsProportionateQuota != YesNo.Yes.GetString() || string.IsNullOrWhiteSpace ( theOneRegCase.EstateInquiryId ) )
                    continue;

                if ( !theOneRegCase.DocumentEstateQuotaDetails.Any () )
                    return false;
            }

            return true;
        }
    }
}
