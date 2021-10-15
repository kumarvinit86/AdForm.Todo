using Adform.Todo.Dto;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface IUserCommandManager
    {
        Task<int> Add(AppUser user);
    }
}
