using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OS.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleUnintendedQueryableAsEnumerable

namespace OS.DAL
{
    public class Repository<TContext, TEntity, TId> : BaseRepository<TContext>
        where TContext : DbContext
        where TEntity : Entity<TId>, IAggregateRoot
        where TId : struct, IEquatable<TId>, IComparable<TId>
    {
        public Repository(TContext context) : base(context)
        {
        }

        protected virtual IQueryable<TEntity> Query => Context.Set<TEntity>().AsNoTracking().AsQueryable();

        protected long Count(IQueryable<TEntity> q, Query query = null)
        {
            return q.Where(query ?? new Query()).Count();
        }

        protected T Get<T>(TId id, Func<TEntity, T> mapper)
        {
            var entity = Query.FirstOrDefault(EqualsPredicate<TEntity, TId>(id));
            return entity == null ? default(T) : mapper(entity);
        }

        protected QueryResult<T> Get<T>(IQueryable<TEntity> source, Query query, Func<TEntity, T> mapper)
        {
            var unfilteredCount = source.Count();
            var q = source.Where(query);
            var filteredCount = q.Count();

            var items = q.OrderByAndPage(query).Select(mapper).ToList();

            return new QueryResult<T>(items, unfilteredCount, filteredCount);
        }

        #region Insert

        protected virtual TId Insert(TEntity entity, Action<TEntity, EntityEntry> updateAction = null)
        {
            var added = Context.Add(entity);

            OnInsert(updateAction);

            return added.Entity.Id;
        }

        protected virtual IList<TId> Insert(IList<TEntity> entities, Action<TEntity, EntityEntry> updateAction = null)
        {
            Context.AddRange(entities);

            OnInsert(updateAction);

            return entities.Select(x => x.Id).ToList();
        }

        private void OnInsert(Action<TEntity, EntityEntry> updateAction)
        {
            var allModified = Context.ChangeTracker.Entries().Where(r => r.State == EntityState.Modified).ToList();

            UpdateProperties(allModified, updateAction);
            UpdateProperties<ITrackCreationDate>(allModified, (x, entity) =>
            {
                x.CreatedAt = DateTime.UtcNow;
            });
            UpdateProperties<ITrackModificationDate>(allModified, (x, entity) =>
            {
                x.ModifiedAt = DateTime.UtcNow;
            });

            Context.SaveChanges();
        }

        #endregion Insert
    }
}