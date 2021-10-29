using Adform.Todo.Model.Entity;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface IUserCommandManager
    {
        Task<int> Add(User user);
    }
}
