using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
    using Notary.SSAA.BO.Infrastructure.Contexts;
    using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading;
    using DocumentType = Notary.SSAA.BO.Domain.Entities.DocumentType;
    using Enumerations = Notary.SSAA.BO.SharedKernel.Enumerations;
    using State = Notary.SSAA.BO.SharedKernel.Constants.State;

    /// <summary>
    /// Defines the <see cref="DocumentRepository" />
    /// </summary>
    internal sealed class SsrDocModifyClassifyNoRepository : Repository<SsrDocModifyClassifyNo>, ISsrDocModifyClassifyNoRepository
    {
        /// <summary>
        /// Defines the dateTimeService
        /// </summary>

        /// <summary>
        /// Initializes a new instance of the <see cref="SsrDocModifyClassifyNoRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="ApplicationContext"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        public SsrDocModifyClassifyNoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// The GetDocumentGridItems
        /// </summary>
        /// <param name="pageIndex">The pageIndex<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="GridSearchInput">The GridSearchInput<see cref="ICollection{SearchData}"/></param>
        /// <param name="GlobalSearch">The GlobalSearch<see cref="string"/></param>
        /// <param name="gridSortInput">The gridSortInput<see cref="SortData"/></param>
        /// <param name="selectedItemsIds">The selectedItemsIds<see cref="IList{Guid}"/></param>
        /// <param name="FieldsNotInFilterSearch">The FieldsNotInFilterSearch<see cref="List{string}"/></param>
        /// <param name="scriptoriumId">The scriptoriumId<see cref="string"/></param>
        /// <param name="extraParams">The extraParams<see cref="DocumentExtraParams"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="isOrderBy">The isOrderBy<see cref="bool"/></param>
        /// <returns>The <see cref="Task{DocumentGrid}"/></returns>
        public async Task<DocumentModifyGrid> GetDocumentModifyGridItems(
            int pageIndex,
            int pageSize,
            ICollection<SearchData> GridSearchInput,
            string GlobalSearch,
            SortData gridSortInput,
            IList<Guid> selectedItemsIds,
            List<string> FieldsNotInFilterSearch,
            string scriptoriumId,
            CancellationToken cancellationToken,
            bool isOrderBy = false)
        {
            DocumentModifyGrid result = new();
            var initialQuery = TableNoTracking.Include(x => x.Document)
            .Where(x => x.ScriptoriumId == scriptoriumId);

            var selectedItemQuery = TableNoTracking.Include(x=>x.Document)
                        .Where(x => x.ScriptoriumId == scriptoriumId && selectedItemsIds.Contains(x.Id))
                        .Select(y => new DocumentModifyGridItem()
                        {
                            DocumentId = y.DocumentId.ToString(),
                            Id = y.Id.ToString(),
                            DocumentType = y.Document.DocumentTypeTitle,
                            DocumentDate = y.Document.DocumentDate,
                            NationalNo = y.Document.NationalNo,
                            ClassifyNoOld = y.ClassifyNoOld.ToString(),
                            ClassifyNoNew = y.ClassifyNoNew.ToString(),
                            WriteInBookDateOld = y.WriteInBookDateOld,
                            WriteInBookDateNew = y.WriteInBookDateNew,
                            RegisterVolumeNoOld = y.RegisterVolumeNoOld,
                            RegisterVolumeNoNew = y.RegisterVolumeNoNew,
                            RegisterPagesNoOld = y.RegisterPapersNoOld,
                            RegisterPagesNoNew = y.RegisterPapersNoNew,
                            ModifyDate = y.ModifyDate
                        });

            string filterQueryString =
                LambdaString<DocumentModifyGridItem, SearchData>.CreateWhereLambdaString(
                    new DocumentModifyGridItem(),
                    GridSearchInput,
                    GlobalSearch,
                    FieldsNotInFilterSearch
                );

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);

            var query = initialQuery.Select(y => new DocumentModifyGridItem()
            {
                DocumentId = y.DocumentId.ToString(),
                Id = y.Id.ToString(),
                DocumentType = y.Document.DocumentTypeTitle,
                DocumentDate = y.Document.DocumentDate,
                NationalNo = y.Document.NationalNo,
                ClassifyNoOld = y.ClassifyNoOld.ToString(),
                ClassifyNoNew = y.ClassifyNoNew.ToString(),
                WriteInBookDateOld = y.WriteInBookDateOld,
                WriteInBookDateNew = y.WriteInBookDateNew,
                RegisterVolumeNoOld = y.RegisterVolumeNoOld,
                RegisterVolumeNoNew = y.RegisterVolumeNoNew,
                RegisterPagesNoOld = y.RegisterPapersNoOld,
                RegisterPagesNoNew = y.RegisterPapersNoNew,
                ModifyDate = y.ModifyDate
            });

            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType}");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }


        public async Task<SsrDocModifyClassifyNo> GetDocumentModify(Guid DocumentModifyId, string ScriptoriumId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
             .AsNoTracking().Include(x => x.Document)
            .Where(x => x.Id == DocumentModifyId && x.ScriptoriumId == ScriptoriumId)
            .FirstOrDefaultAsync(cancellationToken);
        }
    
    }
}
