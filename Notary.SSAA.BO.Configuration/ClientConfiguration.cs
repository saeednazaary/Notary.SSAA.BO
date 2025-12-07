using Notary.SSAA.BO.SharedKernel.Contracts.Security;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.SharedKernel.Result;
using Microsoft.Extensions.Configuration;
using Stimulsoft.Css;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;

namespace Notary.SSAA.BO.Configuration
{
    public class ClientConfiguration
    {
        /* NewVersion ToDoList*/
        // ICMSOrganization _currentOrg = null;
       public ScriptoriumInformation _currentOrg = new ScriptoriumInformation();
        private bool _rejectWrongIndividuals = true;
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientConfiguration ( IUserService _userService, IConfiguration _configuration, IHttpContextAccessor httpContextAccessor )
        {
            userService = _userService;
            _currentOrg = userService.UserApplicationContext.ScriptoriumInformation;
            configuration = _configuration;
            _httpContextAccessor = httpContextAccessor;

            /* NewVersion ToDoList*/
            //_currentOrg = inputOrganization;

            //if (inputOrganization == null)
            //    _currentOrg = Rad.CMS.InstanceBuilder.GetEntityById<ICMSOrganization>(Rad.CMS.BaseInfoContext.Instance.CurrentCMSOrganization.Id);

            #region RejectWrongIndividuals
            // string rejectWrongIndividuals = System.Configuration.ConfigurationManager.AppSettings["RejectWrongIndividuals"];
            //Boolean.TryParse(rejectWrongIndividuals, out _rejectWrongIndividuals);
            _rejectWrongIndividuals = Settings.RejectWrongIndividuals;
            #endregion
        }

        public async Task  initializeScriptorium(string scriptoriumId)
        {
            var token = GetBearerToken();

            var scriptorium = await this.getScriptoriumInformation(new string[] { scriptoriumId }, token);


            _currentOrg = scriptorium;
            currentCMSOrganization = _currentOrg;
        }

        public string NationalNoTimeLimitation
        {
            get
            {
                return Settings.NationalNoTimeLimit;//System.Configuration.ConfigurationManager.AppSettings["NationalNoTimeLimit"];
            }
        }

        public bool showFinalVerificationWindow { get; set; }

        public bool RejectWrongIndividuals
        {
            get
            {
                return _rejectWrongIndividuals;
            }
        }

        public bool GetPersonPhotoEnabled { get; set; }

        public bool ViewAgentDocumentImageEnabled { get; set; }

        public bool ViewRelatedDocumentImageEnabled { get; set; }

        public bool SMSServiceEnabled { get; set; }

        public bool UserDefinedMessages { get; set; }

        public bool IsDocCreationPermitted { get; set; }

