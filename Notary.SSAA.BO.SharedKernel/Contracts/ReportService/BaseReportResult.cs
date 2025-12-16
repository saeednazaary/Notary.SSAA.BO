namespace Notary.SSAA.BO.SharedKernel.Contracts.ReportService
{
    public class BaseReportResult
    {
        public byte[] Data { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }

    }
}
