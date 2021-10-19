using Adform.Todo.Model.Models;
using System.Collections.Generic;

namespace Adform.Todo.Dto
{
    public class ItemListPaged
    {
        public List<ItemList> item { get; set; }
        public PagingDataResponse pagingData { get; set; }
    }
}
