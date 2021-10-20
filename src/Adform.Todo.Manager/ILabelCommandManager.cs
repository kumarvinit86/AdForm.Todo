using Adform.Todo.Dto;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ILabelCommandManager
    {
        Task<int> Add(Label label);
        Task<int> DeletebyId(int id,int userId);
    }
}
