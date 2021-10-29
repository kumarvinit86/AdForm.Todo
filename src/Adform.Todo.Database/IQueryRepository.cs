using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.Database
{
    public interface IQueryRepository<TEntity> where TEntity : class
	{
		DbContext TodoDatabase { get; set; }
		DbSet<TEntity> Entities { get; set; }
		Task<List<TEntity>> FillEntities(string[] includePath = null);
		Task<List<TEntity>> FillEntitiesByQuery(string query);
		Task<TEntity> FindById(int id, string[] includePath = null);
	}
}
