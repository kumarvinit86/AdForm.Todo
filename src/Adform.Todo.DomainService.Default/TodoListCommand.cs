using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService.Default
{
    /// <summary>
    /// To operate the commands Action of todo list.
    /// </summary>
    public class TodoListCommand : ITodoListCommand
    {
        public TodoListCommand(ICommandRepository<TodoList> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        private readonly ICommandRepository<TodoList> _commandRepository;
        /// <summary>
        /// to add list into the database
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Add(TodoList toDoItem)
        {
            return await _commandRepository.Add(toDoItem);
        }
        /// <summary>
        /// to delete list from database
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Delete(TodoList toDoItem)
        {
            return await _commandRepository.Remove(toDoItem);
        }
        /// <summary>
        /// to delete list by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Operation result</returns>
        public async Task<int> DeletebyId(int id, int userId)
        {
            var data = _commandRepository.Entities.Where(x => x.Id == id && x.UserId == userId).FirstOrDefault();
            if (data != null)
                return await _commandRepository.Remove(data);
            else { return 0; }
        }
        /// <summary>
        /// to update the list
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Update(TodoList toDoItem)
        {
            return await _commandRepository.Update(toDoItem);
        }

        public async Task<int> DeleteRange(List<TodoList> list)
        {
            return await _commandRepository.RemoveRange(list);
        }
    }
}
