using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Notary.SSAA.BO.Domain.Abstractions.Base
{

    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        void Add(TEntity entity, bool saveNow = true);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<TEntity> GetNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        void Attach(TEntity entity);
        void Modified(TEntity originalEntity, object modifiedEntity);
        void Delete(TEntity entity, bool saveNow = true);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        void Detach(TEntity entity);
        TEntity GetById(params object[] ids);
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
        void Update(TEntity entity, bool saveNow = true);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task<List<TEntity>> GetAllAsync(bool asNoTracking, CancellationToken cancellationToken);
        Task<List<TEntity>> GetSomeByAsync<T>(IList<T> searchPhrases, CancellationToken cancellationToken, string propName = "Id");
        Task SaveChanges (  CancellationToken cancellationToken);
        Task UpdateAfterAddToContext ( TEntity entity, CancellationToken cancellationToken, bool saveNow = true );

        bool IsNew ( TEntity entity );
        public bool IsDirty ( TEntity entity );
        public bool IsDelete ( TEntity entity );
       

    }

}
