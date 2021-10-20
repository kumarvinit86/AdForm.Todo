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
        public TodoItemQuery(IQueryRepository<ToDoItem> queryRepository)
        {
            _queryRepository = queryRepository;
        }
        private readonly IQueryRepository<ToDoItem> _queryRepository;
        
        /// <summary>
        /// to fetch the list of item of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of item</returns>
        public async Task<List<ToDoItem>> Get(int userId)
        {
            return await Task.Run(() => { return _queryRepository.Entities.Where(x => x.UserId == userId).ToList(); });
        }
        
        /// <summary>
        /// fetch item by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ToDoItem> GetbyId(int Id, int userId)
        {
            return await Task.Run(() => { return _queryRepository.Entities.Where(x => x.UserId == userId && x.Id==Id).FirstOrDefault(); });
        }
    }
}
