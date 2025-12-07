using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint
{
    public class CreateMocLogCommand:BaseCommandRequest<ApiResult>
    {
        public string ActionName { get; set; }
        public string ActionResults { get; set; }
        public string ActionType { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public Finger FingerType { get; set; }
        public string NationalNo { get; set; }
        public string OtherInformation { get; set; }
        public string RecievedPacket { get; set; }
        public string RelatedDocId { get; set; }
        public string RelatedFingerPrintLogId { get; set; }
        public string SentPacket { get; set; }
        public byte[] FaceImage { get; set; }
    }
}
