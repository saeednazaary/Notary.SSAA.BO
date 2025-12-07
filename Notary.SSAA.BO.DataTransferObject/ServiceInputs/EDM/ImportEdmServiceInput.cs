

using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices.EDM;
using Notary.SSAA.BO.SharedKernel.Result;

namespace SNotary.SSAA.BO.DataTransferObject.ServiceInputs.Edm
{
    public class ImportEdmServiceInput :BaseExternalRequest<ApiResult<ImportEdmServiceResponse>>
    {
        public string ClientId { get; set; }
        public string MainObjectId { get; set; }
        public string? externalTrackCode { get; set; }
        public DocumentMetadata? documentMetadata { get; set; }
        public DocumentPayload? documentPayload { get; set; }
    }

    public class DocumentMetadata
    {
        public DescriptiveData? descriptiveData { get; set; }
        public AdministrativeData? administrativeData { get; set; }
        public StructuralData? structuralData { get; set; }
    }

    public class AdministrativeData
    {
        public string? createDate { get; set; }
        public string? modifyDate { get; set; }
        public string? documentSize { get; set; }
        public string? accessLevel { get; set; }
        public string? documentPath { get; set; }
        public string? backupTime { get; set; }
        public int signatureType { get; set; }
    }

    public class DescriptiveData
    {
        public string? documentCode { get; set; }
        public int documentType { get; set; }
        public string? documentName { get; set; }
        public string? description { get; set; }
        public IList<DocumentOwners> documentOwners { get; set; }
        public int originalSystem { get; set; }
        public string? province { get; set; }
        public string? provinceCode { get; set; }
        public string? unitId { get; set; }
    }

    public class DocumentOwners
    {
        public int sequence { get; set; }
        public string? ownerId { get; set; }
        public int ownerType { get; set; }
        public string? documentOwner { get; set; }
        public string? ownerMobileNo { get; set; }
        public string? legalPersonAgentId { get; set; }
        public int? ownerRole { get; set; }
    }

    public class StructuralData
    {
        public int pageCount { get; set; }
        public int status { get; set; }
        public int electronicFormat { get; set; }
        public string? schemaVersion { get; set; }
        public int documentVersion { get; set; }
    }

    public class DocumentPayload
    {
        public string? TokenAndClaims { get; set; }
        public string? SignedDoc { get; set; }
    }
}
