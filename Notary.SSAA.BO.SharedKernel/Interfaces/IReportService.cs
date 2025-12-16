using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Contracts.ReportService;

namespace Notary.SSAA.BO.SharedKernel.Interfaces
{
    public interface IReportService
    {
        public Task<BaseReportResult> SignRequestReport1<TRequest>(TRequest request) where TRequest : class ;
        public Task<BaseReportResult> SignRequestReport2<TRequest>(TRequest request) where TRequest : class ;

    }
}
