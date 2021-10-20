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
        public TodoItemCommand(ICommandRepository<ToDoItem> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        private readonly ICommandRepository<ToDoItem> _commandRepository;
        /// <summary>
        /// To add Item into database
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Add(ToDoItem toDoItem)
        {
            return await _commandRepository.Add(toDoItem);
        }
        /// <summary>
        /// To delete item from database
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Delete(ToDoItem toDoItem)
        {
            return await _commandRepository.Remove(toDoItem);
        }
        /// <summary>
        /// To delete item from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Operation result</returns>
        public async Task<int> DeletebyId(int id)
        {
            var data = _commandRepository.Entities.Where(x => x.Id==id).FirstOrDefault();
            if (data != null)
               return await _commandRepository.Remove(data);
            else { return 0; }
        }
        /// <summary>
        /// To update item 
        /// </summary>
        /// <param name="toDoItem">Object of item</param>
        /// <returns>Operation result</returns>
        public async Task<int> Update(ToDoItem toDoItem)
        {
            return await _commandRepository.Update(toDoItem);
        }

        public async Task<int> DeleteRange(List<ToDoItem> items)
        {
            return await _commandRepository.RemoveRange(items);
        }
    }
}
