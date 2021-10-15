using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the command of todoItems
    /// Tranform Dto to Entity
    /// </summary>
    public class TodoItemCommandManager : ITodoItemCommandManager
    {
        public TodoItemCommandManager(ITodoItemCommand todoItemCommand, ITodoItemQuery todoItemQuery,IMapper mapper)
        {
            _todoItemCommand = todoItemCommand;
            _todoItemQuery = todoItemQuery;
            _mapper = mapper;
        }
        private readonly ITodoItemCommand _todoItemCommand;
        ITodoItemQuery _todoItemQuery;
        private readonly IMapper _mapper;
        /// <summary>
        /// To add Item into database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Add(Item item)
        {
            return await _todoItemCommand.Add(_mapper.Map<ToDoItem>(item));
        }
        /// <summary>
        /// To delete item from database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Delete(Item item)
        {
            return await _todoItemCommand.Delete(_mapper.Map<ToDoItem>(item));
        }
        /// <summary>
        /// To delete item from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Operation result</returns>
        public async Task<int> DeletebyId(int id)
        {
            return await _todoItemCommand.DeletebyId(id);
        }
        /// <summary>
        /// To update item 
        /// </summary>
        /// <param name="item">Object of item</param>
        /// <returns>Operation result</returns>
        public async Task<int> Update(Item item)
        {
            return await _todoItemCommand.Update(_mapper.Map<ToDoItem>(item));
        }

        /// <summary>
        /// to update label to item
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="labelId"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Updatelabel(int itemId, int labelId)
        {
            var item = await _todoItemQuery.GetbyId(itemId);
            if (item == null)
            {
                return 0;
            }
            else
            {
                item.LabelId = labelId;
                return await _todoItemCommand.Update(item);
            }
                
        }
    }
}
