namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Infrastructure.Contexts;
    using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="DocumentPersonRelatedRepository" />
    /// </summary>
    public class DocumentPersonRelatedRepository : Repository<DocumentPersonRelated>, IDocumentPersonRelatedRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentPersonRelatedRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="ApplicationContext"/></param>
        public DocumentPersonRelatedRepository ( ApplicationContext dbContext ) : base ( dbContext )
        {
        }

        /// <summary>
        /// The GetDocumentPersonRelatedAgent
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="List{DocumentPersonRelated}"/></returns>
        public List<DocumentPersonRelated> GetDocumentPersonRelatedAgent ( Guid id )
        {
            return TableNoTracking.Include ( x => x.AgentType ).Include ( x => x.AgentPerson ).Where ( x => x.MainPersonId == id ).ToList ();
        }

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
        public async Task<List<DocumentPersonRelated>> GetDocumentPersonRelated ( List<string> detailsList,
            CancellationToken cancellationToken, string documentId = null,
            string id = null, string mainPersonNationalNo = null, string relatedPersonNationalNo = null,
            List<string> agentTypesId = null, string mainPersonId = null, string agentPersonId = null )
        {
            IQueryable< DocumentPersonRelated> query=null;
            if ( documentId != null )
            {
                query = TableNoTracking.Where ( x => x.DocumentId == Guid.Parse ( id ) );

            }

            if ( id != null )
            {
                query = TableNoTracking.Where ( x => x.Id == Guid.Parse ( id ) );

            }
            if ( agentTypesId != null )
            {
                query = query.Where ( x => agentTypesId.Contains ( x.AgentTypeId ) );
            }
            if ( mainPersonId != null )
            {
                query = query.Where ( x => x.MainPersonId == Guid.Parse ( mainPersonId ) );

            }
            if ( agentPersonId != null )
            {
                query = query.Where ( x => x.AgentPersonId == Guid.Parse ( agentPersonId ) );

            }
            foreach ( var item in detailsList )
            {

                if ( item == "AgentType" )
                {
                    query = query.Include ( x => x.AgentType );
                }
                if ( item == "AgentPerson" )
                {
                    query = query.Include ( x => x.AgentPerson );
                }
                if ( item == "MainPerson" )
                {
                    query = query.Include ( x => x.MainPerson );
                }
            }
            if ( mainPersonNationalNo != null )
            {
                query = query.Include ( x => x.MainPerson ).Where ( x => x.MainPerson.NationalNo == mainPersonNationalNo );

            }
            if ( relatedPersonNationalNo != null )
            {
                query = query.Include ( x => x.AgentPerson ).Where ( x => x.AgentPerson.NationalNo == relatedPersonNationalNo );

            }
            var documentPersonRelatedList =await query.ToListAsync(cancellationToken);
            return documentPersonRelatedList;
        }

        /// <summary>
        /// The GetDocumentPersonRelatedById
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <param name="detailsList">The detailsList<see cref="List{string}"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="agentTypesId">The agentTypesId<see cref="List{string}"/></param>
        /// <param name="mainPersonId">The mainPersonId<see cref="string"/></param>
        /// <param name="agentPersonId">The agentPersonId<see cref="string"/></param>
        /// <returns>The <see cref="Task{DocumentPersonRelated}"/></returns>
        public Task<DocumentPersonRelated> GetDocumentPersonRelatedById ( Guid id, string nationalNo, List<string> detailsList, CancellationToken cancellationToken, List<string> agentTypesId = null, string mainPersonId = null, string agentPersonId = null )
        {
            throw new NotImplementedException ();
        }
    }
}
