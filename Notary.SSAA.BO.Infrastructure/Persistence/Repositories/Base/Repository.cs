using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Infrastructure.Contexts;
using System.Linq.Expressions;
using Pluralize.NET;
using Notary.SSAA.BO.Utilities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public readonly ApplicationContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(ApplicationContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); // City => Cities

        }

        #region Async Method
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await DbContext.Set<TEntity>().Where(expression).ToListAsync(cancellationToken);
        }
        public async Task<List<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await DbContext.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync(cancellationToken);
        }
        public async virtual Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await Entities.FindAsync(ids, cancellationToken);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(expression, cancellationToken);
        }
        public virtual async Task<TEntity> GetNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await DbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
        }


        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await DbContext.Set<TEntity>().AnyAsync(expression, cancellationToken);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        #endregion

        #region Sync Methods
        public virtual bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return DbContext.Set<TEntity>().Any(expression);
        }
        public virtual TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return DbContext.Set<TEntity>().FirstOrDefault(expression);
        }
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return DbContext.Set<TEntity>().Where(expression).ToList();
        }
        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            //Entities.Update(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(bool asNoTracking, CancellationToken cancellationToken)
        {
            return await (asNoTracking ? TableNoTracking : Entities)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetSomeByAsync<T>(IList<T> searchPhrases, CancellationToken cancellationToken, string propName = "Id")
        {
            if (searchPhrases?.Count > 0)
            {
                var query = $"SELECT * FROM {SchemaConstant.DefaultSchema}.{new Pluralizer().Pluralize(typeof(TEntity).Name)} WHERE ";
                for (int i = 0; i < searchPhrases.Count; i++)
                    query += $"{propName}='{searchPhrases[i]}'" + (i + 1 < searchPhrases.Count ? " OR " : "");

                return await Entities.FromSqlRaw(query).ToListAsync(cancellationToken);
            }
            return new List<TEntity>();
        }
        #endregion

        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }

        public virtual void Modified(TEntity originalEntity, object modifiedEntity)
        {
            Assert.NotNull(originalEntity, nameof(originalEntity));
            Assert.NotNull(modifiedEntity, nameof(modifiedEntity));

            var sourceType = typeof(TEntity);
            var properties = sourceType.GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    var value = property.GetValue(originalEntity);
                    var propertyType = property.PropertyType;

                    // Handle non-collection properties
                    if (!propertyType.IsClass || !propertyType.IsGenericType)
                    {
                        property.SetValue(originalEntity, value);
                        continue;
                    }

                    // Handle collection properties
                    var targetCollection = (ICollection<object>)property.GetValue(originalEntity);
                    targetCollection.Clear(); // Clear existing items

                    var sourceCollection = (ICollection<TEntity>)value;
                    foreach (var item in sourceCollection)
                    {
                        // Create a new instance for each item in the collection
                        var newItem = Activator.CreateInstance(item.GetType());
                        // Recursively copy properties of each item (if necessary)
                        Modified(item, newItem);
                        targetCollection.Add(newItem);
                    }
                }
            }


        }
        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);

            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }



        #endregion
        public bool IsNew(TEntity entity)
        {
            return DbContext.Entry(entity).State == EntityState.Added;
        }
        public bool IsDirty(TEntity entity)
        {
            return DbContext.Entry(entity).State == EntityState.Modified;
        }
        public bool IsDelete(TEntity entity)
        {
            return DbContext.Entry(entity).State == EntityState.Deleted;
        }
        public virtual async Task UpdateAfterAddToContext(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            var id = entity.GetType().GetProperty("Id").GetValue(entity);
            var m = Entities.Find(entity.GetType().GetProperty("Id").GetValue(entity));
            Entities.Remove(Entities.Find(entity.GetType().GetProperty("Id").GetValue(entity)));
            m = Entities.Find(entity.GetType().GetProperty("Id").GetValue(entity));

            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        public virtual async Task SaveChanges(CancellationToken cancellationToken)
        {
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
