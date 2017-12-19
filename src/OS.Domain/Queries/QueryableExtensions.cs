using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OS.Domain.Queries
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IList<SortCriterium> criteriums)
        {
            var expression = source.Expression;

            if (criteriums.Count == 0)
            {
                return source;
            }

            for (var i = 0; i < criteriums.Count; i++)
            {
                var @param = Expression.Parameter(typeof(T), "x");      // x =>
                var @property = criteriums[i].GetProperty(@param);      // x => x.Property

                var method = criteriums[i].Ascending ?
                    i == 0 ? nameof(Queryable.OrderBy) : nameof(Queryable.ThenBy) :
                    i == 0 ? nameof(Queryable.OrderByDescending) : nameof(Queryable.ThenByDescending);

                var @typeParams = new[] { source.ElementType, property.Type };
                var @expressionParams = new[] { expression, Expression.Quote(Expression.Lambda(@property, @param)) };

                expression = Expression.Call(typeof(Queryable), method, @typeParams, expressionParams);
            }

            return source.Provider.CreateQuery<T>(expression);
        }
    }
}