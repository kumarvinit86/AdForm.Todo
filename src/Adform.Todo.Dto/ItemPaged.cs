using Adform.Todo.Model.Models;
using System.Collections.Generic;

namespace Adform.Todo.Dto
{
    public class ItemPaged
    {
        public List<Item> item { get; set; }
        public PagingDataResponse pagingData { get; set; }
    }
}
