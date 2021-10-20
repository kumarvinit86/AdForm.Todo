using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService
{
    public interface ITodoItemCommand
    {
		Task<int> Add(ToDoItem toDoItem);
		Task<int> Update(ToDoItem toDoItem);
		Task<int> Delete(ToDoItem toDoItem);
		Task<int> DeleteRange(List<ToDoItem> items);
		Task<int> DeletebyId(int id,int userId);
	}
}
