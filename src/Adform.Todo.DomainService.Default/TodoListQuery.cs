using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService.Default
{
    /// <summary>
    /// To operate the Query Action of list.
    /// </summary>
    public class TodoListQuery : ITodoListQuery
    {
        public TodoListQuery(IQueryRepository<ToDoList> queryRepository)
        {
            _queryRepository = queryRepository;
        }
        private readonly IQueryRepository<ToDoList> _queryRepository;
        /// <summary>
        /// fetch the list of todolist of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of item</returns>
        public async Task<List<ToDoList>> Get(int userId)
        {
            return await Task.Run(() => { return _queryRepository.Entities.Where(x => x.UserId == userId).AsQueryable().ToList(); }); 
        }

        /// <summary>
        /// fetch todolist by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ToDoList> GetbyId(int id,int userId)
        {
            return await Task.Run(() => { return _queryRepository.Entities.Where(x => x.UserId == userId && x.Id == id).FirstOrDefault(); });
        }
    }
}
