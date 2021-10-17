using Adform.Todo.DomainService;
using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the commands Action of todo list.
    /// Tranform Dto to Entity
    /// </summary>
    public class TodoListCommandManager : ITodoListCommandManager
    {
        public TodoListCommandManager(ITodoListCommand todoListCommand, ITodoListQuery todoListQuery, IMapper mapper)
        {
            _todoListCommand = todoListCommand;
            _todoListQuery = todoListQuery;
            _mapper = mapper;
        }
        private readonly ITodoListCommand _todoListCommand;
        private readonly ITodoListQuery _todoListQuery;
        private readonly IMapper _mapper;

        /// <summary>
        /// to add list into the database
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns>Operation result</returns>
        public Task<int> Add(ItemList itemList)
        {
            return _todoListCommand.Add(_mapper.Map<ToDoList>(itemList));
        }
        /// <summary>
        /// to delete list from database
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns>Operation result</returns>
        public Task<int> Delete(ItemList itemList)
        {
            return _todoListCommand.Delete(_mapper.Map<ToDoList>(itemList));
        }
        /// <summary>
        /// to delete list by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Operation result</returns>
        public Task<int> DeletebyId(int id)
        {
            return _todoListCommand.DeletebyId(id);
        }
        /// <summary>
        /// to update the list
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns>Operation result</returns>
        public Task<int> Update(ItemList itemList)
        {
            return _todoListCommand.Update(_mapper.Map<ToDoList>(itemList));
        }
        /// <summary>
        /// to update the label of list
        /// </summary>
        /// <param name="itemListId"></param>
        /// <param name="labelId"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Updatelabel(int itemListId, int labelId)
        {
            var list = await _todoListQuery.GetbyId(itemListId);
            if (list == null)
            {
                return 0;
            }
            else
            {
                list.LabelId = labelId;
                return await _todoListCommand.Update(list);
            }

        }
    }
}
