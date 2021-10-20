using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the command of todoItems
    /// Tranform Dto to Entity
    /// </summary>
    public class TodoItemCommandManager : ITodoItemCommandManager
    {
        public TodoItemCommandManager(ITodoItemCommand todoItemCommand,
            ITodoItemQuery todoItemQuery,
            IMapper mapper,
            ILabelQueryManager labelQueryManager)

        {
            _todoItemCommand = todoItemCommand;
            _todoItemQuery = todoItemQuery;
            _mapper = mapper;
            _labelQueryManager = labelQueryManager;
        }
        private readonly ITodoItemCommand _todoItemCommand;
        private readonly ITodoItemQuery _todoItemQuery;
        private readonly IMapper _mapper;
        private readonly ILabelQueryManager _labelQueryManager;
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
        public async Task<int> DeletebyId(int id,int userId)
        {
            return await _todoItemCommand.DeletebyId(id,userId);
        }
        /// <summary>
        /// To update item 
        /// </summary>
        /// <param name="item">Object of item</param>
        /// <returns>Operation result</returns>
        public async Task<int> Update(Item item)
        {
            var todoItem = _mapper.Map<ToDoItem>(item);
            var data = await _todoItemQuery.GetbyId(todoItem.Id, todoItem.UserId ?? default);
            if (data == null)
            {
                return 0;
            }
            else
            {
                var label = (await _labelQueryManager.Get()).Where(x => x.Name == todoItem.Label.Name).FirstOrDefault();
                if (label != null)
                {
                    data.LabelId = label.Id;
                }
                data.Name = todoItem.Name;
                data.UpdatedDate = System.DateTime.Now;
                return await _todoItemCommand.Update(data);
            }
        }

        /// <summary>
        /// to update label to item
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="labelId"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Updatelabel(int itemId, int labelId, int userId)
        {
            var item = await _todoItemQuery.GetbyId(itemId, userId);
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

        public async Task<int> DeleteRange(List<ToDoItem> items)
        {
            return await _todoItemCommand.DeleteRange(items);
        }
    }
}
