using Adform.Todo.Dto;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoListCommandManager
    {
        Task<int> Add(ItemList itemList);
        Task<int> Update(ItemList itemList);
        Task<int> Delete(ItemList itemList);
        Task<int> DeletebyId(int id);
        Task<int> UpdateLable(int itemId, int lableId);
    }
}
