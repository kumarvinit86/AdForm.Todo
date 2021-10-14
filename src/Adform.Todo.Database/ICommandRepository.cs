using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Database
{
    public interface ICommandRepository<TEntity> : IDisposable where TEntity : class
	{
		DbContext TodoDatabase { get; set; }
		DbSet<TEntity> Entities { get; set; }
		Task<int> Add(TEntity entity);

		Task<int> AddRange(List<TEntity> entities);

		Task<int> Remove(TEntity entity);

		Task<int> RemoveRange(List<TEntity> entities);

		Task<int> Update(TEntity entity);

		Task<int> UpdateRange(List<TEntity> entities);

		Task<int> CommandByQuery(string query);

	}
}
