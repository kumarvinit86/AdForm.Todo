using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Adform.Todo.DomainService.Default
{
    /// <summary>
    /// To operate the Query Action of item.
    /// </summary>
    public class TodoItemQuery : ITodoItemQuery
    {
        public TodoItemQuery(IQueryRepository<TodoItem> queryRepository)
        {
            _queryRepository = queryRepository;
        }
        private readonly IQueryRepository<TodoItem> _queryRepository;

        /// <summary>
        /// to fetch the list of item of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of item</returns>
        public async Task<List<TodoItem>> Get(int userId)
        {
            string[] includeParam = { "Label" };
            return (await _queryRepository.FillEntities(includeParam)).Where(x => x.UserId == userId).ToList();

        }

        /// <summary>
        /// fetch item by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TodoItem> GetbyId(int Id, int userId)
        {
            string[] includeParam = { "Label" };
            return (await _queryRepository.FillEntities(includeParam)).Where(x => x.UserId == userId && x.Id == Id).FirstOrDefault();
        }
    }
}
