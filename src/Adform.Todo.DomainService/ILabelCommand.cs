using Adform.Todo.Model.Entity;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ILabelCommand
	{
		Task<int> Add(TodoLabel toDoItem);
		Task<int> DeletebyId(int id);
	}
}
