using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Application.QuerableExtensions
{
    public static class QueryableExtension
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, SortrableParams sortableParams)
        {
            var elemType = typeof(TEntity);
            var parameter = Expression.Parameter(elemType);


            var prop = elemType.GetProperty(sortableParams.OrderByField);
            var expPropertryOrField = Expression.Property(parameter, prop.Name);

            var genType = typeof(Func<,>).MakeGenericType(elemType, prop.PropertyType);

            var selector = Expression.Lambda(expPropertryOrField, parameter);

            var orderBy = sortableParams.OrderByDesceding ? "OrderByDescending" : "OrderBy";

            var orderByExpression = Expression.Call(typeof(Queryable),
                orderBy,
                new[] { elemType, prop.PropertyType },
                query.Expression,
                selector
                );
            return (IOrderedQueryable<TEntity>)query.Provider.CreateQuery<TEntity>(orderByExpression);
        }


    }
}
