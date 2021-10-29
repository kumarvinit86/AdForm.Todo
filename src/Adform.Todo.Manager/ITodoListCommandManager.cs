using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoListCommandManager
    {
        Task<int> Add(TodoList itemList);
        Task<int> Update(TodoList itemList);
        Task<int> Delete(TodoList itemList);
        Task<int> DeletebyId(int id,int userId);
        Task<int> Updatelabel(int itemId, int labelId, int userId);
        Task<int> DeleteRange(List<TodoList> lists);
    }
}
