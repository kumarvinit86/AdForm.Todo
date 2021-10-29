using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ITodoItemQuery
    {
        Task<List<TodoItem>> Get(int userId);
        Task<TodoItem> GetbyId(int Id, int userId);
    }
}
