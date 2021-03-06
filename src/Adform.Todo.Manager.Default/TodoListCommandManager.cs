using Adform.Todo.DomainService;
using Adform.Todo.Model.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the commands Action of todo list.
    /// Transform Dto to Entity
    /// </summary>
    public class TodoListCommandManager : ITodoListCommandManager
    {
        public TodoListCommandManager(ITodoListCommand todoListCommand, 
            ITodoListQuery todoListQuery, 
            IMapper mapper, 
            ILabelQueryManager labelQueryManager)
        {
            _todoListCommand = todoListCommand;
            _todoListQuery = todoListQuery;
            _mapper = mapper;
            _labelQueryManager = labelQueryManager;
        }
        private readonly ITodoListCommand _todoListCommand;
        private readonly ITodoListQuery _todoListQuery;
        private readonly IMapper _mapper;
        private readonly ILabelQueryManager _labelQueryManager;

        /// <summary>
        /// to add list into the database
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns>Operation result</returns>
        public Task<int> Add(TodoList itemList)
        {
            var data = _mapper.Map<TodoList>(itemList);
            data.CreatedDate = DateTime.Now;
            data.UpdatedDate = DateTime.Now;
            return _todoListCommand.Add(data);
        }
        /// <summary>
        /// to delete list from database
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns>Operation result</returns>
        public Task<int> Delete(TodoList itemList)
        {
            return _todoListCommand.Delete(_mapper.Map<TodoList>(itemList));
        }
        /// <summary>
        /// to delete list by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Operation result</returns>
        public Task<int> DeletebyId(int id,int userId)
        {
            return _todoListCommand.DeletebyId(id,userId);
        }
        /// <summary>
        /// to update the list
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Update(TodoList itemList)
        {
            var data = await _todoListQuery.GetbyId(itemList.Id,itemList.UserId??default);
            if (data == null)
            {
                return 0;
            }
            else
            {
                var label = (await _labelQueryManager.Get()).Where(x => x.Name == itemList.Label.Name).FirstOrDefault();
                if (label != null)
                {
                    data.LabelId = label.Id;
                }
                data.Name = itemList.Name;
                data.UpdatedDate = DateTime.Now;
                return await _todoListCommand.Update(data);
            }
        }
        /// <summary>
        /// to update the label of list
        /// </summary>
        /// <param name="itemListId"></param>
        /// <param name="labelId"></param>
        /// <returns>Operation result</returns>
        public async Task<int> Updatelabel(int itemListId, int labelId, int userId)
        {
            var list = await _todoListQuery.GetbyId(itemListId, userId);
            if (list == null)
            {
                return 0;
            }
            else
            {
                list.LabelId = labelId;
                list.UpdatedDate = DateTime.Now;
                return await _todoListCommand.Update(list);
            }

        }

        public async Task<int> DeleteRange(List<TodoList> lists)
        {
            return await _todoListCommand.DeleteRange(lists);
        }
    }
}
