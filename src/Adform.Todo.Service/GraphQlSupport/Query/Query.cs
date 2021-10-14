using Adform.Todo.Dto;
using Adform.Todo.Manager;
using Adform.Todo.Model.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Service.GraphQlSupport.Query
{
    /// <summary>
    /// Queries for GraphQL
    /// </summary>
    [Authorize]
    public class Query
    {
        private readonly ILableQueryManager _lableQueryManager;
        private readonly ITodoListQueryManager _todoListQueryManager;
        private readonly ITodoItemQueryManager _todoItemQueryManager;
        private readonly IUserQueryManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        public Query(ILableQueryManager lableQueryManager,
            ITodoListQueryManager todoListQueryManager,
            ITodoItemQueryManager todoItemQueryManager,
            IUserQueryManager userManager)
        {
            _lableQueryManager = lableQueryManager;
            _todoListQueryManager = todoListQueryManager;
            _todoItemQueryManager = todoItemQueryManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Query to get all labels
        /// </summary>
        /// <returns></returns>
        public Task<List<Lable>> GetLabels() 
        { 
            return _lableQueryManager.Get(); 
        }

        /// <summary>
        /// Query to get label by id
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        public Task<Lable> GetLabelById(int labelId) 
        { 
            return _lableQueryManager.GetbyId(labelId);
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
        public async Task<Tuple<List<ItemList>, PagingDataResponse>> GetToDoLists(PagingDataRequest pagingData, int userId) 
        { 
            return await _todoListQueryManager.Get(pagingData,userId); 
        }

        /// <summary>
        /// Query to get all todoitems
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<List<Item>, PagingDataResponse>> GetToDoItems(PagingDataRequest pagingData, int userId) 
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
