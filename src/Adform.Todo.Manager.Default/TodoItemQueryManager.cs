using Adform.Todo.DomainService;
using Adform.Todo.Dto;
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
    public class TodoItemQueryManager : ITodoItemQueryManager
    {
        public TodoItemQueryManager(ITodoItemQuery todoItemQuery, IMapper mapper, ILabelQueryManager labelQueryManager)
        {
            _todoItemQuery = todoItemQuery;
            _mapper = mapper;
            _labelQueryManager = labelQueryManager;
        }
        private readonly ITodoItemQuery _todoItemQuery;
        private readonly IMapper _mapper;
        private readonly ILabelQueryManager _labelQueryManager;
        /// <summary>
        /// to fetch the list of item of a user
        /// with the pagination.
        /// </summary>
        /// <param name="pagingData">for current/default page data</param>
        /// <param name="userId"></param>
        /// <returns>Tuple of list of item and the pagination details</returns>
        public async Task<ItemPaged> Get(PagingDataRequest pagingData, int userId)
        {

            var data = (from i in await _todoItemQuery.Get(userId)
                       join
                       l in await _labelQueryManager.Get()
                       on i.LabelId equals l.Id
                       select new Item
                       {
                           Id = i.Id,
                           Name = i.Name,
                           LabelName = l.Name,
                           UserId = i.UserId ?? default
                       }).ToList();
 
            int count = data.Count;
            int CurrentPage = pagingData.PageNumber;
            int PageSize = pagingData.PageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = data.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            var pageMetadata = new PagingDataResponse
            { 
                TotalCount = TotalCount,
                PageSize = PageSize,
                CurrentPage = CurrentPage,
                TotalPages = TotalPages,
                PreviousPage=previousPage,
                NextPage=nextPage
            };

            return new ItemPaged { item = items, pagingData = pageMetadata };
        }
        /// <summary>
        /// fetch item by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Item> GetbyId(int Id, int userId)
        {

            return (from i in await _todoItemQuery.Get(userId)
                        join
                        l in await _labelQueryManager.Get()
                        on i.LabelId equals l.Id
                        where i.Id.Equals(Id)
                        select new Item
                        {
                            Id = i.Id,
                            Name = i.Name,
                            LabelName = l.Name,
                            UserId = i.UserId ?? default
                        }).FirstOrDefault();

        }

        public async Task<List<ToDoItem>> Get(int userId)
        {
            return await _todoItemQuery.Get(userId);
        }
    }
}
