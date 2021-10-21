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
    /// To orchestrate the Query Action of todo list.
    /// Transform Entity to Dto.
    /// </summary>
    public class TodoListQueryManager : ITodoListQueryManager
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
        public async Task<ItemListPaged> Get(PagingDataRequest pagingData, int userId)
        {
            var data = (from i in await _todoListQuery.Get(userId)
                        join
                        l in await _labelQueryManager.Get()
                        on i.LabelId equals l.Id
                        join
                        t in await _todoItemQueryManager.GetItem(userId)
                        on i.Id equals t.ListId into ti
                        from p in ti.DefaultIfEmpty()
                        select new ItemList
                        {
                            Id = i.Id,
                            Name = i.Name,
                            LabelName = l.Name,
                            UserId = i.UserId ?? default,
                            ChildItems = _mapper.Map<List<Item>>(ti)
                        }).GroupBy(p => p.Id).Select(g => g.First()).ToList();

            int count = data.Count;
            int CurrentPage = pagingData.PageNumber;
            int PageSize = pagingData.PageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var lists = data.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            var pageMetadata = new PagingDataResponse
            {
                TotalCount = TotalCount,
                PageSize = PageSize,
                CurrentPage = CurrentPage,
                TotalPages = TotalPages,
                PreviousPage = previousPage,
                NextPage = nextPage
            };
            return new ItemListPaged { item = lists, pagingData = pageMetadata };
        }

        /// <summary>
        /// fetch todolist by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ItemList> GetbyId(int id, int userId)
        {
            var todoList = await _todoListQuery.GetbyId(id, userId);
            var label = await _labelQueryManager.GetbyId(todoList.LabelId ?? default);
            var items = _mapper.Map<List<Item>>(await _todoItemQueryManager.GetItem(userId));
            var list = _mapper.Map<ItemList>(todoList);
            if (label != null)
            {
                list.LabelName = label.Name;
            }
            if (items != null)
            {
                list.ChildItems = items;
            }
            return list;
        }

        public async Task<List<ToDoList>> Get(int userId)
        {
            return await _todoListQuery.Get(userId);
        }
    }
}
