using Adform.Todo.Model.Entity;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface IUserCommand
    {
		Task<int> Add(User user);
		Task<int> Update(User user);
		Task<int> Delete(User user);
	}
}
