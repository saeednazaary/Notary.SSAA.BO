namespace Notary.SSAA.BO.Domain.Abstractions
{
    using Notary.SSAA.BO.Domain.Abstractions.Base;
    using Notary.SSAA.BO.Domain.Entities;

    /// <summary>
    /// Defines the <see cref="IDocumentPersonRelatedRepository" />
    /// </summary>
    public interface IDocumentPersonRelatedRepository : IRepository<DocumentPersonRelated>
    {
        /// <summary>
        /// The GetDocumentPersonRelatedAgent
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="List{DocumentPersonRelated}"/></returns>
        List<DocumentPersonRelated> GetDocumentPersonRelatedAgent ( Guid id );

        /// <summary>
        /// The GetDocumentPersonRelatedById
        /// </summary>
        /// <param name="detailsList">The detailsList<see cref="List{string}"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="mainPersonNationalNo">The mainPersonNationalNo<see cref="string"/></param>
        /// <param name="relatedPersonNationalNo">The relatedPersonNationalNo<see cref="string"/></param>
        /// <param name="agentTypesId">The agentTypesId<see cref="List{string}"/></param>
        /// <param name="mainPersonId">The mainPersonId<see cref="string"/></param>
        /// <param name="agentPersonId">The agentPersonId<see cref="string"/></param>
        /// <returns>The <see cref="Task{DocumentPersonRelated}"/></returns>
        Task<List<DocumentPersonRelated>> GetDocumentPersonRelated ( List<string> detailsList,
              CancellationToken cancellationToken, string documentId = null,
              string id = null, string mainPersonNationalNo = null, string relatedPersonNationalNo = null,
              List<string> agentTypesId = null, string mainPersonId = null, string agentPersonId = null );
    }
}
