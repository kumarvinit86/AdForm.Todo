using Adform.Todo.Manager.PageManager;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoListQueryManager : IPageManager<TodoList>
    {
        Task<TodoList> GetbyId(int id, int userId);
        Task<List<TodoList>> Get(int userId);
        Task<List<TodoList>> Get(PagingDataRequest pagingData, int userId);
        Task<TodoList> GetbyIdforPatch(int id, int userId);
    }
}
