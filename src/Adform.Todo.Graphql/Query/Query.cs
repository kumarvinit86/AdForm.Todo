using Adform.Todo.Dto;
using Adform.Todo.Manager;
using System.Linq;
using SimpleInjector;
using Adform.Todo.Essentials.Authentication;
using Adform.Todo.Model.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AutoMapper;

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
            _mapper = container.GetInstance<IMapper>();
        }

        private readonly ILabelQueryManager _labelQueryManager;
        private readonly ITodoItemQueryManager _todoItemQueryManager;
        private readonly IJsonWebTokenHandler _jsonWebTokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITodoListQueryManager _todoListQueryManager;
        private readonly IMapper _mapper;
        /// <summary>
        /// Query to get all labels
        /// </summary>
        /// <returns></returns>
        public IQueryable<Label> GetLabels() => _mapper.Map<IQueryable<Label>>(_labelQueryManager.Get().Result);

        /// <summary>
        /// Query to get label by id
        /// </summary>
        /// <returns></returns>
        public Label GetLabelsbyId(int id) => _mapper.Map<Label>(_labelQueryManager.GetbyId(id).Result);


        /// <summary>
        /// Query to get all Item
        /// </summary>
        /// <returns></returns>
        public ItemPaged<Item> GetItem(PagingDataRequest pagingDataRequest)
        {

            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            var result = _todoItemQueryManager.Get(pagingDataRequest, userId ?? default).Result;
            var pageData = new ItemPaged<Item>
            {
                data = _mapper.Map<List<Item>>(result),
                pagingData = _todoItemQueryManager.pagingResponse
            };
            return pageData;
        }

        /// <summary>
        /// Query to get Item by Id
        /// </summary>
        /// <returns></returns>
        public Item GetItembyId(int id)
        {

            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            var tupleResult = _mapper.Map<Item>(_todoItemQueryManager.GetbyId(id, userId ?? default).Result);
            return tupleResult;
        }

        /// <summary>
        /// Query to get all Item
        /// </summary>
        /// <returns></returns>
        public ItemPaged<ItemList> GetList(PagingDataRequest pagingDataRequest)
        {

            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            var result = _todoListQueryManager.Get(pagingDataRequest, userId ?? default).Result;
            var pageData = new ItemPaged<ItemList>
            {
                data = _mapper.Map<List<ItemList>>(result),
                pagingData = _todoItemQueryManager.pagingResponse
            };
            return pageData;
        }

        /// <summary>
        /// Query to get Item by Id
        /// </summary>
        /// <returns></returns>
        public ItemList GetListbyId(int id)
        {

            var userId = _jsonWebTokenHandler.GetUserIdfromToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            var result = _mapper.Map<ItemList>(_todoListQueryManager.GetbyId(id, userId ?? default).Result);
            return result;
        }
    }
}
