using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoListQueryManager
    {
        Task<ItemList> GetbyId(int id, int userId);
        Task<List<ToDoList>> Get(int userId);
        Task<ItemListPaged> Get(PagingDataRequest pagingData, int userId);
    }
}
