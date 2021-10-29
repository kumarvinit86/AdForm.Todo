using Adform.Todo.Manager.PageManager;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoItemQueryManager : IPageManager<TodoItem>
    {
        Task<List<TodoItem>> Get(PagingDataRequest pagingData, int userId);
        Task<List<TodoItem>> Get(int userId);
        Task<TodoItem> GetbyId(int Id, int userId);
    }
}
