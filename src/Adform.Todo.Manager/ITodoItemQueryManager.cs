using Adform.Todo.Dto;
using Adform.Todo.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adform.Todo.Manager
{
    public interface ITodoItemQueryManager
    {
        Task<Tuple<List<Item>, PagingDataResponse>> Get(PagingDataRequest pagingData, int userId);
        Task<Item> GetbyId(int Id);
    }
}
