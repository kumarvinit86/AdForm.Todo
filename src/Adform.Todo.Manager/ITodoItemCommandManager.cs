using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoItemCommandManager
    {
        Task<int> Add(TodoItem item);
        Task<int> Update(TodoItem item);
        Task<int> Delete(TodoItem item);
        Task<int> DeleteRange(List<TodoItem> items);
        Task<int> DeletebyId(int id, int userId);
        Task<int> Updatelabel(int itemId, int labelId, int userId);
        Task<int> UpdateList(int itemId, int listId, int userId);
    }
}
