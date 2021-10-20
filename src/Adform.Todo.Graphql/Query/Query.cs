using Adform.Todo.Dto;
using Adform.Todo.Manager;
using System.Linq;
using SimpleInjector;
using Microsoft.AspNetCore.Mvc;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Model.Models;
using Microsoft.AspNetCore.Http;

namespace Adform.Todo.GraphQl.Query
{
    /// <summary>
    /// Queries for GraphQL
    /// </summary>
    public class Query
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        public Query(Container container)
        {
            _labelQueryManager = container.GetInstance<ILabelQueryManager>();
            _todoItemQueryManager = container.GetInstance<ITodoItemQueryManager>();
            _jsonWebTokenHandler = container.GetInstance<IJsonWebTokenHandler>();
            _httpContextAccessor = container.GetInstance<IHttpContextAccessor>();
            _todoListQueryManager = container.GetInstance<ITodoListQueryManager>();
        }

        private readonly ILabelQueryManager _labelQueryManager;
        private readonly ITodoItemQueryManager _todoItemQueryManager;
        private readonly IJsonWebTokenHandler _jsonWebTokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITodoListQueryManager _todoListQueryManager;
        /// <summary>
        /// Query to get all labels
        /// </summary>
        /// <returns></returns>
        public IQueryable<Label> GetLabels() => _labelQueryManager.Get().Result.AsQueryable();

        /// <summary>
        /// Query to get label by id
        /// </summary>
        /// <returns></returns>
        public Label GetLabelsbyId(int id) => _labelQueryManager.GetbyId(id).Result;


        /// <summary>
        /// Query to get all Item
        /// </summary>
        /// <returns></returns>
        public ItemPaged GetItem(PagingDataRequest pagingDataRequest)
        {

            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            var tupleResult = _todoItemQueryManager.Get(pagingDataRequest, userId ?? default).Result;
            return tupleResult;
        }

        /// <summary>
        /// Query to get Item by Id
        /// </summary>
        /// <returns></returns>
        public Item GetItembyId(int id)
        {

            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            var tupleResult = _todoItemQueryManager.GetbyId(id, userId ?? default).Result;

            return tupleResult;
        }

        /// <summary>
        /// Query to get all Item
        /// </summary>
        /// <returns></returns>
        public ItemListPaged GetList(PagingDataRequest pagingDataRequest)
        {

            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            var tupleResult = _todoListQueryManager.Get(pagingDataRequest, userId ?? default).Result;

            return tupleResult;
        }

        /// <summary>
        /// Query to get Item by Id
        /// </summary>
        /// <returns></returns>
        public ItemList GetListbyId(int id)
        {

            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            var tupleResult = _todoListQueryManager.GetbyId(id, userId ?? default).Result;

            return tupleResult;
        }
    }
}
