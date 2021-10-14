using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ITodoItemQuery
    {
        Task<List<ToDoItem>> Get(int userId);
        Task<ToDoItem> GetbyId(int Id);
    }
}
