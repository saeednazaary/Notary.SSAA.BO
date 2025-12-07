using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Coordinator.Estate.Helpers
{
    public class ConfigurationParameterHelper
    {
        public  IRepository<BO.Domain.Entities.ConfigurationParameter> ConfigurationParameterRepository { get; set; }
        public IMediator Mediator { get; set; }
        public ConfigurationParameterHelper(IRepository<BO.Domain.Entities.ConfigurationParameter> configurationParameterRepository,IMediator mediator)
        {
            this.ConfigurationParameterRepository = configurationParameterRepository;
            this.Mediator = mediator;
        }
        public async Task<bool> IsEstateInquiryRealSendEnabled(CancellationToken cancellationToken)
        {
            var configurationParameter = await ConfigurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == "Estate_Inquiry_Real_Send_Enabled")
                                               .FirstOrDefaultAsync(cancellationToken);
            if (configurationParameter != null)
            {
                if (Convert.ToBoolean(configurationParameter.Value))
                    return true;
                return false;
            }
            return false;
        }
        public async Task<bool> IsDealSummaryRealSendEnabled(CancellationToken cancellationToken)
        {
            var configurationParameter = await ConfigurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == "Estate_DealSummary_Real_Send_Enabled")
                                               .FirstOrDefaultAsync(cancellationToken);
            if (configurationParameter != null)
            {
                if (Convert.ToBoolean(configurationParameter.Value))
                    return true;
                return false;
            }
            return false;
        }
        public async Task<string> CurrentEstateInquiryIsEnabled(CancellationToken cancellationToken)
        {
            var Current_Estate_Inquiry_Enabled = await ConfigurationParameterRepository.TableNoTracking.Where(x => x.Subject == "Current_Estate_Inquiry_Enabled").FirstAsync(cancellationToken);
            return await ParseConfigValue(Current_Estate_Inquiry_Enabled.Value, cancellationToken);            
        }
        public async Task<string> EstateInquirySabtAhvalMatchingIsRequired(CancellationToken cancellationToken)
        {
            var sabtAhvalMatching = await ConfigurationParameterRepository.TableNoTracking.Where(x => x.Subject == "Estate_Inquiry_Sabt_Ahval_Matching_Is_Required").FirstAsync(cancellationToken);
            return  await ParseConfigValue(sabtAhvalMatching.Value, cancellationToken);
        }
        public async Task<string> EstateInquiryIlencMatchingIsEnabled(CancellationToken cancellationToken)
        {
            var ilencMatching = await ConfigurationParameterRepository.TableNoTracking.Where(x => x.Subject == "Estate_Inquiry_Ilenc_Matching_Is_Enabled").FirstAsync(cancellationToken);
            return await ParseConfigValue(ilencMatching.Value, cancellationToken);
        }
        public async Task<string> EstateInquirySanaIsRequired(CancellationToken cancellationToken)
        {
            var configurationParameter = await ConfigurationParameterRepository.TableNoTracking.Where(x => x.Subject == "Estate_Inquiry_Sana_Is_Required").FirstAsync(cancellationToken);
            return await ParseConfigValue(configurationParameter.Value, cancellationToken);
        }
        public async Task<string> EstateInquiryShahkarIsRequired(CancellationToken cancellationToken)
        {
            var configurationParameter = await ConfigurationParameterRepository.TableNoTracking.Where(x => x.Subject == "Estate_Inquiry_Shahkar_Is_Required").FirstAsync(cancellationToken);            
            return await ParseConfigValue(configurationParameter.Value, cancellationToken);
        }
        public async Task<string> EstateInquirySendSMSIsEnabled(CancellationToken cancellationToken)
        {
            var configurationParameter = await ConfigurationParameterRepository.TableNoTracking.Where(x => x.Subject == "Estate_Inquiry_Send_SMS_Is_Enabled").FirstAsync(cancellationToken);
            return await ParseConfigValue(configurationParameter.Value, cancellationToken);
        }
        public async Task<bool> IsRealSendEnabledForEstateTaxInquiry(CancellationToken cancellationToken)
        {
            var configurationParameter = await ConfigurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == "Estate_Tax_Inquiry_Real_Send_Enabled")
                                               .FirstOrDefaultAsync(cancellationToken);
            if (configurationParameter != null)
            {
                var value = await ParseConfigValue(configurationParameter.Value, cancellationToken);
                if (Convert.ToBoolean(value))
                    return true;
                return false;
            }
            return false;
        }
        public async Task<string> GetConfigurationParameter(string ConfigurationParameterSubject, CancellationToken cancellationToken)
        {
            var configurationParameter = await ConfigurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == ConfigurationParameterSubject)
                                               .FirstOrDefaultAsync(cancellationToken);
            if (configurationParameter != null)
            {
                return configurationParameter.Value;

            }
            return "";
        }
        public async Task<string> ParseConfigValue(string configValue, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(new ParseConfigValueQuery() { Value = configValue }, cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data.Result;
            }
            else
            {
                return string.Empty;                
            }            
        }       
        public async Task<bool> EstateInquiryPaymentIsRequired(CancellationToken cancellationToken)
        {
            var configurationParameter = await  ConfigurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == "Estate_Inquiry_Payment_Is_Required")
                                               .FirstOrDefaultAsync(cancellationToken);
            if (configurationParameter != null)
            {
                var value = await ParseConfigValue(configurationParameter.Value, cancellationToken);
                if (Convert.ToBoolean(value))
                    return true;
                return false;
            }
            return false;
        }
        public async Task<bool> EstateTaxInquirySendSMSIsEnabled(CancellationToken cancellationToken)
        {
            var configurationParameter = await ConfigurationParameterRepository
                                               .TableNoTracking
                                               .Where(x => x.Subject == "Estate_Tax_Inquiry_Send_SMS_Is_Enabled")
                                               .FirstOrDefaultAsync(cancellationToken);
            if (configurationParameter != null)
            {
                var value = await ParseConfigValue(configurationParameter.Value, cancellationToken);
                if (Convert.ToBoolean(value))
                    return true;
                return false;
            }
            return false;
        }
    }
}
