namespace Notary.SSAA.BO.DataTransferObject.Commands.Document
{
    using Notary.SSAA.BO.DataTransferObject.Bases;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Result;

    /// <summary>
    /// Defines the <see cref="AutoGenerateOwnershipDocsCommand" />
    /// </summary>
    public class AutoGenerateOwnershipDocsCommand : BaseCommandRequest<ApiResult>
    {
        public string RestRegisterServiceReqID { get; set; }
        public string ReqNo { get; set; }
    }
}
