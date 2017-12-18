﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OS.DAL
{
    public abstract class BaseRepository<TContext>
        where TContext : DbContext
    {
        protected TContext Context { get; }

        protected BaseRepository(TContext context)
        {
            Context = context;
        }

        protected static Expression<Func<TEntity, bool>> EqualsPredicate<TEntity, TId>(TId id)
            where TEntity : Entity<TId>
            where TId : struct, IEquatable<TId>, IComparable<TId>
        {
            Expression<Func<TEntity, TId>> selector = (x) => x.Id;
            Expression<Func<TId>> closure = () => id;
            return Expression
                .Lambda<Func<TEntity, bool>>(
                    Expression.Equal(selector.Body, closure.Body), selector.Parameters);
        }

        protected static void UpdateProperties<TType>(List<EntityEntry> entities, Action<TType, EntityEntry> action)
            where TType : class
        {
            entities.ForEach(e =>
            {
                if (e.Entity is TType item)
                {
                    action?.Invoke(item, e);
                }
            });
        }
    }
}