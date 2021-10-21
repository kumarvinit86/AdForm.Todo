using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoListCommandManager
    {
        Task<int> Add(ItemListRequest itemList);
        Task<int> Update(ItemListRequest itemList);
        Task<int> Delete(ItemList itemList);
        Task<int> DeletebyId(int id,int userId);
        Task<int> Updatelabel(int itemId, int labelId, int userId);
        Task<int> DeleteRange(List<ToDoList> lists);
    }
}
