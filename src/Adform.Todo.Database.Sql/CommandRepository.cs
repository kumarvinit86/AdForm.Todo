using Adform.Todo.Database.Sql.DataBaseContext;
using Adform.Todo.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Database.Sql
{
    /// <summary>
    /// Generic Repository implementation for Commands.
    /// </summary>
    /// <typeparam name="TEntity">Template parameter to initialize the repository.</typeparam>
    public class CommandRepository<TEntity> : ICommandRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Constructor to initialize Entity by TEntity.
        /// Initializing TodoDbConext by passing DatabaseConnection.
        /// </summary>
        /// <param name="databaseConnection"></param>
        public CommandRepository(DatabaseConnection databaseConnection)
        {
            TodoDatabase = new TodoDbContext(databaseConnection);
            Entities = TodoDatabase.Set<TEntity>();
        }

        public DbContext TodoDatabase { get; set; }
        public DbSet<TEntity> Entities { get; set; }

        /// <summary>
        /// Generic method to add entity.
        /// </summary>
        /// <param name="entity">Tamplate parameter to initialize db entity.</param>
        /// <returns>Task integer value</returns>
        public async Task<int> Add(TEntity entity)
        {
            Entities.Add(entity);
            return await TodoDatabase.SaveChangesAsync();
        }
        /// <summary>
        /// Generic method to add multiple entity.
        /// </summary>
        /// <param name="entities">Template parameter to initialize db entity.</param>
        /// <returns>Task integer value</returns>
        public async Task<int> AddRange(List<TEntity> entities)
        {
            Entities.AddRange(entities);
            return await TodoDatabase.SaveChangesAsync();
        }

        /// <summary>
        /// Generic method to remove entity.
        /// </summary>
        /// <param name="entity">Template parameter to initialize db entity.</param>
        /// <returns>Task integer value</returns>
        public async Task<int> Remove(TEntity entity)
        {
            Entities.Remove(entity);
            return await TodoDatabase.SaveChangesAsync();
        }

        /// <summary>
        /// Generic method to remove multiple entity.
        /// </summary>
        /// <param name="entities">Template parameter to initialize db entity.</param>
        /// <returns>Task integer value</returns>
        public async Task<int> RemoveRange(List<TEntity> entities)
        {
            Entities.RemoveRange(entities);
            return await TodoDatabase.SaveChangesAsync();
        }

        /// <summary>
        /// Generic method to update entity.
        /// </summary>
        /// <param name="entity">Template parameter to initialize db entity.</param>
        /// <returns>Task integer value</returns>
        public async Task<int> Update(TEntity entity)
        {
            try
            {
                Entities.Update(entity);
                return await TodoDatabase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Generic method to update range entity.
        /// </summary>
        /// <param name="entities">Template parameter to initialize db entity.</param>
        /// <returns>Task integer value</returns>
        public async Task<int> UpdateRange(List<TEntity> entities)
        {
            Entities.UpdateRange(entities);
            return await TodoDatabase.SaveChangesAsync();
        }

        /// <summary>
        /// Generic method to execute sql proc or query.
        /// </summary>
        /// <param name="query">To use Procedure or in-line query</param>
        /// <returns>Task integer value</returns>
        public async Task<int> CommandByQuery(string query)
        {
            Entities.FromSqlRaw(query);
            return await TodoDatabase.SaveChangesAsync();
        }
    }
}