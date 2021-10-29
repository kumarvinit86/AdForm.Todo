using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adform.Todo.Database.Sql
{
    public static class IncludeExtensionMethod
    {
        internal static IQueryable<TEntity> IncludeMultiple<TEntity>(
            this IQueryable<TEntity> query,
            params string[] includes) where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        internal static DbSet<TEntity> IncludeMultiple<TEntity>(
            this DbSet<TEntity> query,
            params string[] includes) where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => (DbSet<TEntity>)current.Include(include));
            }
            return query;
        }
    }
}
