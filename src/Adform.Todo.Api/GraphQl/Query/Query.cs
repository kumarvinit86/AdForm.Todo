using Adform.Todo.Dto;
using Adform.Todo.Manager;
using Adform.Todo.Model.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Api.GraphQl.Query
{
    /// <summary>
    /// Queries for GraphQL
    /// </summary>
    [Authorize]
    public class Query
    {
        private readonly ILabelQueryManager _labelQueryManager;
        private readonly ITodoListQueryManager _todoListQueryManager;
        private readonly ITodoItemQueryManager _todoItemQueryManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        public Query(ILabelQueryManager labelQueryManager,
            ITodoListQueryManager todoListQueryManager,
            ITodoItemQueryManager todoItemQueryManager)
        {
            _labelQueryManager = labelQueryManager;
            _todoListQueryManager = todoListQueryManager;
            _todoItemQueryManager = todoItemQueryManager;
        }

        /// <summary>
        /// Query to get all labels
        /// </summary>
        /// <returns></returns>
        public Task<List<Label>> GetLabels() 
        { 
            return _labelQueryManager.Get(); 
        }

        /// <summary>
        /// Query to get label by id
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public Task<Label> GetLabelById(int labelId) 
        { 
            return _labelQueryManager.GetbyId(labelId);
        }

        /// <summary>
        /// Query to get todolist by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<ItemList> GetToDoListById(int itemId) 
        {
            return await _todoListQueryManager.GetbyId(itemId); 
        }

        /// <summary>
        /// Query to get all lists
        /// </summary>
        /// <returns></returns>
        public async Task<ItemListPaged> GetToDoLists(PagingDataRequest pagingData, int userId) 
        { 
            return await _todoListQueryManager.Get(pagingData,userId); 
        }

        /// <summary>
        /// Query to get all todoitems
        /// </summary>
        /// <returns></returns>
        public async Task<ItemPaged> GetToDoItems(PagingDataRequest pagingData, int userId) 
        { 
            return await _todoItemQueryManager.Get(pagingData,userId); 
        }

        /// <summary>
        /// Query to get todoitem by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<Item> GetToDoItemById(int itemId) 
        { 
            return await _todoItemQueryManager.GetbyId(itemId); 
        }

    }








}
