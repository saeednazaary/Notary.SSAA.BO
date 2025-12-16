using Azure.Core;
using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Threading;

namespace Notary.SSAA.BO.Coordinator.Estate.Helpers
{
    public class GeneralExternalServiceHelper
    {
        private IMediator _mediator;
        private ConfigurationParameterHelper _configurationParameterHelper;
        public GeneralExternalServiceHelper(IRepository<BO.Domain.Entities.ConfigurationParameter> configurationParameterRepository, IMediator mediator)
        {
            this._mediator = mediator;
            _configurationParameterHelper = new ConfigurationParameterHelper(configurationParameterRepository, this._mediator);
        }
        
        public async Task<SanaServiceViewModel?> CallSanaService(string nationalityCode, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SanaServiceQuery() { NationalNo = nationalityCode }, cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }
        public async Task<ShahkarServiceViewModel?> CallShahkarService(string nationalityCode, string mobileNo, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ShahkarServiceQuery() { NationalNo = nationalityCode, MobileNumber = mobileNo }, cancellationToken);
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }
        public async Task<ApiResult<SabtAhvalServiceViewModel>> CallSabtAhvalService(string PersonBirthDate,string PersonNationalityCode,CancellationToken cancellationToken)
        {
            return await _mediator.Send(new SabtAhvalServiceQuery() { BirthDate = PersonBirthDate, NationalNo = PersonNationalityCode }, cancellationToken);
        }
        public async Task<ApiResult<RealForeignerServiceViewModel>> CallForeignerRealPersonService(string PersonNationalityCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new RealForeignerServiceQuery() { ForeignerCode = PersonNationalityCode }, cancellationToken);
        }
        public async Task<ApiResult<ILENCServiceViewModel>> CallIlencService(string PersonNationalityCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ILENCServiceQuery() { NationalNo = PersonNationalityCode }, cancellationToken);
        }
        public async Task<ApiResult<LegalForeignerServiceViewModel>> CallForeignerLegalPersonService(string PersonNationalityCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new LegalForiegnerServiceQuery() { ForeignerCode = PersonNationalityCode }, cancellationToken);
        }
    }
}
