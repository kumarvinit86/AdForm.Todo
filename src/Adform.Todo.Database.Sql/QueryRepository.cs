using Adform.Todo.Database.Sql.DataBaseContext;
using Adform.Todo.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Adform.Todo.Database.Sql
{
    /// <summary>
    /// Generic Repository implementation for Query or Retrival.
    /// </summary>
    /// <typeparam name="TEntity">Tamplate parameter to initialize the repository.</typeparam>
    public class QueryRepository<TEntity> : IQueryRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Constructor to initialize Entity by TEntity.
        /// Initializing TodoDbConext by passing DatabaseConnection.
        /// </summary>
        /// <param name="databaseConnection"></param>
        public QueryRepository(DatabaseConnection databaseConnection)
        {
            TodoDatabase = new TodoDbContext(databaseConnection);
            Entities = TodoDatabase.Set<TEntity>();
        }
        private QueryRepository()
        {

        }
        /// <summary>
        /// This property is only to initialize current class for unit test
        /// </summary>
        public static QueryRepository<TEntity> GetInstance { get { return new QueryRepository<TEntity>(); } }
        public DbContext TodoDatabase { get; set; }
        public DbSet<TEntity> Entities { get; set; }
        /// <summary>
        /// Generic method to fetch entity by primary key value.
        /// </summary>
        /// <param name="id">primary key.</param>
        /// <returns>Single Object of Entity.</returns>
        public async Task<TEntity> FindById(int id, string[] includePath = null)
        {
            if (includePath == null)
            {
                return await Entities.FindAsync(id);

            }
            else
            {
                return await Entities.IncludeMultiple(includePath).FindAsync(id);
            }
        }
        /// <summary>
        /// Generic method to fetch all data of entity
        /// </summary>
        /// <returns>All Object of Entity.</returns>

        public async Task<List<TEntity>> FillEntities(string[] includePath = null)
        {
            if (includePath == null)
            {
                return await Entities
                               .AsNoTracking()
                               .AsQueryable()
                               .ToListAsync();
            }
            else
            {
                return await Entities.AsNoTracking()
                                   .AsQueryable()
                                   .IncludeMultiple(includePath)
                                   .ToListAsync();
            }
        }

        /// <summary>
        /// Generic method to execute sql proc or query.
        /// </summary>
        /// <param name="query">To use Procedure or inline query</param>
        /// <returns>>All Object of Entity.</returns>
        public async Task<List<TEntity>> FillEntitiesByQuery(string query)
        {
            return await Entities
                .FromSqlRaw(query)
                .AsNoTracking()
                .AsQueryable()
                .ToListAsync();
        }



    }

}
