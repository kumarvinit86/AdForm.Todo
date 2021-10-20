﻿using Adform.Todo.Dto;
using Adform.Todo.Model.Entity;
using Adform.Todo.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoItemQueryManager
    {
        Task<ItemPaged> Get(PagingDataRequest pagingData, int userId);
        Task<List<ToDoItem>> Get(int userId);
        Task<Item> GetbyId(int Id, int userId);
    }
}
