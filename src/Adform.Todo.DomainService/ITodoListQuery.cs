using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ITodoListQuery
    {
        Task<List<ToDoList>> Get(int userId);
        Task<ToDoList> GetbyId(int Id);
    }
}
