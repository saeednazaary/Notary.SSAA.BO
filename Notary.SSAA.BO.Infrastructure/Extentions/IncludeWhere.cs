using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Infrastructure.Extentions
{
    
        public static class QueryableExtensions
        {
        public static IQueryable<TEntity> IncludeWhere<TEntity, TProperty> (
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, ICollection<TProperty>>> navigationPropertyPath,
            Expression<Func<TProperty, bool>> filter )
            where TEntity : class
            where TProperty : class
        {
            // EF Core 5+ supports filtered includes directly
            return source.Include ( CombineExpressions ( navigationPropertyPath, filter ) );
        }

        private static Expression<Func<TEntity, IEnumerable<TProperty>>> CombineExpressions<TEntity, TProperty> (
            Expression<Func<TEntity, ICollection<TProperty>>> navigationPropertyPath,
            Expression<Func<TProperty, bool>> filter )
            where TEntity : class
            where TProperty : class
        {
            var param = navigationPropertyPath.Parameters[0];
            var navigationBody = navigationPropertyPath.Body;

            // Create the Where method call
            var whereMethod = typeof(Enumerable).GetMethods()
                .First(m => m.Name == "Where" &&
                            m.GetParameters().Length == 2 &&
                            m.GetParameters()[1].ParameterType.GetGenericArguments().Length == 2)
                .MakeGenericMethod(typeof(TProperty));

            var whereCall = Expression.Call(whereMethod, navigationBody, filter);

            return Expression.Lambda<Func<TEntity, IEnumerable<TProperty>>> ( whereCall, param );
        }
    }
    
}
