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
    /// To orchestrate the Query Action of item.
    /// Transform Entity to Dto.
    /// </summary>
    public class TodoItemQueryManager : PageManager<TodoItem>, ITodoItemQueryManager
    {
        public TodoItemQueryManager(ITodoItemQuery todoItemQuery, ILabelQueryManager labelQueryManager)
        {
            _todoItemQuery = todoItemQuery;
            _labelQueryManager = labelQueryManager;
        }
        private readonly ITodoItemQuery _todoItemQuery;
        private readonly ILabelQueryManager _labelQueryManager;

        /// <summary>
        /// to fetch the list of item of a user
        /// with the pagination.
        /// </summary>
        /// <param name="pagingData">for current/default page data</param>
        /// <param name="userId"></param>
        /// <returns>Tuple of list of item and the pagination details</returns>
        public async Task<List<TodoItem>> Get(PagingDataRequest pagingDataRequest, int userId)
        {
            var data = await _todoItemQuery.Get(userId);
            pagingRequest = pagingDataRequest;
            var items = Paging(data);
            return items;
        }

        public async Task<List<TodoItem>> Get(int userId)
        {
            return await _todoItemQuery.Get(userId);
        }

        /// <summary>
        /// fetch item by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TodoItem> GetbyId(int Id, int userId)
        {
            return (await Get(userId)).FirstOrDefault();          
        }
    }
}
