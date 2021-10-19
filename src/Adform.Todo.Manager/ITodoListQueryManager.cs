﻿using Adform.Todo.Dto;
using Adform.Todo.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoListQueryManager
    {
        Task<ItemListPaged> Get(PagingDataRequest pagingData, int userId);
        Task<ItemList> GetbyId(int Id);
    }
}
