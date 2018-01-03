using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OS.Core.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable PossibleUnintendedQueryableAsEnumerable

namespace OS.DAL
{
    public class AsyncRepository<TContext, TEntity, TId> : BaseRepository<TContext>
        where TContext : DbContext
        where TEntity : Entity<TId>, IAggregateRoot
        where TId : struct, IEquatable<TId>, IComparable<TId>
    {
        public AsyncRepository(TContext context) : base(context)
        {
        }

        protected virtual IQueryable<TEntity> Query => Context.Set<TEntity>().AsNoTracking().AsQueryable();

        protected Task<long> Count(IQueryable<TEntity> q, Query query = null)
        {
            return q.Where(query ?? new Query()).CountAsync().ContinueWith(x => Convert.ToInt64(x));
        }

        protected Task<T> GetAsync<T>(TId id, Func<TEntity, T> mapper)
        {
            return Query.FirstOrDefaultAsync(EqualsPredicate<TEntity, TId>(id))
                .ContinueWith(x => x == null ? default(T) : mapper(x.Result));
        }

        //protected Task<QueryResult<T>> GetAsync<T>(IQueryable<TEntity> source, Query query, Func<TEntity, T> mapper)
        //{
        //    var unfilteredCount = source.Count();
        //    var q = source.Where(query);
        //    var filteredCount = q.Count();

        //    return q.OrderByAndPage(query)
        //        .Select(mapper)
        //        .AsQueryable()
        //        .ToListAsync().ContinueWith(x => return new QueryResult(x.Result, unfilteredCount, filteredCount));

        //    //return new QueryResult<T>(items, unfilteredCount, filteredCount);
        //}

        #region Insert

        //protected virtual TId Insert(TEntity entity, Action<TEntity, EntityEntry> updateAction = null)
        //{
        //    var added = Context.Add(entity);

        //    OnInsert(updateAction);

        //    return added.Entity.Id;
        //}

        //protected virtual Task<IList<TId>> Insert(IList<TEntity> entities, Action<TEntity, EntityEntry> updateAction = null)
        //{
        //    Context.AddRange(entities);

        //    return OnInsertAsync(updateAction)
        //        .ContinueWith(x => entities.Select(x => x.Id));

        //    //return entities.Select(x => x.Id).ToList();
        //}

        private Task OnInsertAsync(Action<TEntity, EntityEntry> updateAction)
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

            return Context.SaveChangesAsync();
        }

        #endregion Insert
    }
}