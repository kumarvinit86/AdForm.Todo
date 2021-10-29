using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ITodoListCommand
    {
        Task<int> Add(TodoList toDoItem);
        Task<int> Update(TodoList toDoItem);
        Task<int> Delete(TodoList toDoItem);
        Task<int> DeletebyId(int id, int userId);
        Task<int> DeleteRange(List<TodoList> list);
    }
}
