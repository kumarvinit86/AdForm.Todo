using Adform.Todo.Dto;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoItemCommandManager
    {
        Task<int> Add(Item item);
        Task<int> Update(Item item);
        Task<int> Delete(Item item);
        Task<int> DeletebyId(int id);
        Task<int> Updatelabel(int itemId, int labelId, int userId);
    }
}
