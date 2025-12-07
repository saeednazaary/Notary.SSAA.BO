using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
using Notary.SSAA.BO.SharedKernel.Contracts.Security;

namespace Notary.SSAA.BO.SharedKernel.Interfaces
{
    public interface IAnnotationsGenerator<R,P>
    {
        public Task<AnnotationPack> GenerateSingleAnnotation ( string documentId = null, string nationalNo = null, string currentCMSOrganizationID = null );
        public Task<List<AnnotationPack>> GenerateAnnotations ( string state, string scriptoriumID, List<string> no, string currentCMSOrganizationID = null );
        public AnnotationPack GenerateSingleAgentAnnotation ( List<R> documentPersonRelatedAgentDocuments );
        public Task<List<AnnotationPack>> GenerateAnnotationsForOnePerson ( P theOneDocPerson );
        public Task<AnnotationPack> GenerateReliablePersonsAnnotations ( string nationalno );


    }
}
