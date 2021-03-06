using Adform.Todo.Dto;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Manager;
using Adform.Todo.Model.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SimpleInjector;
using System.Threading.Tasks;

namespace Adform.Todo.Graphql.Mutation
{

    /// <summary>
    /// Class to hold mutations for GraphQL
    /// </summary>
    public class Mutation
    {
        public Mutation(Container container)
        {
            _labelCommandManager = container.GetInstance<ILabelCommandManager>();
            _todoListCommandManager = container.GetInstance<ITodoListCommandManager>();
            _jsonWebTokenHandler = container.GetInstance<IJsonWebTokenHandler>();
            _httpContextAccessor = container.GetInstance<IHttpContextAccessor>();
            _todoItemCommandManager = container.GetInstance<ITodoItemCommandManager>();
            _mapper = container.GetInstance<IMapper>();
        }

        private readonly ILabelCommandManager _labelCommandManager;
        private readonly ITodoListCommandManager _todoListCommandManager;
        private readonly ITodoItemCommandManager _todoItemCommandManager;
        private readonly IJsonWebTokenHandler _jsonWebTokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;


        /// <summary>
        /// Mutation to add label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public async Task<int> Create(Label label) => await _labelCommandManager.Add(_mapper.Map<TodoLabel>(label));


        /// <summary>
        /// Mutation to delete label by id
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public async Task<int> DeleteLabelbyId(int id)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            return await _labelCommandManager.DeletebyId(id, userId ?? default);
        }

        /// <summary>
        /// Mutation to assign label to item
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public async Task<int> UpdateLabeltoItem(int itemId, int labelId)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            return await _todoItemCommandManager.Updatelabel(itemId, labelId, userId ?? default);
        }

        /// <summary>
        /// Mutation to assign label to item
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        public async Task<int> UpdateListtoItem(int itemId, int listId)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            return await _todoItemCommandManager.UpdateList(itemId, listId, userId ?? default);
        }

        /// <summary>
        /// Mutation to assign label to list
        /// </summary>
        /// <param name="itemListId"></param>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public async Task<int> UpdateLabeltoItemList(int itemListId, int labelId)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            return await _todoListCommandManager.Updatelabel(itemListId, labelId, userId ?? default);
        }

        /// <summary>
        /// Mutation to add todoitem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AddToDoItem(Item item)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            item.UserId = userId ?? default;
            return await _todoItemCommandManager.Add(_mapper.Map<TodoItem>(item));
        }

        /// <summary>
        /// Mutation to delete todoitem by id
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> DeleteToDoItembyId(int id)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            return await _todoItemCommandManager.DeletebyId(id, userId ?? default);
        }

        /// <summary>
        /// Mutation to update todoitem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> UpdateToDoItem(Item item)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            item.UserId = userId ?? default;
            return await _todoItemCommandManager.Update(_mapper.Map<TodoItem>(item));
        }

        /// <summary>
        /// Mutation to add todolist
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public async Task<int> AddToDoList(ItemList itemList)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            itemList.UserId = userId ?? default;
            return await _todoListCommandManager.Add(_mapper.Map<TodoList>(itemList));
        }


        /// <summary>
        /// Mutation to delete todolist by id
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> DeleteToDoListbyId(int id)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            return await _todoListCommandManager.DeletebyId(id, userId ?? default);
        }

        /// <summary>
        /// Mutation to update todolist
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public async Task<int> UpdateToDoList(ItemList itemList)
        {
            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            itemList.UserId = userId ?? default;
            return await _todoListCommandManager.Update(_mapper.Map<TodoList>(itemList));
        }
    }

}
