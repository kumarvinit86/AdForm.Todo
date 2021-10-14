using Adform.Todo.Model.Entity;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ILableCommand
	{
		Task<int> Add(TodoLable toDoItem);
		Task<int> DeletebyId(int id);
	}
}
