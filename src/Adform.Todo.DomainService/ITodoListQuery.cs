using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ITodoListQuery
    {
        Task<List<TodoList>> Get(int userId);
        Task<TodoList> GetbyId(int id, int userId);
    }
}
