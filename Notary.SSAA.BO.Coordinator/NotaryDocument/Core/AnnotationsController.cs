namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core
{
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="AnnotationsController" />
    /// </summary>
    public class AnnotationsController
    {
        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private readonly IDocumentRepository documentRepository;

        /// <summary>
        /// Defines the documentPersonRepository
        /// </summary>
        private readonly IDocumentPersonRepository documentPersonRepository;

        /// <summary>
        /// Defines the documentPersonRelatedRepository
        /// </summary>
        private readonly IDocumentPersonRelatedRepository documentPersonRelatedRepository;

        /// <summary>
        /// Defines the userService
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Defines the annotaionGenerator
        /// </summary>
        internal AnnotationsGenerator annotaionGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnotationsController"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_documentPersonRepository">The _documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="_documentPersonRelatedRepository">The _documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        /// <param name="_annotaionGenerator">The _annotaionGenerator<see cref="AnnotationsGenerator"/></param>
        public AnnotationsController ( IDocumentRepository _documentRepository, IUserService _userService,
            IDocumentPersonRepository _documentPersonRepository, IDocumentPersonRelatedRepository _documentPersonRelatedRepository,
            AnnotationsGenerator _annotaionGenerator )
        {
            documentRepository = _documentRepository;
            userService = _userService;
            documentPersonRepository = _documentPersonRepository;
            documentPersonRelatedRepository = _documentPersonRelatedRepository;
            annotaionGenerator = _annotaionGenerator;
        }

        /// <summary>
        /// The CollectAnnotations
        /// </summary>
        /// <param name="classifyNo">The classifyNo<see cref="string"/></param>
        /// <param name="scriptoriumID">The scriptoriumID<see cref="string"/></param>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{AnnotationPack}?}"/></returns>
        public async Task<List<AnnotationPack>?> CollectAnnotations ( string classifyNo, string scriptoriumID, string nationalNo )
        {
            List<AnnotationPack> annotationsCollection = new List<AnnotationPack>{ };
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            try
            {
                annotationsCollection = await annotaionGenerator.GenerateAnnotations ( "6", scriptoriumID, new List<string> { classifyNo, nationalNo }, userService.UserApplicationContext.BranchAccess.BranchId );
                if ( annotationsCollection != null && annotationsCollection.Count > 0 )
                    annotationsCollection = annotationsCollection.OrderBy ( q => q.RelatedDocLastModifyDateTime ).ToList ();
            }
            catch ( Exception )
            {
            }

            return annotationsCollection;
        }
    }
}
