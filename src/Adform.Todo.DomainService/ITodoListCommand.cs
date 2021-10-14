using Adform.Todo.Model.Entity;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ITodoListCommand
    {
        Task<int> Add(ToDoList toDoItem);
        Task<int> Update(ToDoList toDoItem);
        Task<int> Delete(ToDoList toDoItem);
        Task<int> DeletebyId(int id);
    }
}
