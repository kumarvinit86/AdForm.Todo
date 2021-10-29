using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ITodoItemCommand
    {
		Task<int> Add(TodoItem toDoItem);
		Task<int> Update(TodoItem toDoItem);
		Task<int> Delete(TodoItem toDoItem);
		Task<int> DeleteRange(List<TodoItem> items);
		Task<int> DeletebyId(int id,int userId);
	}
}