        public DSUActionLevel IsDSUDealSummaryCreationEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsDSUDealSummaryEnabled ();
            }
        }
        public bool IsFingerprintHistoryMatchingEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "FingerprintHistoryMatchingEnabledUnits", _currentOrg );
            }
        }
        public bool IsDoubleFingerprintAquisitionEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "FingerprintDoubleAquisitionUnits", _currentOrg );
            }
        }

        public bool ISShowFingerPrintHistory
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ISShowFingerPrintHistory", _currentOrg );
            }
        }

        public bool IsFingerprintEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsFingerprintEnabled ();
            }
        }
        public string FingerprintEnabledDate
        {
            get
            {
                string levelCode = "0";

                if ( _currentOrg != null )
                    levelCode = _currentOrg.Unit.LevelCode.Substring ( 0, 4 );

                switch ( levelCode )
                {
                    case "1018": // ایلام
                        return "1394/07/06";
                    case "1030": // زنجان
                        return "1394/03/24";
                    case "1012": // قزوین
                    case "1029": // همدان
                        return "1394/06/22";
                    case "1013": // مازندران
                    case "1015": // گلستان
                        return "1394/07/20";
                    case "1021": // گیلان
                        return "1394/07/25";
                    case "1005": // اصفهان
                        return "1394/08/09";
                    case "1025": // سیستان و بلوچستان
                        return "1394/08/10";
                    case "1024": // یزد
                        return "1394/08/12";
                    case "1020": // خوزستان
                    case "1022": // کرمان
                    case "1011": // خراسان جنوبی
                        return "1394/08/14";
                    case "1027": // بوشهر
                    case "1023": // کهگیلویه و بویراحمد
                        return "1394/08/21";
                    case "1033": // البرز
                        return "1394/08/24";
                    case "1017": // چهارمحال بختیاری
                    case "1031": // سمنان
                        //case "1008": // مرکزی // استان مرکزی به مدت 1 روز تمدید گردید و به تاریخ 2 آذر منتقل شد
                        return "1394/09/01";
                    case "1008": // مرکزی
                        return "1394/09/02";

                    case "1032": // قم
                        return "1394/09/14";
                    case "1014": // فارس
                    case "1028": // لرستان
                        return "1394/09/12";

                    case "1010": //خراسان شمالی
                    case "1019": //کرمانشاه
                    case "1034": //کردستان
                    case "1026": //هرمزگان
                        return "1394/09/17";

                    case "1009": //خراسان رضوی
                        return "1394/09/22";

                    case "1016": //آذربایجان غربی
                    case "1007": //آذربایجان شرقی
                    case "1006": //اردبیل
                        return "1394/11/08";

                    case "1004": // تهران
                        string tehranPilots = "57999,53162,52820,53169,55955";

                        if ( _currentOrg != null && tehranPilots.Contains ( _currentOrg.Code ) )
                            return "1394/03/20";
                        else
                            return "1394/10/15";
                    default:
                        return "9999/99/99";
                }
            }
        }
        public /*Rad.BaseInfo.SystemConfiguration.ICMSOrganization*/ScriptoriumInformation? currentCMSOrganization { get; set; }
        public bool RejectFinalConfirmOnDSUFail
        {
            get
            {
                //string rejectFinalConfirmOnDSUFailConfig = System.Configuration.ConfigurationManager.AppSettings["RejectFinalConfirmOnDSUFail"] as string;
                bool rejectFinalConfirmOnDSUFail = false;
                rejectFinalConfirmOnDSUFail = Settings.RejectFinalConfirmOnDSUFail;
                //Boolean.TryParse(rejectFinalConfirmOnDSUFailConfig, out rejectFinalConfirmOnDSUFail);

                return rejectFinalConfirmOnDSUFail;
            }
        }
        public bool RejectNaionalNoOnDSUFail
        {
            get
            {
                //string rejectNaionalNoOnDSUFailConfig = System.Configuration.ConfigurationManager.AppSettings["RejectNaionalNoOnDSUFail"] as string;
                bool rejectNaionalNoOnDSUFail = false;
                //Boolean.TryParse(rejectNaionalNoOnDSUFailConfig, out rejectNaionalNoOnDSUFail);
                rejectNaionalNoOnDSUFail = Settings.RejectNaionalNoOnDSUFail;

                return rejectNaionalNoOnDSUFail;
            }
        }
        /// <summary>
        /// تاریخ راه اندازی ارسال مکانیزه خلاصه معامله از طریق سامانه ثبت الکترونیک اسناد
        /// </summary>
        public string DSUInitializationDate
        {
            get
            {
                string levelCode = "0";

                if ( _currentOrg != null )
                    levelCode = _currentOrg.Unit.LevelCode.Substring ( 0, 4 );

                switch ( levelCode )
                {
                    case "1018":
                        return "1393/03/11"; //ایلام
                    case "1014":
                        return "1393/04/23"; //فارس                    
                    default:
                        if ( _currentOrg != null && _currentOrg.Id == "A22CADED9EF14A8C839BE6D656488333" ) //دفترخانه خانم سهرابی
                            return "1393/02/28";
                        else if ( _currentOrg != null && _currentOrg.Id == "907420174D8742F2B507C90971DAE4B9" ) //57999
                            return "1393/04/23";
                        else
                            return "1393/06/09"; //کل کشور
                }
            }
        }
        public int DSUSendingTimeLimit
        {
            get
            {
                string deadLine =Settings.DSUSendingTimeLimit; //System.Configuration.ConfigurationManager.AppSettings["DSUSendingTimeLimit"] as string;
                if ( string.IsNullOrWhiteSpace ( deadLine ) )
                    deadLine = "5"; //As Default Value

                int deadLineDaysCount = 5;
                bool converted = int.TryParse(deadLine, out deadLineDaysCount);

                return deadLineDaysCount;
            }
        }
        public bool IsAutoOwnershipGenerationEnabled
        {
            get
            {
                //string autoOwnershipGenerationEnabledConfig = System.Configuration.ConfigurationManager.AppSettings["AutoOwnershipGenerationEnabled"] as string;
                bool autoOwnershipGenerationEnabled = false;
                //Boolean.TryParse(autoOwnershipGenerationEnabledConfig, out autoOwnershipGenerationEnabled);
                autoOwnershipGenerationEnabled = Settings.AutoOwnershipGenerationEnabled;

                return autoOwnershipGenerationEnabled;
            }
        }
        public bool IsLeaveSubSystemEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsLeaveSubSystemEnabled ( _currentOrg );
            }
        }
        public bool IsMechanizedTaxEnabled
        {
            get
            {
                ParserOperators configParser = new ParserOperators(_currentOrg);
                return configParser.IsCurrentOragnizationPermitted ( "MechanizedTaxEnabledUnits", _currentOrg );
            }
        }

        public bool IsMechanizedExcApplEnabled
        {
            get
            {
                ParserOperators configParser = new ParserOperators(_currentOrg);
                return configParser.IsCurrentOragnizationPermitted ( "MechanizedExcApplEnabledUnits", _currentOrg );
            }
        }

        public bool IsMechanizedMunicipalitySettlementEnabled
        {
            get
            {
                ParserOperators configParser = new ParserOperators(_currentOrg);
                return configParser.IsCurrentOragnizationPermitted ( "MunicipalitySettlementEnabledUnits", _currentOrg );
            }
        }
        public bool IsAutomatedRemoveRestrictionEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "AutomatedRemoveRestrictionEnabledUnits", _currentOrg );
            }
        }
        public bool IsENoteBookViewEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ENoteBookEnabledUnits", _currentOrg );
            }
        }
        public bool IsENoteBookAutoClassifyNoEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ENoteBookAutoClassifyNoEnabledUnits", _currentOrg );
            }
        }
        public string ENoteBookEnabledDate
        {
            get
            {
                string levelCode = "0";

                if ( _currentOrg != null )
                    levelCode = _currentOrg.Unit.LevelCode.Substring ( 0, 4 );
                else
                    return "1395/06/10";
                switch ( levelCode )
                {
                    case "1004": // تهران
                        string tehranPilots = "52314,52144,52308,50601,55955,54497,52771,53169,53162,50093";
                        string testTeam = "57999,57998,57997,57996,57995";

                        if ( testTeam.Contains ( _currentOrg.Code ) )
                            return "1395/02/15";
                        else if ( tehranPilots.Contains ( _currentOrg.Code ) )
                            return "1395/05/25";
                        else
                            return "1395/06/10";

                    case "1005": // اصفهان
                        string isfahanPilots = "55908";
                        if ( isfahanPilots.Contains ( _currentOrg.Code ) )
                            return "1395/05/25";
                        else
                            return "1395/06/10";

                    case "1018":
                        string ilamPilots = "52683";
                        if ( ilamPilots.Contains ( _currentOrg.Code ) )
                            return "1395/05/25";
                        else
                            return "1395/06/10";

                    case "1014":
                        string farsPilots = "53607";
                        if ( farsPilots.Contains ( _currentOrg.Code ) )
                            return "1395/05/25";
                        else
                            return "1395/06/10";

                    case "1015":
                        string golestanPilots = "55495";
                        if ( golestanPilots.Contains ( _currentOrg.Code ) )
                            return "1395/05/25";
                        else
                            return "1395/06/10";

                    default:    //راه اندازی کل کشور در تاریخ 1395/06/10 انجام گردید. 
                        return "1395/06/10";
                }
            }
        }
        public int MinimumFingerprintQualityThreshold
        {
            get
            {
                return int.Parse ( Settings.MinimumFingerprintQualityThreshold );//int.Parse(System.Configuration.ConfigurationManager.AppSettings["MinimumFingerprintQualityThreshold"].ToString());
            }
        }
        public bool IsPortableDeviceEnabled
        {
            get
            {
                ParserOperators parserOperator = new ParserOperators(_currentOrg);
                return parserOperator.IsCurrentOragnizationPermitted ( "PortableDeviceEnabledUnits", _currentOrg );
            }
        }
        public List<string> ForbiddenCostTypeCodes
        {
            get
            {
                string forbiddenCostTypeCodesConfig = Settings.ForbiddenCostTypeCodes;// System.Configuration.ConfigurationManager.AppSettings["ForbiddenCostTypeCodes"] as string;

                string[] forbiddenCostTypeCodes = forbiddenCostTypeCodesConfig.Split(',');

                List<string> forbiddenTypes = new List<string>();
                for ( int i = 0; i < forbiddenCostTypeCodes.Length; i++ )
                {
                    forbiddenTypes.Add ( forbiddenCostTypeCodes [ i ] );
                }

                return forbiddenTypes;
            }
        }
        public List<string> ForbiddenSabtCostDocumentTypeCodes
        {
            get
            {
                string forbiddenSabtCostDocumentTypeCodesConfig = Settings.ForbiddenSabtCostDocumentTypeCodes;//System.Configuration.ConfigurationManager.AppSettings["ForbiddenSabtCostDocumentTypeCodes"] as string;

                string[] forbiddenSabtCostTypeCodes = forbiddenSabtCostDocumentTypeCodesConfig.Split(',');

                List<string> forbiddenTypes = new List<string>();
                for ( int i = 0; i < forbiddenSabtCostTypeCodes.Length; i++ )
                {
                    forbiddenTypes.Add ( forbiddenSabtCostTypeCodes [ i ] );
                }

                return forbiddenTypes;
            }
        }
        public List<string> ForbiddenTahrirCostDocumentTypeCodes
        {
            get
            {
                string forbiddenTahrirCostDocumentTypeCodesConfig = Settings.ForbiddenTahrirCostDocumentTypeCodes;//System.Configuration.ConfigurationManager.AppSettings["ForbiddenTahrirCostDocumentTypeCodes"] as string;

                string[] forbiddenTahrirCostTypeCodes = forbiddenTahrirCostDocumentTypeCodesConfig.Split(',');

                List<string> forbiddenTypes = new List<string>();
                for ( int i = 0; i < forbiddenTahrirCostTypeCodes.Length; i++ )
                {
                    forbiddenTypes.Add ( forbiddenTahrirCostTypeCodes [ i ] );
                }

                return forbiddenTypes;
            }
        }

        public int SemaphoreLifeTime
        {
            get
            {
                return 60;
            }
        }

        #region Added By Khateri
        public bool IsDoubleFingerprintAquisitionInFinalConfirmationForExordiumEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "FingerprintDoubleAquisitionInFinalConfirmationForExordium", _currentOrg );
            }
        }

        public int MinimumFingerprintQualityThresholdForExordium
        {
            get
            {
                return int.Parse ( Settings.MinimumFingerprintQualityThresholdForExordium );//int.Parse(System.Configuration.ConfigurationManager.AppSettings["MinimumFingerprintQualityThresholdForExordium"].ToString());
            }
        }
        public bool IsFingerPrintForExordiumInFinalConfirmationEnabled
        {
            get
            {

                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsFingerPrintForExordiumInFinalConfirmationEnabled", _currentOrg );
            }
        }


        public bool IsDoubleFingerprintAquisitionInLoginForExordiumEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "FingerprintDoubleAquisitionInLoginForExordium", _currentOrg );
            }
        }

        public bool ISMOCEnabledForScriptorium
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ISMOCEnabledForScriptorium", _currentOrg );
            }
        }

        public bool ISMOCTestEnabledForScriptorium
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ISMOCTestEnabledForScriptorium", _currentOrg );
            }
        }

        public bool ISMOCCriticalConfigForScriptorium
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ISMOCCriticalConfigForScriptorium", _currentOrg );
            }
        }

        public bool ISMOCStopBecauseOfCriticalConfigForScriptorium
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ISMOCStopBecauseOfCriticalConfigForScriptorium", _currentOrg );
            }
        }

        public bool IsForcedMOC
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsForcedMOC", _currentOrg );
            }
        }

        public bool LFDMatchedPolicy
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "LFDMatchedPolicy", _currentOrg );
            }
        }

        public bool LeaveBalanceScriptorium
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "LeaveBalanceScriptorium", _currentOrg );
            }
        }

        public bool inquiryLetterServices
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "inquiryLetterServices", _currentOrg );
            }
        }


        public bool ISMunicipalityEnabledForScriptorium
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ISMunicipalityEnabledForScriptorium", _currentOrg );
            }
        }

        public bool IsDocReqEnabledForScriptorium
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsDocReqEnabledForScriptorium", _currentOrg );
            }
        }

        public bool IsFingerPrintForExordiumInLoginEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsFingerPrintForExordiumInLoginEnabled", _currentOrg );
            }
        }

        public bool RegistrationFingerPrintForExordiumIsEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "RegistrationFingerPrintForExordiumIsEnabled", _currentOrg );
            }
        }


        public bool IsDoubleFingerprintAquisitionInRegistrationForExordiumEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "FingerprintDoubleAquisitionInRegistrationForExordium", _currentOrg );
            }
        }

        public bool ISTfaVerificationEnabledForScriptorium
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "ISTfaVerificationEnabledForScriptorium", _currentOrg );
            }
        }

        public string DocCodesForTfaVerification
        {
            get
            {
                return Settings.DocCodesForTfaVerification;//System.Configuration.ConfigurationManager.AppSettings["DocCodesForTfaVerification"] as string;
            }
        }
        //for check sana for all docstype
        public bool IsSanaCheckInAllDocTypesEnable
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsSanaCheckInAllDocTypesEnable", _currentOrg );
            }
        }

        public string TfaVerificationSecondsForResend
        {
            get
            {
                return Settings.TfaVerificationSecondsForResend;//System.Configuration.ConfigurationManager.AppSettings["TfaVerificationSecondsForResend"] as string;
            }
        }

        public bool IsFingerPrintForExordiumInSignCertificateEnabled
        {
            get
            {

                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsFingerPrintForExordiumInSignCertificateEnabled", _currentOrg );
            }
        }

        public bool IsDoubleFingerprintAquisitionInSignCertificateForExordiumEnabled
        {
            get
            {

                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "FingerprintDoubleAquisitionInSignCertificateForExordium", _currentOrg );
            }
        }

        public bool IsUnRegisteredEstateInquiryForced
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "UnRegisteredEstateInquiryForced", _currentOrg );
            }
        }

        public bool IsUnRegisteredEstateSendDSUForced
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "UnRegisteredEstateSendDSUForced", _currentOrg );
            }
        }

        public bool IsHajInquiryMechanized
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsHajInquiryMechanized", _currentOrg );
            }
        }

        public bool IsPostPayForNotifyMechanized
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsPostPayForNotifyMechanized", _currentOrg );
            }
        }

        public bool IsShahkarCheckInAllDocTypesEnable
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsShahkarCheckInAllDocTypesEnable", _currentOrg );
            }
        }

        public bool IsEstateDocPrintInScriptoriumEnabled
        {
            get
            {
                ParserOperators parserOperators = new ParserOperators(_currentOrg);
                return parserOperators.IsCurrentOragnizationPermitted ( "IsEstateDocPrintInScriptoriumEnabled", _currentOrg );
            }
        }
        #endregion

        //  anssari 980222
        public bool ISsardaftarFingerPrintDisabled
        {
            get
            {
                string _CurrentUserID = userService.UserApplicationContext.User.UserName;//Rad.CMS.BaseInfoContext.Instance.CurrentUser.Id;

                string sardaftarsFingerPrintDisabledString = Settings.sardaftarsFingerPrintDisabledConfig;//System.Configuration.ConfigurationManager.AppSettings["sardaftarsFingerPrintDisabledConfig"] as string;

                string[] sardaftarsFingerPrintDisabledArray = sardaftarsFingerPrintDisabledString.Split(',');

                List<string> forbiddenTypes = new List<string>();
                for ( int i = 0; i < sardaftarsFingerPrintDisabledArray.Length; i++ )
                {
                    if ( _CurrentUserID == sardaftarsFingerPrintDisabledArray [ i ] )
                        return true;
                }

                return false;
            }
        }
        public async Task<ScriptoriumInformation> getScriptoriumInformation ( string [ ] ids, string token )
        {
            ScriptoriumInformation scriptoriumInformation = new ScriptoriumInformation();
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                ( httpRequestMessage, cert, cetChain, policyErrors ) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse ( token );
            string bo_Apis_prefix = configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;

            var baseInfoUrl = configuration.GetValue<string>("InternalGatewayUrl")+ bo_Apis_prefix + "api/v1/Specific/Ssar/Common/GetScriptoriumById";
            ScriptoriumInput scriptoriumInput = new ScriptoriumInput(ids);
            StringContent content = new(JsonConvert.SerializeObject(scriptoriumInput), System.Text.Encoding.UTF8, "application/json");


            var eventResponse = await client.PostAsync(baseInfoUrl, content).ConfigureAwait(false);

            if ( eventResponse.StatusCode == HttpStatusCode.OK )
            {
                string res = await eventResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ApiResult<ScriptoriumData>>(res);
                if ( data.IsSuccess && data.Data.ScriptoriumList != null && data.Data.ScriptoriumList.Count > 0 )
                {
                    return data.Data.ScriptoriumList [ 0 ];
                }
                return null;


            }
            else
            {
                return null;
            }
            return null;

        }
        private string GetBearerToken ( )
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string authorizationHeader = context.Request.Headers["Authorization"];

            if ( !string.IsNullOrEmpty ( authorizationHeader ) && authorizationHeader.StartsWith ( "Bearer ", StringComparison.OrdinalIgnoreCase ) )
            {
                // Extract the token from the Authorization header.
                //string token = authorizationHeader.Substring("Bearer ".Length).Trim();
                return authorizationHeader;
            }

            return null; // No Bearer token found in the request.
        }
    }
}
