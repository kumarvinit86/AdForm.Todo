using Adform.Todo.Model.Entity;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ILabelCommandManager
    {
        Task<int> Add(TodoLabel label);
        Task<int> DeletebyId(int id,int userId);
    }
}
