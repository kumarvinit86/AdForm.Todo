using Adform.Todo.Database;
using Adform.Todo.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.DomainService.Default
{
    /// <summary>
    /// To operate the command of todoItems
    /// </summary>
    public class TodoItemCommand : ITodoItemCommand
    {
        public TodoItemCommand(ICommandRepository<TodoItem> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        private readonly ICommandRepository<TodoItem> _commandRepository;
        /// <summary>
        /// To add Item into database
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Add(TodoItem toDoItem)
        {
            return await _commandRepository.Add(toDoItem);
        }
        /// <summary>
        /// To delete item from database
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Delete(TodoItem toDoItem)
        {
            return await _commandRepository.Remove(toDoItem);
        }
        /// <summary>
        /// To delete item from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Operation result</returns>
        public async Task<int> DeletebyId(int id, int userId)
        {
            var data = _commandRepository.Entities.Where(x => x.Id==id && x.UserId==userId).FirstOrDefault();
            if (data != null)
               return await _commandRepository.Remove(data);
            else { return 0; }
        }
        /// <summary>
        /// To update item 
        /// </summary>
        /// <param name="toDoItem">Object of item</param>
        /// <returns>Operation result</returns>
        public async Task<int> Update(TodoItem toDoItem)
        {
            return await _commandRepository.Update(toDoItem);
        }

        public async Task<int> DeleteRange(List<TodoItem> items)
        {
            return await _commandRepository.RemoveRange(items);
        }
    }
}
