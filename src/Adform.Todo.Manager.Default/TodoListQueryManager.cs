using Adform.Todo.DomainService;
using Adform.Todo.Manager.Default.PageManager;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adform.Todo.Manager.Default
{
    /// <summary>
    /// To orchestrate the Query Action of todo list.
    /// Transform Entity to Dto.
    /// </summary>
    public class TodoListQueryManager : PageManager<TodoList>, ITodoListQueryManager
    {
        public TodoListQueryManager(ITodoListQuery todoListQuery, 
            IMapper mapper, 
            ILabelQueryManager labelQueryManager,
            ITodoItemQueryManager todoItemQueryManager)
        {
            _todoListQuery = todoListQuery;
            _mapper = mapper;
            _labelQueryManager = labelQueryManager;
            _todoItemQueryManager = todoItemQueryManager;

        }

        private readonly ITodoListQuery _todoListQuery;
        private readonly IMapper _mapper;
        private readonly ILabelQueryManager _labelQueryManager;
        private readonly ITodoItemQueryManager _todoItemQueryManager;

        /// <summary>
        /// to fetch the list of todolist of a user
        /// with the pagination.
        /// </summary>
        /// <param name="pagingData">for current/default page data</param>
        /// <param name="userId"></param>
        /// <returns>Tuple of list of todolist and the pagination details</returns>
        public async Task<List<TodoList>> Get(PagingDataRequest pagingDataRequest, int userId)
        {
            var data = (await _todoListQuery.Get(userId));
            pagingRequest = pagingDataRequest;
            var list = Paging(data);
            return list;
        }

        /// <summary>
        /// fetch todolist by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TodoList> GetbyId(int id, int userId)
        {
            var todoList = await _todoListQuery.GetbyId(id, userId);
            return todoList;
        }

        /// <summary>
        /// fetch todolist by id for patch
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TodoList> GetbyIdforPatch(int id, int userId)
        {
            return await GetbyId(id, userId);
        }

            public async Task<List<TodoList>> Get(int userId)
        {
            return await _todoListQuery.Get(userId);
        }
    }
}
