using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ILableCommandManager
    {
        Task<int> Add(Lable lable);
        Task<int> DeletebyId(int id);
    }
}
