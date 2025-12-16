using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Configuration
{
    public static class Settings
    {
        public static string AgentDocValidatorConfiguration="E,A,W,A,A,A,W,W,W,W,W,W,W,W,W,W,W,A,A,W,A";
        public static string RelatedDocValidatorConfiguration="E,A,A,A,A,W,W,A,-,-,-,-,-,-,-,-,-,-";
        public static string WillDocValidatorConfiguration="E,A,W,A,A,A,W,W,W,W,W,W,W,W,W,W,W,A,A,W,A";
        public static bool RejectWrongIndividuals=true;
        public static string NationalNoTimeLimit="03:00-06:00";
        public static string FingerprintHistoryMatchingEnabledUnits="*";
        public static string FingerprintDoubleAquisitionUnits="*";
        public static bool ISShowFingerPrintHistory=false;
        public static bool RejectFinalConfirmOnDSUFail=true;
        public static bool RejectNaionalNoOnDSUFail=true;
        public static string DSUSendingTimeLimit="700";
        public static bool  AutoOwnershipGenerationEnabled=true;
        public static string  MechanizedTaxEnabledUnits="E:1|1004:51187,56338,51199,51348,52530,52548,52896" ;
        public static string  MechanizedExcApplEnabledUnits="*" ;
        public static string  MunicipalitySettlementEnabledUnits="E:1|1004:57999,57995,53169" ;
        public static string  AutomatedRemoveRestrictionEnabledUnits="E:1|1004:57999,57995|1008:*" ;
        public static string  ENoteBookEnabledUnits="*" ;
        public static string  ENoteBookAutoClassifyNoEnabledUnits="*" ;
        public static string  MinimumFingerprintQualityThreshold="40" ;
        public static string  PortableDeviceEnabledUnits="E:1|1004:57999|1005:*|1009:*" ;
        public static string  ForbiddenCostTypeCodes="2" ;
        public static string  ForbiddenSabtCostDocumentTypeCodes="" ;
        public static string  ForbiddenTahrirCostDocumentTypeCodes="113,213,231,232,311,312,322,323,331,431,442,611,612,613,711,721,731,912,913,914,915,916,917,917,921,923,942,981,982" ;
        public static bool  FingerprintDoubleAquisitionInFinalConfirmationForExordium=false ;
        public static string  MinimumFingerprintQualityThresholdForExordium="20" ;
        public static bool  IsFingerPrintForExordiumInFinalConfirmationEnabled=false ;
        public static bool  FingerprintDoubleAquisitionInLoginForExordium=false ;
        public static string  ISMOCEnabledForScriptorium="*" ;
        public static string  ISMOCTestEnabledForScriptorium="E:1|1004:57998,57995,57999|1014:57097" ;
        public static string  ISMOCCriticalConfigForScriptorium="E:1|1004:57998,57995,57999|1014:57097" ;
        public static string  ISMOCStopBecauseOfCriticalConfigForScriptorium="E:1|1004:57998,57995,57999|1014:57097" ;
        public static bool  IsForcedMOC=false ;
        public static bool  LFDMatchedPolicy=false ;
        public static string  LeaveBalanceScriptorium="E:1|1004:57999,53169,57995,57998" ;
        public static string  inquiryLetterServices="*" ;
        public static string  ISMunicipalityEnabledForScriptorium="E:1|1004:57998,57995,57999|1014:57097" ;
        public static string  IsDocReqEnabledForScriptorium="*" ;
        public static bool  IsFingerPrintForExordiumInLoginEnabled=false ;
        public static bool  RegistrationFingerPrintForExordiumIsEnabled=true ;
        public static string  FingerprintDoubleAquisitionInRegistrationForExordium="E:1|1004:57995,57998" ;
        public static string  ISTfaVerificationEnabledForScriptorium="E:1|1004:57998,57995,57999|1014:57097" ;
        public static string  DocCodesForTfaVerification="321, 322, 323" ;
        public static string  IsSanaCheckInAllDocTypesEnable="E:1|1004:57995,57999" ;
        public static string  TfaVerificationSecondsForResend="120" ;
        public static bool  IsFingerPrintForExordiumInSignCertificateEnabled=true ;
        public static bool  FingerprintDoubleAquisitionInSignCertificateForExordium=false ;
        public static string  UnRegisteredEstateInquiryForced="E:1|1004:57999,57995,53169,57998" ;
        public static string  UnRegisteredEstateSendDSUForced="E:1|1004:57999,57995,53169,57998" ;
        public static string  IsHajInquiryMechanized="E:1|1004:57999,57995,53169,57998" ;
        public static string  IsPostPayForNotifyMechanized="E:1|1004:57999,57995,53169,57998" ;//to do
        public static string  IsShahkarCheckInAllDocTypesEnable="E:1|1004:57999,57995,53169,57998" ;//to do
        public static string  IsEstateDocPrintInScriptoriumEnabled="E:1|1004:57999,57995,53169,57998" ;//to do
        public static string  sardaftarsFingerPrintDisabledConfig="E:1|1004:57999,57995,53169,57998" ;//to do
        public static bool  AppendErrorCode=false;
        public static string  DSUDealSummaryAllowedLevels="*";
        public static string  LeaveSubSystemEnabledUnits="*";//to do
        public static bool  IsSabtAhvalDirectAccessEnabled=true;//to do
        public static string  ValidatorConfiguration="D,A,A,A,A,A,A,A,A,A,W,W,A";
        public static string SanaIsRequied = "*";




        public static bool  SMSServiceEnabled=true;
        public static bool  DocAgentValidationMechanizedMessages=false;
        public static bool  RelatedDocumentMechanizedMessages=false;
        public static bool  FinalConfirmationMechanizedMessages=true;
        public static bool  UserDefinedMessages=false;
        public static bool  AzlOrEstefaMessages=false;
        public static bool  SSAADefinedMessages=false;


        public static bool  CostOfDocumentForPayMessages=true;
        public static bool CentralizedValidatorsMainGateway=true;
        public static string CanSendDS = "E:1|1004:57998";
        public static bool AccessTimeValidationEnabled=true;
        public static bool RejectIterativeClassifyNo = true;


        public static bool isCentralizedValidatorEnabled ( )
        {

            return CentralizedValidatorsMainGateway; 

        }




    }
}
