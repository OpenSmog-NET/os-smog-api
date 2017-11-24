using System;
using System.Linq;
using System.Linq.Expressions;

namespace OS.DAL
{
    public abstract class QueryBase<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }

        public T SatisfyingElementFrom(IQueryable<T> candidates)
        {
            return candidates.Single(Criteria);
        }

        public IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            return candidates.Where(Criteria).AsQueryable();
        }
    }
}