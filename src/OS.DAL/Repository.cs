using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace OS.DAL
{
    public class Repository<TContext, TModel, TId> : BaseRepository<TContext>
        where TContext : DbContext
        where TModel : Entity<TId>
        where TId : struct, IEquatable<TId>, IComparable<TId>
    {
        public Repository(TContext context) : base(context)
        {
        }

        protected virtual IQueryable<TModel> Query => Context.Set<TModel>().AsNoTracking().AsQueryable();

        protected T Get<T>(TId id, Func<TModel, T> mapper)
        {
            var model = Query.FirstOrDefault(EqualsPredicate<TModel, TId>(id));
            return model == null ? default(T) : mapper(model);
        }

        protected virtual TId Insert(TModel model, Action<TModel, EntityEntry> updateAction = null)
        {
            var added = Context.Add(model);

            OnInsert(updateAction);

            return added.Entity.Id;
        }

        private void OnInsert(Action<TModel, EntityEntry> updateAction)
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
    }
}